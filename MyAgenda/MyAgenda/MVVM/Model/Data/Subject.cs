using System;

namespace MyAgenda.MVVM.Model.Data
{
    /// <summary>
    /// Контейнер данных занятия.
    /// </summary>
    internal class SubjectData : DataContainer
    {
        /// <summary>
        /// Идентификатор преподавателя.
        /// </summary>
        public int TeacherId { get; set; }

        /// <summary>
        /// Название.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Кабинет.
        /// </summary>
        public string Classroom { get; set; }
    }

    /// <summary>
    /// Занятие.
    /// </summary>
    internal class Subject : DataEntity
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
        public static SubjectData Container => new SubjectData();

        /// <summary>
        /// Преобразовать данные в новую сущность.
        /// </summary>
        /// <param name="data">Контейнер данных.</param>
        /// <returns>Новая сущность.</returns>
        public static Subject FromData(SubjectData data)
        {
            if (String.IsNullOrWhiteSpace(data.Classroom))
            {
                return new Subject(data.Id, data.Name);
            }

            return new Subject(data.Id, data.Name, data.Classroom);
        }

        /// <summary>
        /// Преобразовать данные в новую сущность.
        /// </summary>
        /// <param name="data">Контейнер данных.</param>
        /// <param name="teacher">Вложенная сущность.</param>
        /// <returns>Новая сущность.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static Subject FromData(SubjectData data, Teacher teacher)
        {
            if (data.TeacherId != teacher.Id)
            {
                throw new ArgumentException("Переданные контейнер и сущность не соответствуют друг другу.");
            }

            if (String.IsNullOrWhiteSpace(data.Classroom))
            {
                return new Subject(data.Id, teacher, data.Name);
            }

            return new Subject(data.Id, teacher, data.Name);
        }

        /// <summary>
        /// Получить контейнер данных для сущности.
        /// </summary>
        /// <returns>Контейнер данных.</returns>
        public SubjectData ToData()
        {
            SubjectData data = Container;

            data.Id = Id;
            data.Name = Name;

            if (HasTeacher())
            {
                data.TeacherId = Teacher.Id;
            }

            if (HasClassroom())
            {
                data.Classroom = Classroom;
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
