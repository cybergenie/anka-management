using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace Anka2.Models
{

    public class RequiredRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null)
                return new ValidationResult(false, "该字段不能为空值！");
            if (string.IsNullOrEmpty(value.ToString()))
                return new ValidationResult(false, "该字段不能为空字符串！");
            return new ValidationResult(true, null);
        }
    }


    public class NumberRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Regex emailReg = new Regex("^(-?[0-9]*[.]*[0-9]*)$");

            if (!String.IsNullOrEmpty(value.ToString()))
            {
                if (!emailReg.IsMatch(value.ToString()))
                {
                    return new ValidationResult(false, "请输入数字");
                }
            }
            return new ValidationResult(true, null);
        }
    }
    public class IntRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Regex emailReg = new Regex("^(\\d*)$");

            if (!String.IsNullOrEmpty(value.ToString()))
            {
                if (!emailReg.IsMatch(value.ToString()))
                {
                    return new ValidationResult(false, "请输入整数");
                }
            }
            return new ValidationResult(true, null);
        }
    }

    public class PersonIdRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Regex emailReg = new Regex("^(\\d{8})$");
            if (value == null)
                return new ValidationResult(false, "该数值不能为空值！");
            if (!String.IsNullOrEmpty(value.ToString()))
            {
                if (!emailReg.IsMatch(value.ToString()))
                {
                    return new ValidationResult(false, "请输入8位数字");
                }
            }
            return new ValidationResult(true, null);
        }
    }

    public class PersonAgeRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Regex emailReg = new Regex("^(\\d{1,3})$");
            if (value == null)
                return new ValidationResult(false, "年龄不能为空值！");
            if (!String.IsNullOrEmpty(value.ToString()))
            {
                if (!emailReg.IsMatch(value.ToString()))
                {
                    return new ValidationResult(false, "输入年龄为整数");
                }
            }
            return new ValidationResult(true, null);
        }
    }

}
