using BaseLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example1_WithException
{
    class MainWindowViewModel : BindableBase
    {
        private string inputString;
        public string InputString
        {
            get { return inputString; }
            set
            {
                if (value.Count() > 10)
                    throw new ArgumentException();
                this.SetProperty(ref this.inputString, value);
            }
        }
    }
}
