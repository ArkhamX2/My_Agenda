﻿using System;
using System.Collections.Generic;

namespace MyAgenda.MVVM.Model.Data
{
    /// <summary>
    /// Занятие.
    /// </summary>
    internal class Subject : Entity, IIndirectlySchemable
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
        public const string Table = "subject";

        /// <summary>
        /// Название столбца с идентификатором.
        /// </summary>
        public const string IdColumn = DataEntity.IdColumn;

        /// <summary>
        /// Название столбца с идентификатором преподавателя.
        /// </summary>
        public const string TeacherIdColumn = "teacher_id";

        /// <summary>
        /// Название столбца с названием.
        /// </summary>
        public const string NameColumn = "name";

        /// <summary>
        /// Название столбца с кабинетом.
        /// </summary>
        public const string ClassroomColumn = "classroom";

        /// <summary>
        /// Минимальная длина названия.
        /// </summary>
        public const int NameLengthMin = 1;

        /// <summary>
        /// Минимальная длина кабинета.
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

                columnList.Add(new IntColumn(TeacherIdColumn) { IsNullable = true });
                columnList.Add(new StringColumn(NameColumn, NameLengthMax));
                columnList.Add(new StringColumn(ClassroomColumn, ClassroomLengthMax) { IsNullable = true });

                return new Schema(Table, columnList, new List<ReferenceLink>()
                {
                    new ReferenceLink(TeacherIdColumn, Teacher.Table, Teacher.IdColumn)
                });
            }
        }

        /// <summary>
        /// Инициализировать сущность из схемы с данными.
        /// </summary>
        /// <param name="data">Схема, заполненная данными.</param>
        /// <returns>Сущность.</returns>
        public static Subject FromData(Schema data)
        {
            // Базовый уровень валидации.
            DataEntity.FromData(data);

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
        /// Инициализировать сущность из схемы с данными.
        /// </summary>
        /// <param name="data">Схема, заполненная данными.</param>
        /// <param name="teacher">Зависимая сущность.</param>
        /// <returns>Сущность.</returns>
        public static Subject FromData(Schema data, Teacher teacher)
        {
            // Базовый уровень валидации.
            DataEntity.FromData(data);

            if (data.GetIntColumnData(TeacherIdColumn) != teacher.Id)
            {
                throw new ArgumentException("Переданные схема с данными и сущность не соответствуют друг другу.");
            }

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
        public Schema ToData()
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
        public Subject(int id, string name)
        {
            Schemable = new DataEntity(id);

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
            set => _teacher = value;
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

        /// <summary>
        /// Доступ к кабинету.
        /// </summary>
        public string Classroom
        {
            get => _classroom;
            set
            {
                value = value.Trim().ToLower();

                if (value.Length < ClassroomLengthMin || value.Length > ClassroomLengthMax)
                {
                    throw new ArgumentException("Длина кабинета не может выходить за допустимые пределы.");
                }

                _classroom = value;
            }
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
