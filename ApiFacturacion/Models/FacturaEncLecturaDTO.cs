using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiFacturacion.Models
{
    public class FacturaEncLecturaDTO
    {
        public string Cliente { get; set; }
        public int NumFactura { get; set; }

        public DateTime FechaFactura { get; set; }
        public decimal SubTotal { get; set; }
        public decimal? TotalIva { get; set; }
        public decimal? TotalDescuento { get; set; }
        public decimal ValorTotal { get; set; }
        public bool EstadoFactura { get; set; }
        public string UsuarioCrea { get; set; }
        public string UsuarioActualiza { get; set; }
        public DateTime? FechaActualiza { get; set; }
    }
}
