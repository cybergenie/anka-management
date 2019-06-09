using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
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

//using MySql.Data.MySqlClient;


namespace Anka
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {


        public MainWindow()
        {

            InitializeComponent();
            if (DataAdapter.loadNewPerson == true)
            {
                LoadNewPerson();
                DataAdapter.loadNewPerson = false;
            }
            InitBMI();
            InitFMI();
            InitTBW();
            InitBCW();
            InitEBW();
            InitBody();
            InitLA();
            InitTK();
            InitRA();
            InitLL();
            InitRL();
            InitVAT();
            InitWC();
        }

        private void DataConnection_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".db";
            dlg.Filter = "数据库文件 (*.db)|*.db";
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                config.DataSource = dlg.FileName;
                if(TestConnection()==true)
                {
                    config.ConStatus = true;
                }
                else
                {
                    config.ConStatus = false;
                    MessageBox.Show("数据库连接失败.");
                }
            }
        }

        private void MiNewDoc_Click(object sender, RoutedEventArgs e)
        {
            NewPerson newPerson = new NewPerson();
            newPerson.Show();
            this.Close();

        }

        private void LoadNewPerson()
        {
            this.lbBasicName.Content = DataAdapter.Name;
            this.lbBasicNumber.Content = DataAdapter.Number.ToString();
            if (DataAdapter.Male == true)
            {
                this.lbBasicMale.Content = "男";
            }
            else if (DataAdapter.Male == false)
            {
                this.lbBasicMale.Content = "女";
            }
            this.lbBasicAge.Content = DataAdapter.Age.ToString();
            {
                this.lbLoadName.Content = this.lbBasicName.Content;
                this.lbLoadAge.Content = this.lbBasicAge.Content;
                this.lbLoadMale.Content = this.lbBasicMale.Content;
                this.lbLoadNumber.Content = this.lbBasicNumber.Content + " -";
            }
            {
                this.lbPHQName.Content = this.lbBasicName.Content;
                this.lbPHQAge.Content = this.lbBasicAge.Content;
                this.lbPHQMale.Content = this.lbBasicMale.Content;
                this.lbPHQNumber.Content = this.lbBasicNumber.Content + " -";
            }
            {
                this.lbGADName.Content = this.lbBasicName.Content;
                this.lbGADAge.Content = this.lbBasicAge.Content;
                this.lbGADMale.Content = this.lbBasicMale.Content;
                this.lbGADNumber.Content = this.lbBasicNumber.Content + " -";
            }
            {
                this.lbIPAQName.Content = this.lbBasicName.Content;
                this.lbIPAQAge.Content = this.lbBasicAge.Content;
                this.lbIPAQMale.Content = this.lbBasicMale.Content;
                this.lbIPAQNumber.Content = this.lbBasicNumber.Content + " -";
            }
            {
                this.lbOHQName.Content = this.lbBasicName.Content;
                this.lbOHQAge.Content = this.lbBasicAge.Content;
                this.lbOHQMale.Content = this.lbBasicMale.Content;
                this.lbOHQNumber.Content = this.lbBasicNumber.Content + " -";
            }
            {
                this.lbSPPBName.Content = this.lbBasicName.Content;
                this.lbSPPBAge.Content = this.lbBasicAge.Content;
                this.lbSPPBMale.Content = this.lbBasicMale.Content;
                this.lbSPPBNumber.Content = this.lbBasicNumber.Content + " -";
            }
            {
                this.lbBIName.Content = this.lbBasicName.Content;
                this.lbBIAge.Content = this.lbBasicAge.Content;
                this.lbBIMale.Content = this.lbBasicMale.Content;
                this.lbBINumber.Content = this.lbBasicNumber.Content + " -";
            }


            using (SQLiteConnection conn = new SQLiteConnection(config.DataSource))
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    cmd.Connection = conn;
                    conn.Open();

                    SQLiteHelper sh = new SQLiteHelper(cmd);

                    try
                    {
                        DataTable dt = sh.Select(string.Format("select ExerciseNumber from exercise where basicinfo_Number='{0}';", DataAdapter.Number));
                        if (dt.Rows.Count > 0)
                        {
                            foreach(DataRow dr in dt.Rows)
                            {
                                string[] ExerciseNumber = dr["ExerciseNumber"].ToString().Split('-');
                                txExerciseLoop.Items.Add(ExerciseNumber[1]);
                            }                          

                        }
                        else
                        {
                            MessageBox.Show("该编号数据不存在。");
                        }

                        dt = sh.Select(string.Format("select GADNumber from gad where basicinfo_Number='{0}';", DataAdapter.Number));
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                string[] GADNumber = dr["GADNumber"].ToString().Split('-');
                                txGADLoop.Items.Add(GADNumber[1]);
                            }

                        }
                        else
                        {
                            MessageBox.Show("该编号数据不存在。");
                        }

                        dt = sh.Select(string.Format("select IPAQNumber from ipaq where basicinfo_Number='{0}';", DataAdapter.Number));
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                string[] IPAQNumber = dr["IPAQNumber"].ToString().Split('-');
                                txIPAQLoop.Items.Add(IPAQNumber[1]);
                            }

                        }
                        else
                        {
                            MessageBox.Show("该编号数据不存在。");
                        }

                        dt = sh.Select(string.Format("select OHQNumber from ohq where basicinfo_Number='{0}';", DataAdapter.Number));
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                string[] OHQNumber = dr["OHQNumber"].ToString().Split('-');
                                txOHQLoop.Items.Add(OHQNumber[1]);
                            }

                        }
                        else
                        {
                            MessageBox.Show("该编号数据不存在。");
                        }

                        dt = sh.Select(string.Format("select PHQNumber from phq where basicinfo_Number='{0}';", DataAdapter.Number));
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                string[] PHQNumber = dr["PHQNumber"].ToString().Split('-');
                                txPHQLoop.Items.Add(PHQNumber[1]);
                            }

                        }
                        else
                        {
                            MessageBox.Show("该编号数据不存在。");
                        }

                        dt = sh.Select(string.Format("select PhysiqueNumber from physique where basicinfo_Number='{0}';", DataAdapter.Number));
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                string[] PhysiqueNumber = dr["PhysiqueNumber"].ToString().Split('-');
                                txPhysiqueLoop.Items.Add(PhysiqueNumber[1]);
                            }

                        }
                        else
                        {
                            MessageBox.Show("该编号数据不存在。");
                        }

                        dt = sh.Select(string.Format("select SPPBNumber from sppb where basicinfo_Number='{0}';", DataAdapter.Number));
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                string[] SPPBNumber = dr["SPPBNumber"].ToString().Split('-');
                                txSPPBLoop.Items.Add(SPPBNumber[1]);
                            }

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


        private void miExit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private bool TestConnection()
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(config.DataSource))
                {
                    conn.Open();
                    conn.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        

        
    }
}
