using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImoveisPris.Domain.Entity;

namespace ImoveisPris.Infra.Repository
{
    interface IRepositoryTiposDeImovel
    {
        /*       void SaveData(List<Domain.Entity.userCron> aluserCron);
               void SaveData(Domain.Entity.userCron userCron);
               Domain.Entity.userCron GetData(int id);
               void RemoveData(int id); */
        IList<Domain.Entity.TipoDeImovel> GetAllData();

    }
}
