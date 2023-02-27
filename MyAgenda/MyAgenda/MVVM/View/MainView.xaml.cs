using MyAgenda.MVVM.Model.Data;
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
    /// Логика взаимодействия для MainView.xaml
    /// </summary>
    public partial class MainView : UserControl
    {
        CultureInfo myCI = new CultureInfo("en-US");
        System.Globalization.Calendar myCalendar;
        CalendarWeekRule calendarWeekRule;
        DayOfWeek firstDayOfWeek;

        DateTime DT = DateTime.Now;

        List<DayOfWeek> week = new List<DayOfWeek>() {
            DayOfWeek.Monday,
            DayOfWeek.Tuesday,
            DayOfWeek.Wednesday,
            DayOfWeek.Thursday,
            DayOfWeek.Friday,
            DayOfWeek.Saturday,
            DayOfWeek.Sunday };

        const int DayCardHeight = 360;

        public MainView()
        {
            InitializeComponent();

            InitializeCalendar();

            CurrentDayOutline();

            int nWidth = (int)SystemParameters.PrimaryScreenWidth;
            int nHieght = (int)SystemParameters.PrimaryScreenHeight;

            ViewB.MaxWidth = nWidth-50;
            ViewB.MaxHeight = nHieght-100;
        }

        private void InitializeCalendar()
        {
            myCalendar = myCI.Calendar;
            calendarWeekRule = myCI.DateTimeFormat.CalendarWeekRule;
            firstDayOfWeek = myCI.DateTimeFormat.FirstDayOfWeek;
        }

        void CurrentDayOutline()
        {
            if (IsEvenWeek())
            {
                ChangeWeekType();
            }

            ShowCurrentDayMark();

            ScrollToCurrentDay(findCurrentDayTrioIndex());

        }

        private bool IsEvenWeek()
        {
            return myCalendar.GetWeekOfYear(DateTime.Now, calendarWeekRule, firstDayOfWeek) % 2 == 0;
        }
        private void ChangeWeekType()
        {
            var uriSource = new Uri("/Resources/Images/BlueDot.png", UriKind.Relative);
            MondayMark.Source = new BitmapImage(uriSource);
            TuesdayMark.Source = new BitmapImage(uriSource);
            WednesdayMark.Source = new BitmapImage(uriSource);
            ThuesdayMark.Source = new BitmapImage(uriSource);
            FridayMark.Source = new BitmapImage(uriSource);
            SaturdayMark.Source = new BitmapImage(uriSource);
        }
        private void ShowCurrentDayMark()
        {
            switch (DT.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    MondayMark.Visibility = Visibility.Visible;
                    break;
                case DayOfWeek.Tuesday:
                    TuesdayMark.Visibility = Visibility.Visible;
                    break;
                case DayOfWeek.Wednesday:
                    WednesdayMark.Visibility = Visibility.Visible;
                    break;
                case DayOfWeek.Thursday:
                    ThuesdayMark.Visibility = Visibility.Visible;
                    break;
                case DayOfWeek.Friday:
                    FridayMark.Visibility = Visibility.Visible;
                    break;
                case DayOfWeek.Saturday:
                    SaturdayMark.Visibility = Visibility.Visible;
                    break;
            }
        }
        private int findCurrentDayTrioIndex()
        {
            for (int dayIndex = 0; dayIndex < week.Count - 1; dayIndex+=3)
            {
                if (week[dayIndex] == DT.DayOfWeek)
                {
                    return dayIndex/3;
                }
            }
            return 0;
        }

        private void ScrollToCurrentDay(int currentDayIndex)
        {
            Scroll.ScrollToVerticalOffset(DayCardHeight * currentDayIndex);
        }
    }
}
