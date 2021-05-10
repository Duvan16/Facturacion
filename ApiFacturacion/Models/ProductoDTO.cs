using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiFacturacion.Models
{
    public class ProductoDTO
    {
        public Guid Id { get; set; }
        [Required (ErrorMessage = "El campo Descripción es requerido")]
        [MaxLength(255, ErrorMessage = "El campo Descripción debe tener {255} máximo")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El campo Unidad es requerido")]
        public string Unidad { get; set; }

        [Required(ErrorMessage = "El campo Iva es requerido")]
        public Guid IvaId { get; set; }

        [Required(ErrorMessage = "El campo Precio es requerido")]
        [DataType(DataType.Currency)]
        public decimal Precio { get; set; }
        public bool Estado { get; set; }
        //[Required(ErrorMessage = "El campo Usuario es requerido")]
        public string UsuarioCrea { get; set; }
        public DateTime FechaCrea { get; set; }
        public string UsuarioActualiza { get; set; }
        public DateTime? FechaActualiza { get; set; }

    }
}
