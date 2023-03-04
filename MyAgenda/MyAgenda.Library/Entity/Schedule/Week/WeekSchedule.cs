using System;
using System.Collections.Generic;
using MyAgenda.Library.Entity.Base;
using MyAgenda.Library.Entity.Schedule.Entry;

namespace MyAgenda.Library.Entity.Schedule.Week
{
    /// <summary>
    /// Учебная неделя.
    /// </summary>
    public abstract class WeekSchedule : BaseEntity
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
        /// Сущность, для которой предназначено расписание.
        /// </summary>
        private DataEntity _target;

        /// <summary>
        /// Тип учебной недели.
        /// </summary>
        private WeekType _weekType;

        /// <summary>
        /// Список контейнеров учебных дней.
        /// Каждый учебный день хранится в контейнере с закрепленной за ним позицией
        /// в списке и дополнительной связанной с ней информацией.
        /// </summary>
        private readonly List<DayScheduleEntry> _dayList = new List<DayScheduleEntry>();

        /// <summary>
        /// Конструктор учебной недели без учебных дней.
        /// </summary>
        /// <param name="target">Сущность, для которой предназначено расписание..</param>
        /// <param name="weekType">Тип недели.</param>
        protected WeekSchedule(DataEntity target, WeekType weekType)
        {
            Target = target;
            WeekType = weekType;

            // Заполнение списка учебных дней пустыми контейнерами.
            foreach (EntryPosition type in DayScheduleEntry.GetPositionTypeList())
            {
                DayList[DayScheduleEntry.GetIndex(type)] = new DayScheduleEntry(type);
            }
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="target">Сущность, для которой предназначено расписание..</param>
        /// <param name="weekType">Тип недели.</param>
        /// <param name="dayList">Список контейнеров учебных дней.</param>
        /// <exception cref="ArgumentException"></exception>
        protected WeekSchedule(DataEntity target, WeekType weekType, List<DayScheduleEntry> dayList) : this(target, weekType)
        {
            // Замена пустых контейнеров.
            foreach (DayScheduleEntry entry in dayList)
            {
                DayList[entry.Index] = entry;
            }
        }

        /// <summary>
        /// Доступ к сущности, для которой предназначено расписание.
        /// </summary>
        protected DataEntity Target
        {
            get => _target;
            set => _target = value;
        }

        /// <summary>
        /// Доступ к типу недели.
        /// </summary>
        public WeekType WeekType
        {
            get => _weekType;
            protected set => _weekType = value;
        }

        /// <summary>
        /// Доступ к списку контейнеров учебных дней.
        /// </summary>
        public List<DayScheduleEntry> DayList => _dayList;

        /// <summary>
        /// Проверить наличие каких-либо учебных дней.
        /// </summary>
        /// <returns>Статус проверки.</returns>
        public bool HasAnyDays()
        {
            foreach (DayScheduleEntry entry in DayList)
            {
                if (entry.DaySchedule != null)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Проверить наличие учебного дня под указанным индексом.
        /// </summary>
        /// <param name="index">Индекс.</param>
        /// <returns>Статус проверки.</returns>
        public bool HasDay(int index)
        {
            foreach (DayScheduleEntry entry in DayList)
            {
                if (entry.Index != index)
                {
                    continue;
                }

                return entry.DaySchedule != null;
            }

            throw new ArgumentOutOfRangeException("Указанный индекс вышел за допустимые рамки.");
        }

        #endregion
    }
}
