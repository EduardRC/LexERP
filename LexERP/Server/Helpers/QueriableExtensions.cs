using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LexERP.Shared.DTOs;

namespace LexERP.Server.Helpers
{
    public static class QueriableExtensions
    {
        public static IQueryable<T> Paginar<T>(this IQueryable<T> queryable, PaginacionDTO paginacion)
        {
            return queryable
                .Skip((paginacion.Pagina - 1) * paginacion.CantidadRegistros)
                .Take(paginacion.CantidadRegistros);
        }
    }
}
