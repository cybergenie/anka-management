using Anka2.Models;
using System;
using System.Linq;
using System.Windows;

namespace Anka2.Services
{
    public class DataService: IDataService
    {
        public bool AddPerson( BasicInfo newPerson)
        {
            string personId = newPerson.Number;
            try
            {
                using (var context = new DbAdapter())
                {
                    var personList = context.DbPerson
                        .Where(b => b.Number == personId)
                        .ToList();
                    if (personList.Count > 0)
                    {
                        MessageBox.Show("当前档案号已存在，请重新输入。");
                        return false;
                    }
                    else
                    {
                        context.Add(newPerson);
                    }
                    context.SaveChanges();
                }
            }
            catch (Exception)
            {                
                return false;                
            }
            return true;
        }    
    }
}
