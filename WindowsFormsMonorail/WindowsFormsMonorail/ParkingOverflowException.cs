using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsMonorail
{
    public class ParkingOverflowException : Exception
    {
        public ParkingOverflowException() : base("В депо нет свободных мест")
        { }
    }
}