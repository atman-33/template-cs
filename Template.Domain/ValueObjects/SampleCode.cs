using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Domain.ValueObjects
{
    /// <summary>
    /// SAMPLE_TABLE の SAMPLE_CODE
    /// </summary>
    public sealed class SampleCode : ValueObject<SampleCode>
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="value"></param>
        public SampleCode(string value)
        {
            Value = value;
        }

        public string Value { get; }

        protected override bool EqualsCore(SampleCode other)
        {
            return Value == other.Value;
        }

        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode();
        }
    }
}
