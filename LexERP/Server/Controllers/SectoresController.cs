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
    public class SectoresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SectoresController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<SectorDTO>>> Get([FromQuery] ParametrosBusquedaSeleccion parametrosBusqueda)
        {
            var queryable = _context.Sectores.Where(x => x.Eliminado == false).AsQueryable();

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
                    default:
                        queryable = queryable.OrderBy(s => s.Descripcion);
                        break;
                }
            }

            await HttpContext.InsertarParametrosPaginacionEnRespuesta(queryable, parametrosBusqueda.Paginacion.CantidadRegistros);

            return await queryable.Paginar(parametrosBusqueda.Paginacion)
                .Select(x => new SectorDTO
                {
                    Id = x.Id,
                    Descripcion = x.Descripcion,
                    Activo = x.Activo
                }).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SectorDTO>> Get(int id)
        {
            var element = await _context.Sectores.Where(x => x.Id == id && x.Eliminado == false)
                .FirstOrDefaultAsync();

            if (element == null) { return NotFound(); }

            return new SectorDTO
            {
                Id = element.Id,
                Descripcion = element.Descripcion,
                Activo = element.Activo
            };
        }

        [HttpGet("lista")]
        [HttpGet("lista/{id}")]
        public async Task<ActionResult<List<SectorDTO>>> Lista(int id = 0)
        {
            return await _context.Sectores
                .Where(x => x.Eliminado == false && (x.Activo == true || x.Id == id))
                .OrderBy(x => x.Descripcion)
                .Select(x => new SectorDTO
                {
                    Id = x.Id,
                    Descripcion = x.Descripcion
                }).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(SectorDTO elementDTO)
        {
            var element = new Sector
            {
                Descripcion = elementDTO.Descripcion,
                Activo = true,
                CreadoFecha = DateTime.Now,
                CreadoPor = int.Parse(User.FindFirst(JwtClaimTypes.Id).Value)
            };

            _context.Add(element);
            await _context.SaveChangesAsync();

            return element.Id;
        }

        [HttpPut]
        public async Task<ActionResult> Put(SectorDTO elementDTO)
        {
            var element = await _context.Sectores.FirstOrDefaultAsync(x => x.Id == elementDTO.Id && x.Eliminado == false);

            if (element == null) { return NotFound(); }

            element.Descripcion = elementDTO.Descripcion;
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
                return Forbid("No se puede eliminar este 'Sector', esta asignado a otros registros");
            }

            var element = await _context.Sectores.FirstOrDefaultAsync(x => x.Id == id && x.Eliminado == false);

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

