using ApiFacturacion.Entities;
using ApiFacturacion.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiFacturacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly BdFacturacionContext context;
        private readonly IMapper mapper;
        private readonly IConfiguration _configuration;
        private readonly ILogger<ProductosController> logger;

        public ProductosController(BdFacturacionContext context, ILogger<ProductosController> logger, IMapper mapper, IConfiguration _configuration)
        {
            this.context = context;
            this.mapper = mapper;
            this._configuration = _configuration;
            this.logger = logger;
        }

        [HttpGet("{Descripcion}", Name = "ConsultaProducto")]
        public  ActionResult<List<ProductoLecturaDTO>> Get(string Descripcion)
        {
            var producto = (from prod in context.Producto
                              join iv in context.Iva
                                    on prod.IvaId equals iv.Id
                              join exis in context.Existencia
                                   on prod.Id equals exis.ProductoId
                            where prod.Descripcion.Contains(Descripcion)
                            select new ProductoLecturaDTO
                            {
                                  IdProducto = prod.Id,
                                 Descripcion = prod.Descripcion,
                                 Unidad = prod.Unidad,
                                 DescripcionIva = iv.Descripcion,
                                 Precio = prod.Precio,
                                 Estado = prod.Estado,
                                 UsuarioCrea = prod.UsuarioCrea,
                                 FechaCrea = prod.FechaCrea,
                                 UsuarioActualiza = prod.UsuarioActualiza,
                                 FechaActualiza = prod.FechaActualiza,
                                 Existencia = exis.Cantidad
                              }).ToList();

            if (producto.Count==0)
            {
                return NotFound();
            }
            else
            {
                return producto;
            }
        }

        [HttpPost(Name = "CrearProducto")]
        public async Task<ActionResult> Post([FromBody] ProductoDTO productoDTO)
        {
            var producto = mapper.Map<Producto>(productoDTO);
            producto.Descripcion = productoDTO.Descripcion.ToUpper();
            producto.FechaCrea = DateTime.Now;
            producto.Estado = true;
            context.Producto.Add(producto);
            await context.SaveChangesAsync();
            return new CreatedAtRouteResult("ProductoCreado", new { producto });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Guid id, [FromBody] ProductoDTO productoDTO)
        {
            if (id != productoDTO.Id)
            {
                return BadRequest();
            }

            var producto = mapper.Map<Producto>(productoDTO);
            producto.Descripcion = productoDTO.Descripcion.ToUpper();
            producto.FechaActualiza = DateTime.Now;
            context.Entry(producto).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var producto = context.Producto.FirstOrDefault(x => x.Id == id);

            if (producto == null)
            {
                return NotFound();
            }

            if (ValidarPermitirEliminar(id))
            {
                context.Producto.Remove(producto);
                await context.SaveChangesAsync();
                return Ok();
            }
            else {
                return BadRequest();
            }
        }

        private bool ValidarPermitirEliminar(Guid ProductoId)
        {
            var existencia = context.Existencia.FirstOrDefault(x => x.ProductoId == ProductoId);
            var facturas = context.FacturaDet.FirstOrDefault(x => x.ProductoId == ProductoId);

            //Si no tiene existencias ni facturas se permite eliminar
            if (existencia==null && facturas==null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


    }
}
