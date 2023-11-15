using FluentAssertions;
using LaloLibrary.Maths;

namespace Maths
{
    public class TMonomial
    {
        [Theory]
        [InlineData("-288*X^15", new[] { 'X' }, new[] { 15 }, -288)]
        [InlineData("-288*X^15*Y^2*Z^1", new[] { 'X', 'Y', 'Z' }, new[] { 15, 2, 1 }, -288)]
        [InlineData("1*T^1", new[] { 'T' }, new[] { 1 }, 1)]
        [InlineData("-1*T^1", new[] { 'T' }, new[] { 1 }, -1)]
        [InlineData("T", new[] { 'T' }, new[] { 1 }, 1)]
        [InlineData("-T", new[] { 'T' }, new[] { 1 }, -1)]
        [InlineData("3", new[] { 'X' }, new[] { 0 }, 3)]
        [InlineData("-3", new[] { 'X' }, new[] { 0 }, -3)]
        [InlineData("-a", new[] { 'A' }, new[] { 1 }, -1)]
        [InlineData("-a^5", new[] { 'A' }, new[] { 5 }, -1)]

        public void Parse(string input, char[] expectedChars, int[] expectedExponents, int expectedCoefficient)
        {
            var expectedLiterals = new List<Literal>();

            for (int i = 0; i < expectedChars.Length; i++)
            {
                expectedLiterals.Add(new Literal(expectedChars[i], expectedExponents[i]));
            }

            Monomial term = Monomial.Parse(input);
            term.Literals.Should().BeEquivalentTo(expectedLiterals);
            term.Coefficient.Should().Be(expectedCoefficient);
        }

        [Theory]
        [InlineData("X^2", "Z^2", false)]
        [InlineData("X", "Z", false)]
        [InlineData("X", "X", true)]
        [InlineData("2*X^2", "2*X^2", true)]
        [InlineData("2*X^4", "1*X^4", true)]
        [InlineData("1*X^1", "1*Y^1", false)]
        [InlineData("1*X^0", "1*X^0", true)]
        private void SimilarTerms(string poly1, string poly2, bool boolExpected)
        {
            Monomial m1 = Monomial.Parse(poly1);
            Monomial m2 = Monomial.Parse(poly2);

            Monomial.HaveSameDeterminant(m1, m2).Should().Be(boolExpected);
        }

        [Theory]
        [InlineData("2*X^2", "-2*X^2")]
        [InlineData("2*X^4", "-2*X^4")]
        [InlineData("1*Y^1", "-1*Y^1")]
        [InlineData("1*X^0", "-1*X^0")]
        [InlineData("X", "-1*X^1")]
        private void ToNegative(string monomialString, string negativeMonomial)
        {
            Monomial mon = Monomial.Parse(monomialString);
            mon = mon.ToNegative();
            mon.ToString().Should().Be(negativeMonomial);
        }

        [Theory]
        [InlineData("2*X^2", "-2*X^2", "-4*X^4")]
        [InlineData("2*X^4", "-2*X^4", "-4*X^8")]
        [InlineData("1*Y^1", "-1*Y^1", "-1*Y^2")]
        [InlineData("1*X^0", "-1*X^0", "-1*X^0")]
        [InlineData("X", "-1*X^1", "-1*X^2")]
        [InlineData("5*X^1*Y^1", "5*X^1", "25*X^2*Y^1")]
        [InlineData("5*X^1*Y^1*Y^1", "5*X^1", "25*X^2*Y^2")]
        [InlineData("1*X^0", "-3*A^1", "-3*A^1")]
        [InlineData("-1*X^0", "-3*A^1", "3*A^1")]
        [InlineData("-2*M^2*N^1", "4*M^2*N^1", "-8*M^4*N^2")]
        private void Multiply(string monomialString, string monomialString2, string expected)
        {
            Monomial mon = Monomial.Parse(monomialString);
            Monomial mon2 = Monomial.Parse(monomialString2);

            Monomial result = mon * mon2;
            result.ToString().Should().Be(expected);
        }

        [Theory]
        [InlineData("5*X^1*X^1*X^1", "5*X^3")]
        [InlineData("5*X^1*Y^1*Y^1", "5*X^1*Y^2")]
        [InlineData("5*X^1*Y^1*Z^1", "5*X^1*Y^1*Z^1")]
        private void Simplify(string monomialString, string expected)
        {
            Monomial mon = Monomial.Parse(monomialString);
            mon = mon.Simplify();
            mon.ToString().Should().Be(expected);
        }

        [Theory]
        [InlineData("X^2", "1*X^-2")]
        [InlineData("2*X^2", "2*X^-2")]
        [InlineData("2*X^-2", "2*X^2")]
        [InlineData("1*X^4", "1*X^-4")]
        [InlineData("1*X^-4", "1*X^4")]
        [InlineData("1*X^1", "1*X^-1")]
        [InlineData("1*X^0", "1*X^0")]
        private void ToNegativeExponents(string poly1, string expected)
        {
            Monomial m1 = Monomial.Parse(poly1);
            m1 = m1.ToNegativeExponents();

            m1.ToString().Should().Be(expected);
        }

        [Theory]
        [InlineData("X^2", "1*X^-2", "1*X^4")]
        [InlineData("2*X^2", "2*X^-2", "1*X^4")]
        [InlineData("2*X^-2", "2*X^2", "1*X^-4")]
        [InlineData("1*X^4", "1*X^-4", "1*X^8")]
        [InlineData("1*X^-4", "1*X^4", "1*X^-8")]
        [InlineData("1*X^1", "1*X^-1", "1*X^2")]
        [InlineData("1*X^0", "1*X^0", "1*X^0")]
        private void Divide(string poly1, string poly2, string expected)
        {
            Monomial m1 = Monomial.Parse(poly1);
            Monomial m2 = Monomial.Parse(poly2);
            Monomial result = m1 / m2;

            result.ToString().Should().Be(expected);
        }

        [Theory]
        [InlineData("2*X", "2x")]
        [InlineData("15*x*Y", "15xy")]
        [InlineData("5*x^3*Y^1", "5*x^3*y")]
        private void ToHumanizeString(string monomial, string humanWay)
        {
            Monomial monTest = Monomial.Parse(monomial);
            monTest.ToHumanizeString().Should().Be(humanWay);
        }

        [Theory]
        [InlineData("0*A^0", "0*X^0")]
        private void ReplaceEmptyLiterals(string monString, string expected)
        {
            Monomial test = Monomial.Parse(monString);
            test = test.NormalizeOnlyNumbers();
            test.ToString().Should().Be(expected);
        }

        [Theory]
        [InlineData("3*d*c*b*a", "3*A^1*B^1*C^1*D^1")]
        private void OrderLiteralsAlphabetically(string monomial, string expected)
        {
            Monomial test = Monomial.Parse(monomial);
            test = test.OrderAlphabetically();
            test.ToString().Should().Be(expected);
        }


        [Theory]
        [InlineData("3*d^-1*c^1*b^2*a^4", 'A')]
        [InlineData("3*d^-1*c^1*b^2*a^1", 'B')]
        [InlineData("3*d^-1*c^8*b^2*a^1", 'C')]
        private void GetHighestLiteral(string monomial, char charExpected)
        {
            Monomial test = Monomial.Parse(monomial);

            Literal highest = test.GetHighestLiteral();
            highest.Char.Should().Be(charExpected);
        }

    }
}