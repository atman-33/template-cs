using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using Template.Domain.Entities;
using Template.Domain.Modules.Helpers;

namespace Template.WPF.ViewModels
{
    public class SampleTableEdit3ViewModelSampleItemTime
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="sampleItemTimeEntities"></param>
        public SampleTableEdit3ViewModelSampleItemTime(ObservableCollection<SampleItemTimeEntity> sampleItemTimeEntities)
        {
            DataViewItemSource = DataViewHelper.CreatePivotTable<SampleItemTimeEntity, float>(
                "Name",
                sampleItemTimeEntities.ToLookup(o => o.SampleName.Value),
                (i) =>
                {
                    return i.SampleItem.Value;
                },
                (i) =>
                {
                    return i.SampleTime.Value;
                });
        }

        /// <summary>
        /// ViewにバインドするDataView
        /// </summary>
        public DataView DataViewItemSource { get; set; }
    }
}