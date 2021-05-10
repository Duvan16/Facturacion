using ApiFacturacion.Entities;
using ApiFacturacion.Models;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
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
    public class FacturasController : ControllerBase
    {
        private readonly BdFacturacionContext context;
        private readonly IMapper mapper;
        private readonly IConfiguration _configuration;
        private readonly ILogger<FacturasController> logger;

        public FacturasController(BdFacturacionContext context, ILogger<FacturasController> logger, IMapper mapper, IConfiguration _configuration)
        {
            this.context = context;
            this.mapper = mapper;
            this._configuration = _configuration;
            this.logger = logger;
        }

        [HttpPost(Name = "CrearFactura")]
        public async Task<ActionResult> Post([FromBody] FacturaEncDTO facturaEncDTO)
        {
            //1. Validar Existencia Productos Detalle
            if (await ValidarExistencia(facturaEncDTO.Detalle))
            {
                //2. Registrar Encabezado
                facturaEncDTO.FechaFactura = DateTime.Now;
                facturaEncDTO.SubTotal = 0;
                facturaEncDTO.TotalIva = 0;
                facturaEncDTO.ValorDescuento = 0;
                facturaEncDTO.ValorTotal = 0;
                facturaEncDTO.Estado = true;
                facturaEncDTO.FechaCrea = DateTime.Now;

                var facturaEnc = mapper.Map<FacturaEnc>(facturaEncDTO);
                context.FacturaEnc.Add(facturaEnc);
                await context.SaveChangesAsync();
                var IdEnc = facturaEnc.Id;

                //3. Registrar Detalle
                foreach (var det in facturaEncDTO.Detalle)
                {
                    var facturaDet = mapper.Map<FacturaDet>(det);

                    facturaDet.FacturaEncId = IdEnc;

                    //3.1 Consutar Iva y Precio Producto
                    var infoProducto = (from prod in context.Producto
                                        join iv in context.Iva
                                                  on prod.IvaId equals iv.Id
                                        where prod.Id == facturaDet.ProductoId
                                        select new
                                        {
                                            precio = prod.Precio
                                            ,
                                            iva = iv.Valor
                                        }).FirstOrDefault();

                    facturaDet.ValorUnitario = infoProducto.precio;
                    facturaDet.ValorBruto = infoProducto.precio * facturaDet.Cantidad;
                    facturaDet.ValorIva = infoProducto.iva;
                    Decimal ivaPorc = (decimal)infoProducto.iva / 100;
                    Decimal IvaProducto = infoProducto.precio * (ivaPorc + 1);
                    facturaDet.ValorNeto = IvaProducto * facturaDet.Cantidad;
                    context.FacturaDet.Add(facturaDet);

                    //3.2 Actualizar Totales Encabezado
                    facturaEnc.SubTotal += facturaDet.ValorBruto;
                    Decimal ValorIva = infoProducto.precio * ivaPorc;
                    facturaEnc.TotalIva += ValorIva * facturaDet.Cantidad;
                    facturaEnc.ValorTotal += facturaDet.ValorNeto;
                    context.FacturaEnc.Attach(facturaEnc);
                    context.Entry(facturaEnc).Property(x => x.SubTotal).IsModified = true;
                    context.Entry(facturaEnc).Property(x => x.TotalIva).IsModified = true;
                    context.Entry(facturaEnc).Property(x => x.ValorTotal).IsModified = true;


                    //3.4 Actualiza Existencia
                    var existenciaId = await context.Existencia
                   .Where(x => x.ProductoId == det.ProductoId
                               )
                   .Select(x => new { x.Id, x.Cantidad }).FirstOrDefaultAsync();

                    Existencia existenciaUpdate = new Existencia();
                    existenciaUpdate.Id = existenciaId.Id;
                    existenciaUpdate.Cantidad = existenciaId.Cantidad-det.Cantidad;
                    existenciaUpdate.FechaActualiza = DateTime.Now;
                    existenciaUpdate.UsuarioActualiza = facturaEnc.UsuarioCrea;
                    context.Existencia.Attach(existenciaUpdate);
                    context.Entry(existenciaUpdate).Property(x => x.Cantidad).IsModified = true;
                    context.Entry(existenciaUpdate).Property(x => x.FechaActualiza).IsModified = true;
                    context.Entry(existenciaUpdate).Property(x => x.UsuarioActualiza).IsModified = true;

                    await context.SaveChangesAsync();
                }
                return new CreatedAtRouteResult("FacturaCreada", new { facturaEnc });
            }
            else {
                return BadRequest();
            }
        }

        private async Task<bool> ValidarExistencia(List<FacturaDetDTO> Detalle)
        {
            var resp = true;
            foreach (var det in Detalle)
            {
                var UnidadesExistencia = await context.Existencia
                    .Where(x => x.ProductoId == det.ProductoId)
                    .SumAsync(x => x.Cantidad);

                var unidadesMinimas = decimal.Parse(_configuration["CantidadUnidadesMinimas"]);

                var UnidadesRestantes = UnidadesExistencia - det.Cantidad;

                if (UnidadesRestantes< unidadesMinimas)
                {
                    resp = false;
                }
            }
            return resp;
        }

        [HttpGet("{NumFactura}", Name = "ConsultaFactura")]
        public ActionResult<FacturaDTO> Get(int NumFactura)
        {
               var encabezado  =  (  from enc  in  context.FacturaEnc 
                              join client in  context.Cliente 
                                    on enc.ClienteId equals client.Id
                              where  enc.NumeroFactura == NumFactura
                            select new FacturaEncLecturaDTO
                           {
                               NumFactura = enc.NumeroFactura
                               ,Cliente = $" {client.PrimerNombre} {client.SegundoNombre} {client.PrimerApellido} {client.SegundoApellido} {client.RazonSocial}"
                               ,FechaFactura = enc.FechaFactura
                               ,SubTotal = enc.SubTotal
                               ,TotalIva = enc.TotalIva
                               ,TotalDescuento = enc.ValorDescuento
                               ,ValorTotal = enc.ValorTotal
                               ,EstadoFactura = enc.Estado
                               ,UsuarioCrea = enc.UsuarioCrea
                               ,UsuarioActualiza = enc.UsuarioActualiza
                               ,FechaActualiza = enc.FechaActualiza
                           }).ToList();

                var detalle = (from enc in context.FacturaEnc
                                join det in context.FacturaDet
                                          on enc.Id equals det.FacturaEncId
                                join prod in context.Producto
                                     on det.ProductoId equals prod.Id
                           where enc.NumeroFactura == NumFactura
                            select new FacturaDetLecturaDTO
                            {
                                NumFactura = enc.NumeroFactura
                                ,Producto = prod.Descripcion
                                ,Cantidad = det.Cantidad
                                ,ValorUnitario = det.ValorUnitario
                                ,ValorBruto = det.ValorBruto
                                ,ValorIva = det.ValorIva
                                ,ValorNeto = det.ValorNeto
                            }).ToList();

            if (encabezado.Count == 0)
            {
                return NotFound();
            }
            else
            {
                var resultado = new FacturaDTO();
                resultado.EncabezadoFactura = mapper.Map<List<FacturaEncLecturaDTO>>(encabezado);
                resultado.DetalleFactura = mapper.Map<List<FacturaDetLecturaDTO>>(detalle);
                return  resultado;
            }
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<FacturaDTOAnulacion> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }

            var facturaDB = await context.FacturaEnc.FirstOrDefaultAsync(x => x.NumeroFactura == id);

            if (facturaDB == null)
            {
                return NotFound();
            }

            var facturaDTO = mapper.Map<FacturaDTOAnulacion>(facturaDB);
            facturaDTO.FechaActualiza = DateTime.Now;
            facturaDTO.Estado = false;
            patchDocument.ApplyTo(facturaDTO, ModelState);

            mapper.Map(facturaDTO, facturaDB);

            var isValid = TryValidateModel(facturaDB);

            if (!isValid)
            {
                return BadRequest(ModelState);
            }

            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
