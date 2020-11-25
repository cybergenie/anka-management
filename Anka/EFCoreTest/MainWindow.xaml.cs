using EFCoreTest.TableAdapter;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EFCoreTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly DbAdapter _context =
            new DbAdapter();
        DataAdapter InfoAdapter;

        private CollectionViewSource categoryViewSource;
        public MainWindow()
        {
            InitializeComponent();
            categoryViewSource =
                (CollectionViewSource)FindResource(nameof(categoryViewSource));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _context.Database.EnsureCreated();
            _context.basicinfo.Load();
            _context.Exercise.Load();
            var BasicInfoData = _context.basicinfo.Local.ToList();
            var ExerciseData = _context.Exercise.Local.ToList();

            InfoAdapter = new DataAdapter();

            var CollectionView = InfoAdapter.GetDataList(BasicInfoData);
            

            categoryViewSource.Source = CollectionView;
            
        }
                    

        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            _context.SaveChanges();
            categoryDataGrid.Items.Refresh();           
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            _context.Dispose();
            base.OnClosing(e);
        }

        private void btOutPut_Click(object sender, RoutedEventArgs e)
        {
            DataTable[] dtOutputs = { InfoAdapter.CollectionView };
            try
            {
                string strName = null;
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.RestoreDirectory = true;
                dlg.DefaultExt = "xls";
                dlg.Filter = "Excle工作簿|*.xlsx|Excle97-2003工作簿|*.xls";
                if (dlg.ShowDialog() == true)
                {
                    strName = dlg.FileName;
#if DEBUG
                    Debug.WriteLine(strName);
#endif
                }
                if (strName == null)
                    return;
                NPOIHelper.TableToExcel(dtOutputs, strName);
                MessageBox.Show("数据导出已完成");
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
    }
}
