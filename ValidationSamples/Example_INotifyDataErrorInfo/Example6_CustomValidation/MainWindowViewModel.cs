using BaseLibrary.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example6_CustomValidation
{
    public class MainWindowViewModel : ValidateableBase
    {
        private string inputString1;
        [EmptyValidateAttribute(ErrorMessage = "空白は無効です")]
        public string InputString1
        {
            get { return inputString1; }
            set { this.SetProperty(ref this.inputString1, value); }
        }

        private string inputString2;
        [CustomValidation(typeof(MainWindowViewModel), "CustomMethod", ErrorMessage = "空白は無効です")]
        public string InputString2
        {
            get { return inputString2; }
            set { this.SetProperty(ref this.inputString2, value); }
        }


        public static ValidationResult CustomMethod(string value, ValidationContext context)
        {
            if (value != null && string.IsNullOrWhiteSpace(value))
            {
                return new ValidationResult(null);
            }
            return ValidationResult.Success;
        }

        public MainWindowViewModel()
        {
            this.InputString1 = string.Empty;
            this.InputString2 = string.Empty;
        }
    }

    class EmptyValidateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var v = value as string;
            if (v != null && string.IsNullOrWhiteSpace(v))
            {
                return new ValidationResult(this.ErrorMessage);
            }
            return ValidationResult.Success;
        }
    }
}
