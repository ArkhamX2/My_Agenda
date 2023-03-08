using System;
using System.Collections.Generic;
using MyAgenda.Library.Data;
using MyAgenda.Library.Data.Column;

namespace MyAgenda.Library.Model.Base
{
    /// <summary>
    /// Группа.
    /// </summary>
    public class Group : DataEntity
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
        internal const string Table = "group";

        /// <summary>
        /// Название столбца с идентификатором курса.
        /// </summary>
        internal const string CourseIdColumn = "course_id";

        /// <summary>
        /// Название столбца с кодом.
        /// </summary>
        internal const string CodeColumn = "code";

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
        /// Доступ к схеме данных.
        /// </summary>
        internal static Schema Schema
        {
            get
            {
                var columnList = new List<DataColumn>
                {
                    new IntColumn(IdColumn) { IsPrimaryKey = true, IsAutoIncrementable = true },
                    new IntColumn(CourseIdColumn),
                    new StringColumn(CodeColumn, CodeLengthMax)
                };

                return new Schema(Table, columnList, new List<ReferenceLink>()
                {
                    new ReferenceLink(CourseIdColumn, Course.Table, Course.IdColumn)
                });
            }
        }

        /// <summary>
        /// Инициализировать группу из схемы с данными.
        /// </summary>
        /// <param name="data">Схема, заполненная данными.</param>
        /// <param name="course">Курс.</param>
        /// <returns>Группа.</returns>
        /// <exception cref="ArgumentException"></exception>
        internal static Group FromData(Schema data, Course course)
        {
            if (data == null || !data.IsSameAsSample(Schema))
            {
                throw new ArgumentException("Переданная схема не соответствует схеме для сущности.");
            }

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
        internal override Schema ToData()
        {
            var data = Schema;

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
        public Group(int id, Course course, string code) : base(id)
        {
            Course = course;
            Code = code;
        }

        /// <summary>
        /// Доступ к курсу.
        /// </summary>
        public Course Course
        {
            get => _course;
            private set => _course = value;
        }

        /// <summary>
        /// Доступ к коду.
        /// </summary>
        public string Code
        {
            get => _code;
            private set => _code = ValidateStringData(value, CodeLengthMin, CodeLengthMax);
        }

        #endregion
    }
}
