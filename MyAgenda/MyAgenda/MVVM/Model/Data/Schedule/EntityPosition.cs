using System;

namespace MyAgenda.MVVM.Model.Data.Schedule
{
    /// <summary>
    /// Тип позиции.
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
    /// Позиция существа в списке.
    /// </summary>
    internal abstract class EntityPosition
    {
        /// <summary>
        /// Получить индекс через тип позиции.
        /// </summary>
        /// <param name="type">Тип позиции.</param>
        /// <returns>Индекс.</returns>
        public static int GetIndexByPosition(PositionType type)
        {
            return (int)type;
        }

        /// <summary>
        /// Получить тип позиции через индекс.
        /// </summary>
        /// <param name="index">Индекс.</param>
        /// <returns>Тип позиции.</returns>
        public static PositionType GetPositionByIndex(int index)
        {
            return (PositionType)index;
        }

        /// <summary>
        /// Тип.
        /// </summary>
        private PositionType _type;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="type">Тип позиции.</param>
        public EntityPosition(PositionType type)
        {
            Type = type;
        }

        /// <summary>
        /// Доступ к типу.
        /// </summary>
        public PositionType Type
        {
            get => _type;
            private set => _type = value;
        }

        /// <summary>
        /// Доступ к индексу.
        /// </summary>
        public int Index => GetIndexByPosition(Type);
    }

    /// <summary>
    /// Позиция занятия в списке и связная с ней информация.
    /// </summary>
    internal class SubjectPosition : EntityPosition
    {
        /// <summary>
        /// Получить время начала занятия через тип позиции.
        /// </summary>
        /// <param name="type">Тип позиции занятия.</param>
        /// <returns>Время начала занятия.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static string GetStartTimeByPosition(PositionType type)
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
        public static string GetEndTimeByPosition(PositionType type)
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
        /// Конструктор.
        /// </summary>
        /// <param name="type">Тип позиции.</param>
        public SubjectPosition(PositionType type) : base(type)
        {
            // PASS.
        }

        /// <summary>
        /// Доступ к времени начала занятия.
        /// </summary>
        public string StartTime => GetStartTimeByPosition(Type);

        /// <summary>
        /// Доступ к времени окончания занятия.
        /// </summary>
        public string EndTime => GetEndTimeByPosition(Type);
    }

    /// <summary>
    /// Позиция чебного дня в списке и связная с ней информация.
    /// </summary>
    internal class DayPosition : EntityPosition
    {
        /// <summary>
        /// Получить день недели через тип позиции.
        /// </summary>
        /// <param name="type">Тип позиции учебного дня.</param>
        /// <returns>День недели.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static string GetWeekDayByPosition(PositionType type)
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
        /// Конструктор.
        /// </summary>
        /// <param name="type">Тип позиции.</param>
        public DayPosition(PositionType type) : base(type)
        {
            // PASS.
        }

        /// <summary>
        /// Доступ к дню недели.
        /// </summary>
        public string WeekDay => GetWeekDayByPosition(Type);
    }
}
