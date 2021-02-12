using IdentityModel;
using LexERP.Server.Data;
using LexERP.Server.Helpers;
using LexERP.Server.Models;
using LexERP.Shared.DTOs;
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
    public class PaisesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PaisesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<PaisDTO>>> Get([FromQuery] ParametrosBusquedaSeleccion parametrosBusqueda)
        {
            var queryable = _context.Paises.Where(x => x.Eliminado == false)
                .Include(x=>x.Idioma)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(parametrosBusqueda.Buscar))
            {
                var searchString = parametrosBusqueda.Buscar.ToLower();

                queryable = queryable
                    .Where(x => x.Nombre.ToLower().Contains(searchString) ||
                                x.Idioma.Descripcion.ToLower().Contains(searchString));
            }

            if (!string.IsNullOrWhiteSpace(parametrosBusqueda.Orden))
            {
                switch (parametrosBusqueda.Orden.ToLower())
                {
                    case "descripcion_desc":
                        queryable = queryable.OrderByDescending(s => s.Nombre);
                        break;
                    case "idioma":
                        queryable = queryable.OrderBy(s => s.Idioma.Descripcion);
                        break;
                    case "idioma_desc":
                        queryable = queryable.OrderByDescending(s => s.Idioma.Descripcion);
                        break;
                    default:
                        queryable = queryable.OrderBy(s => s.Nombre);
                        break;
                }
            }

            await HttpContext.InsertarParametrosPaginacionEnRespuesta(queryable, parametrosBusqueda.Paginacion.CantidadRegistros);

            return await queryable.Paginar(parametrosBusqueda.Paginacion)
                .Select(x => new PaisDTO
                {
                    Id = x.Id,
                    Nombre = x.Nombre,
                    Idioma = new IdiomaDTO { Descripcion = x.Idioma.Descripcion },
                    Activo = x.Activo
                }).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PaisDTO>> Get(int id)
        {
            var element = await _context.Paises.Where(x => x.Id == id && x.Eliminado == false)
                .Include(x => x.Idioma)
                .FirstOrDefaultAsync();

            if (element == null) { return NotFound(); }

            return new PaisDTO
            {
                Id = element.Id,
                Nombre = element.Nombre,
                Idioma = new IdiomaDTO {Id = element.IdiomaId},
                Activo = element.Activo
            };
        }

        [HttpGet("lista")]
        [HttpGet("lista/{id}")]
        public async Task<ActionResult<List<PaisDTO>>> Lista(int id = 0)
        {
            return await _context.Paises
                .Where(x => x.Eliminado == false && (x.Activo == true || x.Id == id))
                .OrderBy(x => x.Nombre)
                .Select(x => new PaisDTO
                {
                    Id = x.Id,
                    Nombre = x.Nombre,
                    Activo = x.Activo
                }).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(PaisDTO elementDTO)
        {
            var element = new Pais
            {
                Nombre = elementDTO.Nombre,
                IdiomaId = elementDTO.Idioma.Id,
                Activo = true,
                CreadoFecha = DateTime.Now,
                CreadoPor = int.Parse(User.FindFirst(JwtClaimTypes.Id).Value)
            };

            _context.Add(element);
            await _context.SaveChangesAsync();

            return element.Id;
        }

        [HttpPut]
        public async Task<ActionResult> Put(PaisDTO elementDTO)
        {
            var element = await _context.Paises.FirstOrDefaultAsync(x => x.Id == elementDTO.Id && x.Eliminado == false);

            if (element == null) { return NotFound(); }

            element.Nombre = elementDTO.Nombre;
            element.IdiomaId = elementDTO.Idioma.Id;
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

            if (!await CanDelete(id))
            {
                return Forbid("No se puede eliminar este 'País', esta asignado a otros registros");
            }

            var element = await _context.Paises.FirstOrDefaultAsync(x => x.Id == id && x.Eliminado == false);

            if (element == null) { return NotFound(); }

            element.ModificadoFecha = DateTime.Now;
            element.ModificadoPor = int.Parse(User.FindFirst(JwtClaimTypes.Id).Value);
            element.Eliminado = true;

            _context.Attach(element).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("CanDelete/{id}")]
        public async Task<bool> CanDelete(int id)
        {
            // Mirar si se puede eliminar el registro
            // para bloquear si hay dependencias existentes

            return true;
        }


    }
}

