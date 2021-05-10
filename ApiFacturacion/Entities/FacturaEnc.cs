using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ApiFacturacion.Entities
{
    public partial class FacturaEnc
    {
        public FacturaEnc()
        {
            FacturaDet = new HashSet<FacturaDet>();
        }

        public Guid Id { get; set; }
        public int NumeroFactura { get; set; }
        public Guid ClienteId { get; set; }
        public DateTime FechaFactura { get; set; }
        public decimal SubTotal { get; set; }
        public decimal? TotalIva { get; set; }
        public decimal? ValorDescuento { get; set; }
        public decimal ValorTotal { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCrea { get; set; }
        public DateTime FechaCrea { get; set; }
        public string UsuarioActualiza { get; set; }
        public DateTime? FechaActualiza { get; set; }

        public virtual Cliente Cliente { get; set; }
        public virtual ICollection<FacturaDet> FacturaDet { get; set; }
    }
}
