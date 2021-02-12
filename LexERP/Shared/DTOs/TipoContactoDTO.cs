using LexERP.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexERP.Shared.DTOs
{
    public class TipoContactoDTO
    {
        public int Id { get; set; }
        public ClaseTipoContacto Clase { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
    }
}
