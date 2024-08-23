using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightScheduleLibrary.Models
{
    public class FlightScheduleException : Exception
    {
        public FlightScheduleException(string msg) : base(msg)
        {
            
        }
    }
}
