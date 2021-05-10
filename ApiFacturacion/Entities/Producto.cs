using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ApiFacturacion.Entities
{
    public partial class Producto
    {
        public Producto()
        {
            Existencia = new HashSet<Existencia>();
            FacturaDet = new HashSet<FacturaDet>();
        }

        public Guid Id { get; set; }
        public string Descripcion { get; set; }
        public string Unidad { get; set; }
        public Guid IvaId { get; set; }
        public decimal Precio { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCrea { get; set; }
        public DateTime FechaCrea { get; set; }
        public string UsuarioActualiza { get; set; }
        public DateTime? FechaActualiza { get; set; }

        public virtual Iva Iva { get; set; }
        public virtual ICollection<Existencia> Existencia { get; set; }
        public virtual ICollection<FacturaDet> FacturaDet { get; set; }
    }
}
