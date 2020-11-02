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
        private void BtSPPBSave_Click(object sender, RoutedEventArgs e)
        {
            bool SaveCheck = true;

            if(config.IsNumeric(txSPPB41.Text)==false)
            {
                txSPPB41.Background = new SolidColorBrush(Colors.Yellow);
                SaveCheck = false;
            }
            else
            {
                txSPPB41.Background = new SolidColorBrush(Colors.White);
            }
            if (config.IsNumeric(txSPPB42.Text) == false)
            {
                txSPPB42.Background = new SolidColorBrush(Colors.Yellow);
                SaveCheck = false;
            }
            else
            {
                txSPPB42.Background = new SolidColorBrush(Colors.White);
            }
            if (config.IsNumeric(txSPPB51.Text) == false)
            {
                txSPPB51.Background = new SolidColorBrush(Colors.Yellow);
                SaveCheck = false;
            }
            else
            {
                txSPPB51.Background = new SolidColorBrush(Colors.White);
            }
            if (config.IsNumeric(txSPPB52.Text) == false)
            {
                txSPPB52.Background = new SolidColorBrush(Colors.Yellow);
                SaveCheck = false;
            }
            else
            {
                txSPPB52.Background = new SolidColorBrush(Colors.White);
            }

            if (config.IsNumeric(txSize1.Text) == false)
            {
                txSize1.Background = new SolidColorBrush(Colors.Yellow);
                SaveCheck = false;
            }
            else
            {
                txSize1.Background = new SolidColorBrush(Colors.White);
            }

            if (config.IsNumeric(txSize2.Text) == false)
            {
                txSize2.Background = new SolidColorBrush(Colors.Yellow);
                SaveCheck = false;
            }
            else
            {
                txSize2.Background = new SolidColorBrush(Colors.White);
            }

            if (config.IsNumeric(txSize3.Text) == false)
            {
                txSize3.Background = new SolidColorBrush(Colors.Yellow);
                SaveCheck = false;
            }
            else
            {
                txSize3.Background = new SolidColorBrush(Colors.White);
            }

            if (config.IsNumeric(txSize5.Text) == false)
            {
                txSize5.Background = new SolidColorBrush(Colors.Yellow);
                SaveCheck = false;
            }
            else
            {
                txSize5.Background = new SolidColorBrush(Colors.White);
            }
            if (config.IsNumeric(txSize6.Text) == false)
            {
                txSize6.Background = new SolidColorBrush(Colors.Yellow);
                SaveCheck = false;
            }
            else
            {
                txSize6.Background = new SolidColorBrush(Colors.White);
            }
            if (config.IsNumeric(txSize7.Text) == false)
            {
                txSize7.Background = new SolidColorBrush(Colors.Yellow);
                SaveCheck = false;
            }
            else
            {
                txSize7.Background = new SolidColorBrush(Colors.White);
            }
            if (config.IsNumeric(txSize8.Text) == false)
            {
                txSize8.Background = new SolidColorBrush(Colors.Yellow);
                SaveCheck = false;
            }
            else
            {
                txSize8.Background = new SolidColorBrush(Colors.White);
            }
            if (config.IsNumeric(txVitals1.Text) == false)
            {
                txVitals1.Background = new SolidColorBrush(Colors.Yellow);
                SaveCheck = false;
            }
            else
            {
                txVitals1.Background = new SolidColorBrush(Colors.White);
            }
            if (config.IsNumeric(txVitals2.Text) == false)
            {
                txVitals2.Background = new SolidColorBrush(Colors.Yellow);
                SaveCheck = false;
            }
            else
            {
                txVitals2.Background = new SolidColorBrush(Colors.White);
            }
            if (config.IsNumeric(txVitals3.Text) == false)
            {
                txVitals3.Background = new SolidColorBrush(Colors.Yellow);
                SaveCheck = false;
            }
            else
            {
                txVitals3.Background = new SolidColorBrush(Colors.White);
            }
            if (config.IsNumeric(txVitals4.Text) == false)
            {
                txVitals4.Background = new SolidColorBrush(Colors.Yellow);
                SaveCheck = false;
            }
            else
            {
                txVitals4.Background = new SolidColorBrush(Colors.White);
            }
            if (config.IsNumeric(txVitals5.Text) == false)
            {
                txVitals5.Background = new SolidColorBrush(Colors.Yellow);
                SaveCheck = false;
            }
            else
            {
                txVitals5.Background = new SolidColorBrush(Colors.White);
            }

            if (config.IsNumeric(txGripStrength1.Text) == false)
            {
                txGripStrength1.Background = new SolidColorBrush(Colors.Yellow);
                SaveCheck = false;
            }
            else
            {
                txGripStrength1.Background = new SolidColorBrush(Colors.White);
            }
            if (config.IsNumeric(txGripStrength2.Text) == false)
            {
                txGripStrength2.Background = new SolidColorBrush(Colors.Yellow);
                SaveCheck = false;
            }
            else
            {
                txGripStrength2.Background = new SolidColorBrush(Colors.White);
            }
            if (config.IsNumeric(txGripStrength3.Text) == false)
            {
                txGripStrength3.Background = new SolidColorBrush(Colors.Yellow);
                SaveCheck = false;
            }
            else
            {
                txGripStrength3.Background = new SolidColorBrush(Colors.White);
            }
            if (config.IsNumeric(txGripStrength4.Text) == false)
            {
                txGripStrength4.Background = new SolidColorBrush(Colors.Yellow);
                SaveCheck = false;
            }
            else
            {
                txGripStrength4.Background = new SolidColorBrush(Colors.White);
            }
            if (config.IsNumeric(txLapStrength1.Text) == false)
            {
                txLapStrength1.Background = new SolidColorBrush(Colors.Yellow);
                SaveCheck = false;
            }
            else
            {
                txLapStrength1.Background = new SolidColorBrush(Colors.White);
            }
            if (config.IsNumeric(txLapStrength2.Text) == false)
            {
                txLapStrength2.Background = new SolidColorBrush(Colors.Yellow);
                SaveCheck = false;
            }
            else
            {
                txLapStrength2.Background = new SolidColorBrush(Colors.White);
            }
            if (config.IsNumeric(txLapStrength3.Text) == false)
            {
                txLapStrength3.Background = new SolidColorBrush(Colors.Yellow);
                SaveCheck = false;
            }
            else
            {
                txLapStrength3.Background = new SolidColorBrush(Colors.White);
            }
            if (config.IsNumeric(txLapStrength4.Text) == false)
            {
                txLapStrength4.Background = new SolidColorBrush(Colors.Yellow);
                SaveCheck = false;
            }
            else
            {
                txLapStrength4.Background = new SolidColorBrush(Colors.White);
            }


            if (SaveCheck==true)
            {

            if (this.txSPPBLoop.Text.Trim().Length > 0)
            {

                using (SQLiteConnection conn = new SQLiteConnection(config.DataSource))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand())
                    {
                        cmd.Connection = conn;
                        conn.Open();

                        SQLiteHelper sh = new SQLiteHelper(cmd);
                        var dicData = new Dictionary<string, object>();

                        SPPBDataSave(dicData);
                        BalanceCapabilitySave(dicData);
                        SizeResultSave(dicData);
                        VitalsResultSave(dicData);
                        GripStrengthResultSave(dicData);
                        LapStrengthResultSave(dicData);

                        string SPPBNumber = DataAdapter.Number + "-" + this.txSPPBLoop.Text.Trim();

                        string sql = string.Format("SELECT * FROM sppb where SPPBNumber='{0}';", SPPBNumber);
                        DataTable dt = sh.Select(sql);
                        try
                        {
                            if (dt.Rows.Count > 0)
                            {
                                var dicCondition = new Dictionary<string, object>();
                                dicCondition["basicinfo_Number"] = DataAdapter.Number;
                                dicCondition["SPPBNumber"] = SPPBNumber;
                                sh.Update("sppb", dicData, dicCondition);
                            }
                            else
                            {
                                dicData["SPPBNumber"] = SPPBNumber;
                                dicData["basicinfo_Number"] = DataAdapter.Number;
                                sh.Insert("sppb", dicData);
                                txSPPBLoop.Items.Add(this.txSPPBLoop.Text.Trim());
                            }
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
                ((Button)sender).Background = new SolidColorBrush(Colors.LightGreen);
            }
            else
            {
                MessageBox.Show("请输入测试编号。");
            }
            }
            else
            {
                ((Button)sender).Background = new SolidColorBrush(Colors.Red);
            }

        }

        private void BtSPPBLoad_Click(object sender, RoutedEventArgs e)
        {
            if (this.txSPPBLoop.Text.Trim().Length > 0)
            {
                string SPPBNumber = DataAdapter.Number + "-" + this.txSPPBLoop.Text.Trim();
                using (SQLiteConnection conn = new SQLiteConnection(config.DataSource))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand())
                    {
                        cmd.Connection = conn;
                        conn.Open();

                        SQLiteHelper sh = new SQLiteHelper(cmd);

                        try
                        {
                            DataTable dt = sh.Select(string.Format("select * from sppb where SPPBNumber='{0}';", SPPBNumber));
                            if (dt.Rows.Count > 0)
                            {
                                SPPBDataLoad(dt);
                                BalanceCapabilityLoad(dt);
                                SizeResultLoad(dt);
                                VitalsResultLoad(dt);
                                GripStrengthResultLoad(dt);
                                LapStrengthResultLoad(dt);
                            }
                            else
                            {
                                MessageBox.Show("该编号数据不存在。");
                            }

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
            else
            {
                MessageBox.Show("请输入项目编号。");
            }

        }

        private void SPPBDataSave(Dictionary<string, object> dic)
        {
            if (this.rbSPPBYes1.IsChecked == true)
            {
                dic["BalanceTesting1"] = "A-0-0";
            }
            if (this.rbSPPBNo1.IsChecked == true)
            {
                dic["BalanceTesting1"] = "B-" + this.txSPPB11.Text.Trim() + "-" + this.txSPPB12.Text.Trim();
            }

            if (this.rbSPPBYes2.IsChecked == true)
            {
                dic["BalanceTesting2"] = "A-0-0";
            }
            if (this.rbSPPBNo2.IsChecked == true)
            {
                dic["BalanceTesting2"] = "B-" + this.txSPPB21.Text.Trim() + "-" + this.txSPPB22.Text.Trim();
            }

            if (this.rbSPPBYes3.IsChecked == true)
            {
                dic["BalanceTesting3"] = "A-0-0";
            }
            if (this.rbSPPBNo3.IsChecked == true)
            {
                dic["BalanceTesting3"] = "B-" + this.txSPPB31.Text.Trim() + "-" + this.txSPPB32.Text.Trim();
            }

            if (this.rbSPPBYes4.IsChecked == true)
            {
                dic["walkingTesting1"] = "A-" + this.txSPPB41.Text.Trim() + "-" + this.txSPPB42.Text.Trim();
                dic["walkingTesting2"] = "A-" + this.txSPPB51.Text.Trim() + "-" + this.txSPPB52.Text.Trim();
            }
            if (this.rbSPPBNo4.IsChecked == true)
            {
                dic["walkingTesting1"] = "B-" + this.txSPPB41.Text.Trim() + "-" + this.txSPPB42.Text.Trim();
                dic["walkingTesting2"] = "B-" + this.txSPPB51.Text.Trim() + "-" + this.txSPPB52.Text.Trim();
            }

            if (this.rbSPPBYes5.IsChecked == true)
            {
                dic["SitUpTesting"] = "A-" + this.txSPPB61.Text.Trim() + "-" + this.txSPPB62.Text.Trim();
            }
            if (this.rbSPPBNo5.IsChecked == true)
            {
                dic["SitUpTesting"] = "B-0-" + this.txSPPB7.Text.Trim();
            }

        }

        private void SPPBDataLoad(DataTable dt)
        {
            DataRow dr = dt.Rows[0];
            rbSPPBYes1.IsChecked = false; rbSPPBNo1.IsChecked = false;
            if (dr["BalanceTesting1"] != System.DBNull.Value)
            {
                txSPPB11.Text = ""; txSPPB12.Text = "";
                string sBalanceTesting1 = dr["BalanceTesting1"] == System.DBNull.Value ? "" : dr["BalanceTesting1"].ToString();
                string[] BalanceTesting1Array = sBalanceTesting1.Split('-');
                switch (BalanceTesting1Array[0])
                {
                    case "A":
                        rbSPPBYes1.IsChecked = true;                       
                        break;
                    case "B":
                        rbSPPBNo1.IsChecked = true;
                        if (BalanceTesting1Array.Length > 1)
                            txSPPB11.Text = BalanceTesting1Array[1];
                        else
                            txSPPB11.Text = "";
                        if (BalanceTesting1Array.Length > 2)
                            txSPPB12.Text = BalanceTesting1Array[2];
                        else
                            txSPPB12.Text = "";
                        break;                  
                }
            }
            rbSPPBYes2.IsChecked = false; rbSPPBNo2.IsChecked = false;
            if (dr["BalanceTesting2"] != System.DBNull.Value)
            {
                txSPPB21.Text = ""; txSPPB22.Text = "";
                string sBalanceTesting2 = dr["BalanceTesting2"] == System.DBNull.Value ? "" : dr["BalanceTesting2"].ToString();
                string[] BalanceTesting2Array = sBalanceTesting2.Split('-');
                switch (BalanceTesting2Array[0])
                {
                    case "A":
                        rbSPPBYes2.IsChecked = true;
                        break;
                    case "B":
                        rbSPPBNo2.IsChecked = true;
                        if (BalanceTesting2Array.Length > 1)
                            txSPPB21.Text = BalanceTesting2Array[1];
                        else
                            txSPPB21.Text = "";
                        if (BalanceTesting2Array.Length > 2)
                            txSPPB22.Text = BalanceTesting2Array[2];
                        else
                            txSPPB22.Text = "";
                        break;
                }
            }
            rbSPPBYes3.IsChecked = false; rbSPPBNo3.IsChecked = false;
            if (dr["BalanceTesting3"] != System.DBNull.Value)
            {
                txSPPB31.Text = ""; txSPPB32.Text = "";
                string sBalanceTesting3 = dr["BalanceTesting3"] == System.DBNull.Value ? "" : dr["BalanceTesting3"].ToString();
                string[] BalanceTesting3Array = sBalanceTesting3.Split('-');
                switch (BalanceTesting3Array[0])
                {
                    case "A":
                        rbSPPBYes3.IsChecked = true;
                        break;
                    case "B":
                        rbSPPBNo3.IsChecked = true;
                        if (BalanceTesting3Array.Length > 1)
                            txSPPB31.Text = BalanceTesting3Array[1];
                        else
                            txSPPB31.Text = "";
                        if (BalanceTesting3Array.Length > 2)
                            txSPPB32.Text = BalanceTesting3Array[2];
                        else
                            txSPPB32.Text = "";
                        break;
                }
            }
            rbSPPBYes4.IsChecked = false; rbSPPBNo4.IsChecked = false;
            if (dr["walkingTesting1"] != System.DBNull.Value)
            {
                txSPPB41.Text = ""; txSPPB42.Text = ""; txSPPB51.Text = ""; txSPPB52.Text = "";
                string swalkingTesting1 = dr["walkingTesting1"] == System.DBNull.Value ? "" : dr["walkingTesting1"].ToString();
                string swalkingTesting2 = dr["walkingTesting2"] == System.DBNull.Value ? "" : dr["walkingTesting2"].ToString();
                string[] walkingTesting1Array = swalkingTesting1.Split('-');
                string[] walkingTesting2Array = swalkingTesting2.Split('-');
                switch (walkingTesting1Array[0])
                {
                    case "A":
                        rbSPPBYes4.IsChecked = true;                       
                        break;
                    case "B":
                        rbSPPBNo4.IsChecked = true;                       
                        break;
                }

                if (walkingTesting1Array.Length > 1)
                    txSPPB41.Text = walkingTesting1Array[1];
                else
                    txSPPB41.Text = "";
                if (walkingTesting1Array.Length > 2)
                    txSPPB42.Text = walkingTesting1Array[2];
                else
                    txSPPB42.Text = "";

                if (walkingTesting2Array.Length > 1)
                    txSPPB51.Text = walkingTesting2Array[1];
                else
                    txSPPB51.Text = "";
                if (walkingTesting2Array.Length > 2)
                    txSPPB52.Text = walkingTesting2Array[2];
                else
                    txSPPB52.Text = "";
            }

            rbSPPBYes5.IsChecked = false; rbSPPBNo5.IsChecked = false;
            if (dr["SitUpTesting"] != System.DBNull.Value)
            {
                txSPPB61.Text = ""; txSPPB62.Text = ""; txSPPB7.Text = ""; 
                string sSitUpTesting = dr["SitUpTesting"] == System.DBNull.Value ? "" : dr["SitUpTesting"].ToString();                
                string[] SitUpTestingArray = sSitUpTesting.Split('-');                
                switch (SitUpTestingArray[0])
                {
                    case "A":
                        rbSPPBYes5.IsChecked = true;
                        if (SitUpTestingArray.Length > 1)
                            txSPPB61.Text = SitUpTestingArray[1];
                        else
                            txSPPB61.Text = "";
                        if (SitUpTestingArray.Length > 2)
                            txSPPB62.Text = SitUpTestingArray[2];
                        else
                            txSPPB62.Text = "";
                        break;
                    case "B":
                        rbSPPBNo5.IsChecked = true;
                        if (SitUpTestingArray.Length > 2)
                            txSPPB7.Text = SitUpTestingArray[2];
                        else
                            txSPPB7.Text = "";
                        break;
                }

               
            }
        }
        private void BalanceCapabilitySave(Dictionary<string, object> dic)
        {
            if (this.rbBalanceYes.IsChecked == true)
            {
                dic["TUG"] = "A-" + this.txBalance11.Text.Trim() + "-" + this.txBalance12.Text.Trim();
            }
            if (this.rbBalanceNo.IsChecked == true)
            {
                dic["TUG"] = "B-" + this.txBalance11.Text.Trim() + "-" + this.txBalance12.Text.Trim();
            }
            dic["FRTLeft1"] = this.txBalance21.Text.Trim();
            dic["FRTLeft2"] = this.txBalance22.Text.Trim();
            dic["FRTRight1"] = this.txBalance31.Text.Trim();
            dic["FRTRight2"] = this.txBalance32.Text.Trim();
            dic["SFO1"] = this.txBalance41.Text.Trim();
            dic["SFO2"] = this.txBalance42.Text.Trim();
            dic["OneFootLeft1"] = this.txBalance51.Text.Trim();
            dic["OneFootLeft2"] = this.txBalance52.Text.Trim();
            dic["OneFootRight1"] = this.txBalance61.Text.Trim();
            dic["OneFootRight2"] = this.txBalance62.Text.Trim();
        }

        private void BalanceCapabilityLoad(DataTable dt)
        {
            DataRow dr = dt.Rows[0];

            rbBalanceYes.IsChecked = false; rbBalanceNo.IsChecked = false;
            if (dr["TUG"] != System.DBNull.Value)
            {
                txBalance11.Text = ""; txBalance12.Text = "";
                string sTUG = dr["TUG"] == System.DBNull.Value ? "" : dr["TUG"].ToString();
                string[] TUGArray = sTUG.Split('-');
                switch (TUGArray[0])
                {
                    case "A":
                        rbBalanceYes.IsChecked = true;
                        break;
                    case "B":
                        rbBalanceNo.IsChecked = true;                        
                        break;
                }
                if (TUGArray.Length > 1)
                    txBalance11.Text = TUGArray[1];
                else
                    txBalance11.Text = "";
                if (TUGArray.Length > 2)
                    txBalance12.Text = TUGArray[2];
                else
                    txBalance12.Text = "";
            }

            txBalance21.Text = dr["FRTLeft1"] == System.DBNull.Value ? "" : dr["FRTLeft1"].ToString();
            txBalance22.Text = dr["FRTLeft2"] == System.DBNull.Value ? "" : dr["FRTLeft2"].ToString();
            txBalance31.Text = dr["FRTRight1"] == System.DBNull.Value ? "" : dr["FRTRight1"].ToString();
            txBalance32.Text = dr["FRTRight2"] == System.DBNull.Value ? "" : dr["FRTRight2"].ToString();
            txBalance41.Text = dr["SFO1"] == System.DBNull.Value ? "" : dr["SFO1"].ToString();
            txBalance42.Text = dr["SFO2"] == System.DBNull.Value ? "" : dr["SFO2"].ToString();
            txBalance51.Text = dr["OneFootLeft1"] == System.DBNull.Value ? "" : dr["OneFootLeft1"].ToString();
            txBalance52.Text = dr["OneFootLeft2"] == System.DBNull.Value ? "" : dr["OneFootLeft2"].ToString();
            txBalance61.Text = dr["OneFootRight1"] == System.DBNull.Value ? "" : dr["OneFootRight1"].ToString();
            txBalance62.Text = dr["OneFootRight2"] == System.DBNull.Value ? "" : dr["OneFootRight2"].ToString();

        }
        private void SizeResultSave(Dictionary<string, object> dic)
        {
            dic["Hight"] = this.txSize1.Text.Trim();
            dic["Weight"] = this.txSize2.Text.Trim();
            dic["Waistline"] = this.txSize3.Text.Trim();
            dic["Hipline"] = this.txSize4.Text.Trim();
            dic["ArmlineLeft"] = this.txSize5.Text.Trim();
            dic["ArmlineRight"] = this.txSize6.Text.Trim();
            dic["LeglineLeft"] = this.txSize7.Text.Trim();
            dic["LeglineRight"] = this.txSize8.Text.Trim();
        }
        private void SizeResultLoad(DataTable dt)
        {
            DataRow dr = dt.Rows[0];
            txSize1.Text = dr["Hight"] == System.DBNull.Value ? "" : dr["Hight"].ToString();
            txSize2.Text = dr["Weight"] == System.DBNull.Value ? "" : dr["Weight"].ToString();
            txSize3.Text = dr["Waistline"] == System.DBNull.Value ? "" : dr["Waistline"].ToString();
            txSize4.Text = dr["Hipline"] == System.DBNull.Value ? "" : dr["Hipline"].ToString();
            txSize5.Text = dr["ArmlineLeft"] == System.DBNull.Value ? "" : dr["ArmlineLeft"].ToString();
            txSize6.Text = dr["ArmlineRight"] == System.DBNull.Value ? "" : dr["ArmlineRight"].ToString();
            txSize7.Text = dr["LeglineLeft"] == System.DBNull.Value ? "" : dr["LeglineLeft"].ToString();
            txSize8.Text = dr["LeglineRight"] == System.DBNull.Value ? "" : dr["LeglineRight"].ToString();

        }

        private void VitalsResultSave(Dictionary<string, object> dic)
        {
            dic["BloodPressureLower"] = this.txVitals1.Text.Trim();
            dic["BloodPressureUpper"] = this.txVitals2.Text.Trim();
            dic["HeartRate"] = this.txVitals3.Text.Trim();
            dic["Temperature"] = this.txVitals4.Text.Trim();
            dic["Breathe"] = this.txVitals5.Text.Trim();
        }
        private void VitalsResultLoad(DataTable dt)
        {
            DataRow dr = dt.Rows[0];
            txVitals1.Text = dr["BloodPressureLower"] == System.DBNull.Value ? "" : dr["BloodPressureLower"].ToString();
            txVitals2.Text = dr["BloodPressureUpper"] == System.DBNull.Value ? "" : dr["BloodPressureUpper"].ToString();
            txVitals3.Text = dr["HeartRate"] == System.DBNull.Value ? "" : dr["HeartRate"].ToString();
            txVitals4.Text = dr["Temperature"] == System.DBNull.Value ? "" : dr["Temperature"].ToString();
            txVitals5.Text = dr["Breathe"] == System.DBNull.Value ? "" : dr["Breathe"].ToString();
        }
        private void GripStrengthResultSave(Dictionary<string, object> dic)
        {
            dic["GripStrengthLeft1"] = this.txGripStrength1.Text.Trim();
            dic["GripStrengthLeft2"] = this.txGripStrength2.Text.Trim();
            dic["GripStrengthRight1"] = this.txGripStrength3.Text.Trim();
            dic["GripStrengthRight2"] = this.txGripStrength4.Text.Trim();
            if (this.rbGripStrengthYes1.IsChecked == true)
            {
                dic["LeftHandHurt"] = true;
            }
            else if (this.rbGripStrengthNo1.IsChecked == true)
            {
                dic["LeftHandHurt"] = false;
            }
            else
            {
                //MessageBox.Show("请确定左手外伤情况。");
            }

            if (this.rbGripStrengthYes2.IsChecked == true)
            {
                dic["RightHandHurt"] = true;
            }
            else if (this.rbGripStrengthNo2.IsChecked == true)
            {
                dic["RightHandHurt"] = false;
            }
            else
            {
                //MessageBox.Show("请确定右手外伤情况。");
            }

        }
        private void GripStrengthResultLoad(DataTable dt)
        {
            DataRow dr = dt.Rows[0];
            txGripStrength1.Text = dr["GripStrengthLeft1"] == System.DBNull.Value ? "" : dr["GripStrengthLeft1"].ToString();
            txGripStrength2.Text = dr["GripStrengthLeft2"] == System.DBNull.Value ? "" : dr["GripStrengthLeft2"].ToString();
            txGripStrength3.Text = dr["GripStrengthRight1"] == System.DBNull.Value ? "" : dr["GripStrengthRight1"].ToString();
            txGripStrength4.Text = dr["GripStrengthRight2"] == System.DBNull.Value ? "" : dr["GripStrengthRight2"].ToString();
            if (dr["LeftHandHurt"]!= System.DBNull.Value)
            {
                switch(Convert.ToBoolean(dr["LeftHandHurt"]))
                {
                    case true:
                        rbGripStrengthYes1.IsChecked = true;
                        rbGripStrengthNo1.IsChecked = false;
                        break;
                    case false:
                        rbGripStrengthYes1.IsChecked = false;
                        rbGripStrengthNo1.IsChecked = true;
                        break;
                    default:
                        rbGripStrengthYes1.IsChecked = false;
                        rbGripStrengthNo1.IsChecked = false;
                        break;
                }
            }
            if (dr["RightHandHurt"] != System.DBNull.Value)
            {
                switch (Convert.ToBoolean(dr["RightHandHurt"]))
                {
                    case true:
                        rbGripStrengthYes2.IsChecked = true;
                        rbGripStrengthNo2.IsChecked = false;
                        break;
                    case false:
                        rbGripStrengthYes2.IsChecked = false;
                        rbGripStrengthNo2.IsChecked = true;
                        break;
                    default:
                        rbGripStrengthYes2.IsChecked = false;
                        rbGripStrengthNo2.IsChecked = false;
                        break;
                }
            }

        }
        private void LapStrengthResultSave(Dictionary<string, object> dic)
        {
            dic["LapStrengthLeft1"] = this.txLapStrength1.Text.Trim();
            dic["LapStrengthLeft2"] = this.txLapStrength2.Text.Trim();
            dic["LapStrengthRight1"] = this.txLapStrength3.Text.Trim();
            dic["LapStrengthRight2"] = this.txLapStrength4.Text.Trim();
            if (this.rbLapStrengthYes1.IsChecked == true)
            {
                dic["LeftLapHurt"] = true;
            }
            else if (this.rbLapStrengthNo1.IsChecked == true)
            {
                dic["LeftLapHurt"] = false;
            }
            else
            {
               // MessageBox.Show("请确定伸膝力左外伤等情况。");
            }

            if (this.rbLapStrengthYes2.IsChecked == true)
            {
                dic["RightLapHurt"] = true;
            }
            else if (this.rbLapStrengthNo2.IsChecked == true)
            {
                dic["RightLapHurt"] = false;
            }
            else
            {
               // MessageBox.Show("请确定伸膝力右外伤情况。");
            }

        }
        private void LapStrengthResultLoad(DataTable dt)
        {
            DataRow dr = dt.Rows[0];
            txLapStrength1.Text = dr["LapStrengthLeft1"] == System.DBNull.Value ? "" : dr["LapStrengthLeft1"].ToString();
            txLapStrength2.Text = dr["LapStrengthLeft2"] == System.DBNull.Value ? "" : dr["LapStrengthLeft2"].ToString();
            txLapStrength3.Text = dr["LapStrengthRight1"] == System.DBNull.Value ? "" : dr["LapStrengthRight1"].ToString();
            txLapStrength4.Text = dr["LapStrengthRight1"] == System.DBNull.Value ? "" : dr["LapStrengthRight1"].ToString();
            if (dr["LeftLapHurt"] != System.DBNull.Value)
            {
                switch (Convert.ToBoolean(dr["LeftLapHurt"]))
                {
                    case true:
                        rbLapStrengthYes1.IsChecked = true;
                        rbLapStrengthNo1.IsChecked = false;
                        break;
                    case false:
                        rbLapStrengthYes1.IsChecked = false;
                        rbLapStrengthNo1.IsChecked = true;
                        break;
                    default:
                        rbLapStrengthYes1.IsChecked = false;
                        rbLapStrengthNo1.IsChecked = false;
                        break;
                }
            }
            if (dr["RightLapHurt"] != System.DBNull.Value)
            {
                switch (Convert.ToBoolean(dr["RightLapHurt"]))
                {
                    case true:
                        rbLapStrengthYes2.IsChecked = true;
                        rbLapStrengthNo2.IsChecked = false;
                        break;
                    case false:
                        rbLapStrengthYes2.IsChecked = false;
                        rbLapStrengthNo2.IsChecked = true;
                        break;
                    default:
                        rbLapStrengthYes2.IsChecked = false;
                        rbLapStrengthNo2.IsChecked = false;
                        break;
                }
            }

        }

        private void RbSPPB1_Click(object sender, RoutedEventArgs e)
        {
            if (this.rbSPPBNo1.IsChecked == true)
            {
                this.txSPPB11.IsEnabled = true;
                this.txSPPB12.IsEnabled = true;
            }
            else
            {
                this.txSPPB11.Text = "";
                this.txSPPB12.Text = "";
                this.txSPPB11.IsEnabled = false;
                this.txSPPB12.IsEnabled = false;
            }
        }

        private void RbSPPB2_Click(object sender, RoutedEventArgs e)
        {
            if (this.rbSPPBNo2.IsChecked == true)
            {
                this.txSPPB21.IsEnabled = true;
                this.txSPPB22.IsEnabled = true;
            }
            else
            {
                this.txSPPB21.Text = "";
                this.txSPPB22.Text = "";
                this.txSPPB21.IsEnabled = false;
                this.txSPPB22.IsEnabled = false;
            }
        }

        private void RbSPPB3_Click(object sender, RoutedEventArgs e)
        {
            if (this.rbSPPBNo3.IsChecked == true)
            {
                this.txSPPB31.IsEnabled = true;
                this.txSPPB32.IsEnabled = true;
            }
            else
            {
                this.txSPPB31.Text = "";
                this.txSPPB32.Text = "";
                this.txSPPB31.IsEnabled = false;
                this.txSPPB32.IsEnabled = false;
            }
        }

        private void RbSPPB5_Click(object sender, RoutedEventArgs e)
        {
            if (this.rbSPPBYes5.IsChecked == true)
            {
                this.txSPPB61.IsEnabled = true;
                this.txSPPB62.IsEnabled = true;
                this.txSPPB7.Text = "";
                this.txSPPB7.IsEnabled = false;
            }
            else
            {
                this.txSPPB61.Text = "";
                this.txSPPB62.Text = "";
                this.txSPPB61.IsEnabled = false;
                this.txSPPB62.IsEnabled = false;
                this.txSPPB7.IsEnabled = true;
            }
        }


    }
}
