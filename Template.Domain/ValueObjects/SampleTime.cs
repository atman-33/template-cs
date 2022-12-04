using Template.Domain.Helpers;

namespace Template.Domain.ValueObjects
{
    /// <summary>
    /// SAMPLE_ITEM_TIME の SampleTime
    /// </summary>
    public sealed class SampleTime : ValueObject<SampleTime>
    {
        public const int DecimalPoint = 2;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="value"></param>
        public SampleTime(float value)
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

        protected override bool EqualsCore(SampleTime other)
        {
            return Value == other.Value;
        }

        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode();
        }
    }
}
