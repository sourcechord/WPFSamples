using NavigationWithFrame.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NavigationWithFrame
{
    /// <summary>
    /// Shell.xaml の相互作用ロジック
    /// </summary>
    public partial class Shell : Window
    {
        public Shell()
        {
            InitializeComponent();
        }

        public void Navigation(object sender, ExecutedRoutedEventArgs e)
        {
            Page nextView = null;
            switch ((string)e.Parameter)
            {
                case "MainView":
                    nextView = new MainView();
                    break;
                case "SubView":
                    nextView = new SubView();
                    break;
            }

            this.frame.Navigate(nextView);
        }
    }
}
