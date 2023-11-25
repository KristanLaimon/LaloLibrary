namespace LaloLibrary.DataStuctures
{
    internal class DualNode<T>
    {
        internal DualNode<T> NextNode;
        internal DualNode<T> BackNode;
        internal T Data;

        public DualNode()
        { }

        public DualNode(T data)
        {
            Data = data;
        }
    }
}