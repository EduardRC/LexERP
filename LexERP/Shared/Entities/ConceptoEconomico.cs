using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexERP.Shared.Entities
{
    public class ConceptoEconomico: _comun
    {
        public ConceptoEconomico()
        {
            Children = new HashSet<ConceptoEconomico>();
            Actuaciones = new HashSet<Actuacion>();
        }

        public int Id { get; set; }
        
        public int EmpresaId { get; set; }
        public int? ClienteId { get; set; }
        public DateTime Fecha { get; set; }
        public TipoConceptoEconomico TipoConceptoEconomico { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Importe { get; set; }

        public string Texto { get; set; }
        public string Observaciones { get; set; }
        public int? ProveedorId { get; set; }
        public bool Facturable { get; set; }

        [Column(TypeName = "decimal(4,2)")]
        public decimal ImpuestoTPC { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal ImpuestoImporte { get; set; }

        [Column(TypeName = "decimal(4,2)")]
        public decimal IRPFTPC { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal IRPFImporte { get; set; }

        public int? FacturaId { get; set; }
        public int? ParentId { get; set; }

        public virtual Empresa Empresa { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual Proveedor Proveedor { get; set; }
        public virtual Factura Factura { get; set; }
        public virtual ConceptoEconomico Parent { get; set; }

        public virtual ICollection<ConceptoEconomico> Children { get; set; }
        public virtual ICollection<Actuacion> Actuaciones { get; set; }
    }

    public enum TipoConceptoEconomico
    {
        Gasto, Suplido, Honorario, ProvisionFondos
    }
}
