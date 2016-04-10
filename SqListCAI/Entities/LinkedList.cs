using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqListCAI.Entities
{
    public partial class LinkedList
    {
        string srcData;
        char insertData;
        int insertPosition;
        int deletePosition;

        public  LinkedList(string srcData)
        {
            this.srcData = srcData;
        }
        public LinkedList(string srcData,char insData,int position)
        {
            this.srcData = srcData;
            this.insertData = insData;
            this.insertPosition = position;
        }
        public LinkedList(string srcData,int position)
        {
            this.srcData = srcData;
            this.deletePosition = position;
        }
        public void createLinkedList(LinkedList list,int n)
        {

        }
        public void insLinkedList(LinkedList list,char insData,int position)
        {

        }
        public void delLinkedList(LinkedList list,int position)
        {

        }
    }
}
