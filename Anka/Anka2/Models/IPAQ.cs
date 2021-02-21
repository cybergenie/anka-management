using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anka2.Models
{
    [Table("ipaq")]
    public class IPAQ
    {
        [Key]
        public string IPAQNumber { get; set; }
        public bool? IPAQ0 { get; set; }
        public string? IPAQ1 { get; set; }
        public string? IPAQ2 { get; set; }
        public string? IPAQ3 { get; set; }
        public string? IPAQ4 { get; set; }
        public string? IPAQ5 { get; set; }
        public string? basicinfoNumber { set; get; }
        public BasicInfo Basicinfo { get; set; }
    }

    public class IPAQList
    {
        public string Number { get; set; }//病案号
        public string? Name { get; set; }//姓名
        public string? Age { get; set; }//年龄
        public bool? Male { get; set; }//性别
        public string IPAQNumber { get; set; }
        public bool? IPAQ0 { get; set; }
        public string? IPAQ1 { get; set; }
        public string? IPAQ2 { get; set; }
        public string? IPAQ3 { get; set; }
        public string? IPAQ4 { get; set; }
        public string? IPAQ5 { get; set; }

    }
}
