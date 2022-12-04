namespace Template.Domain.ValueObjects
{
    /// <summary>
    /// SAMPLE_TABLE の SAMPLE_DATE
    /// </summary>
    public sealed  class SampleDate : ValueObject<SampleDate>
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="value"></param>
        public SampleDate(DateTime? value)
        {
            Value = value;
        }

        public DateTime? Value { get; }

        public string DisplayValue 
        {
            get 
            { 
                if (Value == null)
                {
                    return string.Empty;
                }

                return Value.ToString();
            }
        }

        protected override bool EqualsCore(SampleDate other)
        {
            return Value == other.Value;
        }

        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode();
        }
    }
}
