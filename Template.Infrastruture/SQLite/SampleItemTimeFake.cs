using System.Data.SQLite;
using Template.Domain.Entities;
using Template.Domain.Repositories;
using Template.Domain.SQLite;

namespace Template.Infrastruture.SQLite
{
    internal class SampleItemTimeFake : ISampleItemTimeRepository
    {
        public IReadOnlyList<SampleItemTimeEntity> GetData()
        {
            string sql = @"
SELECT
  sample_name,
  sample_item,
  sample_time
FROM
  sample_item_time
";

            return SQLiteHelper.Query(sql,
                reader =>
                {
                    return new SampleItemTimeEntity(
                        Convert.ToString(reader["sample_name"]),
                        Convert.ToString(reader["sample_item"]),
                        Convert.ToSingle(reader["sample_time"]));
                });
        }

        public void Save(SampleItemTimeEntity entity)
        {
            string insert = @"
insert into sample_item_time
(sample_name, sample_item, sample_time)
values
(@sample_name, @sample_item, @sample_time)
";
            string update = @"
update sample_item_time
set 
  sample_time = @sample_time
where
  sample_name = @sample_name and sample_item = @sample_item
";
            var args = new List<SQLiteParameter>
            {
                new SQLiteParameter("@sample_name", entity.SampleName.Value),
                new SQLiteParameter("@sample_item", entity.SampleItem.Value),
                new SQLiteParameter("@sample_time", entity.SampleTime.Value),
            };

            SQLiteHelper.Execute(insert, update, args.ToArray());
        }
    }
}
