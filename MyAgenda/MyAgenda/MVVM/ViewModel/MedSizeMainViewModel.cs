using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyAgenda.Core;

namespace MyAgenda.MVVM.ViewModel
{

    class MedSizeMainViewModel : ObservableObject
    {
        public MidSizeViewModel HomeVM { get; set; }
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

        public MedSizeMainViewModel()
        {
            HomeVM = new MidSizeViewModel();
            CurrentView = HomeVM;
        }
    }
 
    
}
