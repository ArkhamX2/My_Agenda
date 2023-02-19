using System;

namespace MyAgenda.MVVM.Model
{
    /// <summary>
    /// Ссылка на таблицу.
    /// </summary>
    internal class ReferenceLink
    {
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
        /// <param name="reference">Таблица, на которую ведет ссылка.</param>
        /// <param name="referenceColumnName">Название столбца - внутреннего ключа.</param>
        public ReferenceLink(Column foreignKey, Blueprint reference, string referenceColumnName) : this(foreignKey.Name, reference.Name, referenceColumnName)
        {
            bool found = false;

            foreach (Column item in reference.ColumnList)
            {
                if (item.Name != referenceColumnName)
                {
                    continue;
                }

                found = true;

                break;
            }

            if (!found)
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
    }
}
