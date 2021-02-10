using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexERP.Server.Models
{
    public class Cliente: _comun
    {
        public Cliente()
        {
            Children = new HashSet<Cliente>();
            Servicios = new HashSet<Servicio>();
            Contactos = new HashSet<Persona>();
            Contratos = new HashSet<Contrato>();
            Facturas = new HashSet<Factura>();
        }

        public int Id { get; set; }
        public Guid IdPublico { get; set; }

        public int EmpresaId { get; set; }
        public int PersonaId { get; set; }
        
        [StringLength(10)]
        public string Abreviatura { get; set; }

        [StringLength(25)]
        public string Codigo { get; set; }

        [StringLength(25)]
        public string CodigoAlternativo { get; set; }

        [StringLength(80)]
        public string NombreComercial { get; set; }

        public int? ResponsableComercialId { get; set; }
        public int? ResponsableFacturacionId { get; set; }
        public int? CaptadorId { get; set; }
        public int? TarifaId { get; set; }
        public bool AplicarIVA { get; set; }
        public bool AplicarRetencion { get; set; }
        public int Empleados { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal ValorFacturacion { get; set; }

        public int? ParentId { get; set; }

        public virtual Empresa Empresa { get; set; }
        public virtual Persona Persona { get; set; }
        public virtual ApplicationUser ResponsableComercial { get; set; }
        public virtual ApplicationUser ResponsableFacturacion { get; set; }
        public virtual ApplicationUser Captador { get; set; }
        public virtual Tarifa Tarifa { get; set; }
        public virtual Cliente Parent { get; set; }

        public virtual ICollection<Cliente> Children { get; set; }
        public virtual ICollection<Servicio> Servicios { get; set; }
        public virtual ICollection<Persona> Contactos { get; set; }
        public virtual ICollection<Contrato> Contratos { get; set; }
        public virtual ICollection<Factura> Facturas { get; set; }
    }
}
