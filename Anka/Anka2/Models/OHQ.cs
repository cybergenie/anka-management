using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Anka2.Models
{
    [Table("ohq")]
    public class OHQ
    {
        [Key]
        public string OHQNumber { get; set; }
        public string? OHQ1 { get; set; }
        public string? OHQ2 { get; set; }
        public string? OHQ3 { get; set; }
        public string? OHQ4 { get; set; }
        public string? OHQ5 { get; set; }
        public string? OHQ6 { get; set; }
        public string? OHQ7 { get; set; }
        public string? OHQ8 { get; set; }
        public string? OHQ9 { get; set; }
        public string? basicinfoNumber { set; get; }
        public BasicInfo Basicinfo { get; set; }
    }

    public class OHQList
    {
        public string Number { get; set; }//病案号
        public string? Name { get; set; }//姓名
        public string? Age { get; set; }//年龄
        public bool? Male { get; set; }//性别
        public string OHQNumber { get; set; }
        public string? OHQ1 { get; set; }
        public string? OHQ2 { get; set; }
        public string? OHQ3 { get; set; }
        public string? OHQ4 { get; set; }
        public string? OHQ5 { get; set; }
        public string? OHQ6 { get; set; }
        public string? OHQ7 { get; set; }
        public string? OHQ8 { get; set; }
        public string? OHQ9 { get; set; }
    }
}
