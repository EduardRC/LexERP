using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexERP.Shared.DTOs
{
    // Abogado Junior, Abogado Senior, Becario, Socio, Comercial, ...

    public class CategoriaDTO
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string Descripcion { get; set; }
        public decimal ImporteHoraBase { get; set; }
        public bool Activo { get; set; }
    }

    public class CategoriaDTOmin
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
    }
}
