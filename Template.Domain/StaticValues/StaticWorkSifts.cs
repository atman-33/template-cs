using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Domain.StaticValues
{
    public static class StaticWorkSifts
    {
        /// <summary>
        /// 勤務帯のコレクション
        /// </summary>
        public static ObservableCollection<WorkShift> WorkShifts = new ObservableCollection<WorkShift>();

        static StaticWorkSifts()
        {
            string[] workShifts = ConfigurationManager.AppSettings["WorkShifts"].Split(',');

            //foreach (string s in workShifts)
            //{
            //    WorkShifts.Add(new WorkShift { DisplayValue = s });
            //}

            for (int i = 0; i < workShifts.Length; i++)
            {
                WorkShifts.Add(new WorkShift { Value = i, DisplayValue = workShifts[i] });
            }
        }
    }

    public class WorkShift
    {
        public int Value { get; set; }
        public string? DisplayValue { get; set; }
    }
}
