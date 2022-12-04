using Template.Domain.Helpers;

namespace Template.Domain.ValueObjects
{
    /// <summary>
    /// SAMPLE_TABLE の SAMPLE_VALUE
    /// </summary>
    public sealed class SampleValue : ValueObject<SampleValue>
    {
        public const int DecimalPoint = 2;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="value"></param>
        public SampleValue(float value)
        {
            Value = value;
        }

        public float Value { get; }

        public string DisplayValue
        {
            get
            {
                return Value.RoundString(DecimalPoint);
            }
        }

        protected override bool EqualsCore(SampleValue other)
        {
            return Value == other.Value;
        }

        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode();
        }
    }
}
