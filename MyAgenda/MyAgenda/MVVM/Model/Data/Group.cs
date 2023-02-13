using System;

namespace MyAgenda.MVVM.Model.Data
{
    /// <summary>
    /// Группа.
    /// </summary>
    internal class Group : DataEntity, IInitializable<Course>
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
        public new const string Table = "group";

        /// <summary>
        /// Название столбца с идентификатором курса.
        /// </summary>
        public const string CourseIdColumn = "course_id";

        /// <summary>
        /// Название столбца с кодом.
        /// </summary>
        public const string CodeColumn = "code";

        /// <summary>
        /// Минимальный идентификатор факультета.
        /// </summary>
        public const int CourseIdMin = Course.IdMin;

        /// <summary>
        /// Минимальная длина названия.
        /// </summary>
        public const int CodeLengthMin = 1;

        /// <summary>
        /// Максимальная длина названия.
        /// </summary>
        public const int CodeLengthMax = 32;

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
        /// Курс.
        /// </summary>
        private Course _course = null;

        /// <summary>
        /// Доступ к курсу.
        /// </summary>
        public Course Course
        {
            get => _course;
            private set => _course = value;
        }

        /// <summary>
        /// Проверить статус инициализации.
        /// </summary>
        /// <returns>Статус инициализации.</returns>
        public bool IsInitialized()
        {
            return Course != null;
        }

        /// <summary>
        /// Инициализировать данные.
        /// </summary>
        /// <param name="course">Курс.</param>
        /// <exception cref="ArgumentException"></exception>
        public void Initialize(Course course)
        {
            if (course.Id != CourseId)
            {
                throw new ArgumentException("Попытка инициализации с некорректной сущностью.");
            }

            Course = course;
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
        /// Идентификатор курса.
        /// </summary>
        private int _courseId;

        /// <summary>
        /// Код.
        /// </summary>
        private string _code;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="courseId">Идентификатор курса.</param>
        /// <param name="code">Код.</param>
        public Group(int id, int courseId, string code) : base(id)
        {
            CourseId = courseId;
            Code = code;
        }

        /// <summary>
        /// Инициализируемый конструктор.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="course">Курс.</param>
        /// <param name="code">Код.</param>
        public Group(int id, Course course, string code) : this(id, course.Id, code)
        {
            Initialize(course);
        }

        /// <summary>
        /// Доступ к идентификатору курса.
        /// </summary>
        public int CourseId
        {
            get => _courseId;
            set
            {
                if (value < CourseIdMin)
                {
                    throw new ArgumentException("Идентификатор курса не может выходить за допустимые пределы.");
                }

                _courseId = value;
            }
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
