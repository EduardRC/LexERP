using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexERP.Server.Models
{
    public class TipoActuacion: _comun
    {
        public int Id { get; set; }
        
        [StringLength(10)]
        public string Abreviatura { get; set; }

        [StringLength(50)]
        public string Descripcion { get; set; }

        public string TextoActuacion { get; set; }
    }
}
