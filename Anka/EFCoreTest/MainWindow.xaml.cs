using EFCoreTest.TableAdapter;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
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
            var BasicInfoData = _context.basicinfo.Local.ToList();

            DataAdapter basicInfoAdapter = new DataAdapter();

            var BasicInfoDataList = basicInfoAdapter.GetDataList(BasicInfoData);

            categoryViewSource.Source = new ObservableCollection<BasicInfo>(BasicInfoData);
            
        }
                    

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _context.SaveChanges();
            categoryDataGrid.Items.Refresh();
            productsDataGrid.Items.Refresh();
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            _context.Dispose();
            base.OnClosing(e);
        }
    }
}
