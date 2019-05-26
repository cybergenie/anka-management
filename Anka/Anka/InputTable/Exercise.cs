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
                }

                ((Button)sender).Background = new SolidColorBrush(Colors.LightGreen);
            }

            else
            {

                MessageBox.Show("请输入运动负荷记录表编号。");
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
            for (int i = 0; i < 9; i++)
            {
                DataAdapter.GetBloodPressure(txBPUp[i], bpLower[i], bpUpper[i]);
            }
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
            bool[] Checks = new bool[9];
            for (int i = 0; i < 9; i++)
            {
                if (CheckYes[i].IsChecked == true)
                    Checks[i] = true;
                else if (CheckNo[i].IsChecked == true)
                    Checks[i] = false;
            }

            dic["Checks"] = DataAdapter.ArrayToString(Checks);

            if (this.rbInRoomUp5.IsChecked == true)
            {
                dic["InRoomUp"] = false;
            }
            else if (this.rbInRoomUp10.IsChecked == true)
            {
                dic["InRoomUp"] = true;
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
