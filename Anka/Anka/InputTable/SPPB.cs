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
                //DataAdapter.SPPBNumber = DataAdapter.Number + "-" + this.txSPPBLoop.Text.Trim();
                //SPPBDataSave();
                //BalanceCapabilitySave();
                //SizeResultSave();
                //VitalsResultSave();
                //GripStrengthResultSave();
                //LapStrengthResultSave();

                //string sql = string.Format("SELECT * FROM sppb where SPPBNumber='{0}';", DataAdapter.SPPBNumber);
                //SQLiteDataReader dataReader = SQLiteAdapter.ExecuteReader(sql);

                //if (dataReader.StepCount == 0)
                //{

                //     sql = string.Format("INSERT INTO sppb (SPPBNumber, BalanceTesting1, BalanceTesting2, BalanceTesting3, walkingTesting1, walkingTesting2, SitUpTesting" +
                //    ", TUG, FRTLeft1, FRTLeft2, FRTRight1, FRTRight2, SFO1, SFO2, OneFootLeft1, OneFootLeft2, OneFootRight1, OneFootRight2," +
                //    " Hight, Weight, Waistline, Hipline, ArmlineLeft, ArmlineRight, LeglineLeft, LeglineRight, BloodPressureUpper, BloodPressureLower, HeartRate,Temperature, Breathe" +
                //    ", LeftHandHurt, RightHandHurt, GripStrengthLeft1, GripStrengthRight1, GripStrengthLeft2, GripStrengthRight2, " +
                //    "LeftLapHurt, RightLapHurt,LapStrengthLeft1, LapStrengthRight1, LapStrengthLeft2, LapStrengthRight2, basicinfo_Number) VALUES" +
                //    " ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}',{31},{32},'{33}','{34}','{35}','{36}',{37},{38},'{39}','{40}','{41}','{42}','{43}'); ",
                //   DataAdapter.SPPBNumber,
                //   DataAdapter.SPPBResult.BalanceTesting1,
                //   DataAdapter.SPPBResult.BalanceTesting2,
                //   DataAdapter.SPPBResult.BalanceTesting3,
                //   DataAdapter.SPPBResult.walkingTesting1,
                //   DataAdapter.SPPBResult.walkingTesting2,
                //   DataAdapter.SPPBResult.SitUpTesting,
                //   DataAdapter.BalanceCapabilityResult.TUG,
                //   DataAdapter.BalanceCapabilityResult.FRTLeft1,
                //   DataAdapter.BalanceCapabilityResult.FRTLeft2,
                //   DataAdapter.BalanceCapabilityResult.FRTRight1,
                //   DataAdapter.BalanceCapabilityResult.FRTRight2,
                //   DataAdapter.BalanceCapabilityResult.SFO1,
                //   DataAdapter.BalanceCapabilityResult.SFO2,
                //   DataAdapter.BalanceCapabilityResult.OneFootLeft1,
                //   DataAdapter.BalanceCapabilityResult.OneFootLeft2,
                //   DataAdapter.BalanceCapabilityResult.OneFootRight1,
                //   DataAdapter.BalanceCapabilityResult.OneFootRight2,
                //   DataAdapter.SizeResult.Hight,
                //   DataAdapter.SizeResult.Weight,
                //   DataAdapter.SizeResult.Waistline,
                //   DataAdapter.SizeResult.Hipline,
                //   DataAdapter.SizeResult.ArmlineLeft,
                //   DataAdapter.SizeResult.ArmlineRight,
                //   DataAdapter.SizeResult.LeglineLeft,
                //   DataAdapter.SizeResult.LeglineRight,
                //   DataAdapter.VitalsResult.BloodPressureUpper,
                //   DataAdapter.VitalsResult.BloodPressureLower,
                //   DataAdapter.VitalsResult.HeartRate,
                //   DataAdapter.VitalsResult.Temperature,
                //   DataAdapter.VitalsResult.Breathe,
                //   DataAdapter.GripStrengthResult.LeftHandHurt,
                //   DataAdapter.GripStrengthResult.RightHandHurt,
                //   DataAdapter.GripStrengthResult.GripStrengthLeft1,
                //   DataAdapter.GripStrengthResult.GripStrengthRight1,
                //   DataAdapter.GripStrengthResult.GripStrengthLeft2,
                //   DataAdapter.GripStrengthResult.GripStrengthRight2,
                //   DataAdapter.LapStrengthResult.LeftLapHurt,
                //   DataAdapter.GripStrengthResult.RightHandHurt,
                //   DataAdapter.GripStrengthResult.GripStrengthLeft1,
                //   DataAdapter.GripStrengthResult.GripStrengthRight1,
                //   DataAdapter.GripStrengthResult.GripStrengthLeft2,
                //   DataAdapter.GripStrengthResult.GripStrengthRight2,
                //   DataAdapter.Number);
                //}
                //else
                //{

                //    sql = string.Format("UPDATE sppb SET BalanceTesting1 = '{1}', BalanceTesting2 = '{2}', BalanceTesting3 = '{3}', walkingTesting1 = '{4}', walkingTesting2 = '{5}', SitUpTesting = '{6}'" +
                //        ", TUG = '{7}', FRTLeft1 = '{8}', FRTLeft2 = '{9}', FRTRight1 = '{10}', FRTRight2 = '{11}', SFO1 = '{12}', SFO2 = '{13}', OneFootLeft1 = '{14}', OneFootLeft2 = '{15}', OneFootRight1 = '{16}', OneFootRight2 = '{17}'" +
                //        ", Hight = '{18}', Weight = '{19}', Waistline = '{20}', Hipline = '{21}', ArmlineLeft = '{22}', ArmlineRight = '{23}', LeglineLeft = '{24}', LeglineRight = '{25}', BloodPressureUpper = '{26}', BloodPressureLower = '{27}', HeartRate = '{28}', Temperature = '{29}', Breathe = '{30}'" +
                //        ", LeftHandHurt = {31}, RightHandHurt = {32}, GripStrengthLeft1 = '{33}', GripStrengthRight1 = '{34}', GripStrengthLeft2 = '{35}', GripStrengthRight2 = '{36}'" +
                //        ", LeftLapHurt = {37}, RightLapHurt = {38}, LapStrengthLeft1 = '{39}', LapStrengthRight1 = '{40}', LapStrengthLeft2 = '{41}', LapStrengthRight2 = '{42}'" +
                //        " WHERE (SPPBNumber = '{0}') and (basicinfo_Number = '{43}');",
                //       DataAdapter.SPPBNumber,
                //       DataAdapter.SPPBResult.BalanceTesting1,
                //       DataAdapter.SPPBResult.BalanceTesting2,
                //       DataAdapter.SPPBResult.BalanceTesting3,
                //       DataAdapter.SPPBResult.walkingTesting1,
                //       DataAdapter.SPPBResult.walkingTesting2,
                //       DataAdapter.SPPBResult.SitUpTesting,
                //       DataAdapter.BalanceCapabilityResult.TUG,
                //       DataAdapter.BalanceCapabilityResult.FRTLeft1,
                //       DataAdapter.BalanceCapabilityResult.FRTLeft2,
                //       DataAdapter.BalanceCapabilityResult.FRTRight1,
                //       DataAdapter.BalanceCapabilityResult.FRTRight2,
                //       DataAdapter.BalanceCapabilityResult.SFO1,
                //       DataAdapter.BalanceCapabilityResult.SFO2,
                //       DataAdapter.BalanceCapabilityResult.OneFootLeft1,
                //       DataAdapter.BalanceCapabilityResult.OneFootLeft2,
                //       DataAdapter.BalanceCapabilityResult.OneFootRight1,
                //       DataAdapter.BalanceCapabilityResult.OneFootRight2,
                //       DataAdapter.SizeResult.Hight,
                //       DataAdapter.SizeResult.Weight,
                //       DataAdapter.SizeResult.Waistline,
                //       DataAdapter.SizeResult.Hipline,
                //       DataAdapter.SizeResult.ArmlineLeft,
                //       DataAdapter.SizeResult.ArmlineRight,
                //       DataAdapter.SizeResult.LeglineLeft,
                //       DataAdapter.SizeResult.LeglineRight,
                //       DataAdapter.VitalsResult.BloodPressureUpper,
                //       DataAdapter.VitalsResult.BloodPressureLower,
                //       DataAdapter.VitalsResult.HeartRate,
                //       DataAdapter.VitalsResult.Temperature,
                //       DataAdapter.VitalsResult.Breathe,
                //       DataAdapter.GripStrengthResult.LeftHandHurt,
                //       DataAdapter.GripStrengthResult.RightHandHurt,
                //       DataAdapter.GripStrengthResult.GripStrengthLeft1,
                //       DataAdapter.GripStrengthResult.GripStrengthRight1,
                //       DataAdapter.GripStrengthResult.GripStrengthLeft2,
                //       DataAdapter.GripStrengthResult.GripStrengthRight2,
                //       DataAdapter.LapStrengthResult.LeftLapHurt,
                //       DataAdapter.GripStrengthResult.RightHandHurt,
                //       DataAdapter.GripStrengthResult.GripStrengthLeft1,
                //       DataAdapter.GripStrengthResult.GripStrengthRight1,
                //       DataAdapter.GripStrengthResult.GripStrengthLeft2,
                //       DataAdapter.GripStrengthResult.GripStrengthRight2,
                //       DataAdapter.Number);
                //}
                //dataReader.Close();
                //SQLiteAdapter.ExecuteNonQuery(sql);
                // DatabaseInfo.ModifyDatabase(sql1, sql2);
                ((Button)sender).Background = new SolidColorBrush(Colors.LightGreen);
            }
            else
            {
                MessageBox.Show("请输入测试编号。");
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
                dic["BalanceTesting1"] = "A -" + this.txSPPB11.Text.Trim() + "-" + this.txSPPB12.Text.Trim();
            }

            if (this.rbSPPBYes2.IsChecked == true)
            {
                dic["BalanceTesting2"] = "A -0-0";
            }
            if (this.rbSPPBNo2.IsChecked == true)
            {
                dic["BalanceTesting2"] = "A -" + this.txSPPB21.Text.Trim() + "-" + this.txSPPB22.Text.Trim();
            }

            if (this.rbSPPBYes3.IsChecked == true)
            {
                dic["BalanceTesting3"] = "A -0-0";
            }
            if (this.rbSPPBNo3.IsChecked == true)
            {
                dic["BalanceTesting3"] = "A -" + this.txSPPB31.Text.Trim() + "-" + this.txSPPB32.Text.Trim();
            }

            if (this.rbSPPBYes4.IsChecked == true)
            {
                dic["walkingTesting1"] = "A -" + this.txSPPB41.Text.Trim() + "-" + this.txSPPB42.Text.Trim();
                dic["walkingTesting2"] = "A -" + this.txSPPB51.Text.Trim() + "-" + this.txSPPB52.Text.Trim();
            }
            if (this.rbSPPBNo4.IsChecked == true)
            {
                dic["walkingTesting1"] = "B -" + this.txSPPB41.Text.Trim() + "-" + this.txSPPB42.Text.Trim();
                dic["walkingTesting2"] = "B -" + this.txSPPB51.Text.Trim() + "-" + this.txSPPB52.Text.Trim();
            }

            if (this.rbSPPBYes5.IsChecked == true)
            {
                dic["SitUpTesting"] = "A -" + this.txSPPB61.Text.Trim() + "-" + this.txSPPB62.Text.Trim();
            }
            if (this.rbSPPBNo5.IsChecked == true)
            {
                dic["SitUpTesting"] = "B -0-" + this.txSPPB7.Text.Trim();
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
                dic["TUG"] = "B -" + this.txBalance11.Text.Trim() + "-" + this.txBalance12.Text.Trim();
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
        private void SizeResultSave(Dictionary<string, object> dic)
        {
            dic["Hight"] = DataAdapter.IsNumber(this.txSize1.Text.Trim()) ? Convert.ToInt32(this.txSize1.Text.Trim()) : 0;
            dic["Weight"] = DataAdapter.IsNumber(this.txSize2.Text.Trim()) ? Convert.ToInt32(this.txSize2.Text.Trim()) : 0;
            dic["Waistline"] = DataAdapter.IsNumber(this.txSize3.Text.Trim()) ? Convert.ToInt32(this.txSize3.Text.Trim()) : 0;
            dic["Hipline"] = DataAdapter.IsNumber(this.txSize4.Text.Trim()) ? Convert.ToInt32(this.txSize4.Text.Trim()) : 0;
            dic["ArmlineLeft"] = DataAdapter.IsNumber(this.txSize5.Text.Trim()) ? Convert.ToInt32(this.txSize5.Text.Trim()) : 0;
            dic["ArmlineRight"] = DataAdapter.IsNumber(this.txSize6.Text.Trim()) ? Convert.ToInt32(this.txSize6.Text.Trim()) : 0;
            dic["LeglineLeft"] = DataAdapter.IsNumber(this.txSize7.Text.Trim()) ? Convert.ToInt32(this.txSize7.Text.Trim()) : 0;
            dic["LeglineRight"] = DataAdapter.IsNumber(this.txSize8.Text.Trim()) ? Convert.ToInt32(this.txSize8.Text.Trim()) : 0;
        }
        private void VitalsResultSave(Dictionary<string, object> dic)
        {
            dic["BloodPressureLower"] = DataAdapter.IsNumber(this.txVitals1.Text.Trim()) ? Convert.ToInt32(this.txVitals1.Text.Trim()) : 0;
            dic["BloodPressureUpper"] = DataAdapter.IsNumber(this.txVitals2.Text.Trim()) ? Convert.ToInt32(this.txVitals2.Text.Trim()) : 0;
            dic["HeartRate"] = DataAdapter.IsNumber(this.txVitals3.Text.Trim()) ? Convert.ToInt32(this.txVitals3.Text.Trim()) : 0;
            dic["Temperature"] = DataAdapter.IsNumber(this.txVitals4.Text.Trim()) ? Convert.ToDouble(this.txVitals4.Text.Trim()) : 0;
            dic["Breathe"] = DataAdapter.IsNumber(this.txVitals5.Text.Trim()) ? Convert.ToInt32(this.txVitals5.Text.Trim()) : 0;
        }
        private void GripStrengthResultSave(Dictionary<string, object> dic)
        {
            dic["GripStrengthLeft1"] = DataAdapter.IsNumber(this.txGripStrength1.Text.Trim()) ? Convert.ToDouble(this.txGripStrength1.Text.Trim()) : 0;
            dic["GripStrengthLeft2"] = DataAdapter.IsNumber(this.txGripStrength2.Text.Trim()) ? Convert.ToDouble(this.txGripStrength2.Text.Trim()) : 0;
            dic["GripStrengthRight1"] = DataAdapter.IsNumber(this.txGripStrength3.Text.Trim()) ? Convert.ToDouble(this.txGripStrength3.Text.Trim()) : 0;
            dic["GripStrengthRight2"] = DataAdapter.IsNumber(this.txGripStrength4.Text.Trim()) ? Convert.ToDouble(this.txGripStrength4.Text.Trim()) : 0;
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
        private void LapStrengthResultSave(Dictionary<string, object> dic)
        {
            dic["LapStrengthLeft1"] = DataAdapter.IsNumber(this.txLapStrength1.Text.Trim()) ? Convert.ToDouble(this.txLapStrength1.Text.Trim()) : 0;
            dic["LapStrengthLeft2"] = DataAdapter.IsNumber(this.txLapStrength2.Text.Trim()) ? Convert.ToDouble(this.txLapStrength2.Text.Trim()) : 0;
            dic["LapStrengthRight1"] = DataAdapter.IsNumber(this.txLapStrength3.Text.Trim()) ? Convert.ToDouble(this.txLapStrength3.Text.Trim()) : 0;
            dic["LapStrengthRight2"] = DataAdapter.IsNumber(this.txLapStrength4.Text.Trim()) ? Convert.ToDouble(this.txLapStrength4.Text.Trim()) : 0;
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
