using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Domain.ValueObjects;

namespace Template.Domain.Entities
{
    public sealed class SampleMasterEntity
    {

        public SampleMasterEntity(string sampleCode, string sampleCodeName)
        {
            SampleCode = new SampleCode(sampleCode);
            SampleCodeName = new SampleCodeName(sampleCodeName);
        }

        public SampleCode SampleCode { get; }
        public SampleCodeName SampleCodeName { get; }

        public string ComboBoxValue => SampleCode.Value;
        public string ComboBoxDisplayValue => SampleCodeName.Value;
    }
}
