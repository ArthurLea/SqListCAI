using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqListCAI.Utils.SourceCodes
{
    public partial class SqListCodes
    {
        public static string[] INSERT_CODE = new string[]
        {
            "Status ListInsert_Sq(SqList &L, int i, ElemType e) {  // 算法2.4 ",
            "   // 在顺序线性表L的第insertPosition个元素之前插入新的元素e，",
            "   // i的合法值为1≤i≤ListLength_Sq(L)+1",
            "   ElemType *p;",
            "   if (i < 1 || i > L.length+1)",
            "        return ERROR;  // i值不合法",
            "   if (L.length >= L.MAXSIZE)    // 当前存储空间已满，增加容量",
            "        return ERROR;",
            "   for (p = &(L.elem[L.length-1]);p>=q; --p)",
            "        *(p+1) = *p; // 插入位置及之后的元素右移",
            "   L.elem[L.length-1] = e;       // 插入e",
            "   ++L.length;   // 表长增1",
            "   return OK;",
            "} // ListInsert_Sq"
        };
        public static string[] INSERT_VALUE = new string[]
        {
            "L",//顺序表
            "i",//插入位置
            "insData",//插入数据
            "MAXSIZE",//最大长度
            "p"//当前移动位置
        };
        public static string[] DELETE_CODE = new string[]
        {
            "Status ListDelete_Sq(SqList &L, int i, ElemType &e) {  // 算法2.5",
            "   // 在顺序线性表L中删除第i个元素，并用e返回其值。",
            "   // i的合法值为1≤i≤ListLength_Sq(L)。",
            "   ElemType *p;",
            "   if (i<1 || i>L.length) ",
            "       return ERROR;  // i值不合法",
            "   e = (L.elem[i-1]);                    // 被删除元素的值赋给e",
            "   for (++(p=&(L.elem[i-1])); p<=L.elem+L.length-1; ++p) ",
            "       *(p-1) = *p;     // 被删除元素之后的元素左移",
            "   --L.length;                           // 表长减1",
            "   return OK;",
            "} // ListDelete_Sq "
        };
        public static string[] DELETE_VALUE = new string[]
        {
                "L",//顺序表
                "i",//删除位置
                "e",//删除位置返回的数据
                "MAXSIZE",//最大长度
                "p"//当前移动位置
        };
    }
}