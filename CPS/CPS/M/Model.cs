using CPS.Logics;
using LiveCharts;
using LiveCharts.Defaults;
using System;

namespace CPS.M
{
    class Model
    {
        internal IChartValues GetUniformDistributionNoise(int signalFrequency, double timeDuration, double minimum, double maximum)
        {
            double[] xValues = Logic.GetTimeValues(signalFrequency, timeDuration);
            ChartValues<ObservablePoint> result = new ChartValues<ObservablePoint>();

            for (int i = 0; i < xValues.Length; i++)
            {
                result.Add(new ObservablePoint { X = xValues[i], Y = Logic.GetUniformDistributionNoiseValue(minimum, maximum) });
            }

            return result;
        }

        internal IChartValues GetGaussianNoise(int signalFrequency, double timeDuration, double amplitude, double mean, double variance)
        {
            double[] xValues = Logic.GetTimeValues(signalFrequency, timeDuration);
            ChartValues<ObservablePoint> result = new ChartValues<ObservablePoint>();

            for (int i = 0; i < xValues.Length; i++)
            {
                result.Add(new ObservablePoint { X = xValues[i], Y = Logic.GetGaussianNoiseValue(amplitude, mean, variance) });
            }

            return result;
        }

        internal IChartValues GetSinSignal(int signalFrequency, double timeDuration, double amplitude, double frequency, double startTime)
        {
            double[] xValues = Logic.GetTimeValues(signalFrequency, timeDuration, startTime);
            ChartValues<ObservablePoint> result = new ChartValues<ObservablePoint>();

            for (int i = 0; i < xValues.Length; i++)
            {
                result.Add(new ObservablePoint { X = xValues[i], Y = Logic.GetSinValue(xValues[i], amplitude, frequency) });
            }

            return result;
        }

        internal IChartValues GetSinAbsSignal(int signalFrequency, double timeDuration, double amplitude, double frequency, double startTime)
        {
            double[] xValues = Logic.GetTimeValues(signalFrequency, timeDuration, startTime);
            ChartValues<ObservablePoint> result = new ChartValues<ObservablePoint>();

            for (int i = 0; i < xValues.Length; i++)
            {
                result.Add(new ObservablePoint { X = xValues[i], Y = Logic.GetSinAbsValue(xValues[i], amplitude, frequency) });
            }

            return result;
        }

        internal IChartValues GetSinDoubleAbsSignal(int signalFrequency, double timeDuration, double amplitude, double frequency, double startTime)
        {
            double[] xValues = Logic.GetTimeValues(signalFrequency, timeDuration, startTime);
            ChartValues<ObservablePoint> result = new ChartValues<ObservablePoint>();

            for (int i = 0; i < xValues.Length; i++)
            {
                result.Add(new ObservablePoint { X = xValues[i], Y = Logic.GetSinDoubleAbsValue(xValues[i], amplitude, frequency) });
            }

            return result;
        }

        internal IChartValues GetRectangularSignal(int signalFrequency, double timeDuration, double amplitude, double period, double startTime, double dutyCycle)
        {
            double[] xValues = Logic.GetTimeValues(signalFrequency, timeDuration, startTime);
            ChartValues<ObservablePoint> result = new ChartValues<ObservablePoint>();

            for (int i = 0; i < xValues.Length; i++)
            {
                result.Add(new ObservablePoint { X = xValues[i], Y = Logic.GetRectangularSignalValue(xValues[i], amplitude, period, startTime, dutyCycle) });
            }

            return result;
        }

        internal IChartValues GetSymmetricRectangularSignal(int signalFrequency, double timeDuration, double amplitude, double period, double startTime, double dutyCycle)
        {
            double[] xValues = Logic.GetTimeValues(signalFrequency, timeDuration, startTime);
            ChartValues<ObservablePoint> result = new ChartValues<ObservablePoint>();

            for (int i = 0; i < xValues.Length; i++)
            {
                result.Add(new ObservablePoint { X = xValues[i], Y = Logic.GetSymmetricRectangularSignalValue(xValues[i], amplitude, period, startTime, dutyCycle) });
            }

            return result;
        }

        internal IChartValues GetTriangularSignal(int signalFrequency, double timeDuration, double amplitude, double period, double startTime, double dutyCycle)
        {
            double[] xValues = Logic.GetTimeValues(signalFrequency, timeDuration, startTime);
            ChartValues<ObservablePoint> result = new ChartValues<ObservablePoint>();

            for (int i = 0; i < xValues.Length; i++)
            {
                result.Add(new ObservablePoint { X = xValues[i], Y = Logic.GetTriangularSignalValue(xValues[i], amplitude, period, startTime, dutyCycle) });
            }

            return result;
        }

        internal IChartValues GetJumpSignal(int signalFrequency, double timeDuration, double amplitude, double jumpTime, double startTime)
        {
            double[] xValues = Logic.GetTimeValues(signalFrequency, timeDuration, startTime);
            ChartValues<ObservablePoint> result = new ChartValues<ObservablePoint>();

            for (int i = 0; i < xValues.Length; i++)
            {
                result.Add(new ObservablePoint { X = xValues[i], Y = Logic.GetJumpSignalValue(xValues[i], amplitude, jumpTime, startTime) });
            }

            return result;
        }

        internal IChartValues GetSingleImpulseSignal(int signalFrequency, double timeDuration, double amplitude, double jumpTime, double startTime)
        {
            double[] xValues = Logic.GetTimeValues(signalFrequency, timeDuration, startTime);
            ChartValues<ObservablePoint> result = new ChartValues<ObservablePoint>();

            for (int i = 0; i < xValues.Length; i++)
            {
                result.Add(new ObservablePoint { X = xValues[i], Y = Logic.GetSingleImpulseSignalValue(xValues[i], amplitude, jumpTime, startTime) });
            }

            return result;
        }

        internal IChartValues GetImpulseNoise(int signalFrequency, double timeDuration, double amplitude, double impulsProbability)
        {
            double[] xValues = Logic.GetTimeValues(signalFrequency, timeDuration);
            ChartValues<ObservablePoint> result = new ChartValues<ObservablePoint>();

            for (int i = 0; i < xValues.Length; i++)
            {
                result.Add(new ObservablePoint { X = xValues[i], Y = Logic.GetImpulseNoiseValue(xValues[i], amplitude, impulsProbability) });
            }

            return result;
        }
    }
}
