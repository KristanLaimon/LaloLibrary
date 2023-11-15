global using Xunit;
using System.Text.RegularExpressions;
using FluentAssertions;
using LaloLibrary.DataStuctures;
using LaloLibrary.Maths;

namespace Maths
{
    public class TPolynomials
    {
        [Theory]
        [InlineData("X^2+Y", "Z^2-3", "1*X^2, 1*Y^1, 1*Z^2, -3*X^0")]
        [InlineData("X", "X", "1*X^1, 1*X^1")]
        private void Concat(string poly1, string poly2, string expected)
        {
            Polynomial p1 = Polynomial.Parse(poly1);
            Polynomial p2 = Polynomial.Parse(poly2);

            LinkedDoubleCircularList<Monomial> termsTogether = new LinkedDoubleCircularList<Monomial>()
            {
                p1.Monomials,
                p2.Monomials
            };

            Polynomial concatenaded = new Polynomial(termsTogether);
            concatenaded.ToString().Should().ContainAll(expected);
        }

        [Theory]
        [InlineData("X^2+Y", "Z^2-3", "1*X^2, 1*Y^1, 1*Z^2, -3*X^0")]
        [InlineData("2*X^2", "2*X^2", "4*X^2")]
        [InlineData("2*X^4", "1*X^4", "3*X^4")]
        [InlineData("1*X^1", "1*X^1+1*Y^1", "1*Y^1, 2*X^1")]
        [InlineData("1*X^0", "1*X^0", "2*X^0")]
        [InlineData("x", "X", "2*X^1")]
        [InlineData("2*X^4", "-1*X^4", "1*X^4")]
        [InlineData("2*X^4", "-5*X^4", "-3*X^4")]
        [InlineData("2*X^4+Y", "-5*X^4+Y", "-3*X^4, 2*Y^1")]
        [InlineData("2*x+2*x", "2*x", "6*X^1")]
        private void Sum(string poly1, string poly2, string sumExpected)
        {
            Polynomial p1 = Polynomial.Parse(poly1);
            Polynomial p2 = Polynomial.Parse(poly2);

            Polynomial sumResult = p1 + p2;
            sumResult.ToString().Should().BeEquivalentTo(sumExpected);
        }

        [Theory]
        [InlineData("x", "X", "0", "0")]
        [InlineData("2*x", "1*X", "1*X^1", "X")]
        [InlineData("2*x", "-1*X", "3*X^1", "3x")]
        [InlineData("-2*x", "-1*X", "-1*X^1", "-x")]
        [InlineData("-2*x", "1*X", "-3*X^1", "-3x")]
        [InlineData("-2*x+Z", "X", "1*Z^1, -3*X^1", "z-3x")]
        [InlineData("-2*x", "X+Z", "-1*Z^1, -3*X^1", "-z-3x")]
        [InlineData("2*x+2*x", "2*x+y", "-1*Y^1, 2*X^1", "-y+2x")]
        private void Substract(string poly1, string poly2, string sumExpected, string humanWay)
        {
            Polynomial p1 = Polynomial.Parse(poly1);

            Polynomial p2 = Polynomial.Parse(poly2);

            Polynomial sumResult = p1 - p2;
            sumResult.ToString().Should().BeEquivalentTo(sumExpected);
            sumResult.ToHumanizeString().Should().BeEquivalentTo(humanWay);
        }

        [Theory]
        [InlineData("2*x+2*x^2", "2*X^2, 2*X^1")]
        private void OrganizeByExp(string poly1, string exp)
        {
            Polynomial p1 = Polynomial.Parse(poly1);
            p1 = p1.SortByExponent();
            p1.ToString().Should().Be(exp);
        }

        [Theory]
        [InlineData("X^2+Y", 2)]
        [InlineData("2*X^2", 2)]
        [InlineData("2*X^4", 4)]
        [InlineData("1*X^1", 1)]
        [InlineData("1*X^0", 0)]
        [InlineData("x", 1)]
        [InlineData("2*X^4+Y", 4)]
        [InlineData("2*x+2*x", 1)]
        private void Degreee(string poly1, int degreeExpected)
        {
            Polynomial p1 = Polynomial.Parse(poly1);

        }

        //[Theory]
        //[InlineData("1-a-a^5-3*a^2", "1+2*a+a^2", "-1*A^3, 2*A^2, -3*A^1, 1*X^0", "0*X^0")]
        //[InlineData("12*a^3+33*a*b^2-35*a^2*b-10*b^3", "4*a-5*b", "3*X^2, -5*A^1*B^1, 2*B^2", "0*X^0")]
        //private void Divide(string poly1, string poly2, string expectedCocient, string expectedReminder)
        //{
        //    Polynomial p1 = Polynomial.Parse(poly1);
        //    Polynomial p2 = Polynomial.Parse(poly2);

        //    Polynomial cocient = p1 / p2;
        //    Polynomial reminder = p1 % p2;

        //    cocient.ToString().Should().Be(expectedCocient);
        //    reminder.ToString().Should().Be(expectedReminder);
        //}

        [Theory]
        [InlineData("5*x^2+3*Y^1-5*Z^1", "5*x^2+3y-5z")]
        private void ToHumanizeString(string polyString, string humanWay)
        {
            Polynomial poly = Polynomial.Parse(polyString);
            poly.ToHumanizeString().Should().Be(humanWay);
        }

        [Theory]
        [InlineData("-2*m^2*n+3*m", "-5*m+4*m^2*n-6", "22*M^3*N, -8*M^4*N^2, 12*M^2*N, -15*M^2, 18*M^1")]
        private void Multiply(string poly1, string poly2, string result)
        {
            Polynomial p1 = Polynomial.Parse(poly1);
            Polynomial p2 = Polynomial.Parse(poly2);
            Polynomial pResult = p1 * p2;
            pResult = pResult.SortByExponent();
        }

        [Theory]
        [InlineData("3*d*c*b*a-2*x*b", "3*A^1*B^1*C^1*D^1, -2*B^1*X^1")]
        private void SortByABCeveryMonomial(string poly, string expected)
        {
            Polynomial test = Polynomial.Parse(poly);
            test = test.SortByABCeveryMonomial();
            test.ToString().Should().Be(expected);
        }

        [Theory]
        [InlineData("12*a^3+33*a*b^2-35*a^2*b-10*b^3", "12*A^3, -35*A^2*B^1, 33*A^1*B^2, -10*B^3")]
        private void OrganizeByExponentLetter(string poly1, string expectedSort)
        {
            Polynomial p1 = Polynomial.Parse(poly1);
            p1 = p1.SortByExponentLetter('A');
            p1.ToString().Should().Be(expectedSort);
        }

        [Fact]
        private void TestingParseAlone()
        {
            string regex = "(([\\-+]?)(\\d+)\\*([a-zA-Z]{1})\\^(\\d+))()(\\*([\\-+]?)(\\d+)\\*([a-zA-Z]{1})\\^(\\d+))*";
            Regex regexPattern = new Regex(regex);

            MatchCollection collection = regexPattern.Matches("12*a^3+33*a*b^2-35*a^2*b-10*b^3");
            int number = collection.Count;
            Polynomial test = Polynomial.Parse("123*X^234*+123*X^234");
        }


    }
}