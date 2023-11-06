using LaloLibrary.DataStructures;
using System.Windows.Forms;

namespace LaloLibrary.DataStuctures
{
    public class LinkedCircularList<T>
    {
        private Node<T> beginning, end;

        public LinkedCircularList()
        { }

        public void Add(T value)
        {
            Node<T> newNode = new Node<T>(value);

            if (beginning == null)
            {
                beginning = end = newNode;
                newNode.NextNode = beginning;
            }
            else
            {
                newNode.NextNode = beginning;
                end.NextNode = newNode;
                end = newNode;
            }
        }

        public void AddBefore(T value, T wall)
        {
            Node<T> first;
            Node<T> second;
        }

        public void AddAfter()
        {
        }

        public void Desplegar(ListBox L)
        {
            Node<T> actualNode;
            if (beginning == null)
            {
                throw new Exception("No existe ninguna lista ciruclar");
            }
            else
            {
                actualNode = beginning;
                L.Items.Clear();

                do
                {
                    L.Items.Add(actualNode.Data);
                    actualNode = actualNode.NextNode;
                } while (actualNode != beginning);
            }
        }

        public void DesplegarInverso(ListBox L)
        {
            if (beginning == null)
            {
                throw new Exception("No hay lista");
            }
            else
            {
                Node<T> wall;
                Node<T> pointer;

                L.Items.Clear();
                //Usando los 2 punteros principalmente
            }
        }

        public void Remove(T value)
        {
            Node<T> p, q;
            if (beginning != null)
            {
                p = beginning;
                q = end;

                do
                {
                    if (p.Data.Equals(value))
                    {
                        if (p == beginning)
                        {
                            if (beginning.NextNode == beginning)
                                beginning = end = null;
                            else
                            {
                                beginning = beginning.NextNode;
                                q.NextNode = beginning;
                            }
                        }
                    }
                    else
                    {
                        if (p != end)
                            end = q;
                        q.NextNode = p.NextNode;
                    }

                    p.NextNode = null;
                    p = beginning;
                } while (p != beginning);
            }
        }
    }
}