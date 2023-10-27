using LaloLibrary.DataStructures;
using LaloLibrary.Exceptions;
using LaloLibrary.Utils;

namespace LaloLibrary.Parsers
{
    public class ExpressionCalc
    {
        private List<string> logger;
        private ExpressionParser parser;

        public ExpressionCalc()
        {
            parser = new();
            logger = new();
        }

        //This evaluates and log every calculation... it should be splitted in two methods (?)
        public double EvaluatePrefix(string prefix, out string[] process)
        {
            string infix;
            try
            {
                infix = GetInfijoFormatted(prefix);
            }
            catch (ParserException)
            {
                throw new CalcException("Se ha intentado calcular una expresión con solo un operando");
            }

            logger.Add(infix);
            LinkedStack<double> stack = new();
            string[] tokens = prefix.Split(' ');
            for (int i = tokens.Length - 1; i >= 0; i--)
            {
                if (double.TryParse(tokens[i], out double num))
                {
                    //It's a number
                    stack.Push(num);
                }
                else
                {
                    double result = -1;
                    string equation = "Error";

                    //It is basic operator
                    double operand1 = 0;

                    try
                    {
                        operand1 = stack.Pop();
                    }
                    catch
                    {
                        if (!StringUtils.AreThereLetters(StringUtils.GetTextFromStringArray(tokens)))
                        {
                            throw new CalcException("Se ha utilizando puntos flotantes demás e.g '12.12.12'");
                        }
                        throw new CalcException("Seleccionando valores para las letras...");
                    }

                    double operand2;

                    if (!StringUtils.IsSpecialOperator(tokens[i]))
                    {
                        try
                        {
                            operand2 = stack.Pop();
                        }
                        catch
                        {
                            if (!StringUtils.AreThereLetters(StringUtils.GetTextFromStringArray(tokens)))
                            {
                                throw new CalcException("Se ha utilizando puntos flotantes demás e.g '12.12.12'");
                            }
                            throw new CalcException("Seleccionando valores para las letras...");
                        }

                        switch (tokens[i])
                        {
                            case "+":
                                result = operand1 + operand2;
                                break;

                            case "-":
                                result = operand1 - operand2;
                                break;

                            case "*":
                                result = operand1 * operand2;
                                break;

                            case "/":
                                result = operand1 / operand2;
                                break;

                            case "^":
                                result = Math.Pow(operand1, operand2);
                                break;

                            case "%":
                                result = operand1 % operand2;
                                break;
                        }
                        equation = EcuationToString(operand1, tokens[i], operand2);
                    }
                    else
                    {
                        switch (tokens[i])
                        {
                            case "√":
                                result = Math.Sqrt(operand1);
                                break;

                            default:
                                throw new Exception("The expression has an invalid operator: " + tokens[i]);
                        }
                        equation = EcuationToString(tokens[i], operand1);
                    }

                    //Logging code
                    stack.Push(result);
                    infix = StringUtils.RemoveRedundantZeros(infix);
                    infix = infix.Replace("(" + equation + ")", result.ToString());
                    logger.Add(infix);
                }
            }
            //logger
            /*return*/
            process = logger.ToArray();

            logger.Clear();
            //Evaluation
            double finalResult = stack.Pop();
            return finalResult;
        }

        private string GetInfijoFormatted(string prefix)
        {
            string infix = parser.ConvertToInfix(prefix);
            infix = infix.Replace(" ", "");
            return infix;
        }

        public string EcuationToString(double op1, string operation, double op2)
        {
            return op1.ToString() + operation + op2.ToString();
        }

        public string EcuationToString(string specialOperation, double ep1)
        {
            return specialOperation + ep1.ToString();
        }
    }
}