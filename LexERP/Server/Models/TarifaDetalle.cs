using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexERP.Server.Models
{
    public class TarifaDetalle : _comun
    {
        public int Id { get; set; }
        public int TarifaId { get; set; }
        public int? CategoriaId { get; set; }
        public int? UsuarioId { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal ImporteHora { get; set; }

        public DateTime Fecha { get; set; }

        public virtual Tarifa Tarifa { get; set; }
        public virtual Categoria Categoria { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
