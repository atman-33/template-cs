using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Template.Domain.Entities;
using Template.Domain.ValueObjects;

namespace Template.WPF.ViewModels
{
    /// <summary>
    /// TestDataGridViewのDataGridに表示する1レコード
    /// </summary>
    public class SampleTableEdit2ViewModelSampleTable
    {
        private SampleTableEntity _entity;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="entity"></param>
        public SampleTableEdit2ViewModelSampleTable(SampleTableEntity entity)
        {
            _entity = entity;
        }

        public long SampleId 
        {
            get { return _entity.SampleId.Value; }
            set { _entity.SampleId = new SampleId(value); }
        }
        public string SampleText 
        {
            get { return _entity.SampleText.Value; }
            set { _entity.SampleText = new SampleText(value); }
        }
        public string SampleComboBoxText 
        {
            get { return _entity.SampleComboBoxText.Value; }
            set { _entity.SampleComboBoxText = new SampleComboBoxText(value); }
        }
        public float SampleValue 
        {
            get { return _entity.SampleValue.Value; }
            set { _entity.SampleValue = new SampleValue(value); }
        }
        public DateTime? SampleDate 
        {
            get { return _entity.SampleDate.Value; }
            set { _entity.SampleDate = new SampleDate(value); }
        }
        public bool SampleFlag 
        {
            get { return  Convert.ToBoolean(_entity.SampleFlag.Value); }
            set { _entity.SampleFlag = new SampleFlag(Convert.ToInt32(value)); }
        }
        public string SampleCode 
        {
            get { return _entity.SampleCode.Value; }
            set { _entity.SampleCode = new SampleCode(value); }
        }

        //// 画面に表示しないが、コンボボックス初期値表示のために必要
        public string SampleCodeName 
        {
            get { return _entity.SampleCodeName.Value; }
            set { _entity.SampleCodeName = new SampleCodeName(value); }
        }

        public SampleTableEntity Entity { get { return _entity; } }
    }
}
