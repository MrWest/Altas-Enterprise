

using System;
using System.Windows.Controls;
using CompanyName.Atlas.Contracts.Presentation.Services;

namespace CompanyName.Atlas.UIControls
{
    public class AtlasSignalTextBox:TextBox
    {
        public AtlasSignalTextBox()
        {
            DefaultStyleKey = typeof(TextBox);
            TextChanged+= OnTextChanged;
        }

        private void OnTextChanged(object sender, EventArgs eventArgs)
        {
           var statusBarService = DataContext as IStatusBarServices;
            if (statusBarService != null)
                statusBarService.isTextReallyShowed = true;
        }
    }
}