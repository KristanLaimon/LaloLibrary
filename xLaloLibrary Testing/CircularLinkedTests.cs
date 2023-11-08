using FluentAssertions;
using Polynomials_Evaluator.CañedoDependencies;

namespace xLaloLibrary_Testing
{
    public class CircularLinkedTests
    {
        LinkedCircularList<int> circularList = new();

        [Fact]
        private void Add()
        {
            circularList.Add(1, 2, 3);

            circularList.MakeToArray().Should().BeEquivalentTo(
               new int[]
               {
                    1,2,3
               });
        }

        [Theory]
        [InlineData(new int[] { 1, 2, 3, 4, 5 }, 3, new int[] { 1, 2, 4, 5 }, true)]
        [InlineData(new int[] { 1, 2, 3, 4, 5 }, 5, new int[] { 1, 2, 3, 4 }, true)]
        [InlineData(new int[] { 1, 2, 3, 4, 5 }, 1, new int[] { 2, 3, 4, 5 }, true)]
        [InlineData(new int[] { 1, 2, 3, 4, 5 }, 6, new int[] { 1, 2, 3, 4, 5 }, false)]
        private void Remove(int[] input, int remove, int[] output, bool shouldbe)
        {
            circularList.Add(input);
            circularList.Remove(remove).Should().Be(shouldbe);
            circularList.MakeToArray().Should().BeEquivalentTo(output);
        }

        [Theory]
        [InlineData(new int[] { 1, 2, 3, 4, 5 },new int[] { 1, 2, 3, 4})]
        [InlineData(new int[] { 1, 2, 3},new int[] { 1, 2})]
        [InlineData(new int[] { 1 },new int[] { })]
        [InlineData(new int[] {},new int[] {})]
        private void RemoveLast(int[] input, int[] output)
        {
            circularList.Add(input);
            circularList.RemoveLast();
            circularList.MakeToArray().Should().BeEquivalentTo(output);
        }

        [Theory]
        [InlineData(new int[] { 1, 2, 3, 4, 5 }, 4, 100, new int[] { 1, 2, 3, 100, 4, 5 }, true)]
        [InlineData(new int[] { 1, 2, 3, 4, 5 }, 5, 100, new int[] { 1, 2, 3, 4, 100, 5 }, true)]
        [InlineData(new int[] { 1, 2, 3, 4, 5 }, 1, 100, new int[] { 100, 1, 2, 3, 4, 5 }, true)]
        [InlineData(new int[] { 1, 2, 3, 4, 5 }, 7, 100, new int[] { 1, 2, 3, 4, 5 }, false)]
        private void AddBefore(int[] input, int numberToStop, int numberBehing, int[] output, bool boolean)
        {
            circularList.Add(input);
            circularList.AddBefore(numberToStop, numberBehing).Should().Be(boolean);
            circularList.MakeToArray().Should().BeEquivalentTo(output);
        }

        [Theory]
        [InlineData(new int[] { 1, 2, 3, 4, 5 }, 4, 100, new int[] { 1, 2, 3, 4, 100, 5 }, true)]
        [InlineData(new int[] { 1, 2, 3, 4, 5 }, 5, 100, new int[] { 1, 2, 3, 4, 5, 100 }, true)]
        [InlineData(new int[] { 1, 2, 3, 4, 5 }, 1, 100, new int[] { 1, 100, 2, 3, 4, 5 }, true)]
        [InlineData(new int[] { 1, 2, 3, 4, 5 }, 7, 100, new int[] { 1, 2, 3, 4, 5 }, false)]
        private void AddAfter(int[] input, int numberToStop, int numberAfter, int[] output, bool boolean)
        {
            circularList.Add(input);
            circularList.AddAfter(numberToStop, numberAfter).Should().Be(boolean);
            circularList.MakeToArray().Should().BeEquivalentTo(output);
        }

        [Theory]
        [InlineData(new int[] { 1, 2, 3, 4, 5 }, 4, true)]
        [InlineData(new int[] { 1, 2, 3, 4, 5 }, 5, true)]
        [InlineData(new int[] { 1, 2, 3, 4, 5 }, 7, false)]
        [InlineData(new int[] { 1, 2, 3, 4, 5 }, 1, true)]
        [InlineData(new int[] { 1, 2, 3, 4, 5 }, 0, false)]
        private void Contains(int[] input, int numberToFind, bool boolean)
        {
            circularList.Add(input);
            circularList.Contains(numberToFind).Should().Be(boolean);
        }

    }
}