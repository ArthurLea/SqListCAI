using SqListCAI.Pages.MainPage;
using System.Threading;
using SqListCAI.Entities;
using System;

namespace SqListCAI.Algorithm
{
    public partial class AlgorThread
    {
        public static ManualResetEvent allDone = new ManualResetEvent(false);
        public static int WAITTIME = (int)App.WAITTIME;//执行算法的一行时需要缓冲等待的时间
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
                case 4://链表插入
                    linkedInsert(operatorFlag);
                    break;
                case 5://链表删除
                    linkedDelete(operatorFlag);
                    break;
                case 6://顺序查找
                    orderSearch(operatorFlag);
                    break;
                case 7://折半查找
                    binarySearch(operatorFlag);
                    break;
                case 8://直接插入排序
                    insertSort(operatorFlag);
                    break;
                case 9://交换（冒泡）排序
                    swapSort(operatorFlag);
                    break;
                case 10://快速排序
                    partitionSort(operatorFlag);
                    break;
                default:
                    break;
                   
            }
        }

        private void partitionSort(int operatorFlag)
        {
            if (operatorFlag == 1) partitionSortRun();//快速排序全速执行
            if (operatorFlag == 2) partitionSortStep();//快速排序单步执行
            if (operatorFlag == 3) partitionSortRunTo();//快速排序断点执行到 
        }
        /// <summary>
        /// 快速排序断点执行到 
        /// </summary>
        private void partitionSortRunTo()
        {
            quickSortRunTo(Sort.srcData, 1, Sort.length - 1);
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 8, false, 1000, 0);      //结束运行
            MainDemon.allDone.WaitOne();

            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_delegateExeFinish, 10);//通知主线程算法执行完毕
        }
        private void quickSortRunTo(char[] srcData, int low, int high)
        {
            //对顺序表srcData中的子序列R[low...high]做快速排序
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 3, false, 0, 0);      //3
            Thread.Sleep(WAITTIME);

            if (low < high)
            {
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 4, false, 0, 0);      //4
                Thread.Sleep(WAITTIME);
                int pivotloc = partitionSortRunTo(Sort.srcData, low, high);

                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 1000, false, 1000, 0);  //1000(单步不做同步)
                MainDemon.allDone.WaitOne();
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 5, true, 1000, pivotloc);  //5
                MainDemon.allDone.WaitOne();
                quickSortRunTo(Sort.srcData, low, pivotloc - 1);

                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 1000, false, 1000, 0);  //1000(单步不做同步)
                MainDemon.allDone.WaitOne();
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 6, true, 1000, pivotloc);  //6
                MainDemon.allDone.WaitOne();
                quickSortRunTo(Sort.srcData, pivotloc + 1, high);
            }
        }
        private int partitionSortRunTo(char[] srcData, int low, int high)
        {
            //改变算法代码区为partitionSort
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 1000, false, 2000, 0);      //1000(单步不做同步)
            Thread.Sleep(WAITTIME);

            Sort.srcData[0] = Sort.srcData[low]; //用子表的第一个记录作枢轴记录
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 3, false, 0, 0);      //3
            Thread.Sleep(WAITTIME);
            judgeIsPoint(3);

            char pivotkey = Sort.srcData[low];   //枢纽记录关键字
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 4, true, 0, Sort.srcData[0]);      //4//给pivotkey赋值
            Thread.Sleep(WAITTIME);
            judgeIsPoint(4);

            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 5, true, 0, pivotkey);      //5//给pivotkey赋值
            Thread.Sleep(WAITTIME);
            judgeIsPoint(5);
            while (low < high)//从表的两端交替地向中间扫描
            {
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 6, false, 0, 0);      //6
                Thread.Sleep(WAITTIME);
                judgeIsPoint(6);

                while ((low < high) && (Sort.srcData[high] >= pivotkey)) --high;
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 7, true, 0, high);      //7//改变high
                Thread.Sleep(WAITTIME);
                judgeIsPoint(7);

                Sort.srcData[low] = Sort.srcData[high];//将比枢轴记录小的记录移到低端
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 8, true, low, Sort.srcData[high]);  //8//改变Sort.srcData[low]的值
                Thread.Sleep(WAITTIME);
                judgeIsPoint(8);

                while ((low < high) && (Sort.srcData[low] <= pivotkey)) ++low;
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 9, true, 0, low);      //9//改变low
                Thread.Sleep(WAITTIME);
                judgeIsPoint(9);

                Sort.srcData[high] = Sort.srcData[low];
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 10, true, high, Sort.srcData[low]);      //10//改变Sort.srcData[high]的值
                Thread.Sleep(WAITTIME);
                judgeIsPoint(10);

                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 5, false, 0, 0);      //5
                Thread.Sleep(WAITTIME);
                judgeIsPoint(5);
            }

            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 11, false, 0, 0);      //5//给pivotkey赋值
            Thread.Sleep(WAITTIME);
            judgeIsPoint(11);

            Sort.srcData[low] = Sort.srcData[0];
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 12, true, low, Sort.srcData[0]);      //12//枢纽记录到位
            Thread.Sleep(WAITTIME);
            judgeIsPoint(12);

            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 13, false, 0, 0);      //13
            Thread.Sleep(WAITTIME);
            judgeIsPoint(13);

            return low;//return high;//循环退出后low==high
        }
        /// <summary>
        /// 快速排序单步执行
        /// </summary>
        private void partitionSortStep()
        {
            quickSortStep(Sort.srcData, 1, Sort.length - 1);

            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 8, false, 1000, 0);      //结束运行
            MainDemon.allDone.WaitOne();
            MainDemon.allDone.Reset();
            MainDemon.allDone.WaitOne();

            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_delegateExeFinish, 10);//通知主线程算法执行完毕
        }
        private void quickSortStep(char[] srcData, int low, int high)
        {
            //对顺序表srcData中的子序列R[low...high]做快速排序
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 3, false, 0, 0);      //3
            Thread.Sleep(WAITTIME);
            MainDemon.allDone.WaitOne();

            if (low < high)
            {
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 4, false, 0, 0);      //4
                Thread.Sleep(WAITTIME);
                MainDemon.allDone.Reset();
                MainDemon.allDone.WaitOne();

                int pivotloc = partitionSortStep(Sort.srcData, low, high);

                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 1000, false, 1000, 0);  //1000(单步不做同步)
                MainDemon.allDone.WaitOne();
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 5, true, 1000, pivotloc);  //5
                MainDemon.allDone.WaitOne();
                MainDemon.allDone.Reset();
                MainDemon.allDone.WaitOne();

                quickSortStep(Sort.srcData, low, pivotloc - 1);
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 1000, false, 1000, 0);  //1000(单步不做同步)
                MainDemon.allDone.WaitOne();
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 6, true, 1000, pivotloc);  //6
                MainDemon.allDone.WaitOne();
                MainDemon.allDone.Reset();
                MainDemon.allDone.WaitOne();

                quickSortStep(Sort.srcData, pivotloc + 1, high);
            }
        }
        private int partitionSortStep(char[] srcData, int low, int high)
        {
            //改变算法代码区为partitionSort
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 1000, false, 2000, 0);      //1000(单步不做同步)
            Thread.Sleep(WAITTIME);

            Sort.srcData[0] = Sort.srcData[low]; //用子表的第一个记录作枢轴记录
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 3, false, 0, 0);      //3
            Thread.Sleep(WAITTIME);
            MainDemon.allDone.Reset();
            MainDemon.allDone.WaitOne();

            char pivotkey = Sort.srcData[low];   //枢纽记录关键字
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 4, true, 0, Sort.srcData[0]);      //4//给pivotkey赋值
            Thread.Sleep(WAITTIME);
            MainDemon.allDone.Reset();
            MainDemon.allDone.WaitOne();

            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 5, true, 0, pivotkey);      //5//给pivotkey赋值
            Thread.Sleep(WAITTIME);
            MainDemon.allDone.Reset();
            MainDemon.allDone.WaitOne();
            while (low < high)//从表的两端交替地向中间扫描
            {
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 6, false, 0, 0);      //6
                Thread.Sleep(WAITTIME);
                MainDemon.allDone.Reset();
                MainDemon.allDone.WaitOne();

                while ((low < high) && (Sort.srcData[high] >= pivotkey)) --high;
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 7, true, 0, high);      //7//改变high
                Thread.Sleep(WAITTIME);
                MainDemon.allDone.Reset();
                MainDemon.allDone.WaitOne();

                Sort.srcData[low] = Sort.srcData[high];//将比枢轴记录小的记录移到低端
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 8, true, low, Sort.srcData[high]);  //8//改变Sort.srcData[low]的值
                Thread.Sleep(WAITTIME);
                MainDemon.allDone.Reset();
                MainDemon.allDone.WaitOne();

                while ((low < high) && (Sort.srcData[low] <= pivotkey)) ++low;
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 9, true, 0, low);      //9//改变low
                Thread.Sleep(WAITTIME);
                MainDemon.allDone.Reset();
                MainDemon.allDone.WaitOne();

                Sort.srcData[high] = Sort.srcData[low];
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 10, true, high, Sort.srcData[low]);      //10//改变Sort.srcData[high]的值
                Thread.Sleep(WAITTIME);
                MainDemon.allDone.Reset();
                MainDemon.allDone.WaitOne();

                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 5, false, 0, 0);      //5
                Thread.Sleep(WAITTIME);
                MainDemon.allDone.Reset();
                MainDemon.allDone.WaitOne();
            }

            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 11, false, 0, 0);      //5//给pivotkey赋值
            Thread.Sleep(WAITTIME);
            MainDemon.allDone.Reset();
            MainDemon.allDone.WaitOne();

            Sort.srcData[low] = Sort.srcData[0];
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 12, true, low, Sort.srcData[0]);      //12//枢纽记录到位
            Thread.Sleep(WAITTIME);
            MainDemon.allDone.Reset();
            MainDemon.allDone.WaitOne();

            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 13, false, 0, 0);      //13
            Thread.Sleep(WAITTIME);
            MainDemon.allDone.Reset();
            MainDemon.allDone.WaitOne();

            return low;//return high;//循环退出后low==high
        }
        /// <summary>
        /// 快速排序全速执行
        /// </summary>
        private void partitionSortRun()
        {
            quickSortRun(Sort.srcData,1,Sort.length-1);

            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 8, false, 1000, 0);      //结束运行
            MainDemon.allDone.WaitOne();

            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_delegateExeFinish, 10);//通知主线程算法执行完毕
        }
        private void quickSortRun(char[] srcData, int low, int high)
        {
            //对顺序表srcData中的子序列R[low...high]做快速排序
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 3, false, 0, 0);      //3
            Thread.Sleep(WAITTIME);

            if (low < high)
            {
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 4, false, 0, 0);      //4
                Thread.Sleep(WAITTIME);
                int pivotloc = partitionSortRun(Sort.srcData, low, high);

                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 1000, false,1000,0);  //1000(单步不做同步)
                MainDemon.allDone.WaitOne();
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 5, true, 1000, pivotloc);  //5
                MainDemon.allDone.WaitOne();
                quickSortRun(Sort.srcData, low, pivotloc - 1);

                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 1000, false, 1000, 0);  //1000(单步不做同步)
                MainDemon.allDone.WaitOne();
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 6, true, 1000, pivotloc);  //6
                MainDemon.allDone.WaitOne();
                quickSortRun(Sort.srcData, pivotloc+1,high);
            }
        }
        private int partitionSortRun(char[] srcData, int low, int high)
        {
            //改变算法代码区为partitionSort
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 1000, false, 2000, 0);      //1000(单步不做同步)
            Thread.Sleep(WAITTIME);

            Sort.srcData[0] = Sort.srcData[low]; //用子表的第一个记录作枢轴记录
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 3, false, 0, 0);      //3
            Thread.Sleep(WAITTIME);

            char pivotkey = Sort.srcData[low];   //枢纽记录关键字
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 4, true, 0, Sort.srcData[0]);      //4//给pivotkey赋值
            Thread.Sleep(WAITTIME);

            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 5, true, 0, pivotkey);      //5//给pivotkey赋值
            Thread.Sleep(WAITTIME);
            while (low < high)//从表的两端交替地向中间扫描
            {
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 6, false, 0, 0);      //6
                Thread.Sleep(WAITTIME);

                while ((low < high) && (Sort.srcData[high] >= pivotkey)) --high;
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 7, true, 0, high);      //7//改变high
                Thread.Sleep(WAITTIME);

                Sort.srcData[low] = Sort.srcData[high];//将比枢轴记录小的记录移到低端
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 8, true, low, Sort.srcData[high]);  //8//改变Sort.srcData[low]的值
                Thread.Sleep(WAITTIME);

                while ((low < high) && (Sort.srcData[low] <= pivotkey)) ++low;
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 9, true, 0, low);      //9//改变low
                Thread.Sleep(WAITTIME);

                Sort.srcData[high] = Sort.srcData[low];
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 10, true, high, Sort.srcData[low]);      //10//改变Sort.srcData[high]的值
                Thread.Sleep(WAITTIME);

                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 5, false, 0, 0);      //5
                Thread.Sleep(WAITTIME);
            }

            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 11, false, 0, 0);      //5//给pivotkey赋值
            Thread.Sleep(WAITTIME);

            Sort.srcData[low] = Sort.srcData[0];
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 12, true, low, Sort.srcData[0]);      //12//枢纽记录到位
            Thread.Sleep(WAITTIME);

            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 13, false, 0, 0);      //13
            Thread.Sleep(WAITTIME);
            return low;//return high;//循环退出后low==high
        }

        private void swapSort(int operatorFlag)
        {
            if (operatorFlag == 1) swapSortRun();//交换（冒泡）排序全速执行
            if (operatorFlag == 2) swapSortStep();//交换（冒泡）排序单步执行
            if (operatorFlag == 3) swapSortRunTo();//交换（冒泡）排序断点执行到 
        }
        /// <summary>
        /// 交换（冒泡）排序断点执行到 
        /// </summary>
        private void swapSortRunTo()
        {
            int i, j;
            int swap;
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 1, false, 0, 0);      //1
            Thread.Sleep(WAITTIME);
            judgeIsPoint(1);

            for (i = 1; i < Sort.length; i++)
            {
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 2, true, 0, i);  //2
                Thread.Sleep(WAITTIME);
                judgeIsPoint(2);

                swap = 0;
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 3, true, 0, swap);  //3
                Thread.Sleep(WAITTIME);
                judgeIsPoint(3);

                for (j = 1; j < Sort.length - i; j++)
                {
                    m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 4, true, 0, j);  //4
                    Thread.Sleep(WAITTIME);
                    judgeIsPoint(4);

                    if (Sort.srcData[j] > Sort.srcData[j + 1])//将大的往后甩
                    {
                        m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 5, false, 0, 0);  //5
                        Thread.Sleep(WAITTIME);
                        judgeIsPoint(5);

                        Sort.srcData[0] = Sort.srcData[j + 1];
                        m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 6, true, 0, Sort.srcData[0]);  //6
                        Thread.Sleep(WAITTIME);
                        judgeIsPoint(6);

                        Sort.srcData[j + 1] = Sort.srcData[j];
                        m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 7, true, j + 1, Sort.srcData[j + 1]);  //7
                        Thread.Sleep(WAITTIME);
                        judgeIsPoint(7);

                        Sort.srcData[j] = Sort.srcData[0];
                        m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 8, true, j, Sort.srcData[j]);  //8
                        Thread.Sleep(WAITTIME);
                        judgeIsPoint(8);

                        swap = 1;
                        m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 9, true, 0, swap);  //9
                        Thread.Sleep(WAITTIME);
                        judgeIsPoint(9);
                    }
                    m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 3, false, 0, 0);  //3
                    Thread.Sleep(WAITTIME);
                    judgeIsPoint(3);
                }

                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 10, false, 0, 0);  //10
                Thread.Sleep(WAITTIME);
                judgeIsPoint(10);

                if (swap == 0) break;

                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 11, false, 0, 0);  //11
                Thread.Sleep(WAITTIME);
                judgeIsPoint(11);

                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 1, true, Sort.length - i, 0);  //1//排序一次完将最大的数置为红色
                Thread.Sleep(WAITTIME);
                judgeIsPoint(1);
            }

            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 11, false, 0, 0);  //11
            Thread.Sleep(WAITTIME);
            judgeIsPoint(11);

            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_delegateExeFinish, 9);//通知主线程算法执行完毕
        }
        /// <summary>
        /// 交换（冒泡）排序单步执行
        /// </summary>
        private void swapSortStep()
        {
            int i, j;
            int swap;
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 1, false, 0, 0);      //1
            MainDemon.allDone.WaitOne();

            for (i = 1; i < Sort.length; i++)
            {
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 2, true, 0, i);  //2
                MainDemon.allDone.Reset();
                MainDemon.allDone.WaitOne();

                swap = 0;
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 3, true, 0, swap);  //3
                MainDemon.allDone.Reset();
                MainDemon.allDone.WaitOne();

                for (j = 1; j < Sort.length - i; j++)
                {
                    m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 4, true, 0, j);  //4
                    MainDemon.allDone.Reset();
                    MainDemon.allDone.WaitOne();

                    if (Sort.srcData[j] > Sort.srcData[j + 1])//将大的往后甩
                    {
                        m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 5, false, 0, 0);  //5
                        MainDemon.allDone.Reset();
                        MainDemon.allDone.WaitOne();

                        Sort.srcData[0] = Sort.srcData[j + 1];
                        m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 6, true, 0, Sort.srcData[0]);  //6
                        MainDemon.allDone.Reset();
                        MainDemon.allDone.WaitOne();

                        Sort.srcData[j + 1] = Sort.srcData[j];
                        m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 7, true, j + 1, Sort.srcData[j + 1]);  //7
                        MainDemon.allDone.Reset();
                        MainDemon.allDone.WaitOne();

                        Sort.srcData[j] = Sort.srcData[0];
                        m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 8, true, j, Sort.srcData[j]);  //8
                        MainDemon.allDone.Reset();
                        MainDemon.allDone.WaitOne();

                        swap = 1;
                        m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 9, true, 0, swap);  //9
                        MainDemon.allDone.Reset();
                        MainDemon.allDone.WaitOne();
                    }
                    m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 3, false, 0, 0);  //3
                    MainDemon.allDone.Reset();
                    MainDemon.allDone.WaitOne();
                }

                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 10, false, 0, 0);  //10
                MainDemon.allDone.Reset();
                MainDemon.allDone.WaitOne();

                if (swap == 0) break;

                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 11, false, 0, 0);  //11
                MainDemon.allDone.Reset();
                MainDemon.allDone.WaitOne();

                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 1, true, Sort.length - i, 0);  //1//排序一次完将最大的数置为红色
                MainDemon.allDone.Reset();
                MainDemon.allDone.WaitOne();
            }

            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 11, false, 0, 0);  //11
            MainDemon.allDone.Reset();
            MainDemon.allDone.WaitOne();
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_delegateExeFinish, 9);//通知主线程算法执行完毕
        }
        /// <summary>
        /// 交换（冒泡）排序全速执行
        /// </summary>
        private void swapSortRun()
        {
            int i, j;
            int swap;
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 1, false, 0, 0);      //1
            Thread.Sleep(WAITTIME);

            for (i = 1; i < Sort.length; i++)
            {
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 2, true, 0, i);  //2
                Thread.Sleep(WAITTIME);

                swap = 0;
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 3, true, 0, swap);  //3
                Thread.Sleep(WAITTIME);

                for (j = 1; j < Sort.length - i; j++)
                {
                    m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 4, true, 0, j);  //4
                    Thread.Sleep(WAITTIME);

                    if (Sort.srcData[j] > Sort.srcData[j + 1])//将大的往后甩
                    {
                        m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 5, false, 0, 0);  //5
                        Thread.Sleep(WAITTIME);

                        Sort.srcData[0] = Sort.srcData[j + 1];
                        m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 6, true, 0, Sort.srcData[0]);  //6
                        Thread.Sleep(WAITTIME);

                        Sort.srcData[j + 1] = Sort.srcData[j];
                        m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 7, true, j + 1, Sort.srcData[j + 1]);  //7
                        Thread.Sleep(WAITTIME);

                        Sort.srcData[j] = Sort.srcData[0];
                        m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 8, true, j, Sort.srcData[j]);  //8
                        Thread.Sleep(WAITTIME);

                        swap = 1;
                        m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 9, true, 0, swap);  //9
                        Thread.Sleep(WAITTIME);
                    }
                    m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 3, false, 0, 0);  //3
                    Thread.Sleep(WAITTIME);
                }

                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 10, false, 0, 0);  //10
                Thread.Sleep(WAITTIME);

                if (swap == 0) break;

                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 11, false, 0, 0);  //11
                Thread.Sleep(WAITTIME);

                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 1, true, Sort.length - i, 0);  //1//排序一次完将最大的数置为红色
                Thread.Sleep(WAITTIME);
            }

            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 11, false, 0, 0);  //11
            Thread.Sleep(WAITTIME);
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_delegateExeFinish, 9);//通知主线程算法执行完毕
        }

        private void insertSort(int operatorFlag)
        {
            if (operatorFlag == 1) insertSortRun();//直接插入排序全速执行
            if (operatorFlag == 2) insertSortStep();//直接插入排序单步执行
            if (operatorFlag == 3) insertSortRunTo();//直接插入排序断点执行到 
        }

        private void insertSortRunTo()
        {
            int i, j;
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 2, false, 0, 0);      //2
            Thread.Sleep(WAITTIME);
            judgeIsPoint(2);

            for (i = 2; i < Sort.length; i++)
            {
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 3, true, 0, i);   //3//对外层循环索引变量i赋值
                Thread.Sleep(WAITTIME);
                judgeIsPoint(3);

                if (Sort.srcData[i] < Sort.srcData[i - 1])
                {
                    m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 4, false, 0, 0);  //4
                    Thread.Sleep(WAITTIME);
                    judgeIsPoint(4);

                    Sort.srcData[0] = Sort.srcData[i];
                    m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 5, true, 10000, Sort.srcData[0]);   //5
                    Thread.Sleep(WAITTIME);
                    judgeIsPoint(5);

                    for (j = i - 1; Sort.srcData[0] < Sort.srcData[j]; j--)
                    {
                        m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 6, true, 0, j);   //6
                        Thread.Sleep(WAITTIME);
                        judgeIsPoint(6);

                        Sort.srcData[j + 1] = Sort.srcData[j];
                        m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 5, true, j + 1, Sort.srcData[j + 1]);  //5
                        Thread.Sleep(WAITTIME);
                        judgeIsPoint(5);
                    }
                    m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 7, false, 0, 0);  //7
                    Thread.Sleep(WAITTIME);
                    judgeIsPoint(7);

                    Sort.srcData[j + 1] = Sort.srcData[0];
                    m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 8, true, j + 1, Sort.srcData[j + 1]);  //8
                    Thread.Sleep(WAITTIME);
                    judgeIsPoint(8);
                }

                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 2, false, 0, 0);
                Thread.Sleep(WAITTIME);
                judgeIsPoint(2);
            }

            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 9, false, 0, 0);
            Thread.Sleep(WAITTIME);
            judgeIsPoint(9);

            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_delegateExeFinish, 8);//通知主线程算法执行完毕
        }

        private void insertSortStep()
        {
            int i, j;
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 2, false, 0, 0);      //2
            MainDemon.allDone.WaitOne();

            for (i = 2; i < Sort.length; i++)
            {
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 3, true, 0, i);   //3//对外层循环索引变量i赋值
                MainDemon.allDone.Reset();
                MainDemon.allDone.WaitOne();

                if (Sort.srcData[i] < Sort.srcData[i - 1])
                {
                    m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 4, false, 0, 0);  //4
                    MainDemon.allDone.Reset();
                    MainDemon.allDone.WaitOne();

                    Sort.srcData[0] = Sort.srcData[i];
                    m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 5, true, 10000, Sort.srcData[0]);   //5
                    MainDemon.allDone.Reset();
                    MainDemon.allDone.WaitOne();

                    for (j = i - 1; Sort.srcData[0] < Sort.srcData[j]; j--)
                    {
                        m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 6, true, 0, j);   //6
                        MainDemon.allDone.Reset();
                        MainDemon.allDone.WaitOne();

                        Sort.srcData[j + 1] = Sort.srcData[j];
                        m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 5, true, j + 1, Sort.srcData[j + 1]);  //5
                        MainDemon.allDone.Reset();
                        MainDemon.allDone.WaitOne();
                    }
                    m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 7, false, 0, 0);  //7
                    MainDemon.allDone.Reset();
                    MainDemon.allDone.WaitOne();

                    Sort.srcData[j + 1] = Sort.srcData[0];
                    m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 8, true, j + 1, Sort.srcData[j + 1]);  //8
                    MainDemon.allDone.Reset();
                    MainDemon.allDone.WaitOne();
                }

                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 2, false, 0, 0);
                MainDemon.allDone.Reset();
                MainDemon.allDone.WaitOne();
            }

            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 9, false, 0, 0);
            MainDemon.allDone.Reset();
            MainDemon.allDone.WaitOne();

            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_delegateExeFinish, 8);//通知主线程算法执行完毕
        }

        private void insertSortRun()
        {
            int i, j;
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 2, false, 0, 0);      //2
            Thread.Sleep(WAITTIME);

            for(i=2;i<Sort.length;i++)
            {
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 3, true, 0, i);   //3//对外层循环索引变量i赋值
                Thread.Sleep(WAITTIME);

                if (Sort.srcData[i] < Sort.srcData[i-1])
                {
                    m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 4, false, 0, 0);  //4
                    Thread.Sleep(WAITTIME);

                    Sort.srcData[0] = Sort.srcData[i];
                    m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 5, true, 10000, Sort.srcData[0]);   //5
                    Thread.Sleep(WAITTIME);

                    for (j = i - 1; Sort.srcData[0] < Sort.srcData[j]; j--)
                    {
                        m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 6, true, 0, j);   //6
                        Thread.Sleep(WAITTIME);

                        Sort.srcData[j + 1] = Sort.srcData[j];

                        m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 5, true, j+1, Sort.srcData[j + 1]);  //5
                        Thread.Sleep(WAITTIME);
                    }
                    m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 7, false, 0, 0);  //7
                    Thread.Sleep(WAITTIME);

                    Sort.srcData[j + 1] = Sort.srcData[0];

                    m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 8, true, j+1, Sort.srcData[j + 1]);  //8
                    Thread.Sleep(WAITTIME);
                }

                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 2, false, 0, 0);
                Thread.Sleep(WAITTIME);
            }

            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 9, false, 0, 0);
            Thread.Sleep(WAITTIME);

            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_delegateExeFinish, 8);//通知主线程算法执行完毕
        }

        private void binarySearch(int operatorFlag)
        {
            if (operatorFlag == 1) binarySearchRun();//折半查找全速执行
            if (operatorFlag == 2) binarySearchStep();//折半查找单步执行
            if (operatorFlag == 3) binarySearchRunTo();//折半查找断点执行到 
        }

        private void binarySearchRunTo()
        {
            char key = Search.searchData;
            int low = 1, high = Search.length;
            int mid = 0;
            int mid_temp = (low + high) / 2;
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 3, false, 0, 0);  //3
            Thread.Sleep(WAITTIME);
            judgeIsPoint(3);

            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 4, true, 10000, 0);  //4//初始化low和high，
            Thread.Sleep(WAITTIME);
            judgeIsPoint(4);
            while (low <= high)
            {
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 5, false, 0, 0);  //4
                Thread.Sleep(WAITTIME);
                judgeIsPoint(5);

                mid = (low + high) / 2;
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 6, true, mid_temp, mid);  //6//mid赋值
                Thread.Sleep(WAITTIME);
                judgeIsPoint(6);

                if (key == Search.srcData_BinSearch[mid - 1])
                {
                    m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 6, true, mid_temp, mid);  //6//找到元素给mid赋值并对比
                    Thread.Sleep(WAITTIME);
                    judgeIsPoint(6);

                    m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_delegateExeFinish, 7);//通知主线程算法执行完毕
                    //return mid;
                    break;
                }
                else if (key < Search.srcData_BinSearch[mid - 1])
                {
                    high = mid - 1;
                    m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 7, true, mid, high);  //7//移动high,改变high
                    Thread.Sleep(WAITTIME);
                    judgeIsPoint(7);
                }
                else //if(key > Search.srcData_BinSearch[mid-1])
                {
                    low = mid + 1;
                    m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 8, true, mid, low);  //8//移动low,改变low
                    Thread.Sleep(WAITTIME);
                    judgeIsPoint(8);
                }

                mid_temp = mid;

                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 9, false, 0, 0);  //9
                Thread.Sleep(WAITTIME);
                judgeIsPoint(9);

                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 4, false, 0, 0);  //4
                Thread.Sleep(WAITTIME);
                judgeIsPoint(4);
            }

            if (low > high)
            {
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 10, false, 0, 0);  //10
                Thread.Sleep(WAITTIME);
                judgeIsPoint(10);

                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 11, false, 0, 0);  //11
                Thread.Sleep(WAITTIME);
                judgeIsPoint(11);

                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_delegateExeFinish, 7);//通知主线程算法执行完毕
            }
        }

        private void binarySearchStep()
        {
            char key = Search.searchData;
            int low = 1, high = Search.length;
            int mid = 0;
            int mid_temp = (low + high) / 2;
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 3, false, 0, 0);  //3
            MainDemon.allDone.WaitOne();

            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 4, true, 10000, 0);  //4//初始化low和high，
            MainDemon.allDone.Reset();
            MainDemon.allDone.WaitOne();

            while (low <= high)
            {
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 5, false, 0, 0);  //4
                MainDemon.allDone.Reset();
                MainDemon.allDone.WaitOne();

                mid = (low + high) / 2;
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 6, true, mid_temp, mid);  //6//mid赋值
                MainDemon.allDone.Reset();
                MainDemon.allDone.WaitOne();

                if (key == Search.srcData_BinSearch[mid - 1])
                {
                    m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 6, true, mid_temp, mid);  //6//找到元素给mid赋值并对比
                    MainDemon.allDone.Reset();
                    MainDemon.allDone.WaitOne();

                    m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_delegateExeFinish, 7);//通知主线程算法执行完毕
                    //return mid;
                    break;
                }
                else if (key < Search.srcData_BinSearch[mid - 1])
                {
                    high = mid - 1;
                    m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 7, true, mid, high);  //7//移动high,改变highv
                    MainDemon.allDone.Reset();
                    MainDemon.allDone.WaitOne();
                }
                else //if(key > Search.srcData_BinSearch[mid-1])
                {
                    low = mid + 1;
                    m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 8, true, mid, low);  //8//移动low,改变low
                    MainDemon.allDone.Reset();
                    MainDemon.allDone.WaitOne();
                }

                mid_temp = mid;

                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 9, false, 0, 0);  //9
                MainDemon.allDone.Reset();
                MainDemon.allDone.WaitOne();

                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 4, false, 0, 0);  //4
                MainDemon.allDone.Reset();
                MainDemon.allDone.WaitOne();
            }

            if (low > high)
            {
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 10, false, 0, 0);  //10
                MainDemon.allDone.Reset();
                MainDemon.allDone.WaitOne();

                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 11, false, 0, 0);  //11
                MainDemon.allDone.Reset();
                MainDemon.allDone.WaitOne();

                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_delegateExeFinish, 7);//通知主线程算法执行完毕
            }
        }

        private void binarySearchRun()
        {
            char key = Search.searchData;
            int low = 1, high = Search.length;
            int mid = 0;
            int mid_temp = (low + high) / 2;
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 3, false, 0, 0);  //3
            Thread.Sleep(WAITTIME);

            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 4, true, 10000, 0);  //4//初始化low和high，
            Thread.Sleep(WAITTIME);
            while (low <=high)
            {
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 5, false, 0, 0);  //4
                Thread.Sleep(WAITTIME);
                
                mid = (low + high) / 2;
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 6, true, mid_temp, mid);  //6//mid赋值
                Thread.Sleep(WAITTIME);

                if (key == Search.srcData_BinSearch[mid-1])
                {
                    m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 6, true, mid_temp, mid);  //6//找到元素给mid赋值并对比
                    Thread.Sleep(WAITTIME);

                    m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_delegateExeFinish, 7);//通知主线程算法执行完毕
                    //return mid;
                    break;
                }
                else if(key < Search.srcData_BinSearch[mid-1])
                {
                    high = mid - 1;
                    m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 7, true, mid, high);  //7//移动high,改变high
                    Thread.Sleep(WAITTIME);
                }
                else //if(key > Search.srcData_BinSearch[mid-1])
                {
                    low = mid + 1;
                    m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 8, true, mid, low);  //8//移动low,改变low
                    Thread.Sleep(WAITTIME);
                }

                mid_temp = mid;

                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 9, false, 0, 0);  //9
                Thread.Sleep(WAITTIME);

                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 4, false, 0, 0);  //4
                Thread.Sleep(WAITTIME);
            }

            if (low > high)
            {
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 10, false, 0, 0);  //10
                Thread.Sleep(WAITTIME);

                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 11, false, 0, 0);  //11
                Thread.Sleep(WAITTIME);

                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_delegateExeFinish, 7);//通知主线程算法执行完毕
            }
        }

        private void orderSearch(int operatorFlag)
        {
            if (operatorFlag == 1) orderSearchRun();//顺序查找全速执行
            if (operatorFlag == 2) orderSearchStep();//顺序查找单步执行
            if (operatorFlag == 3) orderSearchRunTo();//顺序查找断点执行到
        }

        private void orderSearchRunTo()
        {
            char key = Search.searchData;
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 3, false, 0, 0);  //3
            Thread.Sleep(WAITTIME);
            judgeIsPoint(3);

            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 4, true, 10000, key);   //4
            Thread.Sleep(WAITTIME);
            judgeIsPoint(4);
            int i = 0;
            for (i = Search.length - 1; (Search.srcData_OrderSearch[i] != key) && (i >= 0); --i)
            {
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 4, true, i, i);
                Thread.Sleep(WAITTIME);
                judgeIsPoint(4);
            }
            //return i;
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 5, true, i, i);
            Thread.Sleep(WAITTIME);
            judgeIsPoint(5);

            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 6, false, 0, 0);
            Thread.Sleep(WAITTIME);
            judgeIsPoint(6);

            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_delegateExeFinish, 6);//通知主线程算法执行完毕
        }

        private void orderSearchStep()
        {
            char key = Search.searchData;
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 3, false, 0, 0);  //3
            MainDemon.allDone.WaitOne();

            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 4, true, 10000, key);   //4
            MainDemon.allDone.Reset();
            MainDemon.allDone.WaitOne();

            int i = 0;
            for (i = Search.length - 1; (Search.srcData_OrderSearch[i] != key) && (i >= 0); --i)
            {
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 4, true, i, i);
                MainDemon.allDone.Reset();
                MainDemon.allDone.WaitOne();
            }
            //return i;
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 5, true, i, i);
            MainDemon.allDone.Reset();
            MainDemon.allDone.WaitOne();

            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 6, false, 0, 0);
            MainDemon.allDone.Reset();
            MainDemon.allDone.WaitOne();

            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_delegateExeFinish, 6);//通知主线程算法执行完毕
        }

        private void orderSearchRun()
        {
            char key = Search.searchData;
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 3, false, 0, 0);  //3
            Thread.Sleep(WAITTIME);

            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 4, true, 10000, key);   //4
            Thread.Sleep(WAITTIME);
            int i = 0;
            for (i = Search.length - 1; (Search.srcData_OrderSearch[i]!=key)&&(i>=0); --i)
            {
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 4, true, i, i);
                Thread.Sleep(WAITTIME);
            }
            //return i;
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 5, true, i, i);
            Thread.Sleep(WAITTIME);

            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 6, false, 0, 0);
            Thread.Sleep(WAITTIME);

            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_delegateExeFinish, 6);//通知主线程算法执行完毕
        }

        private void linkedDelete(int operatorFlag)
        {
            if (operatorFlag == 1) linkedDeleteRun();//链表删除全速执行
            if (operatorFlag == 2) linkedDeleteStep();//链表删除单步执行
            if (operatorFlag == 3) linkedDeleteRunTo();//链表删除断点执行到
        }

        private void linkedDeleteRunTo()
        {
            LinkedNode p, q = null;
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 2, false, 0, 0);       //2
            Thread.Sleep(WAITTIME);
            judgeIsPoint(2);

            p = LinkedList.head;
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 3, false, 0, 0);        //3
            Thread.Sleep(WAITTIME);
            judgeIsPoint(3);

            int j = 0;
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 4, true, 0, j);       //4//p指向头结点
            Thread.Sleep(WAITTIME);
            judgeIsPoint(4);

            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 5, true, 0, 0);       //5//j==0
            Thread.Sleep(WAITTIME);
            judgeIsPoint(5);

            while ((p.next != null) && j < LinkedList.deletePosition - 1 - 1)//找到删除位置LinkedList.deletePosition - 1 -1的前驱
            {
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 6, false, 0, 0);  //6
                Thread.Sleep(WAITTIME);
                judgeIsPoint(6);

                p = p.next;
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 7, true, j, j);  //7//p结点改变指向
                Thread.Sleep(WAITTIME);
                judgeIsPoint(7);

                j++;
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 8, true, 0, j);  //8//j值开始改变
                Thread.Sleep(WAITTIME);
                judgeIsPoint(8);

                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 5, false, 0, 0);  //5
                Thread.Sleep(WAITTIME);
                judgeIsPoint(5);

            }
            //删除位置不在        //未找到删除位置
            if ((p.next == null) || (j != LinkedList.insertPosition - 1 - 1)) { }//return ERROR; //i小于1或者大于表长
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 9, false, 0, 0);  //9//判断是否合理
            Thread.Sleep(WAITTIME);
            judgeIsPoint(9);

            q = p.next;
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 10, false, 0, 0);  //10
            Thread.Sleep(WAITTIME);
            judgeIsPoint(10);

            p.next = q.next;
            //代码区11行找到删除元素位置并图色,变量区Q指向值改变
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 11, true, j, q.data);  //11
            Thread.Sleep(WAITTIME);
            judgeIsPoint(11);

            LinkedList.deleteData = q.data;
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 12, true, j, 0);  //12//删除结点的前驱的next开始重新指向删除结点的后继
            Thread.Sleep(WAITTIME);
            judgeIsPoint(12);

            //free(q);
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 13, true, 0, q.data);  //13//删除元素返回值
            Thread.Sleep(WAITTIME);
            judgeIsPoint(13);

            //return OK;
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 14, true, 0, j);  //14//释放删除结点（去掉删除元素结点同时，去掉左右箭头）
            Thread.Sleep(WAITTIME);
            judgeIsPoint(14);
            //"}//ListInsert_L"
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 15, false, 0, 0);  //15//程序结束 
            Thread.Sleep(WAITTIME);
            judgeIsPoint(15);

            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_delegateExeFinish, 5);//通知主线程算法执行完毕
        }

        private void linkedDeleteStep()
        {
            LinkedNode p, q = null;
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 2, false, 0, 0);       //2
            MainDemon.allDone.WaitOne();

            p = LinkedList.head;
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 3, false, 0, 0);        //3
            MainDemon.allDone.Reset();
            MainDemon.allDone.WaitOne();

            int j = 0;
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 4, true, 0, j);       //4//p指向头结点
            MainDemon.allDone.Reset();
            MainDemon.allDone.WaitOne();

            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 5, true, 0, 0);       //5//j==0
            MainDemon.allDone.Reset();
            MainDemon.allDone.WaitOne();

            while ((p.next != null) && j < LinkedList.deletePosition - 1 - 1)//找到删除位置LinkedList.deletePosition - 1 -1的前驱
            {
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 6, false, 0, 0);  //6
                MainDemon.allDone.Reset();
                MainDemon.allDone.WaitOne();

                p = p.next;
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 7, true, j, j);  //7//p结点改变指向
                MainDemon.allDone.Reset();
                MainDemon.allDone.WaitOne();

                j++;
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 8, true, 0, j);  //8//j值开始改变
                MainDemon.allDone.Reset();
                MainDemon.allDone.WaitOne();

                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 5, false, 0, 0);  //5
                MainDemon.allDone.Reset();
                MainDemon.allDone.WaitOne();

            }
            //删除位置不在        //未找到删除位置
            if ((p.next == null) || (j != LinkedList.insertPosition - 1 - 1)) { }//return ERROR; //i小于1或者大于表长
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 9, false, 0, 0);  //9//判断是否合理
            MainDemon.allDone.Reset();
            MainDemon.allDone.WaitOne();

            q = p.next;
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 10, false, 0, 0);  //10
            MainDemon.allDone.Reset();
            MainDemon.allDone.WaitOne();

            p.next = q.next;
            //代码区11行找到删除元素位置并图色,变量区Q指向值改变
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 11, true, j, q.data);  //11
            MainDemon.allDone.Reset();
            MainDemon.allDone.WaitOne();

            LinkedList.deleteData = q.data;
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 12, true, j, 0);  //12//删除结点的前驱的next开始重新指向删除结点的后继
            MainDemon.allDone.Reset();
            MainDemon.allDone.WaitOne();

            //free(q);
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 13, true, 0, q.data);  //13//删除元素返回值
            MainDemon.allDone.Reset();
            MainDemon.allDone.WaitOne();

            //return OK;
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 14, true, 0, j);  //14//释放删除结点（去掉删除元素结点同时，去掉左右箭头）
            MainDemon.allDone.Reset();
            MainDemon.allDone.WaitOne();
            //"}//ListInsert_L"
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 15, false, 0, 0);  //15//程序结束 
            MainDemon.allDone.Reset();
            MainDemon.allDone.WaitOne();
            MainDemon.allDone.Reset();
            MainDemon.allDone.WaitOne();

            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_delegateExeFinish, 5);//通知主线程算法执行完毕
        }

        private void linkedDeleteRun()
        {
            LinkedNode p, q = null;
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 2, false, 0, 0);       //2
            Thread.Sleep(WAITTIME);

            p = LinkedList.head;
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 3, false, 0, 0);        //3
            Thread.Sleep(WAITTIME);

            int j = 0;
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 4, true, 0, j);       //4//p指向头结点
            Thread.Sleep(WAITTIME);

            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 5, true, 0, 0);       //5//j==0
            Thread.Sleep(WAITTIME);

            while ((p.next!=null) && j < LinkedList.deletePosition-1-1)//找到删除位置LinkedList.deletePosition - 1 -1的前驱
            {
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 6, false, 0, 0);  //6
                Thread.Sleep(WAITTIME);

                p = p.next;
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 7, true, j, j);  //7//p结点改变指向
                Thread.Sleep(WAITTIME);

                j++;
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 8, true, 0, j);  //8//j值开始改变
                Thread.Sleep(WAITTIME);

                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 5, false, 0, 0);  //5
                Thread.Sleep(WAITTIME);

            }
            //删除位置不在        //未找到删除位置
            if ((p.next==null) || (j!=LinkedList.insertPosition-1-1)) { }//return ERROR; //i小于1或者大于表长
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 9, false, 0, 0);  //9//判断是否合理
            Thread.Sleep(WAITTIME);

            q = p.next;
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 10, false, 0, 0);  //10
            Thread.Sleep(WAITTIME);

            p.next = q.next;
            //代码区11行找到删除元素位置并图色,变量区Q指向值改变
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 11, true, j, q.data);  //11
            Thread.Sleep(WAITTIME);

            LinkedList.deleteData = q.data;
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 12, true, j,0);  //12//删除结点的前驱的next开始重新指向删除结点的后继
            Thread.Sleep(WAITTIME);

            //free(q);
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 13, true, 0, q.data);  //13//删除元素返回值
            Thread.Sleep(WAITTIME);

            //return OK;
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 14, true, 0, j);  //14//释放删除结点（去掉删除元素结点同时，去掉左右箭头）
            Thread.Sleep(WAITTIME);
            //"}//ListInsert_L"
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 15, false, 0, 0);  //15//程序结束 
            Thread.Sleep(WAITTIME);

            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_delegateExeFinish, 5);//通知主线程算法执行完毕
        }

        private void linkedInsert(int operatorFlag)
        {
            if (operatorFlag == 1) linkedInsertRun();//链表插入全速执行
            if (operatorFlag == 2) linkedInsertStep();//链表插入单步执行
            if (operatorFlag == 3) linkedInsertRunTo();//链表插入断点执行到
        }

        private void linkedInsertRunTo()
        {
            LinkedNode p, s = null;
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 2, false, 0, 0);       //2
            Thread.Sleep(WAITTIME);
            judgeIsPoint(2);

            p = LinkedList.head;
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 3, false, 0, 0);        //3
            Thread.Sleep(WAITTIME);
            judgeIsPoint(3);

            int j = 0;
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 4, true, 0, j);       //4//p指向头结点
            Thread.Sleep(WAITTIME);
            judgeIsPoint(4);

            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 5, true, 0, 0);       //5//j==0
            Thread.Sleep(WAITTIME);
            judgeIsPoint(5);

            while ((p.next != null) && j != LinkedList.insertPosition - 1)
            {
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 6, false, 0, 0);  //6
                Thread.Sleep(WAITTIME);
                judgeIsPoint(6);

                p = p.next;
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 7, true, j, j);  //7//p结点改变指向
                Thread.Sleep(WAITTIME);
                judgeIsPoint(7);

                j++;
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 8, true, 0, j);  //8//j值开始改变
                Thread.Sleep(WAITTIME);
                judgeIsPoint(8);

                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 5, false, 0, 0);  //5
                Thread.Sleep(WAITTIME);
                judgeIsPoint(5);

            }
            if ((p == null) || (j > LinkedList.insertPosition - 1)) { }//return ERROR; //i小于1或者大于表长
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 9, false, 0, 0);  //9//判断是否合理
            Thread.Sleep(WAITTIME);
            judgeIsPoint(9);

            s = new LinkedNode(null);//生成新结点
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 10, false, 0, 0);  //10
            Thread.Sleep(WAITTIME);
            judgeIsPoint(10);

            s.data = LinkedList.insertData;
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 11, true, j, 0);  //11//生成新的结点
            Thread.Sleep(WAITTIME);
            judgeIsPoint(11);

            s.next = p.next;
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 12, true, 0, LinkedList.insertData);  //12//新的结点赋值
            Thread.Sleep(WAITTIME);
            judgeIsPoint(12);

            p.next = s;
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 13, true, j, 0);  //13//右边连接 
            Thread.Sleep(WAITTIME);
            judgeIsPoint(13);

            //return OK;
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 14, true, j, 0);  //14//左边连接 
            Thread.Sleep(WAITTIME);
            judgeIsPoint(14);
            //"}//ListInsert_L"
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 15, false, 0, 0);  //15//程序结束 
            Thread.Sleep(WAITTIME);
            judgeIsPoint(15);

            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_delegateExeFinish, 4);//通知主线程算法执行完毕
        }

        private void linkedInsertStep()
        {
            LinkedNode p, s = null;
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 2, false, 0, 0);       //2
            MainDemon.allDone.WaitOne();

            p = LinkedList.head;
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 3, false, 0, 0);        //3
            MainDemon.allDone.Reset();
            MainDemon.allDone.WaitOne();

            int j = 0;
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 4, true, 0, j);       //4//p指向头结点
            MainDemon.allDone.Reset();
            MainDemon.allDone.WaitOne();

            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 5, true, 0, 0);       //5//j==0
            MainDemon.allDone.Reset();
            MainDemon.allDone.WaitOne();

            while ((p.next != null) && j < LinkedList.insertPosition - 1)
            {
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 6, false, 0, 0);  //6
                MainDemon.allDone.Reset();
                MainDemon.allDone.WaitOne();

                p = p.next;

                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 7, true, j, j);  //7//p结点改变指向
                MainDemon.allDone.Reset();
                MainDemon.allDone.WaitOne();

                j++;
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 8, true, 0, j);  //8//j值开始改变
                MainDemon.allDone.Reset();
                MainDemon.allDone.WaitOne();

                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 5, false, 0, 0);  //5
                MainDemon.allDone.Reset();
                MainDemon.allDone.WaitOne();

            }
            if ((p == null) || (j != LinkedList.insertPosition - 1)) { }//return ERROR; //i小于1或者大于表长
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 9, false, 0, 0);  //9//判断是否合理
            MainDemon.allDone.Reset();
            MainDemon.allDone.WaitOne();

            s = new LinkedNode(null);//生成新结点
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 10, false, 0, 0);  //10
            MainDemon.allDone.Reset();
            MainDemon.allDone.WaitOne();

            s.data = LinkedList.insertData;
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 11, true, j, 0);  //11//生成新的结点
            MainDemon.allDone.Reset();
            MainDemon.allDone.WaitOne();

            s.next = p.next;
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 12, true, 0, LinkedList.insertData);  //12//新的结点赋值
            MainDemon.allDone.Reset();
            MainDemon.allDone.WaitOne();

            p.next = s;
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 13, true, j, 0);  //13//右边连接 
            MainDemon.allDone.Reset();
            MainDemon.allDone.WaitOne();

            //return OK;
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 14, true, j, 0);  //14//左边连接 
            MainDemon.allDone.Reset();
            MainDemon.allDone.WaitOne();
            //"}//ListInsert_L"
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 15, false, 0, 0);  //15//程序结束 
            MainDemon.allDone.Reset();
            MainDemon.allDone.WaitOne();

            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_delegateExeFinish, 4);//通知主线程算法执行完毕
        }

        private void linkedInsertRun()
        {
            LinkedNode p, s=null;
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 2, false, 0, 0);       //2
            Thread.Sleep(WAITTIME);

            p = LinkedList.head;
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 3, false, 0, 0);        //3
            Thread.Sleep(WAITTIME);

            int j = 0;
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 4, true, 0, j);       //4//p指向头结点
            Thread.Sleep(WAITTIME);

            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 5, true, 0, 0);       //5//j==0
            Thread.Sleep(WAITTIME);

            while ((p.next != null) && j<LinkedList.insertPosition-1)
            {
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 6, false, 0, 0);  //6
                Thread.Sleep(WAITTIME);

                p = p.next;

                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 7, true, j, j);  //7//p结点改变指向
                Thread.Sleep(WAITTIME);

                j++;
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 8, true, 0, j);  //8//j值开始改变
                Thread.Sleep(WAITTIME);
                
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 5, false, 0, 0);  //5
                Thread.Sleep(WAITTIME);

            }
            if ((p == null) || (j != LinkedList.insertPosition-1)) { }//return ERROR; //i小于1或者大于表长
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 9, false, 0, 0);  //9//判断是否合理
            Thread.Sleep(WAITTIME);

            s = new LinkedNode(null);//生成新结点
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 10, false, 0, 0);  //10
            Thread.Sleep(WAITTIME);

            s.data = LinkedList.insertData;
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 11, true, j, 0);  //11//生成新的结点
            Thread.Sleep(WAITTIME);

            s.next = p.next;
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 12, true, 0, LinkedList.insertData);  //12//新的结点赋值
            Thread.Sleep(WAITTIME);

            p.next = s;
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 13, true, j, 0);  //13//右边连接 
            Thread.Sleep(WAITTIME);

            //return OK;
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 14, true, j, 0);  //14//左边连接 
            Thread.Sleep(WAITTIME);
            //"}//ListInsert_L"
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 15, false, 0, 0);  //15//程序结束 
            Thread.Sleep(WAITTIME);

            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_delegateExeFinish, 4);//通知主线程算法执行完毕
        }

        /// <summary>
        /// 链表创建
        /// </summary>
        /// <param name="operatorFlag"></param>
        private void linkedCreate(int operatorFlag)
        { 
            if (operatorFlag == 1) linkedCreateRun();//链表创建全速执行
            if (operatorFlag == 2) linkedCreateStep();//链表创建单步执行
            if (operatorFlag == 3) linkedCreateRunTo();//链表创建断点执行到
        }
        private void linkedCreateRunTo()
        {
            LinkedList.head = new LinkedNode(null);//定义一个空链表的头结点
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 2, false, 0, null);       //2
            Thread.Sleep(WAITTIME);
            judgeIsPoint(2);

            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 3, true, 10000, null);    //3
            Thread.Sleep(WAITTIME);
            judgeIsPoint(3);

            LinkedNode p;
            LinkedNode r = LinkedList.head.next;
            for (int i = 0; i < LinkedList.srcData.Length; i++)
            {
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 4, true, 0, i);   //4
                Thread.Sleep(WAITTIME);
                judgeIsPoint(4);

                p = new LinkedNode(null);
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 5, true, i, null);   //5
                Thread.Sleep(WAITTIME);
                judgeIsPoint(5);

                p.data = LinkedList.srcData[i];
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 6, true, i, LinkedList.srcData[i]);   //6
                Thread.Sleep(WAITTIME);
                judgeIsPoint(6);

                p.next = r;
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 7, false, 0, null);   //7
                Thread.Sleep(WAITTIME);
                judgeIsPoint(7);

                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 8, false, 0, null);      //8
                Thread.Sleep(WAITTIME);
                judgeIsPoint(8);

                r = p;
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 3, true, i, null);   //3
                Thread.Sleep(WAITTIME);
                judgeIsPoint(3);
            }
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 9, false, 0, null);      //9
            Thread.Sleep(WAITTIME);
            judgeIsPoint(9);

            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_delegateExeFinish, 3);   //4
        }
        private void linkedCreateStep()
        {
            LinkedList.head = new LinkedNode(null);//定义一个空链表的头结点
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 2, false, 0, null);       //2
            MainDemon.allDone.WaitOne();

            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 3, true, 10000, null);    //3
            MainDemon.allDone.Reset();
            MainDemon.allDone.WaitOne();

            LinkedNode p;
            LinkedNode r = LinkedList.head.next;
            for (int i = 0; i < LinkedList.srcData.Length; i++)
            {
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 4, true, 0, i);   //4
                MainDemon.allDone.Reset();
                MainDemon.allDone.WaitOne();

                p = new LinkedNode(null);
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 5, true, i, null);   //5
                MainDemon.allDone.Reset();
                MainDemon.allDone.WaitOne();

                p.data = LinkedList.srcData[i];
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 6, true, i, LinkedList.srcData[i]);   //6
                MainDemon.allDone.Reset();
                MainDemon.allDone.WaitOne();

                p.next = r;
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 7, false, 0, null);   //7
                MainDemon.allDone.Reset();
                MainDemon.allDone.WaitOne();

                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 8, false, 0, null);      //8
                MainDemon.allDone.Reset();
                MainDemon.allDone.WaitOne();

                r = p;
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 3, true, i, null);   //3
                MainDemon.allDone.Reset();
                MainDemon.allDone.WaitOne();
            }
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 9, false, 0, null);      //9
            MainDemon.allDone.Reset();
            MainDemon.allDone.WaitOne();

            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_delegateExeFinish, 3);   //4
        }
        private void linkedCreateRun()
        {
            LinkedList.head = new LinkedNode(null);//定义一个空链表的头结点
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 2, false, 0, null);       //2
            Thread.Sleep(WAITTIME);

            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 3, true, 10000, null);    //3
            Thread.Sleep(WAITTIME);
            LinkedNode p;
            //LinkedNode r = LinkedList.head.next;
            for (int i = 0; i < LinkedList.srcData.Length; i++)
            {
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 4, true, 0, i);       //4
                Thread.Sleep(WAITTIME);

                p = new LinkedNode(null);
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 5, true, i, null);    //5
                Thread.Sleep(WAITTIME);

                p.data = LinkedList.srcData[i];
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 6, true, i, LinkedList.srcData[i]);   //6
                Thread.Sleep(WAITTIME);

                p.next = LinkedList.head.next;
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 7, false, 0, null);   //7
                Thread.Sleep(WAITTIME);

                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 8, false, 0, null);   //8
                Thread.Sleep(WAITTIME);

                LinkedList.head.next = p;
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 3, true, i, null);    //3
                Thread.Sleep(WAITTIME);
            }
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 9, false, 0, null);       //9
            Thread.Sleep(WAITTIME);
            LinkedNode pp = LinkedList.head.next;
            //for (int i = 0; i < LinkedList.length; i++)
            //{
            //    Trace.WriteLine(pp.data);
            //    pp = pp.next;
            //}
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_delegateExeFinish, 3);                
        }

        /// <summary>
        /// 顺序表删除
        /// </summary>
        /// <param name="operatorFlag"></param>
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
            MainDemon.allDone.WaitOne();

            if (SqList.delPosition < 1 || SqList.delPosition > SqList.length)//检查空表及删除位置的合理性
            {
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 4, false, 0, null);   //4
                MainDemon.allDone.Reset();
                MainDemon.allDone.WaitOne();

                //return ERROR;
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 5, false, 0, null);   //5
                MainDemon.allDone.Reset();
                MainDemon.allDone.WaitOne();
            }
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 4, false, 0, null);       //4
            MainDemon.allDone.Reset();
            MainDemon.allDone.WaitOne();


            string e = SqList.srcData_del[SqList.delPosition - 1].ToString();//被删除元素的值赋给e
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 6, false, 0, null);           //6
            MainDemon.allDone.Reset();
            MainDemon.allDone.WaitOne();

            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 7, true, 10000, e);           //7
            MainDemon.allDone.Reset();
            MainDemon.allDone.WaitOne();

            for (p = SqList.delPosition; p < SqList.length; p++)
            {
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 8, true, 0, p);       //8
                MainDemon.allDone.Reset();
                MainDemon.allDone.WaitOne();

                SqList.srcData_del[p - 1] = SqList.srcData_del[p];//向左移动
                m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 7, true, p, null);    //7
                MainDemon.allDone.Reset();
                MainDemon.allDone.WaitOne();
            }
            SqList.length--;
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 9, false, 0, null);        //9
            MainDemon.allDone.Reset();
            MainDemon.allDone.WaitOne();

            //return OK;
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 10, true, 0, SqList.length);//10
            MainDemon.allDone.Reset();
            MainDemon.allDone.WaitOne();
            //}
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 11, false, 0, null);       //11
            MainDemon.allDone.Reset();
            MainDemon.allDone.WaitOne();

            //顺序表删除算法执行完毕
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

        /// <summary>
        /// 顺序表插入
        /// </summary>
        /// <param name="operatorFlag"></param>
        private void orderInsert(int operatorFlag)
        {
            if(operatorFlag == 1) orderInsertRun();//顺序表插入全速执行
            if(operatorFlag == 2) orderInsertStep();//顺序表插入单步执行
            if (operatorFlag == 3) orderInsertRunTo();//顺序表断点执行到
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
            MainDemon.allDone.Reset();
            MainDemon.allDone.WaitOne();

            //顺序表插入算法执行完毕
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_delegateExeFinish, 1);
        }
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

