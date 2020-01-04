using System;
using System.Notifications;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Contracts.Presentation.Transfer;

namespace CompanyName.Atlas.Contracts.Presentation.Common
{
    /// <summary>
    /// represents an instance of a convertible entity for presentation
    /// </summary>
    public interface IConvertibleEntityPresenter<TEntity> : IPresenter<TEntity>, INomenclator,INotifiyer, IExportable
        where TEntity : class ,IConvertibleEntity
    {
        string Id { get; }
        /// <summary>
        /// Letter which identifies the entity for abreviation
        /// </summary>
        String Letters { get; set; }

        IUnitConverterViewModel<TEntity> Convertions { get; }

        decimal ConvertionFactorFor(TEntity entity);
      
    }
}
