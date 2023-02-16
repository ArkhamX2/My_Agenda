using System;

namespace MyAgenda.MVVM.Model
{
    /// <summary>
    /// Контейнер для транспортировки данных.
    /// </summary>
    internal class DataContainer
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int Id { get; set; }
    }

    /// <summary>
    /// Сущность данных.
    /// TODO: Пересмотреть.
    /// </summary>
    internal abstract class DataEntity : Entity
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
        public const string Table = "";

        /// <summary>
        /// Название столбца с идентификатором.
        /// </summary>
        public const string IdColumn = "id";

        /// <summary>
        /// Минимальный идентификатор.
        /// </summary>
        public const int IdMin = 0;

        /// <summary>
        /// Незаданный идентификатор.
        /// </summary>
        public const int IdUndefined = -1;

        #endregion

        /*      _       _                     _   _ _
         *   __| | __ _| |_ __ _    ___ _ __ | |_(_) |_ _   _
         *  / _` |/ _` | __/ _` |  / _ \ '_ \| __| | __| | | |
         * | (_| | (_| | || (_| | |  __/ | | | |_| | |_| |_| |
         *  \__,_|\__,_|\__\__,_|  \___|_| |_|\__|_|\__|\__, |
         *                                              |___/
         */
        #region DataEntity

        /// <summary>
        /// Идентификатор
        /// </summary>
        private int _id;

        /// <summary>
        /// Пустой конструктор.
        /// </summary>
        public DataEntity(int id)
        {
            Id = id;
        }

        /// <summary>
        /// Доступ к идентификатору.
        /// </summary>
        public int Id
        {
            get => _id;
            set
            {
                if (value < IdMin)
                {
                    throw new ArgumentException("Идентификатор не может быть отрицательным.");
                }

                _id = value;
            }
        }

        #endregion
    }
}
