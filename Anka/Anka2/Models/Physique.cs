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
        public double SMMAll { get; set; }
        public double SMMArmLeft { get; set; }
        public double SMMArmRight { get; set; }
        public double SMMBody { get; set; }
        public double SMMLegLeft { get; set; }
        public double SMMLegRight { get; set; }
        public double VAT { get; set; }
        public double Waistline { get; set; }
        public double PA { get; set; }
        public double PAPercent { get; set; }        
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
        public double? SMMAll { get; set; }
        public double? SMMArmLeft { get; set; }
        public double? SMMArmRight { get; set; }
        public double? SMMBody { get; set; }
        public double? SMMLegLeft { get; set; }
        public double? SMMLegRight { get; set; }
        public double? VAT { get; set; }
        public double? Waistline { get; set; }
        public double? PA { get; set; }
        public double? PAPercent { get; set; }

    }
}
