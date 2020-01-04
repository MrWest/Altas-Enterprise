using System.Linq;
using System.Windows.Input;

namespace System.Windows.Controls
{
    [TemplatePart(Name = TextBoxControlPartName, Type = typeof(TextBox))]
    [TemplatePart(Name = ContextMenuPartName, Type = typeof(TextBox))]
    [TemplatePart(Name = RenameMenuItemPartName, Type = typeof(TextBox))]
    public class EditableTextBlock : UserControl
    {
        public const string TextBoxControlPartName = "PART_TextBox";
        public const string RenameMenuItemPartName = "PART_RenameMenuItem";
        public const string ContextMenuPartName = "PART_ContextMenu";
        private const string LabelName = "Label";

        /// <summary>
        /// Identifies the dependency property to contain the value of the text being edited in this label.
        /// </summary>
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(EditableTextBlock),
            new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        /// <summary>
        /// Identifies the dependency property to contain the value of the text displayed by the rename command.
        /// </summary>
        public static readonly DependencyProperty CommandTextProperty =
            DependencyProperty.Register("CommandText", typeof(string), typeof(EditableTextBlock), new PropertyMetadata(string.Empty));

        /// <summary>
        /// Identifies the dependency property used to keep a value saying whether the current Editable TextBlock is in edition or not.
        /// </summary>
        public static readonly DependencyProperty InEditionProperty =
            DependencyProperty.Register("InEdition", typeof(bool), typeof(EditableTextBlock), new PropertyMetadata(false));

        /// <summary>
        /// Identifies the dependency property used to keep a value saying whether the current Editable TextBlock may engage editions.
        /// </summary>
        public static readonly DependencyProperty IsEditableProperty = DependencyProperty.Register(
            "IsEditable", typeof(bool), typeof(EditableTextBlock), new PropertyMetadata(true));


        public EditableTextBlock()
        {
            CommandText = Properties.Resources.Rename;

            DefaultStyleKey = typeof(EditableTextBlock);
        }


        /// <summary>
        /// Gets or sets the value of the text handled in this label.
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        /// <summary>
        /// Gets or sets the value of the text displayed by the rename command.
        /// </summary>
        public string CommandText
        {
            get { return (string)GetValue(CommandTextProperty); }
            set { SetValue(CommandTextProperty, value); }
        }

        /// <summary>
        /// Gets whether this control is in edition mode.
        /// </summary>
        public bool InEdition
        {
            get { return (bool)GetValue(InEditionProperty); }
            private set { SetValue(InEditionProperty, value); }
        }

        /// <summary>
        /// Gets or sets whether the current Editable TextBlock allows edition or not.
        /// </summary>
        public bool IsEditable
        {
            get { return (bool)GetValue(IsEditableProperty); }
            set { SetValue(IsEditableProperty, value); }
        }

        private TextBox TextBoxControl
        {
            get { return (TextBox)Template.FindName(TextBoxControlPartName, this); }
        }

        private Label LabelControl
        {
            get { return (Label)Template.FindName(LabelName, this); }
        }

        private MenuItem RenameMenuItemControl
        {
            get { return (MenuItem)Template.FindName(RenameMenuItemPartName, this); }
        }

        private ContextMenu ContextMenuControl
        {
            get { return (ContextMenu)Template.FindName(ContextMenuPartName, this); }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            RenameMenuItemControl.Click += RenameMenuItem_Click;
            TextBoxControl.KeyUp += TextBox_KeyUp;
            TextBoxControl.LostFocus += TextBox_LostFocus;
            LabelControl.MouseDoubleClick += LabelControl_MouseDoubleClick;

            InitializeContextMenu();
        }


        private void RenameMenuItem_Click(object sender, RoutedEventArgs e)
        {
            StartEdition();
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            bool acceptedValue = e.Key == Key.Enter;
            bool rejectedValue = e.Key == Key.Escape;

            if (acceptedValue || rejectedValue)
                FinishEdition(acceptedValue);
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            FinishEdition();
        }

        private void LabelControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;

            if (!IsEditable)
                return;

            StartEdition();
        }

        private void StartEdition()
        {
            // Put the control in edition mode
            if (InEdition)
                return;

            InEdition = true;

            // Also focus the text box
            TextBoxControl.Text = Text;
            TextBoxControl.Focus();
            TextBoxControl.SelectAll();
        }

        private void FinishEdition(bool acceptValue = true)
        {
            if (!InEdition)
                return;

            if (acceptValue)
                Text = TextBoxControl.Text;

            InEdition = false;
        }

        private void InitializeContextMenu()
        {
            ContextMenu contextMenuControl = ContextMenuControl;
            ContextMenu contextMenu = ContextMenu;

            if (ContextMenu != null)
            {
                contextMenuControl.Items.Add(new Separator());

                foreach (object item in contextMenu.Items.OfType<object>().ToArray())
                {
                    contextMenu.Items.Remove(item);
                    contextMenuControl.Items.Add(item);
                }
            }
        }
    }
}