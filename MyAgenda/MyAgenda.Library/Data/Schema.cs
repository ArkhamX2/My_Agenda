using System;
using System.Collections.Generic;
using System.Linq;
using MyAgenda.Library.Data.Column;

namespace MyAgenda.Library.Data
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
        /// </summary>
        /// <param name="sample">Образец.</param>
        /// <returns>Статус проверки.</returns>
        public override bool IsSameAsObject(ComparableObject sample)
        {
            if (!(sample is Schema schema))
            {
                return false;
            }

            if (schema.Name != Name)
            {
                return false;
            }

            foreach (var item in ColumnList)
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

            // Проверка списков ссылок на соответствие друг другу.
            return ReferenceList.All(linkA => schema.ReferenceList.All(linkA.IsSameAsObject));
        }

        /// <summary>
        /// Проверить экземпляр на сходство с образцом.
        /// </summary>
        /// <param name="sample">Образец.</param>
        /// <returns>Статус проверки.</returns>
        public override bool IsSameAsSample(ComparableObject sample)
        {
            if (!(sample is Schema schema))
            {
                return false;
            }

            if (schema.Name != Name)
            {
                return false;
            }

            foreach (var item in schema.ColumnList)
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

            // Проверка списков ссылок на соответствие друг другу.
            return ReferenceList.All(linkA => schema.ReferenceList.All(linkA.IsSameAsObject));
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
        private List<DataColumn> _columnList = new List<DataColumn>();

        /// <summary>
        /// Список ссылок.
        /// </summary>
        private List<ReferenceLink> _referenceList = new List<ReferenceLink>();

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="name">Название.</param>
        /// <param name="columnList">Список столбцов.</param>
        public Schema(string name, List<DataColumn> columnList)
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
        public Schema(string name, List<DataColumn> columnList, List<ReferenceLink> referenceList) : this(name, columnList)
        {
            ReferenceList = referenceList;
        }

        /// <summary>
        /// Доступ к названию таблицы.
        /// </summary>
        public string Name
        {
            get => _name;
            private set => _name = value;
        }

        /// <summary>
        /// Доступ к списку столбцов.
        /// </summary>
        public List<DataColumn> ColumnList
        {
            get => _columnList;
            private set => _columnList = value;
        }

        /// <summary>
        /// Доступ к списку ссылок.
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        public List<ReferenceLink> ReferenceList
        {
            get => _referenceList;
            protected set
            {
                // Перекрестная проверка переданного списка ссылок
                // и списка столбцов на соответствие.
                if (value.Any(link => ColumnList.All(column => link.ColumnName != column.Name)))
                {
                    throw new ArgumentException("Список ссылок не соответствует списку столбцов.");
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
            return ColumnList.Any(item => item.Name == columnName);
        }

        /// <summary>
        /// Получить столбец по названию.
        /// </summary>
        /// <param name="columnName">Название столбца.</param>
        /// <returns>Столбец.</returns>
        /// <exception cref="ArgumentException"></exception>
        public DataColumn GetColumn(string columnName)
        {
            foreach (var item in ColumnList.Where(item => item.Name == columnName))
            {
                return item;
            }

            throw new ArgumentException("Не найден столбец с указанным названием.");
        }

        /// <summary>
        /// Проверить наличие столбца с указанным названием и данных в в нем.
        /// </summary>
        /// <param name="columnName">Название столбца.</param>
        /// <returns>Статус проверки.</returns>
        public bool HasColumnData(string columnName)
        {
            return HasColumn(columnName) && GetColumn(columnName).HasData();
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
            if (GetColumn(columnName).GetType() != typeof(IntColumn))
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
            if (GetColumn(columnName).GetType() != typeof(IntColumn))
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
            var column = GetColumn(columnName);

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
            var result = $"CREATE TABLE IF NOT EXISTS `{Name}` (";

            result = ColumnList.Aggregate(result, (current, item) => current + $"{item}, ");
            result = ReferenceList.Aggregate(result, (current, item) => current + $"{item}, ");

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
            return $"DROP TABLE `{Name}`;";
        }

        /// <summary>
        /// Получить SQL запрос для обновления данных в таблице.
        /// </summary>
        /// <returns>Строка в формате SQL.</returns>
        public string ToInsertQuery()
        {
            var result = string.Empty;

            // TODO: Пересмотреть Schema.ToInsertQuery().

            result += $"INSERT INTO `{Name}` ({ImplodeInsertColumnNames()})";
            result += $" VALUES({ImplodeInsertColumnValues()})";
            result += $" ON DUPLICATE KEY UPDATE {ImplodeInsertColumnLinks()}";

            return result + ";";
        }

        /// <summary>
        /// Выстроить в SQL запросе для обновления данных
        /// названия столбцов, содержащих данные.
        /// </summary>
        /// <returns>Строка в формате SQL.</returns>
        /// <exception cref="Exception"></exception>
        private string ImplodeInsertColumnNames()
        {
            var result = string.Empty;

            foreach (var column in ColumnList)
            {
                if (!column.HasData())
                {
                    if (!column.IsNullable)
                    {
                        throw new Exception();
                    }

                    continue;
                }

                result += $"`{column.Name}`, ";
            }

            // Удалить последние запятую и пробел и вернуть результат.
            return result.Remove(result.Length - 2);
        }

        /// <summary>
        /// Выстроить в SQL запросе для обновления данных
        /// значения столбцов, содержащих данные.
        /// </summary>
        /// <returns>Строка в формате SQL.</returns>
        /// <exception cref="Exception"></exception>
        private string ImplodeInsertColumnValues()
        {
            var result = string.Empty;

            foreach (var column in ColumnList)
            {
                if (!column.HasData())
                {
                    if (!column.IsNullable)
                    {
                        throw new Exception();
                    }

                    continue;
                }

                result += $"'{column.Data}', ";
            }

            // Удалить последние запятую и пробел и вернуть результат.
            return result.Remove(result.Length - 2);
        }

        /// <summary>
        /// Выстроить в SQL запросе для обновления данных
        /// названия столбцов, содержащих данные, вместе с ссылкой
        /// на привязянное к нему значение.
        /// </summary>
        /// <returns>Строка в формате SQL.</returns>
        /// <exception cref="Exception"></exception>
        private string ImplodeInsertColumnLinks()
        {
            var result = string.Empty;

            foreach (var column in ColumnList)
            {
                if (!column.HasData())
                {
                    if (!column.IsNullable)
                    {
                        throw new Exception();
                    }

                    continue;
                }

                result += $"`{column.Name}` = VALUES(`{column.Name}`), ";
            }

            // Удалить последние запятую и пробел и вернуть результат.
            return result.Remove(result.Length - 2);
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
