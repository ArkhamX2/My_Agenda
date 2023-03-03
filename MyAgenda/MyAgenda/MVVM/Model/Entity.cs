using System;

namespace MyAgenda.MVVM.Model
{
    /// <summary>
    /// Базовая сущность.
    /// Представляет собой объект - строительный блок.
    /// Все объекты, участвующие в построении расписания наследуют этот класс.
    /// </summary>
    internal abstract class Entity
    {
        /// <summary>
        /// Произвести проверку и подготовку входных строковых данных.
        /// Удалить лишние пробелы и привести к нижнему регистру для
        /// унификации.
        /// </summary>
        /// <param name="data">Строка.</param>
        /// <param name="lengthMin">Минимальная длина.</param>
        /// <param name="lengthMax">Максимальная длина.</param>
        /// <returns>Подготовленная строка в нижнем регистре без лишних пробелов.</returns>
        /// <exception cref="ArgumentException"></exception>
        protected string ValidateStringData(string data, int lengthMin, int lengthMax)
        {
            data = data.Trim().ToLower();

            if (data.Length < lengthMin || data.Length > lengthMax)
            {
                throw new ArgumentException("Длина строковых данных не может выходить за установленные пределы.");
            }

            return data;
        }
    }
}
