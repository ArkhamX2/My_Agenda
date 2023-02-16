using System;
using System.Collections.Generic;

namespace MyAgenda.MVVM.Model.Data.Schedule
{
    /// <summary>
    /// Контейнер данных учебной недели.
    /// </summary>
    internal class WeekScheduleData : DataContainer
    {
        /// <summary>
        /// Список идентификаторов учебных дней.
        /// </summary>
        public List<int> DayIdList { get; set; }
    }

    /// <summary>
    /// Учебная неделя.
    /// </summary>
    internal class WeekSchedule : DataEntity
    {
        /*                      _              _
         *   ___ ___  _ __  ___| |_ __ _ _ __ | |_ ___
         *  / __/ _ \| '_ \/ __| __/ _` | '_ \| __/ __|
         * | (_| (_) | | | \__ \ || (_| | | | | |_\__ \
         *  \___\___/|_| |_|___/\__\__,_|_| |_|\__|___/
         *
         */
        #region Constants

        /// <summary>
        /// Название таблицы.
        /// </summary>
        public new const string Table = "week_schedule";

        /// <summary>
        /// Название столбца с идентификатором учебного дня.
        /// </summary>
        public const string FirstDayIdColumn = "first_day_id";
        public const string SecondDayIdColumn = "second_day_id";
        public const string ThirdDayIdColumn = "third_day_id";
        public const string FourthDayIdColumn = "fourth_day_id";
        public const string FifthDayIdColumn = "fifth_day_id";
        public const string SixthDayIdColumn = "sixth_day_id";
        public const string SeventhDayIdColumn = "seventh_day_id";

        /// <summary>
        /// Количество учебных дней.
        /// </summary>
        public const int DayCount = 7;

        #endregion

        /*      _       _                          _        _
         *   __| | __ _| |_ __ _    ___ ___  _ __ | |_ __ _(_)_ __   ___ _ __
         *  / _` |/ _` | __/ _` |  / __/ _ \| '_ \| __/ _` | | '_ \ / _ \ '__|
         * | (_| | (_| | || (_| | | (_| (_) | | | | || (_| | | | | |  __/ |
         *  \__,_|\__,_|\__\__,_|  \___\___/|_| |_|\__\__,_|_|_| |_|\___|_|
         *
         */
        #region DataContainer

        /// <summary>
        /// Доступ к контейнеру данных.
        /// </summary>
        public static WeekScheduleData Container => new WeekScheduleData();

        /// <summary>
        /// Преобразовать данные в новую сущность.
        /// </summary>
        /// <param name="data">Контейнер данных.</param>
        /// <returns>Новая сущность.</returns>
        public static WeekSchedule FromData(WeekScheduleData data)
        {
            return new WeekSchedule(data.Id);
        }

        /// <summary>
        /// Преобразовать данные в новую сущность.
        /// </summary>
        /// <param name="data">Контейнер данных.</param>
        /// <param name="dayList">Вложенный список сущностей.</param>
        /// <returns>Новая сущность.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static WeekSchedule FromData(WeekScheduleData data, List<DaySchedule> dayList)
        {
            // Валидация данных.
            for (int i = 0; i < DayCount; i++)
            {
                if (data.DayIdList[i] == IdUndefined)
                {
                    if (dayList[i] == null)
                    {
                        continue;
                    }

                    throw new ArgumentException("Переданные контейнер и список сущностей не соответствуют друг другу.");
                }

                if (data.DayIdList[i] != IdUndefined && dayList[i] == null)
                {
                    throw new ArgumentException("Переданные контейнер и список сущностей не соответствуют друг другу.");
                }

                if (data.DayIdList[i] != dayList[i].Id)
                {
                    throw new ArgumentException("Переданные контейнер и список сущностей не соответствуют друг другу.");
                }
            }

            return new WeekSchedule(data.Id, dayList);
        }

        /// <summary>
        /// Получить контейнер данных для сущности.
        /// </summary>
        /// <returns>Контейнер данных.</returns>
        public WeekScheduleData ToData()
        {
            WeekScheduleData data = Container;

            data.Id = Id;
            data.DayIdList = new List<int>();

            for (int i = 0; i < DayCount; i++)
            {
                data.DayIdList[i] = DayList[i] == null ? IdUndefined : DayList[i].Id;
            }

            return data;
        }

        #endregion

        /*      _                        _              _       _
         *   __| | __ _ _   _   ___  ___| |__   ___  __| |_   _| | ___
         *  / _` |/ _` | | | | / __|/ __| '_ \ / _ \/ _` | | | | |/ _ \
         * | (_| | (_| | |_| | \__ \ (__| | | |  __/ (_| | |_| | |  __/
         *  \__,_|\__,_|\__, | |___/\___|_| |_|\___|\__,_|\__,_|_|\___|
         *              |___/
         */
        #region WeekSchedule

        /// <summary>
        /// Список учебных дней.
        /// </summary>
        private readonly List<DaySchedule> _dayList = new List<DaySchedule>();

        /// <summary>
        /// Конструктор учебного дня без занятий.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        public WeekSchedule(int id) : base(id)
        {
            for (int i = 0; i < DayCount; i++)
            {
                DayList[i] = null;
            }
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="subjectList">Список учебных дней.</param>
        /// <exception cref="ArgumentException"></exception>
        public WeekSchedule(int id, List<DaySchedule> subjectList) : this(id)
        {
            for (int i = 0; i < DayCount; i++)
            {
                DayList[i] = subjectList[i];
            }
        }

        /// <summary>
        /// Доступ к списку учебных дней.
        /// </summary>
        public List<DaySchedule> DayList => _dayList;

        /// <summary>
        /// Проверить наличие каких-либо учебных дней.
        /// </summary>
        /// <returns>Статус проверки.</returns>
        public bool HasAnyDays()
        {
            for (int i = 0; i < DayCount; i++)
            {
                if (DayList[i] != null)
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
            if (index < 0 || index >= DayCount)
            {
                return false;
            }

            return DayList[index] != null;
        }

        #endregion
    }
}
