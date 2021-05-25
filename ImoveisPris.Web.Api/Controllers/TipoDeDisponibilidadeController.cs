using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using ImoveisPris.Domain.Entity;
using ImoveisPris.Application.UseCases;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Diagnostics;
//using System.Web.Http.Filters;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ImoveisPris.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoDeDisponibilidadeController : ControllerBase
    {

        private readonly Parametros parametros;
        Application.UseCases.MaintainTipoDeDisponibilidade useCase;

        public TipoDeDisponibilidadeController(IOptions<Parametros> pparametros)
        {
            parametros = pparametros.Value;
            useCase = new(parametros.conexao);
        }

        // GET: api/<TipoDeDisponibilidadeController>
        [HttpGet]
        public IEnumerable<Domain.Entity.TipoDeDisponibilidade> Get()
        {
            return useCase.getAll();
        }

        // GET api/<TipoDeDisponibilidadeController>/5
        [HttpGet("{id}")]
        public Domain.Entity.TipoDeDisponibilidade Get(int Id)
        {

            return useCase.get(Id);

        }

        // POST api/<TipoDeDisponibilidadeController>
        [HttpPost]
        public void Post(Domain.Entity.TipoDeDisponibilidade entity)
        {
            useCase.post(entity);
        }

        // PUT api/<TipoDeDisponibilidadeController>/5
        [HttpPut]
        public void Put(Domain.Entity.TipoDeDisponibilidade entity)
        {
            useCase.put(entity);
        }

        // DELETE api/<TipoDeDisponibilidadeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            
                Domain.Entity.TipoDeDisponibilidade entity = new();
                entity.Id = id;
                useCase.remove(entity);

        }
    }
}
