using System;

namespace MyAgenda.MVVM.Model
{
    /// <summary>
    /// Инициализируемый сущностью интерфейс.
    /// Используется для обозначения возможности хранения отдельных сущностей
    /// внутри инициализируемых сущностей.
    /// </summary>
    /// <typeparam name="DataEntity">Сущность данных.</typeparam>
    internal interface IInitializable<DataEntity>
    {
        /// <summary>
        /// Проверить статус инициализации.
        /// </summary>
        /// <returns>Статус инициализации.</returns>
        bool IsInitialized();

        /// <summary>
        /// Инициализировать данные.
        /// </summary>
        /// <param name="entity">Сущность данных.</param>
        /// <exception cref="ArgumentException"></exception>
        void Initialize(DataEntity entity);
    }
}
