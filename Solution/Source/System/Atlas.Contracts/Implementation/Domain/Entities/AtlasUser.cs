using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using Microsoft.Practices.Prism.Modularity;

namespace CompanyName.Atlas.Contracts.Implementation.Domain.Entities
{
    public class AtlasUser:NomenclatorBase,IAtlasUser
    {
        public AtlasUser()
        {
           this.AllowedModules = new HashSet<AtlasModuleInfo>();

          //  Period = new Period();
        }
        public string Password { get; set; }
        public AtlasUserRol Rol { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<AtlasModuleInfo> AllowedModules { get;  set; }

        //public IPeriod Period { get; set; }
        //public string PeriodId { get; set; }
    }

    public class AtlasModuleInfo : EntityBase, IAtlasModuleInfo
    {
        
        public string AtlasUserId { get; set; }

        [ForeignKey("AtlasUserId")]
        public AtlasUser AtlasUser { get;}
       

        //
        // Summary:
        //     Gets or sets the list of modules that this module depends upon.
        //public Collection<string> DependsOn { get; set; }
        //
        // Summary:
        //     Specifies on which stage the Module will be initialized.
        public InitializationMode InitializationMode { get; set; }
        //
        // Summary:
        //     Gets or sets the name of the module.
        public string ModuleName { get; set; }
        //
        // Summary:
        //     Gets or sets the module System.Type's AssemblyQualifiedName.
        public string ModuleType { get; set; }
        //
        // Summary:
        //     Reference to the location of the module assembly. The following are examples
        //     of valid Microsoft.Practices.Prism.Modularity.ModuleInfo.Ref values: file://c:/MyProject/Modules/MyModule.dll
        //     for a loose DLL in WPF.
        public string Ref { get; set; }
        //
        // Summary:
        //     Gets or sets the state of the Microsoft.Practices.Prism.Modularity.ModuleInfo
        //     with regards to the module loading and initialization process.
        public ModuleState State { get; set; }
    }

}
