namespace MyAgenda.Library.Data.Column
{
    /// <summary>
    /// Столбец типа VARCHAR в SQL.
    /// Соответствует string в C#.
    /// Может хранить в себе данные.
    /// </summary>
    internal class StringColumn : DataColumn
    {
        /*                                            _     _              _     _           _
         *   ___ ___  _ __ ___  _ __   __ _ _ __ __ _| |__ | | ___    ___ | |__ (_) ___  ___| |_
         *  / __/ _ \| '_ ` _ \| '_ \ / _` | '__/ _` | '_ \| |/ _ \  / _ \| '_ \| |/ _ \/ __| __|
         * | (_| (_) | | | | | | |_) | (_| | | | (_| | |_) | |  __/ | (_) | |_) | |  __/ (__| |_
         *  \___\___/|_| |_| |_| .__/ \__,_|_|  \__,_|_.__/|_|\___|  \___/|_.__// |\___|\___|\__|
         *                     |_|                                            |__/
         */
        #region ComparableObject

        /// <summary>
        /// Проверить образец на сходство с экземпляром.
        /// </summary>
        /// <param name="sample">Образец.</param>
        /// <returns>Статус проверки.</returns>
        public override bool IsSameAsObject(ComparableObject sample)
        {
            if (!(sample is StringColumn column))
            {
                return false;
            }

            if (!HandleIsSameAsObject(column))
            {
                return false;
            }

            return column.MaxLength == MaxLength;
        }

        /// <summary>
        /// Проверить образец на полное сходство с экземпляром.
        /// Помимо сравнивания полей класса также сравниваются хранимые данные.
        /// </summary>
        /// <param name="sample">Образец.</param>
        /// <returns>Статус проверки.</returns>
        public override bool IsExactSameAsObject(ComparableObject sample)
        {
            return IsSameAsObject(sample) && HandleIsExactSameAsObject(sample as StringColumn);
        }

        #endregion

        /*            _
         *   ___ ___ | |_   _ _ __ ___  _ __
         *  / __/ _ \| | | | | '_ ` _ \| '_ \
         * | (_| (_) | | |_| | | | | | | | | |
         *  \___\___/|_|\__,_|_| |_| |_|_| |_|
         *
         */
        #region DataColumn

        /// <summary>
        /// Максимальная длина данных по-умолчанию.
        /// </summary>
        public const int DefaultMaxLength = 255;

        /// <summary>
        /// Максимальная длина данных.
        /// </summary>
        private int _maxLength = DefaultMaxLength;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="name">Название.</param>
        public StringColumn(string name) : base(name)
        {
            // PASS.
        }

        /// <summary>
        /// Расширенный конструктор.
        /// </summary>
        /// <param name="name">Название.</param>
        /// <param name="maxLength">Максимальная длина данных.</param>
        public StringColumn(string name, int maxLength) : this(name)
        {
            MaxLength = maxLength;
        }

        /// <summary>
        /// Доступ к максимальной длине данных.
        /// </summary>
        public int MaxLength
        {
            get => _maxLength;
            set => _maxLength = value;
        }

        /// <summary>
        /// Проверить корректность типа данных.
        /// </summary>
        /// <param name="data">Данные.</param>
        /// <returns>Статус проверки.</returns>
        public override bool IsDataTypeAllowed(object data)
        {
            return data is string;
        }

        /// <summary>
        /// Проверить тип данных на возможность автоматического инкременирования.
        /// </summary>
        /// <returns>Статус проверки.</returns>
        public override bool IsDataTypeAutoIncrementable()
        {
            return false;
        }

        /// <summary>
        /// Получить представление типа данных в виде строки.
        /// </summary>
        /// <returns>Строка в формате SQL.</returns>
        public override string DataTypeAsString()
        {
            return $"VARCHAR({MaxLength})";
        }

        #endregion
    }
}
