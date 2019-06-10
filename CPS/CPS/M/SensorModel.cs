using CPS.Logics;
using LiveCharts;
using LiveCharts.Defaults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CPS.M
{
    class SensorModel
    {
        internal DataCapsule Deserialize()
        {
            return SerializationLogics.Deserialize();
        }

        internal DataCapsule GenerateSignalComeBack(DataCapsule signalBefore, double signalSpeed, double objectPosition, double objectSpeed, double momentInTime, double timeFrequency = 0.01)
        {
            double objRoad = objectPosition + objectSpeed * momentInTime, signalRoad = signalSpeed * momentInTime;

            if(signalRoad < objRoad * 2)
            {
                MessageBox.Show("W zadanym czasie sygnał nie zdąrzy powrócić.");
                return null;
            }
            else if (signalRoad > objRoad * 2)
            {
                signalRoad = objRoad * 2;
            }

            int samplesToMove = (int)(signalRoad / signalSpeed * signalBefore.SamplingFrequency);
            while (samplesToMove >= signalBefore.XValues.Count) samplesToMove -= signalBefore.XValues.Count;

            if (samplesToMove == 0) return new DataCapsule(signalBefore);
            else return new DataCapsule(signalBefore, samplesToMove);
        }

        internal string CalculateDistance(List<double> yValues, double signalSpeed, int freq)
        {
            List<double> right = yValues.Skip((yValues.Count - 1) / 2).ToList();
            double a = right.Max();
            int max = right.FindIndex(c => c == right.Max());
            double b = (signalSpeed * ((double)max / freq) / 2);
            return b.ToString();
        }
    }
}
