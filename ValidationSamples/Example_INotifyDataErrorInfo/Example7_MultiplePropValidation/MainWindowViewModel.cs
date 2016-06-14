using BaseLibrary.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example7_MultiplePropValidation
{
    public class MainWindowViewModel : ValidateableBase
    {
        private string inputString;
        [CustomValidation(typeof(MainWindowViewModel), "CustomMethod")]
        public string InputString
        {
            get { return inputString; }
            set { this.SetProperty(ref this.inputString, value); }
        }

        /// <summary>
        /// InputStringの入力値検証に使用するカスタム検証メソッド
        /// </summary>
        public static ValidationResult CustomMethod(string value, ValidationContext context)
        {
            var obj = context.ObjectInstance as MainWindowViewModel;
            if (obj != null)
            {
                // ValidationCountプロパティで指定された文字数以上だった場合は、エラーとする。
                if (obj.InputString.Length >= obj.ValidationCount)
                {
                    var msg = string.Format("{0}文字以内で入力してください。", obj.ValidationCount);
                    return new ValidationResult(msg);
                }
            }
            return ValidationResult.Success;
        }

        private int validationCount;
        /// <summary>
        /// InputStringの検証に使用する上限文字数を取得または設定します。
        /// </summary>
        public int ValidationCount
        {
            get { return validationCount; }
            set { this.SetProperty(ref this.validationCount, value); this.ValidateProperty(this.InputString, nameof(this.InputString)); }
        }

        public MainWindowViewModel()
        {
            this.InputString = string.Empty;
            this.ValidationCount = 5;
        }
    }
}
