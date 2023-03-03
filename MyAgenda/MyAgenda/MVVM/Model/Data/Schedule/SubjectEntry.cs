using System;

namespace MyAgenda.MVVM.Model.Data.Schedule
{
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
}
