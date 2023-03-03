using System;
using System.Collections.Generic;

namespace MyAgenda.MVVM.Model.Data.Schedule
{
    /// <summary>
    /// Позиция контейнера.
    /// Фиксированное представление местоположения в списке.
    /// </summary>
    internal enum EntryPosition
    {
        First,
        Second,
        Third,
        Fourth,
        Fifth,
        Sixth,
        Seventh
    }

    /// <summary>
    /// Контейнер сущности с фиксированной позицией.
    /// Представляет собой связь индекса (ключа) и сущности (значения)
    /// для размещения в списке.
    /// </summary>
    internal abstract class EntityEntry
    {
        /// <summary>
        /// Количество типов позиций.
        /// </summary>
        public const int PositionTypeCount = 7;

        /// <summary>
        /// Получить индекс через позицию.
        /// </summary>
        /// <param name="position">Позиция.</param>
        /// <returns>Индекс.</returns>
        public static int GetIndex(EntryPosition position)
        {
            return (int)position;
        }

        /// <summary>
        /// Получить позицию через индекс.
        /// </summary>
        /// <param name="index">Индекс.</param>
        /// <returns>Позиция.</returns>
        public static EntryPosition GetPositionType(int index)
        {
            return (EntryPosition)index;
        }

        /// <summary>
        /// Получить список позиций.
        /// </summary>
        /// <returns>Список позиций.</returns>
        public static List<EntryPosition> GetPositionTypeList()
        {
            return new List<EntryPosition>()
            {
                EntryPosition.First,
                EntryPosition.Second,
                EntryPosition.Third,
                EntryPosition.Fourth,
                EntryPosition.Fifth,
                EntryPosition.Sixth,
                EntryPosition.Seventh
            };
        }

        /// <summary>
        /// Позиция.
        /// </summary>
        private EntryPosition _position;

        /// <summary>
        /// Сущность.
        /// Может быть не задана.
        /// </summary>
        private Entity _entity = null;

        /// <summary>
        /// Конструктор пустого контейнера.
        /// </summary>
        /// <param name="position">Позиция.</param>
        public EntityEntry(EntryPosition position)
        {
            Position = position;
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="position">Позиция.</param>
        /// <param name="entity">Сущность.</param>
        protected EntityEntry(EntryPosition position, Entity entity) : this(position)
        {
            Entity = entity;
        }

        /// <summary>
        /// Доступ к позиции.
        /// </summary>
        public EntryPosition Position
        {
            get => _position;
            private set => _position = value;
        }

        /// <summary>
        /// Доступ к сущности.
        /// </summary>
        protected Entity Entity
        {
            get => _entity;
            set => _entity = value;
        }

        /// <summary>
        /// Доступ к индексу.
        /// </summary>
        public int Index => GetIndex(Position);
    }

    /// <summary>
    /// Контейнер занятия с фиксированной позицией.
    /// Представляет собой связь индекса, занятия и дополнительной информации
    /// для размещения в списке.
    /// </summary>
    internal class SubjectEntry : EntityEntry
    {
        /// <summary>
        /// Получить время начала занятия через позицию.
        /// </summary>
        /// <param name="position">Позиция занятия.</param>
        /// <returns>Время начала занятия.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static string GetStartTime(EntryPosition position)
        {
            switch (position)
            {
                case EntryPosition.First: return "8:30";
                case EntryPosition.Second: return "10:15";
                case EntryPosition.Third: return "12:15";
                case EntryPosition.Fourth: return "14:00";
                case EntryPosition.Fifth: return "15:45";
                case EntryPosition.Sixth: return "17:30";
                case EntryPosition.Seventh: return "19:15";
            }

            throw new ArgumentException("Внутренняя ошибка.");
        }

        /// <summary>
        /// Получить время окончания занятия через позицию.
        /// </summary>
        /// <param name="position">Позиция занятия.</param>
        /// <returns>Время окончания занятия.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static string GetEndTime(EntryPosition position)
        {
            switch (position)
            {
                case EntryPosition.First: return "10:05";
                case EntryPosition.Second: return "11:50";
                case EntryPosition.Third: return "13:50";
                case EntryPosition.Fourth: return "15:35";
                case EntryPosition.Fifth: return "17:20";
                case EntryPosition.Sixth: return "19:05";
                case EntryPosition.Seventh: return "20:50";
            }

            throw new ArgumentException("Внутренняя ошибка.");
        }

        /// <summary>
        /// Конструктор пустого контейнера.
        /// </summary>
        /// <param name="position">Позиция.</param>
        public SubjectEntry(EntryPosition position) : base(position)
        {
            // PASS.
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="position">Позиция.</param>
        /// <param name="subject">Занятие.</param>
        public SubjectEntry(EntryPosition position, Subject subject) : base(position, subject)
        {
            // PASS
        }

        /// <summary>
        /// Доступ к занятию.
        /// </summary>
        public Subject Subject
        {
            get => Entity as Subject;
            set => Entity = value;
        }

        /// <summary>
        /// Доступ к времени начала занятия.
        /// </summary>
        public string StartTime => GetStartTime(Position);

        /// <summary>
        /// Доступ к времени окончания занятия.
        /// </summary>
        public string EndTime => GetEndTime(Position);
    }

    /// <summary>
    /// Контейнер учебного дня с фиксированной позицией.
    /// Представляет собой связь индекса, учебного дня и дополнительной информации
    /// для размещения в списке.
    /// </summary>
    internal class DayScheduleEntry : EntityEntry
    {
        /// <summary>
        /// Получить день недели через позицию.
        /// </summary>
        /// <param name="position">Позиция учебного дня.</param>
        /// <returns>День недели.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static string GetWeekDay(EntryPosition position)
        {
            switch (position)
            {
                case EntryPosition.First: return "понедельник";
                case EntryPosition.Second: return "вторник";
                case EntryPosition.Third: return "среда";
                case EntryPosition.Fourth: return "четверг";
                case EntryPosition.Fifth: return "пятница";
                case EntryPosition.Sixth: return "суббота";
                case EntryPosition.Seventh: return "воскресенье";
            }

            throw new ArgumentException("Внутренняя ошибка.");
        }

        /// <summary>
        /// Конструктор пустого контейнера.
        /// </summary>
        /// <param name="position">Позиция.</param>
        public DayScheduleEntry(EntryPosition position) : base(position)
        {
            // PASS.
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="position">Позиция.</param>
        /// <param name="day">Учебный день.</param>
        public DayScheduleEntry(EntryPosition position, DaySchedule day) : base(position, day)
        {
            // PASS
        }

        /// <summary>
        /// Доступ к учебному дню.
        /// </summary>
        public DaySchedule DaySchedule
        {
            get => Entity as DaySchedule;
            set => Entity = value;
        }

        /// <summary>
        /// Доступ к дню недели.
        /// </summary>
        public string WeekDay => GetWeekDay(Position);
    }
}
