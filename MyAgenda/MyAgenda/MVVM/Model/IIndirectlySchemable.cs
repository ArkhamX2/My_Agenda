namespace MyAgenda.MVVM.Model
{
    /// <summary>
    /// Интерфейс, позволяющий классу унаследовать <see cref="ISchemable"/>
    /// через косвенного родителя, в котором уже заложена необходимая
    /// логика. Таким образом класс становится участником общения программы
    /// и базы данных, не принуждая к этому своих непосредственных родителей.
    /// </summary>
    internal interface IIndirectlySchemable : ISchemable
    {
        /// <summary>
        /// Доступ к косвенному родителю.
        /// </summary>
        ISchemable Parent
        {
            get;
        }
    }
}
