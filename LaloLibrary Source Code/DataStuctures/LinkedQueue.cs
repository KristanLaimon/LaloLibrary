namespace LaloLibrary.DataStructures
{
    public class LinkedQueue<T>
    {
        private Node<T> front;
        private Node<T> back;

        public LinkedQueue()
        { }

        public void Enqueue(T data)
        {
            Node<T> newNode = new Node<T>(data);

            if (IsEmpty())
            {
                front = back = newNode;
            }
            else
            {
                back.NextNode = newNode;
                back = newNode;
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

        public bool IsEmpty()
        {
            return front == null;
        }

    }
}