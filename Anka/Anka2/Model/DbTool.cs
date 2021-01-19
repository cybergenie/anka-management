using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Anka2.Model
{
    public static class DbTool
    {
        public static bool  AddPerson( BasicInfo newPerson)
        {
            string personId = newPerson.Number;
            using(var context = new DbAdapter())
            {
                var personList = context.DbPerson
                    .Where(b => b.Number== personId)
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
            return true;
        }    
    }
}
