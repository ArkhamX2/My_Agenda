using System;
using System.Collections.Generic;

namespace MyAgenda.MVVM.Model.Data.Schedule
{
    /// <summary>
    /// Тип позиции. Представление местоположения сущности в списке.
    /// </summary>
    internal enum PositionType
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
        /// Получить индекс через тип позиции.
        /// </summary>
        /// <param name="type">Тип позиции.</param>
        /// <returns>Индекс.</returns>
        public static int GetIndex(PositionType type)
        {
            return (int)type;
        }

        /// <summary>
        /// Получить тип позиции через индекс.
        /// </summary>
        /// <param name="index">Индекс.</param>
        /// <returns>Тип позиции.</returns>
        public static PositionType GetPositionType(int index)
        {
            return (PositionType)index;
        }

        /// <summary>
        /// Получить список типов позиции.
        /// </summary>
        /// <returns>Список типов позиции.</returns>
        public static List<PositionType> GetPositionTypeList()
        {
            return new List<PositionType>()
            {
                PositionType.First,
                PositionType.Second,
                PositionType.Third,
                PositionType.Fourth,
                PositionType.Fifth,
                PositionType.Sixth,
                PositionType.Seventh
            };
        }

        /// <summary>
        /// Тип позиции.
        /// </summary>
        private PositionType _type;

        /// <summary>
        /// Сущность.
        /// </summary>
        private Entity _entity = null;

        /// <summary>
        /// Конструктор пустого контейнера.
        /// </summary>
        /// <param name="type">Тип позиции.</param>
        public EntityEntry(PositionType type)
        {
            PositionType = type;
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="type">Тип позиции.</param>
        /// <param name="entity">Сущность.</param>
        protected EntityEntry(PositionType type, Entity entity) : this(type)
        {
            Entity = entity;
        }

        /// <summary>
        /// Доступ к типу позиции.
        /// </summary>
        public PositionType PositionType
        {
            get => _type;
            private set => _type = value;
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
        public int Index => GetIndex(PositionType);
    }

    /// <summary>
    /// Контейнер занятия с фиксированной позицией.
    /// Представляет собой связь индекса, занятия и дополнительной информации
    /// для размещения в списке.
    /// </summary>
    internal class SubjectEntry : EntityEntry
    {
        /// <summary>
        /// Получить время начала занятия через тип позиции.
        /// </summary>
        /// <param name="type">Тип позиции занятия.</param>
        /// <returns>Время начала занятия.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static string GetStartTime(PositionType type)
        {
            switch (type)
            {
                case PositionType.First: return "8:30";
                case PositionType.Second: return "10:15";
                case PositionType.Third: return "12:15";
                case PositionType.Fourth: return "14:00";
                case PositionType.Fifth: return "15:45";
                case PositionType.Sixth: return "17:30";
                case PositionType.Seventh: return "19:15";
            }

            throw new ArgumentException("Внутренняя ошибка.");
        }

        /// <summary>
        /// Получить время окончания занятия через тип позиции.
        /// </summary>
        /// <param name="type">Тип позиции занятия.</param>
        /// <returns>Время окончания занятия.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static string GetEndTime(PositionType type)
        {
            switch (type)
            {
                case PositionType.First: return "10:05";
                case PositionType.Second: return "11:50";
                case PositionType.Third: return "13:50";
                case PositionType.Fourth: return "15:35";
                case PositionType.Fifth: return "17:20";
                case PositionType.Sixth: return "19:05";
                case PositionType.Seventh: return "20:50";
            }

            throw new ArgumentException("Внутренняя ошибка.");
        }

        /// <summary>
        /// Конструктор пустого контейнера.
        /// </summary>
        /// <param name="type">Тип позиции.</param>
        public SubjectEntry(PositionType type) : base(type)
        {
            // PASS.
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="type">Тип позиции.</param>
        /// <param name="subject">Занятие.</param>
        public SubjectEntry(PositionType type, Subject subject) : base(type, subject)
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
        public string StartTime => GetStartTime(PositionType);

        /// <summary>
        /// Доступ к времени окончания занятия.
        /// </summary>
        public string EndTime => GetEndTime(PositionType);
    }

    /// <summary>
    /// Контейнер учебного дня с фиксированной позицией.
    /// Представляет собой связь индекса, учебного дня и дополнительной информации
    /// для размещения в списке.
    /// </summary>
    internal class DayScheduleEntry : EntityEntry
    {
        /// <summary>
        /// Получить день недели через тип позиции.
        /// </summary>
        /// <param name="type">Тип позиции учебного дня.</param>
        /// <returns>День недели.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static string GetWeekDay(PositionType type)
        {
            switch (type)
            {
                case PositionType.First: return "понедельник";
                case PositionType.Second: return "вторник";
                case PositionType.Third: return "среда";
                case PositionType.Fourth: return "четверг";
                case PositionType.Fifth: return "пятница";
                case PositionType.Sixth: return "суббота";
                case PositionType.Seventh: return "воскресенье";
            }

            throw new ArgumentException("Внутренняя ошибка.");
        }

        /// <summary>
        /// Конструктор пустого контейнера.
        /// </summary>
        /// <param name="type">Тип позиции.</param>
        public DayScheduleEntry(PositionType type) : base(type)
        {
            // PASS.
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="type">Тип позиции.</param>
        /// <param name="day">Учебный день.</param>
        public DayScheduleEntry(PositionType type, DaySchedule day) : base(type, day)
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
        public string WeekDay => GetWeekDay(PositionType);
    }
}
