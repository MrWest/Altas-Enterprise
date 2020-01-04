using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Contracts.Presentation.Transfer;

namespace CompanyName.Atlas.Contracts.Presentation.Common
{
    public interface IAtlasModuleGenericSubjectPresenter<TEntity> : IPresenter<TEntity>, IAtlasModuleGenericSubjectPresenter
        where TEntity:class,IAtlasModuleGenericSubject
    {
    }
    public interface IAtlasModuleGenericSubjectPresenter :  INavigable,IExportable
    {
        IAtlasModuleGenericSubject ModuleSubject { get; set; }
        IAtlasModuleSubjectViewModel Subjects { get; }
        ISubjectConceptViewModel SubjectConcepts { get; }
        IReferenceDocumentViewModel ReferenceDocuments { get; }
    }
}
