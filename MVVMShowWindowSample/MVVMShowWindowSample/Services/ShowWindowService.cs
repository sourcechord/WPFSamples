using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MVVMShowWindowSample.Services
{
    class ShowWindowService<TWindow, TViewModel> : IShowWindowService<TViewModel>
        where TWindow : Window, new()
    {
        public Window Owner { get; set; }

        public bool? ShowDialog(TViewModel context)
        {
            var dlg = new TWindow()
            {
                Owner = this.Owner,
                DataContext = context,
            };

            return dlg.ShowDialog();
        }

        public void Show(TViewModel context)
        {
            var dlg = new TWindow()
            {
                Owner = this.Owner,
                DataContext = context,
            };

            dlg.Show();
        }
    }
}
