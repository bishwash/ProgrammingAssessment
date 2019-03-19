using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assessment.Api.Exceptions
{
    public class StudentNotFoundException : Exception
    {
        public StudentNotFoundException()
        {

        }

        public StudentNotFoundException(string name)
            : base(String.Format("Cannot find Student : {0}", name))
        {

        }
    }
}
