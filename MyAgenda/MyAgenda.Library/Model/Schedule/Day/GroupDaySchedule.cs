using System;
using System.Collections.Generic;
using System.Linq;
using MyAgenda.Library.Data;
using MyAgenda.Library.Data.Column;
using MyAgenda.Library.Model.Base;
using MyAgenda.Library.Model.Schedule.Entry;

namespace MyAgenda.Library.Model.Schedule.Day
{
    /// <summary>
    /// Учебный день для группы.
    /// От <see cref="DaySchedule"/> отличается наличием идентификатора
    /// и схемы таблицы.
    /// Косвенно наследует <see cref="ISchemable"/> через второго
    /// родителя <see cref="DataEntity"/>.
    /// </summary>
    public class GroupDaySchedule : DaySchedule, IIndirectlySchemable
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
        internal const string Table = "day_schedule";

        /// <summary>
        /// Название столбца с идентификатором.
        /// </summary>
        internal const string IdColumn = DataEntity.IdColumn;

        /// <summary>
        /// Название столбца с идентификатором занятия.
        /// </summary>
        internal const string FirstSubjectIdColumn = "first_subject_id";
        internal const string SecondSubjectIdColumn = "second_subject_id";
        internal const string ThirdSubjectIdColumn = "third_subject_id";
        internal const string FourthSubjectIdColumn = "fourth_subject_id";
        internal const string FifthSubjectIdColumn = "fifth_subject_id";
        internal const string SixthSubjectIdColumn = "sixth_subject_id";
        internal const string SeventhSubjectIdColumn = "seventh_subject_id";

        /// <summary>
        /// Количество занятий.
        /// </summary>
        public const int SubjectCount = EntityEntry.PositionTypeCount;

        #endregion

        /*             _   _ _                      _
         *   ___ _ __ | |_(_) |_ _   _    ___ _ __ | |_ _ __ _   _
         *  / _ \ '_ \| __| | __| | | |  / _ \ '_ \| __| '__| | | |
         * |  __/ | | | |_| | |_| |_| | |  __/ | | | |_| |  | |_| |
         *  \___|_| |_|\__|_|\__|\__, |  \___|_| |_|\__|_|   \__, |
         *                       |___/                       |___/
         */
        #region EntityEntry

        /// <summary>
        /// Получить позицию занятия через название столбца с идентификатором.
        /// </summary>
        /// <param name="columnName">Название столбца с идентификатором занятия.</param>
        /// <returns>Позиция занятия.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static EntryPosition GetPositionType(string columnName)
        {
            switch (columnName)
            {
                case FirstSubjectIdColumn: return EntryPosition.First;
                case SecondSubjectIdColumn: return EntryPosition.Second;
                case ThirdSubjectIdColumn: return EntryPosition.Third;
                case FourthSubjectIdColumn: return EntryPosition.Fourth;
                case FifthSubjectIdColumn: return EntryPosition.Fifth;
                case SixthSubjectIdColumn: return EntryPosition.Sixth;
                case SeventhSubjectIdColumn: return EntryPosition.Seventh;
                default: throw new ArgumentOutOfRangeException(nameof(columnName), columnName, null);
            }
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
        /// Получить название столбца с идентификатором занятия через позицию.
        /// </summary>
        /// <param name="position">Позиция занятия.</param>
        /// <returns>Название столбца с идентификатором занятия.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static string GetIdColumnName(EntryPosition position)
        {
            switch (position)
            {
                case EntryPosition.First: return FirstSubjectIdColumn;
                case EntryPosition.Second: return SecondSubjectIdColumn;
                case EntryPosition.Third: return ThirdSubjectIdColumn;
                case EntryPosition.Fourth: return FourthSubjectIdColumn;
                case EntryPosition.Fifth: return FifthSubjectIdColumn;
                case EntryPosition.Sixth: return SixthSubjectIdColumn;
                case EntryPosition.Seventh: return SeventhSubjectIdColumn;
                default: throw new ArgumentOutOfRangeException(nameof(position), position, null);
            }
        }

        /// <summary>
        /// Получить название столбца с идентификатором занятия через контейнер.
        /// </summary>
        /// <param name="entry">Контейнер занятия.</param>
        /// <returns>Название столбца с идентификатором занятия.</returns>
        public static string GetIdColumnName(SubjectEntry entry)
        {
            return GetIdColumnName(entry.Position);
        }

        #endregion

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
        }

        /// <summary>
        /// Доступ к идентификатору.
        /// </summary>
        public int Id => Schemable.Id;

        // UNDONE: Невозможно использовать из-за ограничений .Net Framework.
        // Обновление до .Net должно решить проблему.
        // public int Id { get => Schemable.Id; private set => Schemable.Id = value; }

        /// <summary>
        /// Доступ к схеме данных.
        /// </summary>
        internal static Schema Schema
        {
            get
            {
                var columnList = new List<DataColumn>
                {
                    new IntColumn(IdColumn) { IsPrimaryKey = true, IsAutoIncrementable = true },
                    new IntColumn(FirstSubjectIdColumn) { IsNullable = true },
                    new IntColumn(SecondSubjectIdColumn) { IsNullable = true },
                    new IntColumn(ThirdSubjectIdColumn) { IsNullable = true },
                    new IntColumn(FourthSubjectIdColumn) { IsNullable = true },
                    new IntColumn(FifthSubjectIdColumn) { IsNullable = true },
                    new IntColumn(SixthSubjectIdColumn) { IsNullable = true },
                    new IntColumn(SeventhSubjectIdColumn) { IsNullable = true }
                };

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
        /// Инициализировать учебный день из схемы с данными.
        /// </summary>
        /// <param name="data">Схема, заполненная данными.</param>
        /// <returns>Учебный день.</returns>
        /// <exception cref="ArgumentException"></exception>
        internal static GroupDaySchedule FromData(Schema data)
        {
            if (data == null || !data.IsSameAsSample(Schema))
            {
                throw new ArgumentException("Переданная схема не соответствует схеме для сущности.");
            }

            return new GroupDaySchedule(data.GetIntColumnData(IdColumn));
        }

        /// <summary>
        /// Инициализировать учебный день из схемы с данными.
        /// </summary>
        /// <param name="data">Схема, заполненная данными.</param>
        /// <param name="subjectList">Список контейнеров занятий.</param>
        /// <returns>Учебный день.</returns>
        /// <exception cref="ArgumentException"></exception>
        internal static GroupDaySchedule FromData(Schema data, List<SubjectEntry> subjectList)
        {
            if (data == null || !data.IsSameAsSample(Schema))
            {
                throw new ArgumentException("Переданная схема не соответствует схеме для сущности.");
            }

            return new GroupDaySchedule(data.GetIntColumnData(IdColumn), subjectList);
        }

        /// <summary>
        /// Получить схему таблицы с данными.
        /// </summary>
        /// <returns>Схема, заполненная данными.</returns>
        internal Schema ToData()
        {
            var data = Schema;

            data.SetColumnData(IdColumn, Id);

            // "Расчехляем" контейнеры и вставляем занятия
            // в нужные столбцы.
            foreach (var entry in SubjectList.Where(entry => entry.Subject != null))
            {
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
        /// Конструктор учебного дня без занятий.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        public GroupDaySchedule(int id) : base()
        {
            Schemable = new DataEntity(id);
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="subjectList">Список контейнеров занятий.</param>
        public GroupDaySchedule(int id, List<SubjectEntry> subjectList) : base(subjectList)
        {
            Schemable = new DataEntity(id);
        }

        #endregion
    }
}
