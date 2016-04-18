using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqListCAI.Utils.SourceCodes
{
    class SearchCodes
    {
        public static string[] ORDER_SEARCH = 
        {

            "int Search_Seq(SSTable ST, KeyType key) {  // 算法9.1",
           "// 在顺序表ST中顺序查找其关键字等于key的数据元素。",
           "// 若找到，则函数值为该元素在表中的位置，否则为0。",
           "    ST.elem[0].key=key;   //哨兵",
           "    for (i=ST.length;  ST.elem[i].key!=key;  --i);  // 从后往前找",
           "    return i;      // 找不到时，i为0",
           "} // Search_Seq"
        };
        public static string[] ORDER_SEARCH_VALUE =
        {
            "srcData",
            "SearchData",
            "索引i",
            "length"
        };


        public static string[] BINARY_SEARCH =
        {
            "int Search_Bin ( SSTable ST, KeyType key ) {  // 算法9.2",
            "// 在有序表ST中折半查找其关键字等于key的数据元素。",
            "// 若找到，则函数值为该元素在表中的位置，否则为0。",
            "   low = 1;  high = ST.length;    // 置区间初值",
            "   while (low <= high) {",
            "       mid = (low + high) / 2;",
            "       if (EQ(key , ST.elem[mid].key)) return mid;    // 找到待查元素",
            "       else if (LT(key, ST.elem[mid].key)) high = mid - 1; // 继续在前半区间进行查找",
            "       else low = mid + 1;    // 继续在后半区间进行查找",
            "   }",
            "   return 0;                 // 顺序表中不存在待查元素",
             "} // Search_Bin",
        };
        public static string[] BINARY_SEARCH_VALUE =
        {
            "srcData",
            "SearchData",
            "low",
            "mid",
            "high",
            "length"
        };
    }
}
