using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ApiFacturacion.Entities
{
    public partial class Iva
    {
        public Iva()
        {
            Producto = new HashSet<Producto>();
        }

        public Guid Id { get; set; }
        public string Descripcion { get; set; }
        public int Valor { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCrea { get; set; }
        public DateTime FechaCrea { get; set; }
        public string UsuarioActualiza { get; set; }
        public DateTime? FechaActualiza { get; set; }

        public virtual ICollection<Producto> Producto { get; set; }
    }
}
