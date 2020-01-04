using System.Windows.Input;

namespace CompanyName.Atlas.Contracts.Presentation.Features
{
    public interface ICopyPasteableController : ICopyPasteable
    {
        ICopyPasteable ObjectToPasteOn { get; set; }

        //object SystemSelectedObject { get; set; }

        ICommand CopyCommand { get; }
        ICommand PasteCommand { get; }

        bool CanCopy { get; }
        bool CanPaste { get; }
    }
}