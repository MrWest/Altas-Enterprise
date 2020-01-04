using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;

namespace CompanyName.Atlas.Contracts.Presentation.Common
{
    public interface IUnitConverterViewModel<TEntity> : ICrudViewModel<IUnitConverter,IUnitConverterPresenter<TEntity>>
         where TEntity : class ,IConvertibleEntity
    {
        IConvertibleEntityPresenter<TEntity> ConvertibleEntityPresenter { get; set; }
    }
}
