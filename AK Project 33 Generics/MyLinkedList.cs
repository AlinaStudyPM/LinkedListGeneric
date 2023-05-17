using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AK_Project_33_Generics
{
    public class MyLinkedList<T>: IEnumerable<T>, IComparable<MyLinkedList<T>>, ICloneable, ICollection<T>
        where T: IComparable
    {
        public MyLinkedListNode<T> First;
        public MyLinkedListNode<T> Last;
        public int Count = 0;

        public MyLinkedList() { }
        public MyLinkedList(T[] array) //Создание списка из массива
        {
            foreach (T i in array)
            {
                this.AddLast(i);
            }
        }
        public MyLinkedList(ICollection<T> collection) //Реализовать интерфейс IEnumarable
        {
            foreach (T item in collection)
            {
                AddLast(item);
                Count++;
            }
        }

        public void AddFirst(T value) //Добавление элемента в начало
        {
            MyLinkedListNode<T> NewFirst = new MyLinkedListNode<T>(value);
            NewFirst.List = this;
            if (Count == 0)
            {
                First = NewFirst;
                Last = NewFirst;
            }
            else
            {
                First.Previous = NewFirst;
                NewFirst.Next = First;
                First = NewFirst;
            }
            Count++;
        }
        public void AddLast(T value) //добавление элемента в конец списка
        {
            MyLinkedListNode<T> NewLast = new MyLinkedListNode<T>(value);
            NewLast.List = this;
            if (Count == 0)
            {
                First = NewLast;
                Last = NewLast;
                Count++;
            }
            else
            {
                Last.Next = NewLast;
                NewLast.Previous = Last;
                Last = NewLast;
                Count++;
            }
        }
        public override string ToString() //Все элементы в строчку
        {
            if (Count == 0) return "";
            StringBuilder result = new StringBuilder();
            MyLinkedListNode<T> temp = First;
            while (temp.Next != null)
            {
                result.Append(temp.Value.ToString() + " ");
                temp = temp.Next;
            }
            result.Append(temp.Value.ToString());
            return result.ToString();
        }
        public void RemoveFirst() //Удаление первого элемента
        {
            if (Count == 1)
            {
                First = null;
                Last = null;
                Count = 0;
            }
            if (Count > 1)
            {
                First.List = null;
                First = First.Next;
                First.Previous = null;
                Count--;
            }
        }
        public void RemoveLast() //Удаление последнего элемента
        {
            if (Count == 1)
            {
                First = null;
                Last = null;
                Count = 0;
            }
            if (Count > 1)
            {
                Last.List = null;
                Last = Last.Previous;
                Last.Next = null;
                Count--;
            }
        }
        public void Remove(MyLinkedListNode<T> Node) //Удаление элемента списка
        {
            if (Node.List != this)
            {
                return;
            }
            Node.List = null;

            if (Node.Previous != null) { Node.Previous.Next = Node.Next; }
            else { First = Node.Next; }

            if (Node.Next != null) { Node.Next.Previous = Node.Previous; }
            else { Last = Node.Previous; }

            Count--;

        }
        public void Reverse() //Разворот списка
        {
            MyLinkedListNode<T> current = Last;
            while (current.Previous != null)
            {
                MyLinkedListNode<T> temp = current.Previous;
                current.Previous = current.Next;
                current.Next = temp;
                current = current.Previous;
            }
            Last.Next = First.Previous;
            Last.Previous = null;
            First = Last;
            Last = current;
            Last.Previous = Last.Next;
            Last.Next = null;
        }
        public void AddBefore(MyLinkedListNode<T> NodeInList, MyLinkedListNode<T> NodeToAdd) //Добавляет новый элемент перед каким-то из списка
        {
            if (NodeToAdd.List == this)
            {
                throw new InvalidOperationException("The LinkedList node belongs a LinkedList");
            }
            NodeToAdd.List = this;
            NodeToAdd.Next = NodeInList;
            NodeToAdd.Previous = NodeInList.Previous;
            NodeInList.Previous.Next = NodeToAdd;
            NodeInList.Previous = NodeToAdd;
            Count++;
        }
        public void AddBefore(MyLinkedListNode<T> NodeInList, T value) //Добавляет элемент с заданным значением перед каким-то из списка
        {
            MyLinkedListNode<T> NodeToAdd = new MyLinkedListNode<T>(value);
            AddBefore(NodeInList, NodeToAdd);
        }
        public void AddAfter(MyLinkedListNode<T> NodeInList, MyLinkedListNode<T> NodeToAdd)
        {
            if (NodeToAdd.List == this)
            //if (Find(NodeToAdd.Value).Value != null)
            {
                throw new InvalidOperationException("The LinkedList node belongs a LinkedList");
            }
            NodeToAdd.List = this;
            NodeToAdd.Previous = NodeInList;
            NodeToAdd.Next = NodeInList.Next;
            NodeInList.Next.Previous = NodeToAdd;
            NodeInList.Next = NodeToAdd;
            Count++;
        }
        public void AddAfter(MyLinkedListNode<T> NodeInList, T value)
        {
            MyLinkedListNode<T> NodeToAdd = new MyLinkedListNode<T>(value);
            AddAfter(NodeInList, NodeToAdd);
        }
        public void Clear() //Удаляет все элементы из списка
        {
            First = null;
            Last = null;
            Count = 0;
        }
        public void CopyTo(T[] array, int k) //Копирует список в массив, начиная с k-ого элемента
        {
            MyLinkedListNode<T> temp = First;
            int currentInd = 0;
            while (temp.Next != null)
            {
                if (currentInd >= k)
                {
                    array[currentInd - k] = temp.Value;
                }
                temp = temp.Next;
                currentInd++;
            }
            array[currentInd - k] = temp.Value;
        }

        public bool Remove(T value) //Удаление элемента по значению
        {
            try
            {
                Remove(Find(value));
                return true;
            }
            catch
            {
                return false;
            }
        }
        public MyLinkedListNode<T> Find(T value) //Поиск элемента по значению
        {
            MyLinkedListNode<T> temp = First;
            if (First == null)
            {
                temp = new MyLinkedListNode<T>();
                return temp;
            }
            while (temp.Next != null && !Equals(temp.Value, value))
            {
                temp = temp.Next;
            }
            if (Equals(temp.Value, value))
            {
                return temp;
            }
            temp = new MyLinkedListNode<T>();
            return temp;
        }
        public MyLinkedListNode<T> FindLast(T value) //Поиск элемента по значению с конца
        {
            MyLinkedListNode<T> temp = Last;
            while (temp.Previous != null)
            {
                if (Equals(temp.Value, value))
                {
                    return temp;
                }
                temp = temp.Previous;
            }

            temp = new MyLinkedListNode<T>();
            return temp;
        }
        public bool Contains(T value) //Определяет, есть ли заданный элемент в списке
        {
            if (Find(value).Value != null)
            {
                return true;
            }
            return false;
        }
        public MyLinkedList<T> Clone() //Клонирование списка
        {
            MyLinkedList<T> newMyLinkedList = new MyLinkedList<T>();
            foreach (var i in this)
            {
                newMyLinkedList.AddLast((T)i);
            }
            return newMyLinkedList;
        }
        public void Sort(IComparer<T> comparator)
        {
            MyLinkedListNode<T> temp1 = First;
            MyLinkedListNode<T> temp2;
            for (int i = 0; i < Count; i++)
            {
                if (i > 0) { temp1 = temp1.Next; }
                temp2 = temp1;
                for (int j = i; j > 0; j--)
                {
                    if (j < i) { temp2 = temp2.Previous; }
                    if (comparator.Compare(temp2.Value, temp2.Previous.Value) < 0)
                    {
                        T tempValue = temp2.Value;
                        temp2.Value = temp2.Previous.Value;
                        temp2.Previous.Value = tempValue;
                    }
                }
            }
        }




        //Интерфейсы
        public IEnumerator GetEnumerator()
        {
            return (IEnumerator)new MyListEnumerator<T>(this);
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return (IEnumerator<T>)GetEnumerator();
        }
        public int CompareTo(MyLinkedList<T> list)
        {
            /*if (list.Count < Count) return 1;
            if (list.Count > Count) return -1;
            return 0;*/
            return Count - list.Count;
        }
        
        object ICloneable.Clone()
        {
            return this.Clone();
        }
        void ICollection<T>.Add(T value)
        {
            AddLast(value);
        }
        int ICollection<T>.Count
        {
            get{ return Count; }
        }
        bool ICollection<T>.IsReadOnly
        {
            get { return false; }
        }
    }
}
