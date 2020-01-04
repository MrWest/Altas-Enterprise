using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;

namespace CompanyName.Atlas.Contracts.Presentation.Common
{
    /// <summary>
    ///     Contract to be implemented by the Convertible Entity crud view models, these view models
    ///     will handle the CRUD operations for Convertible entities
    ///     domain entities in the UI (presentation layer).
    /// </summary>
    public interface IConvertibleEntityViewModel<TEntity> : ICrudViewModel<TEntity, IConvertibleEntityPresenter<TEntity>>
        where TEntity:class ,IConvertibleEntity
    {
    }
    /// <summary>
    ///     Contract to be implemented by the Convertible Entity crud view models, these view models
    ///     will handle the CRUD operations for Convertible entities
    ///     domain entities in the UI (presentation layer).
    /// </summary>
    public interface IConvertibleEntityViewModel<TEntity,TPresenter> : ICrudViewModel<TEntity, TPresenter>
        where TEntity : class ,IConvertibleEntity
        where TPresenter : class ,IConvertibleEntityPresenter<TEntity>
       
    {
        void AddFromScratch(string muname, string letters);
        IConvertibleEntityPresenter<TEntity> GetConvertible(string letters);
        bool ExistsConvertible(string letters);
    }
}
