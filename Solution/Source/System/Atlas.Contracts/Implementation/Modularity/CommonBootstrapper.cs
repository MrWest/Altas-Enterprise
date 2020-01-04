using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Practices.Prism.UnityExtensions;

namespace CompanyName.Atlas.Contracts.Implementation.Modularity
{
    public class CommonBootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return null;//throw new NotImplementedException();
        }

        public void GoInitializeModules()
        {
            InitializeModules();
        }
    }
}
