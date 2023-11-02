using LaloLibrary.Parsers;

namespace LaloLibrary.Exceptions
{
    public class ParserException : ApplicationException
    {
        public ParserException(string message, ExpressionParser exP) : base(message)
        {
            exP.ClearParser();
        }
    }
}