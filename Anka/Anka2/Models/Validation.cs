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
            if (string.IsNullOrEmpty(value.ToString()))
                return new ValidationResult(false, "该项不能为空值！");
            return new ValidationResult(true, null);
        }
    }


    public class NumberRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Regex numberReg = new Regex("^(-?[0-9]*[.]*[0-9]*)$");

            if (!String.IsNullOrEmpty(value.ToString()))
            {
                if (!numberReg.IsMatch(value.ToString()))
                {
                    return new ValidationResult(false, "请输入数字");
                }
            }
            return new ValidationResult(true, null);
        }
    }

    public class DateRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Regex DateNumberReg = new Regex("^([1-2][0-9][0-9][0-9][/][0-9][0-9]?[/][0-9][0-9]?)$");

            if (!String.IsNullOrEmpty(value.ToString()))
            {
                if (!DateNumberReg.IsMatch(value.ToString()))
                {
                    return new ValidationResult(false, @"请输入正确的格式,例如：2020/10/10-01，2020\10\10-01，2020-10-10-01,2020.10.10-01");
                }
            }
            return new ValidationResult(true, null);
        }
    }
    public class DateNumberRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Regex DateNumberReg = new Regex("^([1-2][0-9][0-9][0-9][,\\\\,/,\\-,., ]?[0-9][0-9]?[,\\\\,/,\\-,., ][0-9][0-9]?-[0-9][0-9])$");

            if (!String.IsNullOrEmpty(value.ToString()))
            {
                if (!DateNumberReg.IsMatch(value.ToString()))
                {
                    return new ValidationResult(false, @"请输入正确的格式,例如：2020/10/10-01，2020\10\10-01，2020-10-10-01,2020.10.10-01");
                }
            }
            return new ValidationResult(true, null);
        }
    }
    public class IntRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Regex intReg = new Regex("^(\\d*)$");

            if (!String.IsNullOrEmpty(value.ToString()))
            {
                if (!intReg.IsMatch(value.ToString()))
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
            Regex personIdReg = new Regex("^(\\d{8})$");
            if (!String.IsNullOrEmpty(value.ToString()))
            {
                if (!personIdReg.IsMatch(value.ToString()))
                {
                    return new ValidationResult(false, "请输入8位数字");
                }
            }
            else
                return new ValidationResult(false, "该数值不能为空值！");
            return new ValidationResult(true, null);
        }
    }

    public class PersonAgeRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Regex personAgeReg = new Regex("^(\\d{1,3})$");
            if (!String.IsNullOrEmpty(value.ToString()))
            {
                if (!personAgeReg.IsMatch(value.ToString()))
                {
                    return new ValidationResult(false, "输入年龄为整数");
                }
            }
            else
                return new ValidationResult(false, "年龄不能为空值！");
            return new ValidationResult(true, null);
        }
    }

    public class BloodPressureRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Regex bloodPressureReg = new Regex("^(\\d{0,3}/\\d{0,3})$");
            if (!String.IsNullOrEmpty(value.ToString()))
            {
                if (!bloodPressureReg.IsMatch(value.ToString()))
                {
                    return new ValidationResult(false, "请输入正确的血压值，例如：120/85");
                }
            }
            return new ValidationResult(true, null);

        }
    }

}
