using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;
using System.Text;

namespace ImoveisPris.Web.Client.Controllers
{
    public class ImovelController : Controller
    {

        private readonly Parametros parametros;
        private const string NomeDoController = "Imovel";
        private readonly IWebHostEnvironment webHostEnvironment;

        public ImovelController(IOptions<Parametros> pparametros, IWebHostEnvironment _environment)
        {
            parametros = pparametros.Value;
            webHostEnvironment = _environment;
        }

        // GET: ImovelController
        public async Task<ActionResult> IndexAsync()
        {
            IList<Models.Imovel> model = new List<Models.Imovel>();
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
                        IList<Domain.Entity.Imovel> entity = Util.DeserializeJsonFromStream<IList<Domain.Entity.Imovel>>(readTask);

                        foreach (Domain.Entity.Imovel source in entity)
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
                            destiny.suites = source.Suites;
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

        // GET: ImovelController/Details/5
        public async Task<ActionResult> DetailsAsync(int id)
        {

            Models.Imovel destiny = new();

            try { 
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

        // GET: ImovelController/Create
        public async Task<ActionResult> CreateAsync()
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
                    model.TiposDeDisponibilidade= new SelectList(lIdName, "Id", "Name");
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

        // POST: ImovelController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(IFormCollection collection)
        {
            try
            {

                decimal resultDecimal;
                int resultInt;

                Domain.Entity.Imovel source = new();
                source.Logradouro = collection["txtLogradouro"].ToString();
                source.Complemento = collection["txtComplemento"].ToString();
                source.Observacoes = collection["txtObservacoes"].ToString();
                source.Bairro = collection["txtBairro"].ToString();
                if (int.TryParse(collection["ddlTD"].ToString(), out resultInt))
                    source.TipoDeDisponibilidadeId = resultInt;
                else
                    throw new Exception("Valor incorreto para campo Tipo de Disponibilidade");
                if (int.TryParse(collection["ddlTI"].ToString(), out resultInt))
                    source.TipoDeImovelId = resultInt;
                else
                    throw new Exception("Valor incorreto para campo Tipo de Imovel");

                if (int.TryParse(collection["txtCEP"].ToString(), out resultInt))
                    source.CEP = resultInt;
                else
                    throw new Exception("Valor incorreto para campo CEP");
                if (int.TryParse(collection["txtNumero"].ToString(), out resultInt))
                    source.Numero = resultInt;
                else
                    throw new Exception("Valor incorreto para campo numero");
                if (int.TryParse(collection["txtSuites"].ToString(), out resultInt))
                    source.Suites = resultInt;
                else
                    throw new Exception("Valor incorreto para campo suites");
                if (int.TryParse(collection["txtQuarto"].ToString(), out resultInt))
                    source.Quartos = resultInt;
                else
                    throw new Exception("Valor incorreto para campo quartos");
                if (int.TryParse(collection["txtBanheiro"].ToString(), out resultInt))
                    source.Banheiros = resultInt;
                else
                    throw new Exception("Valor incorreto para campo banheiro");
                if (int.TryParse(collection["txtVagas"].ToString(), out resultInt))
                    source.Vagas = resultInt;
                else
                    throw new Exception("Valor incorreto para campo Vagas");
                if (decimal.TryParse(collection["txtArea"].ToString(), out resultDecimal))
                    source.Area = resultDecimal;
                else
                    throw new Exception("Valor incorreto para campo area");
                if (decimal.TryParse(collection["txtValor"].ToString(), out resultDecimal))
                    source.Valor = resultDecimal;
                else
                    throw new Exception("Valor incorreto para campo valor");

                source.Validate();

                using (var client = new System.Net.Http.HttpClient())
                {
                    client.BaseAddress = new Uri(parametros.Endereco + NomeDoController);
                    var json = JsonConvert.SerializeObject(source);
                    var stringContent = new System.Net.Http.StringContent(json);//, System.Text.UnicodeEncoding.UTF8, "application/json");
                    var responseTask = await client.PostAsJsonAsync<Domain.Entity.Imovel>(client.BaseAddress, source);
                    if (responseTask.IsSuccessStatusCode)
                    {
                        var readTask = await responseTask.Content.ReadAsStreamAsync();//.ReadAsAsync<IEnumerate<Domain.Entity.TipoDeImovel>>();
                        int imovelCode = Util.DeserializeJsonFromStream<int>(readTask);
                        if (HttpContext.Request.Form.Files != null)
                        {
                            var fileName = string.Empty;
                            string PathDB = string.Empty;

                            var files = HttpContext.Request.Form.Files;
                            int count = 1;
                            if (files.Count > 0)
                            {
                                if (!(Directory.Exists(webHostEnvironment.WebRootPath + @"\Images\ImagensImoveis\" + imovelCode)))
                                    Directory.CreateDirectory(webHostEnvironment.WebRootPath + @"\Images\ImagensImoveis\" + imovelCode);

                            }
                            foreach (var file in files)
                            {
                                if (file.Length > 0)
                                {
                                    fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                                    var FileExtension = Path.GetExtension(fileName);
                                    string NewFileName = count.ToString() + FileExtension;
                                    fileName = webHostEnvironment.WebRootPath + @"\Images\ImagensImoveis\" + imovelCode + $@"\{NewFileName}";
                                    using (FileStream fs = System.IO.File.Create(fileName))
                                    {
                                        file.CopyTo(fs);
                                        fs.Flush();
                                    }
                                    count++;
                                }
                            }
                        }

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

        // GET: ImovelController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ImovelController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, IFormCollection collection)
        {
            try
            {

                decimal resultDecimal;
                int resultInt;

                Domain.Entity.Imovel source = new();
                source.Id = int.Parse(collection["txtId"].ToString());
                source.Logradouro = collection["txtLogradouro"].ToString();
                source.Complemento = collection["txtComplemento"].ToString();
                source.Observacoes = collection["txtObservacoes"].ToString();
                source.Bairro = collection["txtBairro"].ToString();
                if (int.TryParse(collection["ddlTD"].ToString(), out resultInt))
                    source.TipoDeDisponibilidadeId = resultInt;
                else
                    throw new Exception("Valor incorreto para campo Tipo de Disponibilidade");
                if (int.TryParse(collection["ddlTI"].ToString(), out resultInt))
                    source.TipoDeImovelId = resultInt;
                else
                    throw new Exception("Valor incorreto para campo Tipo de Imovel");

                if (int.TryParse(collection["txtCEP"].ToString(), out resultInt))
                    source.CEP = resultInt;
                else
                    throw new Exception("Valor incorreto para campo CEP");
                if (int.TryParse(collection["txtNumero"].ToString(), out resultInt))
                    source.Numero = resultInt;
                else
                    throw new Exception("Valor incorreto para campo numero");
                if (int.TryParse(collection["txtSuites"].ToString(), out resultInt))
                    source.Suites = resultInt;
                else
                    throw new Exception("Valor incorreto para campo suites");
                if (int.TryParse(collection["txtQuarto"].ToString(), out resultInt))
                    source.Quartos = resultInt;
                else
                    throw new Exception("Valor incorreto para campo quartos");
                if (int.TryParse(collection["txtBanheiro"].ToString(), out resultInt))
                    source.Banheiros = resultInt;
                else
                    throw new Exception("Valor incorreto para campo banheiro");
                if (int.TryParse(collection["txtVagas"].ToString(), out resultInt))
                    source.Vagas = resultInt;
                else
                    throw new Exception("Valor incorreto para campo Vagas");
                if (decimal.TryParse(collection["txtArea"].ToString(), out resultDecimal))
                    source.Area = resultDecimal;
                else
                    throw new Exception("Valor incorreto para campo area");
                if (decimal.TryParse(collection["txtValor"].ToString(), out resultDecimal))
                    source.Valor = resultDecimal;
                else
                    throw new Exception("Valor incorreto para campo valor");
                source.Validate();

                using (var client = new System.Net.Http.HttpClient())
                {
                    client.BaseAddress = new Uri(parametros.Endereco + NomeDoController);
                    var json = JsonConvert.SerializeObject(source);
                    var stringContent = new System.Net.Http.StringContent(json);
                    var responseTask = await client.PutAsJsonAsync<Domain.Entity.Imovel>(client.BaseAddress, source);
                    if (responseTask.IsSuccessStatusCode)
                    {
                        var readTask = await responseTask.Content.ReadAsStreamAsync();//.ReadAsAsync<IEnumerate<Domain.Entity.TipoDeImovel>>();
                        int imovelCode = source.Id;
                        if (HttpContext.Request.Form.Files != null)
                        {
                            var fileName = string.Empty;
                            string PathDB = string.Empty;

                            var files = HttpContext.Request.Form.Files;
                            int count = 1;
                            if (files.Count > 0)
                            {
                                if (!(Directory.Exists(webHostEnvironment.WebRootPath+ @"\Images\ImagensImoveis\" + imovelCode)))
                                    Directory.CreateDirectory(webHostEnvironment.WebRootPath + @"\Images\ImagensImoveis\" + imovelCode);

                            }
                            foreach (var file in files)
                            {
                                if (file.Length > 0)
                                {
                                    fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                                    var FileExtension = Path.GetExtension(fileName);
                                    string NewFileName = count.ToString() + FileExtension;
                                    fileName = webHostEnvironment.WebRootPath + @"\Images\ImagensImoveis\" + imovelCode + $@"\{NewFileName}";
                                    using (FileStream fs = System.IO.File.Create(fileName))
                                    {
                                        file.CopyTo(fs);
                                        fs.Flush();
                                    }
                                    count++;
                                }
                            }
                        }


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

        // GET: ImovelController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {

            try { 
            Domain.Entity.Imovel entity = new();
            entity.Id = id;

            using (var client = new System.Net.Http.HttpClient())
            {
                client.BaseAddress = new Uri(parametros.Endereco + NomeDoController);
                var json = JsonConvert.SerializeObject(entity);
                var stringContent = new System.Net.Http.StringContent(json);
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
        public string getFileName(int Id)
        {
            StringBuilder FileNames = new StringBuilder();
            string uploadsFolder = webHostEnvironment.WebRootPath + @"\Images\ImagensImoveis\" + Id.ToString();
            if (Directory.Exists(uploadsFolder))
            {
                DirectoryInfo dir = new DirectoryInfo(uploadsFolder);


                foreach (FileInfo file in dir.GetFiles())
                {
                    FileNames.Append(file.Name);
                    FileNames.Append('|');
                }
                return FileNames.ToString().Substring(0, FileNames.ToString().Length - 1);
            }
            return "";
        }
    }
}
