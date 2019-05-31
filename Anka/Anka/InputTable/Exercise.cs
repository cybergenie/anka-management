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
        private void BtExcerciseSave_Click(object sender, RoutedEventArgs e)
        {

            if (txExerciseLoop.Text.Trim().Length > 0)
            {
                using (SQLiteConnection conn = new SQLiteConnection(config.DataSource))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand())
                    {
                        cmd.Connection = conn;
                        conn.Open();

                        SQLiteHelper sh = new SQLiteHelper(cmd);
                        var dicData = new Dictionary<string, object>();

                        ExcerciseDataSave(dicData);
                        if(this.txExerciseLoop.Text.Trim().Length>0)
                        {
                            string ExerciseNumber = DataAdapter.Number + "-" + this.txExerciseLoop.Text.Trim();
                            string sql = string.Format("SELECT * FROM exercise where ExerciseNumber='{0}';", ExerciseNumber);
                            DataTable dt = sh.Select(sql);
                            try
                            {
                                if (dt.Rows.Count > 0)
                                {
                                    var dicCondition = new Dictionary<string, object>();
                                    dicCondition["basicinfo_Number"] = DataAdapter.Number;
                                    dicCondition["ExerciseNumber"] = ExerciseNumber;
                                    sh.Update("exercise", dicData, dicCondition);
                                }
                                else
                                {
                                    dicData["ExerciseNumber"] = ExerciseNumber;
                                    dicData["basicinfo_Number"] = DataAdapter.Number;
                                    sh.Insert("exercise", dicData);
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
                        else
                        {
                            MessageBox.Show("请输入项目编号。");
                        }
                        

                       

                    }
                }

                ((Button)sender).Background = new SolidColorBrush(Colors.LightGreen);
            }
            else
            {

                MessageBox.Show("请输入运动负荷记录表编号。");
            }
        }

        private void BtExcerciseLoad_Click(object sender, RoutedEventArgs e)
        {
            if (this.txExerciseLoop.Text.Trim().Length > 0)
            {
                string ExerciseNumber = DataAdapter.Number + "-" + this.txExerciseLoop.Text.Trim();
                using (SQLiteConnection conn = new SQLiteConnection(config.DataSource))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand())
                    {
                        cmd.Connection = conn;
                        conn.Open();

                        SQLiteHelper sh = new SQLiteHelper(cmd);

                        try
                        {
                            DataTable dt = sh.Select(string.Format("select * from exercise where ExerciseNumber=\"{0}\";", ExerciseNumber));
                            if(dt.Rows.Count>0)
                            {
                                ExcerciseDataLoad(dt);
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

        private void ExcerciseDataSave(Dictionary<string, object> dic)
        {
            

            string[] Date = new string[9] { this.dpBedup1.Text.Trim(),
                this.dpBedup2.Text.Trim(),
                this.dpBedup3.Text.Trim(),
                this.dpBedup4.Text.Trim(),
                this.dpInRoom1.Text.Trim(),
                this.dpInRoom2.Text.Trim(),
                this.dpOutRoom1.Text.Trim(),
                this.dpOutRoom2.Text.Trim(),
                this.dpOutRoom3.Text.Trim()
            };
            dic["Date"] = DataAdapter.ArrayToString(Date);
            int[] bpLower = new int[9]; int[] bpUpper = new int[9];
            string[] txBPUp = new string[9]{this.txBPBedUp1.Text.ToString(),
                this.txBPBedUp2.Text.ToString(),
                this.txBPBedUp3.Text.ToString(),
                this.txBPBedUp4.Text.ToString(),
                this.txBPInRoom1.Text.ToString(),
                this.txBPInRoom2.Text.ToString(),
                this.txBPOutRoom1.Text.ToString(),
                this.txBPOutRoom2.Text.ToString(),
                this.txBPOutRoom3.Text.ToString()
            };
            DataAdapter.GetBloodPressure(txBPUp, bpLower, bpUpper);
            
            dic["BloodPressureLower"] = DataAdapter.ArrayToString(bpLower);
            dic["BloodPressureUpper"] = DataAdapter.ArrayToString(bpUpper);

            int[] HeartRate = new int[9];
            string[] txBMP = new string[9] {this.txBMPBedUp1.Text.Trim(),
                this.txBMPBedUp2.Text.Trim(),
                this.txBMPBedUp3.Text.Trim(),
                this.txBMPBedUp4.Text.Trim(),
                this.txBMPInRoom1.Text.Trim(),
                this.txBMPInRoom2.Text.Trim(),
                this.txBMPOutRoom1.Text.Trim(),
                this.txBMPOutRoom2.Text.Trim(),
                this.txBMPOutRoom3.Text.Trim()
            };
            for (int i = 0; i < 9; i++)
            {
                HeartRate[i] = DataAdapter.IsNumber(txBMP[i]) ? Convert.ToInt32(txBMP[i]) : 0;
            }
            dic["HeartRate"] = DataAdapter.ArrayToString(HeartRate);

            int[] BloodOxygen = new int[9];
            string[] txBloodOxygen = new string[9] {this.txBOBedUp1.Text.Trim(),
                this.txBOBedUp2.Text.Trim(),
                this.txBOBedUp3.Text.Trim(),
                this.txBOBedUp4.Text.Trim(),
                this.txBOInRoom1.Text.Trim(),
                this.txBOInRoom2.Text.Trim(),
                this.txBOOutRoom1.Text.Trim(),
                this.txBOOutRoom2.Text.Trim(),
                this.txBOOutRoom3.Text.Trim()
            };
            for (int i = 0; i < 9; i++)
            {
                BloodOxygen[i] = DataAdapter.IsNumber(txBloodOxygen[i]) ? Convert.ToInt32(txBloodOxygen[i]) : 0;
            }
            dic["BloodOxygen"] = DataAdapter.ArrayToString(BloodOxygen);

            int[] BorgIndex = new int[9];
            string[] txBorgIndex = new string[9] {this.txBorgBedUp1.Text.Trim(),
                this.txBorgBedUp2.Text.Trim(),
                this.txBorgBedUp3.Text.Trim(),
                this.txBorgBedUp4.Text.Trim(),
                this.txBorgInRoom1.Text.Trim(),
                this.txBorgInRoom2.Text.Trim(),
                this.txBorgOutRoom1.Text.Trim(),
                this.txBorgOutRoom2.Text.Trim(),
                this.txBorgOutRoom3.Text.Trim()
            };
            for (int i = 0; i < 9; i++)
            {
                BorgIndex[i] = DataAdapter.IsNumber(txBorgIndex[i]) ? Convert.ToInt32(txBorgIndex[i]) : 0;
            }
            dic["BorgIndex"] = DataAdapter.ArrayToString(BorgIndex);

            //string[] Remarks = new string[9];
            string[] Remarks = new string[9] {this.txAddBedUp1.Text.Trim(),
                this.txAddBedUp2.Text.Trim(),
                this.txAddBedUp3.Text.Trim(),
                this.txAddBedUp4.Text.Trim(),
                this.txAddInRoom1.Text.Trim(),
                this.txAddInRoom2.Text.Trim(),
                this.txAddOutRoom1.Text.Trim(),
                this.txAddOutRoom2.Text.Trim(),
                this.txAddOutRoom3.Text.Trim()
            };
            dic["Remarks"] = DataAdapter.ArrayToString(Remarks);

            string[] ECGs = new string[9] {this.txECGBedUp1.Text.Trim(),
                this.txECGBedUp2.Text.Trim(),
                this.txECGBedUp3.Text.Trim(),
                this.txECGBedUp4.Text.Trim(),
                this.txECGInRoom1.Text.Trim(),
                this.txECGInRoom2.Text.Trim(),
                this.txECGOutRoom1.Text.Trim(),
                this.txECGOutRoom2.Text.Trim(),
                this.txECGOutRoom3.Text.Trim()
            };
            dic["ECGs"] = DataAdapter.ArrayToString(ECGs);

            RadioButton[] CheckYes = new RadioButton[9]{this.rbBedUpYes1,
                this.rbBedUpYes2,
                this.rbBedUpYes3,
                this.rbBedUpYes4,
                this.rbInRoomYes1,
                this.rbInRoomYes2,
                this.rbOutRoomYes1,
                this.rbOutRoomYes2,
                this.rbOutRoomYes3
            };
            RadioButton[] CheckNo = new RadioButton[9]{this.rbBedUpNo1,
                this.rbBedUpNo2,
                this.rbBedUpNo3,
                this.rbBedUpNo4,
                this.rbInRoomNo1,
                this.rbInRoomNo2,
                this.rbOutRoomNo1,
                this.rbOutRoomNo2,
                this.rbOutRoomNo3
            };
            string Checks="" ;
            for (int i = 0; i < 9; i++)
            {
                if (CheckYes[i].IsChecked == true)
                    Checks += "1";
                else if (CheckNo[i].IsChecked == true)
                    Checks += "0";
                Checks += "|";
            }

            dic["Checks"] = Checks;

            if (this.rbInRoomUp5.IsChecked == true)
            {
                dic["InRoomUp"] = false;
            }
            else if (this.rbInRoomUp10.IsChecked == true)
            {
                dic["InRoomUp"] = true;
            }

        }

        private void ExcerciseDataLoad(DataTable dt)
        {
            DataRow dr = dt.Rows[0];

            DatePicker[] Date = new DatePicker[9] { this.dpBedup1,
                this.dpBedup2,
                this.dpBedup3,
                this.dpBedup4,
                this.dpInRoom1,
                this.dpInRoom2,
                this.dpOutRoom1,
                this.dpOutRoom2,
                this.dpOutRoom3
            };

            string sDate = dr["Date"] == System.DBNull.Value ? "" : dr["Date"].ToString();
            string[] DateArray = sDate.Split('|');
            for (int i = 0; i < 9; i++)
            {
                DateArray[i] = DateArray[i] == "0" ? "" : DateArray[i];
                Date[i].Text = DateArray[i];
            }
                                   
            TextBox[] txBPUp = new TextBox[9]{this.txBPBedUp1,
                this.txBPBedUp2,
                this.txBPBedUp3,
                this.txBPBedUp4,
                this.txBPInRoom1,
                this.txBPInRoom2,
                this.txBPOutRoom1,
                this.txBPOutRoom2,
                this.txBPOutRoom3
            };
            string sBloodPressureLower = dr["BloodPressureLower"] == System.DBNull.Value ? "" : dr["BloodPressureLower"].ToString();
            string sBloodPressureUpper = dr["BloodPressureUpper"] == System.DBNull.Value ? "" : dr["BloodPressureUpper"].ToString();
            string[] bpLowerArray = sBloodPressureLower.Split('|');
            string[] bpUpperArray = sBloodPressureUpper.Split('|');

            for (int i = 0; i < 9; i++)
            {
                bpLowerArray[i] = bpLowerArray[i] == "0" ? "" : bpLowerArray[i];
                bpUpperArray[i] = bpUpperArray[i] == "0" ? "" : bpUpperArray[i];
                txBPUp[i].Text = bpLowerArray[i] + "/" + bpUpperArray[i];
            }
            

            
            TextBox[] txBMP = new TextBox[9] {this.txBMPBedUp1,
                this.txBMPBedUp2,
                this.txBMPBedUp3,
                this.txBMPBedUp4,
                this.txBMPInRoom1,
                this.txBMPInRoom2,
                this.txBMPOutRoom1,
                this.txBMPOutRoom2,
                this.txBMPOutRoom3
            };
            string sHeartRate = dr["HeartRate"] == System.DBNull.Value ? "" : dr["HeartRate"].ToString();
            string[] HeartRateArray = sHeartRate.Split('|');
            for (int i = 0; i < 9; i++)
            {
                HeartRateArray[i] = HeartRateArray[i] == "0" ? "" : HeartRateArray[i];
                txBMP[i].Text = HeartRateArray[i];
            }
            

            
            TextBox[] txBloodOxygen = new TextBox[9] {this.txBOBedUp1,
                this.txBOBedUp2,
                this.txBOBedUp3,
                this.txBOBedUp4,
                this.txBOInRoom1,
                this.txBOInRoom2,
                this.txBOOutRoom1,
                this.txBOOutRoom2,
                this.txBOOutRoom3
            };
            string sBloodOxygen = dr["BloodOxygen"] == System.DBNull.Value ? "" : dr["BloodOxygen"].ToString();
            string[] BloodOxygenArray = sBloodOxygen.Split('|');
            for (int i = 0; i < 9; i++)
            {
                BloodOxygenArray[i] = BloodOxygenArray[i] == "0" ? "" : BloodOxygenArray[i];
                txBloodOxygen[i].Text = BloodOxygenArray[i];
            }
            

            
            TextBox[] txBorgIndex = new TextBox[9] {this.txBorgBedUp1,
                this.txBorgBedUp2,
                this.txBorgBedUp3,
                this.txBorgBedUp4,
                this.txBorgInRoom1,
                this.txBorgInRoom2,
                this.txBorgOutRoom1,
                this.txBorgOutRoom2,
                this.txBorgOutRoom3
            };
            string sBorgIndex = dr["BorgIndex"] == System.DBNull.Value ? "" : dr["BorgIndex"].ToString();
            string[] BorgIndexArray = sBorgIndex.Split('|');
            for (int i = 0; i < 9; i++)
            {
                BorgIndexArray[i] = BorgIndexArray[i] == "0" ? "" : BorgIndexArray[i];
                txBorgIndex[i].Text = BorgIndexArray[i];
            }


            TextBox[] Remarks = new TextBox[9] {this.txAddBedUp1,
                this.txAddBedUp2,
                this.txAddBedUp3,
                this.txAddBedUp4,
                this.txAddInRoom1,
                this.txAddInRoom2,
                this.txAddOutRoom1,
                this.txAddOutRoom2,
                this.txAddOutRoom3
            };
            string sRemarks = dr["Remarks"] == System.DBNull.Value ? "" : dr["Remarks"].ToString();
            string[] RemarksArray = sRemarks.Split('|');
            for (int i = 0; i < 9; i++)
            {
                RemarksArray[i] = RemarksArray[i] == "0" ? "" : RemarksArray[i];
                Remarks[i].Text = RemarksArray[i];
            }


            TextBox[] ECGs = new TextBox[9] {this.txECGBedUp1,
                this.txECGBedUp2,
                this.txECGBedUp3,
                this.txECGBedUp4,
                this.txECGInRoom1,
                this.txECGInRoom2,
                this.txECGOutRoom1,
                this.txECGOutRoom2,
                this.txECGOutRoom3
            };
            string sECGs = dr["ECGs"] == System.DBNull.Value ? "" : dr["ECGs"].ToString();
            string[] ECGsArray = sECGs.Split('|');
            for (int i = 0; i < 9; i++)
            {
                ECGsArray[i] = ECGsArray[i] == "0" ? "" : ECGsArray[i];
                ECGs[i].Text = ECGsArray[i];
            }

            RadioButton[] CheckYes = new RadioButton[9]{this.rbBedUpYes1,
                this.rbBedUpYes2,
                this.rbBedUpYes3,
                this.rbBedUpYes4,
                this.rbInRoomYes1,
                this.rbInRoomYes2,
                this.rbOutRoomYes1,
                this.rbOutRoomYes2,
                this.rbOutRoomYes3
            };
            RadioButton[] CheckNo = new RadioButton[9]{this.rbBedUpNo1,
                this.rbBedUpNo2,
                this.rbBedUpNo3,
                this.rbBedUpNo4,
                this.rbInRoomNo1,
                this.rbInRoomNo2,
                this.rbOutRoomNo1,
                this.rbOutRoomNo2,
                this.rbOutRoomNo3
            };
            string sChecks = dr["Checks"] == System.DBNull.Value ? "" : dr["Checks"].ToString();
            string[] ChecksArray = sChecks.Split('|');
            for (int i = 0; i < 9; i++)
            {
                if (ChecksArray[i] == "1")
                    CheckYes[i].IsChecked = true;
                else if (ChecksArray[i] == "0")
                    CheckNo[i].IsChecked = true; ;
            }

            if (dr["InRoomUp"] != System.DBNull.Value)
            {

                if (Convert.ToBoolean(dr["InRoomUp"]) == true)
                {
                    rbInRoomUp10.IsChecked = true;
                }
                else if (Convert.ToBoolean(dr["InRoomUp"]) == false)
                {
                    rbInRoomUp5.IsChecked = true;
                }
            }

        }



        private void TxBloodPressure_GotFocus(object sender, RoutedEventArgs e)
        {
            if (((TextBox)sender).Text.Trim(' ') == "/")
            {
                ((TextBox)sender).Text = "";
            }
        }

        private void TxBloodPressure_LostFocus(object sender, RoutedEventArgs e)
        {
            if (((TextBox)sender).Text.Trim(' ') == "")
            {
                ((TextBox)sender).Text = "/";
            }
        }


    }
}
