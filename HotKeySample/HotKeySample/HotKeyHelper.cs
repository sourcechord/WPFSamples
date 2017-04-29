using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;

namespace HotKeySample
{
    public class HotKeyItem
    {
        public ModifierKeys ModifierKeys { get; private set; }
        public Key Key { get; private set; }
        public EventHandler Handler { get; private set; }

        public HotKeyItem(ModifierKeys modKey, Key key, EventHandler handler)
        {
            this.ModifierKeys = modKey;
            this.Key = key;
            this.Handler = handler;
        }
    }

    /// <summary>
    /// HotKey登録を管理するヘルパークラス
    /// </summary>
    public class HotKeyHelper : IDisposable
    {
        private IntPtr _windowHandle;
        private Dictionary<int, HotKeyItem> _hotkeyList = new Dictionary<int, HotKeyItem>();

        private const int WM_HOTKEY = 0x0312;

        [DllImport("user32.dll")]
        private static extern int RegisterHotKey(IntPtr hWnd, int id, int modKey, int vKey);

        [DllImport("user32.dll")]
        private static extern int UnregisterHotKey(IntPtr hWnd, int id);

        public HotKeyHelper(Window window)
        {
            var host = new WindowInteropHelper(window);
            this._windowHandle = host.Handle;

            ComponentDispatcher.ThreadPreprocessMessage += ComponentDispatcher_ThreadPreprocessMessage;
        }

        private void ComponentDispatcher_ThreadPreprocessMessage(ref MSG msg, ref bool handled)
        {
            if (msg.message != WM_HOTKEY) { return; }

            var id = msg.wParam.ToInt32();
            var hotkey = this._hotkeyList[id];

            hotkey?.Handler
                  ?.Invoke(this, EventArgs.Empty);
        }

        private int _hotkeyID = 0x0000;

        private const int MAX_HOTKEY_ID = 0xC000;

        /// <summary>
        /// 引数で指定された内容で、HotKeyを登録します。
        /// </summary>
        /// <param name="modKey"></param>
        /// <param name="key"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        public bool Register(ModifierKeys modKey, Key key, EventHandler handler)
        {
            var modKeyNum = (int)modKey;
            var vKey = KeyInterop.VirtualKeyFromKey(key);

            // HotKey登録
            while (this._hotkeyID < MAX_HOTKEY_ID)
            {
                var ret = RegisterHotKey(this._windowHandle, this._hotkeyID, modKeyNum, vKey);

                if (ret != 0)
                {
                    // HotKeyのリストに追加
                    var hotkey = new HotKeyItem(modKey, key, handler);
                    this._hotkeyList.Add(this._hotkeyID, hotkey);
                    this._hotkeyID++;
                    return true;
                }
                this._hotkeyID++;
            }

            return false;
        }

        /// <summary>
        /// 引数で指定されたidのHotKeyを登録解除します。
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Unregister(int id)
        {
            var ret = UnregisterHotKey(this._windowHandle, id);
            return ret == 0;
        }

        /// <summary>
        /// 引数で指定されたmodKeyとkeyの組み合わせからなるHotKeyを登録解除します。
        /// </summary>
        /// <param name="modKey"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Unregister(ModifierKeys modKey, Key key)
        {
            var item = this._hotkeyList
                           .FirstOrDefault(o => o.Value.ModifierKeys == modKey && o.Value.Key == key);
            var isFound = !item.Equals(default(KeyValuePair<int, HotKeyItem>));

            if (isFound)
            {
                var ret = Unregister(item.Key);
                if (ret)
                {
                    this._hotkeyList.Remove(item.Key);
                }
                return ret;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 登録済みのすべてのHotKeyを解除します。
        /// </summary>
        /// <returns></returns>
        public bool UnregisterAll()
        {
            var result = true;
            foreach(var item in this._hotkeyList)
            {
                result &= this.Unregister(item.Key);
            }

            return result;
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // マネージリソースの破棄
                }

                // アンマネージリソースの破棄
                this.UnregisterAll();

                disposedValue = true;
            }
        }

        ~HotKeyHelper()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

    }
}
