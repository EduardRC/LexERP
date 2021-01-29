using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexERP.Shared.Entities
{
    public class Expediente: _comun
    {
        public Expediente()
        {
            Children = new HashSet<Expediente>();
            Usuarios = new HashSet<Usuario>();
        }

        public int Id { get; set; }
        public Guid IdPublico { get; set; }

        public int? EmpresaId { get; set; }  // se podria acceder desde contrato->cliente, pero asi las consultas pueden ser mas eficientes
        public int? ClienteId { get; set; }  // se podria acceder desde contrato, pero asi las consultas pueden ser mas eficientes
        public int ContratoId { get; set; }
        public int TipoExpedienteId { get; set; }

        public int? ResponsableId { get; set; }
        public int? SocioResponsableId { get; set; }
        public int? ResponsableComercialId { get; set; }
        public int? ResponsableFacturacionId { get; set; }

        [StringLength(25)]
        public string Referencia { get; set; }
        
        [StringLength(25)]
        public string ReferenciaCliente { get; set; }

        public DateTime FechaAlta { get; set; }
        public DateTime FechaCierre { get; set; }

        [StringLength(80)]
        public string Descripcion { get; set; }
        
        public string Observaciones { get; set; }
        
        [StringLength(50)]
        public string Ubicacion { get; set; }

        public bool Facturable { get; set; }

        public int AreaId { get; set; }
        public int? TarifaId { get; set; }

        public int EstimacionHoras { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal EstimacionImporte { get; set; }

        public bool AlarmaSobrepasoEstimacion { get; set; }

        public int? ParentId { get; set; }

        public bool Confidencial { get; set; } // si esta activo, solo pueden ver el exp los usuarios asignados
        public virtual ICollection<Usuario> Usuarios { get; set; }  // usuarios asignados

        public virtual Expediente Parent { get; set; }
        public virtual ICollection<Expediente> Children { get; set; }

        public virtual Empresa Empresa { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual Contrato Contrato { get; set; }
        public virtual TipoExpediente TipoExpediente { get; set; }
        public virtual Usuario Responsable { get; set; }
        public virtual Usuario SocioResponsable { get; set; }
        public virtual Usuario ResponsableComercial { get; set; }
        public virtual Usuario ResponsableFacturacion { get; set; }
        public virtual Area Area { get; set; }
        public virtual Tarifa Tarifa { get; set; }

    }
}
