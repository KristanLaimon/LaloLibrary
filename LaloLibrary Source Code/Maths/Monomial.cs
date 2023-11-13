using LaloCloning;
using LaloLibrary.DataStructures.Lineal;
using LaloLibrary.DataStuctures;
using LaloLibrary.Exceptions;

namespace LaloLibrary.Maths
{
    //input should be: -23*X^2*Y^1 . or just X should be 1*X^1, or 3 should be 3*X^0 for Parse()
    public class Monomial
    {
        private LinkedDoubleCircularList<Literal> _literals;
        private int _coefficient = 1;
        private int _degree = 0;

        internal Monomial()
        {
            _literals = new LinkedDoubleCircularList<Literal>();
        }

        public Monomial(int coefficient, LinkedDoubleCircularList<Literal> letters) 
        {
            this._coefficient = coefficient;
            _literals = letters;

            _degree = FindDegree();
        }
        public Monomial(int coefficient, params Literal[] someLiterals)
        {
            Coefficient = coefficient;
            LinkedDoubleCircularList<Literal> temp = new();

            foreach(Literal literal in someLiterals)
            {
                temp.Add(literal);
            }

            _literals = temp;
            _degree = FindDegree();
        }

        public int Coefficient { get { return _coefficient; } set { _coefficient = value; } }
        public int Degree { get { return _degree; } }
        public LinkedDoubleCircularList<Literal> Literals { get { return _literals; } }


        #region Operators
        public static Monomial operator +(Monomial left, Monomial right)
        {
            return Sum(left, right);
        }
        public static Monomial operator -(Monomial left, Monomial right)
        {
            return Substract(left, right);
        }
        public static Monomial operator *(Monomial left, Monomial right)
        {
            left = left.Simplify();
            right = right.Simplify();
            return Multiply(left, right);
        }
        public static Monomial operator /(Monomial left, Monomial right)
        {
            left = left.Simplify();
            right = right.Simplify();
            return Divide(left, right, out int reminder);
        }
        public static int operator %(Monomial left, Monomial right)
        {
            Divide(left, right, out int reminder);
            return reminder;
        }
        private static Monomial Substract(Monomial left, Monomial right)
        {
            right = right.ToNegative();
            return left + right;
        }
        private static Monomial Sum(Monomial left, Monomial right)
        {
            if (Monomial.HaveSameDeterminant(left, right))
            {
                int newCoefficient = left.Coefficient + right.Coefficient;
                LinkedDoubleCircularList<Literal> keepedLiterals = left.Literals;

                Monomial toReturn = new Monomial(newCoefficient, keepedLiterals);
                toReturn = toReturn.NormalizeOnlyNumbers();
                return toReturn;
            }
            else
            {
                return new Monomial();
            }
        }
        private static Monomial Multiply(Monomial left, Monomial right)
        {
            if (left.HasDefaultLiteral())
                left.CopyOnlyLiteralsCharsFrom(right._literals);

            if (right.HasDefaultLiteral())
                right.CopyOnlyLiteralsCharsFrom(left._literals);

            LinkedDoubleCircularList<Literal> newListLiterals = new();
            Monomial leftCopy = left.DeepClone();
            Monomial rightCopy = right.DeepClone();
            int length = leftCopy._literals.Count();

            do
            {
                for (int leftIndex = 0; leftIndex < length; leftIndex++)
                {
                    Literal leftL = leftCopy._literals[leftIndex];

                    for (int rightIndex = 0; rightIndex < length; rightIndex++)
                    {
                        Literal rightL = rightCopy.GetLiteralFrom(leftL.Char);

                        int newExponent = leftL.Exponent + rightL.Exponent;
                        newListLiterals.Add(new Literal(leftL.Char, newExponent));

                        leftCopy._literals.Remove(leftL);
                        rightCopy._literals.Remove(rightL);
                        length--;
                    }
                }
            } while (!leftCopy.Literals.IsEmpty());


            if(!leftCopy._literals.IsEmpty())
            {
                foreach(Literal remaining in  leftCopy.Literals)
                {
                    newListLiterals.Add(remaining);
                }
            }

            if(!rightCopy._literals.IsEmpty())
            {
                foreach (Literal remaining in rightCopy.Literals)
                {
                    newListLiterals.Add(remaining);
                }
            }
            int newCoefficient = left.Coefficient * right.Coefficient;
            Monomial toReturn =  new Monomial(newCoefficient, newListLiterals);
            toReturn = toReturn.NormalizeOnlyNumbers();
            return toReturn;
        }
        private static Monomial Divide(Monomial left, Monomial right, out int reminder)
        {
            int newCoefficient = left.Coefficient / right.Coefficient;
            reminder = left.Coefficient % right.Coefficient;

            left._coefficient = 1;
            right._coefficient = 1;
            right = right.ToNegativeExponents();

            Monomial resultExponents = left * right;

            resultExponents._coefficient = newCoefficient;

            return resultExponents;
        }
        #endregion
  
        public Monomial OrderAlphabetically()
        {
            //Bubble Sort
            LinkedDoubleCircularList<Literal> copy = this._literals.DeepClone();
            int length = copy.Count();

            bool thereWasSwaps;
            do
            {
                thereWasSwaps = false;

                for (int i = 0; i < length-1; i++)
                {
                    if (copy[i].Char > copy[i+1].Char)
                    {
                        char temChar = copy[i].Char;
                        copy[i].Char = copy[i + 1].Char;
                        copy[i + 1].Char = temChar;
                        thereWasSwaps = true;
                    }
                }

            } while (thereWasSwaps);

            return new Monomial(this.Coefficient, copy);
        }
        private void CopyOnlyLiteralsCharsFrom(LinkedDoubleCircularList<Literal> copyFrom)
        {
            this._literals = copyFrom.DeepClone();
            foreach(Literal l in _literals)
            {
                l.Exponent = 0;
            }
        }
        public static bool HaveSameDeterminant(Monomial left, Monomial right)
        {
            bool areSimilar = true;

            if (left._literals.Count() != right._literals.Count())
            {
                areSimilar = false;
            }
            else
            {
                for (int i = 0; i < left.Literals.Count(); i++)
                {
                    Literal leftL = left.Literals[i];
                    Literal rightL = right.Literals[i];

                    if (leftL.Char != rightL.Char || leftL.Exponent != rightL.Exponent)
                    {
                        areSimilar = false;
                    }
                }
            }

            return areSimilar;
        }
        public static Monomial Parse(string stringTerm)
        {
            string stringTermNoSpaces = stringTerm.Replace(" ", "");
            LinkedDoubleCircularList<string> splittedTerm = stringTermNoSpaces.Split('*', StringSplitOptions.RemoveEmptyEntries).ToDoubleCircularList();
            if (!splittedTerm.Any()) throw new PolynomialException("Term doesnt have any terms");

            Monomial termToReturn = new();

            bool inputComesWithCoefficient = false;
            foreach (string pieceTerm in splittedTerm)
            {
                if (int.TryParse(pieceTerm, out int result))
                {
                    termToReturn._coefficient = result;
                    inputComesWithCoefficient = true;
                }
                else
                {
                    //This divides X^15 in   X, 15   and creates a new literal from that
                    LinkedDoubleCircularList<string> splittedByExponent = pieceTerm.Split('^', StringSplitOptions.RemoveEmptyEntries).ToDoubleCircularList();

                    //This separates sign from letter in cases like: -a,  -a^4 (to make it 1 or -1 by default)
                    if (splittedByExponent[0].Contains('-'))
                    {
                        termToReturn._coefficient = -1;
                        splittedByExponent[0] = splittedByExponent[0].Replace("-", "");
                    }
                    else
                    {
                        if (!inputComesWithCoefficient)
                        {
                            termToReturn._coefficient = 1;
                        }
                    }

                    if (splittedByExponent.Count() == 2)
                    {
                        char newChar = splittedByExponent[0][0];
                        int exponent = int.Parse(splittedByExponent[1]);
                        Literal newLiteral = new Literal(newChar, exponent);

                        termToReturn._literals.Add(newLiteral);
                    }
                    else if (splittedByExponent.Count() == 1) //Means this is just a letter : "Y" so ----[0] contains the unique element of this circular list
                    {
                        char letterLiteral = splittedByExponent[0][0];
                        Literal newLiteral = new Literal(letterLiteral, 1);

                        termToReturn._literals.Add(newLiteral);
                    }
                    else
                    {
                        throw new PolynomialException("Wrong Format - Something Strange Happened...");
                    }
                }
            }


            if (termToReturn._literals.IsEmpty())
                termToReturn._literals.Add(new Literal());

            termToReturn._degree = termToReturn.FindDegree();
            return termToReturn;
        }
        private Literal GetLiteralFrom(char charsito)
        {
            foreach (Literal lit in _literals)
            {
                if (lit.Char == charsito)
                {
                    return lit;
                }
            }

            return new Literal();
        }
        public Literal GetHighestLiteral()
        {
            Literal highest = new();

            foreach(Literal lit in _literals)
            {
                if(lit.Exponent > highest.Exponent)
                {
                    highest = lit.DeepClone();
                }
            }

            return highest;
        }
        public int GetHighestExponent()
        {
            Literal highest = new();

            foreach (Literal lit in _literals)
            {
                if (lit.Exponent > highest.Exponent)
                {
                    highest = lit.DeepClone();
                }
            }

            return highest.Exponent;
        }
        public int GetExponentFrom(char charsito)
        {
            foreach (Literal lit in _literals)
            {
                if (lit.Char == charsito)
                {
                    return lit.Exponent;
                }
            }

            return 0;
        }
        public Monomial NormalizeOnlyNumbers()
        {
            LinkedDoubleCircularList<Literal> cloneList = this._literals.DeepClone();

            foreach (Literal l in cloneList)
            {
                if (l.IsEmptyLiteral())
                {
                    cloneList.Replace(l, new Literal());
                }
            }

            Monomial toReturn = new Monomial(this.Coefficient, cloneList);
            return toReturn;
        }
        public Monomial ToNegativeExponents()
        {
            Monomial copyMon = this.DeepClone();
            foreach(Literal lit in copyMon.Literals)
            {
                lit.ToNegativeExponent();
            }
            return copyMon;
        }
        private bool HasDefaultLiteral()
        {
            return _literals.Count() == 1 && _literals[0].IsEmptyLiteral();
        }
        public Monomial ToNegative()
        {
            Monomial copyMon = this.DeepClone();
            copyMon._coefficient *= -1;
            return copyMon;
        }
        public Monomial Simplify()
        {
            Monomial monoCopy = this.DeepClone();
            int length = monoCopy.Literals.Count();
            LinkedDoubleCircularList<Literal> newLiterals = new();

            for (int lit1Index = 0; lit1Index < length; lit1Index++)
            {
                Literal lit1 = monoCopy.Literals[lit1Index];
                for (int lit2Index = 0; lit2Index < length; lit2Index++)
                {
                    Literal lit2 = monoCopy.Literals[lit2Index];

                    if(lit1.Char == lit2.Char && lit1Index != lit2Index)
                    {
                        int newExponent = lit1.Exponent + lit2.Exponent;
                        Literal newLit = new Literal(lit1.Char, newExponent);
                        monoCopy._literals.Remove(lit1);
                        monoCopy._literals.Remove(lit2);
                        monoCopy._literals.Add(newLit);

                        length--;
                    }
                }
            }


            return new Monomial(this.Coefficient, monoCopy.Literals);
        }
        private int FindDegree()
        {
            int maxExponent = 0;
            foreach (Literal lit in _literals)
            {
                if (lit.Exponent > maxExponent)
                {
                    maxExponent = Math.Abs(lit.Exponent);
                }
            }
            return maxExponent;
        }
        public string ToHumanizeString()
        {
            if(Coefficient == 0)
            {
                return "";
            }
            else
            {
                string output = String.Empty;

                if(Coefficient == -1)
                {
                    output += "-";
                }
                else
                if(Coefficient != 1)
                {
                    output += $"{Coefficient}";
                }

                bool beforeWasExponent = false;

                foreach (Literal lit in Literals)
                {
                    //lit.Exponent == 0 is already handled by literalclass

                    if(lit.Exponent == 1)
                    {
                        if(beforeWasExponent)
                        {
                            output += $"*{lit.ToHumanizeString()}";
                        }
                        else
                        {
                            output += $"{lit.ToHumanizeString()}";
                        }
                        beforeWasExponent = false;
                    }
                    else
                    {
                        beforeWasExponent = true;
                        output += $"*{lit.ToHumanizeString()}";
                    }
                }

                return output;
            }
        }
        public override string ToString()
        {
            string output = $"{Coefficient}";

            foreach (Literal lit in Literals)
            {
                output += $"*{lit.ToString()}";
            }

            return output;
        }
    }
}