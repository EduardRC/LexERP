using IdentityModel;
using LexERP.Server.Data;
using LexERP.Server.Helpers;
using LexERP.Shared.DTOs;
using LexERP.Shared.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LexERP.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CategoriasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoriasController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoriaDTO>>> Get([FromQuery] ParametrosBusquedaSeleccion parametrosBusqueda)
        {
            var queryable = _context.Categorias.Where(x => x.Eliminado == false).AsQueryable();

            if (!string.IsNullOrWhiteSpace(parametrosBusqueda.Buscar))
            {
                var searchString = parametrosBusqueda.Buscar.ToLower();

                queryable = queryable
                    .Where(x => x.Descripcion.ToLower().Contains(searchString));
            }

            if (!string.IsNullOrWhiteSpace(parametrosBusqueda.Orden))
            {
                switch (parametrosBusqueda.Orden.ToLower())
                {
                    case "descripcion_desc":
                        queryable = queryable.OrderByDescending(s => s.Descripcion);
                        break;
                    case "importe":
                        queryable = queryable.OrderBy(s => s.ImporteHoraBase);
                        break;
                    case "importe_desc":
                        queryable = queryable.OrderByDescending(s => s.ImporteHoraBase);
                        break;
                    default:
                        queryable = queryable.OrderBy(s => s.Descripcion);
                        break;
                }
            }

            await HttpContext.InsertarParametrosPaginacionEnRespuesta(queryable, parametrosBusqueda.Paginacion.CantidadRegistros);

            return await queryable.Paginar(parametrosBusqueda.Paginacion)
                .Select(x => new CategoriaDTO
                {
                    Id = x.Id,
                    Descripcion = x.Descripcion,
                    ImporteHoraBase = x.ImporteHoraBase,
                    Activo = x.Activo
                }).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriaDTO>> Get(int id)
        {
            var element = await _context.Categorias.Where(x => x.Id == id && x.Eliminado == false)
                .FirstOrDefaultAsync();

            if (element == null) { return NotFound(); }

            return new CategoriaDTO
            {
                Id = element.Id,
                Descripcion = element.Descripcion,
                ImporteHoraBase = element.ImporteHoraBase,
                Activo = element.Activo
            };
        }

        [HttpGet("lista")]
        public async Task<ActionResult<List<CategoriaDTOmin>>> Get()
        {
            return await _context.Categorias
                .Where(x => x.Eliminado == false)
                .OrderBy(x => x.Descripcion)
                .Select(x => new CategoriaDTOmin
                {
                    Id = x.Id,
                    Descripcion = x.Descripcion
                }).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(CategoriaDTO elementDTO)
        {
            var element = new Categoria
            {
                Descripcion = elementDTO.Descripcion,
                ImporteHoraBase = elementDTO.ImporteHoraBase,
                Activo = true,
                CreadoFecha = DateTime.Now,
                CreadoPor = int.Parse(User.FindFirst(JwtClaimTypes.Id).Value)
            };

            _context.Add(element);
            await _context.SaveChangesAsync();

            return element.Id;
        }

        [HttpPut]
        public async Task<ActionResult> Put(CategoriaDTO elementDTO)
        {
            var element = await _context.Categorias.FirstOrDefaultAsync(x => x.Id == elementDTO.Id && x.Eliminado == false);

            if (element == null) { return NotFound(); }

            element.Descripcion = elementDTO.Descripcion;
            element.ImporteHoraBase = elementDTO.ImporteHoraBase;
            element.Activo = elementDTO.Activo;
            element.ModificadoFecha = DateTime.Now;
            element.ModificadoPor = int.Parse(User.FindFirst(JwtClaimTypes.Id).Value);

            _context.Attach(element).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            // al querer eliminiar el registro, lo que haremos sera deshabilitarlo,
            // para no perder los enlaces que hay asignados a el,

            var element = await _context.Categorias.FirstOrDefaultAsync(x => x.Id == id && x.Eliminado == false);

            if (element == null) { return NotFound(); }

            element.ModificadoFecha = DateTime.Now;
            element.ModificadoPor = int.Parse(User.FindFirst(JwtClaimTypes.Id).Value);
            element.Eliminado = true;

            _context.Attach(element).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
