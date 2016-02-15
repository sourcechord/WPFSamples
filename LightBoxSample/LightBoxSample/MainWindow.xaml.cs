using LightBoxSample.Controls;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LightBoxSample
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            // 時間差で複数のダイアログ表示を続けて行う。
            LightBox.Show(this, new SimpleDialog());

            await Task.Delay(1000);
            LightBox.Show(this, new SimpleDialog());

            await Task.Delay(1000);
            LightBox.Show(this, new SimpleDialog());
        }
    }
}
