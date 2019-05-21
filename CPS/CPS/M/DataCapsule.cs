using CPS.Logics;
using LiveCharts;
using LiveCharts.Defaults;
using System;
using System.Collections.Generic;

namespace CPS.M
{
    [Serializable]
    class DataCapsule
    {
        public List<double> XValues { get; set; }
        public List<double> YValues { get; set; }

        public DataCapsule(ChartValues<ObservablePoint> values)
        {
            XValues = new List<double>();
            YValues = new List<double>();

            foreach(var item in values)
            {
                XValues.Add(item.X);
                YValues.Add(item.Y);
            }
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
            ChartValues<ObservablePoint> weaveValues = new ChartValues<ObservablePoint>();

            double frequency = (this.XValues[this.XValues.Count - 1] + dataCapsule.XValues[dataCapsule.XValues.Count - 1]) / (this.XValues.Count + dataCapsule.XValues.Count - 1);
            int counter = 0;
            double[] time = SignalLogics.GetTimeValues(frequency, this.XValues[this.XValues.Count - 1] + dataCapsule.XValues[dataCapsule.XValues.Count - 1]);

            for(int i = 0; i < this.XValues.Count + dataCapsule.XValues.Count; i++)
            {
                double sum = 0;
                var kmin = i >= this.XValues.Count - 1 ? i - (this.XValues.Count - 1) : 0;
                var kmax = i < dataCapsule.XValues.Count - 1 ? i : dataCapsule.XValues.Count - 1;

                for(int j = kmin; j < kmax; j++)
                {
                    sum += dataCapsule.YValues[j] * this.YValues[i - j];
                }

                weaveValues.Add(new ObservablePoint { X = time[counter], Y = sum });
                counter++;
            }

            return new DataCapsule(weaveValues);
        }

        internal DataCapsule Correlation(DataCapsule dataCapsule)
        {
            ChartValues<ObservablePoint> correlationValues = new ChartValues<ObservablePoint>();

            double frequency = (this.XValues[this.XValues.Count - 1] + dataCapsule.XValues[dataCapsule.XValues.Count - 1]) / (this.XValues.Count + dataCapsule.XValues.Count - 1);
            int counter = 0;
            double[] time = SignalLogics.GetTimeValues(frequency, this.XValues[this.XValues.Count - 1] + dataCapsule.XValues[dataCapsule.XValues.Count - 1]);

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

            return new DataCapsule(correlationValues);
        }
    }
}
