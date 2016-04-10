using SqListCAI.Pages.MainPage;
using System.Windows.Shapes;
namespace SqListCAI
{
    public partial class Demonstration
    {
        int flag;
        MainDemon m_maindemon;
        public Demonstration(int flag,MainDemon maindemon)
        {
            this.flag = flag;
            this.m_maindemon = maindemon;
        }
        public void ShowCode(string[] str)
        {
            for (int i = 0; i < str.Length; i++)
                this.m_maindemon.listBox_code.Items.Insert(i, str[i]);
        }
        public void ShowDemon()
        {
            switch (flag)
            {
                case 1://线性表插入
                    {
                        if(m_maindemon.sqlist != null)
                        {
                            //m_maindemon.canse_demon.
                        }
                        break;
                    }
                    
                default:
                    break;
            }
        }
        public void ShowValue(string str)
        { }
        public void ShowExplain(string str)
        { }
        public void SetData(string str)
        { }
        public void ReStart()
        { }
    }
}
