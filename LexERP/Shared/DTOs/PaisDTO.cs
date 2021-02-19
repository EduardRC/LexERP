using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexERP.Shared.DTOs
{
    public class PaisDTO
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string Nombre { get; set; }
        public IdiomaDTO Idioma { get; set; }
        public bool Activo { get; set; }
    }
}
