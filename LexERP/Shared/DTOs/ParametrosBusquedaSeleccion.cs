using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexERP.Shared.DTOs
{
    public class ParametrosBusquedaSeleccion
    {
        public int Pagina { get; set; } = 1;
        public int Registros { get; set; } = 10;
        public PaginacionDTO Paginacion
        {
            get { return new PaginacionDTO() { Pagina = Pagina, CantidadRegistros = Registros }; }
        }
        public string Buscar { get; set; }
        public string Orden { get; set; }
    }
}
