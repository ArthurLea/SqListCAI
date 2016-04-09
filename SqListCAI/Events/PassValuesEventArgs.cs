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
        public int insertPosition;
        public PassValuesEventArgs(string srcData, char insertData,int position)
        {
            this.srcData = srcData;
            this.insertData = insertData;
            this.insertPosition = position;
        }
    }
}
