using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Reflection;
using System.Text;
using System.Windows;

namespace EFCoreTest
{
    public class DbAdapter: DbContext
    {
        public static readonly LoggerFactory MyLoggerFactory = new LoggerFactory(new[] {
            new DebugLoggerProvider()
        });
        public DbSet<BasicInfo> basicinfo { get; set; }
        public DbSet<Exercise> Exercise { get; set; }
        public DbSet<GAD> GAD { get; set; }
        public DbSet<IPAQ> ipaq { get; set; }
        public DbSet<OHQ> OHQ { get; set; }
        public DbSet<PHQ> phq { get; set; }

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(MyLoggerFactory).UseSqlite(
                "Data Source=anka.db");           
            base.OnConfiguring(optionsBuilder);
        }
    }



    public static class DbTools
    {
        public static DataTable ToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }

        public static List<T> ToDataList<T>(this DataTable dt)
        {
            var list = new List<T>();
            var plist = new List<PropertyInfo>(typeof(T).GetProperties());
            foreach (DataRow item in dt.Rows)
            {
                T s = Activator.CreateInstance<T>();
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    PropertyInfo info = plist.Find(p => p.Name == dt.Columns[i].ColumnName);
                    if (info != null)
                    {
                        try
                        {
                            if (!Convert.IsDBNull(item[i]))
                            {
                                object v = null;
                                if (info.PropertyType.ToString().Contains("System.Nullable"))
                                {
                                    v = Convert.ChangeType(item[i], Nullable.GetUnderlyingType(info.PropertyType));
                                }
                                else
                                {
                                    v = Convert.ChangeType(item[i], info.PropertyType);
                                }
                                info.SetValue(s, v, null);
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("字段[" + info.Name + "]转换出错," + ex.Message);
                        }
                    }
                }
                list.Add(s);
            }
            return list;
        }

        public static DataTable BasicinfoConverter(DataTable dt)
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

        public static DataTable ExerciseConverter(DataTable dt)
        {
            DataTable dtOutput = dt.Copy();
            dtOutput.TableName = "02-运动负荷记录表";

            dtOutput.Columns["Number"].ColumnName = "病案号";
            dtOutput.Columns["Name"].ColumnName = "姓名";
            dtOutput.Columns["Age"].ColumnName = "年龄";
            dtOutput.Columns["ExerciseNumber"].ColumnName = "记录编号";

            dtOutput.Columns.Add("性别", typeof(String));
            dtOutput.Columns["性别"].SetOrdinal(3);
            dtOutput.Columns.Add("床上负荷", typeof(int));
            dtOutput.Columns.Add("室内负荷", typeof(int));
            dtOutput.Columns.Add("室外负荷", typeof(int));
            dtOutput.Columns.Add("院外负荷", typeof(int));

            foreach (DataRow dRow in dtOutput.Rows)
            {



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

                string[] strChecks = (dRow["Checks"].ToString()).Split('|');

                int p = 0;
                for (int i = 0; i < strChecks.Length; i++)
                {
                    if (strChecks[i] == "1")
                    {
                        p++;
                    }
                    if (strChecks[i] == "0")
                    {
                        p += 10; ;
                    }

                    if (i == 3)
                    {
                        if (p == 0)
                        {
                            dRow["床上负荷"] = 0;
                        }
                        else if (p > 0 && p < 10)
                        {
                            dRow["床上负荷"] = 1;
                        }
                        else if (p >= 10)
                        {
                            dRow["床上负荷"] = 0;
                        }

                        p = 0;
                    }

                    if (i == 5)
                    {
                        if (p == 0)
                        {
                            dRow["室内负荷"] = 0;
                        }
                        else if (p > 0 && p < 10)
                        {
                            dRow["室内负荷"] = 1;
                        }
                        else if (p >= 10)
                        {
                            dRow["室内负荷"] = 0;
                        }

                        p = 0;
                    }

                    if (i == 8)
                    {
                        if (p == 0)
                        {
                            dRow["室外负荷"] = 0;
                        }
                        else if (p > 0 && p < 10)
                        {
                            dRow["室外负荷"] = 1;
                        }
                        else if (p >= 10)
                        {
                            dRow["室外负荷"] = 0;
                        }

                        p = 0;
                    }

                }

                if (p == 0)
                {
                    dRow["院外负荷"] = 0;
                }
                else if (p > 0 && p < 10)
                {
                    dRow["院外负荷"] = 1;
                }
                else if (p >= 10)
                {
                    dRow["院外负荷"] = 0;
                }




            }
            dtOutput.Columns.Remove("Checks");
            dtOutput.Columns.Remove("Male");
            return dtOutput;
        }

        public static DataTable GADConverter(DataTable dt)
        {
            DataTable dtOutput = dt.Copy();
            dtOutput.TableName = "03-GAD";

            dtOutput.Columns["Number"].ColumnName = "病案号";
            dtOutput.Columns["Name"].ColumnName = "姓名";
            dtOutput.Columns["Age"].ColumnName = "年龄";
            dtOutput.Columns["GADNumber"].ColumnName = "记录编号";
            dtOutput.Columns.Add("性别", typeof(String));
            dtOutput.Columns["性别"].SetOrdinal(3);
            dtOutput.Columns.Add("GAD_7", typeof(String));

            foreach (DataRow dRow in dtOutput.Rows)
            {
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

                string[] strGADs = (dRow["GADResult"].ToString()).Split('|');
                int GAD_7 = 0;
                foreach (string strGAD in strGADs)
                {
                    if (strGAD.Length > 0)
                    {


                        int nGAD = Convert.ToInt32(strGAD);
                        if (nGAD > 0)
                        {
                            GAD_7 += nGAD;
                        }
                    }
                }
                dRow["GAD_7"] = GAD_7.ToString();
            }

            dtOutput.Columns.Remove("GADResult");
            dtOutput.Columns.Remove("Male");

            return dtOutput;
        }

        public static DataTable IPAQConverter(DataTable dt)
        {
            DataTable dtOutput = dt.Copy();
            dtOutput.TableName = "04-IPAQ";

            dtOutput.Columns["Number"].ColumnName = "病案号";
            dtOutput.Columns["Name"].ColumnName = "姓名";
            dtOutput.Columns["Age"].ColumnName = "年龄";
            dtOutput.Columns.Add("性别", typeof(String));
            dtOutput.Columns["性别"].SetOrdinal(3);
            dtOutput.Columns.Add("IPAQ_0是否知道", typeof(String));

            dtOutput.Columns["IPAQNumber"].ColumnName = "IPAQ编号";            
            dtOutput.Columns["IPAQ1"].ColumnName = "IPAQ_1";            
            dtOutput.Columns["IPAQ2"].ColumnName = "IPAQ_2";            
            dtOutput.Columns["IPAQ3"].ColumnName = "IPAQ_3";            
            dtOutput.Columns["IPAQ4"].ColumnName = "IPAQ_4";           
            dtOutput.Columns["IPAQ5"].ColumnName = "IPAQ_5";
            

            foreach (DataRow dRow in dtOutput.Rows)
            {
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

                switch (dRow["IPAQ0"].ToString())
                {
                    case "True":
                        dRow["IPAQ_0是否知道"] = "是";
                        break;
                    case "False":
                        dRow["IPAQ_0是否知道"] = "否";
                        break;
                    default:
                        dRow["IPAQ_0是否知道"] = " ";
                        break;
                }
            }

            dtOutput.Columns.Remove("IPAQ0");
            dtOutput.Columns.Remove("Male");

            return dtOutput;
        }
        public static DataTable OHQConverter(DataTable dt)
        {
            DataTable dtOutput = dt.Copy();
            dtOutput.TableName = "05-OHQ";

            dtOutput.Columns["Number"].ColumnName = "病案号";
            dtOutput.Columns["Name"].ColumnName = "姓名";
            dtOutput.Columns["Age"].ColumnName = "年龄";
            dtOutput.Columns.Add("性别", typeof(String));
            dtOutput.Columns["性别"].SetOrdinal(3);           

            dtOutput.Columns["OHQNumber"].ColumnName = "OHQ编号";           

            try
            {
                foreach (DataRow dRow in dtOutput.Rows)
                {
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

                    switch (dRow["OHQ1"].ToString())
                    {
                        case "0":
                            dRow["OHQ1"] = "3次以上";
                            break;
                        case "1":
                            dRow["OHQ1"] = "2次";
                            break;
                        case "2":
                            dRow["OHQ1"] = "1次";
                            break;
                        case "3":
                            dRow["OHQ1"] = "0次";
                            break;
                        default:
                            dRow["OHQ1"] = "";
                            break;
                    }
                    switch (dRow["OHQ2"].ToString())
                    {
                        case "0":
                            dRow["OHQ2"] = "每天";
                            break;
                        case "1":
                            dRow["OHQ2"] = "有时使用";
                            break;
                        case "2":
                            dRow["OHQ2"] = "不使用";
                            break;
                        default:
                            dRow["OHQ2"] = "";
                            break;
                    }
                    switch (dRow["OHQ3"].ToString())
                    {
                        case "0":
                            dRow["OHQ3"] = "1个";
                            break;
                        case "1":
                            dRow["OHQ3"] = "2个";
                            break;
                        case "2":
                            dRow["OHQ3"] = "3-5个";
                            break;
                        case "3":
                            dRow["OHQ3"] = "6-9个";
                            break;
                        case "4":
                            dRow["OHQ3"] = "10个及以上";
                            break;
                        case "5":
                            dRow["OHQ3"] = "0个（没掉）";
                            break;
                        default:
                            dRow["OHQ3"] = "";
                            break;
                    }
                    if (dRow["OHQ4"].ToString().Length > 0)
                    {
                        string[] OHQ4 = (dRow["OHQ4"].ToString()).Split('-');
                        switch (OHQ4[0])
                        {
                            case "A":
                                {
                                    if (OHQ4[1].Trim().Length > 0)
                                        dRow["OHQ4"] = "经常(每周" + OHQ4[1] + "次";
                                    else
                                        dRow["OHQ4"] = "经常";
                                }
                                break;
                            case "B":
                                {
                                    if (OHQ4[1].Trim().Length > 0)
                                        dRow["OHQ4"] = "偶尔(每周" + OHQ4[1] + "次";
                                    else
                                        dRow["OHQ4"] = "偶尔";
                                }
                                break;
                            case "C":
                                dRow["OHQ4"] = "无(不出血)";
                                break;
                            default:
                                dRow["OHQ4"] = "";
                                break;
                        }
                    }
                    switch (dRow["OHQ5"].ToString())
                    {
                        case "0":
                            dRow["OHQ5"] = "从来没有";
                            break;
                        case "1":
                            dRow["OHQ5"] = "现在有";
                            break;
                        case "2":
                            dRow["OHQ5"] = "现在没有，但有时复发";
                            break;
                        case "3":
                            dRow["OHQ5"] = "以前有，现在好了";
                            break;
                        default:
                            dRow["OHQ5"] = "";
                            break;
                    }

                    if (dRow["OHQ6"].ToString().Trim().Length > 0)
                    {
                        switch (dRow["OHQ6"].ToString())
                        {
                            case "0":
                                dRow["OHQ6"] = "否";
                                break;
                            case "99":
                                dRow["OHQ6"] = "全部替换";
                                break;
                            default:
                                dRow["OHQ6"] = "部分是（共" + dRow["OHQ6"].ToString() + "颗假牙";
                                break;
                        }
                    }
                    else
                        dRow["OHQ6"] = "";

                    switch (dRow["OHQ7"].ToString())
                    {
                        case "0":
                            dRow["OHQ7"] = "清水浸泡";
                            break;
                        case "1":
                            dRow["OHQ7"] = "假牙清洁剂浸泡";
                            break;
                        case "2":
                            dRow["OHQ7"] = "用牙膏刷";
                            break;
                        default:
                            dRow["OHQ7"] = "";
                            break;
                    }

                    switch (dRow["OHQ8"].ToString())
                    {
                        case "0":
                            dRow["OHQ8"] = "<1分钟";
                            break;
                        case "1":
                            dRow["OHQ8"] = "1-2分钟";
                            break;
                        case "2":
                            dRow["OHQ8"] = "2分钟以上";
                            break;
                        default:
                            dRow["OHQ8"] = "";
                            break;
                    }
                    if (dRow["OHQ9"].ToString().Length > 0)
                    {
                        string[] OHQ9 = (dRow["OHQ9"].ToString()).Split('-');
                        switch (OHQ9[0])
                        {
                            case "A":
                                dRow["OHQ9"] = "否";
                                break;
                            case "B":
                                dRow["OHQ9"] = "是（选择原因:";
                                if (OHQ9[1].Trim().Length > 0)
                                    dRow["OHQ9"] += "常规检查" + OHQ9[1].ToString() + "次/年;";
                                if (OHQ9[2].Trim().Length > 0)
                                    dRow["OHQ9"] += "生病" + OHQ9[2].ToString() + "次/年;";
                                dRow["OHQ9"] += ")";
                                break;
                            default:
                                dRow["OHQ9"] = "";
                                break;
                        }
                    }

                    else
                        dRow["OHQ9"] = "";
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("ExerciseConverter" + ex.Message + ex.ToString() + "\n" );
            }

        

        
           
            dtOutput.Columns.Remove("Male");

            return dtOutput;
        }

        public static DataTable PHQConverter(DataTable dt)
        {
            DataTable dtOutput = dt.Copy();
            dtOutput.TableName = "06-PHQ";

            dtOutput.Columns["Number"].ColumnName = "病案号";
            dtOutput.Columns["Name"].ColumnName = "姓名";
            dtOutput.Columns["Age"].ColumnName = "年龄";
            dtOutput.Columns["PHQNumber"].ColumnName = "记录编号";
            dtOutput.Columns.Add("性别", typeof(String));
            dtOutput.Columns["性别"].SetOrdinal(3);
            dtOutput.Columns.Add("PHQ_7", typeof(String));

            foreach (DataRow dRow in dtOutput.Rows)
            {
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

                string[] strPHQs = (dRow["PHQResult"].ToString()).Split('|');
                int PHQ_7 = 0;
                foreach (string strPHQ in strPHQs)
                {
                    if (strPHQ.Length > 0)
                    {


                        int nPHQ = Convert.ToInt32(strPHQ);
                        if (nPHQ > 0)
                        {
                            PHQ_7 += nPHQ;
                        }
                    }
                }
                dRow["PHQ_7"] = PHQ_7.ToString();
            }

            dtOutput.Columns.Remove("PHQResult");
            dtOutput.Columns.Remove("Male");

            return dtOutput;
        }


    }

   

    public class BasicInfo
    {
        [Key]
        public string Number { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }
        public bool? Male { get; set; }
        public string Killip { get; set; }
        public string EF { get; set; }
        public string LV { get; set; }
        public string BasicOther { get; set; }
        public string BasicRisk { get; set; }
        public string RiskOther { get; set; }
        public int? PCI { get; set; }
        public int? ResidualStenosis { get; set; }
        public bool? CollatCirc { get; set; }
        public int? DominantCoronary { get; set; }
        public string Description { get; set; }
        public string Hb { get; set; }
        public string Alb { get; set; }
        public string Cre { get; set; }
        public string BUN { get; set; }
        public string Glu { get; set; }
        public string HbAlc { get; set; }
        public string BNP { get; set; }
        public string D2 { get; set; }
        public string Tchol { get; set; }
        public string TG { get; set; }
        public string HDLC { get; set; }
        public string LDLC { get; set; }
        public string UA { get; set; }
        public string ABI { get; set; }
        public string cTnT { get; set; }
        public string LY { get; set; }
        public List<Exercise> Exercise { get;set; }
    }

    public class BasicInfoTable
    {
        [Key]
        public string 病案号 { get; set; }
        public string 姓名 { get; set; }
        public int? 年龄 { get; set; }
        public string 性别 { get; set; }
        public string Killip { get; set; }
        public string EF { get; set; }
        public string LV { get; set; }
        public string 其他 { get; set; }
        public string 危险因素 { get; set; }        
        public int? PCI支架数 { get; set; }
        public int? _75以上残余狭窄 { get; set; }
        public string 侧枝循环 { get; set; }
        public string 优势冠脉 { get; set; }
        public string 诊断 { get; set; }
        public string Hb { get; set; }
        public string Alb { get; set; }
        public string Cre { get; set; }
        public string BUN { get; set; }
        public string Glu { get; set; }
        public string HbAlc { get; set; }
        public string BNP { get; set; }
        public string D_二聚体 { get; set; }
        public string Tchol { get; set; }
        public string TG { get; set; }
        public string HDLC { get; set; }
        public string LDLC { get; set; }
        public string UA { get; set; }
        public string ABI { get; set; }
        public string cTnT { get; set; }
        public string LY { get; set; }

    }
    public class Exercise
    {
        [Key]
        public string ExerciseNumber { get; set; }
        public bool? InRoomUp { get; set; }
        public string Date { get; set; }
        public string BloodPressureLower { get; set; }
        public string BloodPressureUpper { get; set; }
        public string HeartRate { get; set; }
        public string BloodOxygen { get; set; }
        public string BorgIndex { get; set; }
        public string Remarks { get; set; }
        public string ECGs { get; set; }
        public string Checks { get; set; }
        public string basicinfoNumber { set; get; }
        public BasicInfo basicinfo { get; set; }

    }

    public class ExerciseTable
    {
        [Key]
        public string 病案号 { get; set; }
        public string 姓名 { get; set; }
        public string 性别 { get; set; }
        public string 年龄 { get; set; }
        public string 记录编号 { get; set; }
        public string 床上负荷 { get; set; }
        public string 室内负荷 { get; set; }
        public string 室外负荷 { get; set; }
        public string 院外负荷 { get; set; }        

    }

    public class GAD
    {
        [Key]
        public string GADNumber { get; set; }       
        public string GADResult { get; set; }
        public string basicinfoNumber { set; get; }
        public BasicInfo basicinfo { get; set; }
    }

    public class GADTable
    {
        [Key]
        public string 病案号 { get; set; }
        public string 姓名 { get; set; }
        public string 性别 { get; set; }
        public string 年龄 { get; set; }
        public string GAD_7 { get; set; }

    }

    public class IPAQ
    {
        [Key]
        public string IPAQNumber { get; set; }
        public bool? IPAQ0 { get; set; }
        public string IPAQ1 { get; set; }
        public string IPAQ2 { get; set; }
        public string IPAQ3 { get; set; }
        public string IPAQ4 { get; set; }
        public string IPAQ5 { get; set; }
        public string basicinfoNumber { set; get; }
        public BasicInfo basicinfo { get; set; }
    }

    public class IPAQTable
    {
        [Key]
        public string 病案号 { get; set; }
        public string 姓名 { get; set; }
        public string 性别 { get; set; }
        public string 年龄 { get; set; }
        public string IPAQ编号 { get; set; }
        public string IPAQ_0是否知道 { get; set; }
        public string IPAQ_1 { get; set; }
        public string IPAQ_2 { get; set; }
        public string IPAQ_3 { get; set; }
        public string IPAQ_4 { get; set; }
        public string IPAQ_5 { get; set; }
        
    }

    public class OHQ
    {
        [Key]
        public string OHQNumber { get; set; }       
        public string OHQ1 { get; set; }
        public string OHQ2 { get; set; }
        public string OHQ3 { get; set; }
        public string OHQ4 { get; set; }
        public string OHQ5 { get; set; }
        public string OHQ6 { get; set; }
        public string OHQ7 { get; set; }
        public string OHQ8 { get; set; }
        public string OHQ9 { get; set; }
        public string basicinfoNumber { set; get; }
        public BasicInfo basicinfo { get; set; }
    }

    public class OHQTable
    {
        [Key]
        public string 病案号 { get; set; }
        public string 姓名 { get; set; }
        public string 性别 { get; set; }
        public string 年龄 { get; set; }
        public string OHQ编号 { get; set; }        
        public string OHQ1 { get; set; }
        public string OHQ2 { get; set; }
        public string OHQ3 { get; set; }
        public string OHQ4 { get; set; }
        public string OHQ5 { get; set; }
        public string OHQ6 { get; set; }
        public string OHQ7 { get; set; }
        public string OHQ8 { get; set; }
        public string OHQ9 { get; set; }        
    }
    public class PHQ
    {
        [Key]
        public string PHQNumber { get; set; }
        public string PHQResult { get; set; }
        public string basicinfoNumber { set; get; }
        public BasicInfo basicinfo { get; set; }
    }
    public class PHQTable
    {
        [Key]
        public string 病案号 { get; set; }
        public string 姓名 { get; set; }
        public string 性别 { get; set; }
        public string 年龄 { get; set; }
        public string PHQ_7 { get; set; }

    }



}
