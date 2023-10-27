using LaloLibrary.Exceptions;
using LaloLibrary.DataStructures;
using LaloLibrary.Utils;

namespace LaloLibrary.Parsers
{
    public class ExpressionParser
    {
        private LinkedStack<char> finalResultStack;
        private LinkedStack<char> operationStack;
        private LinkedStack<char> tempStack;
        private LinkedStack<char> inputStack;
        private ParserLogger internalLogger;

        public ExpressionParser()
        {
            internalLogger = new();
            finalResultStack = new();
            operationStack = new();
            tempStack = new();
            inputStack = new();
        }

        public string ConvertToPrefix(string infijo, out ParserLogger allLogs)
        {
            GenerateInputStack(infijo);
            internalLogger.Clear();

            bool lastCharWasOp = false;
            bool lastCharWasOpeningBracket = false;

            while (inputStack.Count() != 0)
            {
                internalLogger.inputStackLog.Add(StringUtils.InvertString(inputStack.MakeToString()));
                char actualChar = inputStack.Pop();
                internalLogger.singleInputLog.Add(actualChar);

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

                if (StringUtils.IsOperator(actualChar.ToString()))
                {
                    if (lastCharWasOp)
                    {
                        throw new ParserException("Se ha puesto dos operandos seguidos... e.g '+/' ó '+)", this);
                    }

                    lastCharWasOp = true;
                    operationStack.Push(actualChar);
                    internalLogger.operatorStackLog.Add(operationStack.MakeToString());

                    if (IsLessImportantThanNextOne(actualChar, GetBelowActualChar()) && operationStack.Count() > 1)
                        SwapAndPushOperators();
                    continue;
                }

                if (StringUtils.IsSpecialOperator(actualChar.ToString()))
                {
                    operationStack.Push(actualChar);
                    internalLogger.operatorStackLog.Add(operationStack.MakeToString());

                    if (IsLessImportantThanNextOne(actualChar, GetBelowActualChar()) && operationStack.Count() > 1)
                        SwapAndPushOperators();
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
                    lastCharWasOpeningBracket = true;
                    PopAndPushUntilClosingBracket();
                    continue;
                }

                if (char.IsWhiteSpace(actualChar))
                {
                    continue;
                }

                if (actualChar == '.')
                {
                    finalResultStack.Push(actualChar);
                }

                lastCharWasOp = false;
                lastCharWasOpeningBracket = false;

                LogActualResultStack();
            }

            if (lastCharWasOpeningBracket)
            {
                LogActualResultStack();
            }

            PushRemainingOperators();

            if (finalResultStack.MakeToString().Contains(')'))
            {
                throw new ParserException("Se ha puesto un ') demás en la ecuación", this);
            }

            //processLog.Add(finalResultStack.MakeToString().Trim());
            /*return*/
            string finalResult = finalResultStack.MakeToString().Trim();
            allLogs = internalLogger;
            ClearParser();
            return finalResult;
        }

        #region Methods

        internal void ClearParser()
        {
            operationStack.Clear();
            inputStack.Clear();
            finalResultStack.Clear();
        }

        private void LogActualResultStack()
        {
            internalLogger.processLog.Add(finalResultStack.MakeToString().Trim());
        }

        public string ConvertToInfix(string prefix)
        {
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
                        throw new ParserException("Se ha puesto operador(es) demás...", this);
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
                internalLogger.operatorStackLog.Add(operationStack.MakeToString());
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
                    internalLogger.operatorStackLog.Add(operationStack.MakeToString());
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
                internalLogger.operatorStackLog.Add(operationStack.MakeToString());
                internalLogger.processLog.Add(finalResultStack.MakeToString());
            }
        }

        #endregion Methods
    }
}