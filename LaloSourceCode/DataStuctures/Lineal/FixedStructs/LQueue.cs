namespace LaloLibrary.DataStructures
{
    //Circular Queue
    internal class LQueue<T>
    {
        private T[] array;
        private int first = -1;
        private int last = -1;
        private int count;

        public LQueue(uint size)
        {
            array = new T[size];
        }

        public void Enqueue(T value)
        {
            if (CheckIfFull())
            {
                throw new Exception("La cola está llena no se puede utilizar 'Enqueue'");
            }
            else
            {
                if (last == array.Length - 1) //array.Length-1 = Max
                {
                    last = 0;
                    count++;
                    array[last] = value;
                }
                else
                {
                    if (first == -1) first = 0;

                    count++;
                    last++;
                    array[last] = value;
                }
            }
        }

        public T Dequeue()
        {
            if (CheckIfEmpty())
            {
                throw new Exception("La cola está vacía, no se puede usar Dequeue a ella");
            }
            else
            {
                if (first == array.Length - 1)//array.Length-1 = Max
                {
                    T temp = array[first];
                    array[first] = default(T);
                    first = 0;
                    count--;
                    return temp;
                }
                else
                {
                    T temp = array[first];
                    array[first] = default(T);
                    first++;
                    count--;
                    return temp;
                }
            }
        }

        public bool CheckIfFull()
        {
            return count == array.Length;
        }

        public bool CheckIfEmpty()
        {
            return count == 0;
        }
    }
}