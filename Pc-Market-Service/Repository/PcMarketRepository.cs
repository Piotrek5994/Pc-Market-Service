using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Pc_Market_Service.Configuration;
using Pc_Market_Service.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pc_Market_Service.Repository
{
    public class PcMarketRepository : IPcMarketRepository
    {
        private readonly SqlConfiguration _sqlConfiguration;
        private readonly Logger.Logger _log;

        public PcMarketRepository(IOptions<SqlConfiguration> sqlConfiguration, Logger.Logger log)
        {
            _sqlConfiguration = sqlConfiguration.Value;
            _log = log;
        }
        public DataTable GetDocumentList()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection conn = new SqlConnection(_sqlConfiguration.ConnectionString))
                {
                    conn.Open();
                    string sql = "SELECT * FROM Documents";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                    _log.LogInformation("Successfully got document data");
                }
            }
            catch (Exception ex)
            {
                _log.LogError("Failed to connect to the database");
            }

            return dt;
        }
    }
}
