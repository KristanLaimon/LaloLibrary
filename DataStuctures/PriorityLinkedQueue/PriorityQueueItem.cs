namespace LaloLibrary.DataStructures
{
    public struct PriorityQueueItem<T>
    {
        public T data;
        public int priority;

        public PriorityQueueItem(T data, int priority)
        {
            this.data = data;
            this.priority = priority;
        }
    }
}