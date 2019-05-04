using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;
using CPS.M;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

namespace CPS.VM
{
    class ViewModel
    {
        private Model model = new Model();
        private int selectedSignal;

        #region THE_LABELS_AND_THE_TEXTBOXES
        private readonly Label SamplesAmountLabel = new Label { Content = "Ilość próbek:" };
        private readonly CustomTextBox<int> SamplesAmountTextBox = new CustomTextBox<int>(100);

        private readonly Label TimeDurationLabel = new Label { Content = "Czas trwania:" };
        private readonly CustomTextBox<double> TimeDurationTextBox = new CustomTextBox<double>(10);

        private readonly Label MinimumLabel = new Label { Content = "Minimum:" };
        private readonly CustomTextBox<double> MinimumTextBox = new CustomTextBox<double>(-5);

        private readonly Label MaximumLabel = new Label { Content = "Maksimum" };
        private readonly CustomTextBox<double> MaximumTextBox = new CustomTextBox<double>(5);

        private readonly Label MeanLabel = new Label { Content = "Średnia:" };
        private readonly CustomTextBox<double> MeanTextBox = new CustomTextBox<double>();

        private readonly Label VarianceLabel = new Label { Content = "Odchylenie:" };
        private readonly CustomTextBox<double> VarianceTextBox = new CustomTextBox<double>();

        private readonly Label AmplitudeLabel = new Label { Content = "Amplituda:" };
        private readonly CustomTextBox<double> AmplitudeTextBox = new CustomTextBox<double>(2);

        private readonly Label FrequencyLabel = new Label { Content = "Okres:" };
        private readonly CustomTextBox<double> FrequencyTextBox = new CustomTextBox<double>(5);

        private readonly Label StartTimeLabel = new Label { Content = "Przesunięcie:" };
        private readonly CustomTextBox<double> StartTimetextBox = new CustomTextBox<double>(0);

        private readonly Label PeriodLabel = new Label { Content = "Okres podstawowy:" };
        private readonly CustomTextBox<double> PeriodTextBox = new CustomTextBox<double>();

        private readonly Label DutyCycleLabel = new Label { Content = "Współczynnik wypełnienia:" };
        private readonly CustomTextBox<double> DutyCycleTextBox = new CustomTextBox<double>();

        private readonly Label JumpTimeLabel = new Label { Content = "Czas skoku:" };
        private readonly CustomTextBox<double> JumpTimeTextBox = new CustomTextBox<double>();

        private readonly Label ImpulseProbabilityLabel = new Label { Content = "Szansa impulsu:" };
        private readonly CustomTextBox<double> ImpulseProbabilityTextBox = new CustomTextBox<double>();
        #endregion

        public ICommand GenerateButtonPressed { get; }

        public SeriesCollection NormalChartSeries { get; set; }
        public ObservableCollection<Control> SignalParametersCollection { get; set; }
        public int SelectedSignal
        {
            get { return selectedSignal; }
            set
            {
                this.GenerateParameters(value);
                this.selectedSignal = value;
            }
        }

        public ViewModel()
        {
            SignalParametersCollection = new ObservableCollection<Control>();
            NormalChartSeries = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Wykres wybranego sygnału",
                    Foreground = null,
                    PointGeometry = null
                }
            };

            GenerateButtonPressed = new RelayCommand(Generate);

            Config();
        }

        public void Config()
        {
            GenerateParameters(0);
        }
             
        public void GenerateParameters(int signalTypeChoice)
        {
            SignalParametersCollection.Clear();

            SignalParametersCollection.Add(SamplesAmountLabel);
            SignalParametersCollection.Add(SamplesAmountTextBox);

            SignalParametersCollection.Add(TimeDurationLabel);
            SignalParametersCollection.Add(TimeDurationTextBox);

            switch(signalTypeChoice)
            {
                case 0:
                    SignalParametersCollection.Add(MinimumLabel);
                    SignalParametersCollection.Add(MinimumTextBox);

                    SignalParametersCollection.Add(MaximumLabel);
                    SignalParametersCollection.Add(MaximumTextBox);
                    break;
                case 1:
                    SignalParametersCollection.Add(AmplitudeLabel);
                    SignalParametersCollection.Add(AmplitudeTextBox);

                    SignalParametersCollection.Add(MeanLabel);
                    SignalParametersCollection.Add(MeanTextBox);

                    SignalParametersCollection.Add(VarianceLabel);
                    SignalParametersCollection.Add(VarianceTextBox);
                    break;
                case 2:
                case 3:
                case 4:
                    SignalParametersCollection.Add(AmplitudeLabel);
                    SignalParametersCollection.Add(AmplitudeTextBox);

                    SignalParametersCollection.Add(FrequencyLabel);
                    SignalParametersCollection.Add(FrequencyTextBox);

                    SignalParametersCollection.Add(StartTimeLabel);
                    SignalParametersCollection.Add(StartTimetextBox);
                    break;
                case 5:
                case 6:
                case 7:
                    SignalParametersCollection.Add(AmplitudeLabel);
                    SignalParametersCollection.Add(AmplitudeTextBox);

                    SignalParametersCollection.Add(PeriodLabel);
                    SignalParametersCollection.Add(PeriodTextBox);

                    SignalParametersCollection.Add(StartTimeLabel);
                    SignalParametersCollection.Add(StartTimetextBox);

                    SignalParametersCollection.Add(DutyCycleLabel);
                    SignalParametersCollection.Add(DutyCycleTextBox);
                    break;
                case 8:
                case 9:
                    SignalParametersCollection.Add(AmplitudeLabel);
                    SignalParametersCollection.Add(AmplitudeTextBox);

                    SignalParametersCollection.Add(JumpTimeLabel);
                    SignalParametersCollection.Add(JumpTimeTextBox);

                    SignalParametersCollection.Add(StartTimeLabel);
                    SignalParametersCollection.Add(StartTimetextBox);
                    break;
                case 10:
                    SignalParametersCollection.Add(AmplitudeLabel);
                    SignalParametersCollection.Add(AmplitudeTextBox);

                    SignalParametersCollection.Add(ImpulseProbabilityLabel);
                    SignalParametersCollection.Add(ImpulseProbabilityTextBox);
                    break;
            }
        }

        public void Generate()
        {
            switch (SelectedSignal)
            {
                case 0:
                    NormalChartSeries[0].Values = model.GetUniformDistributionNoise(SamplesAmountTextBox.GetValue(), TimeDurationTextBox.GetValue(), MinimumTextBox.GetValue(), MaximumTextBox.GetValue());
                    break;
                case 1:
                    NormalChartSeries[0].Values = model.GetGaussianNoise(SamplesAmountTextBox.GetValue(), TimeDurationTextBox.GetValue(), AmplitudeTextBox.GetValue(), MeanTextBox.GetValue(), VarianceTextBox.GetValue());
                    break;
                case 2:
                    NormalChartSeries[0].Values = model.GetSinSignal(SamplesAmountTextBox.GetValue(), TimeDurationTextBox.GetValue(), AmplitudeTextBox.GetValue(), FrequencyTextBox.GetValue(), StartTimetextBox.GetValue());
                    break;
                case 3:
                    NormalChartSeries[0].Values = model.GetSinAbsSignal(SamplesAmountTextBox.GetValue(), TimeDurationTextBox.GetValue(), AmplitudeTextBox.GetValue(), FrequencyTextBox.GetValue(), StartTimetextBox.GetValue());
                    break;
                case 4:
                    NormalChartSeries[0].Values = model.GetSinDoubleAbsSignal(SamplesAmountTextBox.GetValue(), TimeDurationTextBox.GetValue(), AmplitudeTextBox.GetValue(), FrequencyTextBox.GetValue(), StartTimetextBox.GetValue());
                    break;
                case 5:
                    NormalChartSeries[0].Values = model.GetRectangularSignal(SamplesAmountTextBox.GetValue(), TimeDurationTextBox.GetValue(), AmplitudeTextBox.GetValue(), PeriodTextBox.GetValue(), StartTimetextBox.GetValue(), DutyCycleTextBox.GetValue());
                    break;
                case 6:
                    NormalChartSeries[0].Values = model.GetSymmetricRectangularSignal(SamplesAmountTextBox.GetValue(), TimeDurationTextBox.GetValue(), AmplitudeTextBox.GetValue(), PeriodTextBox.GetValue(), StartTimetextBox.GetValue(), DutyCycleTextBox.GetValue());
                    break;
                case 7:
                    NormalChartSeries[0].Values = model.GetTriangularSignal(SamplesAmountTextBox.GetValue(), TimeDurationTextBox.GetValue(), AmplitudeTextBox.GetValue(), PeriodTextBox.GetValue(), StartTimetextBox.GetValue(), DutyCycleTextBox.GetValue());
                    break;
                case 8:
                    NormalChartSeries[0].Values = model.GetJumpSignal(SamplesAmountTextBox.GetValue(), TimeDurationTextBox.GetValue(), AmplitudeTextBox.GetValue(), JumpTimeTextBox.GetValue(), StartTimetextBox.GetValue());
                    break;
                case 9:
                    NormalChartSeries[0].Values = model.GetSingleImpulseSignal(SamplesAmountTextBox.GetValue(), TimeDurationTextBox.GetValue(), AmplitudeTextBox.GetValue(), JumpTimeTextBox.GetValue(), StartTimetextBox.GetValue());
                    break;
                case 10:
                    NormalChartSeries[0].Values = model.GetImpulseNoise(SamplesAmountTextBox.GetValue(), TimeDurationTextBox.GetValue(), AmplitudeTextBox.GetValue(), ImpulseProbabilityTextBox.GetValue());
                    break;
            }
        }
    }
}
