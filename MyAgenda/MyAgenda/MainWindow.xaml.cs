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
        public MainWindow()
        {
            InitializeComponent();

            SizeChanged += MainWindow_SizeChanged;
        }


        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if(MainWind.ActualWidth>1100)
            {
                Content.Content = ShowModel.HomeCurrentView;
            }
            else
                if (MainWind.ActualWidth > 700)
            {
                Content.Content = ShowModel.MidCurrentView;
            }    
            else
                Content.Content = ShowModel.MinCurrentView;


            //throw new NotImplementedException();
        }
    }
}
