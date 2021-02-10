using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexERP.Server.Models
{
    public class TipoContacto : _comun
    {
        // trabajo, movil, casa, email particular, email trabajo, linkedin, facebook

        public int Id { get; set; }
        public ClaseTipoContacto Clase { get; set; }

        [StringLength(50)]
        public string Descripcion { get; set; }
    }

    public enum ClaseTipoContacto
    {
        Telefono, Email, RedSocial
    }
}
