using CPS.Logics;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace CPS.M
{
    class Zad4Model
    {
        public ComplexCapsule GetFourier()
        {
            DataCapsule loaded = SerializationLogics.Deserialize();

            List<Complex> results = new List<Complex>();
            Complex sum;

            for(int i = 0; i < loaded.XValues.Count; i++)
            {
                sum = Complex.Zero;

                for(int k = 0; k < loaded.XValues.Count; k++)
                {
                    sum += loaded.YValues[k] * Complex.Exp(new Complex(0, -2 * Math.PI * i * k / loaded.XValues.Count));
                }

                results.Add(sum / loaded.XValues.Count);
            }
           
            return new ComplexCapsule(loaded.XValues, results, loaded.SamplingFrequency);
        }
    }
}
