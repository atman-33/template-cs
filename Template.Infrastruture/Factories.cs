using Template.Domain;
using Template.Domain.Repositories;
using Template.Infrastruture.Csv;
using Template.Infrastruture.Excel;
using Template.Infrastruture.Oracle;
using Template.Infrastruture.SQLite;

namespace Template.Infrastruture
{
    /// <summary>
    /// Factories
    /// </summary>
    public static class Factories
    {
        public static ISampleTableRepository CreateSampleTable()
        {
            //// FakeはDEBUGのみ実装されるようリスク回避
#if DEBUG
            if (Shared.IsFake)
            {
                return new SampleTableFake();
            }
#endif

            return new SampleTableOracle();
        }

        public static ISampleMasterRepository CreateSampleMaster()
        {
#if DEBUG
            if (Shared.IsFake)
            {
                return new SampleMasterFake();
            }
#endif

            return new SampleMasterOracle();
        }
        public static ISampleItemTimeRepository CreateSampleItemTime()
        {
#if DEBUG
            if (Shared.IsFake)
            {
                return new SampleItemTimeFake();
            }
#endif

            return new SampleItemTimeOracle();
        }

        public static ITaskRepository CreateTaskCsv()
        {
            return new TaskCsv();
        }

        public static ITaskRepository CreateTaskExcel()
        {
            return new TaskExcel();
        }
    }
}
