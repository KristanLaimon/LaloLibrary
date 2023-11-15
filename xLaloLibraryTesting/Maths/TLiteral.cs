using FluentAssertions;
using LaloLibrary.Maths;

namespace Maths
{
    public class TLiteral
    {
        [Theory]
        [InlineData('x', 'X')]
        private void CharToUpper(char inputChar, char expectedChar)
        {
            Literal lit = new Literal(inputChar, 1);

            lit.Char.Should().Be(expectedChar);
        }

        [Theory]
        [InlineData('x', 'X', true)]
        [InlineData('y', 'X', false)]
        [InlineData('t', 'X', false)]
        [InlineData('z', 'z', true)]
        [InlineData('g', 'G', true)]
        private void AreSimilar(char inputChar, char inputChar2, bool expected)
        {
            Literal lit = new Literal(inputChar, 1);
            Literal lit2 = new Literal(inputChar2, 4);
            Literal.AreSimilar(lit, lit2).Should().Be(expected);
        }

        [Theory]
        [InlineData('x', 1, "x")]
        [InlineData('y', -3, "y^-3")]
        [InlineData('t', 0, "")]
        [InlineData('z', 5, "z^5")]
        private void ToHumanize(char inputChar, int exponent, string humanWay)
        {
            Literal lit = new Literal(inputChar, exponent);
            lit.ToHumanizeString().Should().Be(humanWay);
        }
    }
}