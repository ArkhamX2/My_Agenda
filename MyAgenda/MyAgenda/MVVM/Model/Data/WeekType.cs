using System.Collections.Generic;

namespace MyAgenda.MVVM.Model.Data
{
    /// <summary>
    /// Доступные типы учебой недели.
    /// </summary>
    public enum AvailableWeekType
    {
        Incorrect,
        Red,
        Blue
    }
    
    /// <summary>
    /// Тип учебой недели.
    /// TODO: Решить, как с этим работать.
    /// </summary>
    internal class WeekType : Entity, IIndirectlySchemable
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
        public const string Table = "week_type";

        /// <summary>
        /// Название столбца с идентификатором.
        /// </summary>
        public const string IdColumn = DataEntity.IdColumn;

        /// <summary>
        /// Название столбца с типом.
        /// </summary>
        public const string TypeColumn = "type";

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

                columnList.Add(new IntColumn(TypeColumn));

                return new Schema(Table, columnList);
            }
        }

        /// <summary>
        /// Инициализировать сущность из схемы с данными.
        /// </summary>
        /// <param name="data">Схема, заполненная данными.</param>
        /// <returns>Сущность.</returns>
        public static WeekType FromData(Schema data)
        {
            // Базовый уровень валидации.
            DataEntity.FromData(data);

            return new WeekType(
                data.GetIntColumnData(IdColumn),
                (AvailableWeekType)data.GetIntColumnData(TypeColumn));
        }

        /// <summary>
        /// Получить схему таблицы с данными.
        /// </summary>
        /// <returns>Схема, заполненная данными.</returns>
        public Schema ToData()
        {
            Schema data = Schema;

            data.SetColumnData(IdColumn, Id);
            data.SetColumnData(TypeColumn, (int)Type);

            return data;
        }

        #endregion

        /*                    _      _
         * __      _____  ___| | __ | |_ _   _ _ __   ___
         * \ \ /\ / / _ \/ _ \ |/ / | __| | | | '_ \ / _ \
         *  \ V  V /  __/  __/   <  | |_| |_| | |_) |  __/
         *   \_/\_/ \___|\___|_|\_\  \__|\__, | .__/ \___|
         *                               |___/|_|
         */
        #region WeekType

        /// <summary>
        /// Текущий тип учебной недели.
        /// </summary>
        private AvailableWeekType _type = AvailableWeekType.Incorrect;

        /// <summary>
        /// Пустой конструктор.
        /// </summary>
        public WeekType(int id)
        {
            Schemable = new DataEntity(id);
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="type">Тип.</param>
        public WeekType(int id, AvailableWeekType type) : this(id)
        {
            Type = type;
        }

        /// <summary>
        /// Доступ к текущему типу учебной недели.
        /// </summary>
        public AvailableWeekType Type
        {
            get => _type;
            set => _type = value;
        }

        /// <summary>
        /// Представить в виде строки.
        /// </summary>
        /// <returns>Строка с типом учебной недели.</returns>
        public override string ToString()
        {
            // TODO: Пересмотреть.

            switch (Type)
            {
                case AvailableWeekType.Red: return "красная";
                case AvailableWeekType.Blue: return "синяя";
            }

            return "неизвестная";
        }

        #endregion
    }
}
