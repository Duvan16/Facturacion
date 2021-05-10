using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ApiFacturacion.Entities
{
    public partial class FacturaDet
    {
        public Guid Id { get; set; }
        public Guid FacturaEncId { get; set; }
        public Guid ProductoId { get; set; }
        public decimal Cantidad { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal ValorBruto { get; set; }
        public int ValorIva { get; set; }
        public decimal ValorNeto { get; set; }

        public virtual FacturaEnc FacturaEnc { get; set; }
        public virtual Producto Producto { get; set; }
    }
}
