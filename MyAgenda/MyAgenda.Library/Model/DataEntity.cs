using System;
using MyAgenda.Library.Data;

namespace MyAgenda.Library.Model
{
    /// <summary>
    /// Сущность данных.
    /// Позволяет наследникам иметь схему таблицы и, соответственно,
    /// иметь возможность конвертации данных объекта в схему
    /// для сохранения в базе данных.
    /// </summary>
    public class DataEntity : Entity, ISchemable
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
        /// Название столбца с идентификатором.
        /// </summary>
        internal const string IdColumn = "id";

        /// <summary>
        /// Минимальный идентификатор.
        /// </summary>
        public const int IdMin = 1;

        /// <summary>
        /// Незаданный идентификатор.
        /// </summary>
        internal const int IdUndefined = 0;

        #endregion

        /*           _                          _     _
         *  ___  ___| |__   ___ _ __ ___   __ _| |__ | | ___
         * / __|/ __| '_ \ / _ \ '_ ` _ \ / _` | '_ \| |/ _ \
         * \__ \ (__| | | |  __/ | | | | | (_| | |_) | |  __/
         * |___/\___|_| |_|\___|_| |_| |_|\__,_|_.__/|_|\___|
         *
         */
        #region ISchemable

        /// <summary>
        /// Доступ к идентификатору.
        /// </summary>
        public int Id
        {
            get => _id;
            protected set
            {
                if (value < IdMin)
                {
                    throw new ArgumentException("Идентификатор не может быть меньше минимального.");
                }

                _id = value;
            }
        }

        /// <summary>
        /// Получить схему таблицы с данными.
        /// </summary>
        /// <returns>Схема, заполненная данными.</returns>
        /// <exception cref="NotSupportedException"></exception>
        internal virtual Schema ToData()
        {
            throw new NotSupportedException("Получение схемы для базовой сущности данных невозможно.");
        }

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
        /// Идентификатор.
        /// </summary>
        private int _id;

        /// <summary>
        /// Конструктор.
        /// </summary>
        internal DataEntity(int id)
        {
            Id = id;
        }

        #endregion
    }
}
