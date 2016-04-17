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
        private static void Create_LinkedList()
        {
            LinkedList.head = new LinkedNode(null); ;//定义一个空链表的头结点
            LinkedNode p;
            LinkedNode r = LinkedList.head.next;
            for (int i = srcData.Length - 1; i >= 0; i--)
            {
                p = new LinkedNode(null);
                p.data = srcData[i];
                p.next = LinkedList.head.next;
                LinkedList.head.next = p;
            }
        }
        /// <summary>
        /// 链表的插入初始化函数
        /// </summary>
        /// <param name="srcData_LinkedIns"></param>
        /// <param name="insertData_LinkedIns"></param>
        /// <param name="position_LinkedIns"></param>
        internal static void init_LinkedList(string srcData_LinkedIns, char insertData_LinkedIns, int position_LinkedIns)
        {
            //获取源数据
            srcData = new char[srcData_LinkedIns.Length];
            for (int i = 0; i < srcData_LinkedIns.Length; i++)
                srcData[i] = srcData_LinkedIns[i];
            length = srcData_LinkedIns.Length;
            insertData = insertData_LinkedIns;
            insertPosition = position_LinkedIns;

            Create_LinkedList();
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
        /// <summary>
        /// 链表创建的初始化函数
        /// </summary>
        /// <param name="data"></param>
        public LinkedList(string data)
        {
            init_LinkedList(data);
        }
    }
    
}
