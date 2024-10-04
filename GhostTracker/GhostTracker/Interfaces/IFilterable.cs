namespace GhostTracker.Interfaces
{
    /// <summary>
    ///     Представляет сущность, которая может быть отфильтрована.
    /// </summary>
    public interface IFilterable
    {
        /// <summary>
        ///     Выполняет фильтрацию текущей сущности на основе определённых критериев или условий.
        /// </summary>
        void Filter();
    }
}