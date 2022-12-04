using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using Template.Domain.StaticValues;
using Template.Domain;
using Template.WPF.BackgroundWorkers;
using Template.WPF.Services;
using Template.WPF.Views;
using System.Windows.Media;

namespace Template.WPF.ViewModels
{
    public class AutoUpdateDisplayViewModel : BindableBase
    {
        //// メッセージボックス
        private IMessageService _messageService;

        //// 画面遷移（ダイアログ）
        private IDialogService _dialogService;

        /// <summary>
        /// タイマー
        /// </summary>
        private DispatcherTimer _timer;

        public AutoUpdateDisplayViewModel(IDialogService dialogService)
        {
            //// メッセージボックス
            _messageService = new MessageService();

            //// 画面遷移用（ダイアログ）
            _dialogService = dialogService;

            //// タイマー処理の初期化
            InitializeTimer();

            //// DelegateCommandメソッドを登録
            AutoUpdateButtonChecked = new DelegateCommand(AutoUpdateButtonCheckedExecute);
            AutoUpdateButtonUnchecked = new DelegateCommand(AutoUpdateButtonUncheckedExecute);

            SampleTableEdit1ViewButton = new DelegateCommand(SampleTableEdit1ViewButtonExecute);
        }

        //// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 
        //// 各オブジェクトのデータバインド
        //// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 

        private string _cuurentTimeLabel = Shared.CurrentTime.ToString("HH:mm:ss");
        public string CuurentTimeLabel
        {
            get { return _cuurentTimeLabel; }
            set { SetProperty(ref _cuurentTimeLabel, value); }
        }

        private string _autoUpdateButtonContent = "自動更新停止中";
        public string AutoUpdateButtonContent
        {
            get { return _autoUpdateButtonContent; }
            set { SetProperty(ref _autoUpdateButtonContent, value); }
        }

        private SolidColorBrush _autoUpdateButtonBackground = new SolidColorBrush(Colors.LightGray);

        public SolidColorBrush AutoUpdateButtonBackground
        {
            get { return _autoUpdateButtonBackground; }
            set { SetProperty(ref _autoUpdateButtonBackground, value); }
        }

        private ObservableCollection<AutoUpdateDisplayViewModelSampleTable> _sampleTableRecords
            = new ObservableCollection<AutoUpdateDisplayViewModelSampleTable>();
        public ObservableCollection<AutoUpdateDisplayViewModelSampleTable> SampleTableRecords
        {
            get { return _sampleTableRecords; }
            set
            {
                SetProperty(ref _sampleTableRecords, value);
            }
        }

        //// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 
        //// DelegateCommand 関連（パラメータパスを受ける場合は型指定が必要）
        //// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 

        //AutoUpdateButtonChecked
        public DelegateCommand AutoUpdateButtonChecked { get; }

        private void AutoUpdateButtonCheckedExecute()
        {
            AutoUpdateButtonContent = "自動更新中";
            AutoUpdateButtonBackground = new SolidColorBrush(Colors.GreenYellow);

            //// 初期処理
            updateSampleTableRecords();

            //// ViewModel側で描画処理Start
            _timer.Start();
            //// Background側でデータ処理Start
            BackgroudWorker.Start();
        }

        //AutoUpdateButtonUnhecked
        public DelegateCommand AutoUpdateButtonUnchecked { get; }

        private void AutoUpdateButtonUncheckedExecute()
        {
            AutoUpdateButtonContent = "自動更新停止中";
            AutoUpdateButtonBackground = new SolidColorBrush(Colors.LightGray);

            //// ViewModel側で描画処理Stop
            _timer.Stop();
            //// Background側でデータ処理Stop
            BackgroudWorker.Stop();
        }

        public DelegateCommand SampleTableEdit1ViewButton { get; }

        private void SampleTableEdit1ViewButtonExecute()
        {
            //// 画面遷移処理（ダイアログ）
            _dialogService.ShowDialog(nameof(SampleTableEdit1View), null, null);
        }

        //// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 
        //// タイマー処理
        //// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 
        private void InitializeTimer()
        {
            // 優先順位を指定してタイマのインスタンスを生成
            _timer = new DispatcherTimer(DispatcherPriority.Background);

            // インターバルを設定
            _timer.Interval = new TimeSpan(0, 0, 1);

            // タイマメソッドを設定
            _timer.Tick += (e, s) => { TimerExecute(); };
        }

        private void TimerExecute()
        {
            CuurentTimeLabel = Shared.CurrentTime.ToString("HH:mm:ss");
            updateSampleTableRecords();
        }

        //// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 
        //// メソッド
        //// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 
        private void updateSampleTableRecords()
        {
            SampleTableRecords.Clear();

            foreach (var entity in StaticSampleTable.Get())
            {
                //// ViewModelEntityを生成しながらItemSourceに追加
                SampleTableRecords.Add(new AutoUpdateDisplayViewModelSampleTable(entity));
            }
        }
    }
}
