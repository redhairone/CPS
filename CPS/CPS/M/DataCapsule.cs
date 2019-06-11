using CPS.Logics;
using LiveCharts;
using LiveCharts.Defaults;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CPS.M
{
    [Serializable]
    class DataCapsule
    {
        public List<double> XValues { get; set; }
        public List<double> YValues { get; set; }
        public int SamplingFrequency { get; set; }

        public DataCapsule(ChartValues<ObservablePoint> values, int frequency = 0)
        {
            XValues = new List<double>();
            YValues = new List<double>();

            SamplingFrequency = frequency;

            foreach(var item in values)
            {
                XValues.Add(item.X);
                YValues.Add(item.Y);
            }
        }

        public DataCapsule(DataCapsule dataCapsule)
        {
            XValues = new List<double>(dataCapsule.XValues);
            YValues = new List<double>(dataCapsule.YValues);

            SamplingFrequency = dataCapsule.SamplingFrequency;
        }

        public DataCapsule(DataCapsule dataCapsule, int samplesToMove)
        {
            List<double> pointsLeft = dataCapsule.YValues.Take(dataCapsule.YValues.Count - samplesToMove).ToList();
            List<double> receivedSignal = dataCapsule.YValues.Skip(dataCapsule.YValues.Count - samplesToMove).ToList();
            receivedSignal.AddRange(pointsLeft);

            XValues = new List<double>(dataCapsule.XValues);
            YValues = receivedSignal;

            SamplingFrequency = dataCapsule.SamplingFrequency;
        }

        public DataCapsule(List<double> xValues, List<double> yValues, int frequency = 0)
        {
            XValues = xValues;
            YValues = yValues;

            SamplingFrequency = frequency;
        }

        public DataCapsule(List<double> yValues, int frequency)
        {
            YValues = yValues;
            XValues = new List<double>();

            for (int i = 0; i < yValues.Count; i++)
            {
                XValues.Add(i * (1.0 / frequency));
            }

            SamplingFrequency = frequency;
        }

        public DataCapsule(double end, List<double> yValues, int frequency = 0)
        {
            YValues = yValues;
            XValues = new List<double>();

            double freq = end / yValues.Count;

            for(int i = 0; i < yValues.Count; i++)
            {
                XValues.Add(i * (1.0/frequency));
            }

            SamplingFrequency = frequency;
        }


        public IChartValues GetValues()
        {
            ChartValues<ObservablePoint> values = new ChartValues<ObservablePoint>();
            if (XValues.Count == YValues.Count && XValues.Count != 0)
            {
                for (int i = 0; i < XValues.Count; i++)
                {
                    values.Add(new ObservablePoint { X = XValues[i], Y = YValues[i] });
                }
            }
            else throw new Exception("The amounts of X values is not equal the amount of Y values.");
            return values;
        }

        internal DataCapsule Add(DataCapsule dataCapsule)
        {
            ChartValues<ObservablePoint> values = new ChartValues<ObservablePoint>();
            if (XValues.Count == dataCapsule.XValues.Count && XValues.Count != 0)
            {
                for (int i = 0; i < XValues.Count; i++)
                {
                    YValues[i] += dataCapsule.YValues[i];
                }
            }
            else throw new Exception("The amounts of the points are not equal.");
            return this;
        }

        internal DataCapsule Subtract(DataCapsule dataCapsule)
        {
            ChartValues<ObservablePoint> values = new ChartValues<ObservablePoint>();
            if (XValues.Count == dataCapsule.XValues.Count && XValues.Count != 0)
            {
                for (int i = 0; i < XValues.Count; i++)
                {
                    YValues[i] -= dataCapsule.YValues[i];
                }
            }
            else throw new Exception("The amounts of the points are not equal.");
            return this;
        }

        internal DataCapsule Multiply(DataCapsule dataCapsule)
        {
            ChartValues<ObservablePoint> values = new ChartValues<ObservablePoint>();
            if (XValues.Count == dataCapsule.XValues.Count && XValues.Count != 0)
            {
                for (int i = 0; i < XValues.Count; i++)
                {
                    YValues[i] *= dataCapsule.YValues[i];
                }
            }
            else throw new Exception("The amounts of the points are not equal.");
            return this;
        }

        internal DataCapsule Divide(DataCapsule dataCapsule)
        {
            ChartValues<ObservablePoint> values = new ChartValues<ObservablePoint>();
            if (XValues.Count == dataCapsule.XValues.Count && XValues.Count != 0)
            {
                for (int i = 0; i < XValues.Count; i++)
                {
                    if (dataCapsule.YValues[i] == 0) continue;
                    YValues[i] /= dataCapsule.YValues[i];
                }
            }
            else throw new Exception("The amounts of the points are not equal.");
            return this;
        }

        internal DataCapsule Weave(DataCapsule dataCapsule)
        {
            List<double> a = this.YValues, b = dataCapsule.YValues;

            var result = new List<double>();
            for(int i = 0; i < a.Count + b.Count - 1; i++)
            {
                double sum = 0;
                for(int j = 0; j<a.Count; j++)
                {
                    if (i - j < 0 || i - j >= b.Count)
                    {
                        continue;
                    }
                    else sum += a[j] * b[i - j];
                }
                result.Add(sum);
            }

            return new DataCapsule(result, this.SamplingFrequency);
        }

        internal DataCapsule Weave(List<double> factors)
        {
            List<double> result = new List<double>();

            for(int i = 0; i < this.XValues.Count + factors.Count - 1; i++)
            {
                double sum = 0;
                for(int j = 0; j < this.YValues.Count; j++)
                {
                    if (i - j < 0 || i - j >= factors.Count) continue;
                    else 
                    {
                        sum += this.YValues[j] * factors[i-j];
                    }
                }
                result.Add(sum);
            }

            List<double> xs = SignalLogics.GetTimeValues(this.XValues[this.XValues.Count - 1]/result.Count, this.XValues[this.XValues.Count - 1]).ToList();

            return new DataCapsule(xs, result);
        }

        internal DataCapsule Correlation(DataCapsule dataCapsule)
        {
            ChartValues<ObservablePoint> correlationValues = new ChartValues<ObservablePoint>();

            double frequency = (this.XValues.Max() + dataCapsule.XValues.Max()) / (this.XValues.Count + dataCapsule.XValues.Count - 1);
            int counter = 0;
            double[] time = SignalLogics.GetTimeValues(frequency, this.XValues.Max() + dataCapsule.XValues.Max());

            for(int i = 0; i < this.XValues.Count + dataCapsule.XValues.Count - 1; i++)
            {
                double sum = 0;
                int k1min = i >= dataCapsule.XValues.Count - 1 ? i - (dataCapsule.XValues.Count - 1) : 0,
                    k1max = i < this.XValues.Count - 1 ? i : this.XValues.Count - 1,
                    k2min = i <= dataCapsule.XValues.Count - 1 ? dataCapsule.XValues.Count - 1 - i : 0;

                for(int k1 = k1min, k2 = k2min; k1 <= k1max; k1++, k2++)
                {
                    sum += this.YValues[k1] * dataCapsule.YValues[k2];
                }
                correlationValues.Add(new ObservablePoint { X = time[counter], Y = sum });
                counter++;
            }

            return new DataCapsule(correlationValues, dataCapsule.SamplingFrequency);
        }

        internal DataCapsule WeaveCorrelation(DataCapsule dataCapsule)
        {
            this.XValues.Reverse();
            this.YValues.Reverse();

            DataCapsule result = this.Weave(dataCapsule);

            this.XValues.Reverse();
            this.YValues.Reverse();

            return result;
        }
    }
}
