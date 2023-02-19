using System;
using System.Collections.Generic;

namespace MyAgenda.MVVM.Model
{
    /// <summary>
    /// Схема таблицы.
    /// </summary>
    internal abstract class Blueprint
    {
        /// <summary>
        /// Название таблицы.
        /// </summary>
        private string _name;

        /// <summary>
        /// Список столбцов.
        /// </summary>
        private List<Column> _columnList = new List<Column>();

        /// <summary>
        /// Список ссылок.
        /// </summary>
        private List<ReferenceLink> _referenceList = new List<ReferenceLink>();

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="name">Название.</param>
        /// <param name="columnList">Список столбцов.</param>
        public Blueprint(string name, List<Column> columnList)
        {
            Name = name;
            ColumnList = columnList;
        }

        /// <summary>
        /// Конструктор для таблиц со ссылками.
        /// </summary>
        /// <param name="name">Название.</param>
        /// <param name="columnList">Список столбцов.</param>
        /// <param name="referenceList">Список ссылок.</param>
        public Blueprint(string name, List<Column> columnList, List<ReferenceLink> referenceList) : this(name, columnList)
        {
            ReferenceList = referenceList;
        }

        /// <summary>
        /// Доступ к названию таблицы.
        /// </summary>
        public string Name
        {
            get => _name;
            protected set => _name = value;
        }

        /// <summary>
        /// Доступ к списку столбцов.
        /// </summary>
        public List<Column> ColumnList
        {
            get => _columnList;
            protected set => _columnList = value;
        }

        /// <summary>
        /// Доступ к списку ссылок.
        /// </summary>
        public List<ReferenceLink> ReferenceList
        {
            get => _referenceList;
            protected set
            {
                bool found;

                foreach (ReferenceLink link in value)
                {
                    found = false;

                    foreach (Column column in ColumnList)
                    {
                        if (link.ColumnName == column.Name)
                        {
                            found = true;

                            break;
                        }
                    }

                    if (!found)
                    {
                        throw new ArgumentException("Список ссылок не соответствует списку столбцов.");
                    }
                }

                _referenceList = value;
            }
        }

        /// <summary>
        /// Получить SQL запрос для создания таблицы.
        /// </summary>
        /// <returns>Строка в формате SQL.</returns>
        public string ToCreateQuery()
        {
            string result = $"CREATE TABLE IF NOT EXISTS \"{Name}\" (";

            foreach (Column item in ColumnList)
            {
                result += $"{item}, ";
            }

            foreach (ReferenceLink item in ReferenceList)
            {
                result += $"{item}, ";
            }

            // Удалить последние запятую и пробел,
            // закрыть скобку и вернуть результат.
            return result.Remove(result.Length - 2) + ");";
        }

        /// <summary>
        /// Получить SQL запрос для удаления таблицы.
        /// </summary>
        /// <returns>Строка в формате SQL.</returns>
        public string ToDropQuery()
        {
            return $"DROP TABLE \"{Name}\";";
        }

        /// <summary>
        /// Получить представление в виде строки.
        /// Используется формат SQL.
        /// </summary>
        /// <returns>Строка в формате SQL.</returns>
        public override string ToString()
        {
            return ToCreateQuery();
        }
    }
}
