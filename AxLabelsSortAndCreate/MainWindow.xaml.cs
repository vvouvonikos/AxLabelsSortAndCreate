using AxLabelHelper;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace AxLabelsSortAndCreate
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const string defaultParentText = "Please click [Browse] to select parent label file";
        const string defaultChildText = "Please click [Browse] to select child label file";
        const string defaultTargetText = "Please click [Browse] to select the target file";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void SelectParentFileButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openDlg = new Microsoft.Win32.OpenFileDialog()
            {
                Filter = "Text files (*.txt)|*.txt"
            };
            bool? result = openDlg.ShowDialog(this);

            if (!(bool)result)
            {
                return;
            }

            ParentFilename.Text = openDlg.FileName;
        }

        private void SelectChildFileButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openDlg = new Microsoft.Win32.OpenFileDialog()
            {
                Filter = "Text files (*.txt)|*.txt"
            };
            bool? result = openDlg.ShowDialog(this);

            if (!(bool)result)
            {
                return;
            }

            ChildFilename.Text = openDlg.FileName;
        }

        private void SelectTargetFileButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openDlg = new Microsoft.Win32.OpenFileDialog()
            {
                Filter = "Text files (*.txt)|*.txt"
            };
            bool? result = openDlg.ShowDialog(this);

            if (!(bool)result)
            {
                return;
            }

            TargetFilename.Text = openDlg.FileName;
        }

        private void SortAndCreateNewLabelsButton_Click(object sender, RoutedEventArgs e)
        {
            SortAndCreateNewLabelsButton.IsEnabled = false;
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(ParentFilename.Text) || ParentFilename.Text == defaultParentText)
            {
                errors.Add("Please select parent file");
            }

            if (string.IsNullOrWhiteSpace(ChildFilename.Text) || ChildFilename.Text == defaultChildText)
            {
                errors.Add("Please select child file");
            }

            if (string.IsNullOrWhiteSpace(TargetFilename.Text) || TargetFilename.Text == defaultTargetText)
            {
                errors.Add("Please select target file");
            }

            if (errors.Count > 0)
            {
                var sbErrors = new StringBuilder();
                sbErrors.AppendLine("The following errors were encountered:");
                for (int i = 0; i < errors.Count; i++)
                {
                    sbErrors.AppendLine($"{ (i + 1) }. { errors[i] }");
                }

                MessageBox.Show(this, sbErrors.ToString(), "Error");

                return;
            }

            var result = AxLabelMethods.SortAndCreateMissingLabels(ParentFilename.Text, ChildFilename.Text, TargetFilename.Text);

            MessageBox.Show(this, (result.Success ? "New labels were successfully created" : result.Error), "Result");
            SortAndCreateNewLabelsButton.IsEnabled = true;
        }
    }
}
