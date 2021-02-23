using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexERP.Shared.DTOs
{
    public class DatoContactoDTO
    {
        public DatoContactoDTO()
        {
            TipoContacto = new TipoContactoDTO();
        }

        // aqui se guardará en Valor los datos de "telefono", "email", "socialmedia" segun el TipoContacto definido

        public int Id { get; set; }
        //public int PersonaId { get; set; }
        public TipoContactoDTO TipoContacto { get; set; }

        [StringLength(80)]
        public string Valor { get; set; }

        public string Observaciones { get; set; }

        public bool Activo { get; set; }
    }
}
