using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WakeCommerceProject.Domain
{
    public class DomainExceptionValidation : Exception
    {
        public DomainExceptionValidation(string error) : base(error)
        {

        }

        public static void When(bool hasError, string message)
        {
            if (hasError)
            {
                throw new DomainExceptionValidation(message);
            }
        }
    }
}
