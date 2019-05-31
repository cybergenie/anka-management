using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Data.SQLite;
using System.Data;
using System.Windows;

namespace Anka
{
    class DataAdapter
    {
        
        /// <summary>
        /// 基本信息
        /// </summary>
        public static string Name { set; get; }
        public static string Number { set; get; }
        public static int Age { set; get; }
        public static bool Male { set; get; } //true:男   false:女;

        public static bool loadNewPerson { set; get; } = false;       


        public static bool IsNumber(string str)
        {
            bool result = false;
            str = str.Trim();
            if (str != null)
            {

                string pattern = @"^(-?\d+)(\.\d+)?$";
                Match match = Regex.Match(str, pattern);
                if (match.Success)
                {
                    result = true;                   
                }                        
            }           
            
            return result;
        } 

        public static void GetBloodPressure(string[] strBloodPressure, int[] BPLower,int[] BPUpper)
        {
            for (int i = 0; i < strBloodPressure.Length; i++)
            {
                int[] BMPtemp = { 0, 0 };
                int j = 0;
                char[] Spliter = { '/', '\\', ' ' };
                foreach (string ctemp in strBloodPressure[i].Split(Spliter))
                {
                    if (DataAdapter.IsNumber(ctemp) == true)
                    {
                        if (Convert.ToInt32(ctemp) > 0)
                            BMPtemp[j] = Convert.ToInt32(ctemp);
                    }

                    j++;
                }

                BPLower[i] = BMPtemp[0];
                BPUpper[i] = BMPtemp[1];
            }
        }

        public static string ArrayToString(int[] data)
        {
            string temp = "";

            foreach (var s in data)
            {
                temp += (s.ToString() + "|");
            }

            return temp;
        }

        public static string ArrayToString(string[] data)
        {
            string temp = "";

            foreach (var s in data)
            {
                temp += (s.ToString() + "|");
            }

            return temp;
        }

        public static string ArrayToString(bool[] data)
        {
            string temp = "";

            foreach (var s in data)
            {
                int p = -1;
                if (s == true)
                { p = 1; }
                else if(s == false)
                { p = 0; }
                temp += (p.ToString() + "|");
            }

            return temp;
        }


    }

}
