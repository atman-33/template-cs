namespace Template.Domain.ValueObjects
{
    /// <summary>
    /// SAMPLE_TABLE の SAMPLE_ID
    /// </summary>
    public sealed class SampleId : ValueObject<SampleId>
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="value"></param>
        public SampleId(long value)
        {
            Value = value;
        }

        public long Value { get; }

        protected override bool EqualsCore(SampleId other)
        {
            return Value == other.Value;
        }

        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode();
        }
    }
}
