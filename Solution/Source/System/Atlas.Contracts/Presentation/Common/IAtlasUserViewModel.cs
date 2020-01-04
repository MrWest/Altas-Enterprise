using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;

namespace CompanyName.Atlas.Contracts.Presentation.Common
{
    public interface IAtlasUserViewModel : ICrudViewModel<IAtlasUser, IAtlasUserPresenter>
    {
    }

    public interface IAtlasModuleInfoViewModel : ICrudViewModel<IAtlasModuleInfo, IAtlasModuleInfoPresenter>
    {
        IAtlasUserPresenter AtlasUser { get; set; }
        //

        void AddFromScratch(IAtlasModuleInfo moduleInfo);
    }
}
