using System;

namespace ImoveisPris.Web.Client.Models
{
    public class ErrorViewModel
    {
        public string Mensagem { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(Mensagem);

        public string NomeDeControllerDestino { get; set; }
        public string NomeDaAction { get; set; }
    }
}
