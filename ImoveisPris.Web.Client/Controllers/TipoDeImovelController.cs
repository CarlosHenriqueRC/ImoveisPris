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
using System.Net.Http.Json;
using ImoveisPris.Web.Client;
using Newtonsoft.Json;

namespace ImoveisPris.Web.Client.Controllers
{
    public class TipoDeImovelController : Controller
    {

        private readonly Parametros parametros;
        private const string NomeDoController = "TipoDeImovel";

        public TipoDeImovelController(IOptions<Parametros> pparametros)
        {
            parametros = pparametros.Value;
        }

        // GET: TipoDeImovelController
        public async Task<ActionResult> IndexAsync()
        {
            IList<Models.TipoDeImovel> model = new List<Models.TipoDeImovel>();
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
                        IList<Domain.Entity.TipoDeImovel> entity = Util.DeserializeJsonFromStream<IList<Domain.Entity.TipoDeImovel>>(readTask);

                        foreach (Domain.Entity.TipoDeImovel source in entity)
                        {
                            Models.TipoDeImovel destiny = new();
                            destiny.Id = source.Id;
                            destiny.Descricao = source.Descricao;
                            model.Add(destiny);
                        }
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

        // GET: TipoDeImovelController/Details/5
        public async Task<ActionResult> DetailsAsync(int id)
        {

            Models.TipoDeImovel model = new();
            try
            {
                using (var client = new System.Net.Http.HttpClient())
                {
                    client.BaseAddress = new Uri(parametros.Endereco);
                    //HTTP GET
                    var responseTask = await client.GetAsync(NomeDoController + "/" + id.ToString());
                    if (responseTask.IsSuccessStatusCode)
                    {
                        var readTask = await responseTask.Content.ReadAsStreamAsync();//.ReadAsAsync<IEnumerate<Domain.Entity.TipoDeImovel>>();
                        Domain.Entity.TipoDeImovel entity = Util.DeserializeJsonFromStream<Domain.Entity.TipoDeImovel>(readTask);

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

        // GET: TipoDeImovelController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoDeImovelController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(IFormCollection collection)
        {
            try
            {
                Domain.Entity.TipoDeImovel source = new();
                source.Descricao = collection["txtDescricao"].ToString();
                source.Validate();

                using (var client = new System.Net.Http.HttpClient())
                {
                    client.BaseAddress = new Uri(parametros.Endereco + NomeDoController);
                    var responseTask = await client.PostAsJsonAsync<Domain.Entity.TipoDeImovel>(client.BaseAddress,source);
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
                err.NomeDeControllerDestino = "Home";
                err.NomeDaAction = "Index";
                err.Mensagem = ex.Message;
                return View("Error", err);
            }
        }

        // GET: TipoDeImovelController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TipoDeImovelController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, IFormCollection collection)
        {
            try
            {
                Domain.Entity.TipoDeImovel source = new();
                source.Id = int.Parse(collection["txtId"].ToString());
                source.Descricao = collection["txtDescricao"].ToString();
                source.Validate();

                using (var client = new System.Net.Http.HttpClient())
                {
                    client.BaseAddress = new Uri(parametros.Endereco + NomeDoController);
                    var responseTask = await client.PutAsJsonAsync<Domain.Entity.TipoDeImovel>(client.BaseAddress, source);
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
                err.NomeDeControllerDestino = "Home";
                err.NomeDaAction = "Index";
                err.Mensagem = ex.Message;
                return View("Error", err);
            }
        }

        // GET: TipoDeImovelController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            Domain.Entity.TipoDeImovel entity = new();
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
