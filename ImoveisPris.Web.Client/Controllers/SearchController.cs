using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ImoveisPris.Web.Client.Controllers
{
    public class SearchController : Controller
    {

        private readonly Parametros parametros;
        private const string NomeDoController = "Search";
        private readonly IWebHostEnvironment webHostEnvironment;
        // GET: SearchImovelController

        public SearchController(IOptions<Parametros> pparametros, IWebHostEnvironment _environment)
        {
            parametros = pparametros.Value;
            webHostEnvironment = _environment;
        }
        public async Task<ActionResult> IndexAsync()
        {
            Models.Imovel model = new();
            try {
            using (var client = new System.Net.Http.HttpClient())
            {
                client.BaseAddress = new Uri(parametros.Endereco);
                //HTTP GET
                var responseTask = await client.GetAsync(NomeDoController + "/0");
                if (responseTask.IsSuccessStatusCode)
                {
                    var readTask = await responseTask.Content.ReadAsStreamAsync();//.ReadAsAsync<IEnumerate<Domain.Entity.TipoDeImovel>>();
                    Domain.Entity.VO.EnterImovel VO = Util.DeserializeJsonFromStream<Domain.Entity.VO.EnterImovel>(readTask);
                    IList<Models.IdName> lIdName = new List<Models.IdName>();

                    foreach (Domain.Entity.TipoDeDisponibilidade entity in VO.TiposDeDisponibilidade)
                    {
                        Models.IdName modelTD = new();
                        modelTD.Id = entity.Id;
                        modelTD.Name = entity.Descricao;
                        lIdName.Add(modelTD);
                    }
                    model.TiposDeDisponibilidade = new SelectList(lIdName, "Id", "Name");
                    lIdName = new List<Models.IdName>();
                    foreach (Domain.Entity.TipoDeImovel entity in VO.TiposDeImoveis)
                    {
                        Models.IdName modelTD = new();
                        modelTD.Id = entity.Id;
                        modelTD.Name = entity.Descricao;
                        lIdName.Add(modelTD);
                    }
                    model.TiposDeImovel = new SelectList(lIdName, "Id", "Name");
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

        // GET: SearchImovelController/Details/5
        public async Task<ActionResult> DetailsAsync(int id)
        {

            Models.Imovel destiny = new();

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
                        Domain.Entity.VO.EnterImovel source = Util.DeserializeJsonFromStream<Domain.Entity.VO.EnterImovel>(readTask);

                        destiny.Id = source.imovel.Id;
                        destiny.Logradouro = source.imovel.Logradouro;
                        destiny.Bairro = source.imovel.Bairro;
                        destiny.Banheiros = source.imovel.Banheiros;
                        destiny.CEP = source.imovel.CEP;
                        destiny.Complemento = source.imovel.Complemento;
                        destiny.Numero = source.imovel.Numero;
                        destiny.Observacoes = source.imovel.Observacoes;
                        destiny.Quartos = source.imovel.Quartos;
                        destiny.Area = source.imovel.Area;
                        destiny.Vagas = source.imovel.Vagas;
                        destiny.Valor = source.imovel.Valor;
                        destiny.suites = source.imovel.Suites;
                        destiny.ImagesFileNames = getFileName(destiny.Id);
                        IList<Models.IdName> lIdName = new List<Models.IdName>();

                        foreach (Domain.Entity.TipoDeDisponibilidade entity in source.TiposDeDisponibilidade)
                        {
                            Models.IdName modelTD = new();
                            modelTD.Id = entity.Id;
                            modelTD.Name = entity.Descricao;
                            lIdName.Add(modelTD);
                        }
                        destiny.TiposDeDisponibilidade = new SelectList(lIdName, "Id", "Name", source.imovel.TipoDeDisponibilidadeId);
                        lIdName = new List<Models.IdName>();
                        foreach (Domain.Entity.TipoDeImovel entity in source.TiposDeImoveis)
                        {
                            Models.IdName modelTD = new();
                            modelTD.Id = entity.Id;
                            modelTD.Name = entity.Descricao;
                            lIdName.Add(modelTD);
                        }
                        destiny.TiposDeImovel = new SelectList(lIdName, "Id", "Name", source.imovel.TipoDeImovelId);
                    }
                    else
                    {
                        var readTask = await responseTask.Content.ReadAsStreamAsync();
                        string message = Util.DeserializeStringFromStream(readTask);
                        throw new Exception(message);
                    }
                }
                return View(destiny);
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

        // GET: SearchImovelController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SearchImovelController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(IFormCollection collection)
        {
            try
            {
                Domain.Entity.VO.SearchImovel entity = new();
                entity.TipoDeDisponibilidadeId = int.Parse(collection["ddlTD"].ToString());
                entity.TipoImovelId = int.Parse(collection["ddlTI"].ToString());
                entity.Bairro = collection["txtBairro"].ToString();
                entity.NumeroDeQuartos = int.Parse(collection["txtQuarto"].ToString());
                entity.NumeroDeQuartose = int.Parse(collection["txtQuartoe"].ToString());
                entity.NumeroDeSuites = int.Parse(collection["txtSuite"].ToString());
                entity.NumeroDeSuitese = int.Parse(collection["txtSuitee"].ToString());
                entity.NumeroDeVagas = int.Parse(collection["txtVagas"].ToString());
                entity.NumeroDeVagase = int.Parse(collection["txtVagase"].ToString());
                entity.Valor = decimal.Parse(collection["txtValor"].ToString());
                entity.Valore = decimal.Parse(collection["txtValore"].ToString());

                using (var client = new System.Net.Http.HttpClient())
                {
                    client.BaseAddress = new Uri(parametros.Endereco + NomeDoController);
                    var json = JsonConvert.SerializeObject(entity);
                    var stringContent = new System.Net.Http.StringContent(json);//, System.Text.UnicodeEncoding.UTF8, "application/json");
                    var responseTask = await client.PostAsJsonAsync<Domain.Entity.VO.SearchImovel>(client.BaseAddress, entity);
                    if (responseTask.IsSuccessStatusCode)
                    {
                        var readTask = await responseTask.Content.ReadAsStreamAsync();//.ReadAsAsync<IEnumerate<Domain.Entity.TipoDeImovel>>();
                        Domain.Entity.VO.ReturnSearchImovel VO = Util.DeserializeJsonFromStream<Domain.Entity.VO.ReturnSearchImovel>(readTask);
                        Models.Imovel model = new();
                        IList<Models.IdName> lIdName = new List<Models.IdName>();
                        foreach (Domain.Entity.TipoDeDisponibilidade source in VO.TiposDeDisponibilidade)
                        {
                            Models.IdName destiny = new();
                            destiny.Id = source.Id;
                            destiny.Name = source.Descricao;
                            lIdName.Add(destiny);
                        }
                        model.TiposDeDisponibilidade = new SelectList(lIdName, "Id", "Name");
                        lIdName = new List<Models.IdName>();
                        foreach (Domain.Entity.TipoDeImovel source in VO.TiposDeImoveis)
                        {
                            Models.IdName destiby = new();
                            destiby.Id = source.Id;
                            destiby.Name = source.Descricao;
                            lIdName.Add(destiby);
                        }
                        model.TiposDeImovel = new SelectList(lIdName, "Id", "Name");
                        return View(model);
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

        // GET: SearchImovelController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SearchImovelController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, IFormCollection collection)
        {
            try
            {
                Domain.Entity.VO.SearchImovel entity = new();
                entity.TipoDeDisponibilidadeId = int.Parse(collection["ddlTD"].ToString());
                entity.TipoImovelId = int.Parse(collection["ddlTI"].ToString());
                entity.Bairro = collection["txtBairro"].ToString();
                if (collection["txtQuarto"].ToString().Trim().Length>0)
                    entity.NumeroDeQuartos = int.Parse(collection["txtQuarto"].ToString());
                if (collection["txtQuartoe"].ToString().Trim().Length > 0)
                    entity.NumeroDeQuartose = int.Parse(collection["txtQuartoe"].ToString());
                if (collection["txtSuite"].ToString().Trim().Length > 0)
                    entity.NumeroDeSuites = int.Parse(collection["txtSuite"].ToString());
                if (collection["txtSuitee"].ToString().Trim().Length > 0)
                    entity.NumeroDeSuitese = int.Parse(collection["txtSuitee"].ToString());
                if (collection["txtVagas"].ToString().Trim().Length > 0)
                    entity.NumeroDeVagas = int.Parse(collection["txtVagas"].ToString());
                if (collection["txtVagase"].ToString().Trim().Length > 0)
                    entity.NumeroDeVagase = int.Parse(collection["txtVagase"].ToString());
                if (collection["txtValor"].ToString().Trim().Length > 0)
                    entity.Valor = decimal.Parse(collection["txtValor"].ToString());
                if (collection["txtValore"].ToString().Trim().Length > 0)
                    entity.Valore = decimal.Parse(collection["txtValore"].ToString());

                using (var client = new System.Net.Http.HttpClient())
                {
                    client.BaseAddress = new Uri(parametros.Endereco + NomeDoController);
                    var json = JsonConvert.SerializeObject(entity);
                    var stringContent = new System.Net.Http.StringContent(json);//, System.Text.UnicodeEncoding.UTF8, "application/json");
                    var responseTask = await client.PostAsJsonAsync<Domain.Entity.VO.SearchImovel>(client.BaseAddress, entity);
                    if (responseTask.IsSuccessStatusCode)
                    {
                        var readTask = await responseTask.Content.ReadAsStreamAsync();//.ReadAsAsync<IEnumerate<Domain.Entity.TipoDeImovel>>();
                        Domain.Entity.VO.ReturnSearchImovel VO = Util.DeserializeJsonFromStream<Domain.Entity.VO.ReturnSearchImovel>(readTask);
                        IList<Models.Imovel> imoveis = new List<Models.Imovel>();
                        Models.ReturnSearch model = new();
                        foreach (Domain.Entity.Imovel source in VO.imoveis)
                        {
                            Models.Imovel destiny = new();
                            destiny.Id = source.Id;
                            destiny.Logradouro = source.Logradouro;
                            destiny.Bairro = source.Bairro;
                            destiny.Banheiros = source.Banheiros;
                            destiny.CEP = source.CEP;
                            destiny.Complemento = source.Complemento;
                            destiny.Numero = source.Numero;
                            destiny.Observacoes = source.Observacoes;
                            destiny.Quartos = source.Quartos;
                            destiny.Area = source.Area;
                            destiny.Vagas = source.Vagas;
                            destiny.Valor = source.Valor;
                            destiny.ImagesFileNames = getFileName(destiny.Id);
                            imoveis.Add(destiny);

                        }
                        model.imoveis = imoveis;
                        IList<Models.IdName> lIdName = new List<Models.IdName>();
                        foreach (Domain.Entity.TipoDeDisponibilidade source in VO.TiposDeDisponibilidade)
                        {
                            Models.IdName destiny = new();
                            destiny.Id = source.Id;
                            destiny.Name = source.Descricao;
                            lIdName.Add(destiny);
                        }
                        model.TiposDeDisponibilidade = new SelectList(lIdName, "Id", "Name");
                        lIdName = new List<Models.IdName>();
                        foreach (Domain.Entity.TipoDeImovel source in VO.TiposDeImoveis)
                        {
                            Models.IdName destiby = new();
                            destiby.Id = source.Id;
                            destiby.Name = source.Descricao;
                            lIdName.Add(destiby);
                        }
                        model.TiposDeImovel = new SelectList(lIdName, "Id", "Name");
                        return View(model);
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


        public string getFileName(int Id)
        {
            StringBuilder FileNames = new StringBuilder();
            string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, @"images\ImagensImoveis\" + Id.ToString());
            DirectoryInfo dir = new DirectoryInfo(uploadsFolder);
            foreach (FileInfo file in dir.GetFiles())
            {
                FileNames.Append(file.Name);
                FileNames.Append('|');
            }
            return FileNames.ToString().Substring(0,FileNames.ToString().Length-1);
        }
    }
}
