using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessLayer
{
    public class ValideringEgenEx
    {
        public static  void CustomException(Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
