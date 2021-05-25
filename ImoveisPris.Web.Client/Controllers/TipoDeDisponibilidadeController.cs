using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using ImoveisPris.Web.Client.Models;
using ImoveisPris.Domain.Entity;
using System.IO;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace ImoveisPris.Web.Client.Controllers
{
    public class TipoDeDisponibilidadeController : Controller
    {

        private readonly Parametros parametros;
        private const string NomeDoController = "TipoDeDisponibilidade";

        public TipoDeDisponibilidadeController(IOptions<Parametros> pparametros)
        {
            parametros = pparametros.Value;
        }

        // GET: TipoDeDispnibilidadeController
        public async Task<ActionResult> IndexAsync()
        {
            IList<Models.TipoDeDisponibilidade> model = new List<Models.TipoDeDisponibilidade>();
            try
            { 
                using (var client = new System.Net.Http.HttpClient())
                {
                    client.BaseAddress = new Uri(parametros.Endereco);
                    //HTTP GET
                    var responseTask = await client.GetAsync(NomeDoController);

                    if (responseTask.IsSuccessStatusCode)
                    {
                        var readTask = await responseTask.Content.ReadAsStreamAsync();
                        IList<Domain.Entity.TipoDeDisponibilidade> entity = Util.DeserializeJsonFromStream<IList<Domain.Entity.TipoDeDisponibilidade>>(readTask);

                        foreach (Domain.Entity.TipoDeDisponibilidade source in entity)
                        {
                            Models.TipoDeDisponibilidade destiny = new();
                            destiny.Id = source.Id;
                            destiny.Descricao = source.Descricao;
                            model.Add(destiny);
                        }
                    }
                }
                return View(model);
            }
            catch (Exception ex)
            {
                Models.ErrorViewModel err = new();
                err.Mensagem = ex.Message;
                err.NomeDeControllerDestino = "Home";
                err.NomeDaAction = "Index";

                return View("Error", err);
            }
        }


        // GET: TipoDeDispnibilidadeController/Details/5
        public async Task<ActionResult> DetailsAsync(int id)
        {

            Models.TipoDeDisponibilidade model = new();
            try
            {
                using (var client = new System.Net.Http.HttpClient())
                {
                    client.BaseAddress = new Uri(parametros.Endereco);
                    var responseTask = await client.GetAsync(NomeDoController + "/ " + id.ToString());
                    if (responseTask.IsSuccessStatusCode)
                    {
                        var readTask = await responseTask.Content.ReadAsStreamAsync();//.ReadAsAsync<IEnumerate<Domain.Entity.TipoDeImovel>>();
                        Domain.Entity.TipoDeDisponibilidade entity = Util.DeserializeJsonFromStream<Domain.Entity.TipoDeDisponibilidade>(readTask);

                        model.Id = entity.Id;
                        model.Descricao = entity.Descricao;
                    }
                    else
                    {
                        var readTask = await responseTask.Content.ReadAsStreamAsync();
                        string message = Util.DeserializeStringFromStream(readTask);
                        throw new Exception(message);
                    }
                }
                return View(model);
            }
            
            catch (Exception ex)
            {
                Models.ErrorViewModel err = new();
                err.Mensagem = ex.Message;
                err.NomeDeControllerDestino = "Home";
                err.NomeDaAction = "Index";

                return View("Error", err);
            }
           

        }

        // GET: TipoDeDispnibilidadeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoDeDispnibilidadeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(IFormCollection collection)
        {
            try
            {
                Domain.Entity.TipoDeDisponibilidade source = new();
                source.Descricao = collection["txtDescricao"].ToString();
                source.Validate();

                using (var client = new System.Net.Http.HttpClient())
                {
                    client.BaseAddress = new Uri(parametros.Endereco + NomeDoController);
                    var responseTask = await client.PostAsJsonAsync<Domain.Entity.TipoDeDisponibilidade>(client.BaseAddress,source);
                    if (responseTask.IsSuccessStatusCode)
                    {
                        return RedirectToAction("index", "Home");
                    }
                    else
                    {
                        var readTask = await responseTask.Content.ReadAsStreamAsync();
                        string message = Util.DeserializeStringFromStream(readTask);
                        throw new Exception(message);
                    }
                }

                return View();
            }
            catch (Exception ex)
            {
                Models.ErrorViewModel err = new();
                err.Mensagem = ex.Message;
                err.NomeDeControllerDestino = "Home";
                err.NomeDaAction = "Index";

                return View("Error", err);
            }
        }

        // GET: TipoDeDispnibilidadeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TipoDeDispnibilidadeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, IFormCollection collection)
        {
            try
            {
                Domain.Entity.TipoDeDisponibilidade source = new();
                source.Id = int.Parse(collection["txtId"].ToString());
                source.Descricao = collection["txtDescricao"].ToString();
                source.Validate();

                using (var client = new System.Net.Http.HttpClient())
                {
                    client.BaseAddress = new Uri(parametros.Endereco + NomeDoController);
                    var responseTask = await client.PutAsJsonAsync<Domain.Entity.TipoDeDisponibilidade>(client.BaseAddress, source);

                    if (responseTask.IsSuccessStatusCode)
                    {
                        return RedirectToAction("index", "Home");
                    }
                    else
                    {
                        var readTask = await responseTask.Content.ReadAsStreamAsync();
                        string message = Util.DeserializeStringFromStream(readTask);
                        throw new Exception(message);
                    }
                }

                return View();
            }
            catch (Exception ex)
            {
                Models.ErrorViewModel err = new();
                err.Mensagem = ex.Message;
                err.NomeDeControllerDestino = "Home";
                err.NomeDaAction = "Index";

                return View("Error", err);
            }
        }

        // GET: TipoDeDispnibilidadeController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            Domain.Entity.TipoDeDisponibilidade entity = new();
            entity.Id = id;

            try {

                using (var client = new System.Net.Http.HttpClient())
                {
                    client.BaseAddress = new Uri(parametros.Endereco);
                    var responseTask = await client.DeleteAsync(NomeDoController + "/" + id.ToString());
                    if (responseTask.IsSuccessStatusCode)
                    {
                        return RedirectToAction("index", "Home");
                    }
                    else
                    {
                        var readTask = await responseTask.Content.ReadAsStreamAsync();
                        string message = Util.DeserializeStringFromStream(readTask);
                        throw new Exception(message);
                    }
                }
                return View();
            }
            catch (Exception ex)
            {
                Models.ErrorViewModel err = new();
                err.Mensagem = ex.Message;
                err.NomeDeControllerDestino = "Home";
                err.NomeDaAction = "Index";
                return View("Error", err);
            }
        }

    }
}
