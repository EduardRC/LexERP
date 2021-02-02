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
using LexERP.Shared.Entities;

namespace LexERP.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "admin")]
    public class UsuariosController: ControllerBase
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
        public async Task<ActionResult<List<UsuarioDTOlist>>> Get([FromQuery] PaginacionDTO paginacion)
        {
            var queryable = _context.Usuarios.Where(x=>x.Eliminado==false).AsQueryable();
            await HttpContext.InsertarParametrosPaginacionEnRespuesta(queryable, paginacion.CantidadRegistros);
            return await queryable.Paginar(paginacion)
                .Select(x => new UsuarioDTOlist
            {
                Id = x.Id,
                Nombre = x.FullName,
                Departamento = x.Departamento.Descripcion,
                Categoria = x.Categoria.Descripcion
                }).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioDTO>> Get(int id)
        {
            var usuario = await _context.Usuarios.Where(x => x.Id == id && x.Eliminado==false)
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
                }

                if (usuario.Categoria != null)
                {
                    usuarioDTO.Categoria.Id = usuario.Categoria.Id;
                    usuarioDTO.Categoria.Descripcion = usuario.Categoria.Descripcion;
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
            // renombramos tambien el email para evitar problemas si en el futuro se vuelve a crear con mismo email
            // lo que si borramos es la cuenta que permite el acceso por IdentityServer4

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == id && x.Eliminado == false);

            if (usuario == null) { return NotFound(); }

            var user = _userManager.FindByNameAsync(usuario.Email).Result;
            await _userManager.DeleteAsync(user);

            usuario.Email = "";
            usuario.ModificadoFecha = DateTime.Now;
            usuario.Eliminado = true;

            _context.Attach(usuario).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("roles")]
        public async Task<ActionResult<List<RolDTO>>> GetRoles()
        {
            return await _context.Roles.Select(x => new RolDTO() { RoleId = x.Id, Nombre = x.Name }).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post(UsuarioDTO usuarioDTO)
        {
            // crear usuario en Identity
            var user = new ApplicationUser { UserName = usuarioDTO.Email, Email = usuarioDTO.Email };
            var ir = await _userManager.CreateAsync(user, usuarioDTO.Password);

            if (ir.Succeeded)
            {
                // asignar role
                var createdUser = await _userManager.FindByNameAsync(usuarioDTO.Email);
                await _userManager.AddToRoleAsync(createdUser, usuarioDTO.rolName);

                // crear usuario para gestion
                var usuario = new Usuario
                {
                    Iniciales = usuarioDTO.Iniciales,
                    Nombre = usuarioDTO.Nombre,
                    Apellidos = usuarioDTO.Apellidos,
                    Email = usuarioDTO.Email,
                    EsSocio = usuarioDTO.EsSocio,
                    EsResponsableExpediente = usuarioDTO.EsResponsableExpediente,
                    EsResponsableFacturacion = usuarioDTO.EsResponsableFacturacion,
                    EsResponsableComercial = usuarioDTO.EsResponsableComercial,
                    EsCaptadorComisionista = usuarioDTO.EsCaptadorComisionista,
                    Activo = true,
                    CreadoFecha = DateTime.Now
                };

                if (usuarioDTO.Departamento.Id!=0)
                {
                    usuario.DepartamentoId = usuarioDTO.Departamento.Id;
                }

                if (usuarioDTO.Categoria.Id!=0)
                {
                    usuario.CategoriaId = usuarioDTO.Categoria.Id;
                }
                


                _context.Add(usuario);
                await _context.SaveChangesAsync();

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
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == usuarioDTO.Id && x.Eliminado==false);

            if (usuario == null) { return NotFound(); }

            var user = _userManager.FindByNameAsync(usuarioDTO.Email).Result;
            var roles = await _userManager.GetRolesAsync(user);

            // si hay nueva contraseña, borrar la anterior y asignar la nueva
            if (!string.IsNullOrEmpty(usuarioDTO.Password))
            {
                await _userManager.RemovePasswordAsync(user);
                await _userManager.AddPasswordAsync(user, usuarioDTO.Password);
            }

            // si ha varido el rol, eliminar anteriores y asignar el nuevo
            if (roles.First() != usuarioDTO.rolName)
            {
                await _userManager.RemoveFromRolesAsync(user, roles);
                await _userManager.AddToRoleAsync(user, usuarioDTO.rolName);
            }

            usuario.Nombre = usuarioDTO.Nombre;
            usuario.Apellidos = usuarioDTO.Apellidos;
            usuario.Iniciales = usuarioDTO.Iniciales;
            usuario.EsSocio = usuarioDTO.EsSocio;
            usuario.EsResponsableExpediente = usuarioDTO.EsResponsableExpediente;
            usuario.EsResponsableFacturacion = usuarioDTO.EsResponsableFacturacion;
            usuario.EsResponsableComercial = usuarioDTO.EsResponsableComercial;
            usuario.EsCaptadorComisionista = usuarioDTO.EsCaptadorComisionista;
            usuario.ModificadoFecha = DateTime.Now;

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
            var usuario = await _userManager.FindByIdAsync(editarRolDTO.UserId);
            await _userManager.AddToRoleAsync(usuario, editarRolDTO.RoleNombre);
            return NoContent();
        }

        [HttpPut("removerRol")]
        public async Task<ActionResult> RemoverRolUsuario(RolDTOuser editarRolDTO)
        {
            var usuario = await _userManager.FindByIdAsync(editarRolDTO.UserId);
            await _userManager.RemoveFromRoleAsync(usuario, editarRolDTO.RoleNombre);
            return NoContent();
        }
    }
}
