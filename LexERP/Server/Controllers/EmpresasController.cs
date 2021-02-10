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
    public class EmpresasController: ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EmpresasController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<EmpresaDTO>>> Get([FromQuery] ParametrosBusquedaSeleccion parametrosBusqueda)
        {
            var queryable = _context.Empresas.Where(x => x.Eliminado == false).AsQueryable();

            if (!string.IsNullOrWhiteSpace(parametrosBusqueda.Buscar))
            {
                var searchString = parametrosBusqueda.Buscar.ToLower();

                queryable = queryable
                    .Where(x => x.RazonSocial.ToLower().Contains(searchString) ||
                                x.Abreviatura.ToLower().Contains(searchString));
            }

            if (!string.IsNullOrWhiteSpace(parametrosBusqueda.Orden))
            {
                switch (parametrosBusqueda.Orden.ToLower())
                {
                    case "razonsocial_desc":
                        queryable = queryable.OrderByDescending(s => s.RazonSocial);
                        break;
                    case "abreviatura":
                        queryable = queryable.OrderBy(s => s.Abreviatura);
                        break;
                    case "abreviatura_desc":
                        queryable = queryable.OrderByDescending(s => s.Abreviatura);
                        break;
                    default:
                        queryable = queryable.OrderBy(s => s.RazonSocial);
                        break;
                }
            }

            await HttpContext.InsertarParametrosPaginacionEnRespuesta(queryable, parametrosBusqueda.Paginacion.CantidadRegistros);

            return await queryable.Paginar(parametrosBusqueda.Paginacion)
                .Select(x => new EmpresaDTO
                {
                    Id = x.Id,
                    Abreviatura = x.Abreviatura,
                    RazonSocial = x.RazonSocial,
                    Activo = x.Activo
                }).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmpresaDTO>> Get(int id)
        {
            var element = await _context.Empresas.Where(x => x.Id == id && x.Eliminado == false)
                .FirstOrDefaultAsync();

            if (element == null) { return NotFound(); }

            return new EmpresaDTO
            {
                Id = element.Id,
                Abreviatura = element.Abreviatura,
                RazonSocial = element.RazonSocial,
                NIF = element.NIF,
                Direccion = element.Direccion,
                Poblacion = element.Poblacion,
                CodigoPostal = element.CodigoPostal,
                Provincia = element.Provincia,
                Observaciones = element.Observaciones,
                Activo = element.Activo
            };
        }

        [HttpGet("lista")]
        public async Task<ActionResult<List<EmpresaDTOlist>>> Get()
        {
            return await _context.Empresas
                .Where(x => x.Eliminado == false && x.Activo == true)
                .OrderBy(x => x.RazonSocial)
                .Select(x => new EmpresaDTOlist
                {
                    Id = x.Id,
                    RazonSocial = x.RazonSocial
                }).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(EmpresaDTO elementDTO)
        {
            var element = new Empresa
            {
                Abreviatura = elementDTO.Abreviatura,
                RazonSocial = elementDTO.RazonSocial,
                NIF = elementDTO.NIF,
                Direccion = elementDTO.Direccion,
                Poblacion = elementDTO.Poblacion,
                CodigoPostal = elementDTO.CodigoPostal,
                Provincia = elementDTO.Provincia,
                Observaciones = elementDTO.Observaciones,
                Activo = true,
                CreadoFecha = DateTime.Now,
                CreadoPor = int.Parse(User.FindFirst(JwtClaimTypes.Id).Value)
            };

            _context.Add(element);
            await _context.SaveChangesAsync();

            return element.Id;
        }

        [HttpPut]
        public async Task<ActionResult> Put(EmpresaDTO elementDTO)
        {
            var element = await _context.Empresas.FirstOrDefaultAsync(x => x.Id == elementDTO.Id && x.Eliminado == false);

            if (element == null) { return NotFound(); }

            element.Abreviatura = elementDTO.Abreviatura;
            element.RazonSocial = elementDTO.RazonSocial;
            element.NIF = elementDTO.NIF;
            element.Direccion = elementDTO.Direccion;
            element.Poblacion = elementDTO.Poblacion;
            element.CodigoPostal = elementDTO.CodigoPostal;
            element.Provincia = elementDTO.Provincia;
            element.Observaciones = elementDTO.Observaciones;
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
                return Forbid("No se puede eliminar esta empresa, esta asignada a otros registros");
            }

            var element = await _context.Departamentos.FirstOrDefaultAsync(x => x.Id == id && x.Eliminado == false);

            if (element == null) { return NotFound(); }

            element.ModificadoFecha = DateTime.Now;
            element.ModificadoPor = int.Parse(User.FindFirst(JwtClaimTypes.Id).Value);
            element.Eliminado = true;

            _context.Attach(element).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("CanDelete{id}")]
        public async Task<bool> CanDelete(int id)
        {
            // Mirar si se puede eliminar el registro
            // para bloquear si hay dependencias existentes

            return true;
        }

    }
}
