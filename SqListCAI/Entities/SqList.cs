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
        public SqList()
        { }
        public SqList(string srcData,char instData,int position)//初始化插入操作数据
        {
            this.srcData = srcData;
            this.insertData = instData;
            this.insertPosition = position;
        }
        public SqList(string srcData,char position)//初始化删除操作数据
        {

        }
        public void ins_SqList(SqList l,char insData,int position)
        {

        }
        public void del_SqList(SqList l,int position)
        {

        }
    }
    #endregion
}
