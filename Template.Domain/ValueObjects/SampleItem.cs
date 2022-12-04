
namespace Template.Domain.ValueObjects
{
    /// <summary>
    /// SAMPLE_ITEM_TIME の SampleItem
    /// </summary>
    public sealed class SampleItem : ValueObject<SampleItem>
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="value"></param>
        public SampleItem(string value)
        {
            Value = value;
        }

        public string Value { get; }

        protected override bool EqualsCore(SampleItem other)
        {
            return Value == other.Value;
        }

        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode();
        }
    }
}
