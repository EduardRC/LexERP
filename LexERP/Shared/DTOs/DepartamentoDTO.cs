using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexERP.Shared.DTOs
{
    public class DepartamentoDTO
    {
        // Mercantil, Laboral, Tax, Procesal

        public DepartamentoDTO()
        {
            //Usuarios = new HashSet<Usuario>();
        }

        public int Id { get; set; }

        [StringLength(5)]
        public string Abreviatura { get; set; }

        [StringLength(50)]
        public string Descripcion { get; set; }

        public int Orden { get; set; }

        public bool Activo { get; set; }

        //public virtual ICollection<Usuario> Usuarios { get; set; }
    }

    public class DepartamentoDTOmin
    {
        public int Id { get; set; }
        public string Abreviatura { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
    }

}
