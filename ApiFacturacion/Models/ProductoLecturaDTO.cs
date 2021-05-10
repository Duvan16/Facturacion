using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiFacturacion.Models
{
    public class ProductoLecturaDTO
    {
        public Guid IdProducto { get; set; }
        public string Descripcion { get; set; }
        public string Unidad { get; set; }
        public string DescripcionIva { get; set; }
        public decimal Precio { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCrea { get; set; }
        public DateTime FechaCrea { get; set; }
        public string UsuarioActualiza { get; set; }
        public DateTime? FechaActualiza { get; set; }
        public decimal Existencia { get; set; }
    }
}
