using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexERP.Shared.Entities
{
    public class Persona : _comun
    {
        public Persona()
        {
            Ubicaciones = new HashSet<Ubicacion>();
            Sectores = new HashSet<Sector>();
            DatosContacto = new HashSet<DatoContacto>();
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

        public int? UbicacionPrincipalId { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int IdiomaId { get; set; }
        public string Observaciones { get; set; }

        public virtual Ubicacion UbicacionPrincipal { get; set; }
        public virtual Idioma Idioma { get; set; }

        public virtual ICollection<Ubicacion> Ubicaciones { get; set; }
        public virtual ICollection<DatoContacto> DatosContacto { get; set; }
        public virtual ICollection<Sector> Sectores { get; set; }


        [Display(Name = "Nombre")]
        public string FullName
        {
            get
            {
                return Apellidos + ", " + Nombre;
            }
        }

        [Display(Name = "Nombre")]
        public string FullName2
        {
            get
            {
                return Nombre + " " + Apellidos;
            }
        }

    }

    public enum TipoPersona
    {
        Física, Jurídica
    }
}
