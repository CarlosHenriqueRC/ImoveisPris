using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using ImoveisPris.Domain.Entity;
using ImoveisPris.Application.UseCases;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ImoveisPris.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoDeImovelController : ControllerBase
    {

        private readonly Parametros parametros;
        Application.UseCases.MaintainTiposDeImovel useCases;

        public TipoDeImovelController(IOptions<Parametros> pparametros)
        {
            parametros = pparametros.Value;
            useCases = new(parametros.conexao);
        }

        // GET: api/<TipoDeImovelController>
        [HttpGet]
        public IEnumerable<Domain.Entity.TipoDeImovel> Get()
        {
            return useCases.getAll();
        }

        // GET api/<TipoDeImovelController>/5
        [HttpGet("{id}")]
        public Domain.Entity.TipoDeImovel Get(int Id)
        {
            return useCases.get(Id);
        }

        // POST api/<TipoDeImovelController>
        [HttpPost]
        public void Post(Domain.Entity.TipoDeImovel entity)
        {
            useCases.post(entity);
        }

        // PUT api/<TipoDeImovelController>/5
        [HttpPut]
        public void Put(Domain.Entity.TipoDeImovel entity)
        {
            useCases.put(entity);
        }

        // DELETE api/<TipoDeImovelController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            try
            {
                Domain.Entity.TipoDeImovel entity = new();
                entity.Id = id;
                useCases.remove(entity);
            }
            catch
            {
                throw;
            }
        }
    }
}
