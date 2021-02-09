using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexERP.Shared.Entities
{
    public class MenuRol
    {
        public int MenuId { get; set; }
        public string RolName { get; set; }

        virtual public Menu Menu { get; set; }
    }
}
