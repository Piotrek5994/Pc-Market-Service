using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pc_Market_Service.Configuration
{
    public class SqlConfiguration
    {
        private string _sqlDatabaseServer;
        private string _sqlDatabaseName;
        private string _connectionString = string.Empty;

        public string SqlDatabaseServer
        {
            get { return _sqlDatabaseServer; }
            set
            {
                _sqlDatabaseServer = value;
                UpdateConnectionString();
            }
        }

        public string SqlDatabaseName
        {
            get { return _sqlDatabaseName; }
            set
            {
                _sqlDatabaseName = value;
                UpdateConnectionString();
            }
        }

        public string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        private void UpdateConnectionString()
        {
            _connectionString = $"Server={_sqlDatabaseServer};Database={_sqlDatabaseName};Trusted_Connection=True;TrustServerCertificate=True";
        }
    }
}
