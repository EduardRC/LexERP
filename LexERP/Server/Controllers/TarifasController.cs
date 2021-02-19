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
    public class TarifasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TarifasController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<TarifaDTO>>> Get([FromQuery] ParametrosBusquedaSeleccion parametrosBusqueda)
        {
            var queryable = _context.Tarifas.Where(x => x.Eliminado == false)
                .Include(x => x.Empresa)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(parametrosBusqueda.Buscar))
            {
                var searchString = parametrosBusqueda.Buscar.ToLower();

                queryable = queryable
                    .Where(x => x.Empresa.RazonSocial.ToLower().Contains(searchString) ||
                                x.Abreviatura.ToLower().Contains(searchString) ||
                                x.Descripcion.ToLower().Contains(searchString));
            }

            if (!string.IsNullOrWhiteSpace(parametrosBusqueda.Orden))
            {
                switch (parametrosBusqueda.Orden.ToLower())
                {
                    case "descripcion_desc":
                        queryable = queryable.OrderByDescending(s => s.Descripcion);
                        break;
                    case "empresa":
                        queryable = queryable.OrderBy(s => s.Empresa.RazonSocial);
                        break;
                    case "empresa_desc":
                        queryable = queryable.OrderByDescending(s => s.Empresa.RazonSocial);
                        break;
                    case "abreviatura":
                        queryable = queryable.OrderBy(s => s.Abreviatura);
                        break;
                    case "abreviatura_desc":
                        queryable = queryable.OrderByDescending(s => s.Abreviatura);
                        break;
                    default:
                        queryable = queryable.OrderBy(s => s.Descripcion);
                        break;
                }
            }

            await HttpContext.InsertarParametrosPaginacionEnRespuesta(queryable, parametrosBusqueda.Paginacion.CantidadRegistros);

            return await queryable.Paginar(parametrosBusqueda.Paginacion)
                .Select(x => new TarifaDTO
                {
                    Id = x.Id,
                    Abreviatura = x.Abreviatura,
                    Descripcion = x.Descripcion,
                    Predeterminada = x.Predeterminada,
                    Empresa = new EmpresaDTO { RazonSocial = x.Empresa.RazonSocial },
                    Activo = x.Activo
                }).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TarifaDTO>> Get(int id)
        {
            var element = await _context.Tarifas.Where(x => x.Id == id && x.Eliminado == false)
                .Include(x => x.Detalle).ThenInclude(x => x.Categoria)
                .Include(x => x.Detalle).ThenInclude(x => x.Usuario)
                .FirstOrDefaultAsync();

            if (element == null) { return NotFound(); }

            var tarifa = new TarifaDTO
            {
                Id = element.Id,
                Abreviatura = element.Abreviatura,
                Empresa = new EmpresaDTO { Id = element.EmpresaId },
                Descripcion = element.Descripcion,
                Observaciones = element.Observaciones,
                Fecha = element.Fecha,
                Predeterminada = element.Predeterminada,
                Activo = element.Activo,
                Detalle = new List<TarifaDetalleDTO>()
            };

            foreach (var item in element.Detalle.Where(x=>x.Eliminado==false))
            {
                var detalle = new TarifaDetalleDTO
                {
                    Id = item.Id,
                    ImporteHora = item.ImporteHora,
                    Fecha = item.Fecha,
                    Categoria = new CategoriaDTOmin(),
                    Usuario = new UsuarioDTOmin(),
                    Activo = item.Activo
                };

                if (item.CategoriaId != null)
                {
                    if (!item.Categoria.Eliminado)
                    {
                        detalle.Categoria.Id = item.CategoriaId ?? default(int);
                        detalle.Categoria.Descripcion = item.Categoria.Descripcion;
                        detalle.Categoria.Activo = item.Categoria.Activo;
                    }
                }

                if (item.UsuarioId != null)
                {
                    if (!item.Usuario.Eliminado)
                    {
                        detalle.Usuario.Id = item.UsuarioId ?? default(int);
                        detalle.Usuario.Nombre = item.Usuario.FullName;
                        detalle.Usuario.Activo = item.Usuario.Activo;
                    }
                }

                tarifa.Detalle.Add(detalle);
            }

            return tarifa;
        }

        [HttpGet("lista")]
        [HttpGet("lista/{id}")]
        public async Task<ActionResult<List<TarifaDTO>>> Lista(int id = 0)
        {
            return await _context.Tarifas
                .Where(x => x.Eliminado == false && (x.Activo == true || x.Id == id))
                .OrderBy(x => x.Descripcion)
                .Select(x => new TarifaDTO
                {
                    Id = x.Id,
                    Descripcion = x.Descripcion,
                    Activo = x.Activo
                }).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(TarifaDTO elementDTO)
        {
            var element = new Tarifa
            {
                Abreviatura = elementDTO.Abreviatura,
                EmpresaId = elementDTO.Empresa.Id,
                Descripcion = elementDTO.Descripcion,
                Observaciones = elementDTO.Observaciones,
                Fecha = elementDTO.Fecha,
                Predeterminada = elementDTO.Predeterminada,
                Detalle = new List<TarifaDetalle>(),
                Activo = true,
                CreadoFecha = DateTime.Now,
                CreadoPor = int.Parse(User.FindFirst(JwtClaimTypes.Id).Value)
            };

            foreach (var item in elementDTO.Detalle)
            {
                element.Detalle.Add(NuevoDetalle(item));
            }

            _context.Tarifas.Add(element);
            await _context.SaveChangesAsync();

            return element.Id;
        }

        [HttpPut]
        public async Task<ActionResult> Put(TarifaDTO elementDTO)
        {
            var element = await _context.Tarifas.Include(x=>x.Detalle).FirstOrDefaultAsync(x => x.Id == elementDTO.Id && x.Eliminado == false);

            if (element == null) { return NotFound(); }

            element.Abreviatura = elementDTO.Abreviatura;
            element.EmpresaId = elementDTO.Empresa.Id;
            element.Descripcion = elementDTO.Descripcion;
            element.Observaciones = elementDTO.Observaciones;
            element.Fecha = elementDTO.Fecha;
            element.Predeterminada = elementDTO.Predeterminada;
            element.Activo = elementDTO.Activo;
            element.ModificadoFecha = DateTime.Now;
            element.ModificadoPor = int.Parse(User.FindFirst(JwtClaimTypes.Id).Value);

            // Detalle, 
            // 1.- eliminar los que no esten
            var idsDetalle = elementDTO.Detalle.Select(x => x.Id).ToList();
            foreach (var item in element.Detalle.Where(x => !idsDetalle.Contains(x.Id)))
            {
                //aqui estan los detalles que ahora ya no estan (se han eliminado en la edición)
                item.ModificadoFecha = DateTime.Now;
                item.ModificadoPor = int.Parse(User.FindFirst(JwtClaimTypes.Id).Value);
                item.Eliminado = true;
            }

            // 2.- añadir los nuevos (Id==0)
            foreach (var item in elementDTO.Detalle.Where(x=>x.Id==0))
            {
                element.Detalle.Add(NuevoDetalle(item,element.Id));
            }

            // 3.- actualizar el resto si es necesario
            foreach (var item in elementDTO.Detalle.Where(x => x.Id != 0))
            {
                var detalleActualizar = element.Detalle.FirstOrDefault(x => x.Id == item.Id);

                // si varia Activo, actualizar, si cambia fecha, categoria, usuario o precio,
                // eliminar (marca) y crear nueva, asi se tendra un historial por si se tiene
                // que recuperar analizar algo

                var modificadaCategoria = (detalleActualizar.CategoriaId == null && item.Categoria.Id != 0) ||
                                          (detalleActualizar.CategoriaId != null && item.Categoria.Id != detalleActualizar.CategoriaId);

                var modificadoUsuario = (detalleActualizar.UsuarioId == null && item.Usuario.Id != 0) ||
                                          (detalleActualizar.UsuarioId != null && item.Usuario.Id != detalleActualizar.UsuarioId);

                if (item.ImporteHora != detalleActualizar.ImporteHora ||
                    item.Fecha != detalleActualizar.Fecha ||
                    modificadaCategoria ||
                    modificadoUsuario)
                {
                    // eliminar el actual y crear una nuevo
                    detalleActualizar.ModificadoFecha = DateTime.Now;
                    detalleActualizar.ModificadoPor = int.Parse(User.FindFirst(JwtClaimTypes.Id).Value);
                    detalleActualizar.Eliminado = true;

                    element.Detalle.Add(NuevoDetalle(item, element.Id));
                }
                else
                {
                    if (item.Activo != detalleActualizar.Activo)
                    {
                        detalleActualizar.Activo = item.Activo;
                        detalleActualizar.ModificadoFecha = DateTime.Now;
                        detalleActualizar.ModificadoPor = int.Parse(User.FindFirst(JwtClaimTypes.Id).Value);
                    }
                }
            }

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
                return Forbid("No se puede eliminar esta 'Tarifa', esta asignada a otros registros");
            }

            var element = await _context.Tarifas.FirstOrDefaultAsync(x => x.Id == id && x.Eliminado == false);

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

        private TarifaDetalle NuevoDetalle(TarifaDetalleDTO detalleDTO, int id=0, bool activo=true)
        {
            var detalle = new TarifaDetalle
            {
                TarifaId = id,
                ImporteHora = detalleDTO.ImporteHora,
                Fecha = detalleDTO.Fecha,
                Activo = activo,
                CreadoFecha = DateTime.Now,
                CreadoPor = int.Parse(User.FindFirst(JwtClaimTypes.Id).Value)
            };

            if (detalleDTO.Categoria.Id != 0)
            {
                detalle.CategoriaId = detalleDTO.Categoria.Id;
            }

            if (detalleDTO.Usuario.Id != 0)
            {
                detalle.UsuarioId = detalleDTO.Usuario.Id;
            }

            return detalle;
        }

    }
}
