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

namespace Example2_ValidationRule
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
    }

    public class StringLengthValidationRule : ValidationRule
    {
        public int MaxLength { get; set; }
        public StringLengthValidationRule()
        {
            MaxLength = 10;
        }

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            var v = value as string;
            if (v == null)
                return new ValidationResult(false, "input value should be a string");

            if (v.Count() > MaxLength)
                return new ValidationResult(false, "string is larger than MaxLength");

            return ValidationResult.ValidResult;
        }
    }
}
