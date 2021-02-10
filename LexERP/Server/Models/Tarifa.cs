using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexERP.Server.Models
{
    public class Tarifa : _comun
    {
        // Standard, Internacional, Cliente X, ...

        public int Id { get; set; }

        public int EmpresaId { get; set; }

        [StringLength(5)]
        public string Abreviatura { get; set; }

        [StringLength(50)]
        public string Descripcion { get; set; }

        public string Observaciones { get; set; }
        public DateTime Fecha { get; set; }
        public bool Predeterminada { get; set; }

        public virtual Empresa Empresa { get; set; }

    }
}
