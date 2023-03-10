using System.Collections.Generic;
using MyAgenda.Library.Model.Base;
using MyAgenda.Library.Model.Schedule.Entry;

namespace MyAgenda.Library.Model.Schedule.Week
{
    /// <summary>
    /// Учебная неделя для преподавателя.
    /// Собирается динамически. Не имеет схемы таблицы.
    /// </summary>
    public class TeacherWeekSchedule : WeekSchedule
    {
        /*                    _               _              _       _
         * __      _____  ___| | __  ___  ___| |__   ___  __| |_   _| | ___
         * \ \ /\ / / _ \/ _ \ |/ / / __|/ __| '_ \ / _ \/ _` | | | | |/ _ \
         *  \ V  V /  __/  __/   <  \__ \ (__| | | |  __/ (_| | |_| | |  __/
         *   \_/\_/ \___|\___|_|\_\ |___/\___|_| |_|\___|\__,_|\__,_|_|\___|
         *
         */
        #region WeekSchedule

        /// <summary>
        /// Конструктор учебной недели без учебных дней.
        /// </summary>
        /// <param name="teacher">Преподаватель.</param>
        /// <param name="weekType">Тип недели.</param>
        public TeacherWeekSchedule(Teacher teacher, WeekType weekType) : base(teacher, weekType)
        {
            // PASS.
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="teacher">Преподаватель.</param>
        /// <param name="weekType">Тип недели.</param>
        /// <param name="dayList">Список контейнеров учебных дней.</param>
        public TeacherWeekSchedule(Teacher teacher, WeekType weekType, List<DayScheduleEntry> dayList) : base(teacher, weekType, dayList)
        {
            // PASS.
        }

        /// <summary>
        /// Доступ к преподавателю.
        /// </summary>
        public Teacher Teacher => Target as Teacher;

        #endregion
    }
}
