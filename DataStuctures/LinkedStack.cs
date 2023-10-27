using System.Text;

namespace LaloLibrary.DataStructures
{
    public class LinkedStack<T>
    {
        private short count;
        private Node<T> topNode;

        public LinkedStack()
        { }

        public void Push(T newContent)
        {
            if (topNode == null)
                topNode = new Node<T>(newContent);
            else
            {
                topNode = new Node<T>(newContent, topNode);
            }
            count++;
        }

        public T Pop()
        {
            if (CheckIfEmpty()) throw new Exception("La pila ya está vacía y no se puede hacer Pop de otro más");

            count--;
            T returnValue = topNode.Data;
            topNode = topNode.NextNode;
            return returnValue;
        }

        public string MakeToString()
        {
            StringBuilder sb = new();

            LinkedStack<T> copyStack = this.Clone();

            int fixedCount = count;
            for (int i = 0; i < fixedCount; i++)
                sb.Append(copyStack.Pop());

            return sb.ToString();
        }

        public LinkedStack<T> Clone()
        {
            LinkedStack<T> temp = new();
            LinkedStack<T> output = new();

            int fixedCount = count;
            for (int i = 0; i < fixedCount; i++)
                temp.Push(this.Pop());

            for (int i = 0; i < fixedCount; i++)
            {
                T actual = temp.Peek();
                output.Push(actual);
                this.Push(actual);
                temp.Pop();
            }

            return output;
        }

        public Node<T> PeekNode() => topNode;

        public T Peek() => topNode.Data;

        public void Clear()
        {
            count = 0;
            topNode = null;
        }

        public bool CheckIfEmpty() => topNode == null ? true : false;

        public int Count() => count;
    }
}