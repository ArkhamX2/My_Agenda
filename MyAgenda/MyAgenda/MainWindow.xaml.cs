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

        public MainWindow()
        {
            InitializeComponent();

            StartScreen();

            SizeChanged += MainWindow_SizeChanged;

            WeekDate();
        }

        private void StartScreen()
        {
            if (MainWind.ActualWidth > 1100)
            {
                first = true;
                second = true;
                third = true;
            }
            else if (MainWind.ActualWidth > 740)
            {
                first = false;
                second = false;
                third = true;
            }
            else
            {
                first = false;
                second = true;
                third = false;
            }
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
            if (MainWind.ActualWidth > 1100)
            {
                if ((first && second) || !fourth)
                {
                    frame.Navigate(new MainPageView());
                    first = false;
                    second = false;
                    fourth = true;
                }
            }
            else
            if (MainWind.ActualWidth > 740)
            {
                if ((!second && third) || fourth)
                {
                    frame.Navigate(new MedPageView());
                    second = true;
                    third = false;
                    fourth = false;
                }
            }
            else
            if ((!first && !third) || !fourth)
            {
                frame.Navigate(new MinPageView());
                first = true;
                third = true;
                fourth = true;
            }
        }
    }
}
