using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexERP.Shared.DTOs
{
    public class TipoContratoDTO
    {
        public int Id { get; set; }
        public string Abreviatura { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
    }
}
