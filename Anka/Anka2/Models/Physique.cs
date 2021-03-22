using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Anka2.Models
{
    [Table("physique")]
    public class Physique
    {
        [Key]
        public string PhysiqueNumber { get; set; }   
        public double Weight { get; set; }
        public double Hight { get; set; }
        public double FM { get; set; }
        public double TBW { get; set; }
        public double BCW { get; set; }
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
        public double? Weight { get; set; }
        public double? Hight { get; set; }
        public double? FM { get; set; }
        public double? TBW { get; set; }
        public double? BCW { get; set; }
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
