using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CompanyName.Atlas.UIControls
{
    public interface ILoginInfoHolder
    {
        /// <summary>
        /// Gets or sets the collection of menu items that are displayed in the current <see cref="AtlasWindow"/> menu.
        /// </summary>
        string UserName { get; set; }
        /// <summary>
        /// Gets or sets the collection of menu items that are displayed in the current <see cref="AtlasWindow"/> menu.
        /// </summary>
        String Password { get; set; }


        PasswordBox PasswordBox { get; set; }

        string UnsuccessfullText { get; set; }
    }
}
