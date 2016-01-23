using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MVVMServiceSample.Services
{
    class DialogService : IDialogService
    {
        private Window _owner;

        public DialogService(Window owner = null)
        {
            this._owner = owner;
        }

        public void ShowMessage(string message)
        {
            if (this._owner != null) { MessageBox.Show(this._owner, message); }
            else { MessageBox.Show(message); }
        }
    }
}
