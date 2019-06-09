using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMShowWindowSample.Services
{
    interface IShowWindowService<TViewModel>
    {
        bool? ShowDialog(TViewModel context);
    }
}
