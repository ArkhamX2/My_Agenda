namespace MyAgenda.MVVM.Model
{
    /// <summary>
    /// Столбец типа INT в SQL.
    /// Соответствует int в C#.
    /// Может хранить в себе данные.
    /// </summary>
    internal class IntColumn : Column
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

            if (!HandleIsSameAsObject(sample as IntColumn))
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
        /// Конструктор.
        /// </summary>
        /// <param name="name">Название.</param>
        public IntColumn(string name) : base(name)
        {
            // PASS.
        }

        /// <summary>
        /// Проверить корректность типа данных.
        /// </summary>
        /// <param name="data">Данные.</param>
        /// <returns>Статус проверки.</returns>
        public override bool IsDataTypeAllowed(object data)
        {
            return data is int;
        }

        /// <summary>
        /// Проверить тип данных на возможность автоматического инкременирования.
        /// </summary>
        /// <returns>Статус проверки.</returns>
        public override bool IsDataTypeAutoIncrementable()
        {
            return true;
        }

        /// <summary>
        /// Получить представление типа данных в виде строки.
        /// </summary>
        /// <returns>Строка в формате SQL.</returns>
        public override string DataTypeAsString()
        {
            return "INT";
        }

        #endregion
    }
}
