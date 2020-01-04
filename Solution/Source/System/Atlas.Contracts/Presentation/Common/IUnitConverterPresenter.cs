using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;

namespace CompanyName.Atlas.Contracts.Presentation.Common
{
    public interface IUnitConverterPresenter<TEntity>: IPresenter<IUnitConverter>
         where TEntity : class ,IConvertibleEntity
    {
        IConvertibleEntityPresenter<TEntity> ConversionForEntity { get; set; }

        IConvertibleEntityPresenter<TEntity> ConversionUnit { get; set; }
        decimal Factor { get; set; }
    }
}
