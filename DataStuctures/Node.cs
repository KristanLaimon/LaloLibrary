namespace LaloLibrary.DataStructures
{
    public class Node<T>
    {
        public T Data { get; set; }
        public Node<T> NextNode { get; set; }

        public Node()
        { }

        public Node(T Dato)
        {
            Data = Dato;
        }

        public Node(T Dato, Node<T> Liga) : this(Dato)
        {
            NextNode = Liga;
        }
    }
}