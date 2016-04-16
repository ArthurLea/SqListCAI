using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqListCAI.Events
{
    public partial class PassValuesEventArgs : EventArgs
    {
        public string srcData;
        public char insertData;
        public int position;
        /// <summary>
        /// 顺序表插入
        /// </summary>
        /// <param name="srcData"></param>
        /// <param name="insertData"></param>
        /// <param name="position"></param>
        public PassValuesEventArgs(string srcData, char insertData,int position)
        {
            this.srcData = srcData;
            this.insertData = insertData;
            this.position = position;
        }
        /// <summary>
        /// 顺序表删除
        /// </summary>
        /// <param name="srcData"></param>
        /// <param name="position"></param>
        public PassValuesEventArgs(string srcData, int position)
        {
            this.srcData = srcData;
            this.position = position;
        }

        /// <summary>
        /// 链表创建，只需要得到插入的源数据即可
        /// </summary>
        /// <param name="srcData"></param>
        /// <param name="position"></param>
        public PassValuesEventArgs(string srcData)
        {
            this.srcData = srcData;
        }
    }
}
