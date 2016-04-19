using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqListCAI.Entities
{
    class Sort
    {
        public static char[] srcData;
        public static int length;
        /// <summary>
        /// 直接插入排序、快速排序、交换排序（冒泡排序）的源数据初始化
        /// </summary>
        /// <param name="data"></param>
        public static void init_Sort(string data)
        {
            srcData = new char[data.Length + 1];
            for (int i = 0; i < data.Length + 1; i++)
            {
                if (i == 0)
                    srcData[0] = '?';//srcData[0]作为监视哨
                else
                    srcData[i] = data[i - 1];
            }
            length = srcData.Length;
        }
    }
}
