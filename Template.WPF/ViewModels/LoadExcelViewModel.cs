using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Windows.Controls;
using Template.Domain.Repositories;
using Template.Infrastruture;

namespace Template.WPF.ViewModels
{
    public class LoadExcelViewModel : BindableBase
    {
        //// 外部接触Repository
        private ITaskRepository _taskRepository;

        private ObservableCollection<LoadExcelViewModelTask> _inputExcel2RecordsOriginal
            = new ObservableCollection<LoadExcelViewModelTask>();

        public LoadExcelViewModel()
        {
            //// Factories経由で作成したRepositoryを、プライベート変数に格納
            _taskRepository = Factories.CreateTaskExcel();

            //// DelegateCommandメソッドを登録
            SelectExcelFileButton = new DelegateCommand(SelectExcelFileButtonExecute);
            LoadExcelButton = new DelegateCommand(LoadExcelButtonExecute);
            LoadExcel2Button = new DelegateCommand(LoadExcel2ButtonExecute);
            SearchTextBoxTextChanged = new DelegateCommand(SearchTextBoxTextChangedExecute);
        }

        //// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 
        //// 各オブジェクトのデータバインド
        //// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 
        private string _filePathTextBox = String.Empty;
        public string FilePathTextBox
        {
            get { return _filePathTextBox; }
            set
            {
                SetProperty(ref _filePathTextBox, value);
            }
        }

        private string _sheetNameTextBox = String.Empty;
        public string SheetNameTextBox
        {
            get { return _sheetNameTextBox; }
            set
            {
                SetProperty(ref _sheetNameTextBox, value);
            }
        }

        private DataTable _inputExcelDataGrid;
        public DataTable InputExcelDataGrid
        {
            get { return _inputExcelDataGrid; }
            set
            {
                SetProperty(ref _inputExcelDataGrid, value);
            }
        }

        private string _searchTextBox;
        public string SearchTextBox
        {
            get { return _searchTextBox; }
            set
            {
                SetProperty(ref _searchTextBox, value);
            }
        }

        private ObservableCollection<LoadExcelViewModelTask> _inputExcel2Records
            = new ObservableCollection<LoadExcelViewModelTask>();
        public ObservableCollection<LoadExcelViewModelTask> InputExcel2Records
        {
            get { return _inputExcel2Records; }
            set
            {
                SetProperty(ref _inputExcel2Records, value);
            }
        }

        private int _inputExcel2DataGridSelectedIndex;
        public int InputExcel2DataGridSelectedIndex
        {
            get { return _inputExcel2DataGridSelectedIndex; }
            set
            {
                SetProperty(ref _inputExcel2DataGridSelectedIndex, value);
            }
        }

        //// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 
        //// DelegateCommand 関連（パラメータパスを受ける場合は型指定が必要）
        //// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 
        public DelegateCommand SelectExcelFileButton { get; }
        private void SelectExcelFileButtonExecute()
        {
            //// ダイアログのインスタンスを生成
            var dialog = new OpenFileDialog();

            //// ファイルの種類を設定
            dialog.Filter = @"Excel ファイル|*.xls;*.xlsx;*.xlsm|テキストファイル (*.txt)|*.txt|全てのファイル (*.*)|*.*";

            //// ダイアログを表示する
            if (dialog.ShowDialog() == true)
            {
                // 選択されたファイル名 (ファイルパス) を取得
                FilePathTextBox = dialog.FileName;
            }
        }

        public DelegateCommand LoadExcelButton { get; }
        private void LoadExcelButtonExecute()
        {
            InputExcelDataGrid = _taskRepository.GetExcelSheetDataToDataTable(FilePathTextBox, SheetNameTextBox, true);
        }

        public DelegateCommand LoadExcel2Button { get; }
        private void LoadExcel2ButtonExecute()
        {
            _inputExcel2RecordsOriginal.Clear();

            foreach (var entity in _taskRepository.GetExcelSheetDataToList(FilePathTextBox, SheetNameTextBox, true))
            {
                //// ViewModelEntityを生成しながらItemSourceに追加
                _inputExcel2RecordsOriginal.Add(new LoadExcelViewModelTask(entity));
            }

            InputExcel2Records = _inputExcel2RecordsOriginal;
        }

        public DelegateCommand SearchTextBoxTextChanged { get; }
        private void SearchTextBoxTextChangedExecute()
        {
            if (SearchTextBox == null)
            {
                return;
            }

            var filterList = _inputExcel2RecordsOriginal.Where(x => x.TaskContent.Contains(SearchTextBox)).ToList();
            var collection = new ObservableCollection<LoadExcelViewModelTask>();

            foreach(var record in filterList)
            {
                collection.Add(record);
            }

            InputExcel2Records = collection;
        }
    }
}