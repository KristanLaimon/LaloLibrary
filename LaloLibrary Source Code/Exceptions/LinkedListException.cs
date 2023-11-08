using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaloLibrary.Exceptions
{
    internal class LinkedListException : ApplicationException
    {
        public LinkedListException(string message) : base(message)
        {
        }
    }
}
