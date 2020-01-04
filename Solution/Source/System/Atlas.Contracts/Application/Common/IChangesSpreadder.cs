using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Contracts.Application.Common
{
    public interface IChangesSpreadder<TEntity>
        where TEntity:class ,IEntity
    {
        void SpreadChanges(TEntity toSpread);
    }
}