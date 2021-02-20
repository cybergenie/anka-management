using Anka2.Services;
using Anka2.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace Anka2.Models
{
    public class RepairTools
    {
        async public static Task RepairData()
        {            
            RepairInfo repairInfo;
            repairInfo = new RepairInfo();
            await Task.Run(() =>
            {               
                App.Current.Dispatcher.Invoke((Action)(() =>
                {  
                    repairInfo.Show();
                }));                
              
               

            });

        }
       
    }
}
