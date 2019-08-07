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
using System.Windows.Shapes;

using System.Data.SQLite;
using System.Data;

namespace Anka
{
    /// <summary>
    /// NewPerson.xaml 的交互逻辑
    /// </summary>
    public partial class NewPerson : Window
    {
        public NewPerson()
        {
            InitializeComponent();
        }

        private void BtNew_Click(object sender, RoutedEventArgs e)
        {
            bool isClose = true;
            bool Male = true;
            DataAdapter.loadNewPerson = true;

            if (this.txName.Text.Trim().Length<=0)
            {
                MessageBox.Show("请确认输入姓名。");
                isClose = false;
            }

            if (this.txNumber.Text.Trim().Length != 8|| DataAdapter.IsNumber(this.txAge.Text) == false)
            {
                MessageBox.Show("请确认输入八位数字。");
                isClose = false;
            }
            
            else if (Convert.ToInt32(this.txAge.Text) <= 0 || Convert.ToInt32(this.txAge.Text) >= 150)
            {
                MessageBox.Show("请确认输入年龄正确。");
                isClose = false;
            }

            if (rbMale.IsChecked==true)
            {
                Male = true;
            }
            else if(rbFemale.IsChecked==true)
            {
                Male = false;
            }
            else
            {
                MessageBox.Show("请选择性别。");
                isClose = false;
            }
            if (isClose == true)
            {

                using (SQLiteConnection conn = new SQLiteConnection(config.DataSource))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand())
                    {
                        cmd.Connection = conn;
                        conn.Open();

                        SQLiteHelper sh = new SQLiteHelper(cmd);

                        var dic = new Dictionary<string, object>();
                        dic["Number"] = txNumber.Text.ToString().Trim();
                        dic["Name"] = txName.Text.ToString().Trim();
                        dic["Age"] = txAge.Text.ToString().Trim();
                        dic["Male"] = Male;
                        try
                        {
                            sh.Insert("basicinfo", dic);
                            DataAdapter.Number = this.txNumber.Text;
                            DataAdapter.Age = Convert.ToInt32(this.txAge.Text);
                            DataAdapter.Male = Male;
                            DataAdapter.Name = this.txName.Text.ToString().Trim();                            
                        }
                        catch(SQLiteException ex)
                        {
                            DataAdapter.loadNewPerson = false;
                            if (ex.ErrorCode == 19)
                            {
                                MessageBox.Show("该档案号已存在，请重新输入。", "档案建立错误");
                            }
                            else
                            {
                                MessageBox.Show(string.Format("档案建立错误。错误代码为:{0}", ex.ErrorCode), "档案建立错误");
                            }
                            
                        }
                        finally
                        {
                            conn.Close();
                        }                       

                        
                    }
                }               
                //string sql = string.Format("INSERT INTO basicinfo (Number, Name, Age, Male) VALUES({0}, '{1}', {2}, {3})",DataAdapter.Number,DataAdapter.Name,DataAdapter.Age,DataAdapter.Male) ;

                ////DatabaseInfo.ModifyDatabase(sql);
                //SQLiteAdapter.ExecuteNonQuery(sql);

                isClose = true;
            }

            if(isClose==true)
            {
                
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            
        }

        private void BtLoad_Click(object sender, RoutedEventArgs e)
        {
            bool isClose = true;            
            this.txName.IsReadOnly = true;
            this.txAge.IsReadOnly = true;
            this.rbFemale.IsEnabled = false;
            this.rbMale.IsEnabled = false;
            DataAdapter.loadNewPerson = true;

            if (this.txNumber.Text.Trim().Length != 8)
            {
                MessageBox.Show("请确认输入八位数字。");
                isClose = false;
            }           

           
            if (isClose == true)
            {
                string sql = string.Format("SELECT * FROM basicinfo where Number='{0}';", txNumber.Text.ToString().Trim());
                using (SQLiteConnection conn = new SQLiteConnection(config.DataSource))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand())
                    {
                        conn.Open();
                        cmd.Connection = conn;

                        SQLiteHelper sh = new SQLiteHelper(cmd);
                        try
                        {
                            DataTable dt = sh.Select(sql);
                            if (dt.Rows.Count > 0)
                            {
                                DataRow dr = dt.Rows[0];
                                if (dr["Name"] != System.DBNull.Value)
                                {
                                    DataAdapter.Name = dr["Name"].ToString();
                                }
                                if (dr["Age"] != System.DBNull.Value)
                                {
                                    DataAdapter.Age = Convert.ToInt32(dr["Age"]);
                                }
                                if (dr["Number"] != System.DBNull.Value)
                                {
                                    DataAdapter.Number = Convert.ToString(dr["Number"]);
                                }
                                if (dr["Male"] != System.DBNull.Value)
                                {
                                    DataAdapter.Male = Convert.ToBoolean(dr["Male"]);
                                }
                            }
                            else
                            {
                                MessageBox.Show(string.Format("该档案号不存在。"));
                                isClose = false;
                            }
                            


                        }
                        catch (SQLiteException ex)
                        {
                            DataAdapter.loadNewPerson = false;
                            MessageBox.Show(string.Format("数据查找错误。错误代码为:{0}", ex.ErrorCode), "档案建立错误");
                            isClose = false;
                        }
                        finally
                        {
                            conn.Close();
                        }

                        

                    }
                }                  

                }

            if (isClose == true)
            {
                
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }

        }

        private void BtCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
