using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationLibrary.Models
{
    public class ReservationException : Exception
    {
        public ReservationException(string msg) : base(msg)
        {
            
        }
    }
}
