using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexERP.Shared.Entities
{
    public class TipoContrato: _comun
    {
        // Cuota, Presupuesto, Por Horas, % Por Exito, Fee+Exito

        public int Id { get; set; }

        [StringLength(10)]
        public string Abreviatura { get; set; }

        [StringLength(50)]
        public string Descripcion { get; set; }
    }
}
