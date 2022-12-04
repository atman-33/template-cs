using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using Template.Domain;
using Template.Domain.Repositories;
using Template.Domain.StaticValues;
using Template.Infrastruture;
using Template.WPF.BackgroundWorkers;
using Template.WPF.Services;
using Template.WPF.Views;

namespace Template.WPF.ViewModels
{
    public class HomeViewModel : BindableBase
    {
        //// メッセージボックス
        private IMessageService _messageService;

        //// 画面遷移（ダイアログ）
        private IDialogService _dialogService;  

        /// <summary>
        /// タイマー
        /// </summary>
        private DispatcherTimer _timer;

        public HomeViewModel(IDialogService dialogService)
        {
            //// メッセージボックス
            _messageService = new MessageService();

            //// 画面遷移用（ダイアログ）
            _dialogService = dialogService;

            //// タイマー処理の初期化
            InitializeTimer();

            //// DelegateCommandメソッドを登録
            CurrentTimerStartButton = new DelegateCommand(CurrentTimerStartButtonExecute);
            CurrentTimerStopButton = new DelegateCommand(CurrentTimerStopButtonExecute);
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
        public DelegateCommand CurrentTimerStartButton { get; }

        private void CurrentTimerStartButtonExecute()
        {
            _timer.Start();
            BackgroudWorker.Start();
        }

        public DelegateCommand CurrentTimerStopButton { get; }

        private void CurrentTimerStopButtonExecute()
        {
            _timer.Stop();
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
