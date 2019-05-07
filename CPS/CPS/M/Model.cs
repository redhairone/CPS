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

        internal IChartValues GetSincReconstruction(int reconstructionFrequency, double timeDuration, ChartValues<ObservablePoint> samples)
        {
            double[] xValues = Logic.GetTimeValues(reconstructionFrequency, timeDuration);
            ChartValues<ObservablePoint> result = new ChartValues<ObservablePoint>();

            for (int i = 0; i < xValues.Length; i++)
            {
                result.Add(new ObservablePoint { X = xValues[i], Y = Logic.GetSincReconstructionValue(samples, xValues[i], reconstructionFrequency) });
            }

            return result;
        }

        internal IChartValues GetHistogram(ChartValues<ObservablePoint> samples, int howManySections = 10)
        {
            ChartValues<int> result = new ChartValues<int>();
            double[] minMax = Logic.GetMinMax(samples);
            int[] resultHelp = new int[howManySections];
            double step = Math.Abs(minMax[1] - minMax[0]) / howManySections;

            Array.Clear(resultHelp, 0, howManySections);

            for(int i = 0; i < samples.Count; i++)
            {
                for(int j = howManySections-1; j >= 0; j--)
                {
                    if(samples[i].Y > (minMax[0] + j * step))
                    {
                        resultHelp[j] += 1;
                        break;
                    }
                }
            }

            for(int i = 0; i < howManySections; i++)
            {
                result.Add(resultHelp[i]);
            }

            return result;
        }

        internal IChartValues GetQuant(ChartValues<ObservablePoint> samples, int howManyLevels)
        {
            ChartValues<ObservablePoint> result = new ChartValues<ObservablePoint>();
            double[] minMax = Logic.GetMinMax(samples), yValues = new double[howManyLevels];
            double step = Math.Abs(minMax[1] - minMax[0]) / howManyLevels;
            int closestIndex = 0;

            for(int i = 0; i < howManyLevels; i++)
            {
                yValues[i] = minMax[0] + i * step;
            }

            for(int i = 0; i < samples.Count; i++)
            {
                for(int j = 1; j < howManyLevels; j++)
                {
                    if (Math.Abs(yValues[j] - samples[i].Y) < Math.Abs(yValues[closestIndex] - samples[i].Y)) closestIndex = j;
                }

                result.Add(new ObservablePoint { X = samples[i].X, Y = yValues[closestIndex] });
                closestIndex = 0;
            }

            return result;
        }
    }
}
