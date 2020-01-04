using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Contracts.Application.Common;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Contracts.Presentation.Prism;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;

namespace CompanyName.Atlas.Contracts.Implementation.Presentation.Data.Common
{
    public abstract  class AtlasGenericModuleAccessPresenter<TEntity,TServices> : NavigableNomenclator<TEntity, TServices>, IAtlasGenericModuleAccessPresenter<TEntity>
        where TEntity : class, IAtlasGenericModuleAccess
        where TServices : class, IAtlasGenericModuleAccessManagerApplicationServices<TEntity>
    {
        private IAtlasModuleRoleViewModel _rols;

        private IAtlasModuleAccessViewModel _ownedAccesses;
        private ICommand _updateUser;
        //private IAtlasCommonModuleAccessPresenter<TOwner> _ownerModuleAccess;
        
        public virtual IEnumerable<IPresenter> Collection { get; set; }
        /// <summary>
        /// Gets the crud view model used to manage the planned resources of the budget component contained in the current
        /// presenter.
        /// </summary>
        public  IAtlasModuleAccessViewModel OwnedAccesses
        {
            get
            {
                if (_ownedAccesses == null)
                {
                    _ownedAccesses = ServiceLocator.Current.GetInstance<IAtlasModuleAccessViewModel>();

                    _ownedAccesses.OwnerModuleAccess = this;
                    _ownedAccesses.Load();

                    _ownedAccesses.Raised += OnInteractionRequested;

                }
                return _ownedAccesses;
            }

        }

        private IAtlasUserPresenter _atlasUser;
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public override string Name
        {
            get
            {
                var userId = Object.User;
                if (userId!=null && (_atlasUser == null || _atlasUser.Id != userId.ToString()))
                {
                    var viewmodel = ServiceLocator.Current.GetInstance<IAtlasUserViewModel>();
                    viewmodel.Load();
                    _atlasUser = viewmodel.Find(userId);
                    if (_atlasUser != null)
                        Object.Name = _atlasUser.Name;

                }
                return _atlasUser!=null? _atlasUser.Name: Object.Name;
            }
            set
            {
                OnPropertyChanged(() => ShortName);
                OnPropertyChanged(() => EvenShortterName);
                OnPropertyChanged(() => LimitedName);
                OnPropertyChanged(() => Name);
            }
        }

        public  String ShortName
        {
            get { return Name != null ? (Name.Length > 20 ? Name.Substring(0, 20) + "..." : Name) : string.Empty; }

        }
        /// <summary>
        ///     Gets the shortened version of the current <see cref="InvestmentElementPresenterBase{T,TServices}" /> name.
        /// </summary>
        public string EvenShortterName
        {
            get { return Name != null ? (Name.Length > 2 ? Name.Substring(0, 2) + "..." : Name) : string.Empty; }
           
        }
        /// <summary>
        ///     Gets the shortened version of the current <see cref="InvestmentElementPresenterBase{T,TServices}" /> name.
        /// </summary>
        public string LimitedName
        {
            get { return Name != null ? (Name.Length > 25 ? Name.Substring(0, 25) + "..." : Name) : string.Empty; }
           
        }
        public object User
        {
            get { return Object.User; }
            set { Object.User = Object.User; }
        }
        /// <summary>
        /// Gets the crud view model used to manage the planned resources of the budget component contained in the current
        /// presenter.
        /// </summary>
        public IAtlasModuleRoleViewModel Rols
        {
            get
            {
                if (_rols == null)
                {
                    _rols = ServiceLocator.Current.GetInstance<IAtlasModuleRoleViewModel>();

                    _rols.OwnerModuleAccess = this;
                    _rols.Load();

                    _rols.Raised += OnInteractionRequested;

                }
                return _rols;
            }

        }
       
        /// <summary>
        ///     Gets the data representing the geometry specification of the icon corresponding to the current
        ///     <see cref = "InvestmentElementPresenter" /> according to its depth.
        /// </summary>
        //[ExcludeFromCodeCoverage]
        //public override string IconData
        //{
        //    get
        //    {

        //        return "F1 M 38,19C 43.5417,19 45.9167,22.1667 45.1174,28.8134C 45.8315,29.2229 46.3125,29.9928 46.3125,30.875C 46.3125,31.9545 45.5923,32.8658 44.6061,33.1546C 44.1941,34.623 43.5543,35.9229 42.75,36.9628L 42.75,41.9583C 45.3889,42.4861 47.5,42.75 50.6667,44.3333C 53.8333,45.9167 54.8889,47.3681 57,49.4792L 57,57L 19,57L 19,49.4792C 21.1111,47.3681 22.1667,45.9167 25.3333,44.3333C 28.5,42.75 30.6111,42.4861 33.25,41.9583L 33.25,36.9628C 32.4457,35.9229 31.8059,34.623 31.3939,33.1546C 30.4077,32.8658 29.6875,31.9545 29.6875,30.875C 29.6875,29.9928 30.1685,29.2229 30.8826,28.8134C 30.0833,22.1667 32.4583,19 38,19 Z ";

        //    }
        //}

        public ICommand UpdateUser
        {
            get
            {
                if (_updateUser == null)
                {
                    _updateUser = new DelegateCommand<IAtlasUserPresenter>(Update, CanUpdate);
                }
                return _updateUser;
            }
        }

        private bool CanUpdate(IAtlasUserPresenter atlasUserPresenter)
        {
            return true;
        }

        private void Update(IAtlasUserPresenter atlasUserPresenter)
        {
            var userId = atlasUserPresenter.Id;
            var username = atlasUserPresenter.Name;
            SetProperty(v => Object.User = v, userId,"User");
            OnPropertyChanged(() => ShortName);
            OnPropertyChanged(() => EvenShortterName);
            OnPropertyChanged(() => LimitedName);
            OnPropertyChanged(() => Name);

        }

        public override ICrudViewModel Items
        {
            get { return OwnedAccesses; }
        }
    }
}
