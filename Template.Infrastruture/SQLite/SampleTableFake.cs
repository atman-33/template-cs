using System.Data.SQLite;
using Template.Domain.Entities;
using Template.Domain.Repositories;
using Template.Domain.SQLite;

namespace Template.Infrastruture.SQLite
{
    internal class SampleTableFake : ISampleTableRepository
    {
        /// <summary>
        /// sample_tableのデータを取得
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<SampleTableEntity> GetData()
        {
            string sql = @"
SELECT
  sample_id,
  sample_text,
  sample_value,
  sample_date
FROM
  sample_table
";

            return SQLiteHelper.Query(sql,
                reader =>
                {
                    //// sample_dateがDBNullの場合は、nullでEntity生成
                    return new SampleTableEntity(
                        Convert.ToInt32(reader["sample_id"]),
                        Convert.ToString(reader["sample_text"]),
                        Convert.ToSingle(reader["sample_value"]),
                        Convert.ToDateTime(reader["sample_date"] != DBNull.Value ? reader["sample_date"] : null));
                });
        }

        public IReadOnlyList<SampleTableEntity> GetData2()
        {
            string sql = @"
SELECT
  st.sample_id,
  st.sample_text,  
  st.sample_combobox_text,
  st.sample_value,
  st.sample_date,
  IFNULL(st.sample_flag, 0) AS sample_flag,
  st.sample_code,
  IFNULL(sm.sample_code_name, '') AS sample_code_name
FROM
  sample_table st
  LEFT OUTER JOIN sample_master sm ON st.sample_code = sm.sample_code
";

            return SQLiteHelper.Query(sql,
                reader =>
                {
                    //// sample_dateがDBNullの場合は、nullでEntity生成
                    return new SampleTableEntity(
                            Convert.ToInt32(reader["sample_id"]),
                            Convert.ToString(reader["sample_text"]),
                            Convert.ToString(reader["sample_combobox_text"]),
                            Convert.ToSingle(reader["sample_value"]),
                            Convert.ToDateTime(reader["sample_date"] != DBNull.Value ? reader["sample_date"] : null),
                            Convert.ToInt32(reader["sample_flag"]),
                            Convert.ToString(reader["sample_code"]),
                            Convert.ToString(reader["sample_code_name"]));
                });
        }

        public void Save(SampleTableEntity entity)
        {
            string insert = @"
insert into sample_table
(sample_id, sample_text, sample_value, sample_date)
values
(@sample_id, @sample_text, @sample_value, @sample_date)
";
            string update = @"
update sample_table
set 
  sample_text = @sample_text,
  sample_value = @sample_value,
  sample_date = @sample_date
where
  sample_id = @sample_id
";
            var args = new List<SQLiteParameter>
            {
                new SQLiteParameter("@sample_id", entity.SampleId.Value),
                new SQLiteParameter("@sample_text", entity.SampleText.Value),
                new SQLiteParameter("@sample_value", entity.SampleValue.Value),
                new SQLiteParameter("@sample_date", entity.SampleDate.Value),
            };

            SQLiteHelper.Execute(insert, update, args.ToArray());
        }

        public void Save2(SampleTableEntity entity)
        {
            string insert = @"
insert into sample_table
(sample_id, sample_text, sample_combobox_text, sample_value, sample_date, sample_flag, sample_code)
values
(@sample_id, @sample_text, @sample_combobox_text, @sample_value, @sample_date, @sample_flag, @sample_code)
";
            string update = @"
update sample_table
set 
  sample_text = @sample_text,
  sample_combobox_text = @sample_combobox_text,
  sample_value = @sample_value,
  sample_date = @sample_date,
  sample_flag = @sample_flag,
  sample_code = @sample_code
where
  sample_id = @sample_id
";
            var args = new List<SQLiteParameter>
            {
                new SQLiteParameter("@sample_id", entity.SampleId.Value),
                new SQLiteParameter("@sample_text", entity.SampleText.Value),
                new SQLiteParameter("@sample_combobox_text", entity.SampleComboBoxText.Value),
                new SQLiteParameter("@sample_value", entity.SampleValue.Value),
                new SQLiteParameter("@sample_date", entity.SampleDate.Value),
                new SQLiteParameter("@sample_flag", entity.SampleFlag.Value),
                new SQLiteParameter("@sample_code", entity.SampleCode.Value),
            };

            SQLiteHelper.Execute(insert, update, args.ToArray());
        }

        public void Delete(SampleTableEntity entity)
        {
            string delete = @"
DELETE FROM sample_table WHERE sample_id = @sample_id
";

            var args = new List<SQLiteParameter>
            {
                new SQLiteParameter("@sample_id", entity.SampleId.Value),
            };

            SQLiteHelper.Execute(delete, args.ToArray());
        }
    }
}
