using Pc_Market_Service.Email;
using Pc_Market_Service.Model.PcMarket;
using Pc_Market_Service.Repository.IRepository;
using Pc_Market_Service.Service.IService;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Pc_Market_Service.Service
{
    public class PcMarketService : IPcMarketService
    {
        private readonly IPcMarketRepository _repository;
        private readonly SendingEmails _emails;
        private readonly Logger.Logger _log;

        public PcMarketService(IPcMarketRepository repository, Logger.Logger log,SendingEmails emails)
        {
            _repository = repository;
            _log = log;
            _emails = emails;
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
                            Uregulowano = Convert.ToDecimal(drPzFromPcMarket["Zaplacono"]),
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
            string content;
            foreach(DocumentDto resultDocumentObject in resultDocument)
            {
                //DateTime dataIssuance = resultDocumentObject.DataWystawieniaDokumentu.Value;
                DateTime today = DateTime.Today;
                //DateTime paymentDate = dataIssuance.AddDays(resultDocumentObject.TerminPlatnosci);
                DateTime dataIssuance = DateTime.Parse("2024-02-10"); // Przykładowa data wystawienia
                DateTime paymentDate = DateTime.Parse("2024-02-13"); // Przykładowa data płatności

                int daysUntilDue = (paymentDate - dataIssuance).Days; // Days until payment is due

                if (daysUntilDue == 3 && resultDocumentObject.Uregulowano == 0.0000m)
                {
                    var result = MapQueryCustomerResult(resultDocumentObject.KontrahentId);
                    foreach(CustomerDto customer in result)
                    {
                        content = $"Do upłynięcia terminu płatności za fakture : {resultDocumentObject.NazwaDokumentu}, zostało 3 dni w kwocie : {resultDocumentObject.DoZaplaty}";
                        _emails.SendEmail(content,customer.EmailKontrahenta);
                    }
                }
                else if (daysUntilDue == -3 && resultDocumentObject.Uregulowano == 0.0000m)
                {
                    var result = MapQueryCustomerResult(resultDocumentObject.KontrahentId);
                    foreach (CustomerDto customer in result)
                    {
                        content = $"Termin płatności za fakture : {resultDocumentObject.NazwaDokumentu}, upłyneła 3 dni temu w kwocie : {resultDocumentObject.DoZaplaty}";
                        _emails.SendEmail(content, customer.EmailKontrahenta);
                    }
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
