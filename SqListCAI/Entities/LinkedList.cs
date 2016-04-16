using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqListCAI.Entities
{
    public partial class LinkedNode
    {
        public char data;
        public LinkedNode next;

        public LinkedNode(char data,LinkedNode nextVal)
        {
            this.data = data;
            this.next = nextVal;
        }
        public LinkedNode(LinkedNode nextVal)
        {
            this.next = nextVal;
        }
    }
    public partial class LinkedList
    {
        public static LinkedNode head;

        public static char[] srcData;
        public static int length;

        public static char insertData;
        public static int insertPosition;
        public static int deletePosition;

        //初始化源数据
        public static void init_LinkedList(string data)
        {
            //获取源数据
            srcData = new char[data.Length];
            for (int i = 0; i < data.Length; i++)
                srcData[i] = data[i];
            length = data.Length;
        }
        //public void init_LinkedList(string data)
        //{
        //    //获取源数据
        //    srcData = new char[data.Length];
        //    for (int i = 0; i < data.Length; i++)
        //        srcData[i] = data[i];

        //    //建立链表表头
        //    head = new LinkedNode(null);
        //}
        public LinkedList(string data)
        {
            init_LinkedList(data);
        }
        public static LinkedList Create_LinkedList(string data)
        {
            LinkedList L = new LinkedList(data);//定义一个空链表的头结点
            LinkedNode node;
            for(int i=data.Length-1;i>=0;i--)
            {
                node = new LinkedNode(null);
                node.data = data[i];
                node.next = head.next;
            }    
            return L;
        }
    }
    
}
