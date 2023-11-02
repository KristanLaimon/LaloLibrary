namespace LaloLibrary.DataStructures
{
    internal class LinkedQueue<T>
    {
        private Node<T> front;

        public LinkedQueue()
        { }

        public void Enqueue(T data)
        {
            Node<T> newNode = new Node<T>(data);

            if (IsEmpty())
            {
                front = newNode;
            }
            else
            {
                Node<T> lastNode = GetLastNode();
                lastNode.NextNode = newNode;
            }
        }

        public T Dequeue()
        {
            if (IsEmpty())
            {
                throw new Exception("Cannot dequeue from an empty dinamic queue");
            }
            else
            {
                T valueToReturn = front.Data;
                front = front.NextNode;
                return valueToReturn;
            }
        }

        private Node<T> GetLastNode()
        {
            Node<T> pointerNode = front;

            while (pointerNode.NextNode != null)
            {
                pointerNode = pointerNode.NextNode;
            }

            return pointerNode;
        }

        public bool IsEmpty()
        {
            return front == null;
        }
    }
}