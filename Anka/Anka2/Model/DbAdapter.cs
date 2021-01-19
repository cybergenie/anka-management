using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anka2.Model
{
    class DbAdapter: DbContext
    {
        public static readonly LoggerFactory MyLoggerFactory = new LoggerFactory(new[] {
            new DebugLoggerProvider()
        });
        public DbSet<BasicInfo> DbPerson { get; set; }
        //public DbSet<Exercise> DbExercise { get; set; }
        //public DbSet<GAD> DbGAD { get; set; }
        //public DbSet<IPAQ> DbIPAQ { get; set; }
        //public DbSet<OHQ> DbOHQ { get; set; }
        //public DbSet<PHQ> DbPHQ { get; set; }
        //public DbSet<Physique> DbPhysique { get; set; }
        //public DbSet<SPPB> DbSPPB { get; set; }

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(MyLoggerFactory).UseSqlite(
                "Data Source=anka.db");
            base.OnConfiguring(optionsBuilder);
        }
    }
    [Table("basicinfo")]
    public class BasicInfo
    {
        [Key]
        public string Number { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }
        public bool? Male { get; set; }
        public string Killip { get; set; }
        public string EF { get; set; }
        public string LV { get; set; }
        public string BasicOther { get; set; }
        public string BasicRisk { get; set; }
        public string RiskOther { get; set; }
        public int? PCI { get; set; }
        public int? ResidualStenosis { get; set; }
        public bool? CollatCirc { get; set; }
        public int? DominantCoronary { get; set; }
        public string Description { get; set; }
        public string Hb { get; set; }
        public string Alb { get; set; }
        public string Cre { get; set; }
        public string BUN { get; set; }
        public string Glu { get; set; }
        public string HbAlc { get; set; }
        public string BNP { get; set; }
        public string D2 { get; set; }
        public string Tchol { get; set; }
        public string TG { get; set; }
        public string HDLC { get; set; }
        public string LDLC { get; set; }
        public string UA { get; set; }
        public string ABI { get; set; }
        public string cTnT { get; set; }
        public string LY { get; set; }
        public List<Exercise> PExercise { get; set; }
        public List<GAD> PGAD { get; set; }
    }

    [Table("exercise")]
    public class Exercise
    {
        [Key]
        public string ExerciseNumber { get; set; }
        public bool? InRoomUp { get; set; }
        public string Date { get; set; }
        public string BloodPressureLower { get; set; }
        public string BloodPressureUpper { get; set; }
        public string HeartRate { get; set; }
        public string BloodOxygen { get; set; }
        public string BorgIndex { get; set; }
        public string Remarks { get; set; }
        public string ECGs { get; set; }
        public string Checks { get; set; }
        public string basicinfoNumber { set; get; }
        public BasicInfo basicinfo { get; set; }

    }

    [Table("gad")]
    public class GAD
    {
        [Key]
        public string GADNumber { get; set; }
        public string GADResult { get; set; }
        public string basicinfoNumber { set; get; }
        public BasicInfo basicinfo { get; set; }
    }

    [Table("ipaq")]
    public class IPAQ
    {
        [Key]
        public string IPAQNumber { get; set; }
        public bool? IPAQ0 { get; set; }
        public string IPAQ1 { get; set; }
        public string IPAQ2 { get; set; }
        public string IPAQ3 { get; set; }
        public string IPAQ4 { get; set; }
        public string IPAQ5 { get; set; }
        public string basicinfoNumber { set; get; }
        public BasicInfo basicinfo { get; set; }
    }

    [Table("ohq")]
    public class OHQ
    {
        [Key]
        public string OHQNumber { get; set; }
        public string OHQ1 { get; set; }
        public string OHQ2 { get; set; }
        public string OHQ3 { get; set; }
        public string OHQ4 { get; set; }
        public string OHQ5 { get; set; }
        public string OHQ6 { get; set; }
        public string OHQ7 { get; set; }
        public string OHQ8 { get; set; }
        public string OHQ9 { get; set; }
        public string basicinfoNumber { set; get; }
        public BasicInfo basicinfo { get; set; }
    }

    [Table("phq")]
    public class PHQ
    {
        [Key]
        public string PHQNumber { get; set; }
        public string PHQResult { get; set; }
        public string basicinfoNumber { set; get; }
        public BasicInfo basicinfo { get; set; }
    }

    [Table("physique")]
    public class Physique
    {
        [Key]
        public string PhysiqueNumber { get; set; }
        public string basicinfoNumber { set; get; }
        public BasicInfo basicinfo { get; set; }


    }

    [Table("sppb")]
    public class SPPB
    {
        [Key]
        public string SPPBNumber { get; set; }
        public string basicinfoNumber { set; get; }
        public BasicInfo basicinfo { get; set; }
    }

}
