using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Domain;
using Template.Domain.Entities;
using Template.Domain.Repositories;
using Template.Domain.SQLite;

namespace Template.Infrastruture.Oracle
{
    internal class SampleMasterOracle : ISampleMasterRepository
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
            var dao = new OracleOdpDao(Shared.OracleUser, Shared.OraclePassword, Shared.OracleDataSource);
            try
            {

                return dao.Query(sql,
                reader =>
                {
                    return new SampleMasterEntity(
                        Convert.ToString(reader["sample_code"]),
                        Convert.ToString(reader["sample_code_name"]));
                });
            }
            finally
            {
                dao.Close();
            }
        }
    }
}
