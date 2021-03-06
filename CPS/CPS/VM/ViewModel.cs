﻿using CPS.Logics;
using CPS.M;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CPS.VM
{
    class ViewModel
    {
        private Model model = new Model();
        private int selectedSignal;

        #region THE_LABELS_AND_THE_TEXTBOXES
        //<--LABELS AND TEXTBOXES OF THE SIGNAL PARAMETERS TAB-->
        private readonly Label SignalFrequencyLabel = new Label { Content = "Częstotliwość próbkowania sygnały analogowego (HZ):" };
        private readonly CustomTextBox<int> SignalFrequencyTextBox = new CustomTextBox<int>(100);

        private readonly Label TimeDurationLabel = new Label { Content = "Czas trwania:" };
        private readonly CustomTextBox<double> TimeDurationTextBox = new CustomTextBox<double>(10);

        private readonly Label MinimumLabel = new Label { Content = "Minimum:" };
        private readonly CustomTextBox<double> MinimumTextBox = new CustomTextBox<double>(-5);

        private readonly Label MaximumLabel = new Label { Content = "Maksimum" };
        private readonly CustomTextBox<double> MaximumTextBox = new CustomTextBox<double>(5);

        private readonly Label MeanLabel = new Label { Content = "Średnia:" };
        private readonly CustomTextBox<double> MeanTextBox = new CustomTextBox<double>();

        private readonly Label DeviationLabel = new Label { Content = "Odchylenie:" };
        private readonly CustomTextBox<double> DeviationTextBox = new CustomTextBox<double>();

        private readonly Label AmplitudeLabel = new Label { Content = "Amplituda:" };
        private readonly CustomTextBox<double> AmplitudeTextBox = new CustomTextBox<double>(2);

        private readonly Label FrequencyLabel = new Label { Content = "Okres:" };
        private readonly CustomTextBox<double> FrequencyTextBox = new CustomTextBox<double>(5);

        private readonly Label StartTimeLabel = new Label { Content = "Przesunięcie:" };
        private readonly CustomTextBox<double> StartTimetextBox = new CustomTextBox<double>(0);

        private readonly Label PeriodLabel = new Label { Content = "Okres podstawowy:" };
        private readonly CustomTextBox<double> PeriodTextBox = new CustomTextBox<double>(1);

        private readonly Label DutyCycleLabel = new Label { Content = "Współczynnik wypełnienia:" };
        private readonly CustomTextBox<double> DutyCycleTextBox = new CustomTextBox<double>(0.5);

        private readonly Label JumpTimeLabel = new Label { Content = "Czas skoku:" };
        private readonly CustomTextBox<double> JumpTimeTextBox = new CustomTextBox<double>();

        private readonly Label ImpulseProbabilityLabel = new Label { Content = "Szansa impulsu:" };
        private readonly CustomTextBox<double> ImpulseProbabilityTextBox = new CustomTextBox<double>();

        //<--LABELS AND TEXTBOXES OF THE ADDITIONAL PARAMETERS TAB-->
        private readonly Label SamplingFrequencyLabel = new Label { Content = "Częstotliwość próbkowania:" };
        private readonly CustomTextBox<int> SamplingFrequencyTextBox = new CustomTextBox<int>(100);

        private readonly Label ReconstructionFrequencyLabel = new Label { Content = "Częstotliwość rekonstrukcji:" };
        private readonly CustomTextBox<int> ReconstructionFrequencyTextBox = new CustomTextBox<int>(100);

        private readonly Label QuantLevelAmountLabel = new Label { Content = "Poziomy kwantyzacji:" };
        private readonly CustomTextBox<int> QuantLevelAmountTextBox = new CustomTextBox<int>(10);

        private readonly Label SeenSamplesLabel = new Label { Content = "Liczba rozważanych próbek:" };
        private readonly CustomTextBox<int> SeenSamplesTextBox = new CustomTextBox<int>(0);

        //<--LABELS AND TEXTBOXES OF THE FILTER PARAMETERS TAB-->
        private readonly Label MAmountLabel = new Label { Content = "M:" };
        private readonly CustomTextBox<int> MAmountTextBox = new CustomTextBox<int>(25);

        private readonly Label FilterSamplingFrequencyLabel = new Label { Content = "Częst. próbkowania filtru:" };
        private readonly CustomTextBox<int> FilterSamplingFrequencyTextBox = new CustomTextBox<int>(100);

        private readonly Label CutOffSamplingFrequencyLabel = new Label { Content = "Czestotliwość odcięcia:" };
        private readonly CustomTextBox<int> CutOffSamplingFrequencyTextBox = new CustomTextBox<int>(100);

        private readonly Label WindowLabel = new Label { Content = "Okno:" };
        private readonly CustomComboBox WindowComboBox = new CustomComboBox(new string[] { "Okno prostokątne", "Okno Hamminga", "Okno Hanninga", "Okno Blackmana"});

        private readonly Label FilterLabel = new Label { Content = "Filtr:" };
        private readonly CustomComboBox FilterComboBox = new CustomComboBox(new string[] { "Dolnoprzepustowy", "Środkowoprzepustowy", "Górnoprzepustowy" });

        //<--LABELS AND TEXTBOXES OF THE RESULTS TAB-->
        private readonly Label AverageLabel = new Label { Content = "Wartość średnia:" };
        private readonly CustomTextBox<double> AverageTextBox = new CustomTextBox<double>(true);

        private readonly Label AbsAverageLabel = new Label { Content = "Wartość średnia bezwzględna:" };
        private readonly CustomTextBox<double> AbsAverageTextBox = new CustomTextBox<double>(true);

        private readonly Label RootMeanSquareLabel = new Label { Content = "Wartość skuteczna:" };
        private readonly CustomTextBox<double> RootMeanSquareTextBox = new CustomTextBox<double>(true);

        private readonly Label VariationLabel = new Label { Content = "Wariacja:" };
        private readonly CustomTextBox<double> VariationTextBox= new CustomTextBox<double>(true);

        private readonly Label AveragePowerLabel = new Label { Content = "Moc średnia:" };
        private readonly CustomTextBox<double> AveragePowerTextBox = new CustomTextBox<double>(true);

        //<--LABELS AND TEXTBOXES OF THE SINC RESULTS TAB-->
        private readonly Label MeanSquareErrorLabel = new Label { Content = "Błąd średniokwadratowy:" };
        private readonly CustomTextBox<double> MeanSquareErrorTextBox = new CustomTextBox<double>(true);

        private readonly Label RatioLabel = new Label { Content = "Stosunek sygnał-szum:" };
        private readonly CustomTextBox<double> RatioTextBox = new CustomTextBox<double>(true);

        private readonly Label MaxRatioLabel = new Label { Content = "Maksymalny stosunek sygnał-szum:" };
        private readonly CustomTextBox<double> MaxRatioTextBox = new CustomTextBox<double>(true);

        private readonly Label MaxDiffrenceLabel = new Label { Content = "Maksymalna różnica:" };
        private readonly CustomTextBox<double> MaxDiffrenceTextBox = new CustomTextBox<double>(true);
        #endregion

        public ICommand GenerateButtonPressed { get; }
        public ICommand SaveButtonPressed { get; }
        public ICommand LoadButtonPressed { get; }
        public ICommand AdditionButtonPressed { get; }
        public ICommand SubtractionButtonPressed { get; }
        public ICommand MultiplicationButtonPressed { get; }
        public ICommand DivisionButtonPressed { get; }
        public ICommand WeaveButtonPressed { get; }
        public ICommand CorrelationButtonPressed { get; }
        public ICommand WeaveCorrelationButtonPressed { get; }
        public ICommand FilterButtonPressed { get; }
        public ICommand SensorButtonPressed { get; }
        public ICommand TranformationsButtonPressed { get; }

        public SeriesCollection NormalChartSeries { get; set; }
        public SeriesCollection SamplingChartSeries { get; set; }
        public SeriesCollection HistogramChartSeries { get; set; }
        public string[] HistogramLabels { get; set; }
        public SeriesCollection ZeroHoldChartSeries { get; set; }
        public SeriesCollection QuantChartSeries { get; set; }
        public SeriesCollection SincReconstructionChartSeries { get; set; }
        public ObservableCollection<Control> SignalParametersCollection { get; set; }
        public ObservableCollection<Control> AdditionalParametersCollection { get; set; }
        public ObservableCollection<Control> FilterParameters { get; set; }
        public ObservableCollection<Control> ResultsCollection { get; set; }
        public ObservableCollection<Control> SincResultsCollection { get; set; }
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
            AdditionalParametersCollection = new ObservableCollection<Control>();
            FilterParameters = new ObservableCollection<Control>();
            ResultsCollection = new ObservableCollection<Control>();
            SincResultsCollection = new ObservableCollection<Control>();

            NormalChartSeries = new SeriesCollection
            {
                new LineSeries()
            };
            HistogramChartSeries = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Histogram wygenerowanego sygnału"
                }
            };
            SamplingChartSeries = new SeriesCollection
            {
                new ScatterSeries
                {
                    Title = "Spróbkowany sygnał",
                    PointGeometry = DefaultGeometries.Diamond,
                    StrokeThickness = 2
                }
            };
            QuantChartSeries = new SeriesCollection
            {
                new StepLineSeries
                {
                    Title = "Kwantyzacja równomierna z obcięciem"
                },
                new LineSeries
                {
                    Title = "Sygnał oryginalny",
                    PointGeometry = null,
                    Fill = Brushes.Transparent
                }
            };
            ZeroHoldChartSeries = new SeriesCollection
            {
                new StepLineSeries
                {
                    Title = "Ekstrapolacja zerowego rzędu"
                },
                new LineSeries
                {
                    Title = "Sygnał oryginalny",
                    PointGeometry = null,
                    Fill = Brushes.Transparent
                }
            };
            SincReconstructionChartSeries = new SeriesCollection
            {
                new StepLineSeries
                {
                    Title = "Rekonstrukcja sygnału"
                },
                new LineSeries
                {
                    Title = "Sygnał oryginalny",
                    PointGeometry = null,
                    Fill = Brushes.Transparent
                }
            };

            GenerateButtonPressed = new RelayCommand(Generate);
            SaveButtonPressed = new RelayCommand(Save);
            LoadButtonPressed = new RelayCommand(Load);
            AdditionButtonPressed = new RelayCommand(Add);
            SubtractionButtonPressed = new RelayCommand(Subtract);
            MultiplicationButtonPressed = new RelayCommand(Multiply);
            DivisionButtonPressed = new RelayCommand(Divide);
            WeaveButtonPressed = new RelayCommand(Weave);
            CorrelationButtonPressed = new RelayCommand(Correlation);
            WeaveCorrelationButtonPressed = new RelayCommand(WeaveCorrelation);
            FilterButtonPressed = new RelayCommand(Filter);
            SensorButtonPressed = new RelayCommand(Sensor);
            TranformationsButtonPressed = new RelayCommand(Transformations);

            Config();
        }

        public void Config()
        {
            GenerateParameters(0);

            #region ADDING ADDITIONAL PARAMETERS CONTROLS TO THE TAB
            AdditionalParametersCollection.Add(SamplingFrequencyLabel);
            AdditionalParametersCollection.Add(SamplingFrequencyTextBox);

            AdditionalParametersCollection.Add(ReconstructionFrequencyLabel);
            AdditionalParametersCollection.Add(ReconstructionFrequencyTextBox);

            AdditionalParametersCollection.Add(QuantLevelAmountLabel);
            AdditionalParametersCollection.Add(QuantLevelAmountTextBox);

            AdditionalParametersCollection.Add(SeenSamplesLabel);
            AdditionalParametersCollection.Add(SeenSamplesTextBox);
            #endregion

            #region ADDING PARAMETERS FOR THE RESULTS
            FilterParameters.Add(MAmountLabel);
            FilterParameters.Add(MAmountTextBox);

            //FilterParameters.Add(FilterSamplingFrequencyLabel);
            //FilterParameters.Add(FilterSamplingFrequencyTextBox);

            FilterParameters.Add(CutOffSamplingFrequencyLabel);
            FilterParameters.Add(CutOffSamplingFrequencyTextBox);

            FilterParameters.Add(WindowLabel);
            FilterParameters.Add(WindowComboBox);

            FilterParameters.Add(FilterLabel);
            FilterParameters.Add(FilterComboBox);
            #endregion


            #region ADDING RESULTS CONTROLS TO THE TAB
            ResultsCollection.Add(AverageLabel);
            ResultsCollection.Add(AverageTextBox);

            ResultsCollection.Add(AbsAverageLabel);
            ResultsCollection.Add(AbsAverageTextBox);

            ResultsCollection.Add(RootMeanSquareLabel);
            ResultsCollection.Add(RootMeanSquareTextBox);

            ResultsCollection.Add(VariationLabel);
            ResultsCollection.Add(VariationTextBox);

            ResultsCollection.Add(AveragePowerLabel);
            ResultsCollection.Add(AveragePowerTextBox);
            #endregion

            #region ADDING SINC SERULTS CONTROLS TO THE TAB
            SincResultsCollection.Add(MeanSquareErrorLabel);
            SincResultsCollection.Add(MeanSquareErrorTextBox);

            SincResultsCollection.Add(RatioLabel);
            SincResultsCollection.Add(RatioTextBox);

            SincResultsCollection.Add(MaxRatioLabel);
            SincResultsCollection.Add(MaxRatioTextBox);

            SincResultsCollection.Add(MaxDiffrenceLabel);
            SincResultsCollection.Add(MaxDiffrenceTextBox);
            #endregion
        }

        public void GenerateParameters(int signalTypeChoice)
        {
            SignalParametersCollection.Clear();

            SignalParametersCollection.Add(SignalFrequencyLabel);
            SignalParametersCollection.Add(SignalFrequencyTextBox);

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

                    SignalParametersCollection.Add(DeviationLabel);
                    SignalParametersCollection.Add(DeviationTextBox);
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
            ChartValues<ObservablePoint> here = new ChartValues<ObservablePoint>(), here1 = new ChartValues<ObservablePoint>();

            if (selectedSignal < 9)
            {
                NormalChartSeries[0] = new LineSeries
                {
                    Title = "Sygnały przedstawiane liniowo",
                    PointGeometry = DefaultGeometries.Circle,
                    Fill = Brushes.Transparent
                };
            }
            else
            {
                NormalChartSeries[0] = new ScatterSeries
                {
                    Title = "Sygnały przedstawiane punktowo",
                    PointGeometry = DefaultGeometries.Circle,
                    StrokeThickness = 2
                };
            }

            switch (SelectedSignal)
            {
                case 0:
                    NormalChartSeries[0].Values = model.GetUniformDistributionNoise(SignalFrequencyTextBox.GetValue(), TimeDurationTextBox.GetValue(), MinimumTextBox.GetValue(), MaximumTextBox.GetValue());
                    SamplingChartSeries[0].Values = model.GetUniformDistributionNoise(SamplingFrequencyTextBox.GetValue(), TimeDurationTextBox.GetValue(), MinimumTextBox.GetValue(), MaximumTextBox.GetValue());
                    here = (ChartValues<ObservablePoint>)model.GetUniformDistributionNoise(ReconstructionFrequencyTextBox.GetValue(), TimeDurationTextBox.GetValue(), MinimumTextBox.GetValue(), MaximumTextBox.GetValue());
                    here1 = (ChartValues<ObservablePoint>)model.GetUniformDistributionNoise(ReconstructionFrequencyTextBox.GetValue(), TimeDurationTextBox.GetValue(), MinimumTextBox.GetValue(), MaximumTextBox.GetValue());
                    break;
                case 1:
                    NormalChartSeries[0].Values = model.GetGaussianNoise(SignalFrequencyTextBox.GetValue(), TimeDurationTextBox.GetValue(), AmplitudeTextBox.GetValue(), MeanTextBox.GetValue(), DeviationTextBox.GetValue());
                    SamplingChartSeries[0].Values = model.GetGaussianNoise(SamplingFrequencyTextBox.GetValue(), TimeDurationTextBox.GetValue(), AmplitudeTextBox.GetValue(), MeanTextBox.GetValue(), DeviationTextBox.GetValue());
                    here = (ChartValues<ObservablePoint>)model.GetGaussianNoise(ReconstructionFrequencyTextBox.GetValue(), TimeDurationTextBox.GetValue(), AmplitudeTextBox.GetValue(), MeanTextBox.GetValue(), DeviationTextBox.GetValue());
                    here1 = (ChartValues<ObservablePoint>)model.GetGaussianNoise(ReconstructionFrequencyTextBox.GetValue(), TimeDurationTextBox.GetValue(), AmplitudeTextBox.GetValue(), MeanTextBox.GetValue(), DeviationTextBox.GetValue());
                    break;
                case 2:
                    NormalChartSeries[0].Values = model.GetSinSignal(SignalFrequencyTextBox.GetValue(), TimeDurationTextBox.GetValue(), AmplitudeTextBox.GetValue(), FrequencyTextBox.GetValue(), StartTimetextBox.GetValue());
                    SamplingChartSeries[0].Values = model.GetSinSignal(SamplingFrequencyTextBox.GetValue(), TimeDurationTextBox.GetValue(), AmplitudeTextBox.GetValue(), FrequencyTextBox.GetValue(), StartTimetextBox.GetValue());
                    here = (ChartValues<ObservablePoint>)model.GetSinSignal(ReconstructionFrequencyTextBox.GetValue(), TimeDurationTextBox.GetValue(), AmplitudeTextBox.GetValue(), FrequencyTextBox.GetValue(), StartTimetextBox.GetValue());
                    here1 = (ChartValues<ObservablePoint>)model.GetSinSignal(ReconstructionFrequencyTextBox.GetValue(), TimeDurationTextBox.GetValue(), AmplitudeTextBox.GetValue(), FrequencyTextBox.GetValue(), StartTimetextBox.GetValue());
                    break;
                case 3:
                    NormalChartSeries[0].Values = model.GetSinAbsSignal(SignalFrequencyTextBox.GetValue(), TimeDurationTextBox.GetValue(), AmplitudeTextBox.GetValue(), FrequencyTextBox.GetValue(), StartTimetextBox.GetValue());
                    SamplingChartSeries[0].Values = model.GetSinAbsSignal(SamplingFrequencyTextBox.GetValue(), TimeDurationTextBox.GetValue(), AmplitudeTextBox.GetValue(), FrequencyTextBox.GetValue(), StartTimetextBox.GetValue());
                    here = (ChartValues<ObservablePoint>)model.GetSinAbsSignal(ReconstructionFrequencyTextBox.GetValue(), TimeDurationTextBox.GetValue(), AmplitudeTextBox.GetValue(), FrequencyTextBox.GetValue(), StartTimetextBox.GetValue());
                    here1 = (ChartValues<ObservablePoint>)model.GetSinAbsSignal(ReconstructionFrequencyTextBox.GetValue(), TimeDurationTextBox.GetValue(), AmplitudeTextBox.GetValue(), FrequencyTextBox.GetValue(), StartTimetextBox.GetValue());
                    break;
                case 4:
                    NormalChartSeries[0].Values = model.GetSinDoubleAbsSignal(SignalFrequencyTextBox.GetValue(), TimeDurationTextBox.GetValue(), AmplitudeTextBox.GetValue(), FrequencyTextBox.GetValue(), StartTimetextBox.GetValue());
                    SamplingChartSeries[0].Values = model.GetSinDoubleAbsSignal(SamplingFrequencyTextBox.GetValue(), TimeDurationTextBox.GetValue(), AmplitudeTextBox.GetValue(), FrequencyTextBox.GetValue(), StartTimetextBox.GetValue());
                    here = (ChartValues<ObservablePoint>)model.GetSinDoubleAbsSignal(ReconstructionFrequencyTextBox.GetValue(), TimeDurationTextBox.GetValue(), AmplitudeTextBox.GetValue(), FrequencyTextBox.GetValue(), StartTimetextBox.GetValue());
                    break;
                case 5:
                    NormalChartSeries[0].Values = model.GetRectangularSignal(SignalFrequencyTextBox.GetValue(), TimeDurationTextBox.GetValue(), AmplitudeTextBox.GetValue(), PeriodTextBox.GetValue(), StartTimetextBox.GetValue(), DutyCycleTextBox.GetValue());
                    SamplingChartSeries[0].Values = model.GetRectangularSignal(SamplingFrequencyTextBox.GetValue(), TimeDurationTextBox.GetValue(), AmplitudeTextBox.GetValue(), PeriodTextBox.GetValue(), StartTimetextBox.GetValue(), DutyCycleTextBox.GetValue());
                    here = (ChartValues<ObservablePoint>)model.GetRectangularSignal(ReconstructionFrequencyTextBox.GetValue(), TimeDurationTextBox.GetValue(), AmplitudeTextBox.GetValue(), PeriodTextBox.GetValue(), StartTimetextBox.GetValue(), DutyCycleTextBox.GetValue());
                    break;
                case 6:
                    NormalChartSeries[0].Values = model.GetSymmetricRectangularSignal(SignalFrequencyTextBox.GetValue(), TimeDurationTextBox.GetValue(), AmplitudeTextBox.GetValue(), PeriodTextBox.GetValue(), StartTimetextBox.GetValue(), DutyCycleTextBox.GetValue());
                    SamplingChartSeries[0].Values = model.GetSymmetricRectangularSignal(SamplingFrequencyTextBox.GetValue(), TimeDurationTextBox.GetValue(), AmplitudeTextBox.GetValue(), PeriodTextBox.GetValue(), StartTimetextBox.GetValue(), DutyCycleTextBox.GetValue());
                    here = (ChartValues<ObservablePoint>)model.GetSymmetricRectangularSignal(ReconstructionFrequencyTextBox.GetValue(), TimeDurationTextBox.GetValue(), AmplitudeTextBox.GetValue(), PeriodTextBox.GetValue(), StartTimetextBox.GetValue(), DutyCycleTextBox.GetValue());
                    break;
                case 7:
                    NormalChartSeries[0].Values = model.GetTriangularSignal(SignalFrequencyTextBox.GetValue(), TimeDurationTextBox.GetValue(), AmplitudeTextBox.GetValue(), PeriodTextBox.GetValue(), StartTimetextBox.GetValue(), DutyCycleTextBox.GetValue());
                    SamplingChartSeries[0].Values = model.GetTriangularSignal(SamplingFrequencyTextBox.GetValue(), TimeDurationTextBox.GetValue(), AmplitudeTextBox.GetValue(), PeriodTextBox.GetValue(), StartTimetextBox.GetValue(), DutyCycleTextBox.GetValue());
                    here = (ChartValues<ObservablePoint>)model.GetTriangularSignal(ReconstructionFrequencyTextBox.GetValue(), TimeDurationTextBox.GetValue(), AmplitudeTextBox.GetValue(), PeriodTextBox.GetValue(), StartTimetextBox.GetValue(), DutyCycleTextBox.GetValue());
                    break;
                case 8:
                    NormalChartSeries[0].Values = model.GetJumpSignal(SignalFrequencyTextBox.GetValue(), TimeDurationTextBox.GetValue(), AmplitudeTextBox.GetValue(), JumpTimeTextBox.GetValue(), StartTimetextBox.GetValue());
                    SamplingChartSeries[0].Values = model.GetJumpSignal(SamplingFrequencyTextBox.GetValue(), TimeDurationTextBox.GetValue(), AmplitudeTextBox.GetValue(), JumpTimeTextBox.GetValue(), StartTimetextBox.GetValue());
                    here = (ChartValues<ObservablePoint>)model.GetJumpSignal(ReconstructionFrequencyTextBox.GetValue(), TimeDurationTextBox.GetValue(), AmplitudeTextBox.GetValue(), JumpTimeTextBox.GetValue(), StartTimetextBox.GetValue());
                    break;
                case 9:
                    NormalChartSeries[0].Values = model.GetSingleImpulseSignal(SignalFrequencyTextBox.GetValue(), TimeDurationTextBox.GetValue(), AmplitudeTextBox.GetValue(), JumpTimeTextBox.GetValue(), StartTimetextBox.GetValue());
                    SamplingChartSeries[0].Values = model.GetSingleImpulseSignal(SamplingFrequencyTextBox.GetValue(), TimeDurationTextBox.GetValue(), AmplitudeTextBox.GetValue(), JumpTimeTextBox.GetValue(), StartTimetextBox.GetValue());
                    here = (ChartValues<ObservablePoint>)model.GetSingleImpulseSignal(ReconstructionFrequencyTextBox.GetValue(), TimeDurationTextBox.GetValue(), AmplitudeTextBox.GetValue(), JumpTimeTextBox.GetValue(), StartTimetextBox.GetValue());
                    break;
                case 10:
                    NormalChartSeries[0].Values = model.GetImpulseNoise(SignalFrequencyTextBox.GetValue(), TimeDurationTextBox.GetValue(), AmplitudeTextBox.GetValue(), ImpulseProbabilityTextBox.GetValue());
                    SamplingChartSeries[0].Values = model.GetImpulseNoise(SamplingFrequencyTextBox.GetValue(), TimeDurationTextBox.GetValue(), AmplitudeTextBox.GetValue(), ImpulseProbabilityTextBox.GetValue());
                    here = (ChartValues<ObservablePoint>)model.GetImpulseNoise(ReconstructionFrequencyTextBox.GetValue(), TimeDurationTextBox.GetValue(), AmplitudeTextBox.GetValue(), ImpulseProbabilityTextBox.GetValue());
                    break;
            }

            if (SelectedSignal >= 9)
            {
                HistogramLabels = SignalLogics.GetHistogramLabels((ChartValues<ObservablePoint>)NormalChartSeries[0].Values);
                HistogramChartSeries[0].Values = model.GetHistogram((ChartValues<ObservablePoint>)NormalChartSeries[0].Values);
            }
            else
            {
                HistogramLabels = SignalLogics.GetHistogramLabels((ChartValues<ObservablePoint>)NormalChartSeries[0].Values);
                HistogramChartSeries[0].Values = model.GetHistogram((ChartValues<ObservablePoint>)NormalChartSeries[0].Values);
            }

            QuantChartSeries[0].Values = model.GetQuant((ChartValues<ObservablePoint>)SamplingChartSeries[0].Values, QuantLevelAmountTextBox.GetValue());
            QuantChartSeries[1].Values = (ChartValues<ObservablePoint>)NormalChartSeries[0].Values;

            ZeroHoldChartSeries[0].Values = (ChartValues<ObservablePoint>)SamplingChartSeries[0].Values;
            ZeroHoldChartSeries[1].Values = (ChartValues<ObservablePoint>)NormalChartSeries[0].Values;

            SincReconstructionChartSeries[0].Values = model.GetSincReconstruction(ReconstructionFrequencyTextBox.GetValue(), SamplingFrequencyTextBox.GetValue(), TimeDurationTextBox.GetValue(), (ChartValues<ObservablePoint>)SamplingChartSeries[0].Values, SeenSamplesTextBox.GetValue());
            SincReconstructionChartSeries[1].Values = (ChartValues<ObservablePoint>)NormalChartSeries[0].Values;

            AverageTextBox.Text = ResultLogics.GetAverage((ChartValues<ObservablePoint>)NormalChartSeries[0].Values).ToString();
            AbsAverageTextBox.Text = ResultLogics.GetAbsAverage((ChartValues<ObservablePoint>)NormalChartSeries[0].Values).ToString();
            VariationTextBox.Text = ResultLogics.GetVariation((ChartValues<ObservablePoint>)NormalChartSeries[0].Values).ToString();
            AveragePowerTextBox.Text = ResultLogics.GetAveragePower((ChartValues<ObservablePoint>)NormalChartSeries[0].Values).ToString();
            RootMeanSquareTextBox.Text = ResultLogics.GetRootMeanSquare((ChartValues<ObservablePoint>)NormalChartSeries[0].Values).ToString();

            MeanSquareErrorTextBox.Text = ResultLogics.GetMeanSquareError((ChartValues<ObservablePoint>)SincReconstructionChartSeries[0].Values, here).ToString();

            //DLA KWANTY
            RatioTextBox.Text = ResultLogics.GetRatio((ChartValues<ObservablePoint>)QuantChartSeries[0].Values, (ChartValues<ObservablePoint>)SamplingChartSeries[0].Values).ToString();
            MaxRatioTextBox.Text = ResultLogics.GetMaxRatio((ChartValues<ObservablePoint>)QuantChartSeries[0].Values, (ChartValues<ObservablePoint>)SamplingChartSeries[0].Values).ToString();

            MaxDiffrenceTextBox.Text = ResultLogics.GetMaxDiffrence((ChartValues<ObservablePoint>)SincReconstructionChartSeries[0].Values, here).ToString();
        }

        public void Save()
        {
            DataCapsule capsule = new DataCapsule((ChartValues<ObservablePoint>)NormalChartSeries[0].Values, SignalFrequencyTextBox.GetValue());
            model.Serialize(capsule);
        }

        public void Load()
        {
            DataCapsule loaded = model.Deserialize();
            NormalChartSeries[0].Values = loaded.GetValues();
            SignalFrequencyTextBox.Text = loaded.SamplingFrequency.ToString();
        }

        public void Add()
        {
            NormalChartSeries[0].Values = model.Add();
        }

        public void Subtract()
        {
            NormalChartSeries[0].Values = model.Subtract();
        }

        public void Multiply()
        {
            NormalChartSeries[0].Values = model.Multiply();
        }

        public void Divide()
        {
            NormalChartSeries[0].Values = model.Divide();
        }

        public void Weave()
        {
            NormalChartSeries[0].Values = model.Weave();
        }

        public void Correlation()
        {
            NormalChartSeries[0].Values = model.Correlation();
        }

        public void WeaveCorrelation()
        {
            NormalChartSeries[0].Values = model.WeaveCorrelation();
        }

        public void Filter()
        {
            NormalChartSeries[0].Values = model.Filter(MAmountTextBox.GetValue(), CutOffSamplingFrequencyTextBox.GetValue(), WindowComboBox.SelectedIndex, FilterComboBox.SelectedIndex);
        }

        public void Sensor()
        {
            SensorWindow sensorWindow = new SensorWindow();
            sensorWindow.Show();
        }

        public void Transformations()
        {
            Zad4Window zad4Window = new Zad4Window();
            zad4Window.Show();
        }
    }
}
