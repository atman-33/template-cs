using System.Data;
using Template.Domain;
using Template.Domain.Entities;
using Template.Domain.Repositories;

namespace Template.Infrastruture.Csv
{
    internal class TaskCsv : ITaskRepository
    {
        /// <summary>
        /// データ読み込み開始行
        /// </summary>
        const int StartDataRow = 2;

        public IReadOnlyList<TaskEntity> GetCsvData()
        {
            return CsvHelper.ReadLines(Shared.SampleListCsvPath + "SampleList.csv",
                2,
                values =>
                {
                    return new TaskEntity(
                        Convert.ToInt32(values[0]),
                        Convert.ToString(values[1]),
                        Convert.ToDateTime(values[2]));
                });
        }

        public DataTable GetExcelSheetDataToDataTable(string filePath, string sheetName, bool isFirstRowColumnName)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<TaskEntity> GetExcelSheetDataToList(string filePath, string sheetName, bool isFirstRowColumnName)
        {
            throw new NotImplementedException();
        }
    }
}
