using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqListCAI.Entities
{
    #region 线性表实体的基本操作，插入，删除
    public partial class SqList
    {
        public static int length;
        public static int MAXSIZE = 14;

        public static char[] srcData_ins;
        public static char insertData;
        public static int insPosition;

        public static char[] srcData_del;
        public static int delPosition;
        public SqList()
        {

            MAXSIZE = 14;
            srcData_ins = new char[MAXSIZE];
        }
        //初始化插入操作
        public static void init_SqList(string data, char instData, int position)
        {
            srcData_ins = new char[data.Length+1];
            for (int i = 0; i < data.Length; i++)
                srcData_ins[i] = data[i];
            insertData = instData;
            insPosition = position;
            length = data.Length;
        }

        public static int Inset_SqList(int i,char insData)
        {
            int j = 0;
            if(length == MAXSIZE-1)//表空间已满
            { return -1; }
            if(i<1 || i>length+2)
            { return 0; }
            for (j = length; j >= i - 1; j--)
                srcData_ins[j + 1] = srcData_ins[j];
            srcData_ins[i - 1] = insData;
            length++;
            return 1;
        }

        public static void init_SqList(string data, int position)
        {
            srcData_del = new char[data.Length + 1];
            for (int i = 0; i < data.Length; i++)
                srcData_del[i] = data[i];
            delPosition = position;
            length = data.Length;
        }
        public static void del_SqList(SqList l, int position)
        {

        }
    }
    #endregion
}
