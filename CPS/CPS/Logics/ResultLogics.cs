using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CPS.VM;
using LiveCharts;
using LiveCharts.Defaults;

namespace CPS.Logics
{
    class ResultLogics
    {
        internal static double GetAverage(ChartValues<ObservablePoint> values)
        {
            return Math.Round(MathLogics.Sum(values) / values.Count, 4);
        }

        internal static double GetAbsAverage(ChartValues<ObservablePoint> values)
        {
            return Math.Round(MathLogics.Sum(values, v => Math.Abs(v)) / values.Count, 4);
        }

        internal static double GetVariation(ChartValues<ObservablePoint> values)
        {
            return Math.Round(MathLogics.Sum(values, v => Math.Pow(v - ResultLogics.GetAverage(values), 2)) / values.Count, 4);
        }

        internal static double GetAveragePower(ChartValues<ObservablePoint> values)
        {
            return Math.Round(MathLogics.Sum(values, v => Math.Pow(v, 2)) / values.Count, 4);
        }

        internal static double GetRootMeanSquare(ChartValues<ObservablePoint> values)
        {
            return Math.Round(Math.Sqrt(ResultLogics.GetAveragePower(values)), 4);
        }

        internal static double GetMeanSquareError(ChartValues<ObservablePoint> sincValues, ChartValues<ObservablePoint> signalValues)
        {

            ChartValues<ObservablePoint> valuesOne, valuesTwo, a;
            if(sincValues.Count > signalValues.Count)
            {
                valuesOne = sincValues;
                valuesTwo = MathLogics.AdjustSize(signalValues, sincValues.Count);
            }
            else if (sincValues.Count < signalValues.Count)
            {
                valuesOne = signalValues;
                valuesTwo = MathLogics.AdjustSize(sincValues, signalValues.Count);
            }
            else
            {
                valuesOne = signalValues;
                valuesTwo = sincValues;
            }

            if (valuesOne.Count > valuesTwo.Count) a = valuesTwo;
            else a = valuesOne;

            double fraction = 1.0 / valuesOne.Count, sum = 0;

            for(int i = 0; i < valuesOne.Count; i++)
            {
                sum += Math.Pow(valuesOne[i].Y - valuesTwo[i].Y, 2);
            }

            return Math.Round(fraction * sum, 4);
        }

        internal static double GetRatio(ChartValues<ObservablePoint> sincValues, ChartValues<ObservablePoint> signalValues)
        {
            ChartValues<ObservablePoint> sinc, signal, a;
            if (sincValues.Count > signalValues.Count)
            {
                sinc = sincValues;
                signal = MathLogics.AdjustSize(signalValues, sincValues.Count);
            }
            else if(sincValues.Count < signalValues.Count)
            {
                signal = signalValues;
                sinc = MathLogics.AdjustSize(sincValues, signalValues.Count);
            }
            else
            {
                signal = signalValues;
                sinc = sincValues;
            }

            if (signal.Count > sinc.Count) a = sinc;
            else a = signal;

            double numerator = 0, denominator = 0;
            
            for(int i = 0; i < a.Count; i++)
            {
                numerator += Math.Pow(signal[i].Y, 2);
                denominator += Math.Pow(signal[i].Y - sinc[i].Y, 2);
            }

            return Math.Round(10 * Math.Log10(numerator / denominator), 4);
        }

        internal static double GetMaxRatio(ChartValues<ObservablePoint> sincValues, ChartValues<ObservablePoint> signalValues)
        {
            ChartValues<ObservablePoint> values;
            if (sincValues.Count > signalValues.Count)
            {
                values = MathLogics.AdjustSize(signalValues, sincValues.Count);
            }
            else if (sincValues.Count < signalValues.Count)
            {
                values = MathLogics.AdjustSize(sincValues, signalValues.Count);
            }
            else
            {
                values = sincValues;
            }

            double mse = ResultLogics.GetMeanSquareError(sincValues, signalValues), numerator = values[0].Y;
            for(int i = 1; i < values.Count; i++)
            {
                if (values[i].Y > numerator) numerator = values[i].Y;
            }

            return Math.Round(10 * Math.Log10(numerator / mse), 4);
        }

        internal static double GetMaxDiffrence(ChartValues<ObservablePoint> sincValues, ChartValues<ObservablePoint> signalValues)
        {
            ChartValues<ObservablePoint> sinc, signal, a;
            if (sincValues.Count > signalValues.Count)
            {
                sinc = sincValues;
                signal = MathLogics.AdjustSize(signalValues, sincValues.Count);
            }
            else if (sincValues.Count < signalValues.Count)
            {
                signal = signalValues;
                sinc = MathLogics.AdjustSize(sincValues, signalValues.Count);
            }
            else
            {
                signal = signalValues;
                sinc = sincValues;
            }

            if (signal.Count > sinc.Count) a = sinc;
            else a = signal;

            double diff = Math.Abs(sinc[0].Y - signal[0].Y);

            for (int i = 1; i < a.Count; i++)
            {
                if (diff < Math.Abs(sinc[i].Y - signal[i].Y))
                {
                    diff = Math.Abs(sinc[i].Y - signal[i].Y);
                }
            }

            return Math.Round(diff, 4);
        }

    }
}
