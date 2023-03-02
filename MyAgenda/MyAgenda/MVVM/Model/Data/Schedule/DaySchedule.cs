using System;
using System.Collections.Generic;

namespace MyAgenda.MVVM.Model.Data.Schedule
{
    /// <summary>
    /// Учебный день.
    /// </summary>
    internal class DaySchedule : Entity, IIndirectlySchemable
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
        public const string Table = "day_schedule";

        /// <summary>
        /// Название столбца с идентификатором.
        /// </summary>
        public const string IdColumn = DataEntity.IdColumn;

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
        public const int SubjectCount = SubjectEntry.PositionTypeCount;

        #endregion

        /// <summary>
        /// Получить тип позиции занятия через название столбца с идентификатором.
        /// </summary>
        /// <param name="columnName">Название столбца с идентификатором занятия.</param>
        /// <returns>Тип позиции занятия.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static PositionType GetPositionType(string columnName)
        {
            switch (columnName)
            {
                case FirstSubjectIdColumn: return PositionType.First;
                case SecondSubjectIdColumn: return PositionType.Second;
                case ThirdSubjectIdColumn: return PositionType.Third;
                case FourthSubjectIdColumn: return PositionType.Fourth;
                case FifthSubjectIdColumn: return PositionType.Fifth;
                case SixthSubjectIdColumn: return PositionType.Sixth;
                case SeventhSubjectIdColumn: return PositionType.Seventh;
            }

            throw new ArgumentException("Некорректное название столбца.");
        }

        /// <summary>
        /// Получить контейнер занятия через название столбца с идентификатором.
        /// </summary>
        /// <param name="columnName">Название столбца с идентификатором занятия.</param>
        /// <returns>Контейнер занятия.</returns>
        public static SubjectEntry GetSubjectPosition(string columnName)
        {
            return new SubjectEntry(GetPositionType(columnName));
        }

        /// <summary>
        /// Получить название столбца с идентификатором занятия через тип позиции.
        /// </summary>
        /// <param name="type">Тип позиции занятия.</param>
        /// <returns>Название столбца с идентификатором занятия.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static string GetIdColumnName(PositionType type)
        {
            switch (type)
            {
                case PositionType.First: return FirstSubjectIdColumn;
                case PositionType.Second: return SecondSubjectIdColumn;
                case PositionType.Third: return ThirdSubjectIdColumn;
                case PositionType.Fourth: return FourthSubjectIdColumn;
                case PositionType.Fifth: return FifthSubjectIdColumn;
                case PositionType.Sixth: return SixthSubjectIdColumn;
                case PositionType.Seventh: return SeventhSubjectIdColumn;
            }

            throw new ArgumentException("Внутренняя ошибка.");
        }

        /// <summary>
        /// Получить название столбца с идентификатором занятия через контейнер.
        /// </summary>
        /// <param name="entry">Контейнер занятия.</param>
        /// <returns>Название столбца с идентификатором занятия.</returns>
        public static string GetIdColumnName(SubjectEntry entry)
        {
            return GetIdColumnName(entry.PositionType);
        }

        /*           _                          _     _
         *  ___  ___| |__   ___ _ __ ___   __ _| |__ | | ___
         * / __|/ __| '_ \ / _ \ '_ ` _ \ / _` | '_ \| |/ _ \
         * \__ \ (__| | | |  __/ | | | | | (_| | |_) | |  __/
         * |___/\___|_| |_|\___|_| |_| |_|\__,_|_.__/|_|\___|
         *
         */
        #region ISchemable

        /// <summary>
        /// Доступ к косвенному родителю со схемой таблицы.
        /// </summary>
        public ISchemable Schemable
        {
            get;
            set;
        }

        /// <summary>
        /// Доступ к идентификатору.
        /// </summary>
        public int Id
        {
            get => Schemable.Id;
            set => Schemable.Id = value;
        }

        /// <summary>
        /// Доступ к схеме данных.
        /// </summary>
        public static Schema Schema
        {
            get
            {
                List<Column> columnList = DataEntity.Schema.ColumnList;

                columnList.Add(new IntColumn(FirstSubjectIdColumn) { IsNullable = true });
                columnList.Add(new IntColumn(SecondSubjectIdColumn) { IsNullable = true });
                columnList.Add(new IntColumn(ThirdSubjectIdColumn) { IsNullable = true });
                columnList.Add(new IntColumn(FourthSubjectIdColumn) { IsNullable = true });
                columnList.Add(new IntColumn(FifthSubjectIdColumn) { IsNullable = true });
                columnList.Add(new IntColumn(SixthSubjectIdColumn) { IsNullable = true });
                columnList.Add(new IntColumn(SeventhSubjectIdColumn) { IsNullable = true });

                return new Schema(Table, columnList, new List<ReferenceLink>()
                {
                    new ReferenceLink(FirstSubjectIdColumn, Subject.Table, Subject.IdColumn),
                    new ReferenceLink(SecondSubjectIdColumn, Subject.Table, Subject.IdColumn),
                    new ReferenceLink(ThirdSubjectIdColumn, Subject.Table, Subject.IdColumn),
                    new ReferenceLink(FourthSubjectIdColumn, Subject.Table, Subject.IdColumn),
                    new ReferenceLink(FifthSubjectIdColumn, Subject.Table, Subject.IdColumn),
                    new ReferenceLink(SixthSubjectIdColumn, Subject.Table, Subject.IdColumn),
                    new ReferenceLink(SeventhSubjectIdColumn, Subject.Table, Subject.IdColumn)
                });
            }
        }

        /// <summary>
        /// Инициализировать сущность из схемы с данными.
        /// </summary>
        /// <param name="data">Схема, заполненная данными.</param>
        /// <returns>Сущность.</returns>
        public static DaySchedule FromData(Schema data)
        {
            // Базовый уровень валидации.
            DataEntity.FromData(data);

            return new DaySchedule(data.GetIntColumnData(IdColumn));
        }

        /// <summary>
        /// Инициализировать сущность из схемы с данными.
        /// </summary>
        /// <param name="data">Схема, заполненная данными.</param>
        /// <param name="subjectList">Список зависимых сущностей.</param>
        /// <returns>Сущность.</returns>
        public static DaySchedule FromData(Schema data, List<SubjectEntry> subjectList)
        {
            // Базовый уровень валидации.
            DataEntity.FromData(data);

            return new DaySchedule(data.GetIntColumnData(IdColumn), subjectList);
        }

        /// <summary>
        /// Получить схему таблицы с данными.
        /// </summary>
        /// <returns>Схема, заполненная данными.</returns>
        public Schema ToData()
        {
            Schema data = Schema;

            data.SetColumnData(IdColumn, Id);

            foreach (SubjectEntry entry in SubjectList)
            {
                if (entry.Subject == null)
                {
                    continue;
                }

                data.SetColumnData(GetIdColumnName(entry), entry.Subject.Id);
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
        /// Каждое занятие хранится в контейнере с закрепленной за ним позицией
        /// в списке и дополнительной связанной с ней информацией.
        /// </summary>
        private readonly List<SubjectEntry> _subjectList = new List<SubjectEntry>();

        /// <summary>
        /// Конструктор учебного дня без занятий.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        public DaySchedule(int id)
        {
            Schemable = new DataEntity(id);

            // Заполнение списка занятий пустыми контейнерами.
            foreach (PositionType type in SubjectEntry.GetPositionTypeList())
            {
                SubjectList[SubjectEntry.GetIndex(type)] = new SubjectEntry(type);
            }
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="subjectList">Список занятий.</param>
        /// <exception cref="ArgumentException"></exception>
        public DaySchedule(int id, List<SubjectEntry> subjectList) : this(id)
        {
            // Замена пустых контейнеров.
            foreach (SubjectEntry entry in subjectList)
            {
                SubjectList[entry.Index] = entry;
            }
        }

        /// <summary>
        /// Доступ к списку занятий.
        /// </summary>
        public List<SubjectEntry> SubjectList => _subjectList;

        /// <summary>
        /// Проверить наличие каких-либо занятий.
        /// </summary>
        /// <returns>Статус проверки.</returns>
        public bool HasAnySubjects()
        {
            foreach (SubjectEntry entry in SubjectList)
            {
                if (entry.Subject != null)
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
            foreach (SubjectEntry entry in SubjectList)
            {
                if (entry.Index != index)
                {
                    continue;
                }

                return entry.Subject != null;
            }

            throw new ArgumentOutOfRangeException("Указанный индекс вышел за допустимые рамки.");
        }

        #endregion
    }
}
