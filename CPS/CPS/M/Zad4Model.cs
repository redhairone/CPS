using CPS.Logics;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Windows.Media;

namespace CPS.M
{
    class Zad4Model
    {
        public ComplexCapsule GetFourier()
        {
            DataCapsule loaded = SerializationLogics.Deserialize();

            List<Complex> results = new List<Complex>();
            Complex sum;

            int m = 0;
            while (Math.Pow(2, m) <= loaded.XValues.Count) m++;
            loaded.Take((int)Math.Pow(2, --m));

            for (int i = 0; i < loaded.XValues.Count; i++)
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

        public ComplexCapsule GetFastFourier()
        {
            DataCapsule loaded = SerializationLogics.Deserialize();

            TransformationsLogics logics = new TransformationsLogics();

            List<Complex> transformed = new List<Complex>();
            int N = loaded.XValues.Count;
            transformed = logics.SwitchSamples(loaded.YValues);
            return new ComplexCapsule(loaded.XValues.Take(transformed.Count).ToList(), transformed.Select(c => c / N).ToList(), loaded.SamplingFrequency);
        }

        public ComplexCapsule GetWalsh()
        {
            DataCapsule loaded = SerializationLogics.Deserialize();
            List<Complex> result = new List<Complex>();

            Complex sum;

            int m = 0;
            while(Math.Pow(2, m) <= loaded.XValues.Count) m++;
            loaded.Take((int)Math.Pow(2, --m));

            double[][] matrix = TransformationsLogics.GetMatrix(m);

            for(int i = 0; i < loaded.XValues.Count; i++)
            {
                sum = Complex.Zero;
                for(int j = 0; j < Math.Pow(2,m); j++)
                {
                    sum += loaded.YValues[j] * matrix[i][j];
                }

                sum /= loaded.YValues.Count;//(double)(Math.Pow(Math.Sqrt(2.0), m));
                
                result.Add(sum);
            }

            return new ComplexCapsule(loaded.XValues, result, loaded.SamplingFrequency);
        }

        public ComplexCapsule GetFastWalsh()
        {
            DataCapsule loaded = SerializationLogics.Deserialize();
            List<Complex> result = new List<Complex>();

            Complex a, b;

            int m = 0;
            while (Math.Pow(2, m) <= loaded.XValues.Count) m++;
            loaded.Take((int)Math.Pow(2, --m));

            for (int h = 1; h < loaded.XValues.Count; h*=2)
            {
                for(int i = 0; i < loaded.XValues.Count; i += h*2)
                {
                    for(int j = i; j < i+h; j++)
                    {
                        a = loaded.YValues[j];
                        b = loaded.YValues[j + h];

                        loaded.YValues[j] = a + b;
                        loaded.YValues[j + h] = a - b;
                    }
                }
            }

            for(int i = 0; i<loaded.XValues.Count; i++)
            {
                loaded.YValues[i] /= loaded.YValues.Count;
            }

            return new ComplexCapsule(loaded.XValues, loaded.YValues, loaded.SamplingFrequency);
        }
    }
}
