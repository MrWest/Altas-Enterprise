using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlasInstall.Logic
{
    public enum InstallState
    {
        /// <summary>
        /// This is the view to see the Front Page.
        /// </summary>
        Welcome,

        /// <summary>
        /// This is the view to see the Atlas Main Page.
        /// </summary>
        Agreement,

        /// <summary>
        /// This is the view to see the Current Modeule Intrface.
        /// </summary>
        Miscellaneuos,

        /// <summary>
        /// This is the view to see the Front Page.
        /// </summary>
        Install,

        /// <summary>
        /// This is the view to see the Atlas Main Page.
        /// </summary>
        Finish
    }
}
