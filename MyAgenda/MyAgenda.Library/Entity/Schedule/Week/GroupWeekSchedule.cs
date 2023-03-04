using System;
using System.Collections.Generic;
using MyAgenda.Library.Data;
using MyAgenda.Library.Data.Column;
using MyAgenda.Library.Entity.Base;
using MyAgenda.Library.Entity.Schedule.Entry;

namespace MyAgenda.Library.Entity.Schedule.Week
{
    /// <summary>
    /// Учебная неделя для группы.
    /// Косвенно наследует <see cref="ISchemable"/> через второго
    /// родителя <see cref="DataEntity"/>.
    /// </summary>
    public class GroupWeekSchedule : WeekSchedule, IIndirectlySchemable
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
        internal const string Table = "week_schedule";

        /// <summary>
        /// Название столбца с идентификатором.
        /// </summary>
        internal const string IdColumn = DataEntity.IdColumn;

        /// <summary>
        /// Название столбца с идентификатором группы.
        /// </summary>
        internal const string GroupIdColumn = "group_id";

        /// <summary>
        /// Название столбца с идентификатором типа недели.
        /// </summary>
        internal const string WeekTypeIdColumn = "week_type_id";

        /// <summary>
        /// Название столбца с идентификатором учебного дня.
        /// </summary>
        internal const string FirstDayIdColumn = "first_day_id";
        internal const string SecondDayIdColumn = "second_day_id";
        internal const string ThirdDayIdColumn = "third_day_id";
        internal const string FourthDayIdColumn = "fourth_day_id";
        internal const string FifthDayIdColumn = "fifth_day_id";
        internal const string SixthDayIdColumn = "sixth_day_id";
        internal const string SeventhDayIdColumn = "seventh_day_id";

        /// <summary>
        /// Количество учебных дней.
        /// </summary>
        public const int DayCount = DayScheduleEntry.PositionTypeCount;

        #endregion

        /// <summary>
        /// Получить позицию учебного дня через название столбца с идентификатором.
        /// </summary>
        /// <param name="columnName">Название столбца с идентификатором учебного дня.</param>
        /// <returns>Позиция учебного дня.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static EntryPosition GetPositionType(string columnName)
        {
            switch (columnName)
            {
                case FirstDayIdColumn: return EntryPosition.First;
                case SecondDayIdColumn: return EntryPosition.Second;
                case ThirdDayIdColumn: return EntryPosition.Third;
                case FourthDayIdColumn: return EntryPosition.Fourth;
                case FifthDayIdColumn: return EntryPosition.Fifth;
                case SixthDayIdColumn: return EntryPosition.Sixth;
                case SeventhDayIdColumn: return EntryPosition.Seventh;
            }

            throw new ArgumentException("Некорректное название столбца.");
        }

        /// <summary>
        /// Получить контейнер учебного дня через название столбца с идентификатором.
        /// </summary>
        /// <param name="columnName">Название столбца с идентификатором учебного дня.</param>
        /// <returns>Контейнер учебного дня.</returns>
        public static DayScheduleEntry GetDaySchedulePosition(string columnName)
        {
            return new DayScheduleEntry(GetPositionType(columnName));
        }

        /// <summary>
        /// Получить название столбца с идентификатором учебного дня через позицию.
        /// </summary>
        /// <param name="position">Позиция учебного дня.</param>
        /// <returns>Название столбца с идентификатором учебного дня.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static string GetIdColumnName(EntryPosition position)
        {
            switch (position)
            {
                case EntryPosition.First: return FirstDayIdColumn;
                case EntryPosition.Second: return SecondDayIdColumn;
                case EntryPosition.Third: return ThirdDayIdColumn;
                case EntryPosition.Fourth: return FourthDayIdColumn;
                case EntryPosition.Fifth: return FifthDayIdColumn;
                case EntryPosition.Sixth: return SixthDayIdColumn;
                case EntryPosition.Seventh: return SeventhDayIdColumn;
            }

            throw new ArgumentException("Внутренняя ошибка.");
        }

        /// <summary>
        /// Получить название столбца с идентификатором учебного дня через контейнер.
        /// </summary>
        /// <param name="entry">Контейнер учебного дня.</param>
        /// <returns>Название столбца с идентификатором учебного дня.</returns>
        public static string GetIdColumnName(DayScheduleEntry entry)
        {
            return GetIdColumnName(entry.Position);
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
                List<DataColumn> columnList = new List<DataColumn>
                {
                    new IntColumn(IdColumn) { IsPrimaryKey = true, IsAutoIncrementable = true },
                    new IntColumn(GroupIdColumn),
                    new IntColumn(WeekTypeIdColumn),
                    new IntColumn(FirstDayIdColumn) { IsNullable = true },
                    new IntColumn(SecondDayIdColumn) { IsNullable = true },
                    new IntColumn(ThirdDayIdColumn) { IsNullable = true },
                    new IntColumn(FourthDayIdColumn) { IsNullable = true },
                    new IntColumn(FifthDayIdColumn) { IsNullable = true },
                    new IntColumn(SixthDayIdColumn) { IsNullable = true },
                    new IntColumn(SeventhDayIdColumn) { IsNullable = true }
                };

                return new Schema(Table, columnList, new List<ReferenceLink>()
                {
                    new ReferenceLink(GroupIdColumn, Group.Table, Group.IdColumn),
                    new ReferenceLink(WeekTypeIdColumn, WeekType.Table, WeekType.IdColumn),
                    new ReferenceLink(FirstDayIdColumn, DaySchedule.Table, DaySchedule.IdColumn),
                    new ReferenceLink(SecondDayIdColumn, DaySchedule.Table, DaySchedule.IdColumn),
                    new ReferenceLink(ThirdDayIdColumn, DaySchedule.Table, DaySchedule.IdColumn),
                    new ReferenceLink(FourthDayIdColumn, DaySchedule.Table, DaySchedule.IdColumn),
                    new ReferenceLink(FifthDayIdColumn, DaySchedule.Table, DaySchedule.IdColumn),
                    new ReferenceLink(SixthDayIdColumn, DaySchedule.Table, DaySchedule.IdColumn),
                    new ReferenceLink(SeventhDayIdColumn, DaySchedule.Table, DaySchedule.IdColumn)
                });
            }
        }

        /// <summary>
        /// Инициализировать учебную неделю из схемы с данными.
        /// </summary>
        /// <param name="data">Схема, заполненная данными.</param>
        /// <param name="group">Группа.</param>
        /// <param name="weekType">Тип недели.</param>
        /// <returns>Учебная неделя для группы.</returns>
        internal static GroupWeekSchedule FromData(Schema data, Group group, WeekType weekType)
        {
            if (data == null || !data.IsSameAsSample(Schema))
            {
                throw new ArgumentException("Переданная схема не соответствует схеме для сущности.");
            }

            if (data.GetIntColumnData(GroupIdColumn) != group.Id)
            {
                throw new ArgumentException("Переданные схема с данными и сущность не соответствуют друг другу.");
            }

            if (data.GetIntColumnData(WeekTypeIdColumn) != weekType.Id)
            {
                throw new ArgumentException("Переданные схема с данными и сущность не соответствуют друг другу.");
            }

            return new GroupWeekSchedule(
                data.GetIntColumnData(IdColumn),
                group,
                weekType);
        }

        /// <summary>
        /// Инициализировать учебную неделю из схемы с данными.
        /// </summary>
        /// <param name="data">Схема, заполненная данными.</param>
        /// <param name="group">Группа.</param>
        /// <param name="weekType">Тип недели.</param>
        /// <param name="dayList">Список контейнеров учебных дней.</param>
        /// <returns>Учебная неделя для группы.</returns>
        internal static GroupWeekSchedule FromData(Schema data, Group group, WeekType weekType, List<DayScheduleEntry> dayList)
        {
            if (data == null || !data.IsSameAsSample(Schema))
            {
                throw new ArgumentException("Переданная схема не соответствует схеме для сущности.");
            }

            if (data.GetIntColumnData(GroupIdColumn) != group.Id)
            {
                throw new ArgumentException("Переданные схема с данными и сущность не соответствуют друг другу.");
            }

            if (data.GetIntColumnData(WeekTypeIdColumn) != weekType.Id)
            {
                throw new ArgumentException("Переданные схема с данными и сущность не соответствуют друг другу.");
            }

            return new GroupWeekSchedule(
                data.GetIntColumnData(IdColumn),
                group,
                weekType,
                dayList);
        }

        /// <summary>
        /// Получить схему таблицы с данными.
        /// </summary>
        /// <returns>Схема, заполненная данными.</returns>
        internal Schema ToData()
        {
            Schema data = Schema;

            data.SetColumnData(IdColumn, Id);
            data.SetColumnData(GroupIdColumn, Group.Id);
            data.SetColumnData(WeekTypeIdColumn, WeekType.Id);

            // "Расчехляем" контейнеры и вставляем учебные дни
            // в нужные столбцы.
            foreach (DayScheduleEntry entry in DayList)
            {
                if (entry.DaySchedule == null)
                {
                    continue;
                }

                data.SetColumnData(GetIdColumnName(entry), entry.DaySchedule.Id);
            }

            return data;
        }

        #endregion

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
        /// <param name="id">Идентификатор.</param>
        /// <param name="group">Группа.</param>
        /// <param name="weekType">Тип недели.</param>
        public GroupWeekSchedule(int id, Group group, WeekType weekType) : base(group, weekType)
        {
            Schemable = new DataEntity(id);
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="group">Группа.</param>
        /// <param name="weekType">Тип недели.</param>
        /// <param name="dayList">Список контейнеров учебных дней.</param>
        /// <exception cref="ArgumentException"></exception>
        public GroupWeekSchedule(int id, Group group, WeekType weekType, List<DayScheduleEntry> dayList) : base(group, weekType, dayList)
        {
            Schemable = new DataEntity(id);
        }

        /// <summary>
        /// Доступ к группе.
        /// </summary>
        public Group Group
        {
            get => Target as Group;
            private set => Target = value;
        }

        #endregion
    }
}
