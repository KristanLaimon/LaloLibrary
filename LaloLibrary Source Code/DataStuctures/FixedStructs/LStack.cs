namespace LaloLibrary.DataStuctures
{
    internal class LStack<T>
    {
        private T[] stack;
        private int top;

        public T[] Stack { get => stack; }
        public int Top { get => top; }

        public LStack()
        {
            stack = new T[5];
            top = -1;
        }

        public LStack(int capacity)
        {
            stack = new T[capacity];
            top = -1;
        }

        public void Push(T newInt)
        {
            if (CheckIfFull()) throw new Exception("overflow exception en krstackint");

            top++;
            Stack[Top] = newInt;
        }

        public T Pop()
        {
            if (CheckIfEmpty()) throw new Exception("SubOverflow exception, no hay elementos para quitar de la pila");

            T temp = Stack[Top];
            Stack[Top] = default(T);
            top--;
            return temp;
        }

        public T Peek()
        {
            if (Top != -1)
            {
                return Stack[Top];
            }
            throw new Exception("No hay elementos del cual hacer peek");
        }

        private bool CheckIfEmpty()
        {
            if (Top == -1) return true;
            return false;
        }

        private bool CheckIfFull()
        {
            if (Top == Stack.Length - 1) return true;
            return false;
        }
    }
}