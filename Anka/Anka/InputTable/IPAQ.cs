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

namespace Anka
{
    public partial class MainWindow
    {
        private void BtIPAQSave_Click(object sender, RoutedEventArgs e)
        {
            if (this.txIPAQLoop.Text.Trim().Length > 0)
            {
                DataAdapter.IPAQNumber = DataAdapter.Number + "-" + this.txIPAQLoop.Text.Trim();
                IPAQDataSave();

                string sql1 = string.Format("INSERT INTO `anka`.`ipaq` (`IPAQNumber`, `IPAQ0`, `IPAQ1`, `IPAQ2`, `IPAQ3`, `IPAQ4`, `IPAQ5`, `basicinfo_Number`) VALUES ('{0}', {1}, '{2}', '{3}', '{4}', '{5}', '{6}', '{7}');",
                   DataAdapter.IPAQNumber,
                   DataAdapter.IPAQResult.IPAQ0,
                   DataAdapter.IPAQResult.IPAQ1,
                   DataAdapter.IPAQResult.IPAQ2,
                   DataAdapter.IPAQResult.IPAQ3,
                   DataAdapter.IPAQResult.IPAQ4,
                   DataAdapter.IPAQResult.IPAQ5,
                   DataAdapter.Number);

                string sql2 = string.Format("UPDATE `anka`.`ipaq` SET `IPAQ0` = {0}, `IPAQ1` = '{1}', `IPAQ2` = '{2}', `IPAQ3` = '{3}', `IPAQ4` = '{4}', `IPAQ5` = '{5}' WHERE (`IPAQNumber` = '{6}') and (`basicinfo_Number` = '{7}');",
                   DataAdapter.IPAQResult.IPAQ0,
                   DataAdapter.IPAQResult.IPAQ1,
                   DataAdapter.IPAQResult.IPAQ2,
                   DataAdapter.IPAQResult.IPAQ3,
                   DataAdapter.IPAQResult.IPAQ4,
                   DataAdapter.IPAQResult.IPAQ5,
                   DataAdapter.IPAQNumber,
                   DataAdapter.Number);
               // DatabaseInfo.ModifyDatabase(sql1, sql2);
                ((Button)sender).Background = new SolidColorBrush(Colors.LightGreen);
            }
            else
            {
                MessageBox.Show("请输入国际标准化身体活动调查问卷编号。");
            }
           

        }

        private void TxIPAQ4_LostFocus(object sender, RoutedEventArgs e)
        {
            if (this.txIPAQ4.Text.Contains('h') || this.txIPAQ4.Text.Contains('H'))
                this.txIPAQ4.Text = (Convert.ToInt32(this.txIPAQ4.Text.Trim('h')) * 60).ToString();
        }

        private void IPAQDataSave()
        {
            if (this.rbIPAQYes.IsChecked == true)
                DataAdapter.IPAQResult.IPAQ0 = true;
            if (this.rbIPAQNo.IsChecked == false)
                DataAdapter.IPAQResult.IPAQ0 = false;
            DataAdapter.IPAQResult.IPAQ1 = DataAdapter.IsNumber(this.txIPAQ1.Text.Trim()) ? Convert.ToInt32(this.txIPAQ1.Text.Trim()) : 0;
            DataAdapter.IPAQResult.IPAQ2 = DataAdapter.IsNumber(this.txIPAQ2.Text.Trim()) ? Convert.ToInt32(this.txIPAQ2.Text.Trim()) : 0;
            DataAdapter.IPAQResult.IPAQ3 = DataAdapter.IsNumber(this.txIPAQ3.Text.Trim()) ? Convert.ToInt32(this.txIPAQ3.Text.Trim()) : 0;
            DataAdapter.IPAQResult.IPAQ4 = DataAdapter.IsNumber(this.txIPAQ4.Text.Trim()) ? Convert.ToInt32(this.txIPAQ4.Text.Trim()) : 0;

            RadioButton[] rbIPAQ5 = new RadioButton[7] { this.rbIPAQ51, this.rbIPAQ52, this.rbIPAQ53, this.rbIPAQ54, this.rbIPAQ55, this.rbIPAQ56, this.rbIPAQ57 };
            for (int i = 0; i < 7; i++)
            {
                if (rbIPAQ5[i].IsChecked == true)
                    DataAdapter.IPAQResult.IPAQ5 = i;
            }


        }


    }
}
