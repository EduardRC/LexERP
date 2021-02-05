using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LexERP.Server.Data;
using LexERP.Server.Helpers;
using LexERP.Shared.DTOs;
using LexERP.Shared.Entities;
using IdentityModel;

namespace LexERP.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DepartamentosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DepartamentosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<DepartamentoDTO>>> Get([FromQuery] ParametrosBusquedaSeleccion parametrosBusqueda)
        {
            var queryable = _context.Departamentos.Where(x => x.Eliminado == false).AsQueryable();

            if (!string.IsNullOrWhiteSpace(parametrosBusqueda.Buscar))
            {
                var searchString = parametrosBusqueda.Buscar.ToLower();

                queryable = queryable
                    .Where(x => x.Descripcion.ToLower().Contains(searchString) ||
                                x.Abreviatura.ToLower().Contains(searchString));
            }

            if (!string.IsNullOrWhiteSpace(parametrosBusqueda.Orden))
            {
                switch (parametrosBusqueda.Orden.ToLower())
                {
                    case "descripcion_desc":
                        queryable = queryable.OrderByDescending(s => s.Descripcion);
                        break;
                    case "abreviatura":
                        queryable = queryable.OrderBy(s => s.Abreviatura);
                        break;
                    case "abreviatura_desc":
                        queryable = queryable.OrderByDescending(s => s.Abreviatura);
                        break;
                    case "orden":
                        queryable = queryable.OrderBy(s => s.Orden);
                        break;
                    case "orden_desc":
                        queryable = queryable.OrderByDescending(s => s.Orden);
                        break;
                    default:
                        queryable = queryable.OrderBy(s => s.Descripcion);
                        break;
                }
            }

            await HttpContext.InsertarParametrosPaginacionEnRespuesta(queryable, parametrosBusqueda.Paginacion.CantidadRegistros);

            return await queryable.Paginar(parametrosBusqueda.Paginacion)
                .Select(x => new DepartamentoDTO
                {
                    Id = x.Id,
                    Abreviatura = x.Abreviatura,
                    Descripcion = x.Descripcion,
                    Orden = x.Orden,
                    Activo = x.Activo
                }).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DepartamentoDTO>> Get(int id)
        {
            var element = await _context.Departamentos.Where(x => x.Id == id && x.Eliminado==false)
                .FirstOrDefaultAsync();

            if (element == null) { return NotFound(); }

            return new DepartamentoDTO
            { 
                Id = element.Id,
                Abreviatura = element.Abreviatura,
                Descripcion = element.Descripcion,
                Orden = element.Orden,
                Activo = element.Activo
            };
        }

        [HttpGet("lista")]
        public async Task<ActionResult<List<DepartamentoDTOmin>>> Get()
        {
            return await _context.Departamentos
                .Where(x=>x.Eliminado==false && x.Activo==true)
                .OrderBy(x => x.Orden)
                .Select(x=> new DepartamentoDTOmin
                {
                    Id = x.Id,
                    Abreviatura = x.Abreviatura,
                    Descripcion = x.Descripcion
                }).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(DepartamentoDTO elementDTO)
        {
            var element = new Departamento
            {
                Abreviatura = elementDTO.Abreviatura,
                Descripcion = elementDTO.Descripcion,
                Orden = elementDTO.Orden,
                Activo = true,
                CreadoFecha = DateTime.Now,
                CreadoPor = int.Parse(User.FindFirst(JwtClaimTypes.Id).Value)
            };

            _context.Add(element);
            await _context.SaveChangesAsync();

            return element.Id;
        }

        [HttpPut]
        public async Task<ActionResult> Put(DepartamentoDTO elementDTO)
        {
            var element = await _context.Departamentos.FirstOrDefaultAsync(x => x.Id == elementDTO.Id && x.Eliminado==false);

            if (element == null) { return NotFound(); }

            element.Abreviatura = elementDTO.Abreviatura;
            element.Descripcion = elementDTO.Descripcion;
            element.Orden = elementDTO.Orden;
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

            var element = await _context.Departamentos.FirstOrDefaultAsync(x => x.Id == id && x.Eliminado == false);

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
