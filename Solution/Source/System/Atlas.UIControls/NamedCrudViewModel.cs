using System;
using CompanyName.Atlas.Contracts.Presentation.Data;

namespace CompanyName.Atlas.UIControls
{
    public struct NamedCrudViewModel
    {
        public String Name { get; set; }
        public ICrudViewModel ViewModel { get; set; }
    }
}