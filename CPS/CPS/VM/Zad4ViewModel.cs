using CPS.Logics;
using CPS.M;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CPS.VM
{
    class Zad4ViewModel : INotifyPropertyChanged
    {
        private readonly Zad4Model model = new Zad4Model();
        public event PropertyChangedEventHandler PropertyChanged;
        private string timeMeasured;

        public SeriesCollection W1TopChartSeries { get; set; }
        public SeriesCollection W1BottomChartSeries { get; set; }

        public SeriesCollection W2TopChartSeries { get; set; }
        public SeriesCollection W2BottomChartSeries { get; set; }

        public string TimeMeasured
        {
            get
            {
                return timeMeasured;
            }
            set
            {
                timeMeasured = value;
                OnPropertyChanged("TimeMeasured");
            }
        }

        public ICommand NormalFourierButtonPressed { get; }
        public ICommand NormalWalshButtonPressed { get; }
        public ICommand FastWalshButtonPressed { get; }
        public ICommand FastFourierButtonPressed { get; }

        public Zad4ViewModel()
        {
            W1TopChartSeries = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Część rzeczywista"
                }
            };
            W1BottomChartSeries = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Część urojona"
                }
            };
            W2TopChartSeries = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Moduł liczby zespolonej"
                }
            };
            W2BottomChartSeries = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Liczby w funkcji częstotliwości"
                }
            };

            NormalFourierButtonPressed = new RelayCommand(NormalFourier);
            NormalWalshButtonPressed = new RelayCommand(NormalWalsh);
            FastWalshButtonPressed = new RelayCommand(FastWalsh);
            FastFourierButtonPressed = new RelayCommand(FastFourier);
        }

        public void NormalFourier()
        {
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            ComplexCapsule complexCapsule = model.GetFourier();
            stopwatch.Stop();

            TimeSpan stopWatchElapsed = stopwatch.Elapsed;
            TimeMeasured = stopWatchElapsed.ToString();

            W1TopChartSeries[0].Values = complexCapsule.GetW1TopValues();
            W1BottomChartSeries[0].Values = complexCapsule.GetW1BottomValues();

            W2TopChartSeries[0].Values = complexCapsule.GetW2TopValues();
            W2BottomChartSeries[0].Values = complexCapsule.GetW2BottomValues();
        }

        public void FastFourier()
        {
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            ComplexCapsule complexCapsule = model.GetFastFourier();
            stopwatch.Stop();

            TimeSpan stopWatchElapsed = stopwatch.Elapsed;
            TimeMeasured = stopWatchElapsed.ToString();

            W1TopChartSeries[0].Values = complexCapsule.GetW1TopValues();
            W1BottomChartSeries[0].Values = complexCapsule.GetW1BottomValues();

            W2TopChartSeries[0].Values = complexCapsule.GetW2TopValues();
            W2BottomChartSeries[0].Values = complexCapsule.GetW2BottomValues();
        }

        public void NormalWalsh()
        {
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            ComplexCapsule complexCapsule = model.GetWalsh();
            stopwatch.Stop();

            TimeSpan stopWatchElapsed = stopwatch.Elapsed;
            TimeMeasured = stopWatchElapsed.ToString();

            Console.WriteLine(stopWatchElapsed.ToString());

            W1TopChartSeries[0].Values = complexCapsule.GetW1TopValues();
            W1BottomChartSeries[0].Values = complexCapsule.GetW1BottomValues();

            W2TopChartSeries[0].Values = complexCapsule.GetW2TopValues();
            W2BottomChartSeries[0].Values = complexCapsule.GetW2BottomValues();
        }

        public void FastWalsh()
        {
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            ComplexCapsule complexCapsule = model.GetFastWalsh();
            stopwatch.Stop();

            TimeSpan stopWatchElapsed = stopwatch.Elapsed;
            TimeMeasured = stopWatchElapsed.ToString();

            W1TopChartSeries[0].Values = complexCapsule.GetW1TopValues();
            W1BottomChartSeries[0].Values = complexCapsule.GetW1BottomValues();

            W2TopChartSeries[0].Values = complexCapsule.GetW2TopValues();
            W2BottomChartSeries[0].Values = complexCapsule.GetW2BottomValues();
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
