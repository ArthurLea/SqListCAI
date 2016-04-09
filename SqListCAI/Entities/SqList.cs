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
        string srcData;
        char insertData;
        int insertPosition;
        int deletePosition;

        int length;
        public SqList()
        { }
        public SqList(string srcData,char instData,int position)//初始化插入操作数据
        {
            this.srcData = srcData;
            this.insertData = instData;
            this.insertPosition = position;
            this.length = srcData.Length;
        }
        public SqList(string srcData,int position)//初始化删除操作数据
        {
            this.srcData = srcData;
            this.deletePosition = position;
            this.length = srcData.Length;
        }
        public void ins_SqList(SqList sq,char insData,int position)
        {
            StringBuilder sb = new StringBuilder(sq.srcData.Length + 1);
            for(int i=0;i<sq.srcData.Length;i++)
            {

            }
        }
        public void del_SqList(SqList l,int position)
        {

        }
    }
    #endregion
}
