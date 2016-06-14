using BaseLibrary.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example5_DataAnnotations
{
    class MainWindowViewModel : ValidateableBase
    {
        private string inputString;
        [StringLength(10, ErrorMessage = "InputStringは10文字以内で入力してください")]
        public string InputString
        {
            get { return inputString; }
            set
            {
                this.SetProperty(ref this.inputString, value);
            }
        }
    }
}
