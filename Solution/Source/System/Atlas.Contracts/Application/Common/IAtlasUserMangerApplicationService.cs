using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Contracts.Application.Common
{
    /// <summary>
    /// for managin <see cref="IAtlasUser"/>
    /// </summary>
    public interface IAtlasUserMangerApplicationService:IItemManagerApplicationServices<IAtlasUser>
    {
    }

    /// <summary>
    /// for managin <see cref="IAtlasUser"/>
    /// </summary>
    public interface IAtlasModuleInfoMangerApplicationService : IItemManagerApplicationServices<IAtlasModuleInfo>
    {
        IAtlasUser AtlasUser { get; set; }
    }
}
