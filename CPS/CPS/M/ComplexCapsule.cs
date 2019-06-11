using LiveCharts;
using LiveCharts.Defaults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CPS.M
{
    class ComplexCapsule
    {
        public List<double> XValues { get; set; }
        public List<Complex> YValues { get; set; }
        public int SamplingFrequency { get; set; }

        public ComplexCapsule(List<double> xValues, List<Complex> yValues, int samplingFrequency)
        {
            if (xValues.Count != yValues.Count) throw new Exception("Nie rowne X i Y w complex");

            XValues = xValues;
            YValues = yValues;
            SamplingFrequency = samplingFrequency;
        }

        public IChartValues GetW1TopValues()
        {
            ChartValues<ObservablePoint> values = new ChartValues<ObservablePoint>();

            if (XValues.Count == YValues.Count && XValues.Count != 0)
            {
                for (int i = 0; i < XValues.Count; i++)
                {
                    values.Add(new ObservablePoint { X = XValues[i], Y = YValues[i].Real });
                }
            }
            else throw new Exception("The amounts of X values is not equal the amount of Y values.");
            return values;
        }

        public IChartValues GetW1BottomValues()
        {
            ChartValues<ObservablePoint> values = new ChartValues<ObservablePoint>();

            if (XValues.Count == YValues.Count && XValues.Count != 0)
            {
                for (int i = 0; i < XValues.Count; i++)
                {
                    values.Add(new ObservablePoint { X = XValues[i], Y = YValues[i].Imaginary });
                }
            }
            else throw new Exception("The amounts of X values is not equal the amount of Y values.");
            return values;
        }

        public IChartValues GetW2TopValues()
        {
            ChartValues<ObservablePoint> values = new ChartValues<ObservablePoint>();

            if (XValues.Count == YValues.Count && XValues.Count != 0)
            {
                for (int i = 0; i < XValues.Count; i++)
                {
                    values.Add(new ObservablePoint { X = XValues[i], Y = YValues[i].Magnitude });
                }
            }
            else throw new Exception("The amounts of X values is not equal the amount of Y values.");
            return values;
        }

        public IChartValues GetW2BottomValues()
        {
            ChartValues<ObservablePoint> values = new ChartValues<ObservablePoint>();

            if (XValues.Count == YValues.Count && XValues.Count != 0)
            {
                for (int i = 0; i < XValues.Count; i++)
                {
                    values.Add(new ObservablePoint { X = XValues[i], Y = YValues[i].Phase });
                }
            }
            else throw new Exception("The amounts of X values is not equal the amount of Y values.");
            return values;
        }
    }
}
