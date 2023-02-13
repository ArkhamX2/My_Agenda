using System;

namespace MyAgenda.MVVM.Model.Data
{
    /// <summary>
    /// Занятие.
    /// </summary>
    internal class Subject : DataEntity, IInitializable<Teacher>
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
        public new const string Table = "subject";

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
        /// Идентификатор несуществующего преподавателя.
        /// </summary>
        public const int TeacherIdUndefined = -1;

        /// <summary>
        /// Минимальный идентификатор преподавателя.
        /// Так как преподаватель может быть не задан, используется
        /// идентификатор несуществующего преподавателя в качестве минимального.
        /// </summary>
        public const int TeacherIdMin = TeacherIdUndefined;

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
        private Teacher _teacher = null;

        /// <summary>
        /// Доступ к курсу.
        /// </summary>
        public Teacher Teacher
        {
            get => _teacher;
            private set => _teacher = value;
        }

        /// <summary>
        /// Проверить статус инициализации.
        /// </summary>
        /// <returns>Статус инициализации.</returns>
        public bool IsInitialized()
        {
            // Если преподаватель не задан.
            if (TeacherId == TeacherIdUndefined)
            {
                // Инициализация не нужна.
                return true;
            }

            return Teacher != null;
        }

        /// <summary>
        /// Инициализировать данные.
        /// </summary>
        /// <param name="teacher">Преподаватель.</param>
        /// <exception cref="ArgumentException"></exception>
        public void Initialize(Teacher teacher)
        {
            if (teacher.Id != TeacherId)
            {
                throw new ArgumentException("Попытка инициализации с некорректной сущностью.");
            }

            Teacher = teacher;
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
        /// Идентификатор преподавателя.
        /// </summary>
        private int _teacherId = TeacherIdUndefined;

        /// <summary>
        /// Название.
        /// </summary>
        private string _name;

        /// <summary>
        /// Кабинет.
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
        /// <param name="teacherId">Идентификатор преподавателя.</param>
        /// <param name="name">Название.</param>
        public Subject(int id, int teacherId, string name) : this(id, name)
        {
            TeacherId = teacherId;
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="teacherId">Идентификатор преподавателя.</param>
        /// <param name="name">Название.</param>
        /// <param name="classroom">Кабинет.</param>
        public Subject(int id, int teacherId, string name, string classroom) : this(id, teacherId, name)
        {
            Classroom = classroom;
        }

        /// <summary>
        /// Инициализируемый базовый конструктор.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="teacher">Преподаватель.</param>
        /// <param name="name">Название.</param>
        public Subject(int id, Teacher teacher, string name) : this(id, teacher.Id, name)
        {
            Initialize(teacher);
        }

        /// <summary>
        /// Инициализируемый конструктор.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="teacher">Преподаватель.</param>
        /// <param name="name">Название.</param>
        /// <param name="classroom">Кабинет.</param>
        public Subject(int id, Teacher teacher, string name, string classroom) : this(id, teacher.Id, name, classroom)
        {
            Initialize(teacher);
        }

        /// <summary>
        /// Доступ к идентификатору преподавателя.
        /// </summary>
        public int TeacherId
        {
            get => _teacherId;
            set
            {
                if (value < TeacherIdMin)
                {
                    throw new ArgumentException("Идентификатор преподавателя не может выходить за допустимые пределы.");
                }

                _teacherId = value;
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
        public bool HasTeacherId()
        {
            return TeacherId != TeacherIdUndefined;
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
