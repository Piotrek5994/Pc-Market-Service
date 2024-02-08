using Pc_Market_Service.Model.PcMarket;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pc_Market_Service.Service.IService
{
    public interface IPcMarketService
    {
        Task Proccess();
        List<DocumentDto> MapQueryDocumentResult();
        List<CustomerDto> MapQueryCustomerResult(int customerId);
        Task CheckInformationInPcMarketDocument(List<DocumentDto> resultDocument);
    }
}
