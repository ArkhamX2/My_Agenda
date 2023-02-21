using System;

namespace MyAgenda.MVVM.Model
{
    /// <summary>
    /// Столбец типа INT в SQL.
    /// Соответствует int в C#.
    /// </summary>
    internal class IntColumn : Column
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="name">Название.</param>
        public IntColumn(string name) : base(name)
        {
            // PASS.
        }

        /// <summary>
        /// Доступ к данным.
        /// </summary>
        public override object Data
        {
            get => _data;
            set
            {
                if (!IsDataTypeAllowed(value))
                {
                    throw new ArgumentException("Допускается использование данных только целочисленного типа.");
                }

                _data = value;
            }
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
    }

    /// <summary>
    /// Столбец типа VARCHAR в SQL.
    /// Соответствует string в C#.
    /// </summary>
    internal class StringColumn : Column
    {
        /// <summary>
        /// Максимальная длина данных по-умолчанию.
        /// </summary>
        public const int DefaultMaxLength = 255;

        /// <summary>
        /// Максимальная длина данных.
        /// </summary>
        private int _maxLength;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="name">Название.</param>
        public StringColumn(string name, int maxLength = DefaultMaxLength) : base(name)
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
        /// Доступ к данным.
        /// </summary>
        public override object Data
        {
            get => _data;
            set
            {
                if (!IsDataTypeAllowed(value))
                {
                    throw new ArgumentException("Допускается использование данных только строкового типа.");
                }

                _data = value;
            }
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
    }

    /// <summary>
    /// Столбец.
    /// </summary>
    internal abstract class Column : ComparableObject
    {
        /// <summary>
        /// Название.
        /// </summary>
        private string _name;

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
        protected object _data = null;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="name">Название.</param>
        public Column(string name)
        {
            Name = name;
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
        public abstract object Data
        {
            get;
            set;
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
            string result = $"\"{Name}\" {DataTypeAsString()}";

            result += IsNullable ? "" : " NOT NULL";
            result += IsPrimaryKey ? " PRIMARY KEY" : "";
            result += IsAutoIncrementable ? " AUTO_INCREMENT" : "";

            return result;
        }

        #region ComparableObject

        /// <summary>
        /// Обработать проверку образца на сходство с экземпляром.
        /// </summary>
        /// <param name="column">Образец.</param>
        /// <returns>Статус проверки.</returns>
        public bool HandleIsSameAsObject(Column column)
        {
            if (column.IsNullable != IsNullable)
            {
                return false;
            }

            if (column.IsPrimaryKey != IsPrimaryKey)
            {
                return false;
            }

            if (column.IsAutoIncrementable != IsAutoIncrementable)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Обработать проверку образца на полное сходство с экземпляром.
        /// </summary>
        /// <param name="column">Образец.</param>
        /// <returns>Статус проверки.</returns>
        public bool HandleIsExactSameAsObject(Column column)
        {
            if (!HandleIsSameAsObject(column))
            {
                return false;
            }

            if (column.Data != Data)
            {
                return false;
            }

            return true;
        }

        #endregion
    }
}
