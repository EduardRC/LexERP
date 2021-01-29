using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexERP.Shared.Entities
{
    public class Area: _comun
    {
        // Mercantil, IP, M&A, Tax, Laboral, Judicial, Seguros, ...

        public Area()
        {
            Children = new HashSet<Area>();
        }

        public int Id { get; set; }

        [StringLength(5)]
        public string Abreviatura { get; set; }
        
        [StringLength(50)]
        public string Descripcion { get; set; }
        
        public int Orden { get; set; }
        public int DepartamentoId { get; set; }
        public int? ParentId { get; set; }

        public virtual Departamento Departamento { get; set; }
        public virtual Area Parent { get; set; }
        public virtual ICollection<Area> Children { get; set; }
    }
}
