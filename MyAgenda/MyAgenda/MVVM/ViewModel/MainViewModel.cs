using MyAgenda.Core;
using MyAgenda.Library;
using MyAgenda.Library.Model.Base;
using MyAgenda.Library.Model.Schedule.Week;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAgenda.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        #region Fields

        /// <summary>
        /// Выбранный преподаватель
        /// </summary>
        private Teacher _currentTeacher;

        /// <summary>
        /// Выбранная группа
        /// </summary>
        private Group _currentGroup;

        /// <summary>
        /// Выбранный преподаватель расписния
        /// </summary>
        private TeacherWeekSchedule _currentTeacherSchedule;

        /// <summary>
        /// Выбранный преподаватель
        /// </summary>
        private GroupWeekSchedule _currentWeekSchedule;

        /// <summary>
        /// Выбранный группа расписания
        /// </summary>
        private WeekType _currentweek;

        /// <summary>
        /// Выбранная неделя
        /// </summary>
        public List<WeekType> _weeks;

        /// <summary>
        /// Выбранная неделя
        /// </summary>
        public List<Teacher> _teacherList;

        /// <summary>
        /// Выбранная неделя
        /// </summary>
        public List<Group> _groupList;

        /// <summary>
        /// Коллекция списков
        /// </summary>
        private ObservableCollection<Teacher> _teacherLists = new ObservableCollection<Teacher>();

        /// <summary>
        /// Коллекция списков
        /// </summary>
        private ObservableCollection<Group> _groupLists = new ObservableCollection<Group>();
        #endregion

        #region Properties

        #endregion

        #region Commands
        #endregion

        #region Methods
        #endregion

        public MainViewModel()
        {

        }

    }
}