using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqListCAI.Entities
{
    public partial class Explain
    {
        public static string OrderInsExplain = 
            "在线性表的顺序存储结构中，由于逻辑上相邻的数据元素在物理位置上\n" +
            "也相邻，因此，除非插入位置i的值等于表长加1（即插入在表尾之后），\n"+
            "佛足额都必须移动元素才能反映这个逻辑关系的变化。一般情况下，在第\n"+
            "i（1<i<(n+1)）个元素之前插入一个元素时，需将第n个至第i个，共"+
            "n-i+1个元素依次向后移动一个位置。";
        public static string OrderDelExplain =
            "和顺序表插入相反，从顺序表中删除第i（1<i<n）个\n"+
            "元素时，需将从第i+1至第n个，共n-1个元素依次向后移动一个位置";

        public static string LinkedCreExplain = "";
        public static string LinkedInsExplain = "";
        public static string LinkedDelExplain = "";

    }
}
