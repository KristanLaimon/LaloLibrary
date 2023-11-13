using LaloCloning;
using LaloLibrary.DataStructures;
using LaloLibrary.DataStructures.Lineal;
using LaloLibrary.DataStuctures;
using LaloLibrary.Exceptions;

namespace LaloLibrary.Maths
{
    public class Polynomial
    {
        private LinkedDoubleCircularList<Monomial> monomials;
        private int degree;

        internal Polynomial() {
            degree = 0;
            monomials = new();
        }
        public Polynomial(LinkedDoubleCircularList<Monomial> terms)
        {
            this.monomials = terms;
            degree = GetDegree();
        }
        public Polynomial(params Monomial[] monomials)
        {
            LinkedDoubleCircularList<Monomial> tempList = new();

            foreach(Monomial mon in monomials)
            {
                
                tempList.Add(mon);
            }

            this.monomials = tempList;
            degree = GetDegree();
        }

        public LinkedDoubleCircularList<Monomial> Monomials { get { return monomials; } }
        public int Degree { get { return degree; } }

        #region Operators Methods
        public static Polynomial operator +(Polynomial left, Polynomial right)
        {
            return Sum(left, right);
        }
        public static Polynomial operator -(Polynomial left, Polynomial right)
        {
            return Substract(left, right);
        }
        public static Polynomial operator /(Polynomial left, Polynomial right)
        {
            return Divide(left, right, out Polynomial reminder);
        }
        public static Polynomial operator %(Polynomial left, Polynomial right)
        {
            Divide(left, right, out Polynomial reminder);
            return reminder;
        }
        public static Polynomial operator *(Polynomial left, Polynomial right)
        {
            return Multiply(left, right);
        }
        private static Polynomial Sum(Polynomial left, Polynomial right)
        {
            left = left.Simplify();
            right = right.Simplify();

            Polynomial bothTogether = Polynomial.Concat(left, right);
            bothTogether = bothTogether.Simplify();
            bothTogether.RemoveEmptyMonomials();

            return bothTogether;
        }
        private static Polynomial Substract(Polynomial left, Polynomial right)
        {
            right = right.ToNegative();
            return left + right;
        }
        private static Polynomial Divide(Polynomial dividend, Polynomial divisor, out Polynomial remainder)
        {
			if (dividend == null) throw new ArgumentNullException(nameof(dividend));
            if (divisor == null) throw new ArgumentNullException(nameof(divisor));

            dividend = dividend.SortByExponent();
            dividend = dividend.SortByABCeveryMonomial();

            divisor = divisor.SortByExponent();
            divisor = divisor.SortByABCeveryMonomial();

            if(divisor.Degree > dividend.Degree)
            {
                remainder = new Polynomial();
                return dividend.DeepClone();
            }

            int rightDegree = divisor.Degree;
            int quotientDegree = (dividend.Degree - rightDegree) + 1;
            divisor = divisor.SortByExponent();
            int leadingCoefficient = divisor.monomials[rightDegree].Coefficient;

            Polynomial rem = dividend.DeepClone();
            Polynomial quotient = new Polynomial();

            for (int i = quotientDegree - 1; i >= 0; i--)
            {
                quotient[i] = rem[rightDegree + i] / leadingCoefficient;
                quotient.SortByExponent();
                rem[rightDegree + i] = 0;

                for (int j = rightDegree + i -1; j >= i; j--)
                {
                    rem[j] = rem[j] - (quotient[i] * divisor[j - i]);
                    rem = rem.SortByExponent();
                }
            }

            rem.RemoveEmptyMonomials();
            quotient.RemoveEmptyMonomials();

            remainder = rem.DeepClone();
            return quotient.DeepClone();
        }
        private static Polynomial Multiply(Polynomial left, Polynomial right)
        {
            LinkedDoubleCircularList<Monomial> all = new();

            foreach(Monomial leftM in left.monomials)
            {
                foreach(Monomial rightM in right.monomials)
                {
                    all.Add(leftM * rightM);
                }
            }

            Polynomial polynomial = new Polynomial(all);
            polynomial = polynomial.Simplify();
            return polynomial;
        }
        #endregion

        /// <summary>
        /// Gets or sets the coefficient of the term of the specified degree.
        /// </summary>
        /// <param name="actualDegree">Degree of the term to change it's coefficient/param>
        /// <returns></returns>
        public int this[int actualDegree]
        {
            get
            {



                Monomial monomial = monomials.FirstOrDefault(t => t.Degree == actualDegree);

                if(monomial == default(Monomial))
                {
                    return 0;
                }
                else
                {
                    return monomial.Coefficient;
                }
            }
            set
            {
                Monomial monomial = monomials.FirstOrDefault(t => t.Literals.Max(x => x.Exponent) == actualDegree);

                if( monomial == default(Monomial))
                {
                    if(value != 0)
                    {
                        Monomial newMonomial = new Monomial(value, new Literal() { Exponent = actualDegree});
                        monomials.Add(newMonomial);
                    }
                }
                else
                {
                    monomial.Coefficient = value;
                }
            }
        }

        public Monomial GetFirstOrDefaultExponent(int degree)
        {
            foreach (Monomial mon in monomials)
            {
                foreach (Literal l in mon.Literals)
                {
                    if (l.Exponent == degree)
                        return mon;
                }
            }

            return default(Monomial);
        }
        public Polynomial SortByExponentLetter(char charsito)
        {
            charsito = char.ToUpper(charsito);
            LinkedDoubleCircularList<Monomial> thisMons = this.monomials.DeepClone();
            LinkedDoubleCircularList<Monomial> monsFound = new();

            //Find all monomials that contains charsito as literal
            foreach (Monomial mon in thisMons)
            {
                foreach(Literal lit in mon.Literals)
                {
                    if(lit.Char == charsito)
                    {
                        monsFound.Add(mon);
                    }
                }
            }

            //Get what monomials doesnt have charsito
            foreach (Monomial mon in monsFound)
            {
                thisMons.Remove(mon);
            }
            

            //Bubblesort to order monomials by exponent of charsito
            bool thereWasSwaps;
            do
            {
                thereWasSwaps = false;

                for (int i = 0; i < monsFound.Count() - 1; i++)
                {
                    int actualExp = monsFound[i].GetExponentFrom(charsito);
                    int nextExp = monsFound[i + 1].GetExponentFrom(charsito);

                    if (actualExp < nextExp)
                    {
                        Monomial temp = monsFound[i];
                        monsFound[i] = monsFound[i + 1];
                        monsFound[i + 1] = temp;
                        thereWasSwaps = true;
                    }
                }

            } while (thereWasSwaps);

            //Convert and organize polynomials, to return the good gone
            Polynomial orderedCharsitos = new Polynomial(monsFound);
            Polynomial orderedNotCharsitos = new Polynomial(thisMons).SortByExponent().SortByABCeveryMonomial();
            Polynomial finalResult =  Polynomial.Concat(orderedCharsitos, orderedNotCharsitos);
            return finalResult;
        }
        public Polynomial SortByABCeveryMonomial()
        {
            LinkedDoubleCircularList<Monomial> thisMonomials = this.Monomials.DeepClone();

            for (int i = 0; i < thisMonomials.Count(); i++)
            {
                thisMonomials[i] = thisMonomials[i].OrderAlphabetically();
            }

            return new Polynomial(thisMonomials);
        }
        public static Polynomial Concat(params Polynomial[] arrayPoly)
        {
            LinkedDoubleCircularList<Monomial> termsTogether = new LinkedDoubleCircularList<Monomial>();

            foreach (Polynomial poly in arrayPoly)
            {
                termsTogether.Add(poly.Monomials);
            }

            return new Polynomial(termsTogether);
        }
        public static Polynomial Parse(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) { throw new PolynomialException("Input String is Empty"); }

            string inputString = input.Replace(" ", "").Replace("-", "+-");

            LinkedDoubleCircularList<string> splittedTerms = inputString.Split('+', StringSplitOptions.RemoveEmptyEntries).ToDoubleCircularList();

            LinkedDoubleCircularList<Monomial> listTerms = new();
            foreach (string splittedTerm in splittedTerms)
            {
                Monomial convertedTerm = Monomial.Parse(splittedTerm);
                listTerms.Add(convertedTerm);
            }

            Polynomial toReturn = new Polynomial(listTerms);
            toReturn.degree = toReturn.GetDegree();
            return toReturn;
        }

        [Obsolete("It wasn't needed after all...")]
        public Polynomial NormalizeOnlyNumbers()
        {
            LinkedDoubleCircularList<Monomial> thisClone = this.monomials.DeepClone();
            for (int i = 0; i < thisClone.Count(); i++)
            {
                thisClone[i] = thisClone[i].NormalizeOnlyNumbers();
            }
            return new Polynomial(thisClone);
        }
        public Polynomial SortByExponent()
        {

            LinkedDoubleCircularList<Monomial> monsCopy = this.monomials.DeepClone();
            LinkedQueue<Monomial> organized = new();

            while (!monsCopy.IsEmpty())
            {
                Monomial highestMonExp = new();

                foreach (Monomial actualMon in monsCopy)
                {
                    if (actualMon.Degree >= highestMonExp.Degree)
                    {
                        highestMonExp = actualMon;
                    }
                }

                organized.Enqueue(highestMonExp.DeepClone());
                monsCopy.Remove(highestMonExp);
            }

            LinkedDoubleCircularList<Monomial> toReturnKeepedOrder = new();

            while (!organized.IsEmpty())
            {
                toReturnKeepedOrder.Add(organized.Dequeue());
            }

            return new Polynomial(toReturnKeepedOrder);
        }
        private void RemoveEmptyMonomials()
        {
            bool thereWasEliminations;
            do
            {
                thereWasEliminations = false;
                foreach (Monomial m in this.monomials)
                {
                    if (m.Coefficient == 0)
                    {
                        this.monomials.Remove(m);
                        thereWasEliminations = true;
                        break;
                    }
                }

            } while (thereWasEliminations);
        }
        public Polynomial ToNegative()
        {
            Polynomial copyThis = this.DeepClone();

            for (int i = 0; i < copyThis.monomials.Count(); i++)
            {
                copyThis.monomials[i] = copyThis.monomials[i].ToNegative();
            }

            return copyThis;
        }
        public Polynomial Simplify()
        {
            Polynomial copyPoly = this.DeepClone();
            LinkedDoubleCircularList<Monomial> monomialsSimplified = new();

            bool thereWereChanges;
            int length = copyPoly.monomials.Count();

            do
            {
                thereWereChanges = false;
                for (int actualTermIndex = 0; actualTermIndex < length; actualTermIndex++)
                {
                    Monomial left = copyPoly.monomials[actualTermIndex];

                    for (int comparedTermIndex = 0; comparedTermIndex < length; comparedTermIndex++)
                    {
                        Monomial right = copyPoly.monomials[comparedTermIndex];

                        if (Monomial.HaveSameDeterminant(left, right) && actualTermIndex != comparedTermIndex)
                        {
                            Monomial newTerm = left + right;

                            length -= 1;
                            copyPoly.monomials.Remove(left);
                            copyPoly.monomials.Remove(right);
                            monomialsSimplified.Add(newTerm);
                            thereWereChanges = true;
                        }
                    }

                    if (!monomialsSimplified.IsEmpty())
                    {
                        copyPoly.monomials.Add(monomialsSimplified);
                        monomialsSimplified.Clear();
                    }
                }
            } while (thereWereChanges);
                
            return copyPoly;
        }
        private int GetDegree()
        {
            int maxDegree = 0;
            foreach(Monomial m in this.monomials)
            {
                if(m.Degree > maxDegree)
                {
                    maxDegree = m.Degree;
                }
            }
            return maxDegree;
        }
        private bool IsEmpty()
        {
            return monomials.Count() == 0;
        }
        public string ToHumanizeString()
        {
            if (this.IsEmpty())
            {
                return "0";
            }
            else
            {
                string output = String.Empty;

                foreach (Monomial m in this.monomials)
                {
                    //There should not be any 0 coefficient monomial
                    if (m.Coefficient > 0)
                    {
                        output += "+" + m.ToHumanizeString();
                    }
                    else if(m.Coefficient < 0)
                    {
                        output += m.ToHumanizeString();
                    }
                }

                if(output == String.Empty)
                {
                    output = "0";
                }

                if (output.StartsWith("+"))
                {
                    output = output.Remove(0, 1);
                }

                if (output.EndsWith("*"))
                {
                    output = output.Remove(output.Length - 1);
                }
                
                return output;
            }
        }
        public override string ToString()
        {
            if(monomials.IsEmpty())
            {
                return "0";
            }
            else
            {
                return monomials.ToString();

            }
        }
    }
}

