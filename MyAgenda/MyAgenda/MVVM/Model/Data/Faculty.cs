using System;
using System.Collections.Generic;

namespace MyAgenda.MVVM.Model.Data
{
    /// <summary>
    /// Факультет.
    /// </summary>
    internal class Faculty : IIndirectlySchemable
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
        public const string Table = "faculty";

        /// <summary>
        /// Название столбца с идентификатором.
        /// </summary>
        public const string IdColumn = DataEntity.IdColumn;

        /// <summary>
        /// Название столбца с названием.
        /// </summary>
        public const string NameColumn = "name";

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
        /// Доступ к косвенному родителю со схемой таблицы.
        /// </summary>
        public ISchemable Schemable
        {
            get;
            set;
        }

        /// <summary>
        /// Доступ к идентификатору.
        /// </summary>
        public int Id
        {
            get => Schemable.Id;
            set => Schemable.Id = value;
        }

        /// <summary>
        /// Доступ к схеме данных.
        /// </summary>
        public static Schema Schema
        {
            get
            {
                List<Column> columnList = DataEntity.Schema.ColumnList;

                columnList.Add(new StringColumn(NameColumn, NameLengthMax));

                return new Schema(Table, columnList);
            }
        }

        /// <summary>
        /// Инициализировать сущность из схемы с данными.
        /// </summary>
        /// <param name="data">Схема, заполненная данными.</param>
        /// <returns>Сущность.</returns>
        public static Faculty FromData(Schema data)
        {
            // Базовый уровень валидации.
            DataEntity.FromData(data);

            return new Faculty(
                data.GetIntColumnData(IdColumn),
                data.GetStringColumnData(NameColumn));
        }

        /// <summary>
        /// Получить схему таблицы с данными.
        /// </summary>
        /// <returns>Схема, заполненная данными.</returns>
        public Schema ToData()
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
        public Faculty(int id, string name)
        {
            Schemable = new DataEntity(id);

            Name = name;
        }

        /// <summary>
        /// Доступ к названию.
        /// </summary>
        public string Name
        {
            get => _name;
            set
            {
                value = value.Trim().ToLower();

                if (value.Length < NameLengthMin || value.Length > NameLengthMax)
                {
                    throw new ArgumentException("Длина названия не может выходить за допустимые пределы.");
                }

                _name = value;
            }
        }

        #endregion
    }
}
