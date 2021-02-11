using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexERP.Shared.DTOs
{
    public class PaisDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public IdiomaDTO Idioma { get; set; }
        public bool Activo { get; set; }
    }
}
