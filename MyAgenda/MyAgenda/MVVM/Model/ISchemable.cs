namespace MyAgenda.MVVM.Model
{
    /// <summary>
    /// Интерфейс, позволяющий классу иметь схему таблицы в базе данных
    /// и уникальный идентификатор.
    /// </summary>
    internal interface ISchemable
    {
        // UNDONE: Невозможно использовать из-за ограничений .Net Framework.
        // Обновление до .Net должно решить проблему.
        // public const string IdColumn = "id";

        // UNDONE: Невозможно использовать из-за ограничений .Net Framework.
        // Обновление до .Net должно решить проблему.
        // static IData FromData();

        /// <summary>
        /// Получить схему таблицы с данными.
        /// </summary>
        /// <returns>Схема, заполненная данными.</returns>
        Schema ToData();

        /// <summary>
        /// Доступ к идентификатору.
        /// </summary>
        int Id
        {
            get;
            set;
        }
    }
}
