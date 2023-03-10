﻿using MyAgenda.MVVM.View;
using MyAgenda.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
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
        MainViewModel ShowModel = new MainViewModel();
        private bool isNavigationVisible = false;
        int _Mode;
        bool first;
        bool second;
        bool third;
        bool fourth;

        DoubleAnimation topMenuShowAnimation = new DoubleAnimation();
        DoubleAnimation topMenuHideAnimation = new DoubleAnimation();

        List<DayOfWeek> week= new List<DayOfWeek>() {
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

            TopMenu.Orientation = Orientation.Horizontal;

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
            for (int currentDayIndex = 0; currentDayIndex < week.Count-1; currentDayIndex++)
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
                    fourth= false;
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

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
                if (isNavigationVisible)
                {
                    HideNavigation();
                }
                else
                {
                    ShowNavigation();
                }
        }

        void HideNavigation()
        {
            CollapseNavigation();
            AnimateHiding();
            isNavigationVisible = false;
        }
        private void CollapseNavigation()
        {
            Stud.Visibility = Visibility.Collapsed;
            Teach.Visibility = Visibility.Collapsed;
            Modif.Visibility = Visibility.Collapsed;
            Enter.Visibility = Visibility.Collapsed;
            Mode.Visibility = Visibility.Visible;
        }

        private void AnimateHiding()
        {
            topMenuHideAnimation.From = TopMenu.ActualWidth;
            topMenuHideAnimation.Duration = TimeSpan.FromMilliseconds(500);
            TopMenu.BeginAnimation(StackPanel.WidthProperty, topMenuHideAnimation);
        }

        void ShowNavigation()
        {
            MakeNavigationVisible();
            Mode.Width = 0;
            AnimateShow();
            isNavigationVisible = true;
        }

        private void MakeNavigationVisible()
        {
            Stud.Visibility = Visibility.Visible;
            Teach.Visibility = Visibility.Visible;
            Modif.Visibility = Visibility.Visible;
            Enter.Visibility = Visibility.Visible;
            Mode.Visibility = Visibility.Collapsed;
        }

        private void AnimateShow()
        {
            topMenuShowAnimation.From = TopMenu.ActualWidth;
            topMenuShowAnimation.To = 455;
            topMenuShowAnimation.Duration = TimeSpan.FromMilliseconds(500);
            TopMenu.BeginAnimation(StackPanel.WidthProperty, topMenuShowAnimation);
        }


        private void Modif_Click(object sender, RoutedEventArgs e)
        {
            Mode.Text = "Вы в режиме редактора";
            Mode.Width = 185;
            _Mode = 1;
            topMenuHideAnimation.To = 235;
            HideNavigation();
        }

        private void Teach_Click(object sender, RoutedEventArgs e)
        {
            Mode.Text = "Вы в режиме преподователя";
            Mode.Width = 220;
            _Mode = 2;
            topMenuHideAnimation.To = 270;
            HideNavigation();
        }

        private void Stud_Click(object sender, RoutedEventArgs e)
        {
            Mode.Text = "Вы в режиме студента";
            Mode.Width = 170;
            _Mode = 3;
            topMenuHideAnimation.To = 220;
            HideNavigation();
        }
    }
}
