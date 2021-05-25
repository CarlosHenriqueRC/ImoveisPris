using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using ImoveisPris.Domain.Entity;
using ImoveisPris.Application.Services;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ImoveisPris.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImovelController : ControllerBase
    {

        private readonly Parametros parametros;
        Application.UseCases.MaintainImovel useCase;

        public ImovelController(IOptions<Parametros> pparametros)
        {
            parametros = pparametros.Value;
            useCase = new(parametros.conexao);
        }
        // GET: api/<ImovelController>
        [HttpGet]
        public IEnumerable<Domain.Entity.Imovel> Get()
        {
            return useCase.getAllImovel();
        }

        // GET api/<ImovelController>/5
        [HttpGet("{id}")]
        public Domain.Entity.VO.EnterImovel Get(int Id)
        {
            return useCase.getImovel(Id);
        }

        // POST api/<ImovelController>
        [HttpPost]
        public int Post(Domain.Entity.Imovel entity)
        {
            return useCase.post(entity);
        }

        // PUT api/<ImovelController>/5
        [HttpPut]
        public void Put(Domain.Entity.Imovel entity)
        {
            useCase.put(entity);
        }

        // DELETE api/<ImovelController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Domain.Entity.Imovel entity = new();
            entity.Id = id;
            useCase.remove(entity);
        }
    }
}
