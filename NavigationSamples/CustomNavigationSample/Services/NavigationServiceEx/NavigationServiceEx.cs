using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace CustomNavigationSample.Services.NavigationServiceEx
{
    class NavigationServiceEx : DependencyObject
    {
        /// <summary>
        /// ページナビゲーションを行う領域となるContentControlを保持するプロパティ
        /// </summary>
        public ContentControl Content
        {
            get { return (ContentControl)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }
        // Using a DependencyProperty as the backing store for Content.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(ContentControl), typeof(NavigationServiceEx), new PropertyMetadata(null));


        #region ナビゲーションで利用する各種メソッド
        /// <summary>
        /// view引数で指定されたインスタンスのページへとナビゲーションを行います。
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        public bool Navigate(FrameworkElement view)
        {
            this.Content.Content = view;
            return true;
        }

        /// <summary>
        /// viewType引数で指定された型のインスタンスを生成し、そのインスタンスのページへとナビゲーションを行います。
        /// </summary>
        /// <param name="viewType"></param>
        /// <returns></returns>
        public bool Navigate(Type viewType)
        {
            if (viewType == null) { return false; }

            var view = Activator.CreateInstance(viewType) as FrameworkElement;
            return this.Navigate(view);
        }
        #endregion

        /// <summary>
        /// NavigationCommands.GoToPageコマンドに対する応答処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnGoToPage(object sender, ExecutedRoutedEventArgs e)
        {
            var nextPage = e.Parameter as Type;
            this.Navigate(nextPage);
        }


        // 以下、添付プロパティなどの定義

        #region ページナビゲーションを行う領域となるContentControlを指定するための添付プロパティ
        // この添付プロパティで指定した値は、NavigationServiceEx.Contentプロパティとバインドして同期するようにして扱う。
        public static ContentControl GetTarget(DependencyObject obj)
        {
            return (ContentControl)obj.GetValue(TargetProperty);
        }
        public static void SetTarget(DependencyObject obj, ContentControl value)
        {
            obj.SetValue(TargetProperty, value);
        }
        // Using a DependencyProperty as the backing store for Target.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TargetProperty =
            DependencyProperty.RegisterAttached("Target", typeof(ContentControl), typeof(NavigationServiceEx), new PropertyMetadata(null, OnTargetChanged));

        private static void OnTargetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as FrameworkElement;
            var target = e.NewValue as ContentControl;

            if (element != null && target != null)
            {
                // NavigationServiceExのインスタンスを、添付対象のコントロールに付加する。
                var nav = new NavigationServiceEx();
                NavigationServiceEx.SetNavigator(element, nav);

                // ContentプロパティとTargetをバインドしておく。
                BindingOperations.SetBinding(nav, NavigationServiceEx.ContentProperty, new Binding() { Source = target });

                // ナビゲーション用のコマンドバインディング
                element.CommandBindings.Add(new CommandBinding(NavigationCommands.GoToPage, nav.OnGoToPage));

                var startup = NavigationServiceEx.GetStartup(element);
                if (startup != null)
                {
                    nav.Navigate(startup);
                }
            }
        }
        #endregion

        #region スタートアップ時に表示するページを指定するための添付プロパティ
        public static Type GetStartup(DependencyObject obj)
        {
            return (Type)obj.GetValue(StartupProperty);
        }
        public static void SetStartup(DependencyObject obj, Type value)
        {
            obj.SetValue(StartupProperty, value);
        }
        // Using a DependencyProperty as the backing store for Startup.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StartupProperty =
            DependencyProperty.RegisterAttached("Startup", typeof(Type), typeof(NavigationServiceEx), new PropertyMetadata(null, OnStartupChanged));

        private static void OnStartupChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as FrameworkElement;
            var startupType = e.NewValue as Type;

            if (element != null && startupType != null)
            {
                var nav = NavigationServiceEx.GetNavigator(element);
                nav?.Navigate(startupType);
            }
        }
        #endregion


        #region 任意のコントロールに対して、NavigationServiceExをアタッチできるようにするための添付プロパティ
        public static NavigationServiceEx GetNavigator(DependencyObject obj)
        {
            return (NavigationServiceEx)obj.GetValue(NavigatorProperty);
        }
        // ↓protectedにして外部からは利用できないように。
        public static void SetNavigator(DependencyObject obj, NavigationServiceEx value)
        {
            obj.SetValue(NavigatorProperty, value);
        }
        // Using a DependencyProperty as the backing store for Navigator.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NavigatorProperty =
            DependencyProperty.RegisterAttached("Navigator", typeof(NavigationServiceEx), typeof(NavigationServiceEx), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));
        #endregion
    }
}
