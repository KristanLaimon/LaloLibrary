using FluentAssertions;
using LaloLibrary.DataStructures;
using LaloLibrary.Maths;

namespace DataStructures
{
    public class LaloLinkedListTests
    {
        [Fact]
        public void test()
        {
            Monomial monomio = Monomial.Parse("2*X^2");
            Monomial monomi2 = Monomial.Parse("3*X^2");
            Monomial monomi3 = Monomial.Parse("5*X^2");

            Monomial result = monomio + monomi2 + monomi3;
        }


        [Fact]
        public void LaloLinkedList_RemoveFirstOne_ShouldWorkProperly()
        {
            LinkedLaloList<int> list = new();
            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);
            list.Add(5);

            list.RemoveFirstOne(1).Should().BeTrue();
            list.MakeToArray().Should().BeEquivalentTo(
                new int[]
                {
                    2,3,4,5
                }
                );

            list.RemoveFirstOne(2).Should().BeTrue();
            list.MakeToArray().Should().BeEquivalentTo(
                new int[]
                {
                    3,4,5
                }
                );

            list.RemoveFirstOne(5).Should().BeTrue();
            list.MakeToArray().Should().BeEquivalentTo(
                new int[]
                {
                    3,4
                }
                );

            list.RemoveFirstOne(10).Should().BeFalse();
        }
    }
}