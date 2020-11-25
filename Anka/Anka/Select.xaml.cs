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
using System.Diagnostics;
using Microsoft.Win32;

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
            if (cbSelect.SelectedIndex != -1)
            {
                int i = cbSelect.SelectedIndex;
                DataTable dtOutput = GetTables()[i];
                if (dtOutput != null)
                {
                    dgSelect.ItemsSource = dtOutput.DefaultView;
                }
            }
            else
            {
                DataTable dt;
                using (SQLiteConnection conn = new SQLiteConnection(config.DataSource))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand())
                    {
                        conn.Open();
                        cmd.Connection = conn;

                        SQLiteHelper sh = new SQLiteHelper(cmd);

                        try
                        {
                            string DataSelect = cbSelect.Text.Trim();
                            dt = sh.Select(DataSelect);

                        }
                        catch (Exception ex)
                        {
                            dt = null;
                            MessageBox.Show(ex.Message);                            
                        }
                        conn.Close();
                        if (dt != null)
                        {
                            dgSelect.ItemsSource = dt.DefaultView;
                        }

                    }
                }
            }
        } 

        private void BtOutput_Click(object sender, RoutedEventArgs e)
        {
            DataTable[] dtOutputs = GetTables();
            try
            {
                string strName = null;
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.RestoreDirectory = true;
                dlg.DefaultExt = "xls";
                dlg.Filter = "Excle工作簿|*.xlsx|Excle97-2003工作簿|*.xls";
                if (dlg.ShowDialog() == true)
                {
                    strName = dlg.FileName;
#if DEBUG
                    Debug.WriteLine(strName);
#endif
                }
                if (strName == null)
                    return;
                NPOIHelper.TableToExcel(dtOutputs, strName);
                MessageBox.Show("数据导出已完成");
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }


        }

        private DataTable[] GetTables()
        {
            string[] DataSelect = { "select * from basicinfo",
            "select * from basicinfo left join exercise on basicinfo.Number = exercise.basicinfo_number"+
            " left join gad on basicinfo.Number = gad.basicinfo_number"+
            " left join ipaq on basicinfo.Number = ipaq.basicinfo_number"+
            " left join phq on basicinfo.Number = phq.basicinfo_number"+
            " left join ohq on basicinfo.Number = ohq.basicinfo_number",
            "select * from basicinfo left join sppb on basicinfo.Number = sppb.basicinfo_number",
            "select * from basicinfo left join physique on basicinfo.Number = physique.basicinfo_number"
            };

            DataTable[] dataTables = new DataTable[DataSelect.Length];
            DataTable[] dtOutputs = new DataTable[DataSelect.Length];

            using (SQLiteConnection conn = new SQLiteConnection(config.DataSource))
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    conn.Open();
                    cmd.Connection = conn;

                    SQLiteHelper sh = new SQLiteHelper(cmd);

                    try
                    {

                        for (int i = 0; i < DataSelect.Length; i++)
                        {
                            dataTables[i] = sh.Select(DataSelect[i]);
                            dtOutputs[i] = DataAdapter.DataTableConverter(dataTables[i], i);
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);

                    }
                    conn.Close();
                }
            }
            return dtOutputs;
        }

        

    }
}
