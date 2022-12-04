using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Template.WPF.Views;

namespace Template.WPF.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private IRegionManager _regionManager;  //// 画面遷移（ナビゲーション）

        public MainWindowViewModel(IRegionManager regionManager)
        {
            //// 画面遷移用（ナビゲーション）
            _regionManager = regionManager;

            //// DelegateCommandメソッドを登録
            WindowContentRendered = new DelegateCommand(WindowContentRenderedExecute);
            HomeViewButton = new DelegateCommand(HomeViewButtonExecute);
            SampleNavigationViewButton = new DelegateCommand(SampleNavigationViewButtonExecute);
            AutoUpdateDisplayViewButton = new DelegateCommand(AutoUpdateDisplayViewButtonExecute);
            LoadCsvViewButton = new DelegateCommand(LoadCsvViewButtonExecute);
            LoadExcelViewButton = new DelegateCommand(LoadExcelViewButtonExecute);

            SettingsViewButton = new DelegateCommand(SettingsViewButtonExecute);
        }

        //// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 
        //// 各オブジェクトのデータバインド
        //// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 

        /// <summary>
        /// ペインタイトル
        /// </summary>
        private string _title = "テンプレートアプリ";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        /// <summary>
        /// メイン画面上部のタイトル
        /// </summary>
        private string _contentTitle = "ホーム";
        public string ContentTitle
        {
            get { return _contentTitle; }
            set { SetProperty(ref _contentTitle, value); }
        }

        //// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 
        //// DelegateCommand 関連（パラメータパスを受ける場合は型指定が必要）
        //// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 

        /// <summary>
        /// ウィンドウのコンテンツが描画された後の処理
        /// </summary>
        public DelegateCommand WindowContentRendered { get; }

        private void WindowContentRenderedExecute()
        {
            ContentTitle = "ホーム";
            _regionManager.RequestNavigate("ContentRegion", nameof(HomeView));
        }

        public DelegateCommand HomeViewButton { get; }

        private void HomeViewButtonExecute()
        {
            ContentTitle = "ホーム";

            //// 画面遷移処理（ナビゲーション）
            _regionManager.RequestNavigate("ContentRegion", nameof(HomeView));
        }

        public DelegateCommand SampleNavigationViewButton { get; }

        private void SampleNavigationViewButtonExecute()
        {
            ContentTitle = "DataGrid検証";

            //// パラメータ渡し
            var p = new NavigationParameters();
            p.Add(nameof(MainWindowViewModel.Title), Title);

            //// 画面遷移処理（ナビゲーション）
            _regionManager.RequestNavigate("ContentRegion", nameof(SampleNavigationView), p);
        }

        public DelegateCommand AutoUpdateDisplayViewButton { get; }

        private void AutoUpdateDisplayViewButtonExecute()
        {
            ContentTitle = "自動画面更新";

            //// 画面遷移処理（ナビゲーション）
            _regionManager.RequestNavigate("ContentRegion", nameof(AutoUpdateDisplayView));
        }

        public DelegateCommand LoadCsvViewButton { get; }

        private void LoadCsvViewButtonExecute()
        {
            ContentTitle = "CSV読み込み + ListView";

            //// 画面遷移処理（ナビゲーション）
            _regionManager.RequestNavigate("ContentRegion", nameof(LoadCsvView));
        }

        public DelegateCommand LoadExcelViewButton { get; }

        private void LoadExcelViewButtonExecute()
        {
            ContentTitle = "Excel読み込み + DataGrid";

            //// 画面遷移処理（ナビゲーション）
            _regionManager.RequestNavigate("ContentRegion", nameof(LoadExcelView));
        }

        public DelegateCommand SettingsViewButton { get; }
        private void SettingsViewButtonExecute()
        {
            ContentTitle = "設定";

            //// 画面遷移処理（ナビゲーション）
            _regionManager.RequestNavigate("ContentRegion", nameof(SettingsView));
        }
    }
}
