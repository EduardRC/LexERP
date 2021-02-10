using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexERP.Server.Models
{
    public class Menu
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }

        [StringLength(50)]
        public string PageName { get; set; }

        [StringLength(50)]
        public string MenuName { get; set; }

        [StringLength(50)]
        public string IconName { get; set; }

        public int Position { get; set; }

        public virtual Menu Parent { get; set; }
    }
}
