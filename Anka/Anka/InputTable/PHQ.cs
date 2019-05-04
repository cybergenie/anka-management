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
        private void BtPHQSave_Click(object sender, RoutedEventArgs e)
        {
            if (this.txPHQLoop.Text.Trim().Length > 0)
            {
                DataAdapter.PHQNumber = DataAdapter.Number + "-" + this.txPHQLoop.Text.Trim();
                DataAdapter.PHQResult = PHQData();

                string sql1 = string.Format("INSERT INTO `anka`.`phq` (`PHQNumber`, `PHQResult`, `basicinfo_Number`) VALUES('{0}', '{1}', '{2}');",
                    DataAdapter.PHQNumber,
                    DataAdapter.ArrayToString(DataAdapter.PHQResult),
                    DataAdapter.Number);

                string sql2 = string.Format("UPDATE `anka`.`phq` SET `PHQResult` = '{1}' WHERE(`PHQNumber` = '{0}') and(`basicinfo_Number` = '{2}');",
                   DataAdapter.PHQNumber,
                   DataAdapter.ArrayToString(DataAdapter.PHQResult),
                   DataAdapter.Number);
                DatabaseInfo.ModifyDatabase(sql1, sql2);
                ((Button)sender).Background = new SolidColorBrush(Colors.LightGreen);
            }
            else
            {
                MessageBox.Show("请输入PHQ评估量表编号。");
            }
           
        }

        private int[] PHQData()
        {
            int[] Data = new int[9];
            RadioButton[,] rbPHQs = new RadioButton[9, 4]
               {{this.rbPHQ11,this.rbPHQ12,this.rbPHQ13,this.rbPHQ14},
                {this.rbPHQ21,this.rbPHQ22,this.rbPHQ23,this.rbPHQ24 },
                {this.rbPHQ31,this.rbPHQ32,this.rbPHQ33,this.rbPHQ34 },
                {this.rbPHQ41,this.rbPHQ42,this.rbPHQ43,this.rbPHQ44 },
                {this.rbPHQ51,this.rbPHQ52,this.rbPHQ53,this.rbPHQ54 },
                {this.rbPHQ61,this.rbPHQ62,this.rbPHQ63,this.rbPHQ64 },
                {this.rbPHQ71,this.rbPHQ72,this.rbPHQ73,this.rbPHQ74 },
                {this.rbPHQ81,this.rbPHQ82,this.rbPHQ83,this.rbPHQ84 },
                {this.rbPHQ91,this.rbPHQ92,this.rbPHQ93,this.rbPHQ94 }
               };
            for (int i = 0; i < 9; i++)
            {
                int temp = -1;
                for (int j = 0; j < 4; j++)
                {
                    if (rbPHQs[i, j].IsChecked == true)
                        temp = j;
                }
                Data[i] = temp;
            }

            return Data;
        }

        private void RbPHQ_Click(object sender, RoutedEventArgs e)
        {
            int result = 0;
            int[] Data = PHQData();
            foreach (int temp in Data)
            {
                if (temp >= 0)
                    result += temp;
            }

            if (result >= 0 && result <= 4)
                this.rbPHQResult1.IsChecked = true;
            if (result >= 5 && result <= 9)
                this.rbPHQResult2.IsChecked = true;
            if (result >= 10 && result <= 13)
                this.rbPHQResult3.IsChecked = true;
            if (result >= 14 && result <= 18)
                this.rbPHQResult4.IsChecked = true;
            if (result >= 19)
                this.rbPHQResult5.IsChecked = true;

            this.lbPHQResult.Content = result.ToString();
        }
    }
}
