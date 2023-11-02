namespace LaloLibrary.DataStructures
{
    internal class PNode<T>
    {
        internal int priority;
        internal T data;
        internal PNode<T> nextNode;

        internal PNode(T data, int priority)
        {
            this.data = data;
            this.priority = priority;
        }
    }
}