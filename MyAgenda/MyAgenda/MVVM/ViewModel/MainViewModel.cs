using MyAgenda.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAgenda.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public HomeViewModel HomeVM { get; set; }

        public MedSizeViewModel MidVM { get; set; }

        public MinSizeViewModel MinVM { get; set; }
        
        public MaxSizeViewModel MaxVM { get; set; }



        private object _homeCurrentView;

        private object _midCurrentView;

        private object _minCurrentView;

        private object _maxCurrentView;

        public object HomeCurrentView
        {
            get { return _homeCurrentView; }
            set
            {
                _homeCurrentView = value;
                OnPropertyChanged();
            }
        }

        public object MidCurrentView
        {
            get { return _midCurrentView; }
            set
            {
                _midCurrentView = value;
                OnPropertyChanged();
            }
        }

        public object MinCurrentView
        {
            get { return _minCurrentView; }
            set
            {
                _minCurrentView = value;
                OnPropertyChanged();
            }
        }

        public object MaxCurrentView
        {
            get { return _maxCurrentView; }
            set
            {
                _maxCurrentView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            HomeVM = new HomeViewModel();

            MidVM = new MedSizeViewModel();

            MinVM = new MinSizeViewModel();

            MaxVM = new MaxSizeViewModel();

            HomeCurrentView = HomeVM;

            MidCurrentView = MidVM;

            MinCurrentView = MinVM;

            MaxCurrentView = MaxVM;
        }
    }
}
