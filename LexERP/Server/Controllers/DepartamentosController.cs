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
        public async Task<ActionResult<List<DepartamentoDTO>>> Get([FromQuery] PaginacionDTO paginacion)
        {
            var queryable = _context.Departamentos.OrderBy(x=>x.Orden).AsQueryable();

            await HttpContext.InsertarParametrosPaginacionEnRespuesta(queryable, paginacion.CantidadRegistros);

            return await queryable.Paginar(paginacion).Select(x => new DepartamentoDTO
            {
                Id=x.Id, 
                Abreviatura=x.Abreviatura, 
                Descripcion=x.Descripcion, 
                Orden=x.Orden 
            }).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DepartamentoDTO>> Get(int id)
        {
            var departamento = await _context.Departamentos.Where(x => x.Id == id && x.Eliminado==false)
                .FirstOrDefaultAsync();

            if (departamento == null) { return NotFound(); }

            return new DepartamentoDTO
            { 
                Id = departamento.Id,
                Abreviatura = departamento.Abreviatura,
                Descripcion = departamento.Descripcion,
                Orden = departamento.Orden
            };
        }

        [HttpGet("lista")]
        public async Task<ActionResult<List<DepartamentoDTOmin>>> Get()
        {
            return await _context.Departamentos
                .Where(x=>x.Eliminado==false)
                .OrderBy(x => x.Orden)
                .Select(x=> new DepartamentoDTOmin
                {
                    Id = x.Id,
                    Abreviatura = x.Abreviatura,
                    Descripcion = x.Descripcion
                }).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(DepartamentoDTO departamentoDTO)
        {
            var departamento = new Departamento
            {
                Abreviatura = departamentoDTO.Abreviatura,
                Descripcion = departamentoDTO.Descripcion,
                Orden = departamentoDTO.Orden
            };

            _context.Add(departamento);
            await _context.SaveChangesAsync();

            return departamento.Id;
        }

        [HttpPut]
        public async Task<ActionResult> Put(DepartamentoDTO departamentoDTO)
        {
            var departamento = await _context.Departamentos.FirstOrDefaultAsync(x => x.Id == departamentoDTO.Id && x.Eliminado==false);

            if (departamento == null) { return NotFound(); }

            departamento.Abreviatura = departamentoDTO.Abreviatura;
            departamento.Descripcion = departamentoDTO.Descripcion;
            departamento.Orden = departamentoDTO.Orden;

            _context.Attach(departamento).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var departamento = await _context.Departamentos.FirstOrDefaultAsync(x => x.Id == id && x.Eliminado == false);

            if (departamento == null) { return NotFound(); }

            departamento.Eliminado = true;

            _context.Attach(departamento).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
