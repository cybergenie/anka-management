using Anka2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media;

namespace Anka2.Services
{
    public class DataUitls
    {
        public static NewPersonResult AddNewPerson(ref BasicInfo newPerson)
        {
            string personId = newPerson.Number;
            try
            {
                using (var context = new DbAdapter())
                {
                    var existingPerson = context.DbPerson
                        .Where(BasicInfo => BasicInfo.Number == personId)
                        .Include(BasicInfo => BasicInfo.PExercise)
                        .Include(BasicInfo => BasicInfo.PGAD)
                        .Include(BasicInfo => BasicInfo.PPHQ)
                        .Include(BasicInfo => BasicInfo.PIPAQ)
                        .Include(BasicInfo => BasicInfo.POHQ)
                        .Include(BasicInfo => BasicInfo.PSPPB)
                        .Include(BasicInfo => BasicInfo.PPhysique)
                        .ToList();

                    if (existingPerson.Count > 0)
                    {
                        newPerson = existingPerson[0];
                        context.SaveChanges();
                        return NewPersonResult.Success_Load;
                    }
                    else
                    {
                        context.Add(newPerson);
                        context.SaveChanges();
                        return NewPersonResult.Success_New;
                    }

                }
            }
            catch (Exception e)
            {
                MessageBox.Show("新建档案错误，错误信息为：" + e.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return NewPersonResult.Error;
            }
        }
        public static bool CheckNewPerson(ref BasicInfo newPerson)
        {
            string personId = newPerson.Number;
            try
            {
                using var context = new DbAdapter();
                var existingPerson = context.DbPerson.Find(personId);

                if (existingPerson != null)
                {
                    var existingPersonList = context.DbPerson.
                         Include(BasicInfo => BasicInfo.PExercise).
                        Include(BasicInfo => BasicInfo.PGAD).
                        Include(BasicInfo => BasicInfo.PPHQ).
                        Include(BasicInfo => BasicInfo.PIPAQ).
                        Include(BasicInfo => BasicInfo.POHQ).
                        Include(BasicInfo => BasicInfo.PSPPB).
                        Include(BasicInfo => BasicInfo.PPhysique).
                        Where<BasicInfo>(p => p.Number == personId).
                        ToList();

                    newPerson = existingPersonList[0];
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("档案号查询错误，错误信息为：" + e.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        public static bool SavePersonInfo( BasicInfo Person)
        {
            string personId = Person.Number;
            try
            {
                SaveBasicInfo(Person);
                foreach(var item in Person.PExercise)
                {
                    SaveExercise(item);
                }
                foreach (var item in Person.PGAD)
                {
                    SaveGAD(item);
                }               
                foreach (var item in Person.PIPAQ)
                {
                    SaveIPAQ(item);
                }
                foreach (var item in Person.POHQ)
                {
                    SaveOHQ(item);
                }
                foreach (var item in Person.PPHQ)
                {
                    SavePHQ(item);
                }
                foreach (var item in Person.PPhysique)
                {
                    SavePhysique(item);
                }
                foreach (var item in Person.PSPPB)
                {
                    SaveSPPB(item);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("新建档案错误，错误信息为：" + e.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            return true;

        }

        public static bool SaveBasicInfo(BasicInfo basicInfo)
        {
            string basicInfoId = basicInfo.Number;
            try
            {
                using (var context = new DbAdapter())
                {
                    var existingPerson = context.DbPerson.Find(basicInfoId);
                    if (existingPerson != null)
                    {
                        context.Entry(existingPerson).CurrentValues.SetValues(basicInfo);
                        context.SaveChanges();
                    }
                    else
                    {
                        MessageBox.Show("档案号不存在。", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("新建档案错误，错误信息为：" + e.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            return true;
        }

        public static bool SaveExercise(Exercise exercise)
        {
            string exerciseId = exercise.ExerciseNumber;
            try
            {
                using (var context = new DbAdapter())
                {
                    var existingExercise = context.DbExercise.Find(exerciseId);
                    if (existingExercise != null)
                    {
                        context.Entry(existingExercise).CurrentValues.SetValues(exercise);
                        
                    }
                    else
                    {
                        context.Add(exercise);                        
                    }
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("新建档案错误，错误信息为：" + e.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            return true;
        }

        public static bool SaveGAD(GAD gad)
        {
            string gadId = gad.GADNumber;
            try
            {
                using (var context = new DbAdapter())
                {
                    var existingGAD = context.DbGAD.Find(gadId);
                    if (existingGAD != null)
                    {
                        context.Entry(existingGAD).CurrentValues.SetValues(gad);
                        
                    }
                    else
                    {
                        context.Add(gad);
                    }
                    context.SaveChanges();
                }

            }
            catch (Exception e)
            {
                MessageBox.Show("新建档案错误，错误信息为：" + e.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            return true;
        }

        public static bool SavePHQ(PHQ phq)
        {
            string phqId = phq.PHQNumber;
            try
            {
                using (var context = new DbAdapter())
                {
                    var existingPHQ = context.DbPHQ.Find(phqId);
                    if (existingPHQ != null)
                    {
                        context.Entry(existingPHQ).CurrentValues.SetValues(phq);
                        context.SaveChanges();
                    }
                    else
                    {
                        context.Add(phq);
                    }
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("新建档案错误，错误信息为：" + e.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            return true;
        }

        public static bool SaveIPAQ(IPAQ ipaq)
        {
            string ipaqId = ipaq.IPAQNumber;
            try
            {
                using (var context = new DbAdapter())
                {
                    var existingIPAQ = context.DbIPAQ.Find(ipaqId);
                    if (existingIPAQ != null)
                    {
                        context.Entry(existingIPAQ).CurrentValues.SetValues(ipaq);
                        context.SaveChanges();
                    }
                    else
                    {
                        context.Add(ipaq);
                    }
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("新建档案错误，错误信息为：" + e.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            return true;
        }

        public static bool SaveOHQ(OHQ ohq)
        {
            string ohqId = ohq.OHQNumber;
            try
            {
                using (var context = new DbAdapter())
                {
                    var existingOHQ = context.DbOHQ.Find(ohqId);
                    if (existingOHQ != null)
                    {
                        context.Entry(existingOHQ).CurrentValues.SetValues(ohq);
                        context.SaveChanges();
                    }
                    else
                    {
                        context.Add(ohq);
                    }
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("新建档案错误，错误信息为：" + e.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            return true;
        }

        public static bool SaveSPPB(SPPB sppb)
        {
            string sppbId = sppb.SPPBNumber;
            try
            {
                using (var context = new DbAdapter())
                {
                    var existingSPPB = context.DbSPPB.Find(sppbId);
                    if (existingSPPB != null)
                    {
                        context.Entry(existingSPPB).CurrentValues.SetValues(sppb);
                        context.SaveChanges();
                    }
                    else
                    {
                        context.Add(sppb);
                    }
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("新建档案错误，错误信息为：" + e.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            return true;
        }

        public static bool SavePhysique(Physique physique)
        {
            string physiqueId = physique.PhysiqueNumber;
            try
            {
                using (var context = new DbAdapter())
                {
                    var existingPhysique = context.DbPhysique.Find(physiqueId);
                    if (existingPhysique != null)
                    {
                        context.Entry(existingPhysique).CurrentValues.SetValues(physique);
                        context.SaveChanges();
                    }
                    else
                    {
                        context.Add(physique);
                    }
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("新建档案错误，错误信息为：" + e.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            return true;
        }


        public static Window GetParentWindow(DependencyObject Source)
        {
            DependencyObject parent = VisualTreeHelper.GetParent(Source);
            while (parent != null)
            {
                if (parent is Window)
                {
                    break;
                }
                parent = VisualTreeHelper.GetParent(parent);
            }

            return (Window)parent;
        }

        public static bool IsPersonId(string personId)
        {
            Regex personIdReg = new Regex("^(\\d{8})$");
            return personIdReg.IsMatch(personId.ToString());

        }

        public static void GenderConvertor(DataRow dr, string columnName)
        {
            var gender = dr[columnName] switch
            {
                "True" => "男",
                "False" => "女",
                _ => string.Empty,
            };
            dr[columnName] = gender;
        }

        public static void DateConvertor(DataRow dr, string columnName)
        {
            try
            {
                string ItemNumber = dr[columnName] as string;
                string endNumber = ItemNumber.Substring(ItemNumber.Length - 3, 3);
                string result = Regex.Replace(ItemNumber.Substring(0, ItemNumber.Length - 3), @"[^0-9]+", "/") + endNumber;
                dr[columnName] = result;
            }
            catch (Exception)
            {
                dr[columnName] = dr[columnName] as string;
            }
        }

        public static string BackupFile(string originFile, string FilePath)
        {
            var file = originFile.Split('.');
            string fileType;
            string fileName;
            if (file.Length > 1)
            {
                fileType = file[file.Length - 1];
                fileName = originFile.Remove(originFile.Length - fileType.Length - 1, fileType.Length+1);
            }
            else
            {
                fileName = originFile;
            }
            string backupFile = fileName + "_" + DateTime.Now.ToString("yyMMddHHmmss");
            string pLocalFilePath = FilePath + originFile;//要复制的文件路径
            string pSaveFilePath = FilePath + backupFile + ".bak";//指定存储的路径
            try
            {
                if (File.Exists(pLocalFilePath))//必须判断要复制的文件是否存在
                {
                    File.Copy(pLocalFilePath, pSaveFilePath, false);//三个参数分别是源文件路径，存储路径，若存储路径有相同文件是否替换
                }
            }
            catch
            {
                MessageBox.Show("文件"+ FilePath+ originFile+"备份失败。","错误",MessageBoxButton.OK,MessageBoxImage.Error);
                return null;
            }
            return backupFile+".bak";
        }
    }

    public enum SheetItems
    {
        BasicInfo,
        Exercise,
        PHQ,
        GAD,
        IPAQ,
        OHQ,
        SPPB,
        Physique
    };
    public enum InfoType
    {
        Success,
        Warning,
        Error,
        Info
    }

    public enum NewPersonResult
    {
        Error,
        Success_New,
        Success_Load
    }
}
