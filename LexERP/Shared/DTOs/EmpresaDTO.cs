﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexERP.Shared.DTOs
{
    public class EmpresaDTO
    {
        public int Id { get; set; }

        [StringLength(5)]
        public string Abreviatura { get; set; }

        [StringLength(80)]
        public string RazonSocial { get; set; }

        [StringLength(15)]
        public string NIF { get; set; }

        [StringLength(80)]
        public string Direccion { get; set; }

        [StringLength(80)]
        public string Poblacion { get; set; }

        [StringLength(10)]
        public string CodigoPostal { get; set; }

        [StringLength(80)]
        public string Provincia { get; set; }

        public string Observaciones { get; set; }
        public bool Activo { get; set; }
    }

    public class EmpresaDTOlist
    {
        public int Id { get; set; }
        [StringLength(5)]
        public string Abreviatura { get; set; }
        [StringLength(80)]
        public string RazonSocial { get; set; }
        public bool Activo { get; set; }
    }
}
