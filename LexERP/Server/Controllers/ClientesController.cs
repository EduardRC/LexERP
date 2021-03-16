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
    public class ClientesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ClientesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<ClienteDTOmin>>> Get([FromQuery] ParametrosBusquedaSeleccion parametrosBusqueda)
        {
            var queryable = _context.Clientes.Where(x => x.Eliminado == false)
                .Include(x => x.Persona)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(parametrosBusqueda.Buscar))
            {
                var searchString = parametrosBusqueda.Buscar.ToLower();

                queryable = queryable
                    .Where(x => x.Abreviatura.ToLower().Contains(searchString) ||
                                x.NombreComercial.ToLower().Contains(searchString) ||
                                x.Persona.Nombre.ToLower().Contains(searchString) ||
                                x.Persona.Apellidos.ToLower().Contains(searchString));
            }

            if (!string.IsNullOrWhiteSpace(parametrosBusqueda.Orden))
            {
                switch (parametrosBusqueda.Orden.ToLower())
                {
                    case "nombre_desc":
                        queryable = queryable.OrderByDescending(s => s.Persona.Apellidos).ThenByDescending(s=>s.Persona.Nombre);
                        break;
                    case "abreviatura":
                        queryable = queryable.OrderBy(s => s.Abreviatura);
                        break;
                    case "abreviatura_desc":
                        queryable = queryable.OrderByDescending(s => s.Abreviatura);
                        break;
                    default:
                        queryable = queryable.OrderBy(s => s.Persona.Apellidos).ThenBy(s=>s.Persona.Nombre);
                        break;
                }
            }

            await HttpContext.InsertarParametrosPaginacionEnRespuesta(queryable, parametrosBusqueda.Paginacion.CantidadRegistros);

            return await queryable.Paginar(parametrosBusqueda.Paginacion)
                .Select(x => new ClienteDTOmin
                {
                    Id = x.Id,
                    IdPublico = x.IdPublico,
                    Abreviatura = x.Abreviatura,
                    Nombre = x.Persona.FullName,
                    Activo = x.Activo
                }).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteDTO>> Get(Guid id)
        {
            var element = await _context.Clientes.Where(x => x.IdPublico == id && x.Eliminado == false)
                .Include(x => x.Persona).ThenInclude(x => x.Ubicaciones).ThenInclude(x=>x.Pais)
                .Include(x => x.Persona).ThenInclude(x => x.DatosContacto)
                .Include(x => x.Persona).ThenInclude(x => x.Sectores)
                .Include(x => x.Servicios)
                .Include(x => x.Contactos)
                .FirstOrDefaultAsync();

            if (element == null) { return NotFound(); }

            var cliente = new ClienteDTO
            {
                Id = element.Id,
                IdPublico = element.IdPublico,
                Abreviatura = element.Abreviatura,
                Codigo = element.Codigo,
                CodigoAlternativo = element.CodigoAlternativo,
                NombreComercial = element.NombreComercial,
                ResponsableFacturacion = new UsuarioDTOmin { Id = element.ResponsableFacturacionId ?? default(int) },
                ResponsableComercial = new UsuarioDTOmin { Id = element.ResponsableComercialId ?? default(int) },
                Captador = new UsuarioDTOmin { Id = element.CaptadorId ?? default(int) },
                Tarifa = new TarifaDTO { Id = element.TarifaId ?? default(int)},
                AplicarIVA = element.AplicarIVA,
                AplicarRetencion = element.AplicarRetencion,
                Empleados = element.Empleados,
                ValorFacturacion = element.ValorFacturacion,
                Parent = new ClienteDTOmin { Id = element.ParentId ?? default(int)},
                Activo = element.Activo
            };

            foreach (var item in element.Persona.Ubicaciones.Where(x => x.Eliminado == false))
            {
                var ubicacion = new UbicacionDTO
                {
                    Id = item.Id,
                    CodigoPostal = item.CodigoPostal,
                    Direccion = item.Direccion,
                    Poblacion = item.Poblacion,
                    Provincia = item.Provincia,
                    Pais = new PaisDTO { Id = item.PaisId, Nombre = item.Pais?.Nombre },
                    Descripcion = item.Descripcion,
                    Observaciones = item.Observaciones,
                    Activo = item.Activo
                };

                if (item.Id==element.Persona.UbicacionPrincipalId)
                {
                    cliente.Persona.UbicacionPrincipal = ubicacion;
                }
                else
                {
                    cliente.Persona.Ubicaciones.Add(ubicacion);
                }
            }

            foreach (var item in element.Servicios.Where(x => x.Eliminado == false))
            {
                cliente.Servicios.Add(new ServicioDTO { Id = item.Id, Descripcion = item.Descripcion, Activo = item.Activo });
            }

            foreach (var item in element.Contactos.Where(x => x.Eliminado == false))
            {
                cliente.Contactos.Add(new PersonaDTO 
                { 
                    Id = item.Id,
                    Activo = item.Activo,
                    Tipo = item.Tipo,
                    Documento = item.Documento,
                    Saludo = item.Saludo,
                    Nombre = item.Nombre, 
                    Apellidos=item.Apellidos, 
                    Cargo=item.Cargo, 

                    UbicacionPrincipal = new UbicacionDTO { },
                    Idioma = new IdiomaDTO { Id = item.IdiomaId },
                    FechaNacimiento = item.FechaNacimiento,
                    DatosContacto = new List<DatoContactoDTO> { },

                    Observaciones = item.Observaciones
                });
            }

            foreach (var item in element.Persona.Sectores.Where(x => x.Eliminado == false))
            {
                cliente.Persona.Sectores.Add(new SectorDTO { Id = item.Id, Descripcion=item.Descripcion, Activo = item.Activo });
            }

            return cliente;
        }

        [HttpGet("lista")]
        [HttpGet("lista/{id}")]
        public async Task<ActionResult<List<ClienteDTOmin>>> Lista(int id = 0)
        {
            return await _context.Clientes
                .Include(x=>x.Persona)
                .Where(x => x.Eliminado == false && (x.Activo == true || x.Id == id))
                .Select(x => new ClienteDTOmin
                {
                    Id = x.Id,
                    Abreviatura = x.Abreviatura,
                    Nombre = x.Persona.FullName,
                    Activo = x.Activo
                }).ToListAsync();
        }

        //[HttpPost]
        //public async Task<ActionResult<int>> Post(ClienteDTO elementDTO)
        //{
        //    var element = new Tarifa
        //    {
        //        Abreviatura = elementDTO.Abreviatura,
        //        EmpresaId = elementDTO.Empresa.Id,
        //        Descripcion = elementDTO.Descripcion,
        //        Observaciones = elementDTO.Observaciones,
        //        Fecha = elementDTO.Fecha,
        //        Predeterminada = elementDTO.Predeterminada,
        //        Detalle = new List<TarifaDetalle>(),
        //        Activo = true,
        //        CreadoFecha = DateTime.Now,
        //        CreadoPor = int.Parse(User.FindFirst(JwtClaimTypes.Id).Value)
        //    };

        //    foreach (var item in elementDTO.Detalle)
        //    {
        //        element.Detalle.Add(NuevoDetalle(item));
        //    }

        //    _context.Tarifas.Add(element);
        //    await _context.SaveChangesAsync();

        //    return element.Id;
        //}

        //[HttpPut]
        //public async Task<ActionResult> Put(ClienteDTO elementDTO)
        //{
        //    var element = await _context.Tarifas.Include(x => x.Detalle).FirstOrDefaultAsync(x => x.Id == elementDTO.Id && x.Eliminado == false);

        //    if (element == null) { return NotFound(); }

        //    element.Abreviatura = elementDTO.Abreviatura;
        //    element.EmpresaId = elementDTO.Empresa.Id;
        //    element.Descripcion = elementDTO.Descripcion;
        //    element.Observaciones = elementDTO.Observaciones;
        //    element.Fecha = elementDTO.Fecha;
        //    element.Predeterminada = elementDTO.Predeterminada;
        //    element.Activo = elementDTO.Activo;
        //    element.ModificadoFecha = DateTime.Now;
        //    element.ModificadoPor = int.Parse(User.FindFirst(JwtClaimTypes.Id).Value);

        //    // Detalle, 
        //    // 1.- eliminar los que no esten
        //    var idsDetalle = elementDTO.Detalle.Select(x => x.Id).ToList();
        //    foreach (var item in element.Detalle.Where(x => !idsDetalle.Contains(x.Id)))
        //    {
        //        //aqui estan los detalles que ahora ya no estan (se han eliminado en la edición)
        //        item.ModificadoFecha = DateTime.Now;
        //        item.ModificadoPor = int.Parse(User.FindFirst(JwtClaimTypes.Id).Value);
        //        item.Eliminado = true;
        //    }

        //    // 2.- añadir los nuevos (Id==0)
        //    foreach (var item in elementDTO.Detalle.Where(x => x.Id == 0))
        //    {
        //        element.Detalle.Add(NuevoDetalle(item, element.Id));
        //    }

        //    // 3.- actualizar el resto si es necesario
        //    foreach (var item in elementDTO.Detalle.Where(x => x.Id != 0))
        //    {
        //        var detalleActualizar = element.Detalle.FirstOrDefault(x => x.Id == item.Id);

        //        // si varia Activo, actualizar, si cambia fecha, categoria, usuario o precio,
        //        // eliminar (marca) y crear nueva, asi se tendra un historial por si se tiene
        //        // que recuperar analizar algo

        //        var modificadaCategoria = (detalleActualizar.CategoriaId == null && item.Categoria.Id != 0) ||
        //                                  (detalleActualizar.CategoriaId != null && item.Categoria.Id != detalleActualizar.CategoriaId);

        //        var modificadoUsuario = (detalleActualizar.UsuarioId == null && item.Usuario.Id != 0) ||
        //                                  (detalleActualizar.UsuarioId != null && item.Usuario.Id != detalleActualizar.UsuarioId);

        //        if (item.ImporteHora != detalleActualizar.ImporteHora ||
        //            item.Fecha != detalleActualizar.Fecha ||
        //            modificadaCategoria ||
        //            modificadoUsuario)
        //        {
        //            // eliminar el actual y crear una nuevo
        //            detalleActualizar.ModificadoFecha = DateTime.Now;
        //            detalleActualizar.ModificadoPor = int.Parse(User.FindFirst(JwtClaimTypes.Id).Value);
        //            detalleActualizar.Eliminado = true;

        //            element.Detalle.Add(NuevoDetalle(item, element.Id));
        //        }
        //        else
        //        {
        //            if (item.Activo != detalleActualizar.Activo)
        //            {
        //                detalleActualizar.Activo = item.Activo;
        //                detalleActualizar.ModificadoFecha = DateTime.Now;
        //                detalleActualizar.ModificadoPor = int.Parse(User.FindFirst(JwtClaimTypes.Id).Value);
        //            }
        //        }
        //    }

        //    _context.Attach(element).State = EntityState.Modified;
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //[HttpDelete("{id}")]
        //public async Task<ActionResult> Delete(int id)
        //{
        //    // al querer eliminiar el registro, lo que haremos sera deshabilitarlo,
        //    // para no perder los enlaces que hay asignados a el,

        //    if (!await CanDelete(id))
        //    {
        //        return Forbid("No se puede eliminar esta 'Tarifa', esta asignada a otros registros");
        //    }

        //    var element = await _context.Tarifas.FirstOrDefaultAsync(x => x.Id == id && x.Eliminado == false);

        //    if (element == null) { return NotFound(); }

        //    element.ModificadoFecha = DateTime.Now;
        //    element.ModificadoPor = int.Parse(User.FindFirst(JwtClaimTypes.Id).Value);
        //    element.Eliminado = true;

        //    _context.Attach(element).State = EntityState.Modified;
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        [HttpGet("CanDelete/{id}")]
        public async Task<bool> CanDelete(int id)
        {
            // Mirar si se puede eliminar el registro
            // para bloquear si hay dependencias existentes

            return true;
        }

    }
}
