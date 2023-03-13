using MyAgenda.MVVM.View;
using MyAgenda.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Eventing.Reader;
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
        bool weekChanged;
        //Индикация отрисовки View разных размеров
        private bool medDrawn;
        private bool mainDrawn;
        private bool minDrawn;
        private int size;
        DateTime datenow;

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

            SizeChanged += MainWindow_SizeChanged;

            datenow = DateTime.Now;

            WeekDate();
        }

        private void WeekDate()
        {            
            for (int currentDayIndex = 0; currentDayIndex < week.Count; currentDayIndex++)
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
                    frame.Navigate(new MainPageView(datenow));
                    mainDrawn = true;
                    medDrawn= false;
                    minDrawn= false;
                    size = 3;
                }
            }
            else
            if (MainView.ActualWidth > 740)
            {
                if (!medDrawn)
                {
                    frame.Navigate(new MedPageView(datenow));
                    medDrawn = true;
                    mainDrawn = false;
                    minDrawn = false;
                    size = 2;
                }
            }
            else
            {
                if (!minDrawn)
                {
                    frame.Navigate(new MinPageView(datenow));
                    minDrawn = true;
                    medDrawn = false;
                    mainDrawn = false;
                    size = 1;
                }
            }
        }

        private void LastWeek_Click(object sender, RoutedEventArgs e)
        {
            datenow=datenow.AddDays(-7);
            WeekDate();
            PageUpdate();
        }

        private void NextWeek_Click(object sender, RoutedEventArgs e)
        {
            datenow = datenow.AddDays(7);
            WeekDate();
            PageUpdate();
        }

        private void PageUpdate()
        {
            if (size == 3)
                frame.Navigate(new MainPageView(datenow));
            else if (size == 2)
                frame.Navigate(new MedPageView(datenow));
            else if (size == 1)
                frame.Navigate(new MinPageView(datenow));
        }
    }
}
