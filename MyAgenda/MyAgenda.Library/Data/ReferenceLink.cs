using System;
using System.Linq;
using MyAgenda.Library.Data.Column;

namespace MyAgenda.Library.Data
{
    /// <summary>
    /// Ссылка на таблицу.
    /// </summary>
    internal class ReferenceLink : ComparableObject
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
            if (!(sample is ReferenceLink link))
            {
                return false;
            }

            if (link.ColumnName != ColumnName)
            {
                return false;
            }

            if (link.ReferenceTableName != ReferenceTableName)
            {
                return false;
            }

            return link.ReferenceColumnName == ReferenceColumnName;
        }

        #endregion

        /*            __                                _ _       _
         *  _ __ ___ / _| ___ _ __ ___ _ __   ___ ___  | (_)_ __ | | __
         * | '__/ _ \ |_ / _ \ '__/ _ \ '_ \ / __/ _ \ | | | '_ \| |/ /
         * | | |  __/  _|  __/ | |  __/ | | | (_|  __/ | | | | | |   <
         * |_|  \___|_|  \___|_|  \___|_| |_|\___\___| |_|_|_| |_|_|\_\
         *
         */
        #region ReferenceLink

        /// <summary>
        /// Столбец - внешний ключ.
        /// </summary>
        private string _columnName;

        /// <summary>
        /// Название таблицы, на которую ведет ссылка.
        /// </summary>
        private string _referenceTableName;

        /// <summary>
        /// Название столбца - внутреннего ключа, на который ведет ссылка.
        /// </summary>
        private string _referenceColumnName;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="columnName">Внешний ключ.</param>
        /// <param name="referenceTableName">Название таблицы, на которую ведет ссылка.</param>
        /// <param name="referenceColumnName">Название столбца - внутреннего ключа.</param>
        public ReferenceLink(string columnName, string referenceTableName, string referenceColumnName)
        {
            ColumnName = columnName;
            ReferenceTableName = referenceTableName;
            ReferenceColumnName = referenceColumnName;
        }

        /// <summary>
        /// Конструктор из объектов.
        /// </summary>
        /// <param name="foreignKey">Внешний ключ.</param>
        /// <param name="reference">Схема таблицы, на которую ведет ссылка.</param>
        /// <param name="referenceColumnName">Название столбца - внутреннего ключа.</param>
        public ReferenceLink(DataColumn foreignKey, Schema reference, string referenceColumnName) : this(foreignKey.Name, reference.Name, referenceColumnName)
        {
            // Поиск в переданной схеме указанного внутреннего ключа.
            if (reference.ColumnList.All(item => item.Name != referenceColumnName))
            {
                throw new ArgumentException("В указанной таблице не найден указанный внутренний ключ.");
            }
        }

        /// <summary>
        /// Доступ к столбцу - внешнему ключу.
        /// </summary>
        public string ColumnName
        {
            get => _columnName;
            private set => _columnName = value;
        }

        /// <summary>
        /// Доступ к названию таблицы, на которую ведет ссылка.
        /// </summary>
        public string ReferenceTableName
        {
            get => _referenceTableName;
            private set => _referenceTableName = value;
        }

        /// <summary>
        /// Доступ к названию столбца - внутреннего ключа.
        /// </summary>
        public string ReferenceColumnName
        {
            get => _referenceColumnName;
            private set => _referenceColumnName = value;
        }

        /// <summary>
        /// Получить представление в виде строки.
        /// Используется формат SQL.
        /// </summary>
        /// <returns>Строка в формате SQL.</returns>
        public override string ToString()
        {
            return $"FOREIGN KEY (\"{ColumnName}\") REFERENCES \"{ReferenceTableName}\" (\"{ReferenceColumnName}\")";
        }

        #endregion
    }
}
