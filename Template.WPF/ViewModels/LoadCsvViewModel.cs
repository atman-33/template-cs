using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Data;
using Template.Domain.Entities;
using Template.Domain.Repositories;
using Template.Infrastruture;

namespace Template.WPF.ViewModels
{
    public class LoadCsvViewModel : BindableBase
    {
        //// 外部接触Repository
        private ITaskRepository _taskRepository;

        public LoadCsvViewModel()
        {
            //// Factories経由で作成したRepositoryを、プライベート変数に格納
            _taskRepository = Factories.CreateTaskCsv();

            //// DelegateCommandメソッドを登録
            SampleCsvLoadButton = new DelegateCommand(SampleCsvLoadButtonExecute);
            MoveAllItemsButton = new DelegateCommand(MoveAllItemsButtonExecute);
            MoveSelectedItemButton = new DelegateCommand(MoveSelectedItemButtonExecute);
            RemoveSelectedItemButton = new DelegateCommand(RemoveSelectedItemButtonExecute);
            RemoveAllItemsButton = new DelegateCommand(RemoveAllItemsButtonExecute);
        }

        //// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 
        //// 各オブジェクトのデータバインド
        //// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 
        private ObservableCollection<TaskEntity> _inputCsvListView = new ObservableCollection<TaskEntity>();
        public ObservableCollection<TaskEntity> InputCsvListView
        {
            get { return _inputCsvListView; }
            set
            {
                SetProperty(ref _inputCsvListView, value);
            }
        }

        private TaskEntity _inputCsvListViewSelectedItem;
        public TaskEntity InputCsvListViewSelectedItem
        {
            get { return _inputCsvListViewSelectedItem; }
            set
            {
                SetProperty(ref _inputCsvListViewSelectedItem, value);
            }
        }

        private ObservableCollection<TaskEntity> _outputCsvListView = new ObservableCollection<TaskEntity>();
        public ObservableCollection<TaskEntity> OutputCsvListView
        {
            get { return _outputCsvListView; }
            set
            {
                SetProperty(ref _outputCsvListView, value);
            }
        }

        private TaskEntity _outputCsvListViewSelectedItem;
        public TaskEntity OutputCsvListViewSelectedItem
        {
            get { return _outputCsvListViewSelectedItem; }
            set
            {
                SetProperty(ref _outputCsvListViewSelectedItem, value);
            }
        }

        private int _outputCsvListViewSelectedIndex;
        public int OutputCsvListViewSelectedIndex
        {
            get { return _outputCsvListViewSelectedIndex; }
            set
            {
                SetProperty(ref _outputCsvListViewSelectedIndex, value);
            }
        }

        //// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 
        //// DelegateCommand 関連（パラメータパスを受ける場合は型指定が必要）
        //// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 
        public DelegateCommand SampleCsvLoadButton { get; }
        private void SampleCsvLoadButtonExecute()
        {
            foreach (var entity in _taskRepository.GetCsvData())
            {
                InputCsvListView.Add(entity);
            }
        }

        public DelegateCommand MoveAllItemsButton { get; }
        private void MoveAllItemsButtonExecute()
        {
            foreach (var entity in InputCsvListView)
            {
                OutputCsvListView.Add(entity);
            }
        }

        public DelegateCommand MoveSelectedItemButton { get; }
        private void MoveSelectedItemButtonExecute()
        {
            OutputCsvListView.Add(InputCsvListViewSelectedItem);
        }

        public DelegateCommand RemoveSelectedItemButton { get; }
        private void RemoveSelectedItemButtonExecute()
        {
            OutputCsvListView.Remove(OutputCsvListViewSelectedItem);
        }

        public DelegateCommand RemoveAllItemsButton { get; }
        private void RemoveAllItemsButtonExecute()
        {
            OutputCsvListView.Clear();
        }

        //// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 
        //// 添付プロパティのコールバック
        //// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 
        public Action<int> OutputCsvListViewDropCallback { get { return OutputCsvListViewDropCallbackExecute; } }

        private void OutputCsvListViewDropCallbackExecute(int index)
        {
            if (index >= 0)
            {
                OutputCsvListView.Move(OutputCsvListViewSelectedIndex, (int)index);
            }
        }
    }
}
