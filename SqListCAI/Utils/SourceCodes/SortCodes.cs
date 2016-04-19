using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqListCAI.Utils.SourceCodes
{
    class SortCodes
    {
        public static string[] INSERT_SORT_CODE =
            {
                "void InsertSort(SqList &L){  // 算法10.1",
                "// 对顺序表L作直接插入排序。",
                "for (i = 2; i <= L.length; ++i)",
                "   if (LT(L.r[i].key, L.r[i - 1].key)){ // '<'时，需将L.r[i]插入有序子表",
                "        L.r[0] = L.r[i];                 // 复制为哨兵",
                "        for (j = i - 1; LT(L.r[0].key, L.r[j].key); --j)",
                "            L.r[j + 1] = L.r[j];             // 记录后移",
                "        L.r[j + 1] = L.r[0];               // 插入到正确位置",
                "    }",
                "} // InsertSort"
             };
        public static string[] INSERT_SORT_VALUE =
            {
                "R",//源数据
                "length",//待排序的长度
                "i",//外部循环索引
                "j",//内部循环索引
                "R[0]"//监视哨
            };
        public static string[] SWAP_SORT_CODE =
            {
                "void BubbleSort(ListType R)",
                "   for(i=1;i<R.lenth;i++)",
                "       {   swap=0;",
                "           for(j=1;j<R.length-i;j++)",
                "               if(R[j] > R[j+1])//将大的往后甩",
                "               { R[0] = R[j+1];",
                "                 R[j+1] = R[j];",
                "                 R[j] = R[0]",
                "                 swap = 1;",
                "               }",
                "           if(swap == 0) break;",
                "       }"
            };
        public static string[] SWAP_SORT_VALUE =
            {
                "R",//源数据
                "length",//待排序的长度
                "i",//外部循环索引
                "j",//内部循环索引
                "R[0]",//临时变量存储，作为交换的介质
                "swap"//轮询一次是否有交换
            };
        public static string[] FAST_SORT_CODE =
            {
                ""
            };
        public static string[] FAST_SORT_VALUE =
            {

            };
    }
}
