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
using System.Data;


namespace Anka
{
    public partial class MainWindow 
    {
        private void BtBasicSave_Click(object sender, RoutedEventArgs e)
        {   
            using (SQLiteConnection conn = new SQLiteConnection(config.DataSource))
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    cmd.Connection = conn;
                    conn.Open();

                    SQLiteHelper sh = new SQLiteHelper(cmd);
                    var dicData = new Dictionary<string, object>();
                    BasicDataSave(dicData);
                    var dicCondition = new Dictionary<string, object>();
                    dicCondition["Number"] = DataAdapter.Number;
                    
                    try
                    {
                        sh.Update("basicinfo", dicData, dicCondition);
                    }
                    catch(SQLiteException ex)
                    {
                        MessageBox.Show(string.Format("数据更新错误。错误代码为:{0}", ex.ErrorCode), "数据更新错误");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }


                    // SQLiteAdapter.ExecuteNonQuery(sql);

                    ((Button)sender).Background = new SolidColorBrush(Colors.LightGreen);
        }
        private void BtBasicLoad_Click(object sender, RoutedEventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection(config.DataSource))
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    cmd.Connection = conn;
                    conn.Open();

                    SQLiteHelper sh = new SQLiteHelper(cmd);                   

                    try
                    {
                        DataTable dt = sh.Select(string.Format("select * from basicinfo where Number={0};", DataAdapter.Number));
                        BasicDataLoad(dt);
                    }
                    catch (SQLiteException ex)
                    {
                        MessageBox.Show(string.Format("数据更新错误。错误代码为:{0}", ex.ErrorCode), "数据更新错误");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }

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

        private CheckBox[] CbRiskClick()
        {

            CheckBox[] tempCheck = new CheckBox[13]{
                this.cbRisk1,
                this.cbRisk2,
                this.cbRisk3,
                this.cbRisk4,
                this.cbRisk5,
                this.cbRisk6,
                this.cbRisk7,
                this.cbRisk8,
                this.cbRisk9,
                this.cbRisk10,
                this.cbRisk11,
                this.cbRisk12,
                this.cbRisk13
            };

            return tempCheck;


        }

        private void BasicDataSave(Dictionary<string, object> dic)
        {
            
            dic["Killip"] = txKillip.Text;
            dic["EF"] = txEF.Text;
            dic["LV"] = txLV.Text;
            dic["BasicOther"] = txBasicOther.Text;

            string BasicRisk = "";
            foreach (CheckBox temp in CbRiskClick())
            {
                BasicRisk += (temp.IsChecked == true ? "1" : "0");
            }

            dic["BasicRisk"] = BasicRisk;
            dic["RiskOther"] = txRisk13.Text;

            if (DataAdapter.IsNumber(this.txPCI.Text) == true)
                dic["PCI"] = Convert.ToInt32(this.txPCI.Text);

            if (DataAdapter.IsNumber(this.txRS.Text) == true)
                dic["ResidualStenosis"] = Convert.ToInt32(this.txRS.Text);

            if (this.rbDCL.IsChecked == true)
            {
                dic["DominantCoronary"] = -1;
            }
            else if (this.rbDCB.IsChecked == true)
            {
                dic["DominantCoronary"] = 0;
            }
            else if (this.rbDCR.IsChecked == true)
            {
                dic["DominantCoronary"] = 1;
            }

            if (this.cbCC.IsChecked == true)
            {
                dic["CollatCirc"] = true;
            }
            else
            {
                dic["CollatCirc"] = false;
            }           

        }

        private void BasicDataLoad(DataTable dt)
        {
            DataRow dr = dt.Rows[0];
            txKillip.Text = dr["Killip"] == System.DBNull.Value ? "": dr["Killip"].ToString();
            txEF.Text = dr["EF"] == System.DBNull.Value ? "" : dr["EF"].ToString();
            txLV.Text = dr["LV"] == System.DBNull.Value ? "" : dr["LV"].ToString();
            txBasicOther.Text = dr["BasicOther"] == System.DBNull.Value ? "" : dr["BasicOther"].ToString();
            

            string BasicRisk = dr["BasicRisk"] == System.DBNull.Value ? "" : dr["BasicRisk"].ToString();
            int i = 0;
            foreach (char temp in BasicRisk)
            {
                CbRiskClick()[i].IsChecked=temp == '1' ? true:false;
                i++;
            }

            txRisk13.Text = dr["RiskOther"] == System.DBNull.Value ? "" : dr["RiskOther"].ToString();
            txPCI.Text = dr["PCI"] == System.DBNull.Value ? "" : dr["PCI"].ToString();
            txRS.Text = dr["ResidualStenosis"] == System.DBNull.Value ? "" : dr["ResidualStenosis"].ToString();

            if(dr["DominantCoronary"] != System.DBNull.Value)
            {
                switch (Convert.ToInt32( dr["DominantCoronary"]))
                {
                    case -1:
                        rbDCL.IsChecked = true;
                        break;
                    case 0:
                        rbDCB.IsChecked = true;
                        break;
                    case 1:
                        rbDCR.IsChecked = true;
                        break;
                }
                    
            }

            if (Convert.ToBoolean(dr["CollatCirc"]) == true)
            {
                cbCC.IsChecked = true;
                cbCC.Content = "侧枝循环：有";
            }
            else
            {
                cbCC.IsChecked = false;
                cbCC.Content = "侧枝循环：无";
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
