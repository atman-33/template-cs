using Template.Domain.Entities;
using Template.Domain.Repositories;
using Template.Domain.SQLite;

namespace Template.Infrastruture.SQLite
{
    internal class SampleMasterFake : ISampleMasterRepository
    {
        public IReadOnlyList<SampleMasterEntity> GetData()
        {
            string sql = @"
SELECT
  sample_code,
  sample_code_name
FROM
  sample_master
";

            return SQLiteHelper.Query(sql,
                reader =>
                {
                    return new SampleMasterEntity(
                        Convert.ToString(reader["sample_code"]),
                        Convert.ToString(reader["sample_code_name"]));
                });
        }
    }
}
