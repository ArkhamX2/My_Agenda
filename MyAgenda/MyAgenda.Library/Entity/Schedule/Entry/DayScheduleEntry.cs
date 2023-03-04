using System;

namespace MyAgenda.Library.Entity.Schedule.Entry
{
    /// <summary>
    /// Контейнер учебного дня с фиксированной позицией.
    /// Представляет собой связь индекса, учебного дня и дополнительной информации
    /// для размещения в списке.
    /// </summary>
    public class DayScheduleEntry : EntityEntry
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
            private set => Entity = value;
        }

        /// <summary>
        /// Доступ к дню недели.
        /// </summary>
        public string WeekDay => GetWeekDay(Position);
    }
}
