using System;
using System.Collections.Generic;

namespace MyAgenda.MVVM.Model
{
    /// <summary>
    /// Схема таблицы. Служит одновременно и как описание таблицы
    /// в базе данных, и как контейнер для транспортировки данных
    /// из провайдера в сущности.
    /// </summary>
    internal class Schema : ComparableObject
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
        /// TODO: Декомпозировать Schema.IsSameAsObject().
        /// </summary>
        /// <param name="sample">Образец.</param>
        /// <returns>Статус проверки.</returns>
        public override bool IsSameAsObject(ComparableObject sample)
        {
            if (sample.GetType() != GetType())
            {
                return false;
            }

            Schema schema = sample as Schema;

            if (schema.Name != Name)
            {
                return false;
            }

            foreach (Column item in ColumnList)
            {
                if (!schema.HasColumn(item.Name))
                {
                    return false;
                }

                if (!item.IsSameAsObject(schema.GetColumn(item.Name)))
                {
                    return false;
                }
            }

            foreach (ReferenceLink linkA in ReferenceList)
            {
                bool found = false;

                foreach (ReferenceLink linkB in schema.ReferenceList)
                {
                    if (!linkA.IsSameAsObject(linkB))
                    {
                        found = true;

                        break;
                    }
                }

                if (!found)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Проверить экземпляр на сходство с образцом.
        /// TODO: Декомпозировать Schema.IsSameAsSample().
        /// </summary>
        /// <param name="sample">Образец.</param>
        /// <returns>Статус проверки.</returns>
        public override bool IsSameAsSample(ComparableObject sample)
        {
            if (sample.GetType() != GetType())
            {
                return false;
            }

            Schema schema = sample as Schema;

            if (schema.Name != Name)
            {
                return false;
            }

            foreach (Column item in schema.ColumnList)
            {
                if (!HasColumn(item.Name))
                {
                    return false;
                }

                if (!item.IsSameAsObject(GetColumn(item.Name)))
                {
                    return false;
                }
            }

            foreach (ReferenceLink linkA in schema.ReferenceList)
            {
                bool found = false;

                foreach (ReferenceLink linkB in ReferenceList)
                {
                    if (!linkA.IsSameAsObject(linkB))
                    {
                        found = true;

                        break;
                    }
                }

                if (!found)
                {
                    return false;
                }
            }

            return true;
        }

        #endregion

        /*           _
         *  ___  ___| |__   ___ _ __ ___   __ _
         * / __|/ __| '_ \ / _ \ '_ ` _ \ / _` |
         * \__ \ (__| | | |  __/ | | | | | (_| |
         * |___/\___|_| |_|\___|_| |_| |_|\__,_|
         *
         */
        #region Schema

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
        public Schema(string name, List<Column> columnList)
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
        public Schema(string name, List<Column> columnList, List<ReferenceLink> referenceList) : this(name, columnList)
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

                // Перекрестная проверка переданного списка ссылок
                // и списка столбцов на соответствие.
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
        /// Проверить наличие столбца с указанным названием.
        /// </summary>
        /// <param name="columnName">Название столбца.</param>
        /// <returns>Статус проверки.</returns>
        public bool HasColumn(string columnName)
        {
            foreach (Column item in ColumnList)
            {
                if (item.Name == columnName)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Получить столбец по названию.
        /// </summary>
        /// <param name="columnName">Название столбца.</param>
        /// <returns>Столбец.</returns>
        /// <exception cref="ArgumentException"></exception>
        public Column GetColumn(string columnName)
        {
            foreach (Column item in ColumnList)
            {
                if (item.Name == columnName)
                {
                    return item;
                }
            }

            throw new ArgumentException("Не найден столбец с указанным названием.");
        }

        /// <summary>
        /// Проверить наличие данных в столбце с указанным названием.
        /// </summary>
        /// <param name="columnName">Название столбца.</param>
        /// <returns>Статус проверки.</returns>
        /// <exception cref="ArgumentException"></exception>
        public bool HasColumnData(string columnName)
        {
            return GetColumn(columnName).HasData();
        }

        /// <summary>
        /// Получить данные из столбца.
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public object GetColumnData(string columnName)
        {
            if (!HasColumnData(columnName))
            {
                throw new ArgumentException("Столбец с указанным названием не содержит данных.");
            }

            return GetColumn(columnName).Data;
        }

        /// <summary>
        /// Получить данные из столбца типа INT.
        /// </summary>
        /// <param name="columnName">Название столбца.</param>
        /// <returns>Данные.</returns>
        /// <exception cref="ArgumentException"></exception>
        public int GetIntColumnData(string columnName)
        {
            Column column = GetColumn(columnName);

            if (column.GetType() != typeof(IntColumn))
            {
                throw new ArgumentException("Столбец с указанным названием не предназначен для работы с целочисленным типом данных.");
            }

            return (int)GetColumnData(columnName);
        }

        /// <summary>
        /// Получить данные из столбца типа VARCHAR.
        /// </summary>
        /// <param name="columnName">Название столбца.</param>
        /// <returns>Данные.</returns>
        /// <exception cref="ArgumentException"></exception>
        public string GetStringColumnData(string columnName)
        {
            Column column = GetColumn(columnName);

            if (column.GetType() != typeof(IntColumn))
            {
                throw new ArgumentException("Столбец с указанным названием не предназначен для работы со строковым типом данных.");
            }

            return (string)GetColumnData(columnName);
        }

        /// <summary>
        /// Поместить данные в столбец с указанным названием.
        /// </summary>
        /// <param name="columnName">Название столбца.</param>
        /// <param name="data">Данные.</param>
        /// <exception cref="ArgumentException"></exception>
        public void SetColumnData(string columnName, object data)
        {
            Column column = GetColumn(columnName);

            if (!column.IsDataTypeAllowed(data))
            {
                throw new ArgumentException("Столбец с указанным названием не предназначен для работы с указанным типом данных.");
            }

            column.Data = data;
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

        #endregion
    }
}
