using System;
using System.Collections.Generic;
using System.Text;

namespace LexERP.Shared.DTOs
{
    public class PaginacionDTO
    {
        public int Pagina { get; set; } = 1;
        public int CantidadRegistros { get; set; } = 10;
    }
}
