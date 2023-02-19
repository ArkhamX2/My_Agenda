using System;
using System.Collections.Generic;

namespace MyAgenda.MVVM.Model.Data.Schedule
{
    /// <summary>
    /// Контейнер данных учебного дня.
    /// </summary>
    internal class DayScheduleData : DataContainer
    {
        /// <summary>
        /// Список идентификаторов занятий.
        /// </summary>
        public List<int> SubjectIdList { get; set; }
    }

    /// <summary>
    /// Учебный день.
    /// </summary>
    internal class DaySchedule : DataEntity
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
        public new const string Table = "day_schedule";

        /// <summary>
        /// Название столбца с идентификатором занятия.
        /// </summary>
        public const string FirstSubjectIdColumn = "first_subject_id";
        public const string SecondSubjectIdColumn = "second_subject_id";
        public const string ThirdSubjectIdColumn = "third_subject_id";
        public const string FourthSubjectIdColumn = "fourth_subject_id";
        public const string FifthSubjectIdColumn = "fifth_subject_id";
        public const string SixthSubjectIdColumn = "sixth_subject_id";
        public const string SeventhSubjectIdColumn = "seventh_subject_id";

        /// <summary>
        /// Количество занятий.
        /// </summary>
        public const int SubjectCount = 7;

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
        public static DayScheduleData Container => new DayScheduleData();

        /// <summary>
        /// Преобразовать данные в новую сущность.
        /// </summary>
        /// <param name="data">Контейнер данных.</param>
        /// <returns>Новая сущность.</returns>
        public static DaySchedule FromData(DayScheduleData data)
        {
            return new DaySchedule(data.Id);
        }

        /// <summary>
        /// Преобразовать данные в новую сущность.
        /// </summary>
        /// <param name="data">Контейнер данных.</param>
        /// <param name="subjectList">Вложенный список сущностей.</param>
        /// <returns>Новая сущность.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static DaySchedule FromData(DayScheduleData data, List<Subject> subjectList)
        {
            // Валидация данных.
            for (int i = 0; i < SubjectCount; i++)
            {
                if (data.SubjectIdList[i] == IdUndefined)
                {
                    if (subjectList[i] == null)
                    {
                        continue;
                    }

                    throw new ArgumentException("Переданные контейнер и список сущностей не соответствуют друг другу.");
                }

                if (subjectList[i] == null)
                {
                    throw new ArgumentException("Переданные контейнер и список сущностей не соответствуют друг другу.");
                }

                if (data.SubjectIdList[i] != subjectList[i].Id)
                {
                    throw new ArgumentException("Переданные контейнер и список сущностей не соответствуют друг другу.");
                }
            }

            return new DaySchedule(data.Id, subjectList);
        }

        /// <summary>
        /// Получить контейнер данных для сущности.
        /// </summary>
        /// <returns>Контейнер данных.</returns>
        public DayScheduleData ToData()
        {
            DayScheduleData data = Container;

            data.Id = Id;
            data.SubjectIdList = new List<int>();

            for (int i = 0; i < SubjectCount; i++)
            {
                data.SubjectIdList[i] = SubjectList[i] == null ? IdUndefined : SubjectList[i].Id;
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
        #region DaySchedule

        /// <summary>
        /// Список занятий.
        /// </summary>
        private readonly List<Subject> _subjectList = new List<Subject>();

        /// <summary>
        /// Конструктор учебного дня без занятий.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        public DaySchedule(int id) : base(id)
        {
            for (int i = 0; i < SubjectCount; i++)
            {
                SubjectList[i] = null;
            }
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="subjectList">Список занятий.</param>
        /// <exception cref="ArgumentException"></exception>
        public DaySchedule(int id, List<Subject> subjectList) : this(id)
        {
            for (int i = 0; i < SubjectCount; i++)
            {
                SubjectList[i] = subjectList[i];
            }
        }

        /// <summary>
        /// Доступ к списку занятий.
        /// </summary>
        public List<Subject> SubjectList => _subjectList;

        /// <summary>
        /// Проверить наличие каких-либо занятий.
        /// </summary>
        /// <returns>Статус проверки.</returns>
        public bool HasAnySubjects()
        {
            for (int i = 0; i < SubjectCount; i++)
            {
                if (SubjectList[i] != null)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Проверить наличие занятия под указанным индексом.
        /// </summary>
        /// <param name="index">Индекс.</param>
        /// <returns>Статус проверки.</returns>
        public bool HasSubject(int index)
        {
            if (index < 0 || index >= SubjectCount)
            {
                return false;
            }

            return SubjectList[index] != null;
        }

        #endregion
    }
}
