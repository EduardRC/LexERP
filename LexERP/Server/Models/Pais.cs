using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexERP.Server.Models
{
    public class Pais : _comun
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Nombre { get; set; }

        public int IdiomaId { get; set; }

        public virtual Idioma Idioma { get; set; }
    }
}
