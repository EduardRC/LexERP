using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexERP.Server.Models
{
    public class Servicio : _comun
    {
        // Servicio del despacho
        // ej. Nominas, Fiscal, Recobro, ...

        public Servicio()
        {
            Clientes = new HashSet<Cliente>();
        }

        public int Id { get; set; }

        [StringLength(50)]
        public string Descripcion { get; set; }

        public virtual ICollection<Cliente> Clientes { get; set; }
    }
}
