using System;

namespace MyAgenda.Library.Data.Column
{
    /// <summary>
    /// Столбец.
    /// Может хранить в себе данные.
    /// </summary>
    internal abstract class DataColumn : ComparableObject
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
        /// Обработать проверку образца на сходство с экземпляром.
        /// На данном этапе точно известно, что в роли экземпляра выступает столбец.
        /// </summary>
        /// <param name="column">Образец.</param>
        /// <returns>Статус проверки.</returns>
        public bool HandleIsSameAsObject(DataColumn column)
        {
            if (column.IsNullable != IsNullable)
            {
                return false;
            }

            if (column.IsPrimaryKey != IsPrimaryKey)
            {
                return false;
            }

            return column.IsAutoIncrementable == IsAutoIncrementable;
        }

        /// <summary>
        /// Обработать проверку образца на полное сходство с экземпляром.
        /// На данном этапе точно известно, что в роли экземпляра выступает столбец.
        /// Помимо сравнивания полей класса также сравниваются хранимые данные.
        /// </summary>
        /// <param name="column">Образец.</param>
        /// <returns>Статус проверки.</returns>
        public bool HandleIsExactSameAsObject(DataColumn column)
        {
            if (!HandleIsSameAsObject(column))
            {
                return false;
            }

            return column.Data == Data;
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
        /// Название.
        /// </summary>
        private string _name;

        /// <summary>
        /// Статус возможности использования данных типа null.
        /// </summary>
        private bool _isNullable = false;

        /// <summary>
        /// Статус первичного ключа.
        /// </summary>
        private bool _isPrimaryKey = false;

        /// <summary>
        /// Статус автоматического инкременирования данных.
        /// </summary>
        private bool _isAutoIncrementable = false;

        /// <summary>
        /// Данные.
        /// </summary>
        private object _data = null;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="name">Название.</param>
        protected DataColumn(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Доступ к названию.
        /// </summary>
        public string Name
        {
            get => _name;
            private set => _name = value;
        }

        /// <summary>
        /// Доступ к статусу возможности использования значения null.
        /// </summary>
        public bool IsNullable
        {
            get => _isNullable;
            set => _isNullable = value;
        }

        /// <summary>
        /// Доступ к статусу первичного ключа.
        /// </summary>
        public bool IsPrimaryKey
        {
            get => _isPrimaryKey;
            set => _isPrimaryKey = value;
        }

        /// <summary>
        /// Доступ к статусу автоматического инкременирования значения.
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        public bool IsAutoIncrementable
        {
            get => _isAutoIncrementable;
            set
            {
                if (!IsDataTypeAutoIncrementable())
                {
                    throw new InvalidOperationException("Тип данных невозможно автоматически инкременировать.");
                }

                _isAutoIncrementable = value;
            }
        }

        /// <summary>
        /// Доступ к данным.
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        public object Data
        {
            get => _data;
            set
            {
                if (!IsDataTypeAllowed(value))
                {
                    throw new ArgumentException("Несоответствие типов данных.");
                }

                _data = value;
            }
        }

        /// <summary>
        /// Проверить наличие данных.
        /// </summary>
        /// <returns>Статус проверки.</returns>
        public bool HasData()
        {
            return Data != null;
        }

        /// <summary>
        /// Проверить корректность типа данных.
        /// </summary>
        /// <param name="data">Данные.</param>
        /// <returns>Статус проверки.</returns>
        public abstract bool IsDataTypeAllowed(object data);

        /// <summary>
        /// Проверить тип данных на возможность автоматического инкременирования.
        /// </summary>
        /// <returns>Статус проверки.</returns>
        public abstract bool IsDataTypeAutoIncrementable();

        /// <summary>
        /// Получить представление типа данных в виде строки.
        /// </summary>
        /// <returns>Строка в формате SQL.</returns>
        public abstract string DataTypeAsString();

        /// <summary>
        /// Получить представление в виде строки.
        /// Используется формат SQL.
        /// </summary>
        /// <returns>Строка в формате SQL.</returns>
        public override string ToString()
        {
            var result = $"\"{Name}\" {DataTypeAsString()}";

            result += IsNullable ? "" : " NOT NULL";
            result += IsPrimaryKey ? " PRIMARY KEY" : "";
            result += IsAutoIncrementable ? " AUTO_INCREMENT" : "";

            return result;
        }

        #endregion
    }
}
