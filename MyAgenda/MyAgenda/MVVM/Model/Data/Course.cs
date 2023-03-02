using System;
using System.Collections.Generic;

namespace MyAgenda.MVVM.Model.Data
{
    /// <summary>
    /// Курс.
    /// </summary>
    internal class Course : DataEntity
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
        public const string Table = "course";

        /// <summary>
        /// Название столбца с идентификатором факультета.
        /// </summary>
        public const string FacultyIdColumn = "faculty_id";

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
        public const int NameLengthMax = 128;

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
        public static Schema Schema
        {
            get
            {
                List<Column> columnList = new List<Column>
                {
                    new IntColumn(IdColumn) { IsPrimaryKey = true, IsAutoIncrementable = true },
                    new IntColumn(FacultyIdColumn),
                    new StringColumn(NameColumn, NameLengthMax)
                };

                return new Schema(Table, columnList, new List<ReferenceLink>()
                {
                    new ReferenceLink(FacultyIdColumn, Faculty.Table, Faculty.IdColumn)
                });
            }
        }

        /// <summary>
        /// Инициализировать сущность из схемы с данными.
        /// </summary>
        /// <param name="data">Схема, заполненная данными.</param>
        /// <param name="faculty">Зависимая сущность.</param>
        /// <returns>Сущность.</returns>
        public static Course FromData(Schema data, Faculty faculty)
        {
            if (data == null || !data.IsSameAsSample(Schema))
            {
                throw new ArgumentException("Переданная схема не соответствует схеме для сущности.");
            }

            if (data.GetIntColumnData(FacultyIdColumn) != faculty.Id)
            {
                throw new ArgumentException("Переданные схема с данными и сущность не соответствуют друг другу.");
            }

            return new Course(
                data.GetIntColumnData(IdColumn),
                faculty,
                data.GetStringColumnData(NameColumn));
        }

        /// <summary>
        /// Получить схему таблицы с данными.
        /// </summary>
        /// <returns>Схема, заполненная данными.</returns>
        public override Schema ToData()
        {
            Schema data = Schema;

            data.SetColumnData(IdColumn, Id);
            data.SetColumnData(FacultyIdColumn, Faculty.Id);
            data.SetColumnData(NameColumn, Name);

            return data;
        }

        #endregion

        /*
         *   ___ ___  _   _ _ __ ___  ___
         *  / __/ _ \| | | | '__/ __|/ _ \
         * | (_| (_) | |_| | |  \__ \  __/
         *  \___\___/ \__,_|_|  |___/\___|
         *
         */
        #region Course

        /// <summary>
        /// Факультет.
        /// </summary>
        private Faculty _faculty;

        /// <summary>
        /// Название.
        /// </summary>
        private string _name;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="faculty">Факультет.</param>
        /// <param name="name">Название.</param>
        public Course(int id, Faculty faculty, string name) : base(id)
        {
            Faculty = faculty;
            Name = name;
        }

        /// <summary>
        /// Доступ к факультету.
        /// </summary>
        public Faculty Faculty
        {
            get => _faculty;
            set => _faculty = value;
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
