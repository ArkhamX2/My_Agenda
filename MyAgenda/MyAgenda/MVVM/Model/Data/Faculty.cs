using System;

namespace MyAgenda.MVVM.Model.Data
{
    /// <summary>
    /// Контейнер данных факультета.
    /// </summary>
    internal class FacultyData : DataContainer
    {
        /// <summary>
        /// Название.
        /// </summary>
        public string Name { get; set; }
    }

    /// <summary>
    /// Факультет.
    /// </summary>
    internal class Faculty : DataEntity
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
        public new const string Table = "faculty";

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

        /*      _       _                          _        _
         *   __| | __ _| |_ __ _    ___ ___  _ __ | |_ __ _(_)_ __   ___ _ __
         *  / _` |/ _` | __/ _` |  / __/ _ \| '_ \| __/ _` | | '_ \ / _ \ '__|
         * | (_| | (_| | || (_| | | (_| (_) | | | | || (_| | | | | |  __/ |
         *  \__,_|\__,_|\__\__,_|  \___\___/|_| |_|\__\__,_|_|_| |_|\___|_|
         *
         */
        #region DataContainer

        /// <summary>
        /// Доступ к контейнеру данных.
        /// </summary>
        public static FacultyData Container => new FacultyData();

        /// <summary>
        /// Преобразовать данные в новую сущность.
        /// </summary>
        /// <param name="data">Контейнер данных.</param>
        /// <returns>Новая сущность.</returns>
        public static Faculty FromData(FacultyData data)
        {
            return new Faculty(data.Id, data.Name);
        }

        /// <summary>
        /// Получить контейнер данных для сущности.
        /// </summary>
        /// <returns>Контейнер данных.</returns>
        public FacultyData ToData()
        {
            FacultyData data = Container;

            data.Id = Id;
            data.Name = Name;

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
