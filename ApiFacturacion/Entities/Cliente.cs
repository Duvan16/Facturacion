using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ApiFacturacion.Entities
{
    public partial class Cliente
    {
        public Cliente()
        {
            FacturaEnc = new HashSet<FacturaEnc>();
        }

        public Guid Id { get; set; }
        public Guid TipoIdentificacionId { get; set; }
        public string Identificacion { get; set; }
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string RazonSocial { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string Correo { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCrea { get; set; }
        public DateTime FechaCrea { get; set; }
        public string UsuarioActualiza { get; set; }
        public DateTime? FechaActualiza { get; set; }

        public virtual TipoIdentificacion TipoIdentificacion { get; set; }
        public virtual ICollection<FacturaEnc> FacturaEnc { get; set; }
    }
}
