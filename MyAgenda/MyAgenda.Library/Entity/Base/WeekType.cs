using System;
using System.Collections.Generic;
using MyAgenda.Library.Data;
using MyAgenda.Library.Data.Column;

namespace MyAgenda.Library.Entity.Base
{
    /// <summary>
    /// Доступные типы учебой недели.
    /// </summary>
    public enum AvailableWeekType
    {
        Red,
        Blue
    }

    /// <summary>
    /// Тип учебой недели.
    /// TODO: Решить, как с этим работать.
    /// </summary>
    public class WeekType : DataEntity
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
        internal const string Table = "week_type";

        /// <summary>
        /// Название столбца с типом.
        /// </summary>
        internal const string TypeColumn = "type";

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
        internal static Schema Schema => new Schema(Table, new List<DataColumn>
        {
            new IntColumn(IdColumn) { IsPrimaryKey = true, IsAutoIncrementable = true },
            new IntColumn(TypeColumn)
        });

        /// <summary>
        /// Инициализировать тип недели из схемы с данными.
        /// </summary>
        /// <param name="data">Схема, заполненная данными.</param>
        /// <returns>Тип учебной недели.</returns>
        internal static WeekType FromData(Schema data)
        {
            if (data == null || !data.IsSameAsSample(Schema))
            {
                throw new ArgumentException("Переданная схема не соответствует схеме для сущности.");
            }

            return new WeekType(
                data.GetIntColumnData(IdColumn),
                (AvailableWeekType)data.GetIntColumnData(TypeColumn));
        }

        /// <summary>
        /// Получить схему таблицы с данными.
        /// </summary>
        /// <returns>Схема, заполненная данными.</returns>
        internal override Schema ToData()
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
        private AvailableWeekType _type;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="type">Тип недели.</param>
        public WeekType(int id, AvailableWeekType type) : base(id)
        {
            Type = type;
        }

        /// <summary>
        /// Доступ к текущему типу учебной недели.
        /// </summary>
        public AvailableWeekType Type
        {
            get => _type;
            private set => _type = value;
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
