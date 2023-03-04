namespace MyAgenda.Library.Entity
{
    /// <summary>
    /// Интерфейс, позволяющий классу унаследовать <see cref="ISchemable"/>
    /// через косвенного родителя, в котором уже заложена необходимая
    /// логика. Таким образом класс становится участником общения программы
    /// и базы данных, не принуждая к этому своих непосредственных родителей.
    /// </summary>
    public interface IIndirectlySchemable : ISchemable
    {
        /// <summary>
        /// Доступ к косвенному родителю со схемой таблицы.
        /// </summary>
        ISchemable Schemable
        {
            get;
        }
    }
}
