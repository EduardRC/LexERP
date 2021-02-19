using LexERP.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexERP.Shared.DTOs
{
    public class PersonaDTO
    {
        public PersonaDTO()
        {
            UbicacionPrincipal = new UbicacionDTO();
            Ubicaciones = new List<UbicacionDTO>();
            DatosContacto = new List<DatoContactoDTO>();
            Sectores = new List<SectorDTO>();
            Idioma = new IdiomaDTO();
        }

        public int Id { get; set; }
        public TipoPersona Tipo { get; set; }

        [StringLength(15)]
        public string Documento { get; set; }

        [StringLength(5)]
        public string Saludo { get; set; }

        [StringLength(80)]
        public string Nombre { get; set; }

        [StringLength(80)]
        public string Apellidos { get; set; }

        [StringLength(80)]
        public string Cargo { get; set; }

        public UbicacionDTO UbicacionPrincipal { get; set; }
        public IdiomaDTO Idioma { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Observaciones { get; set; }

        public List<UbicacionDTO> Ubicaciones { get; set; }
        public List<DatoContactoDTO> DatosContacto { get; set; }
        public List<SectorDTO> Sectores { get; set; }

        [Display(Name = "Nombre")]
        public string FullName
        {
            get
            {
                if (Apellidos != "" && Nombre != "")
                {
                    return Apellidos + ", " + Nombre;
                }
                return Apellidos + Nombre;
            }
        }

        [Display(Name = "Nombre")]
        public string FullName2
        {
            get
            {
                return (Nombre + " " + Apellidos).Trim();
            }
        }

        public bool Activo { get; set; }
    }

    public class PersonaDTOmin
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Cargo { get; set; }
        public bool Activo { get; set; }
    }
}
