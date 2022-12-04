using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Template.Domain.Entities;
using Template.Domain.Modules.Helpers;
using Template.Domain.Repositories;
using Template.Domain.StaticValues;
using Template.Domain.ValueObjects;
using Template.Infrastruture;
using Template.WPF.Services;
using static Unity.Storage.RegistrationSet;

namespace Template.WPF.ViewModels
{
    public class SampleTableEdit2ViewModel : BindableBase, IDialogAware
    {
        //// メッセージボックス
        private IMessageService _messageService;

        //// 外部接触Repository
        private ISampleTableRepository _sampleTableRepository;
        private ISampleMasterRepository _sampleMasterRepository;

        public SampleTableEdit2ViewModel()
        {
            //// メッセージボックス
            _messageService = new MessageService();

            //// Factories経由で作成したRepositoryを、プライベート変数に格納
            _sampleTableRepository = Factories.CreateSampleTable();
            _sampleMasterRepository = Factories.CreateSampleMaster();

            //// サンプルマスタのデータを取得
            foreach (var entity in _sampleMasterRepository.GetData()) 
            { 
                SampleMasterRecords.Add(entity);
            }

            //// サンプルテーブルのデータを取得 => DataGridにデータを格納
            foreach (var entity in _sampleTableRepository.GetData2())
            {
                //// ViewModelEntityを生成しながらItemSourceに追加
                SampleTableRecords.Add(new SampleTableEdit2ViewModelSampleTable(entity));
            }

            //// DelegateCommandメソッドを登録
            AButton = new DelegateCommand(AButtonExecute);
            SampleTableSelectedCellsChanged = new DelegateCommand<SelectedCellsChangedEventArgs>(SampleTableSelectedCellsChangedExecute);
            SampleTableCurrentCellChanged = new DelegateCommand(SampleTableCurrentCellChangedExecute);
            SampleTableSortButton = new DelegateCommand(SampleTableSortButtonExecute);
            SampleTableSortDescendingButton = new DelegateCommand(SampleTableSortDescendingButtonExecute);
            SampleTableInsertButton = new DelegateCommand(SampleTableInsertButtonExecute);
            SampleTableDeleteButton = new DelegateCommand(SampleTableDeleteButtonExecute);
            SampleTableSaveButton = new DelegateCommand(SampleTableSaveButtonExecute);
        }

        //// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 
        //// 各オブジェクトのデータバインド
        //// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 
        private bool _buttonAVisibility = true;
        public bool ButtonAVisibility
        {
            get { return _buttonAVisibility; }
            set { SetProperty(ref _buttonAVisibility, value); }
        }

        private bool _buttonBVisibility = false;
        public bool ButtonBVisibility
        {
            get { return _buttonBVisibility; }
            set { SetProperty(ref _buttonBVisibility, value); }
        }

        private bool _sampleDateIsVisible = true;
        public bool SampleDateIsVisible
        {
            get { return _sampleDateIsVisible; }
            set { SetProperty(ref _sampleDateIsVisible, value); }
        }

        private bool _hasViewError;
        public bool HasViewError
        {
            get { return _hasViewError; }
            set { SetProperty(ref _hasViewError, value); }
        }

        private bool _saveButtonIsEnabled;
        public bool SaveButtonIsEnabled
        {
            get { return _saveButtonIsEnabled; }
            set { SetProperty(ref _saveButtonIsEnabled, value); }
        }

        /// <summary>
        /// 勤務帯
        /// </summary>
        private ObservableCollection<WorkShift> _workShifts = StaticWorkSifts.WorkShifts;
        public ObservableCollection<WorkShift> WorkShifts
        {
            get { return _workShifts; }
            set
            {
                SetProperty(ref _workShifts, value);
            }
        }

        /// <summary>
        /// DataGrid ItemSource：表データ
        /// </summary>
        private ObservableCollection<SampleTableEdit2ViewModelSampleTable> _sampleTableRecords 
                = new ObservableCollection<SampleTableEdit2ViewModelSampleTable>();
        public ObservableCollection<SampleTableEdit2ViewModelSampleTable> SampleTableRecords
        {
            get { return _sampleTableRecords; }
            set
            {
                SetProperty(ref _sampleTableRecords, value);
            }
        }

        /// <summary>
        /// DataGridの選択されたレコード（複数の場合は初めに選択したレコード）
        /// </summary>
        private object _sampleTableSlectedItem;
        public object SampleTableSlectedItem
        {
            get { return _sampleTableSlectedItem; }
            set { SetProperty(ref _sampleTableSlectedItem, value); }
        }

        /// <summary>
        /// DataGridの選択された複数アイテム
        /// </summary>
        private IEnumerable _sampleTableSlectedItems;
        public IEnumerable SampleTableSlectedItems
        {
            get { return _sampleTableSlectedItems; }
            set { SetProperty(ref _sampleTableSlectedItems, value); }
        }

        /// <summary>
        /// サンプルコードマスタ
        /// </summary>
        private ObservableCollection<SampleMasterEntity> _sampleMasterRecords = new ObservableCollection<SampleMasterEntity>();
        public ObservableCollection<SampleMasterEntity> SampleMasterRecords
        {
            get { return _sampleMasterRecords; }
            set
            {
                SetProperty(ref _sampleMasterRecords, value);
            }
        }

        private string _textBoxText = "デバッグ用・・・";
        public string TextBoxText
        {
            get { return _textBoxText; }
            set { SetProperty(ref _textBoxText, value); }
        }

        //// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 
        //// DelegateCommand 関連（パラメータパスを受ける場合は型指定が必要）
        //// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 
        public DelegateCommand AButton { get; }
        private void AButtonExecute()
        {
            ButtonBVisibility = !ButtonBVisibility;
        }

        public DelegateCommand<SelectedCellsChangedEventArgs> SampleTableSelectedCellsChanged { get; }
        private void SampleTableSelectedCellsChangedExecute(SelectedCellsChangedEventArgs e)
        {
            if (SampleTableSlectedItem == null)
            {
                return;
            }

            SampleTableEdit2ViewModelSampleTable record = SampleTableSlectedItem as SampleTableEdit2ViewModelSampleTable;

            var sb = new StringBuilder();
            sb.AppendLine("SampleId:" + record.SampleId.ToString()); 
            sb.AppendLine("SampleText:" + record.SampleText);
            sb.AppendLine("SampleComboBoxText:" + record.SampleComboBoxText);
            sb.AppendLine("SampleValue:" + record.SampleValue.ToString());
            sb.AppendLine("SampleDate:" + record.SampleDate.ToString());
            sb.AppendLine("SampleFlag:" + record.SampleFlag.ToString());
            sb.AppendLine("SampleCode:" + record.SampleCode);
            sb.AppendLine("SampleCodeName:" + record.SampleCodeName);

            sb.AppendLine("HasViewError:" + HasViewError.ToString());

            TextBoxText = sb.ToString();
        }

        public DelegateCommand SampleTableCurrentCellChanged { get; }
        private void SampleTableCurrentCellChangedExecute()
        {
            //// Viewエラーが無い時はボタンクリック可能
            SaveButtonIsEnabled = !HasViewError;
        }

        /// <summary>
        /// テーブルをソート（昇順）
        /// </summary>
        public DelegateCommand SampleTableSortButton { get; }
        private void SampleTableSortButtonExecute()
        {
            //// 並べ替え
            SampleTableRecords = new ObservableCollection<SampleTableEdit2ViewModelSampleTable>
                (_sampleTableRecords.OrderBy(n => n.SampleId));
        }

        /// <summary>
        /// テーブルをソート（降順）
        /// </summary>
        public DelegateCommand SampleTableSortDescendingButton { get; }
        private void SampleTableSortDescendingButtonExecute()
        {
            //// 並べ替え
            SampleTableRecords = new ObservableCollection<SampleTableEdit2ViewModelSampleTable>
                (_sampleTableRecords.OrderByDescending(n => n.SampleId));
        }

        /// <summary>
        /// テーブルにレコードを追加
        /// </summary>
        public DelegateCommand SampleTableInsertButton { get; }
        public void SampleTableInsertButtonExecute()
        {
            var entity = new SampleTableEntity(
                _sampleTableRecords.Max(x => x.SampleId) + 1,
                String.Empty,
                0,
                null);
            _sampleTableRecords.Add(new SampleTableEdit2ViewModelSampleTable(entity));
        }

        /// <summary>
        /// テーブルのレコードを削除
        /// </summary>
        public DelegateCommand SampleTableDeleteButton { get; }
        public void SampleTableDeleteButtonExecute()
        {
            var a = _sampleTableSlectedItems;

            foreach (var record in _sampleTableSlectedItems)
            {

                _sampleTableRecords.Remove(record as SampleTableEdit2ViewModelSampleTable);
                _sampleTableRepository.Delete((record as SampleTableEdit2ViewModelSampleTable).Entity);
            }
        }

        /// <summary>
        /// テーブルを保存
        /// </summary>
        public DelegateCommand SampleTableSaveButton { get; }
        public void SampleTableSaveButtonExecute()
        {
            if (_messageService.Question("保存しますか？") != System.Windows.MessageBoxResult.OK)
            {
                return;
            }

            foreach (var record in _sampleTableRecords)
            {

                _sampleTableRepository.Save2(record.Entity);
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
