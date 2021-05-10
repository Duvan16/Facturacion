using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiFacturacion.Models
{
    public class FacturaDetDTO
    {
        public Guid FacturaEncId { get; set; }
        [Required]
        public Guid ProductoId { get; set; }
        [Required]
        public decimal Cantidad { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal ValorBruto { get; set; }
        public int ValorIva { get; set; }
        public decimal ValorNeto { get; set; }
    }
}
