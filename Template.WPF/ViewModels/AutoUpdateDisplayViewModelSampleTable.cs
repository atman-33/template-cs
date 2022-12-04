using Template.Domain.Entities;

namespace Template.WPF.ViewModels
{
    public class AutoUpdateDisplayViewModelSampleTable
    {
        private SampleTableEntity _entity;

        public AutoUpdateDisplayViewModelSampleTable(SampleTableEntity entity)
        {
            _entity = entity;
        }

        //// Viewに表示するデータの文字列

        public string SampleId => _entity.SampleId.Value.ToString();    //// => は読取専用プロパティ

        public string SampleText => _entity.SampleText.Value;

        public string SampleValue => _entity.SampleValue.DisplayValue;

        public string SampleDate => _entity.SampleDate.DisplayValue;
    }
}
