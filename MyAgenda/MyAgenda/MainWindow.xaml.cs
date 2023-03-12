using MyAgenda.MVVM.View;
using MyAgenda.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace MyAgenda
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int _Mode;
        bool first;
        bool second;
        bool third;
        bool fourth;

        List<DayOfWeek> week = new List<DayOfWeek>() {
            DayOfWeek.Monday,
            DayOfWeek.Tuesday,
            DayOfWeek.Wednesday,
            DayOfWeek.Thursday,
            DayOfWeek.Friday,
            DayOfWeek.Saturday,
            DayOfWeek.Sunday };
        private bool medDrawn;
        private bool mainDrawn;
        private bool minDrawn;

        public MainWindow()
        {
            InitializeComponent();

            SizeChanged += MainWindow_SizeChanged;

            WeekDate();
        }

        private void WeekDate()
        {
            DateTime datenow = DateTime.Now;
            for (int currentDayIndex = 0; currentDayIndex < week.Count - 1; currentDayIndex++)
            {
                if (week[currentDayIndex] == datenow.DayOfWeek)
                {
                    Date.Text = CalculateWeekBorders(datenow, currentDayIndex);
                }
            }
        }

        private string CalculateWeekBorders(DateTime datenow, int currentDayOfWeekIndex)
        {
            string leftBorder = datenow.AddDays(currentDayOfWeekIndex * (-1)).ToString("d");
            string rightBorder = datenow.AddDays(week.Count - currentDayOfWeekIndex - 1).ToString("d");

            return leftBorder + "-" + rightBorder;
        }

        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (MainView.ActualWidth > 1100)
            {
                if (!mainDrawn)
                {

                    frame.Navigate(new MainPageView());
                    mainDrawn = true;
                    medDrawn= false;
                    minDrawn= false;
                }
            }
            else
            if (MainView.ActualWidth > 740)
            {
                if (!medDrawn)
                {

                    frame.Navigate(new MedPageView());
                    medDrawn = true;
                    mainDrawn = false;
                    minDrawn = false;
                }
            }
            else
            {
                if (!minDrawn)
                {

                    frame.Navigate(new MinPageView());
                    minDrawn = true;
                    medDrawn = false;
                    mainDrawn = false;
                }
            }
        }
    }
}
