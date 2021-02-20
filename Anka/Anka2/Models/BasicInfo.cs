using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anka2.Models
{
    [Table("basicinfo")]
    public class BasicInfo
    {
        [Key]
        public string Number { get; set; }//病案号
        public string? Name { get; set; }//姓名
        public string? Age { get; set; }//年龄
        public bool? Male { get; set; }//性别
        public string? Killip { get; set; }//Killip/NYHA：
        public string? EF { get; set; }//EF：
        public string? LV { get; set; }//LV：
        public string? BasicOther { get; set; }//其  他
        public string? BasicRisk { get; set; }
        public string? RiskOther { get; set; }
        public int? PCI { get; set; }
        public int? ResidualStenosis { get; set; }
        public bool? CollatCirc { get; set; }
        public int? DominantCoronary { get; set; }
        public string? Description { get; set; }
        public string? Hb { get; set; }
        public string? Alb { get; set; }
        public string? Cre { get; set; }
        public string? BUN { get; set; }
        public string? Glu { get; set; }
        public string? HbAlc { get; set; }
        public string? BNP { get; set; }
        public string? D2 { get; set; }
        public string? Tchol { get; set; }
        public string? TG { get; set; }
        public string? HDLC { get; set; }
        public string? LDLC { get; set; }
        public string? UA { get; set; }
        public string? ABI { get; set; }
        public string? cTnT { get; set; }
        public string? LY { get; set; }
        public virtual List<Exercise> PExercise { get; set; }
        public virtual List<GAD> PGAD { get; set; }
        public virtual List<PHQ> PPHQ { get; set; }
    }
}
