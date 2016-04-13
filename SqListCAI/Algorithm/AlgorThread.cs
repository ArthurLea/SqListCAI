using SqListCAI.Pages.MainPage;
using System.Threading;
using SqListCAI.Utils.SourceCodes;
using SqListCAI.Entities;
using System;

namespace SqListCAI.Algorithm
{
    public partial class AlgorThread
    {
        public static ManualResetEvent allDone = new ManualResetEvent(false);
        public static int WAITTIME = 800;//执行算法的一行时需要缓冲等待的时间
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
                    {
                        int p = 0;//当前线性表操作位置                                                                               
                        m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 3,false,0,null);  //当前显示行 //3
                        Thread.Sleep(WAITTIME);
                        if (SqList.insPosition < 1 || SqList.insPosition > SqList.length + 2)                    
                        {
                            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 4, false, 0, null);        //4
                            Thread.Sleep(WAITTIME);
                            // return ERROR;                                                                     
                            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 5, false, 0, null);        //5
                            Thread.Sleep(WAITTIME);
                        }
                        m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 4, false, 0, null);            //4
                        Thread.Sleep(WAITTIME);
                        if (SqList.length > SqList.MAXSIZE - 1)//表空间已满                                     
                        {
                            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 6, false, 0, null);        //6
                            Thread.Sleep(WAITTIME);
                            // return ERROR;                                                                     
                            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 7, false, 0, null);        //7
                            Thread.Sleep(WAITTIME);
                        }
                        m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 6, false, 0, null);            //6
                        Thread.Sleep(WAITTIME);
                        m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 8, false, 0, null);            //8
                        Thread.Sleep(WAITTIME);
                        for (p = SqList.length-1; p >= SqList.insPosition - 1; p--)                              
                        {
                            //开始移动，只发送移动位置，主窗口的变量区同步变量的改变
                            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 9, true, 0, p);        //8
                            Thread.Sleep(WAITTIME);
                            SqList.srcData[p + 1] = SqList.srcData[p];
                            //源数据开始改变，发送源数据改变标志                 
                            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 8, true, p, null);        //9
                            Thread.Sleep(WAITTIME);
                        }
                        SqList.srcData[SqList.insPosition - 1] = SqList.insertData;
                        m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 10, false, 0, null);           //10
                        Thread.Sleep(WAITTIME);
                        SqList.length++;
                        m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 11, true, 0, null);           //11        
                        Thread.Sleep(WAITTIME);
                        m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 12, true, 0, SqList.length);  //12
                        Thread.Sleep(WAITTIME);
                        m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 13, false, 0, null);           //13
                        Thread.Sleep(WAITTIME);

                        //顺序表插入算法执行完毕
                        m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_delegateExeFinish,1);
                        
                        break;
                    }
                default:
                    break;
                   
            }
        }
        
    }
}

