using System;
using System.Collections.Generic;

namespace MyAgenda.MVVM.Model
{
    /// <summary>
    /// Контейнер для транспортировки данных.
    /// TODO: Удалить DataContainer.
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
    internal class DataEntity : Entity, ISchemable
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

        /*           _                          _     _
         *  ___  ___| |__   ___ _ __ ___   __ _| |__ | | ___
         * / __|/ __| '_ \ / _ \ '_ ` _ \ / _` | '_ \| |/ _ \
         * \__ \ (__| | | |  __/ | | | | | (_| | |_) | |  __/
         * |___/\___|_| |_|\___|_| |_| |_|\__,_|_.__/|_|\___|
         *
         */
        #region ISchemable

        /// <summary>
        /// Доступ к схеме данных.
        /// </summary>
        public static Schema Schema => new Schema(Table, new List<Column>()
        {
            new IntColumn(IdColumn) { IsPrimaryKey = true, IsAutoIncrementable = true }
        });

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

        /// <summary>
        /// Инициализировать сущность из схемы с данными.
        /// </summary>
        /// <param name="data">Схема, заполненная данными.</param>
        /// <returns>Сущность.</returns>
        public static ISchemable FromData(Schema data)
        {
            if (data == null || !data.IsSameAsObject(Schema))
            {
                throw new ArgumentException("Схема не соответствует схеме по-умолчанию.");
            }

            return new DataEntity(data.GetIntColumnData(IdColumn));
        }

        /// <summary>
        /// Получить схему таблицы с данными.
        /// </summary>
        /// <returns>Схема, заполненная данными.</returns>
        public Schema ToData()
        {
            Schema data = Schema;

            data.SetColumnData(IdColumn, Id);

            return data;
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

        #endregion
    }
}
