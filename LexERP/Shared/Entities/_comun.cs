using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexERP.Shared.Entities
{
    public class _comun
    {
        public int CreadoPor { get; set; }
        public DateTime CreadoFecha { get; set; }
        public int? ModificadoPor { get; set; }
        public DateTime? ModificadoFecha { get; set; }
        public bool Eliminado { get; set; }

        public bool Activo { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
