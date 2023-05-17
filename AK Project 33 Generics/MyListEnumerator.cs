using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AK_Project_33_Generics
{
    public class MyListEnumerator<T>: IEnumerator
        where T : IComparable
    {
        public MyLinkedList<T> EList ;
        public int position = 0;
        public MyLinkedListNode<T> temp;

        //Конструктор
        public MyListEnumerator(MyLinkedList<T> list)
        {
            EList = list;
            temp = EList.First;
        }

        //Реализация интерфейса:
        public bool MoveNext()
        {
            position++;
            if (position != 1)
            {
                temp = temp.Next;
            }
            return position <= EList.Count;
        }
        public void Reset()
        {
            position = -1;
            temp = EList.First;
        }
        public object Current
        {
            get
            {
                if (position == 0 || temp == null)
                    throw new ArgumentException();
                return temp.Value;
            }
        }
    }
}
