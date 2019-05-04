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
    /// DatabaseConnection.xaml 的交互逻辑
    /// </summary>
    public partial class DatabaseConnection : Window
    {
        public DatabaseConnection()
        {
            InitializeComponent();
            if(AnkaSetting.Default.DataConectionInfoSave==true)
            {
                this.txSever.Text = AnkaSetting.Default.Sever;
                this.txUID.Text = AnkaSetting.Default.UID;
                this.txPW.Password = AnkaSetting.Default.PW;
                this.DatabaseConnectionInfoSave.IsChecked = AnkaSetting.Default.DataConectionInfoSave;
            }
        }
        
        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            DatabaseInfo.Sever = this.txSever.Text;
            DatabaseInfo.UserID = this.txUID.Text;
            DatabaseInfo.PassWord = this.txPW.Password;

            if (this.DatabaseConnectionInfoSave.IsChecked == true)
            {
                AnkaSetting.Default.Sever = this.txSever.Text;
                AnkaSetting.Default.UID = this.txUID.Text;
                AnkaSetting.Default.PW = this.txPW.Password;
                AnkaSetting.Default.DataConectionInfoSave = true;
            }
            else if(this.DatabaseConnectionInfoSave.IsChecked == false)
            {
                AnkaSetting.Default.DataConectionInfoSave = false;
            }

            string connString = "server="+ DatabaseInfo.Sever+";database=anka;uid="+ DatabaseInfo.UserID +";pwd="+DatabaseInfo.PassWord+ ";SslMode = none";
            MySqlConnection conn = new MySqlConnection(connString);
            try
            {
                conn.Open();                
                MessageBox.Show("连接成功！", "连接数据库");
                DatabaseInfo.ConStatus = true;

            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                DatabaseInfo.ConStatus = false;

            }
            finally
            {
                this.Close();

            }

        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }

        
    }
}
