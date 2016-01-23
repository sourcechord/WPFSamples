using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMServiceSample.Services
{
    public class DummyDialogService : IDialogService
    {
        public void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
