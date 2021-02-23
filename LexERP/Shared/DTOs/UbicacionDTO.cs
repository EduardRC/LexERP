using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexERP.Shared.DTOs
{
    public class UbicacionDTO
    {
        public UbicacionDTO()
        {
            Pais = new PaisDTO();
        }

        public int Id { get; set; }

        [StringLength(80)]
        public string Direccion { get; set; }

        [StringLength(80)]
        public string Poblacion { get; set; }

        [StringLength(10)]
        public string CodigoPostal { get; set; }

        [StringLength(80)]
        public string Provincia { get; set; }

        public PaisDTO Pais { get; set; }

        [StringLength(50)]
        public string Descripcion { get; set; }

        public string Observaciones { get; set; }

        public bool Activo { get; set; }
    }
}
