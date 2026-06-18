using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_2
{
    public class YearValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is int year)
            {
                return year <= DateTime.Now.Year; // Год не может быть из будущего
            }
            return false;
        }
    }
}
