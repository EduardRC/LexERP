using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexERP.Server.Models
{
    public class Sector : _comun
    {
        // Sector al que pertenece el contacto/empresa
        // ej. Transportes, Banca, Manufactura, Servicios, ...

        public Sector()
        {
            Personas = new HashSet<Persona>();
        }
        
        public int Id { get; set; }

        [StringLength(50)]
        public string Descripcion { get; set; }

        public virtual ICollection<Persona> Personas { get; set; }
    }
}
