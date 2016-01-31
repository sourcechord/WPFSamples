using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CustomNavigationSample
{
    class NavigationServiceEx
    {
        private ContentControl content;

        public NavigationServiceEx(ContentControl content)
        {
            this.content = content;
        }

        public ContentControl Page { get; set; }




    }


}
