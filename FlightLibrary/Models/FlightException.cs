using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightLibrary.Models
{
    public class FlightException:Exception
    {
        public FlightException(String msg):base(msg)
        {
            
        }
    }
}
