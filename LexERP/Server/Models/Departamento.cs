using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexERP.Server.Models
{
    public class Departamento : _comun
    {
        // Mercantil, Laboral, Tax, Procesal

        public Departamento()
        {
            Usuarios = new HashSet<Usuario>();
        }

        public int Id { get; set; }
        
        [StringLength(5)]
        public string Abreviatura { get; set; }

        [StringLength(50)]
        public string Descripcion { get; set; }

        public int Orden { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
