using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AK_Project_33_Generics
{
    public class MyLinkedListNode<T>
        where T : IComparable
    {
        public T Value { get; set; }
        public MyLinkedListNode<T> Next;
        public MyLinkedListNode<T> Previous;
        public MyLinkedList<T> List;

        //Конструкторы
        public MyLinkedListNode() { }
        public MyLinkedListNode(T value, MyLinkedListNode<T> next, MyLinkedListNode<T> previous)
        {
            Value = value;
            Next = next;
            Previous = previous;
        }
        public MyLinkedListNode(T value)
        {
            Value = value;
            /*Next = null;
            Previous = null;*/
        }
        public MyLinkedListNode(MyLinkedList<T> list, T value)
        {
            List = list;
            Value = value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
