using MVVMShowWindowSample.Services;
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

namespace MVVMShowWindowSample
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // MainWindowViewModelに、コンストラクタ経由でIShowWindowServiceへの依存性を注入する。
            var showWindowService = new ShowWindowService<EditDialog, ToDoItemViewModel>();
            this.DataContext = new MainWindowViewModel(showWindowService);
        }
    }
}
