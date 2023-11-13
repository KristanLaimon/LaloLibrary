using System.Text.RegularExpressions;
using LaloLibrary.DataStructures.Lineal;
using LaloLibrary.DataStuctures;
using LaloLibrary.Exceptions;

namespace LaloLibrary.Maths
{
    public class PolyChecking
    {
        private const string RegexPattern = "^(((-?\\d*(,\\d+)?\\*?)|\\d+\\/\\d+)?([a-zA-Z](\\^-?\\d+)?)?)((\\+|-)(((-?\\d*(,\\d+)?\\*?)|\\d+\\/\\d+)?([a-zA-Z](\\^-?\\d+)?)?))*$|^\\s*(-?)(\\d{1,})\\s*$|^\\s*-?([a-zA-Z])\\s*$|(([\\-+]?)(\\d+)\\*([a-zA-Z]{1})\\^([-]?)(\\d+))()(\\*([\\-+]?)(\\d+)\\*([a-zA-Z]{1})\\^(\\d+))*|([+\\-]?)(\\d+)(\\*([a-zA-z]{1})(([\\^])(\\d+))?)\\s+$|([+\\-]?)(\\d+)(\\*([a-zA-z]{1})(\\^([+\\-]?)(\\d+)))+|^(\\s+)?[+\\-]?(\\d+(\\*))?([+\\-]?)?[a-zA-Z]{1}(\\^|\\*)([+\\-]?)?\\d+|([+\\-]?)(\\d+)(\\*([a-zA-z]{1})(([\\^])(\\d+))?)\\s+$|^(\\s+)?(\\d+)?(\\*[a-zA-Z](\\^[\\-+]?\\d+([+-]\\d+)?)?)+";

        public static bool IsPolynomialString(string input)
        {
            bool isPolynomial = true;

            Regex regex = new Regex(RegexPattern);
            MatchCollection coincidencias = regex.Matches(input);
            if (coincidencias.Count() == 0)
            {
                isPolynomial = false;
            }

            try
            {
                if (string.IsNullOrWhiteSpace(input)) { throw new PolynomialException("Input String is Empty"); }
                string inputString = input.Replace(" ", "").Replace("-", "+-");
                LinkedDoubleCircularList<string> splittedTerms = inputString.Split('+', StringSplitOptions.RemoveEmptyEntries).ToDoubleCircularList();

                bool isWellFormatted;
                LinkedDoubleCircularList<Monomial> listTerms = new();
                foreach (string splittedTerm in splittedTerms)
                {
                    Monomial testMon = Monomial.Parse(splittedTerm);
                }
            }
            catch
            {
                isPolynomial = false;
            }

            return isPolynomial;
        }

        public static string FormatPoly(string input)
        {
            string replacePattern = @"(\d+)\s*([a-zA-Z]+)"; // Asegúrate de que esta expresión regular sea correcta

            Regex regex = new Regex(replacePattern);

            string resultado = regex.Replace(input, match =>
            {
                // Obtener el coeficiente y la literal
                string coeficiente = match.Groups[1].Value;
                string literal = match.Groups[2].Value;

                // Reemplazar con la nueva forma
                return $"{coeficiente}*{literal}";
            });

            return resultado;
        }

    }
}
