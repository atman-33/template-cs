using System.Data;
using Template.Domain.Entities;
using Template.Domain.Repositories;

namespace Template.Infrastruture.Excel
{
    internal class TaskExcel : ITaskRepository
    {
        public IReadOnlyList<TaskEntity> GetCsvData()
        {
            throw new NotImplementedException();
        }

        public DataTable GetExcelSheetDataToDataTable(string filePath, string sheetName, bool isFirstRowColumnName)
        {
            return ExcelHelper.GetExcelSheetDataToDataTable(filePath, sheetName, isFirstRowColumnName);
        }

        public IReadOnlyList<TaskEntity> GetExcelSheetDataToList(string filePath, string sheetName, bool isFirstRowColumnName)
        {
            return ExcelHelper.GetExcelSheetDataToList(filePath,
                sheetName,
                isFirstRowColumnName,
                row =>
                {
                    return new TaskEntity(
                        Convert.ToInt32(row[0]),
                        Convert.ToString(row[1]),
                        Convert.ToDateTime(row[2] != DBNull.Value ? row[2] : null));
                });
        }
    }
}
