using Anka2.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace Anka2.Services
{
    public class DataUitls : IDataUitls
    {
        public bool AddPerson(BasicInfo newPerson)
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
            catch (Exception e)
            {
                MessageBox.Show("新建档案错误，错误信息为：" + e.Message);
                return false;
            }
            return true;
        }


        public Window GetParentWindow(DependencyObject Source)
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
    }
}
