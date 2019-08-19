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
            PerformSelect();
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
                        string DataSelect = cbSelect.Text.Trim();
                        if (cbSelect.SelectedIndex != -1)
                        {                           

                            switch (cbSelect.SelectedIndex)
                            {
                                case 0:
                                    DataSelect = "select * from basicinfo";
                                    break;
                                case 1:
                                    DataSelect = "select * from basicinfo left join exercise on basicinfo.Number = exercise.basicinfo_number";
                                    break;
                                case 2:
                                    DataSelect = "select * from basicinfo left join phq on basicinfo.Number = phq.basicinfo_number";
                                    break;
                                case 3:
                                    DataSelect = "select * from basicinfo left join gad on basicinfo.Number = gad.basicinfo_number";
                                    break;
                                case 4:
                                    DataSelect = "select * from basicinfo left join ipaq on basicinfo.Number = ipaq.basicinfo_number";
                                    break;
                                case 5:
                                    DataSelect = "select * from basicinfo left join ohq on basicinfo.Number = ohq.basicinfo_number";
                                    break;
                                case 6:
                                    DataSelect = "select * from basicinfo left join sppb on basicinfo.Number = sppb.basicinfo_number";
                                    break;
                                case 7:
                                    DataSelect = "select * from basicinfo left join physique on basicinfo.Number = physique.basicinfo_number";
                                    break;
                            }


                        }
                        //if (DataSelect.Length <= 0)
                        //{
                        //    DataSelect = "select * from basicinfo" +
                        //        " left  join exercise on basicinfo.Number = exercise.basicinfo_number" +
                        //        " left  join gad on basicinfo.Number = gad.basicinfo_number" +
                        //        " left  join ipaq on basicinfo.Number = ipaq.basicinfo_number" +
                        //        " left  join ohq on basicinfo.Number = ohq.basicinfo_number" +
                        //        " left  join phq on basicinfo.Number = phq.basicinfo_number" +
                        //        " left  join physique on basicinfo.Number = physique.basicinfo_number" +
                        //        " inner join sppb on basicinfo.Number = sppb.basicinfo_number";
                        //}
#if DEBUG
                        Debug.WriteLine(cbSelect.SelectedIndex + " : " + cbSelect.Text);
#endif
                        DataTable dt = sh.Select(DataSelect);                       
                        dgSelect.ItemsSource = dt.DefaultView;

                        string strName=null;
                        OpenFileDialog dlg = new OpenFileDialog();
                        dlg.DefaultExt = "xls";
                        dlg.Filter = "Excle 文件|*.xls";
                        if (dlg.ShowDialog() == true)
                            strName = dlg.FileName;
                        if (strName == null)
                            return;
                        NPOIHelper.TableToExcel(dt, strName);
                    }
                    catch (Exception ex)
                    {
                        DataTable dt = new DataTable();
                        dt.Columns.Add("Error");
                        dt.Rows.Add(ex.ToString()); 
                        
                    }
                    conn.Close();
                }
            }
        }

        private void BtOutput_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
