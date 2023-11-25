using LaloLibrary.DataStructures.Lineal;
using LaloLibrary.DataStuctures;
using LaloLibrary.Maths;
using LaloLibrary.Utils;

namespace Utils
{
    public class TClonablesd
    {
        [Fact]
        private void DobleCircularList()
        {
            LinkedDoubleCircularList<Person> people = new LinkedDoubleCircularList<Person>
            {
                new Person { Name = "Alice", Age = 25 },
                new Person { Name = "Bob", Age = 30 },
                new Person { Name = "Charlie", Age = 22 }
            };

            LinkedDoubleCircularList<Person> reff = people;
            LinkedDoubleCircularList<Person> cloned = people.DeepClone();

            int algo = 5;
            algo.DeepClone();
        }

        [Fact]
        private void Test()
        {
            Literal[] lit = { new Literal('G', 4) };
            Monomial mon = new Monomial(5, lit.ToDoubleCircularList());
            Monomial monCopy = mon.DeepClone();

            if (lit.Equals(monCopy))
            {
                string yayyyy = "";
            }
        }
    }

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public override string ToString()
        {
            return $"{Name}, {Age} years old";
        }
    }
}