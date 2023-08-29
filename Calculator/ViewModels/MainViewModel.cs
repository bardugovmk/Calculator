using Calculator.Core;
using System.Windows.Input;

namespace Calculator.ViewModels
{
    class MainViewModel : ObservableObject
    {
        public ICommand StandardViewCommand { get; set; }
        public ICommand AngleViewCommand { get; set; }
        public ICommand AreaViewCommand { get; set; }
        public ICommand CurrencyViewCommand { get; set; }
        public ICommand TemperatureViewCommand { get; set; }

        public StandardViewModel StandardVM { get; set; }
        public AngleViewModel AngleVM { get; set; }
        public AreaViewModel AreaVM { get; set; }
        public CurrencyViewModel CurrencyVM { get; set; }
        public TemperatureViewModel TemperatureVM { get; set; }

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        public MainViewModel()
        {
            StandardVM = new StandardViewModel();
            AngleVM = new AngleViewModel();

            AreaVM = new AreaViewModel();
            CurrencyVM = new CurrencyViewModel();
            TemperatureVM = new TemperatureViewModel();

            CurrentView = StandardVM;

            StandardViewCommand = new RelayCommand(_ => CurrentView = StandardVM);
            AngleViewCommand = new RelayCommand(_ => CurrentView = AngleVM);
            AreaViewCommand = new RelayCommand(_ => CurrentView = AreaVM);
            CurrencyViewCommand = new RelayCommand(_ => CurrentView = CurrencyVM);
            TemperatureViewCommand = new RelayCommand(_ => CurrentView = TemperatureVM);
        }
    }
}