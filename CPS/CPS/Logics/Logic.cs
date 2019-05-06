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

        public static double[] GetTimeValues(int signalFrequency, double timeDuration, double startTime = 0)
        {
            Console.WriteLine((int)(timeDuration / (signalFrequency / 1000.0) + 1.0));
            double[] result = new double[(int)(timeDuration/(signalFrequency/1000.0) + 1.0)];
            double step = signalFrequency / 1000.0;

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

        internal static double GetRectangularSignalValue(double time, double amplitude, double period, double startTime, double dutyCycle)
        {
            int k = (int)((time / period) - (startTime / period));
            if ((time >= k * period + startTime) && (time < dutyCycle * period + k * period + startTime))
            {
                return amplitude;
            }
            else return 0;
        }

        internal static double GetSymmetricRectangularSignalValue(double time, double amplitude, double period, double startTime, double dutyCycle)
        {
            int k = (int)((time / period) - (startTime / period));
            if ((time >= k * period + startTime) && (time < dutyCycle * period + k * period + startTime))
            {
                return amplitude;
            }
            else return -amplitude;
        }

        internal static double GetTriangularSignalValue(double time, double amplitude, double period, double startTime, double dutyCycle)
        {
            int k = (int)((time / period) - (startTime / period));
            if(time >= k * period  + startTime && time < dutyCycle * period + k * period + startTime)
            {
                return (amplitude / (dutyCycle * period)) * (time - k * period - startTime);
            } else
            {
                return -amplitude / (period * (1 - dutyCycle)) * (time - k * period - startTime) + (amplitude / (1 - dutyCycle));
            }
        }

        internal static double GetJumpSignalValue(double time, double amplitude, double jumpTime, double startTime)
        {
            if(time > jumpTime)
            {
                return amplitude;
            }
            else if (time < jumpTime)
            {
                return 0;
            }
            else
            {
                return 0.5 * amplitude;
            }
        }

        internal static double GetSingleImpulseSignalValue(double time, double amplitude, double jumpTime, double startTime)
        {
            if (Math.Abs(time - jumpTime) < 0.0001) return amplitude;
            else return 0;
        }

        internal static double GetImpulseNoiseValue(double v, double amplitude, double impulsProbability)
        {
            if (impulsProbability > Random.NextDouble()) return amplitude;
            else return 0;
        }
    }
}
