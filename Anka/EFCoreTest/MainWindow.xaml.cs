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
                //var ExerciseData = (_context.Exercise
                //    .Join(_context.basicinfo, a => a.basicinfoNumber, b => b.Number, (a, b) => new { Number = b.Number, Name = b.Name, Age = b.Age, Male = b.Male, ExerciseNumber = a.ExerciseNumber, Checks = a.Checks }))
                //    .ToList();
                //var GADData = (_context.GAD
                //    .Join(_context.basicinfo, a => a.basicinfoNumber, b => b.Number, (a, b) => new { Number = b.Number, Name = b.Name, Age = b.Age, Male = b.Male, GADNumber = a.GADNumber, Checks = a.GADResult }))
                //    .ToList();   
                //InfoAdapter = new DataAdapter();
                //var CollectionView = InfoAdapter.GetDataList(GADData, "GAD");
                //categoryViewSource.Source = CollectionView;
                //            var IPAQData = (_context.ipaq
                //.Join(_context.basicinfo, a => a.basicinfoNumber, b => b.Number, (a, b) => new { Number = b.Number, Name = b.Name, Age = b.Age, Male = b.Male, IPAQNumber = a.IPAQNumber, IPAQ0 = a.IPAQ0, IPAQ1 = a.IPAQ1, IPAQ2 = a.IPAQ2, IPAQ3 = a.IPAQ3, IPAQ4 = a.IPAQ4, IPAQ5 = a.IPAQ5 }))
                //.ToList();
                //            InfoAdapter = new DataAdapter();
                //            var CollectionView = InfoAdapter.GetDataList(IPAQData, "IPAQ");
                //            categoryViewSource.Source = CollectionView;

                //                var OHQData = (_context.OHQ
                //.Join(_context.basicinfo, a => a.basicinfoNumber, b => b.Number, (a, b) => new { Number = b.Number, Name = b.Name, Age = b.Age, Male = b.Male, OHQNumber = a.OHQNumber, OHQ1 = a.OHQ1, OHQ2 = a.OHQ2, OHQ3 = a.OHQ3, OHQ4 = a.OHQ4, OHQ5 = a.OHQ5, OHQ6 = a.OHQ6, OHQ7 = a.OHQ7, OHQ8 = a.OHQ8, OHQ9 = a.OHQ9 }))
                //.ToList();
                //                InfoAdapter = new DataAdapter();
                //                var CollectionView = InfoAdapter.GetDataList(OHQData, "OHQ");
                //                categoryViewSource.Source = CollectionView;

                var PHQData = (_context.phq
                    .Join(_context.basicinfo, a => a.basicinfoNumber, b => b.Number, (a, b) => new { Number = b.Number, Name = b.Name, Age = b.Age, Male = b.Male, PHQNumber = a.PHQNumber, Checks = a.PHQResult }))
                    .ToList();
                InfoAdapter = new DataAdapter();
                var CollectionView = InfoAdapter.GetDataList(PHQData, "PHQ");
                categoryViewSource.Source = CollectionView;
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
