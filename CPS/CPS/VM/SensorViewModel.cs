using CPS.Logics;
using CPS.M;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Input;

namespace CPS.VM
{
    class SensorViewModel : INotifyPropertyChanged
    {
        private SensorModel sensorModel = new SensorModel();

        public event PropertyChangedEventHandler PropertyChanged;

        public SeriesCollection BeforeChartSeries { get; set; }
        public SeriesCollection AfterChartSeries { get; set; }
        public SeriesCollection CorrelationChartSeries { get; set; }

        public ObservableCollection<Control> ParametersCollection { get; set; }
        public ObservableCollection<Control> ResultsCollection { get; set; }

        private readonly Label ObjectDistanceLabel = new Label { Content = "Odległość obiektu (m):" };
        private readonly CustomTextBox<double> ObjectDistanceTextBox = new CustomTextBox<double>(10);

        private readonly Label ObjectSpeedLabel = new Label { Content = "Prędkość obiektu (m/s):" };
        private readonly CustomTextBox<double> ObjectSpeedTextBox = new CustomTextBox<double>(1);

        private readonly Label SignalSpeedLabel = new Label { Content = "Prędkość sygnału (m/s):" };
        private readonly CustomTextBox<double> SignalSpeedTextBox = new CustomTextBox<double>(1000);

        private readonly Label MomentInTimeLabel = new Label { Content = "Moment w czasie (s):" };
        private readonly CustomTextBox<double> MomentInTimeTextBox = new CustomTextBox<double>(1);

        private readonly Label ResultDistanceLabel = new Label { Content = "ObliczonyDystans (m):" };
        private readonly CustomTextBox<double> ResultDistanceTextBox = new CustomTextBox<double>(true);

        public ICommand GenerateButtonPressed { get; }

        public SensorViewModel()
        {
            ParametersCollection = new ObservableCollection<Control>();
            ResultsCollection = new ObservableCollection<Control>();

            BeforeChartSeries = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Wykres wysyłanego sygnału"
                }
            };
            AfterChartSeries = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Wykres odebranego sygnału"
                }
            };
            CorrelationChartSeries = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Wykres korelacji obu sygnałów"
                }
            };

            GenerateButtonPressed = new RelayCommand(Generate);

            this.Config();
        }

        private void Config()
        {
            ParametersCollection.Add(ObjectDistanceLabel);
            ParametersCollection.Add(ObjectDistanceTextBox);

            ParametersCollection.Add(ObjectSpeedLabel);
            ParametersCollection.Add(ObjectSpeedTextBox);

            ParametersCollection.Add(SignalSpeedLabel);
            ParametersCollection.Add(SignalSpeedTextBox);

            ParametersCollection.Add(MomentInTimeLabel);
            ParametersCollection.Add(MomentInTimeTextBox);


            ResultsCollection.Add(ResultDistanceLabel);
            ResultsCollection.Add(ResultDistanceTextBox);
        }

        public void Generate()
        {
            DataCapsule signalBefore = sensorModel.Deserialize();
            if (signalBefore == null) return;
            BeforeChartSeries[0].Values = signalBefore.GetValues();

            DataCapsule signalAfter = sensorModel.GenerateSignalComeBack(signalBefore, SignalSpeedTextBox.GetValue(), ObjectDistanceTextBox.GetValue(), ObjectSpeedTextBox.GetValue(), MomentInTimeTextBox.GetValue());
            if (signalAfter == null) return;
            AfterChartSeries[0].Values = signalAfter.GetValues();

            DataCapsule signalCorrelation = signalBefore.WeaveCorrelation(signalAfter);
            CorrelationChartSeries[0].Values = signalCorrelation.GetValues();

            ResultDistanceTextBox.Text = sensorModel.CalculateDistance(signalCorrelation.YValues, SignalSpeedTextBox.GetValue(), signalCorrelation.SamplingFrequency);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
