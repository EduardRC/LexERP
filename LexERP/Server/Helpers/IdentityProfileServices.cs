using IdentityModel;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LexERP.Server.Models;
using LexERP.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace LexERP.Server.Helpers
{
    public class IdentityProfileServices : IProfileService
    {
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> _claimsFactory;
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityProfileServices(IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory,
            UserManager<ApplicationUser> userManager)
        {
            _claimsFactory = claimsFactory;
            _userManager = userManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var usuarioId = context.Subject.GetSubjectId();
            var usr = context.Subject.GetDisplayName();
//            var usuario = await _userManager.FindByIdAsync(usuarioId);
            var usuario = await _userManager.FindByNameAsync(usr);
            var claimsPrincipal = await _claimsFactory.CreateAsync(usuario);
            var claims = claimsPrincipal.Claims.ToList();

            var claimsMapeados = new List<Claim>();

            foreach (var claim in claims)
            {
                if (claim.Type == JwtClaimTypes.Role)
                {
                    claimsMapeados.Add(new Claim(ClaimTypes.Role, claim.Value));
                }
            }

            claims.AddRange(claimsMapeados);

            // guardar id del usuario para poder utilizarlo al guardar logs de actividad, 
            // sin tener que consultarlo cada vez a la BD
            claims.Add(new Claim(JwtClaimTypes.Id, usuario.Id.ToString()));

            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var usuarioId = context.Subject.GetSubjectId();
            var usuario = await _userManager.FindByIdAsync(usuarioId);

            context.IsActive = usuario != null;
        }
    }
}
