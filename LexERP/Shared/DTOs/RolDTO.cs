using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexERP.Shared.DTOs
{
    public class RolDTO
    {
//        public string RoleId { get; set; }
        public int RoleId { get; set; }
        public string Nombre { get; set; }
    }

    public class RolDTOuser
    {
//        public string UserId { get; set; }
        public int UserId { get; set; }
        public string RoleNombre { get; set; }
    }
}
