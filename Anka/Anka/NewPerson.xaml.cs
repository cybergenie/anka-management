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

using MySql.Data.MySqlClient;

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

            if(this.txName.Text.Trim().Length<=0)
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
                DataAdapter.Number = this.txNumber.Text;
                DataAdapter.Age = Convert.ToInt32(this.txAge.Text);
                DataAdapter.Male = Male;
                DataAdapter.Name = this.txName.Text.ToString().Trim(' ');
                string sql = string.Format("INSERT INTO `anka`.`basicinfo` (`Number`, `Name`, `Age`, `Male`) VALUES({0}, '{1}', {2}, {3})",DataAdapter.Number,DataAdapter.Name,DataAdapter.Age,DataAdapter.Male) ;

                DatabaseInfo.ModifyDatabase(sql);

                isClose = true;
            }

            if(isClose==true)
            {
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

            if (this.txNumber.Text.Trim().Length != 8)
            {
                MessageBox.Show("请确认输入八位数字。");
                isClose = false;
            }           

           
            if (isClose == true)
            {



                DataAdapter.Number = this.txNumber.Text.Trim();                
                string sql = string.Format("SELECT * FROM anka.basicinfo where Number='{0}';", DataAdapter.Number);


                string connString = "server=" + DatabaseInfo.Sever + ";database=anka;uid=" + DatabaseInfo.UserID + ";pwd=" + DatabaseInfo.PassWord + ";SslMode = none";
                MySqlConnection conn = new MySqlConnection(connString);
               
                try
                {
                    conn.Open();
                    DatabaseInfo.ConStatus = true;



                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                    DatabaseInfo.ConStatus = false;

                }

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                try
                {
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        DataAdapter.Name = dataReader["Name"].ToString();
                        DataAdapter.Age = Convert.ToInt32(dataReader["Age"].ToString());
                        if (dataReader["Male"].ToString() == "1")
                        {
                            this.rbMale.IsChecked = true;
                            DataAdapter.Male = true;
                        }
                        else
                        {
                            this.rbFemale.IsChecked = true;
                            DataAdapter.Male = true;
                        }

                        }
                    dataReader.Close();
                }
                catch (MySqlException ex)
                {                    
                    
                        MessageBox.Show(string.Format("数据查询失败。错误代码:{0}", ex.Number));
                  
                }
                finally
                {
                    conn.Close();
                }

            }

            if (isClose == true)
            {
                this.Close();
            }

        }

        private void BtCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
