using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqListCAI.Pages.MainPage;
using System.Threading;
namespace SqListCAI.Algorithm
{
    public partial class AlgorThread
    {
        ManualResetEvent m_EvnentStop;  //主线程Set以示要停止自定义线程
        ManualResetEvent m_EvnentStopped;  //主线程Set以示线程已经停止
        MainDemon m_mainDemon;  //主窗口引用
        public AlgorThread( ManualResetEvent evnentStop, ManualResetEvent evnentStopped, MainDemon mainDemon)
        {
            this.m_EvnentStop = evnentStop;
            this.m_EvnentStopped = evnentStopped;
            this.m_mainDemon = mainDemon;
        }
        public void Run()
        {
            string str;
            for(int i=0;i<=10;i++)
            {
                str = "Step number " + i.ToString() + " executed.";
                System.Threading.Thread.Sleep(500);
                m_mainDemon.Invoke(demon_lable_name.Content = str;
            }
        }
    }
}
