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
using System.ComponentModel;

namespace NotifyIconSample
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        private NotifyIconEx _notify;

        public MainWindow()
        {
            InitializeComponent();

            var iconPath = new Uri("pack://application:,,,/NotifyIconSample;component/Icon1.ico", UriKind.Absolute);
            var menu = (ContextMenu)this.FindResource("sampleWinMenu");
            //var menu = (ContextMenu)this.FindResource("sampleAppMenu");
            this._notify = new NotifyIconEx(iconPath, "Notify Title", menu);

            this._notify.DoubleClick += (_, __) => { this.ShowWindow(); };
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            // クローズ処理をキャンセルして、タスクバーの表示も消す
            e.Cancel = true;
            this.WindowState = System.Windows.WindowState.Minimized;
            this.ShowInTaskbar = false;
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            // ウィンドウを閉じる際に、タスクトレイのアイコンを削除する。
            this._notify.Dispose();
        }

        private void ShowWindow()
        {
            // ウィンドウ表示&最前面に持ってくる
            if (this.WindowState == System.Windows.WindowState.Minimized)
                this.WindowState = System.Windows.WindowState.Normal;

            this.Show();
            this.Activate();
            this.ShowInTaskbar = true;
        }

        private void btnSetTitle_Click(object sender, RoutedEventArgs e)
        {
            this._notify.Text = this.txtTitle.Text;
        }

        private void btnShowBalloon_Click(object sender, RoutedEventArgs e)
        {
            var icon = (ToolTipIconEx)this.cmbIcon.SelectedValue;
            var title = this.txtBalloonTitle.Text;
            var text = this.txtBalloonText.Text;

            this._notify.ShowBalloonTip(100, title, text, icon);
        }


        #region タスクトレイのContextMenu用のイベント定義
        private void MenuItem_Show_Click(object sender, RoutedEventArgs e)
        {
            this.ShowWindow();
        }

        private void MenuItem_Exit_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void MenuItem_Show_Balloon_Click(object sender, RoutedEventArgs e)
        {
            this._notify.ShowBalloonTip(1000, "tipTitle", "tipText", ToolTipIconEx.Info);
        }
        #endregion

    }
}
