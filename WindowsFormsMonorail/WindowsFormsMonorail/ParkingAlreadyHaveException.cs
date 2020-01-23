using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsMonorail
{
    public class ParkingAlreadyHaveException : Exception
    {

        public ParkingAlreadyHaveException() : base("В депо уже есть такой поезд") { }
    }
}