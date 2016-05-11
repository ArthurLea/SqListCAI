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
            "   r = L;",
            "   for(i=0;i<n;++i) {",
            "       p = (LinkList) malloc(sizeof(LNode));",
            "       scanf(&p->data);",
            "       r->next = p;",
            "       r = p",
            "   }",
            "}//CreateList_L"
        };
        public static string[] CREATE_VALUE =
        {
            "L",//指向头结点
            "a",//当前输入的数据域值（一个数组）
            "n",//数据域的长度，当前链表中除头结点的个数
            "p",//指向值为？的结点（是当前插入的结点）
            "i",//索引变量
            "r"//中间节点
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
            "L",
            "a",
            "n",
            "p",
            "j",
            "i",
            "e",
            "s"
        };
        public static string[] DELETE_CODE =
        {
            "Status ListDelete_L(LinkList &L, int i, ElemType &e) {  // 算法2.10",
            "   // 在带头结点的单链线性表L中，删除第i个元素，并由e返回其值",
            "   LinkList p,q;",
            "   p = L;",
            "   int j = 0;",
            "   while (p->next && j<i-1) {  // 寻找第i个结点，并令p指向其前趋",
            "       p = p->next;",
            "       ++j;",
            "       }",
            "   if (!(p->next) || j > i-1) return ERROR;  // 删除位置不合理",
            "   q = p->next;",
            "   p->next = q->next;           // 删除并释放结点",
            "   e = q->data;",
            "   free(q);",
            "   return OK;",
            "} // ListDelete_L"
        };
        public static string[] DELETE_VALUE =
        {
            "L",
            "a",
            "n",
            "p",//删除结点的前一个位置
            "j",
            "i",
            "e",//返回删除的数据值
            "q"//作为删除的结点的指标
        };
    }
}
