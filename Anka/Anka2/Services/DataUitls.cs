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

        public static bool SavePersonInfo(BasicInfo Person)
        {
            string personId = Person.Number;
            try
            {
                using (var context = new DbAdapter())
                {
                    var existingPerson = context.DbPerson.Find(personId);
                    if (existingPerson != null)
                    {
                        context.Entry(existingPerson).CurrentValues.SetValues(Person);
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
            string ItemNumber = dr[columnName] as string;
            string endNumber = ItemNumber.Substring(ItemNumber.Length - 3, 3);
            string result = Regex.Replace(ItemNumber.Substring(0, ItemNumber.Length - 3), @"[^0-9]+", "/")+ endNumber;
            dr[columnName] = result;
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
