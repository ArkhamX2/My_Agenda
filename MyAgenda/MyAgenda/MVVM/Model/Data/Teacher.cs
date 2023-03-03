﻿using System;
using System.Collections.Generic;

namespace MyAgenda.MVVM.Model.Data
{
    /// <summary>
    /// Преподаватель.
    /// </summary>
    internal class Teacher : DataEntity
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
        public const string Table = "teacher";

        /// <summary>
        /// Название столбца с именем.
        /// </summary>
        public const string NameColumn = "name";

        /// <summary>
        /// Название столбца с фамилией.
        /// </summary>
        public const string SurnameColumn = "surname";

        /// <summary>
        /// Название столбца с отчеством.
        /// </summary>
        public const string PatronymicColumn = "patronymic";

        /// <summary>
        /// Минимальная длина имени.
        /// </summary>
        public const int NameLengthMin = 1;

        /// <summary>
        /// Минимальная длина фамилии.
        /// </summary>
        public const int SurnameLengthMin = 1;

        /// <summary>
        /// Минимальная длина отчества.
        /// Нулевое значение означает, что отчество может быть не задано.
        /// </summary>
        public const int PatronymicLengthMin = 0;

        /// <summary>
        /// Максимальная длина имени.
        /// </summary>
        public const int NameLengthMax = 64;

        /// <summary>
        /// Максимальная длина фамилии.
        /// </summary>
        public const int SurnameLengthMax = 64;

        /// <summary>
        /// Максимальная длина отчества.
        /// </summary>
        public const int PatronymicLengthMax = 64;

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
        public static Schema Schema => new Schema(Table, new List<Column>
        {
            new IntColumn(IdColumn) { IsPrimaryKey = true, IsAutoIncrementable = true },
            new StringColumn(NameColumn, NameLengthMax),
            new StringColumn(SurnameColumn, SurnameLengthMax),
            new StringColumn(PatronymicColumn, PatronymicLengthMax) { IsNullable = true }
        });

        /// <summary>
        /// Инициализировать преподавателя из схемы с данными.
        /// </summary>
        /// <param name="data">Схема, заполненная данными.</param>
        /// <returns>Преподаватель.</returns>
        public static Teacher FromData(Schema data)
        {
            if (data == null || !data.IsSameAsSample(Schema))
            {
                throw new ArgumentException("Переданная схема не соответствует схеме для сущности.");
            }

            // Если отчество не задано.
            if (String.IsNullOrWhiteSpace(data.GetStringColumnData(PatronymicColumn)))
            {
                return new Teacher(
                    data.GetIntColumnData(IdColumn),
                    data.GetStringColumnData(NameColumn),
                    data.GetStringColumnData(SurnameColumn));
            }

            return new Teacher(
                data.GetIntColumnData(IdColumn),
                data.GetStringColumnData(NameColumn),
                data.GetStringColumnData(SurnameColumn),
                data.GetStringColumnData(PatronymicColumn));
        }

        /// <summary>
        /// Получить схему таблицы с данными.
        /// </summary>
        /// <returns>Схема, заполненная данными.</returns>
        public override Schema ToData()
        {
            Schema data = Schema;

            data.SetColumnData(IdColumn, Id);
            data.SetColumnData(NameColumn, Name);
            data.SetColumnData(SurnameColumn, Surname);

            if (HasPatronymic())
            {
                data.SetColumnData(PatronymicColumn, Patronymic);
            }

            return data;
        }

        #endregion

        /*  _                  _
         * | |_ ___  __ _  ___| |__   ___ _ __
         * | __/ _ \/ _` |/ __| '_ \ / _ \ '__|
         * | ||  __/ (_| | (__| | | |  __/ |
         *  \__\___|\__,_|\___|_| |_|\___|_|
         *
         */
        #region Teacher

        /// <summary>
        /// Имя.
        /// </summary>
        private string _name;

        /// <summary>
        /// Фамилия.
        /// </summary>
        private string _surname;

        /// <summary>
        /// Отчество.
        /// Может быть не задано.
        /// </summary>
        private string _patronymic = String.Empty;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="name">Имя.</param>
        /// <param name="surname">Фамилия.</param>
        public Teacher(int id, string name, string surname) : base(id)
        {
            Name = name;
            Surname = surname;
        }

        /// <summary>
        /// Расширенный конструктор.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="name">Имя.</param>
        /// <param name="surname">Фамилия.</param>
        /// <param name="patronymic">Отчество.</param>
        public Teacher(int id, string name, string surname, string patronymic) : this(id, name, surname)
        {
            Patronymic = patronymic;
        }

        /// <summary>
        /// Доступ к имени.
        /// </summary>
        public string Name
        {
            get => _name;
            set => _name = ValidateStringData(value, NameLengthMin, NameLengthMax);
        }

        /// <summary>
        /// Доступ к фамилии.
        /// </summary>
        public string Surname
        {
            get => _surname;
            set => _surname = ValidateStringData(value, SurnameLengthMin, SurnameLengthMax);
        }

        /// <summary>
        /// Доступ к отчеству.
        /// </summary>
        public string Patronymic
        {
            get => _patronymic;
            set => _patronymic = ValidateStringData(value, PatronymicLengthMin, PatronymicLengthMax);
        }

        /// <summary>
        /// Проверить наличие отчества.
        /// </summary>
        /// <returns>Статус проверки.</returns>
        public bool HasPatronymic()
        {
            return String.IsNullOrWhiteSpace(Patronymic);
        }

        #endregion
    }
}
