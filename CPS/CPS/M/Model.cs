using CPS.Logics;
using LiveCharts;
using LiveCharts.Defaults;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;

namespace CPS.M
{
    class Model
    {
        internal IChartValues GetUniformDistributionNoise(int signalFrequency, double timeDuration, double minimum, double maximum)
        {
            double[] xValues = Logics.SignalLogics.GetTimeValues(signalFrequency, timeDuration);
            ChartValues<ObservablePoint> result = new ChartValues<ObservablePoint>();

            for (int i = 0; i < xValues.Length; i++)
            {
                result.Add(new ObservablePoint { X = xValues[i], Y = Logics.SignalLogics.GetUniformDistributionNoiseValue(minimum, maximum) });
            }

            return result;
        }

        internal IChartValues GetGaussianNoise(int signalFrequency, double timeDuration, double amplitude, double mean, double variance)
        {
            double[] xValues = Logics.SignalLogics.GetTimeValues(signalFrequency, timeDuration);
            ChartValues<ObservablePoint> result = new ChartValues<ObservablePoint>();

            for (int i = 0; i < xValues.Length; i++)
            {
                result.Add(new ObservablePoint { X = xValues[i], Y = Logics.SignalLogics.GetGaussianNoiseValue(amplitude, mean, variance) });
            }

            return result;
        }

        internal IChartValues GetSinSignal(int signalFrequency, double timeDuration, double amplitude, double frequency, double startTime)
        {
            double[] xValues = Logics.SignalLogics.GetTimeValues(signalFrequency, timeDuration, startTime);
            ChartValues<ObservablePoint> result = new ChartValues<ObservablePoint>();

            for (int i = 0; i < xValues.Length; i++)
            {
                result.Add(new ObservablePoint { X = xValues[i], Y = Logics.SignalLogics.GetSinValue(xValues[i], amplitude, frequency) });
            }

            return result;
        }

        internal IChartValues GetSinAbsSignal(int signalFrequency, double timeDuration, double amplitude, double frequency, double startTime)
        {
            double[] xValues = Logics.SignalLogics.GetTimeValues(signalFrequency, timeDuration, startTime);
            ChartValues<ObservablePoint> result = new ChartValues<ObservablePoint>();

            for (int i = 0; i < xValues.Length; i++)
            {
                result.Add(new ObservablePoint { X = xValues[i], Y = Logics.SignalLogics.GetSinAbsValue(xValues[i], amplitude, frequency) });
            }

            return result;
        }

        internal IChartValues GetSinDoubleAbsSignal(int signalFrequency, double timeDuration, double amplitude, double frequency, double startTime)
        {
            double[] xValues = Logics.SignalLogics.GetTimeValues(signalFrequency, timeDuration, startTime);
            ChartValues<ObservablePoint> result = new ChartValues<ObservablePoint>();

            for (int i = 0; i < xValues.Length; i++)
            {
                result.Add(new ObservablePoint { X = xValues[i], Y = Logics.SignalLogics.GetSinDoubleAbsValue(xValues[i], amplitude, frequency) });
            }

            return result;
        }

        internal IChartValues GetRectangularSignal(int signalFrequency, double timeDuration, double amplitude, double period, double startTime, double dutyCycle)
        {
            double[] xValues = Logics.SignalLogics.GetTimeValues(signalFrequency, timeDuration, startTime);
            ChartValues<ObservablePoint> result = new ChartValues<ObservablePoint>();

            for (int i = 0; i < xValues.Length; i++)
            {
                result.Add(new ObservablePoint { X = xValues[i], Y = Logics.SignalLogics.GetRectangularSignalValue(xValues[i], amplitude, period, startTime, dutyCycle) });
            }

            return result;
        }

        internal IChartValues GetSymmetricRectangularSignal(int signalFrequency, double timeDuration, double amplitude, double period, double startTime, double dutyCycle)
        {
            double[] xValues = Logics.SignalLogics.GetTimeValues(signalFrequency, timeDuration, startTime);
            ChartValues<ObservablePoint> result = new ChartValues<ObservablePoint>();

            for (int i = 0; i < xValues.Length; i++)
            {
                result.Add(new ObservablePoint { X = xValues[i], Y = Logics.SignalLogics.GetSymmetricRectangularSignalValue(xValues[i], amplitude, period, startTime, dutyCycle) });
            }

            return result;
        }

        internal IChartValues GetTriangularSignal(int signalFrequency, double timeDuration, double amplitude, double period, double startTime, double dutyCycle)
        {
            double[] xValues = Logics.SignalLogics.GetTimeValues(signalFrequency, timeDuration, startTime);
            ChartValues<ObservablePoint> result = new ChartValues<ObservablePoint>();

            for (int i = 0; i < xValues.Length; i++)
            {
                result.Add(new ObservablePoint { X = xValues[i], Y = Logics.SignalLogics.GetTriangularSignalValue(xValues[i], amplitude, period, startTime, dutyCycle) });
            }

            return result;
        }

        internal IChartValues GetJumpSignal(int signalFrequency, double timeDuration, double amplitude, double jumpTime, double startTime)
        {
            double[] xValues = Logics.SignalLogics.GetTimeValues(signalFrequency, timeDuration, startTime);
            ChartValues<ObservablePoint> result = new ChartValues<ObservablePoint>();

            for (int i = 0; i < xValues.Length; i++)
            {
                result.Add(new ObservablePoint { X = xValues[i], Y = Logics.SignalLogics.GetJumpSignalValue(xValues[i], amplitude, jumpTime, startTime) });
            }

            return result;
        }

        internal IChartValues GetSingleImpulseSignal(int signalFrequency, double timeDuration, double amplitude, double jumpTime, double startTime)
        {
            double[] xValues = Logics.SignalLogics.GetTimeValues(signalFrequency, timeDuration, startTime);
            ChartValues<ObservablePoint> result = new ChartValues<ObservablePoint>();

            for (int i = 0; i < xValues.Length; i++)
            {
                result.Add(new ObservablePoint { X = xValues[i], Y = Logics.SignalLogics.GetSingleImpulseSignalValue(xValues[i], amplitude, jumpTime, startTime) });
            }

            return result;
        }

        internal IChartValues GetImpulseNoise(int signalFrequency, double timeDuration, double amplitude, double impulsProbability)
        {
            double[] xValues = Logics.SignalLogics.GetTimeValues(signalFrequency, timeDuration);
            ChartValues<ObservablePoint> result = new ChartValues<ObservablePoint>();

            for (int i = 0; i < xValues.Length; i++)
            {
                result.Add(new ObservablePoint { X = xValues[i], Y = Logics.SignalLogics.GetImpulseNoiseValue(xValues[i], amplitude, impulsProbability) });
            }

            return result;
        }

        internal IChartValues GetSincReconstruction(int reconstructionFrequency, int samplingFrequency, double timeDuration, ChartValues<ObservablePoint> samples, int seenSamplesAmount)
        {
            double[] xValues = Logics.SignalLogics.GetTimeValues(reconstructionFrequency, timeDuration);
            ChartValues<ObservablePoint> result = new ChartValues<ObservablePoint>();
            
            if(seenSamplesAmount != 0)
            {
                foreach (var item in xValues)
                {
                    result.Add(new ObservablePoint { X = item, Y = Logics.SignalLogics.GetSincReconstructionValue(MathLogics.SegregateSamples(samples, seenSamplesAmount, item), item, samplingFrequency) });
                }
            }
            else
            {
                foreach (var item in xValues)
                {
                    result.Add(new ObservablePoint { X = item, Y = Logics.SignalLogics.GetSincReconstructionValue(samples, item, samplingFrequency) });
                }
            }
            
            return result;
        }

        internal IChartValues GetHistogram(ChartValues<ObservablePoint> samples, int howManySections = 10)
        {
            ChartValues<int> result = new ChartValues<int>();
            double[] minMax = Logics.MathLogics.GetMinMax(samples);
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
            double[] minMax = Logics.MathLogics.GetMinMax(samples), yValues = new double[howManyLevels];
            double step = Math.Abs(minMax[1] - minMax[0]) / (howManyLevels - 1);
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

        internal void Serialize(DataCapsule capsule)
        {
            SerializationLogics.Serialize(capsule);
        }

        internal DataCapsule Deserialize()
        {
            return SerializationLogics.Deserialize();
        }

        internal IChartValues Add()
        {
            DataCapsule[] loaded = SerializationLogics.LoadFiles();

            return loaded[0].Add(loaded[1]).GetValues();
        }

        internal IChartValues Subtract()
        {
            DataCapsule[] loaded = SerializationLogics.LoadFiles();

            return loaded[0].Subtract(loaded[1]).GetValues();
        }

        internal IChartValues Multiply()
        {
            DataCapsule[] loaded = SerializationLogics.LoadFiles();

            return loaded[0].Multiply(loaded[1]).GetValues();
        }

        internal IChartValues Divide()
        {
            DataCapsule[] loaded = SerializationLogics.LoadFiles();

            return loaded[0].Divide(loaded[1]).GetValues();
        }

        internal IChartValues Weave()
        {
            DataCapsule[] loaded = SerializationLogics.LoadFiles();

            return loaded[0].Weave(loaded[1]).GetValues();
        }
        
        internal IChartValues Correlation()
        {
            DataCapsule[] loaded = SerializationLogics.LoadFiles();

            return loaded[0].Correlation(loaded[1]).GetValues();
        }

        internal IChartValues WeaveCorrelation()
        {
            DataCapsule[] loaded = SerializationLogics.LoadFiles();

            return loaded[0].WeaveCorrelation(loaded[1]).GetValues();
        }

        internal IChartValues Filter(int m, int cutOffFrequency, int windowChoice, int filterChoice)
        {
            DataCapsule loaded = SerializationLogics.Deserialize();
            List<double> factors, filtered;

            switch(filterChoice)
            {
                case 0:
                    factors = FilterLogics.LowFilter(m, loaded.SamplingFrequency / cutOffFrequency);
                    break;
                case 1:
                    factors = FilterLogics.MediumFilter(FilterLogics.LowFilter(m, (loaded.SamplingFrequency / (loaded.SamplingFrequency / 4.0 - cutOffFrequency))));
                    break;
                case 2:
                    factors = FilterLogics.HighFilter(FilterLogics.LowFilter(m, (loaded.SamplingFrequency / (loaded.SamplingFrequency / 2.0 - cutOffFrequency))));
                    break;
                default:
                    throw new Exception("The filter combobox is out of its boundries.");
            }

            switch(windowChoice)
            {
                case 0:
                    filtered = factors;
                    break;
                case 1:
                    filtered = FilterLogics.HammingWindow(factors);
                    break;
                case 2:
                    filtered = FilterLogics.HanningWindow(factors);
                    break;
                case 3:
                    filtered = FilterLogics.BlackmanWindow(factors);
                    break;
                default:
                    throw new Exception("The window combobox is out of its boundries.");
            }

            return loaded.Weave(filtered).GetValues();
        }
    }
}
