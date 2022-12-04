using Template.Domain.Entities;

namespace Template.WPF.ViewModels
{
    public sealed class LoadExcelViewModelTask
    {
        private TaskEntity _entity;

        public LoadExcelViewModelTask(TaskEntity entity)
        {
            _entity = entity;
        }

        //// Viewに表示するデータの文字列

        public string TaskId => _entity.TaskId.Value.ToString(); 

        public string TaskContent => _entity.TaskContent.Value;

        public string TaskDeadline => _entity.TaskDeadline.DisplayValue;
    }
}
