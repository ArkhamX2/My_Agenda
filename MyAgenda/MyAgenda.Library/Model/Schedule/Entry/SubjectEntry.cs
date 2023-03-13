using MyAgenda.Library.Model.Base;
using System;

namespace MyAgenda.Library.Model.Schedule.Entry
{
    /// <summary>
    /// Контейнер занятия с фиксированной позицией.
    /// Представляет собой связь индекса, занятия и дополнительной информации
    /// для размещения в списке.
    /// </summary>
    public class SubjectEntry : EntityEntry
    {
        /// <summary>
        /// Получить время начала занятия через позицию.
        /// </summary>
        /// <param name="position">Позиция занятия.</param>
        /// <returns>Время начала занятия.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
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
                default: throw new ArgumentOutOfRangeException(nameof(position), position, null);
            }
        }

        /// <summary>
        /// Получить время окончания занятия через позицию.
        /// </summary>
        /// <param name="position">Позиция занятия.</param>
        /// <returns>Время окончания занятия.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
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
                default: throw new ArgumentOutOfRangeException(nameof(position), position, null);
            }
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
        public Subject Subject => Entity as Subject;

        /// <summary>
        /// Доступ к времени начала занятия.
        /// </summary>
        public string StartTime => GetStartTime(Position);

        /// <summary>
        /// Доступ к времени окончания занятия.
        /// </summary>
        public string EndTime => GetEndTime(Position);

        /// <summary>
        /// Проверить наличие занятия.
        /// </summary>
        /// <returns>Статус проверки.</returns>
        public bool HasSubject()
        {
            return HasEntity();
        }
    }
}
