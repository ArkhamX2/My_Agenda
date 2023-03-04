namespace MyAgenda.Library.Model
{
    /// <summary>
    /// Интерфейс, позволяющий классу иметь схему таблицы в базе данных
    /// и уникальный идентификатор.
    /// </summary>
    public interface ISchemable
    {
        // UNDONE: Невозможно использовать из-за ограничений .Net Framework.
        // Обновление до .Net должно решить проблему.
        // public int Id { get; protected set; }

        /// <summary>
        /// Доступ к идетификатору.
        /// </summary>
        int Id { get; }

        // UNDONE: Невозможно использовать из-за ограничений .Net Framework.
        // Обновление до .Net должно решить проблему.
        // internal static Schema Schema { get; }

        // UNDONE: Невозможно использовать из-за ограничений .Net Framework.
        // Обновление до .Net должно решить проблему.
        // internal static ISchemable FromData(Schema data);

        // UNDONE: Невозможно использовать из-за ограничений .Net Framework.
        // Обновление до .Net должно решить проблему.
        // internal Schema ToData();
    }
}
