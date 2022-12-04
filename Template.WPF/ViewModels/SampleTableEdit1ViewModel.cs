using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Controls;
using Template.Domain.Entities;
using Template.Domain.Modules.Helpers;
using Template.Domain.Repositories;
using Template.Infrastruture;
using Template.WPF.Services;

namespace Template.WPF.ViewModels
{
    public class SampleTableEdit1ViewModel : BindableBase, IDialogAware
    {
        //// メッセージボックス
        private IMessageService _messageService;

        //// 外部接触Repository
        private ISampleTableRepository _sampleTableRepository;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SampleTableEdit1ViewModel()
        {
            //// メッセージボックス
            _messageService = new MessageService();

            //// Factories経由で作成したRepositoryを、プライベート変数に格納
            _sampleTableRepository = Factories.CreateSampleTable();

            //// DelegateCommandメソッドを登録
            SampleTableAutoGeneratingColumn = new DelegateCommand<DataGridAutoGeneratingColumnEventArgs>(SampleTableAutoGeneratingColumnExecute);
            SampleTableMouseDoubleClick = new DelegateCommand<object>(SampleTableMouseDoubleClickExecute);
            GetSampleTableButton = new DelegateCommand(GetSampleTableButtonExecute);
            SaveButton = new DelegateCommand(SaveButtonExecute);
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

        /// <summary>
        /// Label Content：ステータスラベル
        /// </summary>
        private string _statusLabel = "--";
        public string StatusLabel
        {
            get { return _statusLabel; }
            set { SetProperty(ref _statusLabel, value); }
        }

        /// <summary>
        /// ItemSource：表データ
        /// </summary>
        private ObservableCollection<SampleTableEdit1ViewModelSampleTable> _sampleTableRecords
            = new ObservableCollection<SampleTableEdit1ViewModelSampleTable>();
        public ObservableCollection<SampleTableEdit1ViewModelSampleTable> SampleTableRecords
        {
            get { return _sampleTableRecords; }
            set
            {
                SetProperty(ref _sampleTableRecords, value);
            }
        }

        /// <summary>
        /// SelectedItem：選択された表レコード
        /// </summary>
        private SampleTableEdit1ViewModelSampleTable _sampleTableRecordSelectedItem;
        public SampleTableEdit1ViewModelSampleTable SampleTableRecordSelectedItem
        {
            get { return _sampleTableRecordSelectedItem; }
            set { SetProperty(ref _sampleTableRecordSelectedItem, value); }
        }

        /// <summary>
        /// SelectedIndex：表の項目のインデックス
        /// </summary>
        private int _sampleTableRecordSelectedIndex;
        public int SampleTableRecordSelectedIndex
        {
            get { return _sampleTableRecordSelectedIndex; }
            set { SetProperty(ref _sampleTableRecordSelectedIndex, value); }
        }

        private string _sampleIdText;
        public string SampleIdText
        {
            get { return _sampleIdText; }
            set { SetProperty(ref _sampleIdText, value); }
        }

        private string _sampleTextText;
        public string SampleTextText
        {
            get { return _sampleTextText; }
            set { SetProperty(ref _sampleTextText, value); }
        }

        private string _sampleValueText;
        public string SampleValueText
        {
            get { return _sampleValueText; }
            set { SetProperty(ref _sampleValueText, value); }
        }

        private DateTime? _sampleDateValue;
        public DateTime? SampleDateValue    //// DatePickerがnull許容のため、?を付けてnull許容型としている
        {
            get { return _sampleDateValue; }
            set { SetProperty(ref _sampleDateValue, value); }
        }


        //// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 
        //// DelegateCommand 関連（パラメータパスを受ける場合は型指定が必要）
        //// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 
        public DelegateCommand<DataGridAutoGeneratingColumnEventArgs> SampleTableAutoGeneratingColumn { get; }

        private void SampleTableAutoGeneratingColumnExecute(DataGridAutoGeneratingColumnEventArgs e)
        {
            Debug.WriteLine(e.PropertyName);
            _sampleTableRecordSelectedIndex = -1;

            switch (e.PropertyName)
            {
                case "SampleId":
                    //e.Column.Visibility = Visibility.Collapsed;
                    break;

                case "SampleText":
                    e.Column.Header = "サンプルテキスト";
                    break;

                case "SampleValue":
                    e.Column.Header = "サンプル値";
                    break;

                case "SampleDate":
                    e.Column.Header = "サンプル日付";
                    break;

                default:
                    break;
            }
        }

        public DelegateCommand<object> SampleTableMouseDoubleClick { get; }
        private void SampleTableMouseDoubleClickExecute(object timestamp)
        {
            Debug.WriteLine(timestamp);
        }

        public DelegateCommand GetSampleTableButton { get; }
        private void GetSampleTableButtonExecute()
        {
            SampleTableRecords.Clear();

            foreach (var entity in _sampleTableRepository.GetData())
            {
                //// ViewModelEntityを生成しながらItemSourceに追加
                SampleTableRecords.Add(new SampleTableEdit1ViewModelSampleTable(entity));
            }

            StatusLabel = "完了";
        }

        public DelegateCommand SaveButton { get; }
        private void SaveButtonExecute()
        {

            Guard.IsNull(SampleIdText, "IDを入力してください");
            var sampleId = Guard.IsLong(SampleIdText, "サンプル値の入力に誤りがあります");
            var sampleValue = Guard.IsFloat(SampleValueText, "サンプル値の入力に誤りがあります");

            if (SampleTextText == null)
            {
                SampleTextText = String.Empty;
            }

            if (_messageService.Question("保存しますか？") != System.Windows.MessageBoxResult.OK)
            {
                return;
            }

            var entity = new SampleTableEntity(
                sampleId,
                SampleTextText,
                sampleValue,
                SampleDateValue);

            _sampleTableRepository.Save(entity);

            _messageService.ShowDialog("保存しました");
        }


        //// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 
        //// IDialogAware 実装関連
        //// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 
        public string Title => "SampleTable編集";

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog()
        {
            return true;    //// true:画面を閉じる事が可能
        }

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            SampleLabel = parameters.GetValue<string>(nameof(SampleNavigationViewModel.SampleLabel));
        }
    }
}
