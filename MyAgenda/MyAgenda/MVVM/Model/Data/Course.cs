using System;

namespace MyAgenda.MVVM.Model.Data
{
    /// <summary>
    /// Курс.
    /// </summary>
    internal class Course : DataEntity, IInitializable<Faculty>
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
        /// Минимальный идентификатор факультета.
        /// </summary>
        public const int FacultyIdMin = Faculty.IdMin;

        /// <summary>
        /// Минимальная длина названия.
        /// </summary>
        public const int NameLengthMin = 1;

        /// <summary>
        /// Максимальная длина названия.
        /// </summary>
        public const int NameLengthMax = 128;

        #endregion

        /*  _       _ _   _       _ _          _     _
         * (_)_ __ (_) |_(_) __ _| (_)______ _| |__ | | ___
         * | | '_ \| | __| |/ _` | | |_  / _` | '_ \| |/ _ \
         * | | | | | | |_| | (_| | | |/ / (_| | |_) | |  __/
         * |_|_| |_|_|\__|_|\__,_|_|_/___\__,_|_.__/|_|\___|
         *
         */
        #region IInitializable

        /// <summary>
        /// Факультет.
        /// </summary>
        private Faculty _faculty;

        /// <summary>
        /// Доступ к факультету.
        /// </summary>
        public Faculty Faculty
        {
            get => _faculty;
            private set => _faculty = value;
        }

        /// <summary>
        /// Проверить статус инициализации.
        /// </summary>
        /// <returns>Статус инициализации.</returns>
        public bool IsInitialized()
        {
            return Faculty != null;
        }

        /// <summary>
        /// Инициализировать данные.
        /// </summary>
        /// <param name="faculty">Факультет.</param>
        /// <exception cref="ArgumentException"></exception>
        public void Initialize(Faculty faculty)
        {
            if (faculty.Id != FacultyId)
            {
                throw new ArgumentException("Попытка инициализации с некорректной сущностью.");
            }

            Faculty = faculty;
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
        /// Идентификатор факультета.
        /// </summary>
        private int _facultyId;

        /// <summary>
        /// Название.
        /// </summary>
        private string _name;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="facultyId">Идентификатор факультета.</param>
        /// <param name="name">Название.</param>
        public Course(int id, int facultyId, string name) : base(id)
        {
            FacultyId = facultyId;
            Name = name;
        }

        /// <summary>
        /// Инициализируемый конструктор.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="faculty">Факультет.</param>
        /// <param name="name">Название.</param>
        public Course(int id, Faculty faculty, string name) : this(id, faculty.Id, name)
        {
            Initialize(faculty);
        }

        /// <summary>
        /// Доступ к идентификатору факультета.
        /// </summary>
        public int FacultyId
        {
            get => _facultyId;
            set
            {
                if (value < FacultyIdMin)
                {
                    throw new ArgumentException("Идентификатор факультета не может выходить за допустимые пределы.");
                }

                _facultyId = value;
            }
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
