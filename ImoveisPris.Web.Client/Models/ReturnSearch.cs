using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImoveisPris.Web.Client.Models
{
    public class ReturnSearch
    {
        public IList<Models.Imovel> imoveis { get; set; }
        public SelectList TiposDeDisponibilidade { get; set; }

        public SelectList TiposDeImovel { get; set; }
    }
}
