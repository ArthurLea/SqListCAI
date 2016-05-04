using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqListCAI.Pages.Example
{
    class ProblemCode
    {
        public static string[] ORDER_MERGE =
        {
            "void merge(SqList A,SqList B,SqList C) {",
            "   int i,j,k;",
            "   i = 0;j = 0;k = 0;",
            "   while((i<A.length) && (j<B.length))",
            "       if(A.data[i] < B.data[j])",
            "           C.data[k++] = A.data[i++];",
            "       else C.data[k++] = A.data[j++];",
            "   while(i<A.length)",
            "       C.data[k++] = A.data[i++];",
            "   while(j<B.length)",
            "       C.data[k++] = B.data[i++];",
            "   C.length = k",
            "}"
        };
        public static string[] LINKED_REVERSE =
        {
            "void reverse(LinkedList H) {",
            "   LinkedNode p,q;",
            "   p = H.head.next;",
            "   H.head.next = null;",
            "   while(p) {",
            "       q = p;",
            "       p = p.next;",
            "       q.next = H.head.next;",
            "       H.head.next = q;",
            "   }",
            "}"
        };
    }
}
