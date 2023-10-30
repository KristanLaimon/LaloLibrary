namespace LaloLibrary.Parsers
{
    internal class InternalParserLogger
    {
        private List<string> inputStackLog { get; set; } = new List<string>();
        private List<char> singleInputLog { get; set; } = new List<char>();
        private List<string> operatorStackLog { get; set; } = new List<string>();
        private List<string> processLog { get; set; } = new List<string>();

        public string[] InputStackLog
        {
            get
            {
                return inputStackLog.ToArray();
            }
        }

        public string[] SingleInputLog
        {
            get
            {
                return singleInputLog.Select(charsito => charsito.ToString()).ToArray();
            }
        }

        public string[] OperatorStackLog
        {
            get
            {
                return operatorStackLog.ToArray();
            }
        }

        public string[] ProcessLog
        {
            get
            {
                return processLog.ToArray();
            }
        }



        public void LogInputStack(string inputStack)
        {
            inputStackLog.Add(inputStack);
        }
        public void LogSingleInput(char input)
        {
            singleInputLog.Add(input);
        }

        public void LogOperatorStack(string operatorStack)
        {
            operatorStackLog.Add(operatorStack);
        }

        public void LogProcess(string process)
        {
            processLog.Add(process);
        }

        internal void Clear()
        {
            inputStackLog.Clear();
            singleInputLog.Clear();
            operatorStackLog.Clear();
            processLog.Clear();
        }
    }

}