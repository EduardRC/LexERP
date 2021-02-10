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
    public class MenuController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MenuController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<MenuDTO>>> Get()
        {
            return await _context.Menus
                .Select(x=> new MenuDTO {
                    Id=x.Id, 
                    ParentId=x.ParentId, 
                    PageName=x.PageName, 
                    MenuName=x.MenuName, 
                    IconName=x.IconName, 
                    Position=x.Position })
                .ToListAsync();
        }

    }
}
