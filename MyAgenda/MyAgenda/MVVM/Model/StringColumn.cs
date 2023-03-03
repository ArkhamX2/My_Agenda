namespace MyAgenda.MVVM.Model
{
    /// <summary>
    /// Столбец типа VARCHAR в SQL.
    /// Соответствует string в C#.
    /// Может хранить в себе данные.
    /// </summary>
    internal class StringColumn : Column
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
            if (sample.GetType() != GetType())
            {
                return false;
            }

            StringColumn column = sample as StringColumn;

            if (!HandleIsSameAsObject(column))
            {
                return false;
            }

            if (column.MaxLength != MaxLength)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Проверить образец на полное сходство с экземпляром.
        /// Помимо сравнивания полей класса также сравниваются хранимые данные.
        /// </summary>
        /// <param name="sample">Образец.</param>
        /// <returns>Статус проверки.</returns>
        public override bool IsExactSameAsObject(ComparableObject sample)
        {
            if (!IsSameAsObject(sample))
            {
                return false;
            }

            if (!HandleIsExactSameAsObject(sample as StringColumn))
            {
                return false;
            }

            return true;
        }

        #endregion

        /*            _
         *   ___ ___ | |_   _ _ __ ___  _ __
         *  / __/ _ \| | | | | '_ ` _ \| '_ \
         * | (_| (_) | | |_| | | | | | | | | |
         *  \___\___/|_|\__,_|_| |_| |_|_| |_|
         *
         */
        #region Column

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
