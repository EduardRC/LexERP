using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexERP.Shared.DTOs
{
    public class MenuDTO
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string PageName { get; set; }
        public string MenuName { get; set; }
        public string IconName { get; set; }
        public int Position { get; set; }
    }
}
