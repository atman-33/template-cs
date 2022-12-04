namespace Template.Domain.ValueObjects
{
    /// <summary>
    /// SAMPLE_TABLE の SAMPLE_COMBOBOX_TEXT
    /// </summary>
    public sealed class SampleComboBoxText : ValueObject<SampleText>
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="value"></param>
        public SampleComboBoxText(string value)
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
