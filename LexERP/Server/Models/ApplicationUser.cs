using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LexERP.Server.Models
{
    public class ApplicationUser : IdentityUser<int>
    {
        public ApplicationUser()
        {
        }

        public ApplicationUser(string userName) : base(userName)
        {
        }

        [StringLength(5)]
        public string Iniciales { get; set; }

        [StringLength(80)]
        public string Nombre { get; set; }

        [StringLength(80)]
        public string Apellidos { get; set; }

        public int? DepartamentoId { get; set; }
        public int? CategoriaId { get; set; }

        public bool EsSocio { get; set; }
        public bool EsResponsableComercial { get; set; }
        public bool EsResponsableExpediente { get; set; }
        public bool EsResponsableFacturacion { get; set; }
        public bool EsCaptadorComisionista { get; set; }

        [Display(Name = "Nombre")]
        public string FullName
        {
            get
            {
                return Apellidos + ", " + Nombre;
            }
        }

        [Display(Name = "Nombre")]
        public string FullName2
        {
            get
            {
                return Nombre + " " + Apellidos;
            }
        }

        public virtual Departamento Departamento { get; set; }
        public virtual Categoria Categoria { get; set; }


        public int CreadoPor { get; set; }
        public DateTime CreadoFecha { get; set; }
        public int? ModificadoPor { get; set; }
        public DateTime? ModificadoFecha { get; set; }
        public bool Eliminado { get; set; }

        public bool Activo { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
