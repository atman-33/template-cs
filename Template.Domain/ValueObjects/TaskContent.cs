
namespace Template.Domain.ValueObjects
{
    public sealed class TaskContent : ValueObject<TaskContent>
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="value"></param>
        public TaskContent(string value)
        {
            Value = value;
        }

        public string Value { get; }

        protected override bool EqualsCore(TaskContent other)
        {
            return Value == other.Value;
        }

        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode();
        }
        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
