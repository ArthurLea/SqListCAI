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
        public static int WAITTIME = 500;//执行算法的一行时需要缓冲等待的时间
        public ManualResetEvent m_EvnentStop;  //主线程Set以示要停止自定义线程
        ManualResetEvent m_EvnentStopped;  //主线程Set以示线程已经停止
        MainDemon m_mainDemon;  //主窗口引用
        public AlgorThread( ManualResetEvent evnentStop, ManualResetEvent evnentStopped, MainDemon mainDemon)
        {
            this.m_EvnentStop = evnentStop;
            this.m_EvnentStopped = evnentStopped;
            this.m_mainDemon = mainDemon;
        }
        public delegate void outputDelegate(int i);
        public void Run(int aldoTypeFlag,int operatorFlag)
        {
            switch(aldoTypeFlag)
            {
                case 1://顺序表插入
                    orderInsert(operatorFlag);
                    break;
                case 2://顺序表删除
                    orderDelete(operatorFlag);
                    break;
                case 3://链表创建
                    linkedCreate(operatorFlag);    
                    break;
                default:
                    break;
                   
            }
        }

        private void linkedCreate(int operatorFlag)
        {
            LinkedList.head = new LinkedNode(null);//定义一个空链表的头结点
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 2, false, 0, null);       //2
            Thread.Sleep(WAITTIME);

            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 3, true, 10000, null);    //3
            Thread.Sleep(WAITTIME);
            LinkedNode p;
            LinkedNode r = LinkedList.head.next;
            for (int i = 0;i< LinkedList.srcData.Length; i++)
            {
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 4, true, 0, i);   //4
                Thread.Sleep(WAITTIME);

                p = new LinkedNode(null);
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 5, true, i, null);   //5
                Thread.Sleep(WAITTIME);

                p.data = LinkedList.srcData[i];
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 6, true, i, LinkedList.srcData[i]);   //6
                Thread.Sleep(WAITTIME);

                p.next = r;
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 7, false, 0, null);   //7
                Thread.Sleep(WAITTIME);

                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 8, false, 0, null);      //8
                Thread.Sleep(WAITTIME);

                r = p;
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 3, true, i, null);   //3
                Thread.Sleep(WAITTIME);
            }
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 9, false, 0, null);      //9
            Thread.Sleep(WAITTIME);

            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_delegateExeFinish,3);   //4
        }

        private void orderDelete(int operatorFlag)
        {
            if (operatorFlag == 1) orderDeleteRun();//顺序表插入全速执行
            if (operatorFlag == 2) orderDeleteStep();//顺序表插入单步执行
            if (operatorFlag == 3) orderDeleteRunTo();//顺序表断点执行到
        }

        private void orderDeleteRunTo()
        {
            int p = 0;
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 3, false, 0, null);       //当前显示行 //3
            Thread.Sleep(WAITTIME);
            judgeIsPoint(3);
            if (SqList.delPosition < 1 || SqList.delPosition > SqList.length)//检查空表及删除位置的合理性
            {
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 4, false, 0, null);   //4
                Thread.Sleep(WAITTIME);
                judgeIsPoint(4);
                //return ERROR;
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 5, false, 0, null);   //5
                Thread.Sleep(WAITTIME);
                judgeIsPoint(5);
            }
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 4, false, 0, null);       //4
            Thread.Sleep(WAITTIME);
            judgeIsPoint(4);

            string e = SqList.srcData_del[SqList.delPosition - 1].ToString();//被删除元素的值赋给e
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 6, false, 0, null);           //6
            Thread.Sleep(WAITTIME);
            judgeIsPoint(6);

            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 7, true, 10000, e);           //7
            Thread.Sleep(WAITTIME);
            judgeIsPoint(7);

            for (p = SqList.delPosition; p < SqList.length; p++)
            {
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 8, true, 0, p);       //8
                Thread.Sleep(WAITTIME);
                judgeIsPoint(8);

                SqList.srcData_del[p - 1] = SqList.srcData_del[p];//向左移动
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 7, true, p, null);    //7
                Thread.Sleep(WAITTIME);
                judgeIsPoint(7);
            }
            SqList.length--;
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 9, false, 0, null);        //9
            Thread.Sleep(WAITTIME);
            judgeIsPoint(9);

            //return OK;
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 10, true, 0, SqList.length);//10
            Thread.Sleep(WAITTIME);
            judgeIsPoint(10);
            //}
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 11, false, 0, null);       //11
            Thread.Sleep(WAITTIME);//顺序表删除算法执行完毕
            judgeIsPoint(11);

            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_delegateExeFinish, 2);
        }
        private void orderDeleteStep()
        {
            int p = 0;
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 3, false, 0, null);       //当前显示行 //3
            Thread.Sleep(WAITTIME);
            MainDemon.allDone.WaitOne();
            if (SqList.delPosition < 1 || SqList.delPosition > SqList.length)//检查空表及删除位置的合理性
            {
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 4, false, 0, null);   //4
                Thread.Sleep(WAITTIME);
                MainDemon.allDone.Reset();
                MainDemon.allDone.WaitOne();
                //return ERROR;
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 5, false, 0, null);   //5
                Thread.Sleep(WAITTIME);
                MainDemon.allDone.Reset();
                MainDemon.allDone.WaitOne();
            }
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 4, false, 0, null);       //4
            Thread.Sleep(WAITTIME);
            MainDemon.allDone.Reset();
            MainDemon.allDone.WaitOne();

            string e = SqList.srcData_del[SqList.delPosition - 1].ToString();//被删除元素的值赋给e
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 6, false, 0, null);           //6
            Thread.Sleep(WAITTIME);
            MainDemon.allDone.Reset();
            MainDemon.allDone.WaitOne();

            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 7, true, 10000, e);           //7
            Thread.Sleep(WAITTIME);
            MainDemon.allDone.Reset();
            MainDemon.allDone.WaitOne();

            for (p = SqList.delPosition; p < SqList.length; p++)
            {
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 8, true, 0, p);       //8
                Thread.Sleep(WAITTIME);
                MainDemon.allDone.Reset();
                MainDemon.allDone.WaitOne();

                SqList.srcData_del[p - 1] = SqList.srcData_del[p];//向左移动
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 7, true, p, null);    //7
                Thread.Sleep(WAITTIME);
                MainDemon.allDone.Reset();
                MainDemon.allDone.WaitOne();
            }
            SqList.length--;
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 9, false, 0, null);        //9
            Thread.Sleep(WAITTIME);
            MainDemon.allDone.Reset();
            MainDemon.allDone.WaitOne();

            //return OK;
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 10, true, 0, SqList.length);//10
            Thread.Sleep(WAITTIME);
            MainDemon.allDone.Reset();
            MainDemon.allDone.WaitOne();
            //}
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 11, false, 0, null);       //11
            Thread.Sleep(WAITTIME);//顺序表删除算法执行完毕
            MainDemon.allDone.Reset();
            MainDemon.allDone.WaitOne();

            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_delegateExeFinish, 2);
        }

        private void orderDeleteRun()
        {
            int p = 0;
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 3, false, 0, null);       //当前显示行 //3
            Thread.Sleep(WAITTIME);

            if (SqList.delPosition < 1 || SqList.delPosition > SqList.length)//检查空表及删除位置的合理性
            {
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 4, false, 0, null);   //4
                Thread.Sleep(WAITTIME);
                //return ERROR;
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 5, false, 0, null);   //5
                Thread.Sleep(WAITTIME);
            }
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 4, false, 0, null);       //4
            Thread.Sleep(WAITTIME);

            string e = SqList.srcData_del[SqList.delPosition - 1].ToString();//被删除元素的值赋给e
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 6, false, 0, null);           //6
            Thread.Sleep(WAITTIME);

            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 7, true, 10000, e);           //7
            Thread.Sleep(WAITTIME);

            for (p = SqList.delPosition; p < SqList.length; p++)
            {
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 8, true, 0, p);       //8
                Thread.Sleep(WAITTIME);

                SqList.srcData_del[p - 1] = SqList.srcData_del[p];//向左移动
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 7, true, p, null);    //7
                Thread.Sleep(WAITTIME);
            }
            SqList.length--;
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 9, false, 0, null);        //9
            Thread.Sleep(WAITTIME);

            //return OK;
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 10, true, 0, SqList.length);//10
            Thread.Sleep(WAITTIME);
            //}
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 11, false, 0, null);       //11

            Thread.Sleep(WAITTIME);//顺序表删除算法执行完毕
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_delegateExeFinish, 2);
        }

        /*
    1://全速执行
    2://单步
    3://执行到
*/
        private void orderInsert(int operatorFlag)
        {
            if(operatorFlag == 1) orderInsertRun();//顺序表插入全速执行
            if(operatorFlag == 2) orderInsertStep();//顺序表插入单步执行
            if (operatorFlag == 3) orderInsertRunTo();//顺序表断点执行到
        }
        //顺序表插入全速执行
        private void orderInsertRun()
        {
            int p = 0;//当前线性表操作位置                                                                               
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 3, false, 0, null);  //当前显示行 //3
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

            for (p = SqList.length - 1; p >= SqList.insPosition - 1; p--)
            {
                //开始移动，只发送移动位置，主窗口的变量区同步变量的改变
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 9, true, 0, p);        //8
                Thread.Sleep(WAITTIME);

                SqList.srcData_ins[p + 1] = SqList.srcData_ins[p];
                //源数据开始改变，发送源数据改变标志                 
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 8, true, p, null);        //9
                Thread.Sleep(WAITTIME);

            }
            SqList.srcData_ins[SqList.insPosition - 1] = SqList.insertData;
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
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_delegateExeFinish, 1);
        }
        private void orderInsertStep()
        {
            int p = 0;//当前线性表操作位置                                                                               
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 3, false, 0, null);  //当前显示行 //3

            MainDemon.allDone.WaitOne();

            if (SqList.insPosition < 1 || SqList.insPosition > SqList.length + 2)
            {
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 4, false, 0, null);        //4

                MainDemon.allDone.Reset();
                MainDemon.allDone.WaitOne();
                // return ERROR;                                                                     
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 5, false, 0, null);        //5

                MainDemon.allDone.Reset();
                MainDemon.allDone.WaitOne();
            }
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 4, false, 0, null);            //4

            MainDemon.allDone.Reset();
            MainDemon.allDone.WaitOne();

            if (SqList.length > SqList.MAXSIZE - 1)//表空间已满                                     
            {
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 6, false, 0, null);        //6

                MainDemon.allDone.Reset();
                MainDemon.allDone.WaitOne();
                // return ERROR;                                                                     
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 7, false, 0, null);        //7

                MainDemon.allDone.Reset();
                MainDemon.allDone.WaitOne();
            }
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 6, false, 0, null);            //6

            MainDemon.allDone.Reset();
            MainDemon.allDone.WaitOne();

            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 8, false, 0, null);            //8

            MainDemon.allDone.Reset();
            MainDemon.allDone.WaitOne();

            for (p = SqList.length - 1; p >= SqList.insPosition - 1; p--)
            {
                //开始移动，只发送移动位置，主窗口的变量区同步变量的改变
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 9, true, 0, p);        //8

                MainDemon.allDone.Reset();
                MainDemon.allDone.WaitOne();

                SqList.srcData_ins[p + 1] = SqList.srcData_ins[p];
                //源数据开始改变，发送源数据改变标志                 
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 8, true, p, null);        //9

                MainDemon.allDone.Reset();
                MainDemon.allDone.WaitOne();
            }
            SqList.srcData_ins[SqList.insPosition - 1] = SqList.insertData;
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 10, false, 0, null);           //10

            MainDemon.allDone.Reset();
            MainDemon.allDone.WaitOne();

            SqList.length++;
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 11, true, 0, null);           //11        

            MainDemon.allDone.Reset();
            MainDemon.allDone.WaitOne();

            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 12, true, 0, SqList.length);  //12

            MainDemon.allDone.Reset();
            MainDemon.allDone.WaitOne();

            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 13, false, 0, null);           //13

            //顺序表插入算法执行完毕
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_delegateExeFinish, 1);
        }
        private void orderInsertRunTo()
        {
            int p = 0;//当前线性表操作位置                                                                           
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 3, false, 0, null);  //当前显示行 //3
            Thread.Sleep(WAITTIME);

            judgeIsPoint(3);

            if (SqList.insPosition < 1 || SqList.insPosition > SqList.length + 2)
            {
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 4, false, 0, null);        //4
                Thread.Sleep(WAITTIME);

                judgeIsPoint(4);

                // return ERROR;                                                                     
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 5, false, 0, null);        //5
                Thread.Sleep(WAITTIME);

                judgeIsPoint(5);

            }
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 4, false, 0, null);            //4
            Thread.Sleep(WAITTIME);

            judgeIsPoint(4);
            if (SqList.length > SqList.MAXSIZE - 1)//表空间已满                                     
            {
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 6, false, 0, null);        //6
                Thread.Sleep(WAITTIME);
                judgeIsPoint(6);
                // return ERROR;                                                                     
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 7, false, 0, null);        //7
                Thread.Sleep(WAITTIME);
                judgeIsPoint(7);
            }
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 6, false, 0, null);            //6
            Thread.Sleep(WAITTIME);
            judgeIsPoint(6);
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 8, false, 0, null);            //8
            Thread.Sleep(WAITTIME);
            judgeIsPoint(8);
            for (p = SqList.length - 1; p >= SqList.insPosition - 1; p--)
            {
                //开始移动，只发送移动位置，主窗口的变量区同步变量的改变
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 9, true, 0, p);        //8
                Thread.Sleep(WAITTIME);
                judgeIsPoint(9);
                SqList.srcData_ins[p + 1] = SqList.srcData_ins[p];
                //源数据开始改变，发送源数据改变标志                 
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 8, true, p, null);        //9
                Thread.Sleep(WAITTIME);
                judgeIsPoint(8);
            }
            SqList.srcData_ins[SqList.insPosition - 1] = SqList.insertData;
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 10, false, 0, null);           //10
            Thread.Sleep(WAITTIME);
            judgeIsPoint(10);
            SqList.length++;
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 11, true, 0, null);           //11        
            Thread.Sleep(WAITTIME);
            judgeIsPoint(11);
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 12, true, 0, SqList.length);  //12
            Thread.Sleep(WAITTIME);
            judgeIsPoint(12);
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 13, false, 0, null);           //13
            Thread.Sleep(WAITTIME);
            judgeIsPoint(13);
            //顺序表插入算法执行完毕
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_delegateExeFinish, 1);
        }
        private void judgeIsPoint(int currentRow)
        {
            if(this.m_mainDemon.wherePoint != null)
            {
                for (int i = 0; i < this.m_mainDemon.wherePoint.Length; i++)
                {
                    if (this.m_mainDemon.wherePoint[i] == currentRow)
                    {
                        MainDemon.allDone.Reset();
                        MainDemon.allDone.WaitOne();
                        break;
                    }
                }
            }
        }
    }
}

