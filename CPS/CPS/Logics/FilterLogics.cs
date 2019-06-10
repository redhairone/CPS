using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveCharts;
using LiveCharts.Defaults;

namespace CPS.Logics
{
    class FilterLogics
    {
        internal static List<double> LowFilter(int m, double k)
        {
            List<double> result = new List<double>();
            int center = (m - 1) / 2;

            for (int i = 0; i < m; i++)
            {
                double factor;
                if(i == center)
                {
                    factor = 2.0 / k;
                }
                else
                {
                    factor = Math.Sin(2 * Math.PI * (i - center) / k) / (Math.PI * (i - center));
                }
                result.Add(factor);
            }

            return result;
        }

        internal static List<double> MediumFilter(List<double> factors)
        {
            List<double> result = new List<double>();

            for (int i = 0; i < factors.Count; i++)
            {
                result.Add(factors[i] * 2 * Math.Sin((Math.PI * i) / 2.0));
            }

            return result;
        }

        internal static List<double> HighFilter(List<double> factors)
        {
            List<double> result = new List<double>();

            for (int i = 0; i < factors.Count; i++)
            {
                result.Add(factors[i] * Math.Pow(-1.0, i));
            }

            return result;
        }

        internal static List<double> RectangularWindow(List<double> factors) => factors;

        internal static List<double> HammingWindow(List<double> factors)
        {
            List<double> results = new List<double>();

            for (int i = 0; i < factors.Count; i++)
            {
                double factor = (0.53836 - (0.46164 * Math.Cos((2 * Math.PI * i) / (factors.Count * 1.0))));
                results.Add(factor * factors[i]);
            }

            return results;
        }

        internal static List<double> HanningWindow(List<double> factors)
        {
            List<double> results = new List<double>();

            for (int i = 0; i < factors.Count; i++)
            {
                double factor = (0.5 - (0.5 * Math.Cos((2 * Math.PI * i) / (factors.Count * 1.0))));
                results.Add(factor * factors[i]);
            }

            return results;
        }

        internal static List<double> BlackmanWindow(List<double> factors)
        {
            List<double> results = new List<double>();

            for (int i = 0; i < factors.Count; i++)
            {
                double factor = (0.42 - (0.5 * Math.Cos((2 * Math.PI * i) / (1.0 * factors.Count))) +
                              0.08 * Math.Cos((4 * Math.PI * i) / (1.0 * factors.Count)));
                results.Add(factor * factors[i]);
            }

            return results;
        }
    }
}
