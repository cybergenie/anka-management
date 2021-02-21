using Anka2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;

namespace Anka2.Services
{
    public class DataPreviewService : NotifyObject
    {

        private static DataTable ToDataTable<T>(IList<T> data, Dictionary<string, string> Dict)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();

            foreach (PropertyDescriptor prop in properties)
            {
                if (Dict.ContainsKey(prop.Name))
                {
                    table.Columns.Add(Dict[prop.Name], typeof(string));

                }

            }
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                {
                    if (Dict.Keys.Contains(prop.Name))
                        row[Dict[prop.Name]] = (prop.GetValue(item) ?? DBNull.Value).ToString();
                }

                foreach (PropertyDescriptor prop in properties)
                {
                    if (prop.PropertyType.Name.Contains("List"))
                    {
                        if (Dict.Keys.Contains(prop.Name))
                        {
                            row[Dict[prop.Name]] = (prop.GetValue(item) ?? DBNull.Value);
                        }
                    }
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
                    { "Killip", "Killip-NYHA" },
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
                    {"TG","TG" },
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
                BasicInfoExportConverter.BasicInfoValueConvertor(ref DtBasicInfo);
                return DtBasicInfo.DefaultView;
            }
            set
            {
                if (dtBasicInfo != value)
                {
                    dtBasicInfo = value;
                }
                RaisePropertyChanged(nameof(DtBasicInfo));
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
                    { "BedUp", "床上负荷" },
                    { "InRoom",  "室内负荷"},
                    { "OutRoom", "室外负荷"},
                    { "OutSide", "院外负荷" }

                };

                using var context = new DbAdapter();
                //var ExerciseList = context.DbPerson
                //    .Include(BasicInfo => BasicInfo.PExercise)
                //    .ToList();

                var ExerciseList = (from basicInfo in context.DbPerson
                                    join exercise in context.DbExercise
                                    on basicInfo.Number equals exercise.basicinfoNumber
                                    select new ExerciseList
                                    {
                                        Number = basicInfo.Number,
                                        Name = basicInfo.Name,
                                        Male = basicInfo.Male,
                                        Age = basicInfo.Age,
                                        ExerciseNumber = exercise.ExerciseNumber.Remove(0, basicInfo.Number.Length + 1),
                                        BedUp = exercise.Checks,
                                        InRoom = exercise.Checks,
                                        OutRoom = exercise.Checks,
                                        OutSide = exercise.Checks
                                    }).ToList();

                DataTable DtExercise = ToDataTable<ExerciseList>(ExerciseList, DicExercise);
                DtExercise.TableName = "运动负荷";
                ExerciseExportConverter.ExerciseValueConvertor(ref DtExercise);
                return DtExercise.DefaultView;
            }
            set
            {
                if (_dtExercise != value)
                {
                    _dtExercise = value;
                }
                RaisePropertyChanged(nameof(DtExercise));
            }


        }

        public DataView _dtGAD;
        public DataView DtGAD
        {
            get
            {
                Dictionary<string, string> DicGAD = new Dictionary<string, string> {
                    { "Number", "病案号" },
                    { "Name",  "姓名" },
                    { "Age", "年龄" },
                    { "Male",  "性别" },
                    { "GADNumber", "记录编号"  },
                    { "GADResult", "GAD评分" }

                };
                using var context = new DbAdapter();
                var GADList = (from basicInfo in context.DbPerson
                               join gad in context.DbGAD
                               on basicInfo.Number equals gad.basicinfoNumber
                               select new GADList
                               {
                                   Number = basicInfo.Number,
                                   Name = basicInfo.Name,
                                   Male = basicInfo.Male,
                                   Age = basicInfo.Age,
                                   GADNumber = gad.GADNumber.Remove(0, basicInfo.Number.Length + 1),
                                   GADResult = gad.GADResult
                               }).ToList();
                DataTable DtGAD = ToDataTable<GADList>(GADList, DicGAD);
                DtGAD.TableName = "GAD评估量表";
                IPAQExportConverter.GADValueConvertor(ref DtGAD);
                return DtGAD.DefaultView;

            }
            set
            {
                if (_dtGAD != value)
                {
                    _dtGAD = value;
                }
                RaisePropertyChanged(nameof(DtGAD));
            }
        }
        public DataView _dtPHQ;
        public DataView DtPHQ
        {
            get
            {
                Dictionary<string, string> DicPHQ = new Dictionary<string, string> {
                    { "Number", "病案号" },
                    { "Name",  "姓名" },
                    { "Age", "年龄" },
                    { "Male",  "性别" },
                    { "PHQNumber", "记录编号"  },
                    { "PHQResult", "PHQ评分" }

                };
                using var context = new DbAdapter();
                var PHQList = (from basicInfo in context.DbPerson
                               join phq in context.DbPHQ
                               on basicInfo.Number equals phq.basicinfoNumber
                               select new PHQList
                               {
                                   Number = basicInfo.Number,
                                   Name = basicInfo.Name,
                                   Male = basicInfo.Male,
                                   Age = basicInfo.Age,
                                   PHQNumber = phq.PHQNumber.Remove(0, basicInfo.Number.Length + 1),
                                   PHQResult = phq.PHQResult
                               }).ToList();
                DataTable DtPHQ = ToDataTable<PHQList>(PHQList, DicPHQ);
                DtPHQ.TableName = "PHQ评估量表";
                PHQExportConverter.PHQValueConvertor(ref DtPHQ);
                return DtPHQ.DefaultView;

            }
            set
            {
                if (_dtPHQ != value)
                {
                    _dtPHQ = value;
                }
                RaisePropertyChanged(nameof(DtPHQ));
            }
        }



    }
}
