using System;
using System.Collections.Generic;
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

using MySql.Data.MySqlClient;


namespace Anka
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {       

        
        public MainWindow()
        {
            
            InitializeComponent();
            InitBMI();
            InitFMI();
            InitTBW();
            InitBCW();
            InitEBW();
            InitBody();
            InitLA();
            InitTK();
            InitRA();
            InitLL();
            InitRL();
            InitVAT();
            InitWC();
        }        
        
        private void DataConnection_Click(object sender, RoutedEventArgs e)
        {
            DatabaseConnection databaseConnection = new DatabaseConnection();
            databaseConnection.ShowDialog();
            if(DatabaseInfo.ConStatus==true)
            {
                this.ConStatus.Background = new SolidColorBrush(Colors.LightGreen);
            }
            else
            {
                this.ConStatus.Background = new SolidColorBrush(Colors.Red);
            }            
        }

        private void MiNewDoc_Click(object sender, RoutedEventArgs e)
        {
            NewPerson newPerson = new NewPerson();
            newPerson.ShowDialog();
            this.lbBasicName.Content = DataAdapter.Name;
            this.lbBasicNumber.Content = DataAdapter.Number.ToString();
            if (DataAdapter.Male == true)
            {
                this.lbBasicMale.Content = "男";
            }
            else if (DataAdapter.Male == false)
            {
                this.lbBasicMale.Content = "女";
            }
            this.lbBasicAge.Content = DataAdapter.Age.ToString();
            {
                this.lbLoadName.Content = this.lbBasicName.Content;
                this.lbLoadAge.Content = this.lbBasicAge.Content;
                this.lbLoadMale.Content = this.lbBasicMale.Content;
                this.lbLoadNumber.Content = this.lbBasicNumber.Content+" -";
            }
            {
                this.lbPHQName.Content = this.lbBasicName.Content;
                this.lbPHQAge.Content = this.lbBasicAge.Content;
                this.lbPHQMale.Content = this.lbBasicMale.Content;
                this.lbPHQNumber.Content = this.lbBasicNumber.Content + " -";
            }
            {
                this.lbGADName.Content = this.lbBasicName.Content;
                this.lbGADAge.Content = this.lbBasicAge.Content;
                this.lbGADMale.Content = this.lbBasicMale.Content;
                this.lbGADNumber.Content = this.lbBasicNumber.Content + " -";
            }
            {
                this.lbIPAQName.Content = this.lbBasicName.Content;
                this.lbIPAQAge.Content = this.lbBasicAge.Content;
                this.lbIPAQMale.Content = this.lbBasicMale.Content;
                this.lbIPAQNumber.Content = this.lbBasicNumber.Content + " -";
            }
            {
                this.lbOHQName.Content = this.lbBasicName.Content;
                this.lbOHQAge.Content = this.lbBasicAge.Content;
                this.lbOHQMale.Content = this.lbBasicMale.Content;
                this.lbOHQNumber.Content = this.lbBasicNumber.Content + " -";
            }
            {
                this.lbSPPBName.Content = this.lbBasicName.Content;
                this.lbSPPBAge.Content = this.lbBasicAge.Content;
                this.lbSPPBMale.Content = this.lbBasicMale.Content;
                this.lbSPPBNumber.Content = this.lbBasicNumber.Content + " -";
            }
            {
                this.lbBIName.Content = this.lbBasicName.Content;
                this.lbBIAge.Content = this.lbBasicAge.Content;
                this.lbBIMale.Content = this.lbBasicMale.Content;
                this.lbBINumber.Content = this.lbBasicNumber.Content + " -";
            }

        }

       
    }
}
