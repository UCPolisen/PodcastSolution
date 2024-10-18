using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class ValideringEx : Exception
    {
        public ValideringEx(string message) : base(message)
        {

        }
    }
}
