namespace LaloLibrary.Maths
{
    public class Literal
    {
        public char Char;
        public int Exponent;

        public Literal()
        {
            Char = 'X';
            Exponent = 0;
        }

        public Literal(char letter, int exponent)
        {
            Char = char.ToUpper(letter);
            Exponent = exponent;
        }

        public static bool AreSimilar(Literal left, Literal right)
        {
            if (left.Char == right.Char)
            {
                return true;
            }
            {
                return false;
            }
        }

        public void ToNegativeExponent()
        {
            Exponent *= -1;
        }

        public bool IsEmptyLiteral()
        {
            return Exponent == 0;
        }

        public string ToHumanizeString()
        {
            char humanChar = char.ToLower(Char);
            string output;

            if (Exponent == 1)
            {
                output = $"{humanChar}";
            }
            else if (Exponent == 0)
            {
                output = "";
            }
            else
            {
                output = $"{humanChar}^{Exponent}";
            }

            return output;
        }

        public override string ToString()
        {
            return $"{Char}^{Exponent}";
        }
    }
}