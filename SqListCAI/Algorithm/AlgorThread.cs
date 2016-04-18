using SqListCAI.Pages.MainPage;
using System.Threading;
using SqListCAI.Entities;

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
                default:
                    break;
                   
            }
        }

        private void binarySearch(int operatorFlag)
        {
            if (operatorFlag == 1) binarySearchRun();//折半查找全速执行
            //if (operatorFlag == 2) binarySearchStep();//折半查找单步执行
            //if (operatorFlag == 3) binarySearchRunTo();//折半查找断点执行到 
        }

        private void binarySearchRun()
        {
            char key = Search.searchData;
            int low = 1, high = Search.length;
            int mid = 0;
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 3, false, 0, 0);  //3
            Thread.Sleep(WAITTIME);

            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_DelegateStep, 4, true, 0, 0);  //4//初始化low和high
            Thread.Sleep(WAITTIME);
            while (low <=high)
            {
                mid = (low + high) / 2;
                if(key == Search.srcData_BinSearch[mid])
                {
                    //return mid;
                    break;
                }
            }
            m_mainDemon.Dispatcher.Invoke(m_mainDemon.m_delegateExeFinish, 7);//通知主线程算法执行完毕
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

