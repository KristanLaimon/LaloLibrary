using LaloLibrary.DataStructures;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaloLibrary.DataStuctures
{
    //The challenge is make it with only one Node<T> reference node!
    public class LinkedLaloList<T>
    {
        Node<T> first;

        public LinkedLaloList()
        { }

        public void Add(T newData)
        {
            Node<T> newNode = new Node<T>(newData);
            if(IsEmpty())
            {
                first = newNode;
            }
            else
            {
                Node<T> tempPointer = first;
                while(tempPointer.NextNode != null)
                {
                    tempPointer = tempPointer.NextNode;
                }
                tempPointer.NextNode = newNode;
            }
        }

        public bool RemoveFirstOne(T dataToRemove)
        {
            Node<T> tempPointer = first;
            Node<T> beforeTempPointer = new();

            while (tempPointer != null)
            {
                if (tempPointer.Data.Equals(dataToRemove))
                {
                    if(tempPointer == first)
                    {
                        first = tempPointer.NextNode;
                    }
                    else
                    {
                        beforeTempPointer.NextNode = tempPointer.NextNode;
                    }

                    return true;
                }

                beforeTempPointer = tempPointer;
                tempPointer = tempPointer.NextNode;
            }

            return false;
        }

        public int Count()
        {
            Node<T> tempPointer = first;
            int count = 0;
            while( tempPointer != null)
            {
                count++;
                tempPointer = tempPointer.NextNode;
            }

            return count;
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

        public bool IsEmpty()
        {
            return first == null;
        }
    }
}
