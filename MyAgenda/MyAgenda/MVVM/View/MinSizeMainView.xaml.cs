using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace MyAgenda.MVVM.View
{
    /// <summary>
    /// Логика взаимодействия для MinSizeMainView.xaml
    /// </summary>
    public partial class MinSizeMainView : UserControl
    {
        public MinSizeMainView()
        {
            InitializeComponent();

            CurDayOutline();
        }

        void CurDayOutline()
        {
            CultureInfo myCI = new CultureInfo("en-US");
            System.Globalization.Calendar myCalendar = myCI.Calendar;
            CalendarWeekRule myCWR = myCI.DateTimeFormat.CalendarWeekRule;
            DayOfWeek myFirstDOW = myCI.DateTimeFormat.FirstDayOfWeek;


            if (myCalendar.GetWeekOfYear(DateTime.Now, myCWR, myFirstDOW) % 2 == 0)
            {
                var uriSource = new Uri("/Resources/Images/BlueDot.png", UriKind.Relative);
                CurMon.Source = new BitmapImage(uriSource);
                CurTue.Source = new BitmapImage(uriSource);
                CurWed.Source = new BitmapImage(uriSource);
                CurThu.Source = new BitmapImage(uriSource);
                CurFri.Source = new BitmapImage(uriSource);
                CurSat.Source = new BitmapImage(uriSource);
            }

            DateTime DT = DateTime.Now;
            if (DT.DayOfWeek == DayOfWeek.Monday)
            {
                CurMon.Visibility = Visibility.Visible;
            }
            else if (DT.DayOfWeek == DayOfWeek.Tuesday)
            {
                CurTue.Visibility = Visibility.Visible;
                Scroll.ScrollToVerticalOffset(360);
            }
            else if (DT.DayOfWeek == DayOfWeek.Wednesday)
            {
                CurWed.Visibility = Visibility.Visible;
                Scroll.ScrollToVerticalOffset(720);
            }
            else if (DT.DayOfWeek == DayOfWeek.Thursday)
            {
                CurThu.Visibility = Visibility.Visible;
                Scroll.ScrollToVerticalOffset(1080);
            }
            else if (DT.DayOfWeek == DayOfWeek.Friday)
            {
                CurFri.Visibility = Visibility.Visible;
                Scroll.ScrollToVerticalOffset(1440);
            }
            else if (DT.DayOfWeek == DayOfWeek.Saturday)
            {
                CurSat.Visibility = Visibility.Visible;
                Scroll.ScrollToVerticalOffset(1800);
            }
        }
    }
}
