namespace MyAgenda.MVVM.Model
{
    /// <summary>
    /// Интерфейс, позволяющий классу иметь схему таблицы в базе данных
    /// и уникальный идентификатор.
    /// </summary>
    internal interface ISchemable
    {
        /// <summary>
        /// Доступ к идентификатору.
        /// </summary>
        int Id
        {
            get;
            set;
        }

        // UNDONE: Невозможно использовать из-за ограничений .Net Framework.
        // Обновление до .Net должно решить проблему.
        // public static Schema Schema { get; }

        // UNDONE: Невозможно использовать из-за ограничений .Net Framework.
        // Обновление до .Net должно решить проблему.
        // public static ISchemable FromData(Schema data);

        /// <summary>
        /// Получить схему таблицы с данными.
        /// </summary>
        /// <returns>Схема, заполненная данными.</returns>
        Schema ToData();
    }
}
