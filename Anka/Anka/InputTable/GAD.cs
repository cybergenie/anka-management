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
using System.Data.SQLite;

namespace Anka
{
    public partial class MainWindow
    {
        private int[] GADData()
        {
            int[] Data = new int[7];
            RadioButton[,] rbGADs = new RadioButton[7, 4]
               {{this.rbGAD11,this.rbGAD12,this.rbGAD13,this.rbGAD14},
                {this.rbGAD21,this.rbGAD22,this.rbGAD23,this.rbGAD24 },
                {this.rbGAD31,this.rbGAD32,this.rbGAD33,this.rbGAD34 },
                {this.rbGAD41,this.rbGAD42,this.rbGAD43,this.rbGAD44 },
                {this.rbGAD51,this.rbGAD52,this.rbGAD53,this.rbGAD54 },
                {this.rbGAD61,this.rbGAD62,this.rbGAD63,this.rbGAD64 },
                {this.rbGAD71,this.rbGAD72,this.rbGAD73,this.rbGAD74 }
               };
            for (int i = 0; i < 7; i++)
            {
                int temp = -1;
                for (int j = 0; j < 4; j++)
                {
                    if (rbGADs[i, j].IsChecked == true)
                        temp = j;
                }
                Data[i] = temp;
            }

            return Data;
        }




        private void RbGAD_Click(object sender, RoutedEventArgs e)
        {
            int result = 0;
            int[] Data = GADData();
            foreach (int temp in Data)
            {
                if (temp >= 0)
                    result += temp;
            }

            this.lbGADResult.Content = result.ToString();
        }

        private void BtGADSave_Click(object sender, RoutedEventArgs e)
        {
            if(this.txGADLoop.Text.Trim().Length>0)
            {
                DataAdapter.GADNumber = DataAdapter.Number + "-" + this.txGADLoop.Text.Trim();
                DataAdapter.GADResult = GADData();

                string sql = string.Format("SELECT * FROM gad where GADNumber='{0}';", DataAdapter.GADNumber);
                SQLiteDataReader dataReader = SQLiteAdapter.ExecuteReader(sql);

                if (dataReader.StepCount == 0)
                {

                    sql = string.Format("INSERT INTO gad (GADNumber, GADResult, basicinfo_Number) VALUES('{0}', '{1}', '{2}');",
                    DataAdapter.GADNumber,
                    DataAdapter.ArrayToString(DataAdapter.GADResult),
                    DataAdapter.Number);
                }
                else
                {
                    sql = string.Format("UPDATE gad SET GADResult = '{1}' WHERE(GADNumber = '{0}') and(basicinfo_Number = '{2}');",
                   DataAdapter.GADNumber,
                   DataAdapter.ArrayToString(DataAdapter.GADResult),
                   DataAdapter.Number);

                }
                dataReader.Close();
                SQLiteAdapter.ExecuteNonQuery(sql);
                ((Button)sender).Background = new SolidColorBrush(Colors.LightGreen);
            }
            else
            {
                MessageBox.Show("请输入PHQ评估量表编号。");
            }
           


        }

    }
}
