using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LexERP.Server.Helpers
{
    public static class HttpContextExtension
    {
        public async static Task InsertarParametrosPaginacionEnRespuesta<T>(this HttpContext context,
            IQueryable<T> queryable, int cantidadRegistrosAMostrar)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            double totalRegistros = await queryable.CountAsync();
            double totalPaginas = Math.Ceiling(totalRegistros / cantidadRegistrosAMostrar);

            context.Response.Headers.Add("totalRegistros", totalRegistros.ToString());
            context.Response.Headers.Add("totalPaginas", totalPaginas.ToString());
        }
    }
}
