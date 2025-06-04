using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCareAPI.Exceptions
{
    public class DuplicateEntityException : Exception
    {
        private string _message = "Duplicate Entity, Item already present in Collection";

        public DuplicateEntityException(string msg)
        {
            _message = msg;
        }

        public override string Message => _message;
    }
}