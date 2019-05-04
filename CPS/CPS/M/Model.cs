using CPS.Logics;
using LiveCharts;
using LiveCharts.Defaults;
using System;

namespace CPS.M
{
    class Model
    {
        internal IChartValues GetUniformDistributionNoise(int samplesAmount, double timeDuration, double minimum, double maximum)
        {
            double[] xValues = Logic.GetTimeValues(samplesAmount, timeDuration);
            ChartValues<ObservablePoint> result = new ChartValues<ObservablePoint>();

            for (int i = 0; i < xValues.Length; i++)
            {
                result.Add(new ObservablePoint { X = xValues[i], Y = Logic.GetUniformDistributionNoiseValue(minimum, maximum) });
            }

            return result;
        }

        internal IChartValues GetGaussianNoise(int samplesAmount, double timeDuration, double amplitude, double mean, double variance)
        {
            double[] xValues = Logic.GetTimeValues(samplesAmount, timeDuration);
            ChartValues<ObservablePoint> result = new ChartValues<ObservablePoint>();

            for (int i = 0; i < xValues.Length; i++)
            {
                result.Add(new ObservablePoint { X = xValues[i], Y = Logic.GetGaussianNoiseValue(amplitude, mean, variance) });
            }

            return result;
        }

        internal IChartValues GetSinSignal(int samplesAmount, double timeDuration, double amplitude, double frequency, double startTime)
        {
            double[] xValues = Logic.GetTimeValues(samplesAmount, timeDuration, startTime);
            ChartValues<ObservablePoint> result = new ChartValues<ObservablePoint>();

            for (int i = 0; i < xValues.Length; i++)
            {
                result.Add(new ObservablePoint { X = xValues[i], Y = Logic.GetSinValue(xValues[i], amplitude, frequency) });
            }

            return result;
        }

        internal IChartValues GetSinAbsSignal(int samplesAmount, double timeDuration, double amplitude, double frequency, double startTime)
        {
            double[] xValues = Logic.GetTimeValues(samplesAmount, timeDuration, startTime);
            ChartValues<ObservablePoint> result = new ChartValues<ObservablePoint>();

            for (int i = 0; i < xValues.Length; i++)
            {
                result.Add(new ObservablePoint { X = xValues[i], Y = Logic.GetSinAbsValue(xValues[i], amplitude, frequency) });
            }

            return result;
        }

        internal IChartValues GetSinDoubleAbsSignal(int samplesAmount, double timeDuration, double amplitude, double frequency, double startTime)
        {
            double[] xValues = Logic.GetTimeValues(samplesAmount, timeDuration, startTime);
            ChartValues<ObservablePoint> result = new ChartValues<ObservablePoint>();

            for (int i = 0; i < xValues.Length; i++)
            {
                result.Add(new ObservablePoint { X = xValues[i], Y = Logic.GetSinDoubleAbsValue(xValues[i], amplitude, frequency) });
            }

            return result;
        }

        internal IChartValues GetRectangularSignal(int v1, double v2, double v3, double v4, double v5, double v6)
        {
            throw new NotImplementedException();
        }

        internal IChartValues GetSymmetricRectangularSignal(int v1, double v2, double v3, double v4, double v5, double v6)
        {
            throw new NotImplementedException();
        }

        internal IChartValues GetTriangularSignal(int v1, double v2, double v3, double v4, double v5, double v6)
        {
            throw new NotImplementedException();
        }

        internal IChartValues GetJumpSignal(int v1, double v2, double v3, double v4, double v5)
        {
            throw new NotImplementedException();
        }

        internal IChartValues GetSingleImpulseSignal(int v1, double v2, double v3, double v4, double v5)
        {
            throw new NotImplementedException();
        }

        internal IChartValues GetImpulseNoise(int v1, double v2, double v3, double v4)
        {
            throw new NotImplementedException();
        }
    }
}
