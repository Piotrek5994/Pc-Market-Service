﻿using Microsoft.Data.SqlClient;
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
        public DataTable GetCustomerList()
        {
            DataTable dt = new DataTable();

            try
            {
                using(SqlConnection conn = new SqlConnection(_sqlConfiguration.ConnectionString))
                {
                    conn.Open();
                    string sql = "select * from dbo.Kontrahent";
                    using(SqlDataAdapter da = new SqlDataAdapter(sql,conn))
                    {
                        da.Fill(dt);
                    }
                    _log.LogInformation("Successfully get customer data");
                }
            }
            catch(SqlException ex)
            {
                _log.LogError($"Failed to connect to the database : {ex.Message}");
            }

            return dt;
        }
    }
}