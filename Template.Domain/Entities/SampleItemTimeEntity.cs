using Template.Domain.ValueObjects;

namespace Template.Domain.Entities
{
    public sealed class SampleItemTimeEntity
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SampleItemTimeEntity(string name, string item, float time)
        {
            SampleName = new SampleName(name);
            SampleItem = new SampleItem(item);
            SampleTime = new SampleTime(time);
        }

        public SampleName SampleName { get; set; }
        public SampleItem SampleItem { get; set; }
        public SampleTime SampleTime { get; set; }
    }
}
