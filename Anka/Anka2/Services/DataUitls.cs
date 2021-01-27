using Anka2.Models;
using Microsoft.EntityFrameworkCore;
using System;
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
                    var existingPerson = context.DbPerson.Find(personId);
                    if (existingPerson != null)
                    {
                        newPerson = existingPerson;
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
                MessageBox.Show("新建档案错误，错误信息为：" + e.Message);
                return NewPersonResult.Error;
            }            
        }


        public static bool CheckNewPerson(ref BasicInfo newPerson)
        {
            string personId = newPerson.Number;
            try
            {
                using (var context = new DbAdapter())
                {
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
            }
            catch (Exception e)
            {
                MessageBox.Show("档案号查询错误，错误信息为：" + e.Message);
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
                    if (existingPerson!=null)
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

        public static bool BasicRisk2Checked(string BasicRisk, int SN)
        {
            if (!String.IsNullOrEmpty(BasicRisk))
            {
                Char[] CharBasicRisks = BasicRisk.ToCharArray();
                return CharBasicRisks[SN] == '1';
            }
            else
                return false;
        }

        public static string Checked2BasicRisk(string BasicRisk, int SN, bool IsChecked)
        {
            if (String.IsNullOrEmpty(BasicRisk)) 
                BasicRisk = "000000000000";

            Char[] CharBasicRisks = BasicRisk.ToCharArray();
            CharBasicRisks[SN] = IsChecked ? '1' : '0';
            BasicRisk = new string(CharBasicRisks);
            return BasicRisk;
        }

        public static bool IsPersonId(string personId)
        {           
            Regex personIdReg = new Regex("^(\\d{8})$");
            return personIdReg.IsMatch(personId.ToString());          

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
        Error
    }

    public enum NewPersonResult
    {
        Error,
        Success_New,
        Success_Load
    }
}
