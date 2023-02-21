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
    /// Логика взаимодействия для MedSizeMainView.xaml
    /// </summary>
    public partial class MedSizeMainView : UserControl
    {
        public MedSizeMainView()
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
                MondayMark.Source = new BitmapImage(uriSource);
                TuesdayMark.Source = new BitmapImage(uriSource);
                WednesdayMark.Source = new BitmapImage(uriSource);
                ThuesdayMark.Source = new BitmapImage(uriSource);
                FridayMark.Source = new BitmapImage(uriSource);
                SaturdayMark.Source = new BitmapImage(uriSource);
            }

            DateTime DT = DateTime.Now;
            if (DT.DayOfWeek == DayOfWeek.Monday)
            {
                MondayMark.Visibility = Visibility.Visible;
            }
            else if (DT.DayOfWeek == DayOfWeek.Tuesday)
            {
                TuesdayMark.Visibility = Visibility.Visible;
            }
            else if (DT.DayOfWeek == DayOfWeek.Wednesday)
            {
                WednesdayMark.Visibility = Visibility.Visible;
                Scroll.ScrollToVerticalOffset(360);
            }
            else if (DT.DayOfWeek == DayOfWeek.Thursday)
            {
                ThuesdayMark.Visibility = Visibility.Visible;
                Scroll.ScrollToVerticalOffset(360);
            }
            else if (DT.DayOfWeek == DayOfWeek.Friday)
            {
                FridayMark.Visibility = Visibility.Visible;
                Scroll.ScrollToVerticalOffset(720);
            }
            else if (DT.DayOfWeek == DayOfWeek.Saturday)
            {
                SaturdayMark.Visibility = Visibility.Visible;
                Scroll.ScrollToVerticalOffset(720);
            }
        }
    }
}
