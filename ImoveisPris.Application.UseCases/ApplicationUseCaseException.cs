using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImoveisPris.Application.UseCases
{
    public class ApplicationUseCaseException:Exception
    {

        public ApplicationUseCaseException()
        {
        }

        public ApplicationUseCaseException(string message)
            : base(message)
        {
        }

        public ApplicationUseCaseException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
