using System.Windows.Controls.Primitives;

namespace CompanyName.Atlas.Contracts.Presentation.Features
{
    public interface IPrinttableContainer
    {
        void Print();
    }

    public interface IExporttableContainer
    {
        void Export();
    }
}