using System;
using System.Collections.Generic;
using MyAgenda.Library.Data;
using MyAgenda.Library.Data.Column;

namespace MyAgenda.Library.Entity.Base
{
    /// <summary>
    /// Занятие.
    /// </summary>
    public class Subject : DataEntity
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
        internal const string Table = "subject";

        /// <summary>
        /// Название столбца с идентификатором преподавателя.
        /// </summary>
        internal const string TeacherIdColumn = "teacher_id";

        /// <summary>
        /// Название столбца с названием.
        /// </summary>
        internal const string NameColumn = "name";

        /// <summary>
        /// Название столбца с кабинетом.
        /// </summary>
        internal const string ClassroomColumn = "classroom";

        /// <summary>
        /// Минимальная длина названия.
        /// </summary>
        public const int NameLengthMin = 1;

        /// <summary>
        /// Минимальная длина кабинета.
        /// Нулевое значение означает, что кабинет может быть не задан.
        /// </summary>
        public const int ClassroomLengthMin = 0;

        /// <summary>
        /// Максимальная длина названия.
        /// </summary>
        public const int NameLengthMax = 128;

        /// <summary>
        /// Максимальная длина кабинета.
        /// </summary>
        public const int ClassroomLengthMax = 32;

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
                List<DataColumn> columnList = new List<DataColumn>
                {
                    new IntColumn(IdColumn) { IsPrimaryKey = true, IsAutoIncrementable = true },
                    new IntColumn(TeacherIdColumn) { IsNullable = true },
                    new StringColumn(NameColumn, NameLengthMax),
                    new StringColumn(ClassroomColumn, ClassroomLengthMax) { IsNullable = true }
                };

                return new Schema(Table, columnList, new List<ReferenceLink>()
                {
                    new ReferenceLink(TeacherIdColumn, Teacher.Table, Teacher.IdColumn)
                });
            }
        }

        /// <summary>
        /// Инициализировать занятие из схемы с данными.
        /// </summary>
        /// <param name="data">Схема, заполненная данными.</param>
        /// <returns>Занятие.</returns>
        internal static Subject FromData(Schema data)
        {
            if (data == null || !data.IsSameAsSample(Schema))
            {
                throw new ArgumentException("Переданная схема не соответствует схеме для сущности.");
            }

            // Если кабинет не задан.
            if (String.IsNullOrWhiteSpace(data.GetStringColumnData(ClassroomColumn)))
            {
                return new Subject(
                    data.GetIntColumnData(IdColumn),
                    data.GetStringColumnData(NameColumn));
            }

            return new Subject(
                data.GetIntColumnData(IdColumn),
                data.GetStringColumnData(NameColumn),
                data.GetStringColumnData(ClassroomColumn));
        }

        /// <summary>
        /// Инициализировать заняте из схемы с данными.
        /// </summary>
        /// <param name="data">Схема, заполненная данными.</param>
        /// <param name="teacher">Преподаватель.</param>
        /// <returns>Занятие.</returns>
        internal static Subject FromData(Schema data, Teacher teacher)
        {
            if (data == null || !data.IsSameAsSample(Schema))
            {
                throw new ArgumentException("Переданная схема не соответствует схеме для сущности.");
            }

            if (data.GetIntColumnData(TeacherIdColumn) != teacher.Id)
            {
                throw new ArgumentException("Переданные схема с данными и сущность не соответствуют друг другу.");
            }

            // Если кабинет не задан.
            if (String.IsNullOrWhiteSpace(data.GetStringColumnData(ClassroomColumn)))
            {
                return new Subject(
                    data.GetIntColumnData(IdColumn),
                    teacher,
                    data.GetStringColumnData(NameColumn));
            }

            return new Subject(
                data.GetIntColumnData(IdColumn),
                teacher,
                data.GetStringColumnData(NameColumn),
                data.GetStringColumnData(ClassroomColumn));
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

            if (HasTeacher())
            {
                data.SetColumnData(TeacherIdColumn, Teacher.Id);
            }

            if (HasClassroom())
            {
                data.SetColumnData(ClassroomColumn, Classroom);
            }

            return data;
        }

        #endregion

        /*            _     _           _
         *  ___ _   _| |__ (_) ___  ___| |_
         * / __| | | | '_ \| |/ _ \/ __| __|
         * \__ \ |_| | |_) | |  __/ (__| |_
         * |___/\__,_|_.__// |\___|\___|\__|
         *               |__/
         */
        #region Subject

        /// <summary>
        /// Преподаватель.
        /// Может быть не задан.
        /// </summary>
        private Teacher _teacher = null;

        /// <summary>
        /// Название.
        /// </summary>
        private string _name;

        /// <summary>
        /// Кабинет.
        /// Может быть не задан.
        /// </summary>
        private string _classroom = String.Empty;

        /// <summary>
        /// Минимальный конструктор.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="name">Название.</param>
        public Subject(int id, string name) : base(id)
        {
            Name = name;
        }

        /// <summary>
        /// Базовый конструктор.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="name">Название.</param>
        /// <param name="classroom">Кабинет.</param>
        public Subject(int id, string name, string classroom) : this(id, name)
        {
            Classroom = classroom;
        }

        /// <summary>
        /// Базовый конструктор.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="teacher">Преподаватель.</param>
        /// <param name="name">Название.</param>
        public Subject(int id, Teacher teacher, string name) : this(id, name)
        {
            Teacher = teacher;
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="teacher">Преподаватель.</param>
        /// <param name="name">Название.</param>
        /// <param name="classroom">Кабинет.</param>
        public Subject(int id, Teacher teacher, string name, string classroom) : this(id, teacher, name)
        {
            Classroom = classroom;
        }

        /// <summary>
        /// Доступ к преподавателю.
        /// </summary>
        public Teacher Teacher
        {
            get => _teacher;
            private set => _teacher = value;
        }

        /// <summary>
        /// Доступ к названию.
        /// </summary>
        public string Name
        {
            get => _name;
            private set => _name = ValidateStringData(value, NameLengthMin, NameLengthMax);
        }

        /// <summary>
        /// Доступ к кабинету.
        /// </summary>
        public string Classroom
        {
            get => _classroom;
            private set => _classroom = ValidateStringData(value, ClassroomLengthMin, ClassroomLengthMax);
        }

        /// <summary>
        /// Проверить наличие преподавателя.
        /// </summary>
        /// <returns>Статус проверки.</returns>
        public bool HasTeacher()
        {
            return Teacher != null;
        }

        /// <summary>
        /// Проверить наличие кабинета.
        /// </summary>
        /// <returns>Статус проверки.</returns>
        public bool HasClassroom()
        {
            return String.IsNullOrWhiteSpace(Classroom);
        }

        #endregion
    }
}
