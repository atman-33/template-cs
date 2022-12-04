using Oracle.ManagedDataAccess.Client;
using Template.Domain;
using Template.Domain.Entities;
using Template.Domain.Repositories;

namespace Template.Infrastruture.Oracle
{
    internal class SampleTableOracle : ISampleTableRepository
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

            var dao = new OracleOdpDao(Shared.OracleUser, Shared.OraclePassword, Shared.OracleDataSource);
            try {
                return dao.Query(sql,
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
            finally
            {
                dao.Close();
            }
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
  NVL(st.sample_flag, 0) AS sample_flag,
  st.sample_code,
  NVL(sm.sample_code_name, '') AS sample_code_name
FROM
  sample_table st
  LEFT OUTER JOIN sample_master sm ON st.sample_code = sm.sample_code
";

            var dao = new OracleOdpDao(Shared.OracleUser, Shared.OraclePassword, Shared.OracleDataSource);
            try
            {

                return dao.Query(sql,
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
            finally
            {
                dao.Close();
            }
        }

        public void Save(SampleTableEntity entity)
        {
            string insert = @"
insert into sample_table
(sample_id, sample_text, sample_value, sample_date)
values
(:sample_id, :sample_text, :sample_value, :sample_date)
";
            string update = @"
update sample_table
set 
  sample_text = :sample_text,
  sample_value = :sample_value,
  sample_date = :sample_date
where
  sample_id = :sample_id
";
            var args = new List<OracleParameter>
            {
                new OracleParameter("sample_id", entity.SampleId.Value),
                new OracleParameter("sample_text", entity.SampleText.Value),
                new OracleParameter("sample_value", entity.SampleValue.Value),
                new OracleParameter("sample_date", entity.SampleDate.Value),
            };

            var dao = new OracleOdpDao(Shared.OracleUser, Shared.OraclePassword, Shared.OracleDataSource);
            try
            {
                dao.Execute(insert, update, args.ToArray());
            }
            finally
            {
                dao.Close();
            }
        }

        public void Save2(SampleTableEntity entity)
        {
            string insert = @"
insert into sample_table
(sample_id, sample_text, sample_combobox_text, sample_value, sample_date, sample_flag, sample_code)
values
(:sample_id, :sample_text, :sample_combobox_text, :sample_value, :sample_date, :sample_flag, :sample_code)
";
            string update = @"
update sample_table
set 
  sample_text = :sample_text,
  sample_combobox_text = :sample_combobox_text,
  sample_value = :sample_value,
  sample_date = :sample_date,
  sample_flag = :sample_flag,
  sample_code = :sample_code
where
  sample_id = :sample_id
";
            var args = new List<OracleParameter>
            {
                new OracleParameter("sample_id", entity.SampleId.Value),
                new OracleParameter("sample_text", entity.SampleText.Value),
                new OracleParameter("sample_combobox_text", entity.SampleComboBoxText.Value),
                new OracleParameter("sample_value", entity.SampleValue.Value),
                new OracleParameter("sample_date", entity.SampleDate.Value),
                new OracleParameter("sample_flag", entity.SampleFlag.Value),
                new OracleParameter("sample_code", entity.SampleCode.Value),
            };

            var dao = new OracleOdpDao(Shared.OracleUser, Shared.OraclePassword, Shared.OracleDataSource);
            try
            {
                dao.Execute(insert, update, args.ToArray());
            }
            finally
            {
                dao.Close();
            }
        }

        public void Delete(SampleTableEntity entity)
        {
            string delete = @"
DELETE FROM sample_table WHERE sample_id = :sample_id
";

            var args = new List<OracleParameter>
            {
                new OracleParameter("sample_id", entity.SampleId.Value),
            };

            var dao = new OracleOdpDao(Shared.OracleUser, Shared.OraclePassword, Shared.OracleDataSource);
            try
            {
                dao.Execute(delete, args.ToArray());
            }
            finally
            {
                dao.Close();
            }
        }
    }
}
