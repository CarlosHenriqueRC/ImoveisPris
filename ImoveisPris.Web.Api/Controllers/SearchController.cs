using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ImoveisPris.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {

        private readonly Parametros parametros;
        Application.UseCases.SearchImovel useCase;

        public SearchController(IOptions<Parametros> pparametros)
        {
            parametros = pparametros.Value;
            useCase = new(parametros.conexao);
        }


        // GET api/<SearchController>/5
        [HttpGet("{id}")]
        public Domain.Entity.VO.EnterImovel Get(int Id)
        {
            return useCase.get(Id);
        }

        // POST api/<SearchController>
        [HttpPost]
        public Domain.Entity.VO.ReturnSearchImovel Post(Domain.Entity.VO.SearchImovel search)
        {
            return useCase.search(search);
        }


    }
}
