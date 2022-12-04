using Template.Domain.ValueObjects;

namespace Template.Domain.Entities
{
    public sealed class SampleTableEntity
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SampleTableEntity(long id, 
                                 string text,
                                 float value, 
                                 DateTime? date)
            : this(id, text, string.Empty, value, date, null, string.Empty, string.Empty)
        {
        }

        public SampleTableEntity(
            long id, string text, string comboBoxText,float value, DateTime? date, 
            int? flag, string code, string codeName)
        {
            SampleId = new SampleId(id);
            SampleText = new SampleText(text);
            SampleComboBoxText = new SampleComboBoxText(comboBoxText);
            SampleValue = new SampleValue(value);
            SampleDate = new SampleDate(date);
            SampleFlag = new SampleFlag(flag);
            SampleCode = new SampleCode(code);
            SampleCodeName = new SampleCodeName(codeName);
        }

        public SampleId SampleId { get; set; }
        public SampleText SampleText { get; set; }
        public SampleComboBoxText SampleComboBoxText { get; set; }
        public SampleValue SampleValue { get; set; }
        public SampleDate SampleDate { get; set; }
        public SampleFlag SampleFlag { get; set; }
        public SampleCode SampleCode { get; set; }
        public SampleCodeName SampleCodeName { get; set; }
    }
}
