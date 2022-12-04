namespace Template.Domain.ValueObjects
{
    /// <summary>
    /// SAMPLE_TABLE の SAMPLE_TEXT
    /// </summary>
    public sealed class SampleText : ValueObject<SampleText>
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="value"></param>
        public SampleText(string value)
        {
            Value = value;
        }

        public string Value { get; }

        protected override bool EqualsCore(SampleText other)
        {
            return Value == other.Value;
        }

        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode();
        }
    }
}
