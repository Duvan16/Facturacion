using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiFacturacion.Models
{
    public class FacturaDTO
    {
        public List<FacturaEncLecturaDTO> EncabezadoFactura { get; set; }
        public List<FacturaDetLecturaDTO> DetalleFactura { get; set; }
    }
}
