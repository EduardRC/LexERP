using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexERP.Shared.DTOs
{
    public class EmpresaDTO
    {
        public int Id { get; set; }
        public string Abreviatura { get; set; }
        public string RazonSocial { get; set; }
        public string NIF { get; set; }
        public string Direccion { get; set; }
        public string Poblacion { get; set; }
        public string CodigoPostal { get; set; }
        public string Provincia { get; set; }
        public string Observaciones { get; set; }
        public bool Activo { get; set; }
    }

    public class EmpresaDTOlist
    {
        public int Id { get; set; }
        public string Abreviatura { get; set; }
        public string RazonSocial { get; set; }
        public bool Activo { get; set; }
    }
}
