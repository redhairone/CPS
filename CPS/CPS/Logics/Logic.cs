using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPS.Logics
{
    class Logic
    {
        private static Random Random = new Random();

        public static double[] GetTimeValues(int samplesAmount, double timeDuration, double startTime = 0)
        {
            double[] result = new double[samplesAmount+1];
            double step = timeDuration / samplesAmount;

            result[0] = startTime;

            for(int i = 1; i < result.Length; i++)
            {
                result[i] = result[i - 1] + step;
            }

            return result;
        }

        public static double GetUniformDistributionNoiseValue(double minimum, double maximum)
        {
            return (minimum + (maximum - minimum) * Random.NextDouble());
        }

        internal static double GetGaussianNoiseValue(double amplitude, double mean, double variance)
        {
            return (amplitude / 3) * (Math.Sqrt(-2.0 * Math.Log(1.0 - Random.NextDouble())) * Math.Sin(2.0 * Math.PI * (1.0 - Random.NextDouble())));
        }

        internal static double GetSinValue(double time, double amplitude, double frequency)
        {
            return amplitude * Math.Sin(((2 * Math.PI) / frequency) * (time));
        }

        internal static double GetSinAbsValue(double time, double amplitude, double frequency)
        {
            return (Math.Sin(((2 * Math.PI) / frequency) * time) + Math.Abs(Math.Sin(((2 * Math.PI) / frequency) * time))) * 0.5 * amplitude;
        }

        internal static double GetSinDoubleAbsValue(double time, double amplitude, double frequency)
        {
            return Math.Abs(Logic.GetSinValue(time, amplitude, frequency));
        }
    }
}
