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

        public static double[] GetTimeValues(int samplesAmount, double timeDuration)
        {
            double[] result = new double[samplesAmount+1];
            double step = timeDuration / samplesAmount;

            result[0] = 0;

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
    }
}
