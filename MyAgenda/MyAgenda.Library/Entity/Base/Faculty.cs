using System;
using System.Collections.Generic;
using MyAgenda.Library.Data;
using MyAgenda.Library.Data.Column;

namespace MyAgenda.Library.Entity.Base
{
    /// <summary>
    /// Факультет.
    /// </summary>
    public class Faculty : DataEntity
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
        internal const string Table = "faculty";

        /// <summary>
        /// Название столбца с названием.
        /// </summary>
        internal const string NameColumn = "name";

        /// <summary>
        /// Минимальная длина названия.
        /// </summary>
        public const int NameLengthMin = 1;

        /// <summary>
        /// Максимальная длина названия.
        /// </summary>
        public const int NameLengthMax = 255;

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
        internal static Schema Schema => new Schema(Table, new List<DataColumn>
        {
            new IntColumn(IdColumn) { IsPrimaryKey = true, IsAutoIncrementable = true },
            new StringColumn(NameColumn, NameLengthMax)
        });

        /// <summary>
        /// Инициализировать факультет из схемы с данными.
        /// </summary>
        /// <param name="data">Схема, заполненная данными.</param>
        /// <returns>Факультет.</returns>
        internal static Faculty FromData(Schema data)
        {
            if (data == null || !data.IsSameAsSample(Schema))
            {
                throw new ArgumentException("Переданная схема не соответствует схеме для сущности.");
            }

            return new Faculty(
                data.GetIntColumnData(IdColumn),
                data.GetStringColumnData(NameColumn));
        }

        /// <summary>
        /// Получить схему таблицы с данными.
        /// </summary>
        /// <returns>Схема, заполненная данными.</returns>
        internal override Schema ToData()
        {
            Schema data = Schema;

            data.SetColumnData(IdColumn, Id);
            data.SetColumnData(NameColumn, Name);

            return data;
        }

        #endregion

        /*   __                  _ _
         *  / _| __ _  ___ _   _| | |_ _   _
         * | |_ / _` |/ __| | | | | __| | | |
         * |  _| (_| | (__| |_| | | |_| |_| |
         * |_|  \__,_|\___|\__,_|_|\__|\__, |
         *                             |___/
         */
        #region Faculty

        /// <summary>
        /// Название.
        /// </summary>
        private string _name;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="name">Название.</param>
        public Faculty(int id, string name) : base(id)
        {
            Name = name;
        }

        /// <summary>
        /// Доступ к названию.
        /// </summary>
        public string Name
        {
            get => _name;
            private set => _name = ValidateStringData(value, NameLengthMin, NameLengthMax);
        }

        #endregion
    }
}
