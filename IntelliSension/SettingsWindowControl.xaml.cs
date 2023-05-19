using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;

namespace IntelliSension
{
    /// <summary>
    /// Interaction logic for SettingsWindowControl.
    /// </summary>
    public partial class SettingsWindowControl : UserControl
    {


        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsWindowControl"/> class.
        /// </summary>
        public SettingsWindowControl()
        {
            this.InitializeComponent();
        }

        public string ScriptPath { get; set; }
        public event ScriptPathChanged scriptPathChanged;
        public event SyncOnOpenChanged syncOnOpenChanged;
        public event SyncOnEditChanged syncOnEditChanged;

        /// <summary>
        /// Handles click on the button by displaying a message box.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event args.</param>
        [SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions", Justification = "Sample code")]
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Default event handler naming pattern")]

        private void SyncOnOpenChecked(object sender, RoutedEventArgs e)
        {
            syncOnOpenChanged?.Invoke(true);
        }
        private void SyncOnOpenUnchecked(object sender, RoutedEventArgs e)
        {
            syncOnOpenChanged?.Invoke(false);
        }

        private void SyncOnEditChecked(object sender, RoutedEventArgs e)
        {
            syncOnEditChanged?.Invoke(true);
        }

        private void SyncOnEditUnchecked(object sender, RoutedEventArgs e)
        {
            syncOnEditChanged?.Invoke(false);
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            dlg.DefaultExt = ".py";
            dlg.Filter = "Python Files (*.py;*.pyw;*.pyi)|*.py;*.pyw;*.pyi|Text files (*.txt)|*.txt|All Files (*.*)|*.*";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                ScriptPath = dlg.FileName;
                
                if(sender is Button chooseButton){
                    chooseButton.Content = ScriptPath;
                    scriptPathChanged?.Invoke(ScriptPath);
                }

            }
        }
    }
}