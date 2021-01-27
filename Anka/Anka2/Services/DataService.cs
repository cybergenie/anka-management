using Anka2.Models;
using Anka2.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anka2.Services
{
    public class DataService
    {
        public void BasicInfo2Table(BasicInfo basicInfo)
        {
            Dictionary<string, string[]> DicBasicInfo = new Dictionary<string, string[]> {
                { "Number", new string[]{ "病案号"} },
                { "Name", new string[]{ "姓名"} },
                { "Age", new string[]{ "年龄"} },
                { "Male", new string[]{ "性别"} },
                { "Description", new string[]{ "诊断" } },
                { "Killip", new string[]{ "Killip/NYHA" } },
                { "EF", new string[]{ "Killip/NYHA" } },
                { "LV", new string[]{ "LV" } },
                { "BasicOther", new string[]{ "其他" } },
                { "BasicRisk", new string[]{ "危险因素" } },
                { "PCI", new string[]{ "PCI" } },
                { "ResidualStenosis", new string[]{ "75%以上残余狭窄数目" } },
                { "CollatCirc", new string[]{ "侧枝循环" } },
                { "DominantCoronary", new string[]{ "优势冠脉" } }
                
            };
           
        }
            

    }
}
