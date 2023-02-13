using System;

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
        public new const string Table = "teacher";

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
            set
            {
                value = value.Trim().ToLower();
                
                if (value.Length < NameLengthMin || value.Length > NameLengthMax)
                {
                    throw new ArgumentException("Длина имени не может выходить за допустимые пределы.");
                }

                _name = value;
            }
        }

        /// <summary>
        /// Доступ к фамилии.
        /// </summary>
        public string Surname
        {
            get => _surname;
            set
            {
                value = value.Trim().ToLower();

                if (value.Length < NameLengthMin || value.Length > NameLengthMax)
                {
                    throw new ArgumentException("Длина фамилии не может выходить за допустимые пределы.");
                }

                _surname = value;
            }
        }

        /// <summary>
        /// Доступ к отчеству.
        /// </summary>
        public string Patronymic
        {
            get => _patronymic;
            set
            {
                value = value.Trim().ToLower();

                if (value.Length < NameLengthMin || value.Length > NameLengthMax)
                {
                    throw new ArgumentException("Длина отчества не может выходить за допустимые пределы.");
                }

                _patronymic = value;
            }
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
