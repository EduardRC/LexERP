using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexERP.Server.Models
{
    public class Actuacion: _comun
    {
        public int Id { get; set; }

        public int? UsuarioId { get; set; }
        public int ExpedienteId { get; set; }
        public int? TipoActuacionId { get; set; }
        public DateTime Fecha { get; set; }
        public string Texto { get; set; }
        public int Minutos { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")] 
        public decimal PrecioHora { get; set; }
        
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Importe { get; set; }

        public int? ConceptoEconomicoId { get; set; }

        public virtual Usuario Usuario { get; set; }
        public virtual Expediente Expediente { get; set; }
        public virtual TipoActuacion TipoActuacion { get; set; }
        public virtual ConceptoEconomico ConceptoEconomico { get; set; }
    }
}
