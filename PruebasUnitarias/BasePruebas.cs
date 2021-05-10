using ApiFacturacion.Entities;
using ApiFacturacion.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace PruebasUnitarias
{
    public class BasePruebas
    {
        protected BdFacturacionContext ConstruirContext(string nombreDB)
        {
            var opciones = new DbContextOptionsBuilder<BdFacturacionContext>()
                .UseInMemoryDatabase(nombreDB).Options;

            var dbContext = new BdFacturacionContext(opciones);
            return dbContext;
        }

        protected IMapper ConfigurarAutoMapper()
        {
            var config = new MapperConfiguration(options =>
            {
                options.CreateMap<FacturaEnc, FacturaEncDTO>().ReverseMap();
                options.CreateMap<FacturaDet, FacturaDetDTO>().ReverseMap();
                options.CreateMap<FacturaEnc, FacturaDTOAnulacion>().ReverseMap();
                options.CreateMap<Producto, ProductoDTO>().ReverseMap();
            });

            return config.CreateMapper();
        }
    }
}
