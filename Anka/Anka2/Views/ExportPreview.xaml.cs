using System.Windows;

namespace Anka2.Views
{
    /// <summary>
    /// ExportPreview.xaml 的交互逻辑
    /// </summary>
    public partial class ExportPreview : Window
    {
        public ExportPreview()
        {
            InitializeComponent();
        }

        private void DataGrid_LoadingRow(object sender, System.Windows.Controls.DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex()+1;
        }
    }
}
