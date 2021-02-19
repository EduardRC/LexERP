using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexERP.Shared.DTOs
{
    public class TarifaDTO
    {
        public TarifaDTO()
        {
            Empresa = new EmpresaDTO();
            Detalle = new List<TarifaDetalleDTO>();
        }

        public int Id { get; set; }
        public EmpresaDTO Empresa { get; set; }
        [StringLength(5)]
        public string Abreviatura { get; set; }
        [StringLength(50)]
        public string Descripcion { get; set; }
        public string Observaciones { get; set; }
        public DateTime Fecha { get; set; }
        public bool Predeterminada { get; set; }
        public bool Activo { get; set; }

        public List<TarifaDetalleDTO> Detalle { get; set; }
    }

    public class TarifaDetalleDTO
    {
        public int Id { get; set; }
        public CategoriaDTOmin Categoria { get; set; }
        public UsuarioDTOmin Usuario { get; set; }
        public decimal ImporteHora { get; set; }
        public DateTime Fecha { get; set; }
        public bool Activo { get; set; }
    }

}
