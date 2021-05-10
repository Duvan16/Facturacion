using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiFacturacion.Models
{
    public class FacturaEncDTO
    {
        [Required]
        public Guid ClienteId { get; set; }

        public DateTime FechaFactura { get; set; }
        public decimal SubTotal { get; set; }
        public decimal? TotalIva { get; set; }
        public decimal? ValorDescuento { get; set; }
        public decimal ValorTotal { get; set; }
        public bool Estado { get; set; }

        [Required]
        public string UsuarioCrea { get; set; }
        public DateTime FechaCrea { get; set; }

        [Required]
        public List<FacturaDetDTO> Detalle { get; set; }

    }
}
