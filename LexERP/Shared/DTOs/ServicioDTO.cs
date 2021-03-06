﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexERP.Shared.DTOs
{
    public class ServicioDTO
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
    }
}
