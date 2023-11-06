using LaloLibrary.DataStructures;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Windows.Forms;

namespace LaloLibrary.DataStuctures
{
    public class LinkedCircularList<T>
    {
        private Node<T> first, last;

        public LinkedCircularList()
        { }

        public void Add(params T[] values)
        {
            foreach(T value in values)
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
            if(!IsEmpty())
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
            
            }while( pointer != first);
            return false;
        }
        
        public T[] MakeToArray()
        {
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
            Node<T> pointer = first;
            Node<T> beforePointer = null;

            do
            {
                if(pointer.Data.Equals(value))
                {
                    if(beforePointer == null)
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
                if(IsEmpty())
                {
                    return;
                }
                else
                if(pointer == last && pointer == first)
                {
                    Remove(pointer.Data);
                    last = first = null;
                }
                else if(pointer == last)
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
    }
}