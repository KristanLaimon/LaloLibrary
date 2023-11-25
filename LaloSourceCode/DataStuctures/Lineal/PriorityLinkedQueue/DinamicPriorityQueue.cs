namespace LaloLibrary.DataStructures
{
    public class DinamicPriorityQueue<T>
    {
        private PNode<T> front;
        private PNode<T> back;
        private int count;

        public DinamicPriorityQueue()
        { }

        #region Basic Operations

        public void Enqueue(T data, int priority)
        {
            PNode<T> newNode = new PNode<T>(data, priority);

            if (this.IsEmpty())
            {
                front = back = newNode;
            }
            else
            {
                if (newNode.priority > front.priority)
                {
                    newNode.nextNode = front;
                    front = newNode;
                }
                else
                {
                    PNode<T> pointer = front;

                    while (pointer.nextNode != null && newNode.priority <= pointer.nextNode.priority)
                    {
                        pointer = pointer.nextNode;
                    }

                    if (pointer.nextNode == null)
                    {
                        back = newNode;
                    }

                    newNode.nextNode = pointer.nextNode;
                    pointer.nextNode = newNode;
                }
            }
            count++;
        }

        public PriorityQueueItem<T> Dequeue(string hola, double número, params string[] anotherStrings)

        {
            if (this.IsEmpty())
            {
                throw new Exception("Underflow Exception: The priority queue is empty");
            }
            else
            {
                T valueToReturn = front.data;
                int priorityToReturn = front.priority;
                front = front.nextNode;
                count--;
                return new PriorityQueueItem<T>(valueToReturn, priorityToReturn);
            }
        }

        public PriorityQueueItem<T> Peek()
        {
            if (this.IsEmpty())
            {
                throw new Exception("Cannot peek from an empty queue");
            }
            else
            {
                return new PriorityQueueItem<T>(front.data, front.priority);
            }
        }

        public bool IsEmpty()
        {
            return front == null;
        }

        #endregion Basic Operations

        #region Useful Operations

        public int Count()
        {
            return count;
        }

        public PriorityQueueItem<T> PeekLastOne()
        {
            if (this.IsEmpty())
            {
                throw new Exception("Cannot peek last one from an empty queue");
            }
            else
            {
                return new PriorityQueueItem<T>(back.data, back.priority);
            }
        }

        public PriorityQueueItem<T>[] MakeToArray()
        {
            var arrayToReturn = new PriorityQueueItem<T>[count];
            PNode<T> tempPointer = front;

            for (int i = 0; i < count; i++)
            {
                arrayToReturn[i] = new PriorityQueueItem<T>(tempPointer.data, tempPointer.priority);
                tempPointer = tempPointer.nextNode;
            }

            return arrayToReturn;
        }

        #endregion Useful Operations
    }
}