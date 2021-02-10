using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexERP.Server.Models
{
    public class UsuarioLog
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public DateTime Fecha { get; set; }
        public TipoLog Tipo { get; set; }

        [StringLength(80)]
        public string Info { get; set; }

        public virtual Usuario Usuario { get; set; }
    }

    public enum TipoLog
    {
        Login, Logout
    }
}
