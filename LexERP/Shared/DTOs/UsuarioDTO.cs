﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexERP.Shared.DTOs
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
        [StringLength(5)]
        public string Iniciales { get; set; }
        [StringLength(80)]
        public string Nombre { get; set; }
        [StringLength(80)]
        public string Apellidos { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        public string Password { get; set; }

        public string rolName { get; set; }

        public DepartamentoDTOmin Departamento { get; set; }
        public CategoriaDTOmin Categoria { get; set; }

        public bool EsSocio { get; set; }
        public bool EsResponsableComercial { get; set; }
        public bool EsResponsableExpediente { get; set; }
        public bool EsResponsableFacturacion { get; set; }
        public bool EsCaptadorComisionista { get; set; }

        public bool Activo { get; set; }

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
    }

    public class UsuarioDTOlist
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Departamento { get; set; }
        public string Categoria { get; set; }
        public bool Activo { get; set; }
    }

    public class UsuarioDTOmin
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }
    }
}
