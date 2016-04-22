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
        public static int ERROR = -1;
        public static int OK = 1;
        public static int length;
        public static int MAXSIZE = 18;

        public static char[] srcData_ins;
        public static char insertData;
        public static int insPosition;

        public static char[] srcData_del;
        public static int delPosition;
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
            int p = 0;
            if(length == SqList.MAXSIZE-1)//表空间已满
            { return -1; }
            if(i<1 || i> SqList.length)
            { return ERROR; }
            for (p = SqList.length; p >= i - 1; p--)
                SqList.srcData_ins[p + 1] = SqList.srcData_ins[p];
            SqList.srcData_ins[i - 1] = SqList.insertData;
            SqList.length++;
            return OK;
        }
        /// <summary>
        /// 初始化删除操作
        /// </summary>
        /// <param name="data"></param>
        /// <param name="position"></param>
        public static void init_SqList(string data, int position)
        {
            srcData_del = new char[data.Length];
            for (int i = 0; i < data.Length; i++)
                srcData_del[i] = data[i];
            delPosition = position;
            length = data.Length;
        }
        public static int del_SqList(int i,string e)
        {
            int p = 0;
            if (SqList.delPosition < 1 || SqList.delPosition > SqList.length)//检查空表及删除位置的合理性
            { return ERROR; }
            e = SqList.srcData_del[SqList.delPosition - 1].ToString();//被删除元素的值赋给e
            for (p = SqList.delPosition; p < SqList.length; p++)
                SqList.srcData_del[p - 1] = SqList.srcData_del[p];//向左移动
            SqList.length--;
            return OK;
        }
    }
    #endregion
}
