using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CPS.Logics
{
    class TransformationsLogics
    {
        private Dictionary<string, Complex> _factors = new Dictionary<string, Complex>();
        private Dictionary<string, Complex> _factorsReverse = new Dictionary<string, Complex>();

        public static int GetWalshValue(int i, int j)
        {
            List<int> iBit = TransformationsLogics.Conversion(i);
            List<int> jBit = TransformationsLogics.Conversion(j);

            if (iBit.Count < jBit.Count)
            {
                List<int> smaller = iBit;
                for (int k = Math.Abs(iBit.Count - jBit.Count); k > 0; k--)
                {
                    smaller.Insert(0, 0);
                }
            }
            else if (iBit.Count > jBit.Count)
            {
                List<int> smaller = jBit;
                for (int k = Math.Abs(iBit.Count - jBit.Count); k > 0; k--)
                {
                    smaller.Insert(0, 0);
                }
            }

            int sum = 0;
            for (int k = 0; k < iBit.Count; k++)
            {
                sum += iBit[k] * jBit[k];
            }

            return (int)Math.Pow((-1), sum);
        }

        public static double[][] GetMatrix(int m)
        {
            double[][] result = new double[(int)Math.Pow(2,m)][];
            for (int i = 0; i < (int)Math.Pow(2, m); i++) result[i] = new double[(int)Math.Pow(2, m)];

            if(m <= 0)
            {
                result[0][0] = 1.0;
                return result;
            }
            else
            {
                double[][] help = GetMatrix(m-1);

                for(int j = 0; j < help.Length; j++)
                {
                    for (int k = 0; k < help[j].Length; k++)
                    {
                        result[j][k] = help[j][k];

                        result[help.Length + j][k] = help[j][k];

                        result[j][help[j].Length + k] = help[j][k];

                        result[help.Length + j][help[j].Length + k] = -help[j][k];
                    }
                }

                return result;
            }
        }

        public static double[][] GetOleMatrix(int m)
        {
            double[][] result = new double[(int)Math.Pow(2, m)][];
            for (int i = 0; i < (int)Math.Pow(2, m); i++) result[i] = new double[(int)Math.Pow(2, m)];

            for (int i = 0; i < result.Length; i++)
            {
                for (int j = 0; j < result[i].Length; j++)
                {
                    result[i][j] = 1;
                }
            }

            int i1 = 1;
            while (i1 < (int)Math.Pow(2, m))
            {
                for(int i2 = 0; i2 < i1; i2++)
                {
                    for(int i3 = 0; i3 < i1; i3++)
                    {
                        result[i2 + i1][i3] = result[i2][i3];
                        result[i2][i3 + i1] = result[i2][i3];
                        result[i2 + i1][i3 + i1] = (result[i2][i3] == -1 ? 1 : -1);
                    }
                }
                i1 += i1;
            }

            return result;
        }

        public static List<int> Conversion(int x)
        {
            var bitConversion = new List<int>();
            var result = x;
            while (result >= 0)
            {
                if (result == 0)
                {
                    bitConversion.Add(0);
                    break;
                }
                bitConversion.Add((int)(result % 2));
                result = result / 2;
            }
            bitConversion.Reverse();

            return bitConversion;
        }

        public List<Complex> SwitchSamples(List<Complex> points, bool reverse = false)
        {
            if (points.Count < 2)
            {
                return points;
            }
            List<Complex> oddPoints = new List<Complex>();
            List<Complex> evenPoints = new List<Complex>();
            for (int i = 0; i < points.Count / 2; i++)
            {
                evenPoints.Add(points[i * 2]);
                oddPoints.Add(points[i * 2 + 1]);
            }
            var result = Connect(SwitchSamples(evenPoints, reverse), SwitchSamples(oddPoints, reverse), reverse);
            return result;
        }

        private List<Complex> Connect(List<Complex> evenPoints, List<Complex> oddPoints, bool reverse)
        {
            int N = oddPoints.Count * 2;
            Complex[] result = new Complex[N];
            for (int i = 0; i < oddPoints.Count; i++)
            {
                if (reverse)
                {
                    if (!_factorsReverse.ContainsKey($"{i}, {N}"))
                        _factorsReverse[$"{i}, {N}"] = CalculateReverseFactor(i, 1, N);
                    result[i] = evenPoints[i] + (_factorsReverse[$"{i}, {N}"] * oddPoints[i]);
                    result[i + oddPoints.Count] = evenPoints[i] - (_factorsReverse[$"{i}, {N}"] * oddPoints[i]);
                }
                else
                {
                    if (!_factors.ContainsKey($"{i}, {N}"))
                        _factors[$"{i}, {N}"] = CalculateFactor(i, 1, N);
                    result[i] = evenPoints[i] + (_factors[$"{i}, {N}"] * oddPoints[i]);
                    result[i + oddPoints.Count] = evenPoints[i] - (_factors[$"{i}, {N}"] * oddPoints[i]);
                }

            }
            return result.ToList();
        }

        private Complex CalculateFactor(int m, int n, int N)
        {
            return Complex.Exp(new Complex(0, -2 * Math.PI * m * n / N));
        }
        private Complex CalculateReverseFactor(int m, int n, int N)
        {
            return Complex.Exp(new Complex(0, 2 * Math.PI * m * n / N));
        }
    }
}
