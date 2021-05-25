using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImoveisPris.Infra.Persistence
{
    public class PersistenceEntityException:Exception
    {
        public PersistenceEntityException()
        {
        }

        public PersistenceEntityException(string message)
            : base(Encoding.UTF8.GetString(Encoding.Default.GetBytes(message))) 
        {          
        }

        public PersistenceEntityException(string message, Exception inner)
            : base(Encoding.UTF8.GetString(Encoding.Default.GetBytes(message)), inner)
        {
        }
    }
}
