using System;
using System.Xml.Linq;

namespace MyAgenda.MVVM.Model.Data
{
    /// <summary>
    /// Контейнер данных группы.
    /// </summary>
    internal class GroupData : DataContainer
    {
        /// <summary>
        /// Идентификатор курса.
        /// </summary>
        public int CourseId { get; set; }

        /// <summary>
        /// Код.
        /// </summary>
        public string Code { get; set; }
    }

    /// <summary>
    /// Группа.
    /// </summary>
    internal class Group : DataEntity
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
        /// Минимальная длина названия.
        /// </summary>
        public const int CodeLengthMin = 1;

        /// <summary>
        /// Максимальная длина названия.
        /// </summary>
        public const int CodeLengthMax = 32;

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
        public static GroupData Container => new GroupData();

        /// <summary>
        /// Преобразовать данные в новую сущность.
        /// </summary>
        /// <param name="data">Контейнер данных.</param>
        /// <param name="course">Вложенная сущность.</param>
        /// <returns>Новая сущность.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static Group FromData(GroupData data, Course course)
        {
            if (data.CourseId != course.Id)
            {
                throw new ArgumentException("Переданные контейнер и сущность не соответствуют друг другу.");
            }

            return new Group(data.Id, course, data.Code);
        }

        /// <summary>
        /// Получить контейнер данных для сущности.
        /// </summary>
        /// <returns>Контейнер данных.</returns>
        public GroupData ToData()
        {
            GroupData data = Container;

            data.Id = Id;
            data.CourseId = Course.Id;
            data.Code = Code;

            return data;
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
        /// Курс.
        /// </summary>
        private Course _course;

        /// <summary>
        /// Код.
        /// </summary>
        private string _code;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="course">Курс.</param>
        /// <param name="code">Код.</param>
        public Group(int id, Course course, string code) : base(id)
        {
            Course = course;
            Code = code;
        }

        /// <summary>
        /// Доступ к курсу.
        /// </summary>
        public Course Course
        {
            get => _course;
            set => _course = value;
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
