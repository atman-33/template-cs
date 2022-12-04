using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Domain.ValueObjects
{
    /// <summary>
    /// SAMPLE_TABLE の SAMPLE_FLAG
    /// </summary>
    public sealed class SampleFlag : ValueObject<SampleFlag>
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="value"></param>
        public SampleFlag(int? value)
        {
            Value = value;
        }

        public int? Value { get; }

        public bool DisplayBool {
            get
            {
                if (Value == null)
                {
                    return false;
                }

                if (Value == 0)
                {
                    return false;
                }

                return true;
            }
        }

        protected override bool EqualsCore(SampleFlag other)
        {
            return Value == other.Value;
        }

        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode();
        }
    }
}
