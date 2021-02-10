using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LexERP.Client.Helpers;
using LexERP.Server.Data;
using LexERP.Server.Helpers;
using LexERP.Server.Models;
using LexERP.Shared.DTOs;
using IdentityServer4.Models;
using IdentityModel;

namespace LexERP.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "admin")]
    public class UsuariosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;


        public UsuariosController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<ActionResult<List<UsuarioDTOlist>>> Get([FromQuery] ParametrosBusquedaSeleccion parametrosBusqueda)
        {
            var queryable = _context.Users.Where(x => x.Eliminado == false).AsQueryable();

            if (!string.IsNullOrWhiteSpace(parametrosBusqueda.Buscar))
            {
                var searchString = parametrosBusqueda.Buscar.ToLower();

                queryable = queryable
                    .Where(x => x.Nombre.ToLower().Contains(searchString) ||
                                x.Departamento.Descripcion.ToLower().Contains(searchString) ||
                                x.Categoria.Descripcion.ToLower().Contains(searchString));
            }

            if (!string.IsNullOrWhiteSpace(parametrosBusqueda.Orden))
            {
                switch (parametrosBusqueda.Orden.ToLower())
                {
                    case "nombre_desc":
                        queryable = queryable.OrderByDescending(s => s.Apellidos).ThenByDescending(s => s.Nombre);
                        break;
                    case "departamento":
                        queryable = queryable.OrderBy(s => s.Departamento.Descripcion);
                        break;
                    case "departamento_desc":
                        queryable = queryable.OrderByDescending(s => s.Departamento.Descripcion);
                        break;
                    case "categoria":
                        queryable = queryable.OrderBy(s => s.Categoria.Descripcion);
                        break;
                    case "categoria_desc":
                        queryable = queryable.OrderByDescending(s => s.Categoria.Descripcion);
                        break;
                    default:
                        queryable = queryable.OrderBy(s => s.Apellidos).ThenBy(s => s.Nombre);
                        break;
                }
            }

            await HttpContext.InsertarParametrosPaginacionEnRespuesta(queryable, parametrosBusqueda.Paginacion.CantidadRegistros);

            return await queryable.Paginar(parametrosBusqueda.Paginacion)
                .Select(x => new UsuarioDTOlist
                {
                    Id = x.Id,
                    Nombre = x.FullName,
                    Departamento = x.Departamento.Descripcion,
                    Categoria = x.Categoria.Descripcion,
                    Activo = x.Activo
                }).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioDTO>> Get(int id)
        {
            var usuario = await _context.Users.Where(x => x.Id == id && x.Eliminado == false)
                                .Include(x => x.Departamento)
                                .Include(x => x.Categoria)
                                .FirstOrDefaultAsync();

            if (usuario == null) { return NotFound(); }

            try
            {
                var roles = await _userManager.GetRolesAsync(await _userManager.FindByNameAsync(usuario.Email));

                var usuarioDTO = new UsuarioDTO
                {
                    Id = usuario.Id,
                    Iniciales = usuario.Iniciales,
                    Nombre = usuario.Nombre,
                    Apellidos = usuario.Apellidos,
                    Email = usuario.Email,
                    Password = "",
                    rolName = roles.First(),
                    Departamento = new DepartamentoDTOmin(),
                    Categoria = new CategoriaDTOmin(),
                    EsSocio = usuario.EsSocio,
                    EsResponsableExpediente = usuario.EsResponsableExpediente,
                    EsResponsableFacturacion = usuario.EsResponsableFacturacion,
                    EsResponsableComercial = usuario.EsResponsableComercial,
                    EsCaptadorComisionista = usuario.EsCaptadorComisionista,
                    Activo = usuario.Activo
                };

                if (usuario.Departamento != null)
                {
                    usuarioDTO.Departamento.Id = usuario.Departamento.Id;
                    usuarioDTO.Departamento.Descripcion = usuario.Departamento.Descripcion;
                    usuarioDTO.Departamento.Abreviatura = usuario.Departamento.Abreviatura;
                    usuarioDTO.Departamento.Activo = usuario.Departamento.Activo;
                }

                if (usuario.Categoria != null)
                {
                    usuarioDTO.Categoria.Id = usuario.Categoria.Id;
                    usuarioDTO.Categoria.Descripcion = usuario.Categoria.Descripcion;
                    usuarioDTO.Categoria.Activo = usuario.Categoria.Activo;
                }

                return usuarioDTO;
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            // al querer eliminiar el usuario, lo que haremos sera deshabilitarlo,
            // para no perder los enlaces que hay asignados a el,
            // borramos tambien el email y username para evitar problemas si en el futuro se vuelve a crear con mismo email

            var usuario = await _context.Users.FirstOrDefaultAsync(x => x.Id == id && x.Eliminado == false);

            if (usuario == null) { return NotFound(); }

            usuario.LockoutEnd = DateTime.MaxValue;
            usuario.Email = "";
            usuario.NormalizedEmail = "";
            usuario.UserName = "";
            usuario.NormalizedUserName = "";
            usuario.ModificadoFecha = DateTime.Now;
            usuario.ModificadoPor = int.Parse(User.FindFirst(JwtClaimTypes.Id).Value);
            usuario.Eliminado = true;

            _context.Attach(usuario).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> Post(UsuarioDTO usuarioDTO)
        {
            // crear usuario en Identity
            var applicationUser = new ApplicationUser 
            { 
                UserName = usuarioDTO.Email, 
                Email = usuarioDTO.Email,
                Iniciales = usuarioDTO.Iniciales,
                Nombre = usuarioDTO.Nombre,
                Apellidos = usuarioDTO.Apellidos,
                EsSocio = usuarioDTO.EsSocio,
                EsResponsableExpediente = usuarioDTO.EsResponsableExpediente,
                EsResponsableFacturacion = usuarioDTO.EsResponsableFacturacion,
                EsResponsableComercial = usuarioDTO.EsResponsableComercial,
                EsCaptadorComisionista = usuarioDTO.EsCaptadorComisionista,
                Activo = true,
                CreadoFecha = DateTime.Now,
                CreadoPor = int.Parse(User.FindFirst(JwtClaimTypes.Id).Value)
            };

            if (usuarioDTO.Departamento.Id != 0)
            {
                applicationUser.DepartamentoId = usuarioDTO.Departamento.Id;
            }

            if (usuarioDTO.Categoria.Id != 0)
            {
                applicationUser.CategoriaId = usuarioDTO.Categoria.Id;
            }


            var ir = await _userManager.CreateAsync(applicationUser, usuarioDTO.Password);

            if (ir.Succeeded)
            {
                // asignar role
                var createdUser = await _userManager.FindByNameAsync(usuarioDTO.Email);
                await _userManager.AddToRoleAsync(createdUser, usuarioDTO.rolName);

                return NoContent();
            }
            else
            {
                var exception = new ApplicationException("No se ha podido crear el usuario");
                throw exception;
            }
        }

        [HttpPut]
        public async Task<ActionResult> Put(UsuarioDTO usuarioDTO)
        {
            var usuario = await _context.Users.FirstOrDefaultAsync(x => x.Id == usuarioDTO.Id && x.Eliminado == false);

            if (usuario == null) { return NotFound(); }

            var applicationUser = _userManager.FindByNameAsync(usuario.Email).Result;
            var roles = await _userManager.GetRolesAsync(applicationUser);

            // si hay nueva contraseña, borrar la anterior y asignar la nueva
            if (!string.IsNullOrEmpty(usuarioDTO.Password))
            {
                await _userManager.RemovePasswordAsync(applicationUser);
                await _userManager.AddPasswordAsync(applicationUser, usuarioDTO.Password);
            }

            // si ha varido el rol, eliminar anteriores y asignar el nuevo
            if (roles.First() != usuarioDTO.rolName)
            {
                await _userManager.RemoveFromRolesAsync(applicationUser, roles);
                await _userManager.AddToRoleAsync(applicationUser, usuarioDTO.rolName);
            }

            // Si ha variado el estado de "activo", bloquear o desbloquear usuario
            if (usuario.Activo != usuarioDTO.Activo)
            {
                if (usuarioDTO.Activo)
                {
                    applicationUser.LockoutEnd = null;
                }
                else
                {
                    applicationUser.LockoutEnd = DateTime.MaxValue;
                }
            }

            // Si ha variado el email, modificarlo para inicio sesion
            if (usuario.Email.Trim().ToLower() != usuarioDTO.Email.Trim().ToLower())
            {
                var usernameResult = await _userManager.SetUserNameAsync(applicationUser, usuarioDTO.Email.Trim().ToLower());

                if (!usernameResult.Succeeded)
                {
                    return Conflict("No se puede modificar el usuario con este email");
                }

                var emailResult = await _userManager.SetEmailAsync(applicationUser, usuarioDTO.Email.Trim().ToLower());

                if (!usernameResult.Succeeded)
                {
                    return Conflict("No se puede modificar el usuario con este email");
                }
            }

            usuario.Nombre = usuarioDTO.Nombre;
            usuario.Apellidos = usuarioDTO.Apellidos;
            usuario.Iniciales = usuarioDTO.Iniciales;
            usuario.Email = usuarioDTO.Email.Trim().ToLower();
            usuario.EsSocio = usuarioDTO.EsSocio;
            usuario.EsResponsableExpediente = usuarioDTO.EsResponsableExpediente;
            usuario.EsResponsableFacturacion = usuarioDTO.EsResponsableFacturacion;
            usuario.EsResponsableComercial = usuarioDTO.EsResponsableComercial;
            usuario.EsCaptadorComisionista = usuarioDTO.EsCaptadorComisionista;
            usuario.Activo = usuarioDTO.Activo;
            usuario.ModificadoFecha = DateTime.Now;
            usuario.ModificadoPor = int.Parse(User.FindFirst(JwtClaimTypes.Id).Value);

            if (usuarioDTO.Departamento.Id != 0)
            {
                usuario.DepartamentoId = usuarioDTO.Departamento.Id;
            }
            else
            {
                usuario.DepartamentoId = null;
            }

            if (usuarioDTO.Categoria.Id != 0)
            {
                usuario.CategoriaId = usuarioDTO.Categoria.Id;
            }
            else
            {
                usuario.CategoriaId = null;
            }

            _context.Attach(usuario).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("asignarRol")]
        public async Task<ActionResult> AsignarRolUsuario(RolDTOuser editarRolDTO)
        {
            var usuario = await _userManager.FindByIdAsync(editarRolDTO.UserId.ToString());
            await _userManager.AddToRoleAsync(usuario, editarRolDTO.RoleNombre);
            return NoContent();
        }

        [HttpPut("removerRol")]
        public async Task<ActionResult> RemoverRolUsuario(RolDTOuser editarRolDTO)
        {
            var usuario = await _userManager.FindByIdAsync(editarRolDTO.UserId.ToString());
            await _userManager.RemoveFromRoleAsync(usuario, editarRolDTO.RoleNombre);
            return NoContent();
        }
    }
}
