using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexERP.Server.Models
{
    public class Proveedor: _comun
    {
        public Proveedor()
        {
            Conceptos = new HashSet<ConceptoEconomico>();
        }

        public int Id { get; set; }
        public Guid IdPublico { get; set; }

        [StringLength(25)]
        public string CodigoCliente { get; set; }

        public int EmpresaId { get; set; }
        public int PersonaId { get; set; }

        public virtual Empresa Empresa { get; set; }
        public virtual Persona Persona { get; set; }
        public virtual ICollection<ConceptoEconomico> Conceptos { get; set; }
    }
}
