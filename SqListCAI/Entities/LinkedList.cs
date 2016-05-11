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
            this.data = ' ';
            this.next = nextVal;
        }
    }
    public partial class LinkedList
    {
        public static LinkedNode head;
        public static int length;
        public static char[] srcData;

        public static char insertData;
        public static int insertPosition;

        public static int deletePosition;
        public static char deleteData;
        /// <summary>
        /// 演示链表创建动画时的初始化
        /// </summary>
        /// <param name="data"></param>
        //初始化源数据
        public static void init_LinkedList(string data)
        {
            //获取源数据
            srcData = new char[data.Length];
            for (int i = 0; i < data.Length; i++)
                srcData[i] = data[i];
        }
        /// <summary>
        /// 链表的创建函数（尾插法）
        /// </summary>
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
            insertData = insertData_LinkedIns;
            insertPosition = position_LinkedIns;

            Create_LinkedList();
        }
        /// <summary>
        /// 链表的删除初始化函数
        /// </summary>
        /// <param name="srcData_LinkedIns"></param>
        /// <param name="insertData_LinkedIns"></param>
        /// <param name="position_LinkedIns"></param>
        internal static void init_LinkedList(string srcData_LinkedDel, int position_LinkedDel)
        {
            //获取源数据
            srcData = new char[srcData_LinkedDel.Length];
            for (int i = 0; i < srcData_LinkedDel.Length; i++)
                srcData[i] = srcData_LinkedDel[i];
            deletePosition = position_LinkedDel;

            Create_LinkedList();
        }


        //做举例说明
        public char[] srcData_example;
        public int length_example;
        public LinkedList(string data)
        {
            this.srcData_example = new char[data.Length + 1];//加上头结点
            for (int i = 0; i < data.Length; i++)
            {
                if (i == 0)
                    srcData_example[i] = ' ';
                else
                    srcData_example[i] = data[i];
            }
            length_example = srcData_example.Length;
        }
    }
    
}
