using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Implementation.Domain.Entities;
using Microsoft.Practices.Prism.Modularity;

namespace CompanyName.Atlas.Contracts.Domain.Common
{
    /// <summary>
    /// represents a user (security) of atlas system
    /// </summary>
    public interface IAtlasUser:INomenclator
    {
        string Password { get; set; }
        AtlasUserRol Rol { get; set; }
        ICollection<AtlasModuleInfo> AllowedModules { get; set; } 

        //IPeriod Period { get; set; }
        //string PeriodId { get; set; }

    }


    public interface IAtlasModuleInfo : IEntity
    {
        string AtlasUserId { get; set; }

        [ForeignKey("AtlasUserId")]
         AtlasUser AtlasUser { get; }


       

        //
        // Summary:
        //     Gets or sets the list of modules that this module depends upon.
        //public Collection<string> DependsOn { get; set; }
        //
        // Summary:
        //     Specifies on which stage the Module will be initialized.
         InitializationMode InitializationMode { get; set; }
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
