using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqListCAI.Entities
{
    public partial class Search
    {
        public static char[] srcData_OrderSearch;
        public static char[] srcData_BinSearch;//需要排序
        public static char searchData;
        public static int length;
        /// <summary>
        /// 顺序表的顺序查找初始化
        /// </summary>
        /// <param name="data"></param>
        public static void init_OrderSearch(string data,char seaData)
        {
            srcData_OrderSearch = new char[data.Length];
            for (int i = 0; i < data.Length; i++)
                srcData_OrderSearch[i] = data[i];

            searchData = seaData;
            length = data.Length;
        }
        /// <summary>
        /// 顺序表已排好序的二分查找（折半查找）
        /// </summary>
        /// <param name="data"></param>
        public static void intt_BinSearch(string data, char seaData)
        {
            srcData_BinSearch = new char[data.Length];
            for (int i = 0; i < data.Length; i++)
                srcData_BinSearch[i] = data[i];

            searchData = seaData;
            length = data.Length;
        }
    }
}
