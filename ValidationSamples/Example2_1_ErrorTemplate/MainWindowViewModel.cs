using BaseLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example2_1_ErrorTemplate
{
    class MainWindowViewModel : BindableBase
    {
        private string inputString;
        public string InputString
        {
            get { return inputString; }
            set { this.SetProperty(ref this.inputString, value); }
        }
    }
}
