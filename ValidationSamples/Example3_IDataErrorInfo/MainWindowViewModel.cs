using BaseLibrary.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example3_IDataErrorInfo
{
    class MainWindowViewModel : BindableBase, IDataErrorInfo
    {
        private string inputString;
        public string InputString
        {
            get { return inputString; }
            set { this.SetProperty(ref this.inputString, value); }
        }

        public string Error
        {
            get { return string.Empty; }
        }

        public string this[string columnName]
        {
            get { return IsValid(columnName); }
        }

        private string IsValid(string propertyName)
        {
            switch (propertyName)
            {
                case "InputString":
                    if (this.InputString.Count() > 10)
                        return "string is larger than MaxLength";
                    break;
                default:
                    break;
            }
            return string.Empty;
        }
    }
}
