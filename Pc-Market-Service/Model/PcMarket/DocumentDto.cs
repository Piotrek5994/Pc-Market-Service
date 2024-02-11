using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pc_Market_Service.Model.PcMarket
{
    public class DocumentDto
    {
        public int DokumentId { get; set; }
        public string NazwaDokumentu { get; set; }
        public DateTime? DataWystawieniaDokumentu { get; set; }
        public DateTime? DataPlatnosciDokumentu { get; set; }
        public int TerminPlatnosci { get; set; }
        public decimal DoZaplaty { get; set; }
        public decimal Uregulowano { get; set; }
        public int KontrahentId { get; set; }   
    }
}
