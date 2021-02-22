using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;
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
