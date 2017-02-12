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

namespace HotKeySample
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        private HotKeyHelper _hotkey;
        public MainWindow()
        {
            InitializeComponent();
            // HotKeyの登録
            this._hotkey = new HotKeyHelper(this);
            this._hotkey.Register(ModifierKeys.Control | ModifierKeys.Shift,
                                  Key.X,
                                  (_, __) => { MessageBox.Show("HotKey"); });
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            // HotKeyの登録解除
            this._hotkey.Dispose();
        }
    }
}
