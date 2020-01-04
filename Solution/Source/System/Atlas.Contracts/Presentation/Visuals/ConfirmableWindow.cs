using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace CompanyName.Atlas.Contracts.Presentation.Visuals
{
    public partial class ConfirmableWindow: Window
    {
        public ConfirmLayout ConfirmLayout { get; set; }

        public ConfirmableWindow()
        {
            ConfirmLayout = new ConfirmLayout()
            {
                Name = "ConfirmLayout",
                PlacementTarget = this,
                PopupAnimation = PopupAnimation.Slide,
                StaysOpen = false,
                Placement = PlacementMode.MousePoint
            };
        }

    }
}
