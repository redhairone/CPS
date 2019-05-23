using CPS.Logics;
using LiveCharts;
using LiveCharts.Defaults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPS.M
{
    class SensorModel
    {
        private Random rand = new Random();

        internal string Simulate(double simulatorTimeUnit, double objectSpeed, double signalSpeed, double probeSignalPeriod, double probeSignalSamplingFrequency, double discreetBufforsLength, double raportingPeriod)
        {
            List<double> leftResult = new List<double>();
            List<double> rightResult = new List<double>();

            List<double> amplitudes = new List<double>();
            List<double> periods = new List<double>();

            periods.Add(rand.NextDouble() * (probeSignalPeriod - 1e-10) + 1e-10);
            amplitudes.Add(rand.NextDouble() * 50.0 + 1.0);

            periods.Add(rand.NextDouble() * (probeSignalPeriod - 1e-10) + 1e-10);
            amplitudes.Add(rand.NextDouble() * 50.0 + 1.0);

            periods[0] = probeSignalPeriod;
            double duration = discreetBufforsLength / probeSignalSamplingFrequency;

            for(double i = 0; i<10.0 * raportingPeriod; i += raportingPeriod)
            {
                double realDistance = 0.0 + i * objectSpeed,
                    propagationTime = 2 * (realDistance / signalSpeed);

                DataCapsule probingSignal = new DataCapsule((ChartValues<ObservablePoint>)SignalLogics.GetChartValues(amplitudes, periods, i - duration, duration, probeSignalSamplingFrequency));
                DataCapsule feedbackSignal = new DataCapsule((ChartValues<ObservablePoint>)SignalLogics.GetChartValues(amplitudes, periods, i - propagationTime - duration, duration, probeSignalSamplingFrequency));
                DataCapsule correlation = probingSignal.WeaveCorrelation(feedbackSignal);

                List<double> correlationY = new List<double>();
                foreach(var item in correlation.YValues)
                {
                    correlationY.Add(item);
                }

                correlationY.RemoveAt(correlationY.Count - 1);
                leftResult.Add(realDistance);
                rightResult.Add(MathLogics.CalculateDistance(correlationY,probeSignalSamplingFrequency,signalSpeed));
            }

            StringBuilder sb = new StringBuilder("Odległości:" + Environment.NewLine);

            for(int i = 0; i < leftResult.Count; i++)
            {
                sb.Append(leftResult[i] + " -> " + rightResult[i] + Environment.NewLine);
            }

            return sb.ToString();
        }
    }
}
