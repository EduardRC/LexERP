using LexERP.Server.Data;
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
    public class CategoriasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoriasController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("lista")]
        public async Task<ActionResult<List<CategoriaDTOmin>>> Get()
        {
            return await _context.Categorias
                .Where(x => x.Eliminado == false)
                .OrderBy(x => x.Descripcion)
                .Select(x => new CategoriaDTOmin
                {
                    Id = x.Id,
                    Descripcion = x.Descripcion
                }).ToListAsync();
        }

    }
}
