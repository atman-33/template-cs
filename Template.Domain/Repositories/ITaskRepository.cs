using System.Data;
using Template.Domain.Entities;

namespace Template.Domain.Repositories
{
    public interface ITaskRepository
    {
        IReadOnlyList<TaskEntity> GetCsvData();

        DataTable GetExcelSheetDataToDataTable(string filePath, string sheetName, bool isFirstRowColumnName);
        IReadOnlyList<TaskEntity> GetExcelSheetDataToList(string filePath, string sheetName, bool isFirstRowColumnName);
    }
}
