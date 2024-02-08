using Pc_Market_Service.Model.PcMarket;
using Pc_Market_Service.Repository.IRepository;
using Pc_Market_Service.Service.IService;
using System.Data;

namespace Pc_Market_Service.Service
{
    public class PcMarketService : IPcMarketService
    {
        private readonly IPcMarketRepository _repository;
        private readonly Logger.Logger _log;

        public PcMarketService(IPcMarketRepository repository, Logger.Logger log)
        {
            _repository = repository;
            _log = log;
        }
        public List<DocumentDto> MapQueryResult()
        {

            List<DocumentDto> resultDocument = new List<DocumentDto>();
            DataTable dtFromPcMarket = _repository.GetDocumentList();
            if (dtFromPcMarket != null && dtFromPcMarket.Rows.Count > 0)
            {

                foreach (DataRow drPzFromPcMarket in dtFromPcMarket.Rows)
                {
                    try
                    {
                        DocumentDto resultDocumentObject = new DocumentDto
                        {
                            DokumentId = Convert.ToInt32(drPzFromPcMarket["DokId"]),
                            NazwaDokumentu = Convert.ToString(drPzFromPcMarket["NrDok"]),
                            DataWystawieniaDokumentu = drPzFromPcMarket["DataDod"] != DBNull.Value ? Convert.ToDateTime(drPzFromPcMarket["DataDod"]) : (DateTime?)null,
                            TerminPlatnosci = Convert.ToInt32(drPzFromPcMarket["TerminPlat"]),
                            DoZaplaty = Convert.ToDecimal(drPzFromPcMarket["Razem"]),
                            KontrahentId = (drPzFromPcMarket["KontrId"]) != DBNull.Value ? Convert.ToInt32(drPzFromPcMarket["KontrId"]) : 0
                        };
                        resultDocument.Add(resultDocumentObject);
                    }
                    catch (Exception ex)
                    {
                        _log.LogError($"Błąd przy zaczytywaniu danych z bazy danych {ex.Message}");
                    }
                }
            }
            return resultDocument;
        }
    }
}
