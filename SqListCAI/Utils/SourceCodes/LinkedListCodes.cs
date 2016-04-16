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
    }
}
