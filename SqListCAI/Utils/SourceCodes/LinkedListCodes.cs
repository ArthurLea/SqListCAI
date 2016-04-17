using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqListCAI.Utils.SourceCodes
{
    public partial class LinkedListCodes
    {
        public static string[] CREATE_CODE =
        {
            "void CreateList_L(LinkList &L,int n) {",
            "   //输入N个元素的值，建立带表头结点的单链表线性表L(尾差法)",
            "   L = (LinkList) malloc(sizeof(LNode)) L->next = NULL;",
            "   for(i=0;i<n;++i) {",
            "       p = (LinkList) malloc(sizeof(LNode));",
            "       scanf(&p->data);",
            "       p->next = L->next;",
            "       L->next = p;",
            "   }",
            "}//CreateList_L"
        };
        public static string[] CREATE_VALUE =
        {
            "La(头结点)",//指向头结点
            "a",//当前输入的数据域值（一个数组）
            "n",//数据域的长度，当前链表中除头结点的个数
            "p",//指向值为？的结点（是当前插入的结点）
            "i"//索引变量
        };
        public static string[] INSERT_CODE =
        {
            "Status ListInsert_L(LinkList &L, int i, ElemType e) {  // 算法2.9",
            "// 在带头结点的单链线性表L的第i个元素之前插入元素e",
            "    LinkList p,s;",
            "    p = L;",
            "    int j = 0;",
            "    while (p && j<i-1) {  // 寻找第i-1个结点",
            "        p = p->next;",
            "        ++j;",
            "    }",
            "    if (!p || j > i-1) return ERROR;      // i小于1或者大于表长",
            "    s = (LinkList)malloc(sizeof(LNode));  // 生成新结点",
            "    s->data = e;",  
            "    s->next = p->next;      // 插入L中",
            "    p->next = s;",
            "    return OK;",
            "} // ListInsert_L",
        };
        public static string[] INSERT_VALUE =
        {
            "La(头结点)",
            "a",
            "n",
            "P",
            "j",
            "i",
            "e",
            "S"
        };
        public static string[] DELETE_CODE =
        {

        };
        public static string[] DELETE_VALUE =
        {

        };
    }
}
