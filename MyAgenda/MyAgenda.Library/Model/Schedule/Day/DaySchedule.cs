using System;
using System.Collections.Generic;
using System.Linq;
using MyAgenda.Library.Model.Schedule.Entry;

namespace MyAgenda.Library.Model.Schedule.Day
{
    /// <summary>
    /// Учебный день.
    /// Собирается динамически. Не имеет схемы таблицы.
    /// </summary>
    public class DaySchedule : Entity
    {
        /*      _                        _              _       _
         *   __| | __ _ _   _   ___  ___| |__   ___  __| |_   _| | ___
         *  / _` |/ _` | | | | / __|/ __| '_ \ / _ \/ _` | | | | |/ _ \
         * | (_| | (_| | |_| | \__ \ (__| | | |  __/ (_| | |_| | |  __/
         *  \__,_|\__,_|\__, | |___/\___|_| |_|\___|\__,_|\__,_|_|\___|
         *              |___/
         */
        #region DaySchedule

        /// <summary>
        /// Список контейнеров занятий.
        /// Каждое занятие хранится в контейнере с закрепленной за ним позицией
        /// в списке и дополнительной связанной с ней информацией.
        /// </summary>
        private readonly List<SubjectEntry> _subjectList = new List<SubjectEntry>();

        /// <summary>
        /// Конструктор учебного дня без занятий.
        /// </summary>
        public DaySchedule()
        {
            // Заполнение списка занятий пустыми контейнерами.
            foreach (var type in EntityEntry.GetPositionTypeList())
            {
                SubjectList.Add(new SubjectEntry(type));
            }
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="subjectList">Список контейнеров занятий.</param>
        public DaySchedule(List<SubjectEntry> subjectList) : this()
        {
            // Замена пустых контейнеров.
            foreach (var entry in subjectList)
            {
                SubjectList[entry.Index] = entry;
            }
        }

        /// <summary>
        /// Доступ к списку контейнеров занятий.
        /// </summary>
        public List<SubjectEntry> SubjectList => _subjectList;

        /// <summary>
        /// Проверить наличие каких-либо занятий.
        /// </summary>
        /// <returns>Статус проверки.</returns>
        public bool HasAnySubjects()
        {
            return SubjectList.Any(entry => entry.Subject != null);
        }

        /// <summary>
        /// Проверить наличие занятия под указанным индексом.
        /// </summary>
        /// <param name="index">Индекс.</param>
        /// <returns>Статус проверки.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public bool HasSubject(int index)
        {
            foreach (var entry in SubjectList.Where(entry => entry.Index == index))
            {
                return entry.Subject != null;
            }

            throw new ArgumentOutOfRangeException(nameof(index), index, null);
        }

        #endregion
    }
}
