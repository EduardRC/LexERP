using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexERP.Shared.Entities
{
    public class DatoContacto : _comun
    {
        // aqui se guardara en Valor los datos de "telefono", "email", "socialmedia" segun el TipoContacto definido

        public int Id { get; set; }

        public int PersonaId { get; set; }
        public int TipoContactoId { get; set; }
        
        [StringLength(80)]
        public string Valor { get; set; }

        public string Observaciones { get; set; }

        public virtual Persona Persona { get; set; }
        public virtual TipoContacto TipoContacto { get; set; }

    }
}
