using Anka2.Models;
using Anka2.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Anka2.Services
{
    public class DataService: NotifyObject
    {

        private static DataTable ToDataTable<T>(IList<T> data, Dictionary<string, string> Dict)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (var prop in Dict)
            {               
                    table.Columns.Add(prop.Value, typeof(string));
            }
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                {
                    if (prop.PropertyType.Name.Contains("List"))
                    {
                        List<Exercise> pExercise = prop.GetValue(item) as List<Exercise>;  
                    }
                    if (Dict.Keys.Contains(prop.Name))
                        row[Dict[prop.Name]] = (prop.GetValue(item) ?? DBNull.Value).ToString();
                }
                table.Rows.Add(row);
            }
            return table;
        }

        private DataView dtBasicInfo;
        public DataView DtBasicInfo
        {
            get
            {
                Dictionary<string, string> DicBasicInfo = new Dictionary<string, string> {
                    { "Number", "病案号" },
                    { "Name",  "姓名" },
                    { "Age", "年龄" },
                    { "Male",  "性别" },
                    { "Description", "诊断"  },
                    { "Killip", "Killip/NYHA" },
                    { "EF",  "EF"  },
                    { "LV",  "LV"  },
                    { "BasicOther", "其他" },
                    { "BasicRisk", "危险因素" },
                    { "RiskOther", "其他危险因素" },
                    { "Hb", "Hb" },
                    { "Alb", "Alb" },
                    { "Cre", "Cre" },
                    { "BUN", "BUN" },
                    { "Glu", "Glu" },
                    { "HbAlc", "HbAlc" },
                    { "BNP", "BNP" },
                    { "D2", "D-二聚体" },
                    { "Tchol", "Tchol" },
                    { "HDLC", "HDL-C" },
                    { "LDLC", "LDL-C" },
                    { "UA", "UA" },
                    { "ABI", "ABI" },
                    { "cTnT", "cTnT" },
                    { "LY", "LY" },
                    { "PCI",  "PCI"  },
                    { "ResidualStenosis", "75%以上残余狭窄数目"  },
                    { "CollatCirc", "侧枝循环"  },
                    { "DominantCoronary", "优势冠脉" } 
                };
                using var context = new DbAdapter();
                var BasicInfoList = context.DbPerson.ToList();
                DataTable DtBasicInfo = ToDataTable<BasicInfo>(BasicInfoList, DicBasicInfo);
                DtBasicInfo.TableName = "基本信息";
                DataUitls.BasicInfoValueConvertor(ref DtBasicInfo);
                return DtBasicInfo.DefaultView;
            }
            set
            {
                if (dtBasicInfo != value)
                {
                    dtBasicInfo = value;
                    RaisePropertyChanged(nameof(DtBasicInfo));
                }
            }


        }

        private DataView _dtExercise;
        public DataView DtExercise
        {
            get
            {
                Dictionary<string, string> DicExercise = new Dictionary<string, string> {
                    { "Number", "病案号" },
                    { "Name",  "姓名" },
                    { "Age", "年龄" },
                    { "Male",  "性别" },
                    { "ExerciseNumber", "记录编号"  },
                    { "Checks", "床上负荷|室内负荷|室外负荷|院外负荷" }
                };
            
                using var context = new DbAdapter();
                var BasicInfoList = context.DbPerson
                    .Include(BasicInfo => BasicInfo.PExercise)
                    .ToList();
                var ExerciseList = BasicInfoList;

                DataTable DtExercise = ToDataTable<BasicInfo>(ExerciseList, DicExercise);
                DtExercise.TableName = "运动负荷";
                DataUitls.ExerciseValueConvertor(ref DtExercise);
                return DtExercise.DefaultView;
            }
            set
            {
                if (_dtExercise != value)
                {
                    _dtExercise = value;
                    RaisePropertyChanged(nameof(DtExercise));
                }
            }


        }


    }
}
