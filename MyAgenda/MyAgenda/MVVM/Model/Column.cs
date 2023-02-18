using System;
using System.ComponentModel;

namespace MyAgenda.MVVM.Model
{
    /// <summary>
    /// Совместимые типы данных для столбца.
    /// TODO: Добавить больше?
    /// </summary>
    internal enum ColumnDataType
    {
        /// <summary>
        /// Некорректный тип данных.
        /// </summary>
        Incorrect,

        /// <summary>
        /// Тип данных INT в SQL.
        /// Соответствует int в C#.
        /// </summary>
        Int,

        /// <summary>
        /// Тип данных VARCHAR в SQL.
        /// Соответствует string в C#.
        /// </summary>
        Varchar
    }

    /// <summary>
    /// Столбец.
    /// </summary>
    /// <typeparam name="T">Тип данных. Допускаются в соответствии с совместимыми типами: int, string.</typeparam>
    internal class Column<T>
    {
        /// <summary>
        /// Максимальная длина данных по-умолчанию.
        /// </summary>
        public const int DefaultMaxLength = 255;

        /// <summary>
        /// Название.
        /// </summary>
        private string _name;

        /// <summary>
        /// Совместимый тип данных.
        /// </summary>
        private ColumnDataType _dataType = GetDataType();

        /// <summary>
        /// Максимальная длина данных.
        /// </summary>
        private int _maxLength = DefaultMaxLength;

        /// <summary>
        /// Статус возможности использования значения null.
        /// </summary>
        private bool _isNullable = false;

        /// <summary>
        /// Статус первичного ключа.
        /// </summary>
        private bool _isPrimaryKey = false;

        /// <summary>
        /// Статус автоматического инкременирования значения.
        /// </summary>
        private bool _isAutoIncrementable = false;

        /// <summary>
        /// Данные.
        /// </summary>
        private T _data = default;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="name">Название.</param>
        /// <exception cref="ArgumentException"></exception>
        public Column(string name)
        {
            Name = name;

            if (!IsDataTypeAllowed(DataType))
            {
                throw new ArgumentException("Родовой параметр класса не совместим с типами данных столбца.");
            }
        }

        /// <summary>
        /// Доступ к названию.
        /// </summary>
        public string Name
        {
            get => _name;
            set => _name = value;
        }

        /// <summary>
        /// Доступ к совместимому типу данных.
        /// </summary>
        public ColumnDataType DataType
        {
            get => _dataType;
            set => _dataType = value;
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
        [RefreshProperties(RefreshProperties.All)]
        public bool IsAutoIncrementable
        {
            get => _isAutoIncrementable;
            set
            {
                // TODO: Если данные по-умолчанию null?

                if (!IsDataTypeAutoIncrementable(DataType))
                {
                    throw new ArgumentException("Автоматическое инкременирование невозможно с указанным типом данных.");
                }

                _isAutoIncrementable = value;
            }
        }

        /// <summary>
        /// Доступ к данным.
        /// </summary>
        public T Data
        {
            get => _data;
            set => _data = value;
        }

        /// <summary>
        /// Определить совместимый тип данных на основании родового параметра.
        /// </summary>
        /// <returns>Совместимый тип данных.</returns>
        private static ColumnDataType GetDataType()
        {
            if (typeof(T) == typeof(int))
            {
                return ColumnDataType.Int;
            }

            if (typeof(T) == typeof(string))
            {
                return ColumnDataType.Varchar;
            }

            return ColumnDataType.Incorrect;
        }

        /// <summary>
        /// Проверить тип данных на совместимость.
        /// </summary>
        /// <param name="dataType">Тип данных.</param>
        /// <returns>Статус проверки.</returns>
        private static bool IsDataTypeAllowed(ColumnDataType dataType)
        {
            return dataType != ColumnDataType.Incorrect;
        }

        /// <summary>
        /// Проверить тип на возможность автоматического инкременирования.
        /// </summary>
        /// <param name="dataType">Тип данных.</param>
        /// <returns>Статус проверки.</returns>
        private static bool IsDataTypeAutoIncrementable(ColumnDataType dataType)
        {
            return dataType == ColumnDataType.Int;
        }

        /// <summary>
        /// Получить представление типа данных в виде строки.
        /// </summary>
        /// <returns>Строка.</returns>
        /// <exception cref="ArgumentException"></exception>
        public string DataTypeToString()
        {
            if (DataType == ColumnDataType.Incorrect)
            {
                throw new ArgumentException("Использование указанного типа данных не допускается.");
            }

            if (DataType == ColumnDataType.Int)
            {
                return "INT";
            }

            return $"VARCHAR({MaxLength})";
        }

        /// <summary>
        /// Получить представление в виде строки.
        /// Используется формат SQL.
        /// </summary>
        /// <returns>Строка в формате SQL.</returns>
        public override string ToString()
        {
            string result = $"\"{Name}\" {DataTypeToString()}";

            result += IsNullable ? "" : " NOT NULL";
            result += IsPrimaryKey ? " PRIMARY KEY" : "";
            result += IsAutoIncrementable ? " AUTO_INCREMENT" : "";

            return result;
        }
    }
}
