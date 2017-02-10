using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotifyIconSample
{
    public enum ToolTipIconEx
    {
        None = 0,
        Info = 1,
        Warning = 2,
        Error = 3
    }

    public class NotifyIconEx : IDisposable
    {
        private System.Windows.Forms.NotifyIcon _notify;

        public string Text
        {
            get { return this._notify.Text; }
            set { this._notify.Text = value; }
        }

        public bool Visible
        {
            get { return this._notify.Visible; }
            set { this._notify.Visible = value; }
        }

        public Uri IconPath
        {
            set
            {
                if (value == null) { return; }
                var iconStream = System.Windows.Application.GetResourceStream(value).Stream;
                this._notify.Icon = new System.Drawing.Icon(iconStream);
            }
        }

        public System.Windows.Controls.ContextMenu ContextMenu { get; set; }

        public ToolTipIconEx BalloonTipIcon
        {
            get { return (ToolTipIconEx)this._notify.BalloonTipIcon; }
            set { this._notify.BalloonTipIcon = (System.Windows.Forms.ToolTipIcon)value; }
        }

        public string BalloonTipTitle
        {
            get { return this._notify.BalloonTipTitle; }
            set { this._notify.BalloonTipTitle = value; }
        }

        public string BalloonTipText
        {
            get { return this._notify.BalloonTipText; }
            set { this._notify.BalloonTipText = value; }
        }

        public void ShowBalloonTip(int timeout)
        {
            this._notify.ShowBalloonTip(timeout);
        }

        public void ShowBalloonTip(int timeout, string tipTitle, string tipText, ToolTipIconEx tipIcon)
        {
            var icon = (System.Windows.Forms.ToolTipIcon)tipIcon;
            this._notify.ShowBalloonTip(timeout, tipTitle, tipText, icon);
        }

        public NotifyIconEx()
            : this(null) { }

        public NotifyIconEx(Uri iconPath)
            : this(iconPath, null) { }

        public NotifyIconEx(Uri iconPath, string text)
            : this(iconPath, text, null) {}

        public NotifyIconEx(Uri iconPath, string text, System.Windows.Controls.ContextMenu menu)
        {
            // 各種プロパティを初期化
            this._notify = new System.Windows.Forms.NotifyIcon();
            this.IconPath = iconPath;
            this.Text = text;
            this.ContextMenu = menu;

            // マウス右ボタンUpのタイミングで、ContextMenuの表示を行う
            // ダミーの透明ウィンドウを表示し、このウィンドウのアクティブ状態を用いてContextMenuの表示/非表示を切り替える
            this._notify.MouseUp += (s, e) =>
            {
                if (e.Button != System.Windows.Forms.MouseButtons.Right) { return; }

                var win = new System.Windows.Window()
                {
                    WindowStyle = System.Windows.WindowStyle.None,
                    ShowInTaskbar = false,
                    AllowsTransparency = true,
                    Background = System.Windows.Media.Brushes.Transparent,
                    Content = new System.Windows.Controls.Grid(),
                    ContextMenu = this.ContextMenu
                };

                var isClosed = false;
                win.Activated += (_, __) =>
                {
                    if (win.ContextMenu != null)
                    {
                        win.ContextMenu.IsOpen = true;
                    }
                };
                win.Closing += (_, __) =>
                {
                    isClosed = true;
                };

                win.Deactivated += (_, __) =>
                {
                    if (win.ContextMenu != null)
                    {
                        win.ContextMenu.IsOpen = false;
                    }
                    if (!isClosed)
                    {
                        win.Close();
                    }
                };
                
                // ダミーウィンドウ表示&アクティブ化をする。
                // ⇒これがActivatedイベントで、ContextMenuが表示される
                win.Show();
                win.Activate();
            };

            this._notify.Visible = true;
        }

        #region NotifyIconクラスの各種イベントをラップする
        public event EventHandler BalloonTipClicked
        {
            add { this._notify.BalloonTipClicked += value; }
            remove { this._notify.BalloonTipClicked -= value; }
        }

        public event EventHandler BalloonTipClosed
        {
            add { this._notify.BalloonTipClosed += value; }
            remove { this._notify.BalloonTipClosed -= value; }
        }

        public event EventHandler BalloonTipShown
        {
            add { this._notify.BalloonTipShown += value; }
            remove { this._notify.BalloonTipShown -= value; }
        }

        public event EventHandler Click
        {
            add { this._notify.Click += value; }
            remove { this._notify.Click -= value; }
        }

        public event EventHandler Disposed
        {
            add { this._notify.Disposed += value; }
            remove { this._notify.Disposed -= value; }
        }

        public event EventHandler DoubleClick
        {
            add { this._notify.DoubleClick += value; }
            remove { this._notify.DoubleClick -= value; }
        }
        #endregion

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this._notify.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion

    }
}
