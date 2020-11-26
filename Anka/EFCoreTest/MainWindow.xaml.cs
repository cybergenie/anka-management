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
            using (var _context = new DbAdapter())
            {
                //var BasicInfoData = _context.basicinfo.ToList();
                var ExerciseData = (_context.Exercise
                    .Join(_context.basicinfo,a=>a.basicinfoNumber,b=>b.Number,(a, b)=>new {  病案号=b.Number,姓名=b.Name,性别=b.Male, 记录编号 = a.ExerciseNumber , 床上负荷 =a.Checks, 室内负荷 =a.Checks, 室外负荷 =a.Checks, 院外负荷 =a.Checks} ))                    
                    .ToList();
                InfoAdapter = new DataAdapter();
                var CollectionView = InfoAdapter.GetDataList(ExerciseData);
                categoryViewSource.Source = ExerciseData;
            }     
        }
                    

        private void btSave_Click(object sender, RoutedEventArgs e)
        {           
            categoryDataGrid.Items.Refresh();           
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
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
