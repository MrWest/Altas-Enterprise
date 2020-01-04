using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using CompanyName.Atlas.Contracts.Presentation.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;

namespace CompanyName.Atlas.UIControls.Views
{
    /// <summary>
    /// Interaction logic for ConceptsView.xaml
    /// </summary>
    public partial class ConceptsView : UserControl
    {
        public ConceptsView()
        {
            InitializeComponent();
            _cachePopup = new Popup(){ StaysOpen = false};
            _cacheListBox = new ListBox();
            _cachePopup.Child = _cacheListBox;
        }

        private void FrameworkElement_OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                ((AtlasDataGrid) sender).AddButtonCommand = ((ICrudViewModel) e.NewValue).AddCommand;
                ((AtlasDataGrid) sender).DeleteButtonCommand = ((ICrudViewModel) e.NewValue).DeleteCommand;
                ((AtlasDataGrid) sender).SelectedItem = ((ICrudViewModel) e.NewValue).SelectedItem;
            }
        }

        //private void FrameworkElement_OnDataContextChanged2(object sender, DependencyPropertyChangedEventArgs e)
        //{
        //    if (e.NewValue != null)
        //    {
        //        ((AtlasDataGrid)sender).AddButtonCommand = ((ICrudViewModel)e.NewValue).AddCommand;
        //        ((AtlasDataGrid)sender).DeleteButtonCommand = ((ICrudViewModel)e.NewValue).DeleteCommand;
        //        ((AtlasDataGrid)sender).SelectedItem = ((ICrudViewModel)e.NewValue).SelectedItem;
        //    }
        //}

        private Popup _cachePopup;
        private ListBox _cacheListBox;

        private void Author_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (((FrameworkElement) sender).DataContext != null)
            {
                var presenter = (((FrameworkElement) sender).DataContext as ISubjectConceptContentPresenter);

                if (presenter!=null)
                {
                    ((AtlasCacheTextBox) sender).CacheSource =
                        presenter.FindAutorByContains(((AtlasCacheTextBox) sender).Text).ToArray();

                }
            }

        }

        private void Source_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (((FrameworkElement)sender).DataContext != null)
            {
                var presenter = (((FrameworkElement)sender).DataContext as ISubjectConceptContentPresenter);

                if (presenter != null)
                {
                    ((AtlasCacheTextBox)sender).CacheSource =
                        presenter.FindSourceByContains(((AtlasCacheTextBox)sender).Text).ToArray();

                }
            }
        }

        private void UIElement_OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (((RichTextBox)sender).DataContext != null)
            {
                var range = new TextRange(((RichTextBox)sender).Document.ContentStart, ((RichTextBox)sender).Document.ContentEnd);

                (((RichTextBox)sender).DataContext as ISubjectConceptContentPresenter).Content = range.Text;
            }
        }

        private void TextBoxBase_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (((RichTextBox)sender).DataContext != null)
            {
                var range = new TextRange(((RichTextBox)sender).Document.ContentStart, ((RichTextBox)sender).Document.ContentEnd);

                if((((RichTextBox)sender).DataContext as ISubjectConceptContentPresenter).Content.ToString()!= range.Text)
                (((RichTextBox)sender).DataContext as ISubjectConceptContentPresenter).Content = range.Text;
            }
        }

        private void FrameworkElement_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (((RichTextBox)sender).DataContext != null)
            {
                //var range = new TextRange(((RichTextBox)sender).Document.ContentStart, ((RichTextBox)sender).Document.ContentEnd);

                // = range.Text;


                // Create a FlowDocument to contain content for the RichTextBox.
                FlowDocument myFlowDoc = new FlowDocument();

                // Create a Run of plain text and some bold text.
                Run myRun = new Run((((RichTextBox)sender).DataContext as ISubjectConceptContentPresenter).Content.ToString());
                //Bold myBold = new Bold(new Run("edit me!"));

                // Create a paragraph and add the Run and Bold to it.
                Paragraph myParagraph = new Paragraph();
                myParagraph.Inlines.Add(myRun);
                //myParagraph.Inlines.Add(myBold);

                // Add the paragraph to the FlowDocument.
                myFlowDoc.Blocks.Add(myParagraph);

                ((RichTextBox) sender).Document = myFlowDoc;


            }
        }
    }
}
