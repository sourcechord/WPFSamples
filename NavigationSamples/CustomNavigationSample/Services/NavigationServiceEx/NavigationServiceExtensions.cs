using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CustomNavigationSample.Services.NavigationServiceEx
{
    public static class NavigationServiceExtensions
    {
        /// <summary>
        /// view引数で指定されたインスタンスのページへとナビゲーションを行います。
        /// </summary>
        /// <param name="element"></param>
        /// <param name="view"></param>
        /// <returns></returns>
        public static bool Navigate(this FrameworkElement element, FrameworkElement view)
        {
            var navigator = NavigationServiceEx.GetNavigator(element);
            return navigator.Navigate(view);
        }

        /// <summary>
        /// viewType引数で指定された型のインスタンスを生成し、そのインスタンスのページへとナビゲーションを行います。
        /// </summary>
        /// <param name="element"></param>
        /// <param name="viewType"></param>
        /// <returns></returns>
        public static bool Navigate(this FrameworkElement element, Type viewType)
        {
            var navigator = NavigationServiceEx.GetNavigator(element);
            return navigator.Navigate(viewType);
        }
    }
}
