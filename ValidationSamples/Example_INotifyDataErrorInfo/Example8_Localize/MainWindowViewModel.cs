using BaseLibrary.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example8_Localize
{
    class MainWindowViewModel : ValidateableBase
    {
        private string inputString;
        [Required(ErrorMessageResourceName = "EmptyMessage", ErrorMessageResourceType = typeof(Properties.Resources))]
        [StringLength(10, ErrorMessageResourceName = "LengthOverMessage", ErrorMessageResourceType = typeof(Properties.Resources))]
        [RegularExpression("[a-z]+", ErrorMessageResourceName = "TextTypeInvalidMessage", ErrorMessageResourceType = typeof(Properties.Resources))]
        public string InputString
        {
            get { return inputString; }
            set { this.SetProperty(ref this.inputString, value); }
        }

        public MainWindowViewModel()
        {
            this.InputString = string.Empty;
        }
    }
}
