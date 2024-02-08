using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Pc_Market_Service.Configuration;
using Pc_Market_Service.Repository.IRepository;
using System.Data;

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
                    string sql = @"select
                                   * from dbo.Dok j 
                                   left join dbo.DokKontr i ON j.DokId = i.DokId";
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
            catch (SqlException ex)
            {
                _log.LogError($"Failed to connect to the database : {ex.Message}");
            }

            return dt;
        }
        public DataTable GetCustomer(int customerId)
        {
            DataTable dt = new DataTable();
            string sql = "SELECT * FROM dbo.Kontrahent WHERE KontrId = @CustomerId";

            try
            {
                using (SqlConnection conn = new SqlConnection(_sqlConfiguration.ConnectionString))
                using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                {
                    conn.Open();

                    da.SelectCommand.Parameters.AddWithValue("@CustomerId", customerId);

                    da.Fill(dt);

                    _log.LogInformation($"Successfully retrieved customer data for CustomerId: {customerId}");
                }
            }
            catch (SqlException ex)
            {
                _log.LogError($"Failed to retrieve customer data for CustomerId: {customerId} - Error: {ex.Message}");
            }

            return dt;
        }
    }
}
