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
                "R",//源数据，其中R[0]为监视哨
                "length",//待排序的长度
                "i",//外部循环索引
                "j",//内部循环索引
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
                "R",//源数据，其中R[0]作为临时变量存储，也就是交换的介质
                "length",//待排序的长度
                "i",//外部循环索引
                "j",//内部循环索引
                "swap"//轮询一次是否有交换
            };
        public static string[] FAST_SORT_CODE_QUICK =
            {
                "void QSort(SqList &L, int low, int high)",
                "{  //算法10.7",
                "   // 对顺序表L中的子序列L.r[low..high]进行快速排序",
                "    if (low < high){ // 长度大于1",
                "        pivotloc = Partition(L, low, high);  // 将L.r[low..high]一分为二",
                "        QSort(L, low, pivotloc - 1); // 对低子表递归排序，pivotloc是枢轴位置",
                "        QSort(L, pivotloc + 1, high); }         // 对高子表递归排序",
                "} // QSort",
             };
        public static string[] FAST_SORT_CODE_PARTITION =
            {
               "int Partition(SqList &L, int low, int high) {  // 算法10.6(b)",
               "// 交换顺序表L中子序列L.r[low..high]的记录，使枢轴记录到位，",
               "// 并返回其所在位置，此时，在它之前（后）的记录均不大（小）于它",
               "    L.r[0] = L.r[low];            // 用子表的第一个记录作枢轴记录",
               "    pivotkey = L.r[low].key;      // 枢轴记录关键字",
               "    while (low<high) {            // 从表的两端交替地向中间扫描",
               "        while (low<high && L.r[high].key>=pivotkey) --high;",
               "            L.r[low] = L.r[high];      // 将比枢轴记录小的记录移到低端",
               "        while (low<high && L.r[low].key<=pivotkey) ++low;",
               "            L.r[high] = L.r[low];      // 将比枢轴记录大的记录移到高端",
               "    }",
               "     L.r[low] = L.r[0];            // 枢轴记录到位",
               "    return low;                   // 返回枢轴位置",
            "} // Partition"
            };
        public static string[] FAST_SORT_VALUE =
            {
                "R",//源数据,其中R[0]临时变量存储，作为交换的介质
                "length",//待排序的长度
                "low",//low
                "high",//high
                "pivotkey",
                "pivotloc"
            };
    }
}
