using System;
using MyAgenda.Library.Model.Schedule.Day;

namespace MyAgenda.Library.Model.Schedule.Entry
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
        /// <exception cref="ArgumentOutOfRangeException"></exception>
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
                default: throw new ArgumentOutOfRangeException(nameof(position), position, null);
            }
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
            // PASS.
        }

        /// <summary>
        /// Доступ к учебному дню.
        /// </summary>
        public DaySchedule DaySchedule => Entity as DaySchedule;

        /// <summary>
        /// Доступ к дню недели.
        /// </summary>
        public string WeekDay => GetWeekDay(Position);

        /// <summary>
        /// Проверить наличие учебного дня.
        /// </summary>
        /// <returns>Статус проверки.</returns>
        public bool HasDaySchedule()
        {
            return HasEntity();
        }
    }
}
