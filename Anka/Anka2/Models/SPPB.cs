using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Anka2.Models
{
    [Table("sppb")]
    public class SPPB
    {
        [Key]
        public string SPPBNumber { get; set; }
        public string? BalanceTesting1 { set; get; }
        public string? BalanceTesting2 { set; get; }
        public string? BalanceTesting3 { set; get; }
        public string? walkingTesting1 { set; get; }
        public string? walkingTesting2 { set; get; }
        public string? SitUpTesting { set; get; }
        public string? TUG { set; get; }
        public string? FRTLeft1 { set; get; }
        public string? FRTLeft2 { set; get; }
        public string? FRTRight1 { set; get; }
        public string? FRTRight2 { set; get; }
        public string? SFO1 { set; get; }
        public string? SFO2 { set; get; }
        public string? OneFootLeft1 { set; get; }
        public string? OneFootLeft2 { set; get; }
        public string? OneFootRight1 { set; get; }
        public string? OneFootRight2 { set; get; }
        public string? Hight { set; get; }
        public string? Weight { set; get; }
        public string? Waistline { set; get; }
        public string? Hipline { set; get; }
        public string? ArmlineLeft { set; get; }
        public string? ArmlineRight { set; get; }
        public string? LeglineLeft { set; get; }
        public string? LeglineRight { set; get; }
        public string? BloodPressureUpper { set; get; }
        public string? BloodPressureLower { set; get; }
        public string? HeartRate { set; get; }
        public string? Temperature { set; get; }
        public string? Breathe { set; get; }
        public bool? LeftHandHurt { set; get; }
        public bool? RightHandHurt { set; get; }
        public string? GripStrengthLeft1 { set; get; }
        public string? GripStrengthRight1 { set; get; }
        public string? GripStrengthLeft2 { set; get; }
        public string? GripStrengthRight2 { set; get; }
        public bool? LeftLapHurt { set; get; }
        public bool? RightLapHurt { set; get; }
        public string? LapStrengthLeft1 { set; get; }
        public string? LapStrengthRight1 { set; get; }
        public string? LapStrengthLeft2 { set; get; }
        public string? LapStrengthRight2 { set; get; }
        public string? basicinfoNumber { set; get; }
        public BasicInfo Basicinfo { get; set; }
    }
    public class SPPBList
    {
        public string Number { get; set; }//病案号
        public string? Name { get; set; }//姓名
        public string? Age { get; set; }//年龄
        public bool? Male { get; set; }//性别
        public string? SPPBNumber { get; set; }
        public string? BalanceTesting1 { set; get; }
        public string? BalanceTesting2 { set; get; }
        public string? BalanceTesting3 { set; get; }
        public string? walkingTesting1 { set; get; }
        public string? walkingTesting2 { set; get; }
        public string? SitUpTesting { set; get; }
        public string? TUG { set; get; }
        public string? FRTLeft1 { set; get; }
        public string? FRTLeft2 { set; get; }
        public string? FRTRight1 { set; get; }
        public string? FRTRight2 { set; get; }
        public string? SFO1 { set; get; }
        public string? SFO2 { set; get; }
        public string? OneFootLeft1 { set; get; }
        public string? OneFootLeft2 { set; get; }
        public string? OneFootRight1 { set; get; }
        public string? OneFootRight2 { set; get; }
        public string? Hight { set; get; }
        public string? Weight { set; get; }
        public string? Waistline { set; get; }
        public string? Hipline { set; get; }
        public string? ArmlineLeft { set; get; }
        public string? ArmlineRight { set; get; }
        public string? LeglineLeft { set; get; }
        public string? LeglineRight { set; get; }
        public string? BloodPressureUpper { set; get; }
        public string? BloodPressureLower { set; get; }
        public string? HeartRate { set; get; }
        public string? Temperature { set; get; }
        public string? Breathe { set; get; }
        public bool? LeftHandHurt { set; get; }
        public bool? RightHandHurt { set; get; }
        public string? GripStrengthLeft1 { set; get; }
        public string? GripStrengthRight1 { set; get; }
        public string? GripStrengthLeft2 { set; get; }
        public string? GripStrengthRight2 { set; get; }
        public bool? LeftLapHurt { set; get; }
        public bool? RightLapHurt { set; get; }
        public string? LapStrengthLeft1 { set; get; }
        public string? LapStrengthRight1 { set; get; }
        public string? LapStrengthLeft2 { set; get; }
        public string? LapStrengthRight2 { set; get; }
    }
}
