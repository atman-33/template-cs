using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using Template.WPF.Views;

namespace Template.WPF.ViewModels
{
    public class SampleNavigationViewModel : BindableBase, INavigationAware
    {
        private IDialogService _dialogService;  //// 画面遷移（ダイアログ）

        public SampleNavigationViewModel(IDialogService dialogService)
        {
            //// 画面遷移用（ダイアログ）
            _dialogService = dialogService;

            SampleTableEditButtonCommand = new DelegateCommand(SampleTableEditButtonCommandExecute);
            SampleTableEdit2ButtonCommand = new DelegateCommand(SampleTableEdit2ButtonCommandExecute);
            SampleTableEdit3ButtonCommand = new DelegateCommand(SampleTableEdit3ButtonCommandExecute);
        }

        //// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 
        //// 各オブジェクトのデータバインド
        //// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 
        private string _sampleLabel;
        public string SampleLabel
        {
            get { return _sampleLabel; }
            set { SetProperty(ref _sampleLabel, value); }
        }

        //// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 
        //// DelegateCommand 関連（パラメータパスを受ける場合は型指定が必要）
        //// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 
        public DelegateCommand SampleTableEditButtonCommand { get; }

        private void SampleTableEditButtonCommandExecute()
        {
            //// パラメータ渡し
            var p = new DialogParameters();
            p.Add(nameof(SampleNavigationViewModel.SampleLabel), SampleLabel);            

            //// 画面遷移処理（ダイアログ）
            _dialogService.ShowDialog(nameof(SampleTableEdit1View), p, null);
        }

        public DelegateCommand SampleTableEdit2ButtonCommand { get; }
        private void SampleTableEdit2ButtonCommandExecute()
        {
            //// 画面遷移処理（ダイアログ）
            _dialogService.ShowDialog(nameof(SampleTableEdit2View), null, null);

        }
        public DelegateCommand SampleTableEdit3ButtonCommand { get; }
        private void SampleTableEdit3ButtonCommandExecute()
        {
            //// 画面遷移処理（ダイアログ）
            _dialogService.ShowDialog(nameof(SampleTableEdit3View), null, null);
        }

        //// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 
        //// INavigationAware 実装関連
        //// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 

        /// <summary>
        /// 画面遷移した際の処理
        /// </summary>
        /// <param name="navigationContext"></param>
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            //// 遷移前の画面からパラメータ渡し
            SampleLabel = navigationContext.Parameters.GetValue<string>(nameof(MainWindowViewModel.Title));
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            //// 画面のデータを追加回す場合はTrue
            return false;
        }

        /// <summary>
        /// 別画面へ遷移する際の処理
        /// </summary>
        /// <param name="navigationContext"></param>
        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
    }
}
