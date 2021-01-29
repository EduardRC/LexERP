using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexERP.Shared.Entities
{
    public class Categoria : _comun
    {
        // Abogado Junior, Abogado Senior, Becario, Socio, Comercial, ...

        public int Id { get; set; }

        [StringLength(50)]
        public string Descripcion { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal ImporteHoraBase { get; set; }
    }
}
