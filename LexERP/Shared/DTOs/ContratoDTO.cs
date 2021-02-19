using LexERP.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexERP.Shared.DTOs
{
    public class ContratoDTO
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }

        public int TipoContratoId { get; set; }

        [StringLength(80)]
        public string Descripcion { get; set; }

        public string Observaciones { get; set; }

        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public Periodicidad Periodicidad { get; set; }
        public int DiaFacturacion { get; set; }
        public int FormaDePagoId { get; set; }
        public int PackHoras { get; set; }

        [DataType(DataType.Currency)]
        public decimal Importe { get; set; }

        public string TextoFacturacion { get; set; }
        public int UsuarioControlId { get; set; }
        public int TarifaId { get; set; }

        //public virtual List<ExpedienteDTOmin> Expedientes { get; set; }

        public bool Activo { get; set; }
    }

    public class ContratoDTOmin
    {
        public int Id { get; set; }
        public string TipoContrato { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
    }
}
