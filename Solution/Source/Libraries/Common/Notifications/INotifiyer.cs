using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Notifications
{
    /// <summary>
    /// represents an class which notifies changes to presentation
    /// </summary>
   public interface INotifiyer
    {
       /// <summary>
       /// notifies changes to som property
       /// </summary>
        void Notify();
    }
}
