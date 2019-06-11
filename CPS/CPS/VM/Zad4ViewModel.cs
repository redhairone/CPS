using CPS.M;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CPS.VM
{
    class Zad4ViewModel
    {
        private readonly Zad4Model model = new Zad4Model();

        public SeriesCollection W1TopChartSeries { get; set; }
        public SeriesCollection W1BottomChartSeries { get; set; }

        public SeriesCollection W2TopChartSeries { get; set; }
        public SeriesCollection W2BottomChartSeries { get; set; }

        public ICommand GenerateButtonPressed { get; }

        public Zad4ViewModel()
        {
            W1TopChartSeries = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Wykres części dyskretnej"
                }
            };
            W1BottomChartSeries = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Wykres części imaginary"
                }
            };
            W2TopChartSeries = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Wykres części dyskretnej"
                }
            };
            W2BottomChartSeries = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Wykres części imaginary"
                }
            };

            GenerateButtonPressed = new RelayCommand(Generate);
        }

        public void Generate()
        {
            ComplexCapsule complexCapsule = model.GetFourier();

            W1TopChartSeries[0].Values = complexCapsule.GetW1TopValues();
            W1BottomChartSeries[0].Values = complexCapsule.GetW1BottomValues();

            W2TopChartSeries[0].Values = complexCapsule.GetW2TopValues();
            W2BottomChartSeries[0].Values = complexCapsule.GetW2BottomValues();
        }
    }
}
