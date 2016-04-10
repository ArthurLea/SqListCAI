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
            "Status ListInsert_Sq(SqList &L, int i, ElemType e) { ",
            "  // 在顺序线性表L的第i个元素之前插入新的元素e，",
            "  // i的合法值为1≤i≤ListLength_Sq(L)+1",
            "  ElemType *p;",
            "  if (i < 1 || i > L.length+1) return ERROR;  // i值不合法",
            "  if (L.length >= L.listsize) {   // 当前存储空间已满，增加容量",
            "    ElemType* newbase = (ElemType*)realloc(L.elem,",
            "                  (L.listsize + LISTINCREMENT) * sizeof(ElemType));",
            "    if (!newbase) return ERROR;   // 存储分配失败",
            "    L.elem = newbase;             // 新基址",
            "    L.listsize += LISTINCREMENT;  // 增加存储容量",
            "  }",
            "  ElemType* q = &(L.elem[i - 1]);   // q为插入位置",
            "  for (p = &(L.elem[L.length - 1]); p>=q; --p) *(p+1) = * p;",
            "                                  // 插入位置及之后的元素右移",
            "  * q = e;       // 插入e",
            "  ++L.length;   // 表长增1",
            "  return OK;",
            "} // ListInsert_Sq"
        };
        public static string[] INSERT_VALUE = new string[]
        {
            
        };
        public static string[] DELETE_CODE = new string[]
        {
            "",
            "",
            "",

        };
    }
}