using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Anka2.Models
{
    [Table("gad")]
    public class GAD
    {
        [Key]
        public string GADNumber { get; set; }
        public string? GADResult { get; set; }
        public string? basicinfoNumber { set; get; }
        public BasicInfo Basicinfo { get; set; }
    }

    public class GADList
    {
        public string Number { get; set; }//病案号
        public string? Name { get; set; }//姓名
        public string? Age { get; set; }//年龄
        public bool? Male { get; set; }//性别
        public string GADNumber { get; set; }       
        public string? GADResult { get; set; }
    }
}
