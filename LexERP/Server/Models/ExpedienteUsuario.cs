using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LexERP.Server.Models
{
    public class ExpedienteUsuario
    {
        public int ExpedienteId { get; set; }
        public int UsuarioId { get; set; }

        public virtual Expediente Expediente { get; set; }
        public virtual ApplicationUser Usuario { get; set; }
    }
}
