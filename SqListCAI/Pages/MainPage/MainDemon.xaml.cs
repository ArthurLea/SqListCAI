using System.Threading;
using System.Windows;
using System.Windows.Controls;
using SqListCAI.Dialogs;
using SqListCAI.Events;
using SqListCAI.Entities;
using SqListCAI.Algorithm;
using SqListCAI.Utils.SourceCodes;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Collections;
using System;
using System.Security.Permissions;
using System.Windows.Threading;
using System.Windows.Media.Animation;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.IO;
namespace SqListCAI.Pages.MainPage
{
    /// <summary>
    /// MianDemon.xaml 的交互逻辑
    /// </summary>
    public partial class MainDemon : Page
    {
        int flag = 0;//说明当前操作的算法
        public MainDemon()
        {
            InitializeComponent();
        }
        public MainDemon(string demon_name)
        {
            InitializeComponent();
            this.demon_lable_name.Content = demon_name;
        }
        public delegate void DelegateStep(int currentRow,bool moveFlag,int movePosition,object changeValue);
        public DelegateStep m_DelegateStep;

        public delegate void DelegateExeFinish(int flag);
        public DelegateExeFinish m_delegateExeFinish;
        //顺序表插入
        string srcData;
        char insertData;
        int position;
        public MainDemon(string demon_name,string srcData, char insertData,int position)
        {
            InitializeComponent();
            this.srcData = srcData;
            this.insertData = insertData;
            this.position = position;
            this.demon_lable_name.Content = demon_name;//初始化当前操作内容
            flag = 1;
            initUI(flag);
        }

        private void initUI(int flag)
        {
            switch(flag)
            {
                case 1:
                    {
                        Demonstration.data.Clear();
                        this.canse_demon.Children.Clear();
                        this.listBox_code.Items.Clear();
                        this.listView_value.DataContext = null;
                        SqList.init_SqList(srcData, insertData, position);
                        ShowDemon();
                        ShowCode();
                        ShowValue();
                        m_DelegateStep += Step;
                        m_delegateExeFinish += ExeFinish;
                        this.listBox_code.SelectedIndex = 3;
                        getCanseContent();
                        break;
                    }
                default:
                    break;
            }
            this.button_pause.IsEnabled = false;
            this.button_pause.Background = Brushes.DimGray;
            this.button_run.IsEnabled = true;
            this.button_run.Background = getImageSrc("/Images/toolbar_run.png");
            this.button_oneStep.IsEnabled = true;
            this.button_oneStep.Background = getImageSrc("/Images/toolbar_onestep.png");
            this.button_follow.IsEnabled = true;
            this.button_follow.Background = getImageSrc("/Images/toolbar_trace.png");
            this.button_runTo.IsEnabled = true;
            this.button_runTo.Background = getImageSrc("/Images/toolbar_runto.png");
            this.button_breakPoint.IsEnabled = true;
            this.button_breakPoint.Background = getImageSrc("/Images/toolbar_point.png");
        }

        private void ExeFinish(int flag)
        {
            switch(flag)
            {
                case 1:
                    MessageBox.Show("算法执行完毕", "顺序表插入", MessageBoxButton.OK);
                    initUI(1);
                    break;
            }
        }

        public Rectangle[] rec;
        public Label[] lab;
        public void getCanseContent()//获取动画显示画布中的所有组件
        {
            int i = 1;
            int rc_num = 0;
            int lable_num = 0;
            rec = new Rectangle[this.canse_demon.Children.Count / 2];
            lab = new Label[this.canse_demon.Children.Count / 2];
            IEnumerator ie = this.canse_demon.Children.GetEnumerator();
            while (ie.MoveNext())
            {
                if (i % 2 == 1)//单数为矩形
                    rec[rc_num++] = (Rectangle)ie.Current;
                else if (i % 2 == 0)//双数为lable
                    lab[lable_num++] = (Label)ie.Current;
                i++;
            }
        }
        private void Step(int currentRow, bool changeFlag, int movePosition, object changeValue)
        {
            //显示执行的当前行
            this.listBox_code.SelectedIndex = currentRow;
            //显示动画操作
            if (changeFlag)
            {
                int margin_left = 20;
                int margin_top = 90;

                Rectangle rc = new Rectangle();//矩形
                Label lable = new Label();//标签，放原始内容

                //初始化需要画的数组矩形和标签元素的相同属性
                rc.Stroke = Brushes.Yellow;
                rc.Fill = Brushes.Red;
                rc.Width = 50;
                rc.Height = 35;
          
                lable.Width = 25;
                lable.Height = 35;
                lable.FontSize = 15;
                lable.VerticalContentAlignment = VerticalAlignment.Center;
                double rc_margin_left;
                double lable_margin_left;
                //变量区源数据开始改变
                if (currentRow == 8)
                {
                    this.canse_demon.Children.Remove(rec[movePosition]);
                    //this.canse_demon.Children.Remove(lab[movePosition]);
                    rc_margin_left = (movePosition + 1) * rc.Width + margin_left;
                    rc.Margin = new Thickness(rc_margin_left, margin_top, 0, 0);

                    lable.Content = SqList.srcData[movePosition];
                    lable_margin_left = rc_margin_left + (rc.Width - lable.Width) / 2;
                    lable.Margin = new Thickness(lable_margin_left, margin_top, 0, 0);
                    this.canse_demon.Children.Add(rc);
                    this.canse_demon.Children.Add(lable);

                    Demonstration dempnstration = new Demonstration(flag, this);
                    this.listView_value.DataContext = dempnstration.GetDataTable(SqListCodes.INSERT_VALUE, 0, SqList.srcData[movePosition].ToString(), movePosition);
                }
                //变量区，P值改变
                if (currentRow == 9)
                {
                    Demonstration dempnstration = new Demonstration(flag, this);
                    this.listView_value.DataContext = dempnstration.GetDataTable(SqListCodes.INSERT_VALUE, 1, changeValue.ToString(),0);
                }
                if (currentRow == 11)
                {
                    //移除上方需要插入的元素后在插入
                    //index = Demonstration.index[Demonstration.index.Length - 1];
                    this.canse_demon.Children.Remove(rec[rec.Length-1]);
                    this.canse_demon.Children.Remove(lab[lab.Length-1]);
                    
                    rc_margin_left = (SqList.insPosition - 1) * rc.Width + margin_left;
                    rc.Margin = new Thickness(rc_margin_left, margin_top, 0, 0);

                    lable.Content = SqList.insertData;
                    lable_margin_left = rc_margin_left + (rc.Width - lable.Width) / 2;
                    lable.Margin = new Thickness(lable_margin_left, margin_top, 0, 0);
                    this.canse_demon.Children.Add(rc);
                    this.canse_demon.Children.Add(lable);

                    Demonstration dempnstration = new Demonstration(flag, this);
                    this.listView_value.DataContext = dempnstration.GetDataTable(SqListCodes.INSERT_VALUE, 0, SqList.insertData.ToString(), SqList.insPosition - 1);
                }
                //改变变量区长度
                if (currentRow == 12)
                {
                    Demonstration dempnstration = new Demonstration(flag, this);
                    this.listView_value.DataContext = dempnstration.GetDataTable(SqListCodes.INSERT_VALUE, 2, changeValue.ToString(),0);
                }
                DispatcherHelper.DoEvents();
                System.Threading.Thread.Sleep(500);
                if (currentRow != 11)
                    rc.Fill = Brushes.SkyBlue;

            }
        }

        //该窗体是否被改变
        public void changeUI(bool isMax)
        {
            if(isMax)
            {
                this.demon_border.Width += 200;
                Thickness margin_demo_drop = new Thickness(this.demo_drop.Margin.Left + 200, 0, 0, 0);
                this.demo_drop.Margin = margin_demo_drop;

                this.grid_code_value.ColumnDefinitions[0].Width = new GridLength(550d);
                this.grid_code_value.ColumnDefinitions[1].Width = new GridLength(401d);
                Thickness margin_code_drop = new Thickness(this.code_drop.Margin.Left + 100, 0, 0, 0);
                this.code_drop.Margin = margin_code_drop;
                Thickness margin_value_drop = new Thickness(this.value_drop.Margin.Left + 100, 0, 0, 0);
                this.value_drop.Margin = margin_value_drop;
            }
            else
            {

                this.demon_border.Width -= 200;
                Thickness margin_demo_drop = new Thickness(this.demo_drop.Margin.Left - 200, 0, 0, 0);
                this.demo_drop.Margin = margin_demo_drop;

                this.grid_code_value.ColumnDefinitions[0].Width = new GridLength(450d);
                this.grid_code_value.ColumnDefinitions[1].Width = new GridLength(300d);
                Thickness margin_code_drop = new Thickness(this.code_drop.Margin.Left - 100, 0, 0, 0);
                this.code_drop.Margin = margin_code_drop;
                Thickness margin_value_drop = new Thickness(this.value_drop.Margin.Left - 100, 0, 0, 0);
                this.value_drop.Margin = margin_value_drop;
            }
        }
        private void data_Click(object sender, RoutedEventArgs e)
        {
            if(m_thread == null)
            {
                string currentOperator = this.demon_lable_name.Content.ToString();
                switch (currentOperator)
                {
                    case "SqListInsert":
                        {
                            Demonstration.data.Clear();
                            this.canse_demon.Children.Clear();
                            this.listBox_code.Items.Clear();
                            this.listView_value.DataContext = null;
                            ListDialog insertWindow = new ListDialog();
                            //订阅事件
                            insertWindow.PassValuesEvent += new ListDialog.PassValuesHandler(RecieveOrderInsert);
                            insertWindow.ShowDialog();
                            break;
                        }
                    default:
                        {
                            ListDialog insertWindow = new ListDialog();
                            insertWindow.PassValuesEvent += new ListDialog.PassValuesHandler(RecieveOrderInsert);
                            insertWindow.ShowDialog();
                            break;
                        }

                }
            }
            else
                MessageBox.Show("算法演示正在执行，不能设置数据","提示",MessageBoxButton.OK);
        }
        private void RecieveOrderInsert(object sender, PassValuesEventArgs e)
        {
            this.srcData = e.srcData;
            this.insertData = e.insertData;
            this.position = e.insertPosition;
            initUI(1);
        }
        

        private void explain_Click(object sender, RoutedEventArgs e)
        {
            Demonstration dempnstration = new Demonstration(flag, this);
            dempnstration.ShowExplain();
        }

        private void pause_Click(object sender, RoutedEventArgs e)
        {
            if(m_thread.IsAlive)
            {
                if (this.lable_pause.Content.Equals("暂停"))
                {
                    this.button_run.IsEnabled = false;
                    this.button_run.Background = Brushes.DimGray;
                    this.lable_pause.Content = "继续";
                    if ((this.m_thread != null) && this.m_thread.IsAlive)
                        this.m_thread.Suspend();
                }
                else if (this.lable_pause.Content.Equals("继续"))
                {
                    this.button_run.IsEnabled = true;
                    this.button_run.Background = getImageSrc("/Images/toolbar_run.png");
                    this.lable_pause.Content = "暂停";
                    this.m_thread.Resume();
                }
            }
        }
    
        public Brush getImageSrc(string v)
        {
            ImageBrush im = new ImageBrush();
            Uri uri = new Uri(Directory.GetCurrentDirectory());//得到当前路径
            im.ImageSource = new BitmapImage(new Uri(uri + v, UriKind.RelativeOrAbsolute));
            im.Stretch = Stretch.None;
            return im;
        }

        private void run_Click(object sender, RoutedEventArgs e)
        {
            if (m_thread == null)
            {
                this.button_pause.IsEnabled = true;
                this.button_pause.Background = getImageSrc("/Images/toolbar_pause.png");
                this.button_oneStep.IsEnabled = false;
                this.button_oneStep.Background = Brushes.DimGray;
                this.button_follow.IsEnabled = false;
                this.button_follow.Background = Brushes.DimGray;
                this.button_runTo.IsEnabled = false;
                this.button_runTo.Background = Brushes.DimGray;
                this.button_breakPoint.IsEnabled = false;
                this.button_breakPoint.Background = Brushes.DimGray;

                startAlgorThread();
            }
            else
                MessageBox.Show("算法已经启动，不能重新启动！", "警告", MessageBoxButton.OK);
            //AlgorThread.allDone.Reset();
        }



        Thread m_thread = null;
        public void startAlgorThread()
        {
            m_thread = new Thread(new ThreadStart(this.THreadFun));//创建线程实例
            m_thread.Start();//启动线程，即调用ThreadFun线程函数
        }

        private void THreadFun()
        {
            AlgorThread algroThread = new AlgorThread(allDone, allDone, this);
            algroThread.Run(1);//调用AlgorThread的run函数，执行线程体  
        }
        private void oneStep_Click(object sender, RoutedEventArgs e)
        {

        }

        private void follow_Click(object sender, RoutedEventArgs e)
        {

        }

        private void runTo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void resume_Click(object sender, RoutedEventArgs e)
        {
            if(m_thread != null)
            {
                if(m_thread.IsAlive)
                {
                    try {
                        m_thread.Abort();
                        //m_thread.Join();//无限期阻塞该线程
                    }
                    catch
                    {

                    }
                }
                m_thread = null;
                initUI(flag);
            }

            this.button_run.IsEnabled = true;
            this.button_run.Background = getImageSrc("/Images/toolbar_run.png");
            this.lable_pause.Content = "暂停";
        }

        private void breakPoint_Click(object sender, RoutedEventArgs e)
        {
            
        }

        public static ManualResetEvent allDone = new ManualResetEvent(false);//当前线程的信号
        private void set_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Thread Start/Stop/Join Sample");
            ThreadClass ThreadC = new ThreadClass();
            Thread thread = new Thread(new ThreadStart(ThreadC.threadFun));
            thread.Start();

            //挂起当前线程
            allDone.WaitOne();

            Console.WriteLine("Main 1");

            //因为ThreadClass.threadFun方法里调用了Reset()
            //所以这里的WaitOne()方法会使主线程也挂起
            //allDone.WaitOne();

            //使主线程挂起1秒钟，
            //为了ThreadClass.threadFun方法里的Program.allDone.WaitOne()方法
            //运行时间在Main()方法的allDone.Set()方法前面
            Thread.Sleep(5000);

            //设置为有信号
            //如果没有这条语句，ThreadClass.threadFun方法里最后一条语句就不会运行
            allDone.Set();
            Console.WriteLine("Main 2");
        }
        class ThreadClass
        {
            public void threadFun()
            {
                Thread.Sleep(1000);
                Console.WriteLine("ThreadClass.threadFun 1");

                //激活被挂起的线程
                MainDemon.allDone.Set();

                Console.WriteLine("ThreadClass.threadFun 2");

                //设置为无信号，如果注释这条语句，
                //下面的WaitOne()方法就不起做用了
                MainDemon.allDone.Reset();

                //使当前线程挂起
                MainDemon.allDone.WaitOne();

                Console.WriteLine("ThreadClass.threadFun 3");

            }
        }
        private void initDemon_code_value(string srcData, string insertData, int pisition)
        {

        }

        //显示当前代码
        public void ShowCode()
        {
            Demonstration dempnstration = new Demonstration(flag, this);
            switch (flag)
            {
                case 1://顺序表的插入
                    {
                        dempnstration.ShowCode(SqListCodes.INSERT_CODE);
                        break;
                    }
                case 2://顺序表的删除
                    {
                        dempnstration.ShowCode(SqListCodes.DELETE_CODE);
                        break;
                    }
                case 3://链表的创建
                    {
                        break;
                    }
                case 4://链表的插入
                    {
                        break;
                    }
                case 5://链表的删除
                    {
                        break;
                    }
                case 6://直接查找
                    {
                        break;
                    }
                case 7://二分查找
                    {
                        break;
                    }

                default:
                    break;
            }
        }
        //显示当前动画
        public void ShowDemon()
        {
            Demonstration dempnstration = new Demonstration(flag, this);
            switch (flag)
            {
                case 1://顺序表的插入
                    {
                        dempnstration.ShowDemon();

                        break;
                    }
                case 2://顺序表的删除
                    {
                        break;
                    }
                case 3://链表的创建
                    {
                        break;
                    }
                case 4://链表的插入
                    {
                        break;
                    }
                case 5://链表的删除
                    {
                        break;
                    }
                case 6://直接查找
                    {
                        break;
                    }
                case 7://二分查找
                    {
                        break;
                    }
                default:
                    break;
            }
        }
        //显示当前变量
        public void ShowValue()
        {
            Demonstration dempnstration = new Demonstration(flag, this);
            switch (flag)
            {
                case 1://顺序表的插入
                    {
                        dempnstration.ShowValue(SqListCodes.INSERT_VALUE);
                        break;
                    }
                case 2://顺序表的删除
                    {
                        break;
                    }
                case 3://链表的创建
                    {
                        break;
                    }
                case 4://链表的插入
                    {
                        break;
                    }
                case 5://链表的删除
                    {
                        break;
                    }
                case 6://直接查找
                    {
                        break;
                    }
                case 7://二分查找
                    {
                        break;
                    }
                default:
                    break;
            }
        }
    }
    public static class DispatcherHelper
    {
        [SecurityPermissionAttribute(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        public static void DoEvents()
        {
            DispatcherFrame frame = new DispatcherFrame();
            Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background, new DispatcherOperationCallback(ExitFrames), frame);
            try { Dispatcher.PushFrame(frame); }
            catch (InvalidOperationException) { }
        }
        private static object ExitFrames(object frame)
        {
            ((DispatcherFrame)frame).Continue = false;
            return null;
        }
    }
}
