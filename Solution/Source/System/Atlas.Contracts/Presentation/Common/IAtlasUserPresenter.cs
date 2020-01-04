using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Domain.Entities;
using CompanyName.Atlas.Contracts.Presentation.Data;
using Microsoft.Practices.Prism.Modularity;

namespace CompanyName.Atlas.Contracts.Presentation.Common
{
    public interface IAtlasUserPresenter : IPresenter<IAtlasUser>,INomenclator
    {
        string Password { get; set; }
        AtlasUserRol Rol { get; set; }
        IAtlasModuleInfoViewModel AllowedModules { get; } 
    }

    public interface IAtlasModuleInfoPresenter : IPresenter<IAtlasModuleInfo>
    {
        IAtlasUserPresenter AtlasUser { get; set; }
        //
        // Summary:
        //     Gets or sets the name of the module.
        string ModuleName { get; set; }
        //
        // Summary:
        //     Gets or sets the module System.Type's AssemblyQualifiedName.
        string ModuleType { get; set; }
        //
        // Summary:
        //     Reference to the location of the module assembly. The following are examples
        //     of valid Microsoft.Practices.Prism.Modularity.ModuleInfo.Ref values: file://c:/MyProject/Modules/MyModule.dll
        //     for a loose DLL in WPF.
        string Ref { get; set; }
        //
        // Summary:
        //     Gets or sets the state of the Microsoft.Practices.Prism.Modularity.ModuleInfo
        //     with regards to the module loading and initialization process.
        ModuleState State { get; set; }
    }
}
