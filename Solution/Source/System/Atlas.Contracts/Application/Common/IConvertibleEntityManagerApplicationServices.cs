using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Contracts.Presentation.Transfer;

namespace CompanyName.Atlas.Contracts.Application.Common
{
    /// <summary>
    ///     Contract for the application services managing the CRUD operations coming from upper layers such as Presentation
    ///     regarding to Convertible entities.
    /// </summary>
    public interface IConvertibleEntityManagerApplicationServices<TEntity> : IItemManagerApplicationServices<TEntity>, IExportable<TEntity>
        where TEntity : class, IConvertibleEntity
    {
        void AddFromScratch(string muname, string s);
    }

}
