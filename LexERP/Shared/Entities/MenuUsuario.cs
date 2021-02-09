using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexERP.Shared.Entities
{
    public class MenuUsuario
    {
        public int MenuId { get; set; }
        public int UsuarioId { get; set; }

        virtual public Menu Menu { get; set; }
        virtual public Usuario Usuario { get; set; }
    }
}
