using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyAgenda.Core;

namespace MyAgenda.MVVM.ViewModel
{
    class MinSizeMainViewModel : ObservableObject
    {
        public MinSizeViewModel HomeVM { get; set; }
        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MinSizeMainViewModel()
        {
            HomeVM = new MinSizeViewModel();
            CurrentView = HomeVM;
        }
    }
}
