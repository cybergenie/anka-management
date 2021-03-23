using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Anka2.Models
{
    [Table("physique")]
    public class Physique
    {
        [Key]
        public string PhysiqueNumber { get; set; }   
        public string Weight { get; set; }
        public string Hight { get; set; }
        public string FM { get; set; }
        public string TBW { get; set; }
        public string BCW { get; set; }
        public string SMMAll { get; set; }
        public string SMMArmLeft { get; set; }
        public string SMMArmRight { get; set; }
        public string SMMBody { get; set; }
        public string SMMLegLeft { get; set; }
        public string SMMLegRight { get; set; }
        public string VAT { get; set; }
        public string Waistline { get; set; }
        public string PA { get; set; }
        public string PAPercent { get; set; }        
        public string? basicinfoNumber { set; get; }
        public BasicInfo Basicinfo { get; set; }
    }
    public class PhysiqueList
    {
        public string Number { get; set; }//病案号
        public string? Name { get; set; }//姓名
        public string? Age { get; set; }//年龄
        public bool? Male { get; set; }//性别
        public string? PhysiqueNumber { get; set; }
        public string? Weight { get; set; }
        public string? Hight { get; set; }
        public string? FM { get; set; }
        public string? TBW { get; set; }
        public string? BCW { get; set; }
        public string? SMMAll { get; set; }
        public string? SMMArmLeft { get; set; }
        public string? SMMArmRight { get; set; }
        public string? SMMBody { get; set; }
        public string? SMMLegLeft { get; set; }
        public string? SMMLegRight { get; set; }
        public string? VAT { get; set; }
        public string? Waistline { get; set; }
        public string? PA { get; set; }
        public string? PAPercent { get; set; }

    }
}
