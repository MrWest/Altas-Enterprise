using System;
using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Contracts.Implementation.Presentation.Data
{
    /// <summary>
    /// Represents the base class of the presenter view models to decorate nomenclator entities.
    /// </summary>
    /// <typeparam name="T">The type of the nomenclator entity to decorate.</typeparam>
    /// <typeparam name="TServices">The type of the application services this presenter view model uses to update the changes.</typeparam>
    public abstract class NomenclatorPresenterBase<T, TServices> : EntityPresenterBase<T, TServices>, INomenclator
        where T : class, INomenclator
        where TServices : IItemManagerApplicationServices<T>
    {
        /// <summary>
        /// Initializes a new instance of a presenter view model to decorate a nomenclator entity without such entity.
        /// </summary>
        protected NomenclatorPresenterBase()
        {
        }

        /// <summary>
        /// Initializes a new instance of a presenter view model to decorate a nomenclator entity given such entity.
        /// </summary>
        /// <param name="nomenclator">The nomenclator entity to be decorated.</param>
        /// <exception cref="ArgumentNullException"><paramref name="nomenclator"/> is null.</exception>
        protected NomenclatorPresenterBase(T nomenclator)
            : base(nomenclator)
        {
        }


        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public virtual string Name
        {
            get { return Object.Name; }
            set
            {
                SetProperty(v => Object.Name = v, value);
                OnPropertyChanged(() => Name);
            }
        }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public virtual string Description
        {
            get { return Object.Description; }
            set
            {
                SetProperty(v => Object.Description = v, value);
                OnPropertyChanged(() => Description);
            }
        }
    }
}
