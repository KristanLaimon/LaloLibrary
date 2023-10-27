namespace LaloLibrary.Parsers
{
    public class ParserLogger
    {
        public List<string> inputStackLog { get; set; } = new List<string>();
        public List<char> singleInputLog { get; set; } = new List<char>();
        public List<string> operatorStackLog { get; set; } = new List<string>();
        public List<string> processLog { get; set; } = new List<string>();

        internal void Clear()
        {
            inputStackLog.Clear();
            singleInputLog.Clear();
            operatorStackLog.Clear();
            processLog.Clear();
        }
    }
}