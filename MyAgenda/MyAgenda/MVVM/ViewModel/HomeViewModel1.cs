﻿using MyAgenda.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAgenda.MVVM.ViewModel
{
    class HomeViewModel1 : ObservableObject
    {
        public DayViewModel DayVM { get; set; }
        private object _currentDayView;

        public object CurrentDayView
        {
            get { return _currentDayView; }
            set
            {
                _currentDayView = value;
                OnPropertyChanged();
            }
        }

        public HomeViewModel1()
        {
            DayVM = new DayViewModel();
            CurrentDayView = DayVM;
        }
    }
}
