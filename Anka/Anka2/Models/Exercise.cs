using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Anka2.Models
{
    [Table("exercise")]
    public class Exercise
    {
        [Key]
        public string ExerciseNumber { get; set; }
        public bool? InRoomUp { get; set; }
        public string? Date { get; set; }
        public string? BloodPressureLower { get; set; }
        public string? BloodPressureUpper { get; set; }
        public string? HeartRate { get; set; }
        public string? BloodOxygen { get; set; }
        public string? BorgIndex { get; set; }
        public string? Remarks { get; set; }
        public string? ECGs { get; set; }
        public string Checks { get; set; }
        public string? basicinfoNumber { set; get; }
        public BasicInfo Basicinfo { get; set; } 

       

    }

    public class ExerciseList
    {
        public string Number { get; set; }//病案号
        public string? Name { get; set; }//姓名
        public string? Age { get; set; }//年龄
        public bool? Male { get; set; }//性别
        public string ExerciseNumber { get; set; }
        public string? BedUp { get; set; }        
        public string? InRoom { get; set; }        
        public string? OutRoom { get; set; }       
        public string? OutSide { get; set; }
    }
}
