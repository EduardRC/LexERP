using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexERP.Server.Models
{
    public class Contrato: _comun
    {
        public Contrato()
        {
            Expedientes = new HashSet<Expediente>();
        }

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
        [Column(TypeName = "money")]
        public decimal Importe { get; set; }

        public string TextoFacturacion { get; set; }
        public int? UsuarioControlId { get; set; }
        public int? TarifaId { get; set; }

        public virtual Cliente Cliente { get; set; }
        public virtual TipoContrato TipoContrato { get; set; }
        public virtual ApplicationUser UsuarioControl { get; set; }
        public virtual Tarifa Tarifa { get; set; }
        public virtual FormaDePago FormaDePago { get; set; }

        public virtual ICollection<Expediente> Expedientes { get; set; }

    }

    public enum Periodicidad
    {
        Mensual, Bimensual, Trimestral, Cuatrimestral, Semestral, Anual
    }
}
