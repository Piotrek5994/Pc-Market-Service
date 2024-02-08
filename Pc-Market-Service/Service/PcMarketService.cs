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
        public async Task Proccess()
        {
            List<DocumentDto> documentList = MapQueryDocumentResult();
            await CheckInformationInPcMarketDocument(documentList);
        }
        public List<DocumentDto> MapQueryDocumentResult()
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
                        _log.LogError($"Błąd przy zaczytywaniu danych z bazy danych o dokumentach {ex.Message}");
                    }
                }
            }
            return resultDocument;
        }
        public async Task CheckInformationInPcMarketDocument(List<DocumentDto> resultDocument)
        {
            foreach(DocumentDto resultDocumentObject in resultDocument)
            {
                DateTime dataWystawienia = resultDocumentObject.DataWystawieniaDokumentu.Value;
                DateTime today = DateTime.Today;
                DateTime dataPlatnosic = dataWystawienia.AddDays(resultDocumentObject.TerminPlatnosci);

                int differenceInDays = (today - dataWystawienia).Days;
                if(differenceInDays > 7)
                {
                }
            }
        }
        public List<CustomerDto> MapQueryCustomerResult(int customerId)
        {
            List<CustomerDto> resultCustomer = new List<CustomerDto>();
            DataTable dtFromPcMarket = _repository.GetCustomer(customerId);
            if (dtFromPcMarket != null && dtFromPcMarket.Rows.Count > 0)
            {
                foreach(DataRow drFromPcMarket in dtFromPcMarket.Rows)
                {
                    try
                    {
                        CustomerDto resultCustomerObject = new CustomerDto
                        {
                            NazwaKontrahenta = Convert.ToString(drFromPcMarket["Nazwa"]),
                            EmailKontrahenta = Convert.ToString(drFromPcMarket["EMail"]),
                            NumerTelKontrahenta = Convert.ToString(drFromPcMarket["Telefon"])
                        };
                        resultCustomer.Add(resultCustomerObject);
                    }
                    catch(Exception ex)
                    {
                        _log.LogError($"Błąd przy zaczytywaniu danych z bazy danych o kontrahentach {ex.Message}");
                    }
                }
            }
            return resultCustomer;
        }
    }
}
