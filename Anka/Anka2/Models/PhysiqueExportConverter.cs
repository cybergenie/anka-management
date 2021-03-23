using Anka2.Services;
using System.Data;

namespace Anka2.Models
{
    public class PhysiqueExportConverter
    {
        public static void PhysiqueValueConvertor(ref DataTable dt)
        {
            foreach (DataRow dr in dt.Rows)
            {
                DataUitls.GenderConvertor(dr, "性别");
                DataUitls.DateConvertor(dr, "记录编号");
               
            }          

        }        
    }
}
