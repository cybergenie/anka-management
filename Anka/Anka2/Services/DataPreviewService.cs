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
                GADExportConverter.GADValueConvertor(ref DtGAD);
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
        public DataView _dtIPAQ;
        public DataView DtIPAQ
        {
            get
            {
                Dictionary<string, string> DicIPAQ = new Dictionary<string, string> {
                    { "Number", "病案号" },
                    { "Name",  "姓名" },
                    { "Age", "年龄" },
                    { "Male",  "性别" },
                    { "IPAQNumber", "记录编号"  },
                    { "IPAQ0", "是否知道"  },
                    { "IPAQ1", "IPAQ-1(天)"  },
                    { "IPAQ2", "IPAQ-2(天)"  },
                    { "IPAQ3", "IPAQ-3(天)"  },
                    { "IPAQ4", "IPAQ-4(分钟)"  },
                    { "IPAQ5", "IPAQ-5(步/天)"  },


                };
                using var context = new DbAdapter();
                var IPAQList = (from basicInfo in context.DbPerson
                               join ipaq in context.DbIPAQ
                               on basicInfo.Number equals ipaq.basicinfoNumber
                               select new IPAQList
                               {
                                   Number = basicInfo.Number,
                                   Name = basicInfo.Name,
                                   Male = basicInfo.Male,
                                   Age = basicInfo.Age,
                                   IPAQNumber = ipaq.IPAQNumber.Remove(0, basicInfo.Number.Length + 1),
                                   IPAQ0 = ipaq.IPAQ0,
                                   IPAQ1 = ipaq.IPAQ1,
                                   IPAQ2 = ipaq.IPAQ2,
                                   IPAQ3 = ipaq.IPAQ3,
                                   IPAQ4 = ipaq.IPAQ4,
                                   IPAQ5 = ipaq.IPAQ5
                               }).ToList();
                DataTable DtIPAQ = ToDataTable<IPAQList>(IPAQList, DicIPAQ);
                DtIPAQ.TableName = "IPAQ评估量表";
                IPAQExportConverter.IPAQValueConvertor(ref DtIPAQ);
                return DtIPAQ.DefaultView;

            }
            set
            {
                if (_dtIPAQ != value)
                {
                    _dtIPAQ = value;
                }
                RaisePropertyChanged(nameof(DtIPAQ));
            }
        }

        public DataView _dtOHQ;
        public DataView DtOHQ
        {
            get
            {
                Dictionary<string, string> DicOHQ = new Dictionary<string, string> {
                    { "Number", "病案号" },
                    { "Name",  "姓名" },
                    { "Age", "年龄" },
                    { "Male",  "性别" },
                    { "OHQNumber", "记录编号"  },                    
                    { "OHQ1", "OHQ-1"  },
                    { "OHQ2", "OHQ-2"  },
                    { "OHQ3", "OHQ-3"  },
                    { "OHQ4", "OHQ-4"  },
                    { "OHQ5", "OHQ-5"  },
                    { "OHQ6", "OHQ-6"  },
                    { "OHQ7", "OHQ-7"  },
                    { "OHQ8", "OHQ-8"  },
                    { "OHQ9", "OHQ-9"  },
                };
                using var context = new DbAdapter();
                var OHQList = (from basicInfo in context.DbPerson
                                join ohq in context.DbOHQ
                                on basicInfo.Number equals ohq.basicinfoNumber
                                select new OHQList
                                {
                                    Number = basicInfo.Number,
                                    Name = basicInfo.Name,
                                    Male = basicInfo.Male,
                                    Age = basicInfo.Age,
                                    OHQNumber = ohq.OHQNumber.Remove(0, basicInfo.Number.Length + 1),
                                    OHQ1 = ohq.OHQ1,
                                    OHQ2 = ohq.OHQ2,
                                    OHQ3 = ohq.OHQ3,
                                    OHQ4 = ohq.OHQ4,
                                    OHQ5 = ohq.OHQ5,
                                    OHQ6 = ohq.OHQ6,
                                    OHQ7 = ohq.OHQ7,
                                    OHQ8 = ohq.OHQ8,
                                    OHQ9 = ohq.OHQ9,

                                }).ToList();
                DataTable DtOHQ = ToDataTable<OHQList>(OHQList, DicOHQ);
                DtOHQ.TableName = "口腔卫生评估量表";
                OHQExportConverter.OHQValueConvertor(ref DtOHQ);
                return DtOHQ.DefaultView;

            }
            set
            {
                if (_dtOHQ != value)
                {
                    _dtOHQ = value;
                }
                RaisePropertyChanged(nameof(DtOHQ));
            }

        }
        public DataView _dtSPPB;
        public DataView DtSPPB
        {
            get
            {
                Dictionary<string, string> DicSPPB = new Dictionary<string, string> {
                    { "Number", "病案号" },
                    { "Name",  "姓名" },
                    { "Age", "年龄" },
                    { "Male",  "性别" },
                    { "SPPBNumber", "记录编号"  },
                    { "BalanceTesting1", "平衡测试-1"  },
                    { "BalanceTesting2", "平衡测试-2"  },
                    { "BalanceTesting3", "平衡测试-3"  },
                    { "walkingTesting1", "4米行走-1"  },
                    { "walkingTesting2", "4米行走-2"  },
                    { "SitUpTesting", "坐起立"  },
                    { "TUG", "3米往返步行(TUG)"  },
                    { "FRTLeft1", "体前屈(左)-1"  },
                    { "FRTLeft2", "体前屈(左)-2"  },
                    { "FRTRight1", "体前屈(右)-1"  },
                    { "FRTRight2", "体前屈(右)-2"  },
                    { "SFO1", "座位双脚开闭-1"  },
                    { "SFO2", "座位双脚开闭-2"  },
                    { "OneFootLeft1", "单脚站立(左)-1"  },
                    { "OneFootLeft2", "单脚站立(左)-2"  },
                    { "OneFootRight1", "单脚站立(右)-1"  },
                    { "OneFootRight2", "单脚站立(右)-2"  },
                    { "Hight", "身高"  },
                    { "Weight", "体重"  },
                    { "Waistline", "腰围"  },
                    { "Hipline", "臀围"  },
                    { "ArmlineLeft", "上臂围度(左)"  },
                    { "ArmlineRight", "上臂围度(右)"  },
                    { "LeglineLeft", "大腿围度(左)"  },
                    { "LeglineRight", "大腿围度(右)"  },
                    { "BloodPressureUpper", "血压"  },
                    { "BloodPressureLower", "BloodPressureLower"  },
                    { "HeartRate", "心率"  },
                    { "Temperature", "体温"  },
                    { "Breathe", "呼吸"  },
                    { "LeftHandHurt", "左手外伤"  },
                    { "RightHandHurt", "右手外伤"  },
                    { "GripStrengthLeft1", "左手握力-1"  },
                    { "GripStrengthLeft2", "左手握力-2"  },
                    { "GripStrengthRight1", "右手握力-1"  },
                    { "GripStrengthRight2", "右手握力-2"  },
                    { "LeftLapHurt", "左腿外伤"  },
                    { "RightLapHurt", "右腿外伤"  },
                    { "LapStrengthLeft1", "伸膝力(左)-1"  },
                    { "LapStrengthLeft2", "伸膝力(左)-2"  },
                    { "LapStrengthRight1", "伸膝力(右)-1"  },
                    { "LapStrengthRight2", "伸膝力(右)-2"  }
                };
                using var context = new DbAdapter();
                var SPPBList = (from basicInfo in context.DbPerson
                               join sppb in context.DbSPPB
                               on basicInfo.Number equals sppb.basicinfoNumber
                               select new SPPBList
                               {
                                   Number = basicInfo.Number,
                                   Name = basicInfo.Name,
                                   Male = basicInfo.Male,
                                   Age = basicInfo.Age,
                                   SPPBNumber = sppb.SPPBNumber.Remove(0, basicInfo.Number.Length + 1),
                                   BalanceTesting1 = sppb.BalanceTesting1,
                                   BalanceTesting2 = sppb.BalanceTesting2,
                                   BalanceTesting3 = sppb.BalanceTesting3,
                                   walkingTesting1 = sppb.walkingTesting1,
                                   walkingTesting2 = sppb.walkingTesting2,
                                   SitUpTesting = sppb.SitUpTesting,
                                   TUG = sppb.TUG,
                                   FRTLeft1 = sppb.FRTLeft1,
                                   FRTLeft2 = sppb.FRTLeft2,
                                   FRTRight1 = sppb.FRTRight1,
                                   FRTRight2 = sppb.FRTRight2,
                                   SFO1 = sppb.SFO1,
                                   SFO2 = sppb.SFO2,
                                   OneFootLeft1 = sppb.OneFootLeft1,
                                   OneFootLeft2 = sppb.OneFootLeft2,
                                   OneFootRight1 = sppb.OneFootRight1,
                                   OneFootRight2 = sppb.OneFootRight2,
                                   Hight = sppb.Hight,
                                   Weight = sppb.Weight,
                                   Waistline = sppb.Waistline,
                                   Hipline = sppb.Hipline,
                                   ArmlineLeft = sppb.ArmlineLeft,
                                   ArmlineRight = sppb.ArmlineRight,
                                   LeglineLeft = sppb.LeglineLeft,
                                   LeglineRight = sppb.LeglineRight,
                                   BloodPressureUpper = sppb.BloodPressureUpper,
                                   BloodPressureLower = sppb.BloodPressureLower,
                                   HeartRate = sppb.HeartRate,
                                   Temperature = sppb.Temperature,
                                   Breathe = sppb.Breathe,
                                   LeftHandHurt = sppb.LeftHandHurt,
                                   RightHandHurt = sppb.RightHandHurt,
                                   GripStrengthLeft1 = sppb.GripStrengthLeft1,
                                   GripStrengthRight1 = sppb.GripStrengthRight1,
                                   GripStrengthLeft2 = sppb.GripStrengthLeft2,
                                   GripStrengthRight2 = sppb.GripStrengthRight2,
                                   LeftLapHurt = sppb.LeftLapHurt,
                                   RightLapHurt = sppb.RightLapHurt,
                                   LapStrengthLeft1 = sppb.LapStrengthLeft1,
                                   LapStrengthRight1 = sppb.LapStrengthRight1,
                                   LapStrengthLeft2 = sppb.LapStrengthLeft2,
                                   LapStrengthRight2 = sppb.LapStrengthRight2
                               }).ToList();
                DataTable DtSPPB = ToDataTable<SPPBList>(SPPBList, DicSPPB);
                DtSPPB.TableName = "SPPB/平衡能力";
                SPPBExportConverter.SPPBValueConvertor(ref DtSPPB);
                return DtSPPB.DefaultView;

            }
            set
            {
                if (_dtSPPB != value)
                {
                    _dtSPPB = value;
                }
                RaisePropertyChanged(nameof(DtSPPB));
            }

        }

        public DataView _dtPhysique;
        public DataView DtPhysique
        {
            get
            {
                Dictionary<string, string> DicPhysique = new Dictionary<string, string> {
                    { "Number", "病案号" },
                    { "Name",  "姓名" },
                    { "Age", "年龄" },
                    { "Male",  "性别" },
                    { "PhysiqueNumber", "记录编号"  },
                    { "Weight", "体重"  },
                    { "Hight", "身高"  },
                    { "FM", "脂肪重量"  },
                    { "TBW", "体液总量(TBW)"  },
                    { "BCW", "细胞外液(BCW)"  },
                    { "SMMAll", "骨骼肌肉质量(SMM)"  },
                    { "SMMArmLeft", "左臂肌肉质量"  },
                    { "SMMBody", "躯干肌肉质量"  },
                    { "SMMArmRight", "右臂肌肉质量"  },
                    { "SMMLegLeft", "左腿肌肉质量"  },
                    { "SMMLegRight", "右腿肌肉质量"  },
                    { "VAT", "内脏脂肪(VAT)"  },
                    { "Waistline", "腰围 单位(WC)"  },
                    { "PA", "相角"  },
                    { "PAPercent", "百分位"  }                    
                };
                using var context = new DbAdapter();
                var PhysiqueList = (from basicInfo in context.DbPerson
                                join physique in context.DbPhysique
                                on basicInfo.Number equals physique.basicinfoNumber
                                select new PhysiqueList
                                {
                                    Number = basicInfo.Number,
                                    Name = basicInfo.Name,
                                    Male = basicInfo.Male,
                                    Age = basicInfo.Age,
                                    PhysiqueNumber = physique.PhysiqueNumber.Remove(0, basicInfo.Number.Length + 1),
                                    Weight = physique.Weight,
                                    Hight = physique.Hight,
                                    FM = physique.FM,
                                    TBW = physique.TBW,
                                    BCW = physique.BCW,
                                    SMMAll = physique.SMMAll,
                                    SMMArmLeft = physique.SMMArmLeft,
                                    SMMArmRight = physique.SMMArmRight,
                                    SMMBody = physique.SMMBody,
                                    SMMLegLeft = physique.SMMLegLeft,
                                    SMMLegRight = physique.SMMLegRight,
                                    VAT = physique.VAT,
                                    Waistline = physique.Waistline,
                                    PA = physique.PA,
                                    PAPercent = physique.PAPercent                                   
                                }).ToList();
                DataTable DtPhysique = ToDataTable<PhysiqueList>(PhysiqueList, DicPhysique);
                DtPhysique.TableName = "体质成分";
                PhysiqueExportConverter.PhysiqueValueConvertor(ref DtPhysique);
                return DtPhysique.DefaultView;

            }
            set
            {
                if (_dtPhysique != value)
                {
                    _dtPhysique = value;
                }
                RaisePropertyChanged(nameof(DtPhysique));
            }

        }



    }
}
