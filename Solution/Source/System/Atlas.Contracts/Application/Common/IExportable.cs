using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Infrastructure.Data;

namespace CompanyName.Atlas.Contracts.Application.Common
{
    public interface IExportable<TExportable>
        where TExportable:class,IEntity
    {
        TExportable Export(IDatabaseContext databaseContext, TExportable exportable);
    }
}