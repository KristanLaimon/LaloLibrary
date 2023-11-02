using LaloLibrary.DataStructures;
using LaloLibrary.Exceptions;
using LaloLibrary.Utils;

namespace LaloLibrary.Parsers
{
    public class ExpressionParser
    {
        private LinkedStack<char> finalResultStack, operationStack, tempStack, inputStack;
        private InternalParserLogger logger;

        public ExpressionParser()
        {
            logger = new();
            finalResultStack = new();
            operationStack = new();
            tempStack = new();
            inputStack = new();
        }

        public ParserResults ConvertToPrefix(string infijo)
        {
            GenerateInputStack(infijo);
            logger.Clear();

            bool lastCharWasOp = false;
            bool lastCharWasOpeningBracket = false;

            while (inputStack.Count() != 0)
            {
                string algo = inputStack.MakeToString();
                logger.LogInputStack(StringUtils.InvertString(inputStack.MakeToString()));

                char actualChar = inputStack.Pop();
                logger.LogSingleInput(actualChar);

                if (char.IsLetterOrDigit(actualChar))
                {
                    if (lastCharWasOp)
                    {
                        SpacedPushToFinalStack(actualChar);
                    }
                    else
                    {
                        finalResultStack.Push(actualChar);
                    }
                }

                if (StringUtils.IsOperator(actualChar))
                {
                    if (lastCharWasOp && actualChar != '-' && actualChar != '+' && !lastCharWasOpeningBracket)
                    {
                        throw new ParserException("Se ha puesto dos operandos seguidos que no sean '+' ni '-'... e.g '//' ó '^^'", this);
                    }

                    lastCharWasOp = true;

                    if (actualChar == '-' && !lastCharWasOp)
                    {
                        // If '-' is not following an operator or opening parenthesis, treat it as a subtraction operator.
                        while (IsLessImportantThanNextOne(actualChar, GetBelowActualChar()) && operationStack.Count() > 1)
                        {
                            SwapAndPushOperators();
                        }
                        lastCharWasOp = true;
                        operationStack.Push(actualChar);
                        logger.LogOperatorStack(operationStack.MakeToString());
                    }

                    logger.LogOperatorStack(operationStack.MakeToString());
                    operationStack.Push(actualChar);

                    if (IsLessImportantThanNextOne(actualChar, GetBelowActualChar()) && operationStack.Count() > 1)
                    {
                        SwapAndPushOperators();
                    }
                    continue;
                }

                if (StringUtils.IsSpecialOperator(actualChar))
                {
                    operationStack.Push(actualChar);
                    logger.LogOperatorStack(operationStack.MakeToString());

                    if (IsLessImportantThanNextOne(actualChar, GetBelowActualChar()) && operationStack.Count() > 1)
                    {
                        SwapAndPushOperators();
                    }
                    continue;
                }

                if (StringUtils.IsClosingBracket(actualChar))
                {
                    lastCharWasOp = true;
                    operationStack.Push(actualChar);
                    continue;
                }

                if (StringUtils.IsOpeningBracket(actualChar))
                {
                    lastCharWasOp = true;
                    lastCharWasOpeningBracket = true;
                    PopAndPushUntilClosingBracket();
                    continue;
                }

                if (char.IsWhiteSpace(actualChar))
                {
                    continue; //Ignore whitespaces
                }

                if (actualChar == '.')
                {
                    finalResultStack.Push(actualChar);
                }

                lastCharWasOp = false;
                lastCharWasOpeningBracket = false;

                logger.LogProcess(finalResultStack.MakeToString().Trim());
            }

            if (lastCharWasOpeningBracket)
            {
                logger.LogProcess(finalResultStack.MakeToString().Trim());
            }

            PushRemainingOperators();

            if (finalResultStack.MakeToString().Contains(')'))
            {
                throw new ParserException("Se ha puesto un ') demás en la ecuación", this);
            }

            ParserResults finalResults = CreateParserResults();
            ClearParser();
            return finalResults;
        }

        private ParserResults CreateParserResults()
        {
            ParserResults results;
            results.processLog = logger.ProcessLog;
            results.operatorStackLog = logger.OperatorStackLog;
            results.singleInputLog = logger.SingleInputLog;
            results.inputStackLog = logger.InputStackLog;
            results.Prefix = finalResultStack.MakeToString().Trim();
            return results;
        }

        public string ConvertToInfix(string prefix, out bool infixChanged)
        {
            infixChanged = false;
            LinkedStack<string> stack = new();
            string[] tokens = prefix.Split(' ');

            for (int i = tokens.Length - 1; i >= 0; i--)
            {
                string token = tokens[i];

                if (StringUtils.IsOperator(token))
                {
                    string operand1;
                    try
                    {
                        operand1 = stack.Pop();
                    }
                    catch
                    {
                        throw new ParserException("Se ha intentado convertir a infijo una expresión con solo un operando", this);
                    }

                    string operand2;

                    try
                    {
                        operand2 = stack.Pop();
                    }
                    catch
                    {
                        if (token != "-" && token != "+" && token != "/" && token != "*")
                        {
                            throw new ParserException("Se ha puesto operador(es) demás...", this);
                        }
                        operand2 = operand1;
                        operand1 = "0";
                    }

                    string infixExpression = "(" + " " + operand1 + " " + token + " " + operand2 + " " + ")";
                    stack.Push(infixExpression);
                }
                else if (StringUtils.IsSpecialOperator(token))
                {
                    string operand1 = stack.Pop();

                    string infixExpression = "(" + " " + token + " " + operand1 + " " + ")";
                    stack.Push(infixExpression);
                }
                else
                {
                    stack.Push(token);
                }
            }

            return stack.Pop();
        }

        #region Parser Core

        internal void ClearParser()
        {
            operationStack.Clear();
            inputStack.Clear();
            finalResultStack.Clear();
            tempStack.Clear();
        }

        private void SpacedPushToFinalStack(char actualChar)
        {
            finalResultStack.Push(' ');
            finalResultStack.Push(actualChar);
        }

        private void GenerateInputStack(string input)
        {
            char[] infijoChars = input.ToCharArray();
            foreach (char c in infijoChars)
                inputStack.Push(c);
        }

        private bool IsLessImportantThanNextOne(char topChar, char belowChar)
        {
            if (topChar == ')' || belowChar == ')' || belowChar == ' ') return false;

            Priority topCharPriority = GetPriorityFrom(topChar);
            Priority belowCharPriority = GetPriorityFrom(belowChar);

            int comparator = topCharPriority.CompareTo(belowCharPriority);

            switch (comparator)
            {
                case -1:
                    return false;

                case 0:
                    return false;

                case 1:
                    return true;
            }
            throw new Exception("Someting strange happened in IsLessImportantThanNextOne Method");
        }

        private Priority GetPriorityFrom(char charsito)
        {
            switch (charsito)
            {
                case '+':
                case '-':
                    return Priority.Low;

                case '*':
                case '/':
                case '%':
                    return Priority.Medium;

                case '^':
                case '√':
                    return Priority.High;

                default:
                    throw new ParserException("It isn't an operator (Not counting ( or ) as one of them", this);
            }
        }

        private char GetBelowActualChar()
        {
            if (operationStack.Count() <= 1)
                return ' ';

            return operationStack.PeekNode().NextNode.Data;
        }

        private void SwapAndPushOperators()
        {
            while (IsLessImportantThanNextOne(operationStack.Peek(), GetBelowActualChar()))
            {
                tempStack.Push(operationStack.Pop());
                SpacedPushToFinalStack(operationStack.Pop());
                operationStack.Push(tempStack.Pop());
                logger.LogOperatorStack(operationStack.MakeToString());
            }
        }

        private void PopAndPushUntilClosingBracket()
        {
            try
            {
                char actualOp = operationStack.Peek();
                while (actualOp != ')')
                {
                    SpacedPushToFinalStack(operationStack.Pop());
                    logger.LogOperatorStack(operationStack.MakeToString());
                    actualOp = operationStack.Peek();
                }
            }
            catch
            {
                throw new ParserException("Se ha agregado un '(' de más...", this);
            }
            //Delete ')' as well
            operationStack.Pop();
        }

        private void PushRemainingOperators()
        {
            while (operationStack.Count() != 0)
            {
                SpacedPushToFinalStack(operationStack.Pop());
                logger.LogOperatorStack(operationStack.MakeToString());
                logger.LogProcess(finalResultStack.MakeToString());
            }
        }

        #endregion Parser Core
    }
}