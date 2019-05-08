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
    }
}
