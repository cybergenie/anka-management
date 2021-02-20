﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anka2.Models
{
    [Table("phq")]
    public class PHQ
    {
        [Key]
        public string PHQNumber { get; set; }
        public string? PHQResult { get; set; }
        public string? basicinfoNumber { set; get; }
        public BasicInfo Basicinfo { get; set; }
    }
    public class PHQList
    {
        public string Number { get; set; }//病案号
        public string? Name { get; set; }//姓名
        public string? Age { get; set; }//年龄
        public bool? Male { get; set; }//性别
        public string? PHQNumber { get; set; }
        public string? PHQResult { get; set; }
    }
}
