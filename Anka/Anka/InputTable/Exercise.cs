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
        private void BtExcerciseSave_Click(object sender, RoutedEventArgs e)
        {
            
            if (txExerciseLoop.Text.Trim().Length > 0)
            {
                DataAdapter.ExerciseNumber = DataAdapter.Number + "-" + this.txExerciseLoop.Text.Trim();
                ExcerciseDataSave();

                string sql1 = string.Format("INSERT INTO `anka`.`exercise` (`ExerciseNumber`, `InRoomUp`,`Date`, `BloodPressureLower`, `BloodPressureUpper`" +
                    ", `HeartRate`, `BloodOxygen`, `BorgIndex`, `Remarks`, `ECGs`, `Checks`, `basicinfo_Number`) VALUES('{0}', {1}, '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}',{11});",
                    DataAdapter.ExerciseNumber,
                    DataAdapter.ExerciseResult.InRoomUp,
                    DataAdapter.ArrayToString(DataAdapter.ExerciseResult.Date),
                    DataAdapter.ArrayToString(DataAdapter.ExerciseResult.BloodPressureLower),
                    DataAdapter.ArrayToString(DataAdapter.ExerciseResult.BloodPressureUpper),
                    DataAdapter.ArrayToString(DataAdapter.ExerciseResult.HeartRate),
                    DataAdapter.ArrayToString(DataAdapter.ExerciseResult.BloodOxygen),
                    DataAdapter.ArrayToString(DataAdapter.ExerciseResult.BorgIndex),
                    DataAdapter.ArrayToString(DataAdapter.ExerciseResult.Remarks),
                    DataAdapter.ArrayToString(DataAdapter.ExerciseResult.ECGs),
                    DataAdapter.ArrayToString(DataAdapter.ExerciseResult.Checks),
                    DataAdapter.Number);
                string sql2 = string.Format("UPDATE `anka`.`exercise` SET `InRoomUp` = {0}, `Date` = '{1}', `BloodPressureLower` = '{2}', `BloodPressureUpper` = '{3}', `HeartRate` = '{4}', `BloodOxygen` = '{5}', `BorgIndex` = '{6}', `Remarks` = '{7}', `ECGs` = '{8}', `Checks` = '{9}' WHERE (`ExerciseNumber` = '{10}') and (`basicinfo_Number` = '{11}');",
                    DataAdapter.ExerciseResult.InRoomUp,
                    DataAdapter.ArrayToString(DataAdapter.ExerciseResult.Date),
                    DataAdapter.ArrayToString(DataAdapter.ExerciseResult.BloodPressureLower),
                    DataAdapter.ArrayToString(DataAdapter.ExerciseResult.BloodPressureUpper),
                    DataAdapter.ArrayToString(DataAdapter.ExerciseResult.HeartRate),
                    DataAdapter.ArrayToString(DataAdapter.ExerciseResult.BloodOxygen),
                    DataAdapter.ArrayToString(DataAdapter.ExerciseResult.BorgIndex),
                    DataAdapter.ArrayToString(DataAdapter.ExerciseResult.Remarks),
                    DataAdapter.ArrayToString(DataAdapter.ExerciseResult.ECGs),
                    DataAdapter.ArrayToString(DataAdapter.ExerciseResult.Checks),
                     DataAdapter.ExerciseNumber, DataAdapter.Number);

                //DatabaseInfo.ModifyDatabase(sql1, sql2);
                ((Button)sender).Background = new SolidColorBrush(Colors.LightGreen);
            }
            else
            {
                
                MessageBox.Show("请输入运动负荷记录表编号。");
            }

           

        }

        private void ExcerciseDataSave()
        {            

            DataAdapter.ExerciseResult.Date = new string[9] { this.dpBedup1.Text.Trim(),
                this.dpBedup2.Text.Trim(),
                this.dpBedup3.Text.Trim(),
                this.dpBedup4.Text.Trim(),
                this.dpInRoom1.Text.Trim(),
                this.dpInRoom2.Text.Trim(),
                this.dpOutRoom1.Text.Trim(),
                this.dpOutRoom2.Text.Trim(),
                this.dpOutRoom3.Text.Trim()
            };

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
            DataAdapter.ExerciseResult.BloodPressureLower = bpLower;
            DataAdapter.ExerciseResult.BloodPressureUpper = bpUpper;

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
            DataAdapter.ExerciseResult.HeartRate = HeartRate;

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
            DataAdapter.ExerciseResult.BloodOxygen = BloodOxygen;

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
            DataAdapter.ExerciseResult.BorgIndex = BorgIndex;

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
            DataAdapter.ExerciseResult.Remarks = Remarks;

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
            DataAdapter.ExerciseResult.ECGs = ECGs;

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
            DataAdapter.ExerciseResult.Checks = new bool[9];
            for (int i = 0; i < 9; i++)
            {
                if (CheckYes[i].IsChecked == true)
                    DataAdapter.ExerciseResult.Checks[i] = true;
                else if (CheckNo[i].IsChecked == true)
                    DataAdapter.ExerciseResult.Checks[i] = false;
            }

            if (this.rbInRoomUp5.IsChecked == true)
            {
                DataAdapter.ExerciseResult.InRoomUp = false;
            }
            else if (this.rbInRoomUp10.IsChecked == true)
            {
                DataAdapter.ExerciseResult.InRoomUp = true;
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
