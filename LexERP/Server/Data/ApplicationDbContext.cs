using IdentityServer4.EntityFramework.Options;
using LexERP.Server.Models;
using LexERP.Shared.Entities;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LexERP.Server.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<MenuRol>().HasKey(c => new { c.MenuId, c.RolName });
            builder.Entity<MenuUsuario>().HasKey(c => new { c.MenuId, c.UsuarioId });

            base.OnModelCreating(builder);
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }

        public DbSet<Area> Areas { get; set; }
        public DbSet<Idioma> Idiomas { get; set; }
        public DbSet<Pais> Paises { get; set; }
        public DbSet<TipoContacto> TipoContactos { get; set; }
        public DbSet<Servicio> Servicios { get; set; }
        public DbSet<TipoContrato> TipoContratos { get; set; }
        public DbSet<FormaDePago> FormasDePago { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<UsuarioLog> UsuarioLogs { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }

        public DbSet<Persona> Personas { get; set; }
        public DbSet<DatoContacto> DatosContacto { get; set; }
        public DbSet<Sector> Sectores { get; set; }
        public DbSet<Ubicacion> Ubicaciones { get; set; }

        public DbSet<Tarifa> Tarifas { get; set; }
        public DbSet<TarifaDetalle> TarifaDetalles { get; set; }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Contacto> Contactos { get; set; }
        public DbSet<Contrato> Contratos { get; set; }
        public DbSet<Expediente> Expedientes { get; set; }

        public DbSet<Factura> Facturas { get; set; }
        public DbSet<ConceptoEconomico> ConceptosEconomicos { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Actuacion> Actuaciones { get; set; }
        public DbSet<TipoActuacion> TipoActuaciones { get; set; }

        public DbSet<Empresa> Empresas { get; set; }

        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuRol> MenuRoles { get; set; }
        public DbSet<MenuUsuario> MenuUsuarios { get; set; }
    }
}
