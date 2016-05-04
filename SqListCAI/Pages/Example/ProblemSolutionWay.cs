using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqListCAI.Pages.Example
{
    class ProblemSolutionWay
    {
        public static string ORDER_MERGE = 
            "    依次扫描A和B元素，比较当前元素的值，将较小值的元素赋给C，\n"+
            "如此直到一个线性表扫描完毕，然后未完的那个顺序表中余下\n"+
            "部分赋给C即可。其中C能够容纳A、B两个线性表相加的长度。\n"+
            "算法的时间性能是O(m+n)，其中m,n分别为A和B的表长。";
        public static string LINKED_REVERSE = 
            "    依次取原链表中的每个节点，将其作为第一个节点插入到新的\n"+
            "链表中去，引用变量p来指向原表中当前节点，P为空时结束。\n"+
            "该算法只是对链表中顺序扫描一遍即完成了逆置，所以时间性能为O(n)。";
    }
}
