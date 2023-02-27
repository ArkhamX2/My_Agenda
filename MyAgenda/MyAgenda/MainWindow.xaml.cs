using MyAgenda.MVVM.View;
using MyAgenda.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyAgenda
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainViewModel ShowModel = new MainViewModel();
        bool Visible=false;
        int _Mode;
        public MainWindow()
        {
            InitializeComponent();

            SizeChanged += MainWindow_SizeChanged;

            WeekDate();
        }

        private void WeekDate()
        {
            DateTime datenow = DateTime.Now;
            if (datenow.DayOfWeek == DayOfWeek.Monday)
                Date.Text = (datenow.AddDays(0).ToString("d")) + "-" + datenow.AddDays(6).ToString("d");
            else if (datenow.DayOfWeek == DayOfWeek.Tuesday)
                Date.Text = (datenow.AddDays(-1).ToString("d")) + "-" + datenow.AddDays(5).ToString("d");
            else if (datenow.DayOfWeek == DayOfWeek.Wednesday)
                Date.Text = (datenow.AddDays(-2).ToString("d")) + "-" + datenow.AddDays(4).ToString("d");
            else if (datenow.DayOfWeek == DayOfWeek.Thursday)
                Date.Text = (datenow.AddDays(-3).ToString("d")) + "-" + datenow.AddDays(3).ToString("d");
            else if (datenow.DayOfWeek == DayOfWeek.Friday)
                Date.Text = (datenow.AddDays(-4).ToString("d")) + "-" + datenow.AddDays(2).ToString("d");
            else if (datenow.DayOfWeek == DayOfWeek.Saturday)
                Date.Text = (datenow.AddDays(-5).ToString("d")) + "-" + datenow.AddDays(1).ToString("d");
            else if (datenow.DayOfWeek == DayOfWeek.Sunday)
                Date.Text = (datenow.AddDays(-6).ToString("d")) + "-" + datenow.AddDays(0).ToString("d");
        }
        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (MainWind.ActualWidth > 1100)
            {
                Content.Content = ShowModel.HomeCurrentView;
            }
            else
                if (MainWind.ActualWidth > 740)
            {
                Content.Content = ShowModel.MidCurrentView;
            }
            else
                Content.Content = ShowModel.MinCurrentView;
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            if (Visible)
            {
                YesVisible();
            }
            else
            {
                NoVisible();
            }


        }

        void YesVisible ()
        {

            Visible = false;
            
            if (Content.Content != ShowModel.MinCurrentView)
            {
                Stud.Visibility = Visibility.Collapsed;
                Teach.Visibility = Visibility.Collapsed;
                Modif.Visibility = Visibility.Collapsed;
                Enter.Visibility = Visibility.Collapsed;
                Mode.Visibility = Visibility.Visible;

                TopMenu.Orientation = Orientation.Horizontal;
                if (_Mode == 1) Mode.Width = 185;
                else if (_Mode == 2) Mode.Width = 220;
                else Mode.Width = 170;

                DoubleAnimation topMenuAnimation = new DoubleAnimation();
                topMenuAnimation.From = TopMenu.ActualWidth;
                if (_Mode == 1) topMenuAnimation.To = 235;
                else if (_Mode == 2) topMenuAnimation.To = 270;
                else topMenuAnimation.To = 220;

                topMenuAnimation.Duration = TimeSpan.FromMilliseconds(500);
                TopMenu.BeginAnimation(StackPanel.WidthProperty, topMenuAnimation);
            }
            else
            {

            }
        }

        void NoVisible ()
        {

            Visible = true;

            if (Content.Content != ShowModel.MinCurrentView)
            {
                Stud.Visibility = Visibility.Visible;
                Teach.Visibility = Visibility.Visible;
                Modif.Visibility = Visibility.Visible;
                Enter.Visibility = Visibility.Visible;
                Mode.Visibility = Visibility.Collapsed;

                TopMenu.Orientation = Orientation.Horizontal;
                Mode.Width = 0;

                DoubleAnimation topMenuAnimation = new DoubleAnimation();
                topMenuAnimation.From = TopMenu.ActualWidth;
                topMenuAnimation.To = 455;
                topMenuAnimation.Duration = TimeSpan.FromMilliseconds(500);
                TopMenu.BeginAnimation(StackPanel.WidthProperty, topMenuAnimation);
            }
            else
            {

            }
        }

        private void Modif_Click(object sender, RoutedEventArgs e)
        {
            Mode.Text = "Вы в режиме редактора";
            Mode.Width = 185;
            _Mode = 1;
            YesVisible();
        }

        private void Teach_Click(object sender, RoutedEventArgs e)
        {
            Mode.Text = "Вы в режиме преподователя";
            Mode.Width = 220;
            _Mode = 2;
            YesVisible();
        }

        private void Stud_Click(object sender, RoutedEventArgs e)
        {
            Mode.Text = "Вы в режиме студента";
            Mode.Width = 170;
            _Mode = 3;
            YesVisible();
        }
    }
}
