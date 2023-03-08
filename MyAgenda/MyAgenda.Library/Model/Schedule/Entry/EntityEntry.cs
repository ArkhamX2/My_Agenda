using System.Collections.Generic;

namespace MyAgenda.Library.Model.Schedule.Entry
{
    /// <summary>
    /// Позиция контейнера.
    /// Фиксированное представление местоположения в списке.
    /// </summary>
    public enum EntryPosition
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
    public abstract class EntityEntry
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
        private DataEntity _entity = null;

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
        protected EntityEntry(EntryPosition position, DataEntity entity) : this(position)
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
        protected DataEntity Entity
        {
            get => _entity;
            private set => _entity = value;
        }

        /// <summary>
        /// Доступ к индексу.
        /// </summary>
        public int Index => GetIndex(Position);
    }
}
