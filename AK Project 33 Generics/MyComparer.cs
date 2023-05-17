using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AK_Project_33_Generics
{
    public class MyComparer<T>: IComparer<T>
        where T: IComparable
    {
        internal int Compare<T>(T x, T y) where T : IComparable
        {
            return x.CompareTo(y);
        }

        int IComparer<T>.Compare(T? x, T? y)
        {
            return x.CompareTo(y);
        }
    }
}
