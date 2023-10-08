using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaloLibrary.Exceptions
{
    internal class ExcessVerticesFigure : ApplicationException
    {
        public ExcessVerticesFigure() : base("Excess of vertices of the figure") 
        { 

        }
    }
}
