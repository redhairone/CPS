using LiveCharts;
using LiveCharts.Defaults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPS.Logics
{
    class MathLogics
    {
        internal static double[] GetMinMax(ChartValues<ObservablePoint> samples)
        {

            double[] result = new double[] { samples[0].Y, samples[0].Y };

            for (int i = 1; i < samples.Count; i++)
            {
                if (samples[i].Y > result[1]) result[1] = samples[i].Y;
                else if (samples[i].Y < result[0]) result[0] = samples[i].Y;
            }




            return result;
        }

        internal static double Sum(ChartValues<ObservablePoint> values, Func<double, double> fun = null)
        {
            double result = 0;
            if(fun == null)
            {
                foreach (var item in values) result += item.Y;
            } 
            else
            {
                foreach (var item in values) result += fun(item.Y);
            }
            return result;
        }

        internal static double Sinc(double p)
        {
            if (p == 0.0) return 1.0;
            else return Math.Sin(Math.PI * p) / (Math.PI * p);
        }

        internal static ChartValues<ObservablePoint> AdjustSize(ChartValues<ObservablePoint> values, int size)
        {
            ChartValues<ObservablePoint> result = new ChartValues<ObservablePoint>();

            foreach(var item in values)
            {
                for(int j = 0; j < size/values.Count + 1; j++)
                {
                    result.Add( new ObservablePoint { X = item.X, Y = item.Y });
                }
            }

            return result;
        }

        internal static IEnumerable<ObservablePoint> SegregateSamples(ChartValues<ObservablePoint> samples, int seenSamplesAmount, double time)
        {
            return samples.OrderBy(s => (Math.Abs(s.X - time))).Take(seenSamplesAmount*2);
        }

        internal static ChartValues<ObservablePoint> GetProperSignal(ChartValues<ObservablePoint> sincValues, Func<double, double> fun)
        {
            ChartValues<ObservablePoint> result = new ChartValues<ObservablePoint>();

            foreach (var item in sincValues)
            {
                result.Add(new ObservablePoint { X = item.X, Y = fun(item.X) });
            }

            return result;
        }
    }
}
