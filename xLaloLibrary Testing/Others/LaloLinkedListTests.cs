using FluentAssertions;
using LaloLibrary.DataStructures;

namespace xLaloLibrary_Testing
{
    public class LaloLinkedListTests
    {
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