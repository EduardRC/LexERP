using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexERP.Shared.DTOs
{
    public class ClienteDTO
    {
        public ClienteDTO()
        {
            Persona = new PersonaDTO();
            ResponsableComercial = new UsuarioDTOmin();
            ResponsableFacturacion = new UsuarioDTOmin();
            Captador = new UsuarioDTOmin();
            Parent = new ClienteDTOmin();
            Children = new List<ClienteDTOmin>();
            Servicios = new List<ServicioDTO>();
            Contactos = new List<PersonaDTO>();
            Contratos = new List<ContratoDTO>();
        }

        public int Id { get; set; }
        public Guid IdPublico { get; set; }

        public int EmpresaId { get; set; }
        public PersonaDTO Persona { get; set; }

        [StringLength(10)]
        public string Abreviatura { get; set; }

        [StringLength(25)]
        public string Codigo { get; set; }

        [StringLength(25)]
        public string CodigoAlternativo { get; set; }

        [StringLength(80)]
        public string NombreComercial { get; set; }

        public UsuarioDTOmin ResponsableComercial { get; set; }
        public UsuarioDTOmin ResponsableFacturacion { get; set; }
        public UsuarioDTOmin Captador { get; set; }
        public TarifaDTO Tarifa { get; set; }

        public bool AplicarIVA { get; set; }
        public bool AplicarRetencion { get; set; }
        public int Empleados { get; set; }

        [DataType(DataType.Currency)]
        public decimal ValorFacturacion { get; set; }

        public ClienteDTOmin Parent { get; set; }

        public List<ClienteDTOmin> Children { get; set; }
        public List<ServicioDTO> Servicios { get; set; }
        public List<PersonaDTO> Contactos { get; set; }
        public List<ContratoDTO> Contratos { get; set; }
        //public virtual ICollection<Factura> Facturas { get; set; }

        public bool Activo { get; set; }
    }

    public class ClienteDTOmin
    {
        public int Id { get; set; }
        public Guid IdPublico { get; set; }
        public string Abreviatura { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }
    }

}
