using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CompanyName.Atlas.Contracts.Application.Common;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;
using Microsoft.Practices.ServiceLocation;
using System.Diagnostics.CodeAnalysis;
using CompanyName.Atlas.Contracts.Infrastructure.Data;

namespace CompanyName.Atlas.Contracts.Implementation.Presentation.Data.Common
{
    public abstract class AtlasModuleGenericSubjectPresenter<TEntity, TService> : NavigableNomenclator<TEntity, TService>, IAtlasModuleGenericSubjectPresenter<TEntity>
        where TEntity : class,IAtlasModuleGenericSubject
        where TService : class,IAtlasModuleGenericSubjectManagerApplicationServices<TEntity>
    {
        private IAtlasModuleSubjectViewModel _subjects;
        private ISubjectConceptViewModel _subjectsConcepts;
        private IReferenceDocumentViewModel _referenceDocumentViewModel;
        public IAtlasModuleGenericSubject ModuleSubject { get { return Object; } set { Object = value as TEntity; } }
        public IAtlasModuleSubjectViewModel Subjects
        {
            get
            {
                if (_subjects == null)
                {
                    _subjects = ServiceLocator.Current.GetInstance<IAtlasModuleSubjectViewModel>();
                    _subjects.OwnerSubject = this;
                    _subjects.Load();
                    _subjects.Raised += OnInteractionRequested;
                    

                }
                return _subjects;
            }
        }

        public IReferenceDocumentViewModel ReferenceDocuments
        {
            get
            {
                if (_referenceDocumentViewModel == null)
                {
                    _referenceDocumentViewModel = ServiceLocator.Current.GetInstance<IReferenceDocumentViewModel>();
                    _referenceDocumentViewModel.Holder = this;
                    _referenceDocumentViewModel.Load();
                    _referenceDocumentViewModel.Raised += OnInteractionRequested;


                }
                return _referenceDocumentViewModel;
            }
        }
        public override ICrudViewModel Items { get { return Subjects; }}

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public override string Name
        {
            get { return Object.Name; }
            set
            {
                SetProperty(v => Object.Name = v, value);
                OnPropertyChanged(() => Name);
                OnPropertyChanged(() => ShortName);
                OnPropertyChanged(() => EvenShortterName);
                OnPropertyChanged(() => LimitedName);
            }
        }
        /// <summary>
        ///     Gets the shortened version of the current <see cref="InvestmentElementPresenterBase{T,TServices}" /> name.
        /// </summary>
        public string ShortName
        {
            get { return Name != null ? (Name.Length > 20 ? Name.Substring(0, 20) + "..." : Name) : string.Empty; }
            set
            {
                SetProperty(v => Object.Name = v, value);
                OnPropertyChanged(() => Name);
                OnPropertyChanged(() => EvenShortterName);
                OnPropertyChanged(() => LimitedName);
                OnPropertyChanged(() => ShortName);
            }
        }
        /// <summary>
        ///     Gets the shortened version of the current <see cref="InvestmentElementPresenterBase{T,TServices}" /> name.
        /// </summary>
        public string EvenShortterName
        {
            get { return Name != null ? (Name.Length > 2 ? Name.Substring(0, 2) + "..." : Name) : string.Empty; }
            set
            {
                SetProperty(v => Object.Name = v, value);
                OnPropertyChanged(() => Name);
                OnPropertyChanged(() => ShortName);
                OnPropertyChanged(() => LimitedName);
                OnPropertyChanged(() => EvenShortterName);
            }
        }
        /// <summary>
        ///     Gets the shortened version of the current <see cref="InvestmentElementPresenterBase{T,TServices}" /> name.
        /// </summary>
        public string LimitedName
        {
            get { return Name != null ? (Name.Length > 25 ? Name.Substring(0, 25) + "..." : Name) : string.Empty; }
            set
            {
                SetProperty(v => Object.Name = v, value);
                OnPropertyChanged(() => Name);
                OnPropertyChanged(() => ShortName);
                OnPropertyChanged(() => EvenShortterName);
                OnPropertyChanged(() => LimitedName);
            }
        }

        public ISubjectConceptViewModel SubjectConcepts
        {
            get
            {
                if (_subjectsConcepts == null)
                {
                    _subjectsConcepts = ServiceLocator.Current.GetInstance<ISubjectConceptViewModel>();
                    _subjectsConcepts.ModuleSubject = this;
                    _subjectsConcepts.Load();
                    _subjectsConcepts.Raised += OnInteractionRequested;


                }
                return _subjectsConcepts;
            }
        }

        ///// <summary>
        /////     Gets the data representing the geometry specification of the icon corresponding to the current
        /////     <see cref="InvestmentElementPresenter" /> according to its depth.
        ///// </summary>
        //[ExcludeFromCodeCoverage]
        //public override string IconData
        //{
        //    get
        //    {
        //        switch (Depth)
        //        {
        //            case 0:
        //                return "F1 M 25,27L 46,19L 46,22.25L 28.5,29L 31.75,31.25L 51,23.75L 51,48.5L 31.75,57L 25,52L 25,27 Z ";
        //            default:
        //                return "F1 M 24,19L 40,19L 40,31L 52,31L 52,57L 24,57L 24,19 Z M 41,30L 41,19L 52,30L 41,30 Z ";
                    
        //        }
        //    }
        //}

        public bool Export(IDatabaseContext databaseContext)
        {
            if (databaseContext == null)
                return false;
            return CreateServices().Export(databaseContext, Object) != null;

        }
    }
}
