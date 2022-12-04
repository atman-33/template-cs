
namespace Template.Domain.ValueObjects
{
    public sealed class TaskId : ValueObject<TaskId>
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="value"></param>
        public TaskId(int value)
        {
            Value = value;
        }

        public int Value { get; }

        protected override bool EqualsCore(TaskId other)
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
