using System;

namespace MyAgenda.MVVM.Model.Data
{
    /// <summary>
    /// Контейнер данных курса.
    /// </summary>
    internal class CourseData : DataContainer
    {
        /// <summary>
        /// Идентификатор факультета.
        /// </summary>
        public int FacultyId { get; set; }

        /// <summary>
        /// Название.
        /// </summary>
        public string Name { get; set; }
    }

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
        public new const string Table = "course";

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
        public static CourseData Container => new CourseData();

        /// <summary>
        /// Преобразовать данные в новую сущность.
        /// </summary>
        /// <param name="data">Контейнер данных.</param>
        /// <param name="faculty">Вложенная сущность.</param>
        /// <returns>Новая сущность.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static Course FromData(CourseData data, Faculty faculty)
        {
            if (data.FacultyId != faculty.Id)
            {
                throw new ArgumentException("Переданные контейнер и сущность не соответствуют друг другу.");
            }

            return new Course(data.Id, faculty, data.Name);
        }

        /// <summary>
        /// Получить контейнер данных для сущности.
        /// </summary>
        /// <returns>Контейнер данных.</returns>
        public CourseData ToData()
        {
            CourseData data = Container;

            data.Id = Id;
            data.FacultyId = Faculty.Id;
            data.Name = Name;

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
