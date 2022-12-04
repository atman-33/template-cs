using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.ObjectModel;
using System.Data;
using Template.Domain.Entities;
using Template.Domain.Modules.Helpers;
using Template.Domain.Repositories;
using Template.Infrastruture;
using Template.WPF.Services;

namespace Template.WPF.ViewModels
{
    public class SampleTableEdit3ViewModel : BindableBase, IDialogAware
    {
        //// メッセージボックス
        private IMessageService _messageService;

        //// 外部接触Repository
        private ISampleItemTimeRepository _sampleItemTimeRepository;

        public SampleTableEdit3ViewModel()
        {
            //// メッセージボックス
            _messageService = new MessageService();

            //// Factories経由で作成したRepositoryを、プライベート変数に格納
            _sampleItemTimeRepository = Factories.CreateSampleItemTime();

            //// サンプル項目時間のデータを取得
            ObservableCollection<SampleItemTimeEntity> sampleItemTimeEntities = new ObservableCollection<SampleItemTimeEntity>();
            foreach (var entity in _sampleItemTimeRepository.GetData())
            {
                sampleItemTimeEntities.Add(entity);
            }
            _sampleItemTimeRecords = new SampleTableEdit3ViewModelSampleItemTime(sampleItemTimeEntities).DataViewItemSource;

            //// DelegateCommandメソッドを登録
            AscendingSortButton = new DelegateCommand(AscendingSortButtonExecute);
            DescendingSortButton = new DelegateCommand(DescendingSortButtonExecute);

            TransferButton = new DelegateCommand(TransferButtonExecute);
            SaveButton = new DelegateCommand(SaveButtonExecute);
        }

        //// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 
        //// 各オブジェクトのデータバインド
        //// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 
        private DataView _sampleItemTimeRecords;
        public DataView SampleItemTimeRecords
        {
            get { return _sampleItemTimeRecords; }
            set
            {
                SetProperty(ref _sampleItemTimeRecords, value);
            }
        }

        private ObservableCollection<SampleItemTimeEntity> _sampleItemTimeStraightRecords;
        public ObservableCollection<SampleItemTimeEntity> SampleItemTimeStraightRecords
        {
            get { return _sampleItemTimeStraightRecords; }
            set
            {
                SetProperty(ref _sampleItemTimeStraightRecords, value);
            }
        }

        //// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 
        //// DelegateCommand 関連（パラメータパスを受ける場合は型指定が必要）
        //// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 
        public DelegateCommand AscendingSortButton { get; }
        private void AscendingSortButtonExecute()
        {
            //// 昇順
            SampleItemTimeRecords.Sort = "Name";
        }

        public DelegateCommand DescendingSortButton { get; }
        private void DescendingSortButtonExecute()
        {
            //// 降順
            SampleItemTimeRecords.Sort = "Name DESC";
        }

        public DelegateCommand TransferButton { get; }
        private void TransferButtonExecute()
        {
            var entities = DataViewHelper.ToEntities<SampleItemTimeEntity, float>(
                _sampleItemTimeRecords,
                "Name",
                (id, dictionary) =>
                {
                    return new SampleItemTimeEntity(id, dictionary.Key, Convert.ToSingle(dictionary.Value));
                });

            SampleItemTimeStraightRecords = entities;
        }

        public DelegateCommand SaveButton { get; }
        private void SaveButtonExecute()
        {
            if(SampleItemTimeStraightRecords == null)
            {
                _messageService.ShowDialog("先にテーブル変換をして下さい");
                return;
            }

            if (_messageService.Question("保存しますか？") != System.Windows.MessageBoxResult.OK)
            {
                return;
            }

            foreach (var record in SampleItemTimeStraightRecords)
            {

                _sampleItemTimeRepository.Save(record);
            }

            _messageService.ShowDialog("保存しました");
        }


        //// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 
        //// IDialogAware 実装関連
        //// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 
        public string Title => "DataGrid編集";

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
        }
    }
}
