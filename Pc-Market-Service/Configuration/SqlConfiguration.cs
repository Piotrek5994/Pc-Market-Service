using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pc_Market_Service.Configuration
{
    public class SqlConfiguration
    {
        public string SqlDatabaseServer { get; set; } = string.Empty;
        public string SqlDatabaseName { get; set; } = string.Empty;
        public string ConnectionString { get; set; } = string.Empty;
    }
}
