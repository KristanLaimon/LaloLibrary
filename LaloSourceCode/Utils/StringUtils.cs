using LaloLibrary.DataStructures;
using System.Text;
using System.Text.RegularExpressions;

namespace LaloLibrary.Utils
{
    public static class StringUtils
    {
        public static string InvertString(this string input)
        {
            LinkedStack<char> stack = new();

            char[] chars = input.ToCharArray();
            foreach (char c in chars)
                stack.Push(c);

            char[] inversedChars = new char[chars.Length];
            for (int i = 0; i < chars.Length; i++)
                inversedChars[i] = stack.Pop();

            StringBuilder sb = new();
            foreach (char c in inversedChars)
                sb.Append(c);

            return sb.ToString();
        }

        public static bool IsOperator(char c)
        {
            switch (c)
            {
                case '+':
                case '-':
                case '*':
                case '/':
                case '^':
                case '%':
                    return true;
            }
            return false;
        }

        public static bool IsOperator(string s)
        {
            switch (s)
            {
                case "+":
                case "-":
                case "*":
                case "/":
                case "^":
                case "%":
                    return true;
            }
            return false;
        }

        public static bool IsSpecialOperator(char c)
        {
            switch (c)
            {
                case '√':
                    return true;
            }
            return false;
        }

        public static bool IsSpecialOperator(string c)
        {
            switch (c)
            {
                case "√":
                    return true;
            }
            return false;
        }

        public static bool IsOpeningBracket(char charsito) => charsito == '(' ? true : false;

        public static bool IsClosingBracket(char charsito) => charsito == ')' ? true : false;

        public static char[] RemoveDuplicates(char[] array)
        {
            int n = array.Length;
            int newSize = 0;
            char[] uniqueArray = new char[n];

            for (int i = 0; i < n; i++)
            {
                bool isDuplicate = false;
                for (int j = 0; j < newSize; j++)
                {
                    if (array[i] == uniqueArray[j])
                    {
                        isDuplicate = true;
                        break;
                    }
                }
                if (!isDuplicate)
                {
                    uniqueArray[newSize] = array[i];
                    newSize++;
                }
            }

            Array.Resize(ref uniqueArray, newSize);
            return uniqueArray;
        }

        public static string GetAppendedLineFromArray(string[] str)
        {
            StringBuilder sb = new();
            foreach (string line in str)
                sb.AppendLine(line);

            return sb.ToString();
        }

        public static string GetStringFromArray(string[] str)
        {
            StringBuilder sb = new();
            foreach (string line in str)
                sb.Append(line);

            return sb.ToString();
        }

        public static bool AreThereLetters(string charArray)
        {
            foreach (char c in charArray)
            {
                if (Char.IsLetter(c))
                    return true;
            }
            return false;
        }

        public static bool isNumber(string numbString)
        {
            foreach (char c in numbString)
            {
                if(!Char.IsNumber(c)) return false;
            }
            return true;
        }

        public static string RemoveRedundantZeros(string expression)
        {
            // Patrón de coincidencia de números decimales con ceros redundantes
            string pattern = @"\b\d+\.\d*?0+\b";

            // Función de reemplazo para eliminar ceros redundantes
            string ReplaceMatch(Match match)
            {
                string number = match.Value;

                // Verificar si el número tiene un punto decimal
                if (number.Contains("."))
                {
                    double parsedNumber;
                    if (double.TryParse(number, out parsedNumber))
                    {
                        // Formatear el número para quitar ceros redundantes
                        string formattedNumber = parsedNumber.ToString("0.#############");
                        return formattedNumber;
                    }
                }
                return number; // Mantener números enteros y otros caracteres sin cambios
            }

            // Reemplazar los números decimales con ceros redundantes en la expresión
            string result = Regex.Replace(expression, pattern, ReplaceMatch);

            return result;
        }
    }
}