
namespace Template.Domain.ValueObjects
{
    public sealed class TaskDeadline : ValueObject<TaskDeadline>
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="value"></param>
        public TaskDeadline(DateTime? value)
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

                return ((DateTime)Value).ToString("yyyy/MM/dd");
            }
        }

        protected override bool EqualsCore(TaskDeadline other)
        {
            return Value == other.Value;
        }

        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode();
        }
        public override string ToString()
        {
            return DisplayValue;
        }
    }
}
