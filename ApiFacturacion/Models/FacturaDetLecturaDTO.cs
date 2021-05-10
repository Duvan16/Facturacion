using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiFacturacion.Models
{
    public class FacturaDetLecturaDTO
    {
        public int NumFactura { get; set; }
        public string Producto { get; set; }
        public decimal Cantidad { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal ValorBruto { get; set; }
        public int ValorIva { get; set; }
        public decimal ValorNeto { get; set; }
    }
}
