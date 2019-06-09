using MVVMShowWindowSample.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMShowWindowSample
{
    class ToDoItemViewModel : BindableBase, ICloneable
    {
        private string title;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        private string detail;
        public string Detail
        {
            get { return detail; }
            set { SetProperty(ref detail, value); }
        }

        public ToDoItemViewModel Clone()
        {
            return (ToDoItemViewModel)this.MemberwiseClone();
        }

        object ICloneable.Clone()
        {
            return this.Clone();
        }
    }
}
