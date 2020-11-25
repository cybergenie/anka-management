using System;
using System.Collections.Generic;
using System.Text;

namespace EFCoreTest.TableAdapter
{
    public class BasicInfoAdapter
    {
        public List<BasicInfo> GetDataList(IList<BasicInfo> data)
        {
            var list = new List<BasicInfo>();
            var dataTable = DbTools.ToDataTable(data);

            list = (List<BasicInfo>)data;
            return list;
        }       
    }
}
