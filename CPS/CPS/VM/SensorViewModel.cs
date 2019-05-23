using CPS.M;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace CPS.VM
{
    class SensorViewModel : INotifyPropertyChanged
    {
        private SensorModel sensorModel = new SensorModel();

        public event PropertyChangedEventHandler PropertyChanged;

        public string SimulatorTimeUnit { get; set; }
        public string ObjectSpeed { get; set; }
        public string SignalSpeed { get; set; }
        public string ProbeSignalPeriod { get; set; }
        public string ProbeSignalSamplingFrequency { get; set; }
        public string DiscreetBufforsLength { get; set; }
        public string RaportingPeriod { get; set; }
        private string a;
        public string SensorResult
        {
            get => a;
            set
            {
                a = value;
                OnPropertyChanged(nameof(SensorResult));
            }
        }

        public ICommand GenerateButtonPressed { get; }

        public SensorViewModel()
        {
            SimulatorTimeUnit = "10";
            ObjectSpeed = "10";
            SignalSpeed = "3000";
            ProbeSignalPeriod = "1";
            ProbeSignalSamplingFrequency = "100";
            DiscreetBufforsLength = "500";
            RaportingPeriod = "2";

            SensorResult = "Here me out";

            GenerateButtonPressed = new RelayCommand(Generate);
        }

        public void Generate()
        {
            string result = sensorModel.Simulate(Convert.ToDouble(SimulatorTimeUnit), Convert.ToDouble(ObjectSpeed), Convert.ToDouble(SignalSpeed), Convert.ToDouble(ProbeSignalPeriod), Convert.ToDouble(ProbeSignalSamplingFrequency), Convert.ToDouble(DiscreetBufforsLength), Convert.ToDouble(RaportingPeriod));
            SensorResult = result;
            Console.WriteLine(SensorResult);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
