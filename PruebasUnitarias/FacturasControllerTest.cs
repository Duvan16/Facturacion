using ApiFacturacion.Controllers;
using ApiFacturacion.Entities;
using ApiFacturacion.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PruebasUnitarias
{
    [TestClass]
    public class FacturasControllerTest : BasePruebas
    {
        private ILogger<FacturasController> GetLogger
        {
            get
            {
                return new Mock<ILogger<FacturasController>>()
                    .Object;
            }
        }

        private IConfiguration GetConfiguration
        {
            get
            {
                return new Mock<IConfiguration>()
                    .Object;
            }
        }

        [TestMethod]
        public async Task CrearFactura()
        {
            // Preparación
            var nombreBD = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreBD);
            var mapper = ConfigurarAutoMapper();

            var guidTipoIdentifiacion = Guid.NewGuid();
            var guidCliente = Guid.NewGuid();
            var guidIva = Guid.NewGuid();
            var guidProducto = Guid.NewGuid();

            contexto.TipoIdentificacion.Add(new TipoIdentificacion()
            {
                Id = guidTipoIdentifiacion,
                Codigo = "CC",
                Descripcion = "Cedula de Ciudadania",
                Estado = true,
                UsuarioCrea = "12312321",
                FechaCrea = DateTime.Now,
            });

            contexto.Cliente.Add(new Cliente()
            {
                Id = guidCliente,
                TipoIdentificacionId = guidTipoIdentifiacion,
                Identificacion = "7485941",
                PrimerNombre = "MARIA",
                SegundoNombre = "CAMILA",
                PrimerApellido = "GOMEZ",
                SegundoApellido = "SALDARRIAGA",
                FechaNacimiento = Convert.ToDateTime("1990-01-05"),
                Telefono = "225415454",
                Celular = "320174861",
                Correo = "maria@outlook.com",
                Estado = true,
                UsuarioCrea = "152418415",
                FechaCrea = DateTime.Now
            });

            contexto.Iva.Add(new Iva()
            {
                Id = guidIva,
                Descripcion = "Iva 19%",
                Valor = 19,
                Estado = true,
                UsuarioCrea = "15222",
                FechaCrea = DateTime.Now
            });

            contexto.Producto.Add(new Producto()
            {
                Id = guidProducto,
                Descripcion = "AUDIFONOS DE DIADEMA",
                Unidad = "UND",
                IvaId = guidIva,
                Precio = 54900,
                Estado = true,
                UsuarioCrea = "12022",
                FechaCrea = DateTime.Now
            });

            await contexto.SaveChangesAsync();
            var factur = new FacturaEncDTO()
            {
                ClienteId = guidCliente,
                UsuarioCrea = "654544",
                Detalle = { new FacturaDetDTO() { Cantidad = 2, ProductoId = guidProducto } }
            };


            FacturasController controller = new FacturasController(contexto, GetLogger, mapper, GetConfiguration);
            var respuesta = await controller.Post(factur);
            var resultado = respuesta as CreatedAtRouteResult;
            Assert.AreEqual(201, resultado.StatusCode);
        }
    }
}
