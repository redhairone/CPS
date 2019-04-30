using System.Windows.Input;
using ViewModel;

namespace CPS.TheView
{
    class View
    {
        public int SelectedSignal { get; set; }
        
        public ICommand GenerateButton { get; }

        public View()
        {
            GenerateButton = new RelayCommand(Generate);
        }

        public void Generate()
        {
            //TODO
        }
    }
}
