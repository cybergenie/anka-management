using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Anka2.Models
{
    class DbAdapter : DbContext
    {
        public static readonly LoggerFactory MyLoggerFactory = new LoggerFactory(new[] {
            new DebugLoggerProvider()
        });
        public DbSet<BasicInfo> DbPerson { get; set; }
        public DbSet<Exercise> DbExercise { get; set; }
        public DbSet<GAD> DbGAD { get; set; }
        public DbSet<IPAQ> DbIPAQ { get; set; }
        public DbSet<OHQ> DbOHQ { get; set; }
        public DbSet<PHQ> DbPHQ { get; set; }
        public DbSet<Physique> DbPhysique { get; set; }
        public DbSet<SPPB> DbSPPB { get; set; }

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseLoggerFactory(MyLoggerFactory).UseSqlite(
            //    "Data Source=./DataBase/anka.db");
            optionsBuilder.UseSqlite(
                "Data Source=./DataBase/anka.db");
            base.OnConfiguring(optionsBuilder);
        }
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
        public BasicInfo Basicinfo { get; set; }
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
        public BasicInfo Basicinfo { get; set; }
    }

    

    [Table("physique")]
    public class Physique
    {
        [Key]
        public string PhysiqueNumber { get; set; }
        public string basicinfoNumber { set; get; }
        public BasicInfo Basicinfo { get; set; }


    }

    [Table("sppb")]
    public class SPPB
    {
        [Key]
        public string SPPBNumber { get; set; }
        public string basicinfoNumber { set; get; }
        public BasicInfo Basicinfo { get; set; }
    }

}
