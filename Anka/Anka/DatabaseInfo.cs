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
    static class DatabaseInfo
    {
        public static string Sever { set; get; }
        public static string UserID { set; get; }
        public static string PassWord { set; get; }
        public static Boolean ConStatus { set; get; }

        public static MySqlDataReader ModifyDatabase( string sql)
        {
            string connString = "server=" + DatabaseInfo.Sever + ";database=anka;uid=" + DatabaseInfo.UserID + ";pwd=" + DatabaseInfo.PassWord + ";SslMode = none";
            MySqlConnection conn = new MySqlConnection(connString);
            MySqlDataReader result = null; ;
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
                result = cmd.ExecuteReader();
            }
            catch(MySqlException ex)
            {
                if (ex.Number == 1062)
                {
                    MessageBox.Show("该编号已经存在，请输入新的编号。");
                }
                else
                {
                    MessageBox.Show(string.Format("数据更新失败。错误代码:{0}", ex.Number));
                }
            }
            finally
            {
                conn.Close();
            }
           
            
            return result;
        }

        public static MySqlDataReader ModifyDatabase(string sql1,string sql2)
        {
            string connString = "server=" + DatabaseInfo.Sever + ";database=anka;uid=" + DatabaseInfo.UserID + ";pwd=" + DatabaseInfo.PassWord + ";SslMode = none";
            MySqlConnection conn = new MySqlConnection(connString);
            MySqlDataReader result = null; ;
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
                       

            try
            {
                MySqlCommand cmd = new MySqlCommand(sql1, conn);
                result = cmd.ExecuteReader();
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1062)
                {
                    if (MessageBox.Show("当前数据已存在，是否更新结果?", "数据更新", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)

                    {
                        try
                        {
                            MySqlCommand cmd = new MySqlCommand(sql2, conn);
                            result = cmd.ExecuteReader();
                        }
                        catch (MySqlException uex)
                        {
                            MessageBox.Show(string.Format("数据更新失败。错误代码:{0}", uex.Number));
                        }
                    }
                }
                else
                {
                    MessageBox.Show(string.Format("数据更新失败。错误代码:{0}", ex.Number));
                }
            }
            finally
            {
                conn.Close();
            }


            return result;
        }

    }
}
