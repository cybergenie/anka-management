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
    public partial class MainWindow 
    {
        private void BtBasicSave_Click(object sender, RoutedEventArgs e)
        {
            BasicDataSave();

            string BasicRisk="";
            foreach (bool temp in DataAdapter.BasicInfoResult.BasicRisk)
            {
                BasicRisk += (temp == true ? "1" : "0");
            }

            string sql = string.Format("UPDATE `anka`.`basicinfo` SET `Killip` = '{0}', `EF` = '{1}', `LV` = '{2}', `BasicOther` = '{3}'," +
                " `BasicRisk` = '{4}',`RiskOther` = '{5}',`PCI` = '{6}',`ResidualStenosis` = '{7}',`CollatCirc` = {8},`DominantCoronary` = '{9}' WHERE (`Number` = '{10}');",
                DataAdapter.BasicInfoResult.Killip, DataAdapter.BasicInfoResult.EF, DataAdapter.BasicInfoResult.LV, DataAdapter.BasicInfoResult.BasicOther,
                BasicRisk, DataAdapter.BasicInfoResult.RiskOther, DataAdapter.BasicInfoResult.PCI, DataAdapter.BasicInfoResult.ResidualStenosis,
                DataAdapter.BasicInfoResult.CollatCirc, DataAdapter.BasicInfoResult.DominantCoronary,DataAdapter.Number);

            DatabaseInfo.ModifyDatabase(sql);

            ((Button)sender).Background = new SolidColorBrush(Colors.LightGreen);
        }

        private void CbCC_Click(object sender, RoutedEventArgs e)
        {
            if (this.cbCC.IsChecked == true)
            {
                this.cbCC.Content = "侧枝循环：有";
            }
            else if (this.cbCC.IsChecked == false)
            {
                this.cbCC.Content = "侧枝循环：无";
            }
        }

        private bool[] CbRiskClick()
        {

            bool[] tempCheck = new bool[13]{
                this.cbRisk1.IsChecked==true?true:false,
                this.cbRisk2.IsChecked==true?true:false,
                this.cbRisk3.IsChecked==true?true:false,
                this.cbRisk4.IsChecked==true?true:false,
                this.cbRisk5.IsChecked==true?true:false,
                this.cbRisk6.IsChecked==true?true:false,
                this.cbRisk7.IsChecked==true?true:false,
                this.cbRisk8.IsChecked==true?true:false,
                this.cbRisk9.IsChecked==true?true:false,
                this.cbRisk10.IsChecked==true?true:false,
                this.cbRisk11.IsChecked==true?true:false,
                this.cbRisk12.IsChecked==true?true:false,
                this.cbRisk13.IsChecked==true?true:false
            };

            return tempCheck;


        }

        private void BasicDataSave()
        {
            DataAdapter.BasicInfoResult.Killip = this.txKillip.Text;
            DataAdapter.BasicInfoResult.EF = this.txEF.Text;
            DataAdapter.BasicInfoResult.LV = this.txLV.Text;
            DataAdapter.BasicInfoResult.BasicOther = this.txBasicOther.Text;
            DataAdapter.BasicInfoResult.BasicRisk = CbRiskClick();
            DataAdapter.BasicInfoResult.RiskOther = this.txRisk13.Text;

            if (DataAdapter.IsNumber(this.txPCI.Text) == true)
                DataAdapter.BasicInfoResult.PCI = Convert.ToInt32(this.txPCI.Text);


            if (DataAdapter.IsNumber(this.txRS.Text) == true)
                DataAdapter.BasicInfoResult.ResidualStenosis = Convert.ToInt32(this.txRS.Text);


            if (this.rbDCL.IsChecked == true)
            {
                DataAdapter.BasicInfoResult.DominantCoronary = -1;
            }
            else if (this.rbDCB.IsChecked == true)
            {
                DataAdapter.BasicInfoResult.DominantCoronary = 0;
            }
            else if (this.rbDCR.IsChecked == true)
            {
                DataAdapter.BasicInfoResult.DominantCoronary = 1;
            }


            if (this.cbCC.IsChecked == true)
            {
                DataAdapter.BasicInfoResult.CollatCirc = true;
            }
            else
            {
                DataAdapter.BasicInfoResult.CollatCirc = false;
            }

        }

        private void CbRisk13_Click(object sender, RoutedEventArgs e)
        {
            if (((CheckBox)sender).IsChecked == true)
            {
                this.txRisk13.IsEnabled = true;
            }
            else
            {
                this.txRisk13.Text = "";
                this.txRisk13.IsEnabled = true;
            }
        }
    }
}
