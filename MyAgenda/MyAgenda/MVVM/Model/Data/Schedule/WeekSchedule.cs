using System;
using System.Collections.Generic;

namespace MyAgenda.MVVM.Model.Data.Schedule
{
    /// <summary>
    /// Учебная неделя.
    /// </summary>
    internal class WeekSchedule : Entity, IIndirectlySchemable
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
        public const string Table = "week_schedule";

        /// <summary>
        /// Название столбца с идентификатором.
        /// </summary>
        public const string IdColumn = DataEntity.IdColumn;

        /// <summary>
        /// Название столбца с идентификатором группы.
        /// </summary>
        public const string GroupIdColumn = "group_id";

        /// <summary>
        /// Название столбца с идентификатором типа недели.
        /// </summary>
        public const string WeekTypeIdColumn = "week_type_id";

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
        public const int DayCount = DayScheduleEntry.PositionTypeCount;

        #endregion

        /// <summary>
        /// Получить тип позиции учебного дня через название столбца с идентификатором.
        /// </summary>
        /// <param name="columnName">Название столбца с идентификатором учебного дня.</param>
        /// <returns>Тип позиции учебного дня.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static PositionType GetPositionType(string columnName)
        {
            switch (columnName)
            {
                case FirstDayIdColumn: return PositionType.First;
                case SecondDayIdColumn: return PositionType.Second;
                case ThirdDayIdColumn: return PositionType.Third;
                case FourthDayIdColumn: return PositionType.Fourth;
                case FifthDayIdColumn: return PositionType.Fifth;
                case SixthDayIdColumn: return PositionType.Sixth;
                case SeventhDayIdColumn: return PositionType.Seventh;
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
        /// Получить название столбца с идентификатором учебного дня через тип позиции.
        /// </summary>
        /// <param name="type">Тип позиции учебного дня.</param>
        /// <returns>Название столбца с идентификатором учебного дня.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static string GetIdColumnName(PositionType type)
        {
            switch (type)
            {
                case PositionType.First: return FirstDayIdColumn;
                case PositionType.Second: return SecondDayIdColumn;
                case PositionType.Third: return ThirdDayIdColumn;
                case PositionType.Fourth: return FourthDayIdColumn;
                case PositionType.Fifth: return FifthDayIdColumn;
                case PositionType.Sixth: return SixthDayIdColumn;
                case PositionType.Seventh: return SeventhDayIdColumn;
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
                List<Column> columnList = new List<Column>
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
        /// Инициализировать сущность из схемы с данными.
        /// </summary>
        /// <param name="data">Схема, заполненная данными.</param>
        /// <param name="group">Зависимая группа.</param>
        /// <param name="weekType">Зависимый тип недели.</param>
        /// <returns>Сущность.</returns>
        public static WeekSchedule FromData(Schema data, Group group, WeekType weekType)
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

            return new WeekSchedule(
                data.GetIntColumnData(IdColumn),
                group,
                weekType);
        }

        /// <summary>
        /// Инициализировать сущность из схемы с данными.
        /// </summary>
        /// <param name="data">Схема, заполненная данными.</param>
        /// <param name="group">Зависимая группа.</param>
        /// <param name="weekType">Зависимый тип недели.</param>
        /// <param name="dayList">Список зависимых учебных дней.</param>
        /// <returns>Сущность.</returns>
        public static WeekSchedule FromData(Schema data, Group group, WeekType weekType, List<DayScheduleEntry> dayList)
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

            return new WeekSchedule(
                data.GetIntColumnData(IdColumn),
                group,
                weekType,
                dayList);
        }

        /// <summary>
        /// Получить схему таблицы с данными.
        /// </summary>
        /// <returns>Схема, заполненная данными.</returns>
        public Schema ToData()
        {
            Schema data = Schema;

            data.SetColumnData(IdColumn, Id);
            data.SetColumnData(GroupIdColumn, Group.Id);
            data.SetColumnData(WeekTypeIdColumn, WeekType.Id);

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
        /// Группа.
        /// </summary>
        private Group _group;

        /// <summary>
        /// Тип учебной недели.
        /// </summary>
        private WeekType _weekType;

        /// <summary>
        /// Список учебных дней.
        /// Каждый учебный день хранится в контейнере с закрепленной за ним позицией
        /// в списке и дополнительной связанной с ней информацией.
        /// </summary>
        private readonly List<DayScheduleEntry> _dayList = new List<DayScheduleEntry>();

        /// <summary>
        /// Конструктор учебной недели без учебных дней.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="group">Группа.</param>
        /// <param name="weekType">Тип недели.</param>
        public WeekSchedule(int id, Group group, WeekType weekType)
        {
            Schemable = new DataEntity(id);

            Group = group;
            WeekType = weekType;

            // Заполнение списка учебных дней пустыми контейнерами.
            foreach (PositionType type in DayScheduleEntry.GetPositionTypeList())
            {
                DayList[DayScheduleEntry.GetIndex(type)] = new DayScheduleEntry(type);
            }
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="group">Группа.</param>
        /// <param name="weekType">Тип недели.</param>
        /// <param name="dayList">Список учебных дней.</param>
        /// <exception cref="ArgumentException"></exception>
        public WeekSchedule(int id, Group group, WeekType weekType, List<DayScheduleEntry> dayList) : this(id, group, weekType)
        {
            // Замена пустых контейнеров.
            foreach (DayScheduleEntry entry in dayList)
            {
                DayList[entry.Index] = entry;
            }
        }

        /// <summary>
        /// Доступ к группе.
        /// </summary>
        public Group Group
        {
            get => _group;
            set => _group = value;
        }

        /// <summary>
        /// Доступ к типу недели.
        /// </summary>
        public WeekType WeekType
        {
            get => _weekType;
            set => _weekType = value;
        }

        /// <summary>
        /// Доступ к списку учебных дней.
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
