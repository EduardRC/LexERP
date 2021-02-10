using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexERP.Server.Models
{
    public class Factura: _comun
    {
        public Factura()
        {
            LineasFactura = new HashSet<ConceptoEconomico>();
        }

        public int Id { get; set; }
        public Guid IdPublica { get; set; }

        public int EmpresaId { get; set; }
        public string Serie { get; set; }
        public int Ejercicio { get; set; }
        public int Numero { get; set; }
        public DateTime Fecha { get; set; }

        public bool Rectificativa { get; set; }
        public bool Rectificada { get; set; }
        public int? FacturaRectificadaId { get; set; }
        public string MotivoRectificada { get; set; }

        public int? ClienteId { get; set; }
        public string Documento { get; set; }
        public string Nombre { get; set; }
        [StringLength(80)]
        public string Direccion { get; set; }
        [StringLength(80)]
        public string Poblacion { get; set; }
        [StringLength(10)]
        public string CodigoPostal { get; set; }
        [StringLength(80)]
        public string Provincia { get; set; }
        public int PaisId { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal TotalCobrada { get; set; }
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal TotalHonorarios { get; set; }
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal TotalSuplidos { get; set; }
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal TotalGastos { get; set; }
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal TotalProvisionesRecibidas { get; set; }
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal TotalProvisionesDevueltas { get; set; }

        public string Observaciones { get; set; }

        public decimal TotalFactura
        {
            get
            {
                return TotalHonorarios + TotalSuplidos + TotalGastos;
            }
        }

        public virtual Empresa Empresa { get; set; }
        public virtual Factura FacturaRectificada { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual Pais Pais { get; set; }

        public virtual ICollection<ConceptoEconomico> LineasFactura { get; set; }
    }
}
