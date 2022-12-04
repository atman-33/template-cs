using Prism.Ioc;
using System.Windows;
using System.Windows.Threading;
using Template.Domain.Exceptions;
using Template.WPF.ViewModels;
using Template.WPF.Views;

namespace Template.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public App()
        {
            //// 例外処理をキャッチする処理
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;
        }
        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            //// 下記処理を追加しても例外発生でアプリが中断する場合、例外停止箇所の例外の編集から中断しないよう設定を変更する

            //_logger.Error(ex.Message, ex);              //// ログ出力

            MessageBoxImage icon = MessageBoxImage.Error;
            string caption = "エラー";
            var exceptionBase = e.Exception as ExceptionBase;    //// 型が異なる場合はnullが返る
            if (exceptionBase != null)
            {
                if (exceptionBase.Kind == ExceptionBase.ExceptionKind.Info)
                {
                    icon = MessageBoxImage.Information;
                    caption = "情報";
                }
                else if (exceptionBase.Kind == ExceptionBase.ExceptionKind.Warning)
                {
                    icon = MessageBoxImage.Warning;
                    caption = "警告";
                }
            }
            MessageBox.Show(e.Exception.Message, caption, MessageBoxButton.OK, icon);

            e.Handled = true;   //// true:アプリケーションが落ちない
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //// ナビゲーション画面
            containerRegistry.RegisterForNavigation<HomeView>();
            containerRegistry.RegisterForNavigation<SampleNavigationView>();
            containerRegistry.RegisterForNavigation<AutoUpdateDisplayView>();
            containerRegistry.RegisterForNavigation<LoadCsvView>();
            containerRegistry.RegisterForNavigation<LoadExcelView>();
            containerRegistry.RegisterForNavigation<SettingsView>();

            //// ダイアログ画面（別画面に表示） ※ViewModelにIDialogAware実装が必要
            containerRegistry.RegisterDialog<SampleTableEdit1View, SampleTableEdit1ViewModel>();
            containerRegistry.RegisterDialog<SampleTableEdit2View, SampleTableEdit2ViewModel>();
            containerRegistry.RegisterDialog<SampleTableEdit3View, SampleTableEdit3ViewModel>();
        }
    }
}
