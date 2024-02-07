using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pc_Market_Service.Repository.IRepository
{
    public interface IPcMarketRepository
    {
        DataTable GetDocumentList();
    }
}
