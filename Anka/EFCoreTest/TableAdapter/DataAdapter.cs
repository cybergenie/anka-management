using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace EFCoreTest.TableAdapter
{
    public class  DataAdapter
    {
        private DataTable _collectionView = null;
        public DataTable CollectionView => _collectionView;
        public object GetDataList<T>(List<T> data)
        {
            object collectionView = null;
            var dataTable = DbTools.ToDataTable(data);
            DataTable dataCoventer = null;
            switch (typeof(T).Name)
            {

                case "BasicInfo": {
                        dataCoventer = BasicinfoConverter(dataTable);
                        _collectionView = dataCoventer;
                        var list = DbTools.ToDataList<BasicInfoTable>(dataCoventer);
                        collectionView = new ObservableCollection<BasicInfoTable>(list);
                    }
                    break;
                default:break;
            }            
            return collectionView;
        }

        private static DataTable BasicinfoConverter(DataTable dt)
        {
            string[] Risks = { "高血压", "糖尿病", "脑卒中", "吸烟", "高LDL-C", "高TG", "肥胖", "痛风", "运动不足", "周围动脉硬化闭塞", "肾功能不全CRE", "肝功能异常ALT", "其他:" };

            DataTable dtOutput = dt.Copy();
            dtOutput.TableName = "01-基本信息";


            dtOutput.Columns["Number"].ColumnName = "病案号";
            dtOutput.Columns["Name"].ColumnName = "姓名";
            dtOutput.Columns["Age"].ColumnName = "年龄";
            dtOutput.Columns.Add("性别", typeof(String));
            dtOutput.Columns["性别"].SetOrdinal(3);
            dtOutput.Columns["Killip"].ColumnName = "Killip";
            dtOutput.Columns["EF"].ColumnName = "EF";
            dtOutput.Columns["LV"].ColumnName = "LV";
            dtOutput.Columns["BasicOther"].ColumnName = "其他";
            dtOutput.Columns["BasicRisk"].ColumnName = "危险因素";
            dtOutput.Columns["PCI"].ColumnName = "PCI支架数";
            dtOutput.Columns["ResidualStenosis"].ColumnName = "75%以上残余狭窄";
            dtOutput.Columns["D2"].ColumnName = "D-二聚体";
            dtOutput.Columns.Add("侧枝循环", typeof(String));
            dtOutput.Columns.Add("优势冠脉", typeof(String));
            if (dtOutput.Columns.Contains("Description") == true)
            {
                dtOutput.Columns["Description"].ColumnName = "诊断";
                dtOutput.Columns["诊断"].SetOrdinal(4);
            }
            try
            {
                foreach (DataRow dRow in dtOutput.Rows)
                {

                    string tempRisk = null;
                    string risk = dRow["危险因素"].ToString();
                    for (int i = 0; i < risk.Length; i++)
                    {
                        if (risk[i] == '1')
                        {
                            tempRisk += (Risks[i] + ";");
                        }

                    }
                    dRow["危险因素"] = tempRisk + dRow["RiskOther"];



                    switch (dRow["Male"].ToString())
                    {
                        case "True":
                            dRow["性别"] = "男";
                            break;
                        case "False":
                            dRow["性别"] = "女";
                            break;
                        default:
                            dRow["性别"] = "";
                            break;
                    }


                    switch (dRow["CollatCirc"].ToString())
                    {
                        case "True":
                            dRow["侧枝循环"] = "有";
                            break;
                        case "False":
                            dRow["侧枝循环"] = "无";
                            break;
                        default:
                            dRow["侧枝循环"] = "";
                            break;
                    }


                    switch (dRow["DominantCoronary"].ToString())
                    {
                        case "-1":
                            dRow["优势冠脉"] = "左优势型";
                            break;
                        case "0":
                            dRow["优势冠脉"] = "均衡型";
                            break;
                        case "1":
                            dRow["优势冠脉"] = "右优势型";
                            break;
                        default:
                            dRow["优势冠脉"] = "";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("基本信息" + ex.Message);
            }
            dtOutput.Columns.Remove("DominantCoronary");
            dtOutput.Columns.Remove("Male");
            dtOutput.Columns.Remove("RiskOther");
            dtOutput.Columns.Remove("CollatCirc");
            return dtOutput;
        }
    }
}
