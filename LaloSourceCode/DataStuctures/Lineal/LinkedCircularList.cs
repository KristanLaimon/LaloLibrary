using LaloLibrary.DataStructures;
using System.Collections;

namespace LaloLibrary.DataStructures
{
    public class LinkedCircularList<T> : IEnumerable<T>
    {
        private Node<T> first, last;

        public LinkedCircularList()
        { }

        public void Add(params T[] values)
        {
            foreach (T value in values)
            {
                Node<T> newNode = new Node<T>(value);

                if (first == null)
                {
                    first = last = newNode;
                    newNode.NextNode = first;
                }
                else
                {
                    newNode.NextNode = first;
                    last.NextNode = newNode;
                    last = newNode;
                }
            }
        }

        public bool AddAfter(T numberToStop, T numberAfter)
        {
            if (IsEmpty()) return false;

            Node<T> pointer = first;

            do
            {
                if (pointer.Data.Equals(numberToStop))
                {
                    Node<T> nextNode = pointer.NextNode;
                    Node<T> newNode = new Node<T>(numberAfter);

                    pointer.NextNode = newNode;
                    newNode.NextNode = nextNode;

                    return true;
                }
                pointer = pointer.NextNode;
            } while (pointer != first);
            return false;
        }

        public bool AddBefore(T numberToStop, T numberBehind)
        {
            if (IsEmpty()) return false;

            Node<T> pointer = first;
            Node<T> beforePointer = null;

            do
            {
                if (pointer.Data.Equals(numberToStop))
                {
                    Node<T> newFirstNode = new Node<T>(numberBehind);

                    if (beforePointer == null)
                    {
                        newFirstNode.NextNode = first;
                        first = newFirstNode;
                        last.NextNode = newFirstNode;
                    }
                    else
                    {
                        beforePointer.NextNode = newFirstNode;
                        newFirstNode.NextNode = pointer;
                    }
                    return true;
                }
                beforePointer = pointer;
                pointer = pointer.NextNode;
            } while (pointer != first);
            return false;
        }

        public int Count()
        {
            if (!IsEmpty())
            {
                Node<T> tempPointer = first;
                int count = 0;

                do
                {
                    count++;
                    tempPointer = tempPointer.NextNode;
                } while (tempPointer != first);

                return count;
            }
            else
                return 0;
        }

        public bool Contains(T value)
        {
            if (IsEmpty()) return false;

            Node<T> pointer = first;
            do
            {
                if (pointer.Data.Equals(value))
                {
                    return true;
                }
                else
                {
                    pointer = pointer.NextNode;
                }
            } while (pointer != first);
            return false;
        }

        public T[] MakeToArray()
        {
            if (IsEmpty()) return new T[0];

            T[] array = new T[Count()];
            Node<T> tempPointer = first;
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = tempPointer.Data;
                tempPointer = tempPointer.NextNode;
            }
            return array;
        }

        public bool Remove(T value)
        {
            if (IsEmpty()) return false;

            Node<T> pointer = first;
            Node<T> beforePointer = null;

            do
            {
                if (pointer.Data.Equals(value))
                {
                    if (beforePointer == null)
                    {
                        first = first.NextNode;
                        last.NextNode = first;
                    }
                    else
                    {
                        beforePointer.NextNode = pointer.NextNode;
                    }
                    return true;
                }
                beforePointer = pointer;
                pointer = pointer.NextNode;
            } while (pointer != first);
            return false;
        }

        public void RemoveLast()
        {
            Node<T> pointer = first;
            Node<T> beforePointer = null;

            do
            {
                if (IsEmpty())
                {
                    return;
                }
                else
                if (pointer == last && pointer == first)
                {
                    Remove(pointer.Data);
                    last = first = null;
                }
                else if (pointer == last)
                {
                    beforePointer.NextNode = first;
                    beforePointer = last;
                }

                beforePointer = pointer;
                pointer = pointer.NextNode;
            } while (pointer != first);
        }

        public bool IsEmpty()
        {
            return first == null && last == null;
        }

        public void Clear() => first = last = null;

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= Count())
                {
                    throw new IndexOutOfRangeException("Index is out of range - Get");
                }

                Node<T> pointer = first;
                for (int i = 0; i < index; i++)
                {
                    pointer = pointer.NextNode;
                }

                return pointer.Data;
            }

            set
            {
                if (index < 0 || index >= Count())
                {
                    throw new IndexOutOfRangeException("Index is out of range - Set");
                }

                Node<T> pointer = first;
                for (int i = 0; i < index; i++)
                {
                    pointer = pointer.NextNode;
                }

                pointer.Data = value;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node<T> current = first;

            if (IsEmpty())
            {
                yield break;
            }

            do
            {
                yield return current.Data;
                current = current.NextNode;
            } while (current != first);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public static LinkedCircularList<T> operator +(LinkedCircularList<T> list1, LinkedCircularList<T> list2)
        {
            list1.Add(list2.MakeToArray());
            return list1;
        }
    }
}