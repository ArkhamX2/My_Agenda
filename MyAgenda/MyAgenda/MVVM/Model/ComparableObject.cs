namespace MyAgenda.MVVM.Model
{
    /// <summary>
    /// Класс для сравнения экземпляров различными способами.
    /// </summary>
    internal abstract class ComparableObject
    {
        /// <summary>
        /// Проверить образец на сходство с экземпляром.
        /// </summary>
        /// <param name="sample">Образец.</param>
        /// <returns>Статус проверки.</returns>
        public virtual bool IsSameAsObject(ComparableObject sample)
        {
            // Переопредели меня.

            return sample.Equals(this);
        }

        /// <summary>
        /// Проверить образец на полное сходство с экземпляром.
        /// </summary>
        /// <param name="sample">Образец.</param>
        /// <returns>Статус проверки.</returns>
        public virtual bool IsExactSameAsObject(ComparableObject sample)
        {
            // Переопредели меня.

            return IsSameAsObject(sample);
        }

        /// <summary>
        /// Проверить экземпляр на сходство с образцом.
        /// </summary>
        /// <param name="sample">Образец.</param>
        /// <returns>Статус проверки.</returns>
        public virtual bool IsSameAsSample(ComparableObject sample)
        {
            // Переопредели меня.

            return Equals(sample);
        }

        /// <summary>
        /// Проверить экземпляр на полное сходство с образцом.
        /// </summary>
        /// <param name="sample">Образец.</param>
        /// <returns>Статус проверки.</returns>
        public virtual bool IsExactSameAsSample(ComparableObject sample)
        {
            // Переопредели меня.

            return IsSameAsSample(sample);
        }
    }
}
