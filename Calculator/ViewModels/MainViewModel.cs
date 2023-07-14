using Calculator.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Calculator.ViewModels
{
    class MainViewModel : ObservableObject
    {
        public ICommand StandardViewCommand { get; set; }
        public ICommand AngleViewCommand { get; set; }
        public ICommand AreaViewCommand { get; set; }
        public ICommand CurrencyViewCommand { get; set; }
        public ICommand DataViewCommand { get; set; }
        public ICommand EnergyViewCommand { get; set; }
        public ICommand LengthViewCommand { get; set; }
        public ICommand PowerViewCommand { get; set; }
        public ICommand PressureViewCommand { get; set; }
        public ICommand SpeedViewCommand { get; set; }
        public ICommand TemperatureViewCommand { get; set; }
        public ICommand TimeViewCommand { get; set; }
        public ICommand VolumeViewCommand { get; set; }
        public ICommand WeightAndMassViewCommand { get; set; }

        public StandardViewModel StandardVM { get; set; }
        public AngleViewModel AngleVM { get; set; }
        public AreaViewModel AreaVM { get; set; }
        public CurrencyViewModel CurrencyVM { get; set; }
        public DataViewModel DataVM { get; set; }
        public EnergyViewModel EnergyVM { get; set; }
        public LengthViewModel LengthVM { get; set; }
        public PowerViewModel PowerVM { get; set; }
        public PressureViewModel PressureVM { get; set; }
        public SpeedViewModel SpeedVM { get; set; }
        public TemperatureViewModel TemperatureVM { get; set; }
        public TimeViewModel TimeVM { get; set; }
        public VolumeViewModel VolumeVM { get; set; }
        public WeightAndMassViewModel WeightAndMassVM { get; set; }

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
            DataVM = new DataViewModel();
            EnergyVM = new EnergyViewModel();
            LengthVM = new LengthViewModel();
            PowerVM = new PowerViewModel();
            PressureVM = new PressureViewModel();
            SpeedVM = new SpeedViewModel();
            TemperatureVM = new TemperatureViewModel();
            TimeVM = new TimeViewModel();
            VolumeVM = new VolumeViewModel();
            WeightAndMassVM = new WeightAndMassViewModel();

            CurrentView = StandardVM;

            StandardViewCommand = new RelayCommand(_ => CurrentView = StandardVM);
            AngleViewCommand = new RelayCommand(_ => CurrentView = AngleVM);
            AreaViewCommand = new RelayCommand(_ => CurrentView = AreaVM);
            CurrencyViewCommand = new RelayCommand(_ => CurrentView = CurrencyVM);
            DataViewCommand = new RelayCommand(_ => CurrentView = DataVM);
            EnergyViewCommand = new RelayCommand(_ => CurrentView = EnergyVM);
            LengthViewCommand = new RelayCommand(_ => CurrentView = LengthVM);
            PowerViewCommand = new RelayCommand(_ => CurrentView = PowerVM);
            PressureViewCommand = new RelayCommand(_ => CurrentView = PressureVM);
            SpeedViewCommand = new RelayCommand(_ => CurrentView = SpeedVM);
            TemperatureViewCommand = new RelayCommand(_ => CurrentView = TemperatureVM);
            TimeViewCommand = new RelayCommand(_ => CurrentView = TimeVM);
            VolumeViewCommand = new RelayCommand(_ => CurrentView = VolumeVM);
            WeightAndMassViewCommand = new RelayCommand(_ => CurrentView = WeightAndMassVM);


        }
    }
}