using System;
using System.Collections.Generic;

namespace MyAgenda.MVVM.Model.Data
{
    /// <summary>
    /// Группа.
    /// </summary>
    internal class Group : IIndirectlySchemable
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
        public const string Table = "group";

        /// <summary>
        /// Название столбца с идентификатором.
        /// </summary>
        public const string IdColumn = DataEntity.IdColumn;

        /// <summary>
        /// Название столбца с идентификатором курса.
        /// </summary>
        public const string CourseIdColumn = "course_id";

        /// <summary>
        /// Название столбца с кодом.
        /// </summary>
        public const string CodeColumn = "code";

        /// <summary>
        /// Минимальная длина названия.
        /// </summary>
        public const int CodeLengthMin = 1;

        /// <summary>
        /// Максимальная длина названия.
        /// </summary>
        public const int CodeLengthMax = 32;

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

                columnList.Add(new IntColumn(CourseIdColumn));
                columnList.Add(new StringColumn(CodeColumn, CodeLengthMax));

                return new Schema(Table, columnList, new List<ReferenceLink>()
                {
                    new ReferenceLink(CourseIdColumn, Course.Table, Course.IdColumn)
                });
            }
        }

        /// <summary>
        /// Инициализировать сущность из схемы с данными.
        /// </summary>
        /// <param name="data">Схема, заполненная данными.</param>
        /// <param name="course">Зависимая сущность.</param>
        /// <returns>Сущность.</returns>
        public static Group FromData(Schema data, Course course)
        {
            // Базовый уровень валидации.
            DataEntity.FromData(data);

            if (data.GetIntColumnData(CourseIdColumn) != course.Id)
            {
                throw new ArgumentException("Переданные схема с данными и сущность не соответствуют друг другу.");
            }

            return new Group(
                data.GetIntColumnData(IdColumn),
                course,
                data.GetStringColumnData(CodeColumn));
        }

        /// <summary>
        /// Получить схему таблицы с данными.
        /// </summary>
        /// <returns>Схема, заполненная данными.</returns>
        public Schema ToData()
        {
            Schema data = Schema;

            data.SetColumnData(IdColumn, Id);
            data.SetColumnData(CourseIdColumn, Course.Id);
            data.SetColumnData(CodeColumn, Code);

            return data;
        }

        #endregion

        /*
         *   __ _ _ __ ___  _   _ _ __
         *  / _` | '__/ _ \| | | | '_ \
         * | (_| | | | (_) | |_| | |_) |
         *  \__, |_|  \___/ \__,_| .__/
         *  |___/                |_|
         */
        #region Group

        /// <summary>
        /// Курс.
        /// </summary>
        private Course _course;

        /// <summary>
        /// Код.
        /// </summary>
        private string _code;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="course">Курс.</param>
        /// <param name="code">Код.</param>
        public Group(int id, Course course, string code)
        {
            Schemable = new DataEntity(id);

            Course = course;
            Code = code;
        }

        /// <summary>
        /// Доступ к курсу.
        /// </summary>
        public Course Course
        {
            get => _course;
            set => _course = value;
        }

        /// <summary>
        /// Доступ к коду.
        /// </summary>
        public string Code
        {
            get => _code;
            set
            {
                value = value.Trim().ToLower();

                if (value.Length < CodeLengthMin || value.Length > CodeLengthMax)
                {
                    throw new ArgumentException("Длина кода не может выходить за допустимые пределы.");
                }

                _code = value;
            }
        }

        #endregion
    }
}
