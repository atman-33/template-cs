using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Domain.ValueObjects
{
    /// <summary>
    /// SAMPLE_MST の SAMPLE_CODE
    /// </summary>
    public sealed class SampleCodeName : ValueObject<SampleCodeName>
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="value"></param>
        public SampleCodeName(string value)
        {
            Value = value;
        }

        public string Value { get; }

        protected override bool EqualsCore(SampleCodeName other)
        {
            return Value == other.Value;
        }

        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode();
        }
    }
}
