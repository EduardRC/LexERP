using System;
using System.Collections.Generic;
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
            Detalle = new HashSet<TarifaDetalleDTO>();
        }

        public int Id { get; set; }
        public EmpresaDTO Empresa { get; set; }
        public string Abreviatura { get; set; }
        public string Descripcion { get; set; }
        public string Observaciones { get; set; }
        public DateTime Fecha { get; set; }
        public bool Predeterminada { get; set; }
        public bool Activo { get; set; }

        public ICollection<TarifaDetalleDTO> Detalle { get; set; }
    }

    public class TarifaDetalleDTO
    {
        public int Id { get; set; }
        public CategoriaDTO Categoria { get; set; }
        public UsuarioDTO Usuario { get; set; }
        public decimal ImporteHora { get; set; }
        public DateTime Fecha { get; set; }
        public bool Activo { get; set; }
    }

}
