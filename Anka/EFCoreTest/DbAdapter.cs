using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Reflection;
using System.Text;

namespace EFCoreTest
{
    public class DbAdapter: DbContext
    {
        public DbSet<BasicInfo> basicinfo { get; set; }
        public DbSet<Exercise> Exercise { get; set; }

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(
                "Data Source=anka.db");
            optionsBuilder.UseLazyLoadingProxies();
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
        public virtual BasicInfo basicinfo { get; set; }
        
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


}
