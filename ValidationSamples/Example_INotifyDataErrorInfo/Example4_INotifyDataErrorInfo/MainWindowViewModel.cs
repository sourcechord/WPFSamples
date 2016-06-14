using BaseLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Example4_INotifyDataErrorInfo
{
    class MainWindowViewModel : ValidateableBase
    {
        private string inputString;
        public string InputString
        {
            get { return inputString; }
            set
            {
                this.SetProperty(ref this.inputString, value);
            }
        }


        protected override void ValidateProperty(object value, [CallerMemberName]string propertyName = null)
        {
            switch (propertyName)
            {
                case nameof(this.InputString):
                    if (this.InputString.Count() > 10)
                        this.SetErrors(nameof(this.InputString), new List<string>() { "string is larger than MaxLength" });
                    else
                        this.ClearErrors(nameof(this.InputString));
                    break;
                default:
                    break;
            }
        }
    }
}
