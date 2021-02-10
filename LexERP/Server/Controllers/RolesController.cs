using IdentityModel;
using LexERP.Server.Data;
using LexERP.Server.Helpers;
using LexERP.Server.Models;
using LexERP.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
    [Authorize(Roles = "admin")]
    public class RolesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public RolesController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<List<RolDTO>> Get([FromQuery] ParametrosBusquedaSeleccion parametrosBusqueda)
        {
            var queryable = _context.Roles.AsQueryable();

            if (!string.IsNullOrWhiteSpace(parametrosBusqueda.Buscar))
            {
                var searchString = parametrosBusqueda.Buscar.ToLower();

                queryable = queryable
                    .Where(x => x.Name.ToLower().Contains(searchString));
            }

            if (!string.IsNullOrWhiteSpace(parametrosBusqueda.Orden))
            {
                switch (parametrosBusqueda.Orden.ToLower())
                {
                    case "nombre_desc":
                        queryable = queryable.OrderByDescending(s => s.Name);
                        break;
                    default:
                        queryable = queryable.OrderBy(s => s.Name);
                        break;
                }
            }

            await HttpContext.InsertarParametrosPaginacionEnRespuesta(queryable, parametrosBusqueda.Paginacion.CantidadRegistros);

            return await queryable.Paginar(parametrosBusqueda.Paginacion)
                .Select(x => new RolDTO
                {
                    RoleId = x.Id,
                    Nombre = x.Name
                }).ToListAsync();
        }

        [HttpGet("edit/{id}")]
//        public async Task<ActionResult<RolDTO>> Get(string id)
        public async Task<ActionResult<RolDTO>> Get(int id)
        {
            var rol = await _context.Roles.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (rol == null) { return NotFound(); }

            try
            {
                var rolDTO = new RolDTO
                {
                    RoleId = rol.Id,
                    Nombre = rol.Name
                };

                return rolDTO;
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpGet("lista")]
        public async Task<ActionResult<List<RolDTO>>> GetRoles()
        {
            return await _context.Roles.Select(x => new RolDTO() { RoleId = x.Id, Nombre = x.Name }).OrderBy(x=>x.Nombre).ToListAsync();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> Post(RolDTO rolDTO)
        {
            var role = new ApplicationRole(rolDTO.Nombre);
            var ir = await _roleManager.CreateAsync(role);

            if (ir.Succeeded)
            {
                return NoContent();
            }
            else
            {
                var exception = new ApplicationException($"Role `{rolDTO.Nombre}` cannot be created");
                throw exception;
            }
        }

        [HttpPut]
        public async Task<ActionResult> Put(RolDTO rolDTO)
        {
            var rol = await _roleManager.FindByIdAsync(rolDTO.RoleId.ToString());

            rol.Name = rolDTO.Nombre;
            await _roleManager.UpdateAsync(rol);

            return NoContent();
        }

    }
}
