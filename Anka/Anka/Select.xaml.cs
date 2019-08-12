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
    /// Select.xaml 的交互逻辑
    /// </summary>
    public partial class Select : Window
    {
        public Select()
        {
            InitializeComponent();
        }

        private void BtSelect_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void PerformSelect()
        {
            using (SQLiteConnection conn = new SQLiteConnection(config.DataSource))
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    conn.Open();
                    cmd.Connection = conn;

                    SQLiteHelper sh = new SQLiteHelper(cmd);

                    try
                    {
                        DataTable dt = sh.Select(textBox1.Text);
                        dataGridView1.DataSource = dt;
                    }
                    catch (Exception ex)
                    {
                        DataTable dt = new DataTable();
                        dt.Columns.Add("Error");
                        dt.Rows.Add(ex.ToString());
                        dataGridView1.DataSource = dt;
                    }

                    conn.Close();

                }
            }
        }
    }
}
