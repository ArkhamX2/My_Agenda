namespace MyAgenda.MVVM.Model.Data
{
    /// <summary>
    /// Доступные типы учебой недели.
    /// </summary>
    public enum AvailableWeekType
    {
        None,
        Red,
        Blue
    }

    /// <summary>
    /// Тип учебой недели.
    /// TODO: Решить, как с этим работать.
    /// </summary>
    internal class WeekType : DataEntity
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
        public new const string Table = "week_type";

        /// <summary>
        /// Название столбца с типом.
        /// </summary>
        public const string TypeColumn = "type";

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
        private AvailableWeekType _type = AvailableWeekType.None;

        /// <summary>
        /// Пустой конструктор.
        /// </summary>
        public WeekType(int id) : base(id)
        {
            // PASS.
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
