using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BaseLibrary.Common
{
    public abstract class ValidateableBase : BindableBase, INotifyDataErrorInfo
    {
        /// <summary>
        /// プロパティが既に目的の値と一致しているかどうかを確認します。必要な場合のみ、
        /// プロパティを設定し、リスナーに通知します。
        /// その後、プロパティの入力値検証を行います。
        /// </summary>
        /// <typeparam name="T">プロパティの型。</typeparam>
        /// <param name="storage">get アクセス操作子と set アクセス操作子両方を使用したプロパティへの参照。</param>
        /// <param name="value">プロパティに必要な値。</param>
        /// <param name="propertyName">リスナーに通知するために使用するプロパティの名前。
        /// この値は省略可能で、
        /// CallerMemberName をサポートするコンパイラから呼び出す場合に自動的に指定できます。</param>
        /// <returns>値が変更された場合は true、既存の値が目的の値に一致した場合は
        /// false です。</returns>
        protected override bool SetProperty<T>(ref T storage, T value, [CallerMemberName] String propertyName = null)
        {
            var isChanged = base.SetProperty(ref storage, value, propertyName);
            if (isChanged)
                this.ValidateProperty(value, propertyName);

            return isChanged;
        }

        protected abstract void ValidateProperty(object value, [CallerMemberName]string propertyName = null);

        #region 発生中のエラーを保持する処理を実装
        readonly Dictionary<string, List<string>> _currentErrors = new Dictionary<string, List<string>>();

        /// <summary>
        /// 引数で指定されたプロパティに、errorsで指定されたエラーをすべて登録します。
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="errors"></param>
        protected void SetErrors(string propertyName, IEnumerable<string> errors)
        {
            var hasCurrentError = _currentErrors.ContainsKey(propertyName);
            var hasNewError = errors != null && errors.Count() > 0;

            if (!hasCurrentError && !hasNewError)
                return;

            if (hasNewError)
            {
                _currentErrors[propertyName] = new List<string>(errors);
            }
            else
            {
                _currentErrors.Remove(propertyName);
            }
            OnErrorsChanged(propertyName);
        }

        /// <summary>
        /// 引数で指定されたプロパティのエラーをすべて解除します。
        /// </summary>
        /// <param name="propertyName"></param>
        protected void ClearErrors(string propertyName)
        {
            if (_currentErrors.ContainsKey(propertyName))
            {
                _currentErrors.Remove(propertyName);
                OnErrorsChanged(propertyName);
            }
        }
        #endregion

        private void OnErrorsChanged(string propertyName)
        {
            var h = this.ErrorsChanged;
            if (h != null)
            {
                h(this, new DataErrorsChangedEventArgs(propertyName));
            }
        }

        #region INotifyDataErrorInfoの実装
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public System.Collections.IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName) ||
                !_currentErrors.ContainsKey(propertyName))
                return null;

            return _currentErrors[propertyName];
        }

        public bool HasErrors
        {
            get { return _currentErrors.Count > 0; }
        }
        #endregion
    }
}
