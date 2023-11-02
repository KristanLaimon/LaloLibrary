using FluentAssertions;
using LaloLibrary.DataStructures;

namespace xLaloLibrary_Testing
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            LinkedStack<string> queue = new();
            queue.Should().BeOfType<LinkedStack<string>>();
            //first library testing!
        }
    }
}