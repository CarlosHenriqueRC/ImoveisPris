using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImoveisPris.Domain.Entity
{
    public class DomainEntityValidateException:Exception
    {
        public DomainEntityValidateException()
        {
        }

        public DomainEntityValidateException(string message)
            : base(message)
        {
        }

        public DomainEntityValidateException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
