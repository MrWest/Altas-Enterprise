using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Application.Common;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Common;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Contracts.Implementation.Presentation.Data.Common
{
    public class ConvertibleEntityProvider<TEntity> : ConvertibleEntityViewModel<TEntity, IConvertibleEntityPresenter<TEntity>, IConvertibleEntityManagerApplicationServices<TEntity>>, IConvertibleEntityProvider<TEntity>
        where TEntity : class,IConvertibleEntity
    {

        private static IConvertibleEntityProvider<TEntity> _convertibleEntityProvider;
        /// <summary>
        ///     Gets the <see cref="IEnumerable{T}" /> containing the presenter view models (<see cref="IWageScalePresenter" />)
        ///     containing the <see cref="IWageScale" /> domain entities in the system.
        /// </summary>
        public static IEnumerable<IConvertibleEntityPresenter<TEntity>> ConvertibleEntities
        {
            get
            {
                if (_convertibleEntityProvider == null)
                    _convertibleEntityProvider = ServiceLocator.Current.GetInstance<IConvertibleEntityProvider<TEntity>>();

                return _convertibleEntityProvider.ConvertibleEntities;
            }
        }
        /// <summary>
        ///     Gets the <see cref="IEnumerable{T}" /> containing the presenter view models (<see cref="IWageScalePresenter" />)
        ///     containing the <see cref="IWageScale" /> domain entities in the system.
        /// </summary>
         IEnumerable<IConvertibleEntityPresenter<TEntity>> IConvertibleEntityProvider<TEntity>.ConvertibleEntities
        {
            get
            {
                Load();
                return Items;
            }
        }

    }
}
