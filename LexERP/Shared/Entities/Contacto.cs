using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexERP.Shared.Entities
{
    public class Contacto: _comun
    {
        public int Id { get; set; }
        public Guid IdPublico { get; set; }

        public int EmpresaId { get; set; }
        public int PersonaId { get; set; }
        public int Empleados { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal ValorFacturacion { get; set; }

        public virtual Empresa Empresa { get; set; }
        public virtual Persona Persona { get; set; }

    }
}
