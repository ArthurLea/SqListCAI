using SqListCAI.Pages.MainPage;
using System.Threading;
using SqListCAI.Utils.SourceCodes;
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
        public delegate void outputDelegate(int i);
        public void Run(int flag)
        {
            switch(flag)
            {
                case 1://顺序表插入
                    for (int i = 0; i < SqListCodes.INSERT_CODE.Length; i++)
                    {
                        Thread.Sleep(200);
                        m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, i);
                    }
                    break;
                default:
                    break;
                   
            }
        }
    }
}
