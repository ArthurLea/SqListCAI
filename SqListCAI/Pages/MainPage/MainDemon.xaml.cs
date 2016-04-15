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
using System.Text;

namespace SqListCAI.Pages.MainPage
{
    /// <summary>
    /// MianDemon.xaml 的交互逻辑
    /// </summary>
    public partial class MainDemon : Page
    {
        /// <summary>
        /// 当前线程的信号
        /// </summary>
        public static ManualResetEvent allDone = new ManualResetEvent(false);//当前线程的信号
        /// <summary>
        /// 说明当前操作的算法的标识
        /// </summary>
        int flag = 0;//说明当前操作的算法
        public MainDemon()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 公用的主窗口初始化构造
        /// </summary>
        /// <param name="demon_name"></param>
        public MainDemon(string demon_name)
        {
            InitializeComponent();
            this.demon_lable_name.Content = demon_name;
        }
        public delegate void DelegateStep(int currentRow,bool changeFlag, int movePosition,object changeValue);
        public DelegateStep m_DelegateStep;

        public delegate void DelegateExeFinish(int flag);
        public DelegateExeFinish m_delegateExeFinish;
        //顺序表插入
        public string srcData_ins;
        public char insertData_ins;
        public int position_ins;
        //顺序表删除
        public string srcData_del;
        public int position_del;
        /// <summary>
        /// 线性表插入的主窗口初始化
        /// </summary>
        /// <param name="demon_name"></param>
        /// <param name="srcData"></param>
        /// <param name="insertData"></param>
        /// <param name="position"></param>
        public MainDemon(string demon_name,string srcData, char insertData,int position)//线性表插入
        {
            InitializeComponent();
            this.srcData_ins = srcData;
            this.insertData_ins = insertData;
            this.position_ins = position;
            this.demon_lable_name.Content = demon_name;//初始化当前操作内容
            flag = 1;
            initUI(flag);
        }
        /// <summary>
        /// 线性表删除的主窗口初始化
        /// </summary>
        /// <param name="demon_name"></param>
        /// <param name="srcData"></param>
        /// <param name="position"></param>
        public MainDemon(string demon_name, string srcData, int position) : this(demon_name)//线性表删除
        {
            InitializeComponent();
            this.srcData_del = srcData;
            this.position_del = position;
            this.demon_lable_name.Content = demon_name;
            flag = 2;
            initUI(flag);
        }
        /// <summary>
        /// UI的初始化
        /// </summary>
        /// <param name="flag"></param>
        public void initUI(int flag)
        {
            this.canse_demon.Children.Clear();
            this.listBox_currentRow.Items.Clear();
            this.listBox_code.Items.Clear();
            this.listView_value.DataContext = null;
            switch (flag)
            {
                case 1://线性表插入
                    {
                        Demonstration.data_ins.Clear();
                        SqList.init_SqList(srcData_ins, insertData_ins, position_ins);
                        ShowDemon();
                        ShowCode();
                        ShowValue();
                        m_DelegateStep = Step_orderIns;
                        m_delegateExeFinish = ExeFinish;
                        getCanseContent();
                        break;
                    }
                case 2:
                    {
                        Demonstration.data_del.Clear();
                        SqList.init_SqList(srcData_del,position_del);
                        ShowDemon();
                        ShowCode();
                        ShowValue();
                        m_DelegateStep = Step_orderDel;
                        m_delegateExeFinish = ExeFinish;
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
            this.button_runTo.IsEnabled = true;
            this.button_runTo.Background = getImageSrc("/Images/toolbar_runto.png");
            this.button_breakPoint.IsEnabled = true;
            this.button_breakPoint.Background = getImageSrc("/Images/toolbar_point.png");
            this.button_clearAllPoint.IsEnabled = true;
            this.button_clearAllPoint.Background = getImageSrc("/Images/toolbar_point.png");
            this.button_currentPoint.IsEnabled = true;
            this.button_currentPoint.Background = getImageSrc("/Images/toolbar_point.png");
            this.button_resume.IsEnabled = true;
            this.button_resume.Background = getImageSrc("/Images/toolbar_resume.png");

            first_enter_oneStep_flag = true;//重置单步操作标志
            fisrst_enter_runTo_click_flag = true;//重置断点执行标志


            order_del_return_value_flag = false;//重置顺序表删除值返回标志
        }
        public bool order_del_return_value_flag = false;
        /// <summary>
        /// 线程顺序表删除算法执行过程中主线程同步的委托方法
        /// </summary>
        /// <param name="currentRow"></param>
        /// <param name="moveFlag"></param>
        /// <param name="movePosition"></param>
        /// <param name="changeValue"></param>
        private void Step_orderDel(int currentRow, bool changeFlag, int movePosition, object changeValue)
        {
            //显示执行的当前行
            this.listBox_code.SelectedIndex = currentRow;
            //显示动画操作
            if(changeFlag)
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
                if(currentRow == 7)//返回的删除值e,开始左移
                {
                    if (!order_del_return_value_flag)//表示删除的数据没有修改，第一次进来就添加返回值，因为删除的值先于左移执行
                    {
                        //将返回的数据显示在canse上的
                        rc = rec[rec.Length - 1];
                        lable = lab[lab.Length - 1];
                        lable.Content = changeValue;
                        this.canse_demon.Children.Remove(rec[rec.Length - 1]);
                        this.canse_demon.Children.Remove(lab[lab.Length - 1]);
                        this.canse_demon.Children.Add(rc);
                        this.canse_demon.Children.Add(lable);
                        order_del_return_value_flag = true;//表示删除的数据已经返回

                        Demonstration dempnstration = new Demonstration(flag, this);
                        this.listView_value.DataContext = dempnstration.GetDataTable_Del(SqListCodes.DELETE_VALUE, 0, changeValue.ToString(), 0);
                    }
                    else
                    {
                        this.canse_demon.Children.Remove(rec[movePosition-1]);
                        this.canse_demon.Children.Remove(lab[movePosition - 1]);
                        rc_margin_left = (movePosition - 1) * rc.Width + margin_left;
                        rc.Margin = new Thickness(rc_margin_left, margin_top, 0, 0);

                        lable.Content = SqList.srcData_del[movePosition];
                        lable_margin_left = rc_margin_left + (rc.Width - lable.Width) / 2;
                        lable.Margin = new Thickness(lable_margin_left, margin_top, 0, 0);
                        this.canse_demon.Children.Add(rc);
                        this.canse_demon.Children.Add(lable);

                        Demonstration dempnstration = new Demonstration(flag, this);
                        this.listView_value.DataContext = dempnstration.GetDataTable_Del(SqListCodes.DELETE_VALUE, 1, SqList.srcData_del[movePosition].ToString(), movePosition);
                    }
                }
                if (currentRow == 8)//改变p值
                {
                    Demonstration dempnstration = new Demonstration(flag, this);
                    this.listView_value.DataContext = dempnstration.GetDataTable_Del(SqListCodes.DELETE_VALUE, 2, changeValue.ToString(), 0);
                }
                if (currentRow == 10)//改变length长度
                {
                    Demonstration dempnstration = new Demonstration(flag, this);
                    this.listView_value.DataContext = dempnstration.GetDataTable_Del(SqListCodes.DELETE_VALUE, 3, changeValue.ToString(), 0);

                    this.canse_demon.Children.Remove(rec[rec.Length - 2]);
                    this.canse_demon.Children.Remove(lab[lab.Length - 2]);
                }
                DispatcherHelper.DoEvents();
                System.Threading.Thread.Sleep(300);
                if (order_del_return_value_flag)
                    rc.Fill = Brushes.SkyBlue;
            }
        }
        /// <summary>
        /// 线程顺序表插入算法执行过程中主线程同步的委托方法
        /// </summary>
        /// <param name="currentRow"></param>
        /// <param name="changeFlag"></param>
        /// <param name="movePosition"></param>
        /// <param name="changeValue"></param>
        private void Step_orderIns(int currentRow, bool changeFlag, int movePosition, object changeValue)
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

                    lable.Content = SqList.srcData_ins[movePosition];
                    lable_margin_left = rc_margin_left + (rc.Width - lable.Width) / 2;
                    lable.Margin = new Thickness(lable_margin_left, margin_top, 0, 0);
                    this.canse_demon.Children.Add(rc);
                    this.canse_demon.Children.Add(lable);

                    Demonstration dempnstration = new Demonstration(flag, this);
                    this.listView_value.DataContext = dempnstration.GetDataTable_Ins(SqListCodes.INSERT_VALUE, 0, SqList.srcData_ins[movePosition].ToString(), movePosition);
                }
                //变量区，P值改变
                if (currentRow == 9)
                {
                    Demonstration dempnstration = new Demonstration(flag, this);
                    this.listView_value.DataContext = dempnstration.GetDataTable_Ins(SqListCodes.INSERT_VALUE, 1, changeValue.ToString(),0);
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
                    this.listView_value.DataContext = dempnstration.GetDataTable_Ins(SqListCodes.INSERT_VALUE, 0, SqList.insertData.ToString(), SqList.insPosition - 1);
                }
                //改变变量区中变量长度
                if (currentRow == 12)
                {
                    Demonstration dempnstration = new Demonstration(flag, this);
                    this.listView_value.DataContext = dempnstration.GetDataTable_Ins(SqListCodes.INSERT_VALUE, 2, changeValue.ToString(),0);
                }
                DispatcherHelper.DoEvents();
                System.Threading.Thread.Sleep(300);
                if (currentRow != 11)
                    rc.Fill = Brushes.SkyBlue;
            }
        }

        /// <summary>
        /// 线程算法执行完毕后主线程弹窗提示
        /// </summary>
        /// <param name="flag"></param>
        private void ExeFinish(int flag)
        {
            switch (flag)
            {
                case 1:
                    MessageBox.Show("顺序表插入算法执行完毕", "提示", MessageBoxButton.OK);
                    break;
                case 2:
                    MessageBox.Show("顺序表删除算法执行完毕", "提示", MessageBoxButton.OK);
                    break;
            }

            this.button_pause.IsEnabled = false;
            this.button_pause.Background = Brushes.DimGray;
            this.button_resume.IsEnabled = true;
            this.button_resume.Background = getImageSrc("/Images/toolbar_resume.png");
        }

        public Rectangle[] rec = null;
        public Label[] lab = null;
        /// <summary>
        /// 获取动画显示画布中的所有组件
        /// </summary>
        public void getCanseContent()//获取动画显示画布中的所有组件
        {
            int i = 1;
            int rc_num = 0;
            int lable_num = 0;
            if (rec != null) rec = null;
            if (lab != null) lab = null;
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
            if((m_thread==null) || !m_thread.IsAlive)//当未初始化线程或者线程不在执行状态时
            {
                Demonstration demonstration = new Demonstration(flag, this);
                demonstration.SetData();
            }
            else
                MessageBox.Show("算法演示正在执行，不能设置数据","提示",MessageBoxButton.OK);
        }
        
        private void explain_Click(object sender, RoutedEventArgs e)
        {
            Demonstration demonstration = new Demonstration(flag, this);
            demonstration.ShowExplain();
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
                    m_thread.Suspend();
                }
                else if (this.lable_pause.Content.Equals("继续"))
                {
                    this.lable_pause.Content = "暂停";
                    m_thread.Resume();
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
        /// <summary>
        /// 全速执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void run_Click(object sender, RoutedEventArgs e)
        {
            this.button_pause.IsEnabled = true;
            this.button_pause.Background = getImageSrc("/Images/toolbar_pause.png");
            this.button_run.IsEnabled = false;
            this.button_run.Background = Brushes.DimGray;
            this.button_oneStep.IsEnabled = false;
            this.button_oneStep.Background = Brushes.DimGray;
            this.button_runTo.IsEnabled = false;
            this.button_runTo.Background = Brushes.DimGray;
            this.button_breakPoint.IsEnabled = false;
            this.button_breakPoint.Background = Brushes.DimGray;
            this.button_clearAllPoint.IsEnabled = false;
            this.button_clearAllPoint.Background = Brushes.DimGray;
            this.button_currentPoint.IsEnabled = false;
            this.button_currentPoint.Background = Brushes.DimGray;

            this.button_resume.IsEnabled = false;
            this.button_resume.Background = Brushes.DimGray;

            m_thread = new Thread(new ThreadStart(this.THreadFun_Run_Click));//创建线程实例
            m_thread.Start();//启动线程，即调用ThreadFun线程函数

        }
        public Thread m_thread = null;

        private void THreadFun_Run_Click()
        {
            AlgorThread algroThread = new AlgorThread(allDone, allDone, this);
            algroThread.Run(flag,1);//调用AlgorThread的run函数，执行线程体，实现全速执行
        }
        public bool first_enter_oneStep_flag = true;
        /// <summary>
        /// 单步执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void oneStep_Click(object sender, RoutedEventArgs e)
        {
            if (first_enter_oneStep_flag)
            {
                this.button_pause.IsEnabled = true;
                this.button_pause.Background = getImageSrc("/Images/toolbar_pause.png");
                this.button_run.IsEnabled = false;
                this.button_run.Background = Brushes.DimGray;
                this.button_oneStep.IsEnabled = true;
                this.button_oneStep.Background = getImageSrc("/Images/toolbar_onestep.png");
                this.button_runTo.IsEnabled = false;
                this.button_runTo.Background = Brushes.DimGray;
                this.button_breakPoint.IsEnabled = false;
                this.button_breakPoint.Background = Brushes.DimGray;
                this.button_clearAllPoint.IsEnabled = false;
                this.button_clearAllPoint.Background = Brushes.DimGray;
                this.button_currentPoint.IsEnabled = false;
                this.button_currentPoint.Background = Brushes.DimGray;


                this.button_resume.IsEnabled = false;
                this.button_resume.Background = Brushes.DimGray;

                m_thread = new Thread(new ThreadStart(this.THreadFun_OneStep_Click));//创建线程实例
                m_thread.Start();//启动线程，即调用ThreadFun线程函数

                first_enter_oneStep_flag = false;
            }
            else
                allDone.Set();
        }
        private void THreadFun_OneStep_Click()
        {
            AlgorThread algroThread = new AlgorThread(allDone, allDone, this);
            algroThread.Run(flag, 2);//调用AlgorThread的run函数，执行线程体，实现单步执行
        }
        public bool fisrst_enter_runTo_click_flag = true;
        /// <summary>
        /// 断点执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void runTo_Click(object sender, RoutedEventArgs e)
        {
            if ((wherePoint!=null) && (wherePoint.Length!=0))
            {
                if (fisrst_enter_runTo_click_flag)
                {
                    this.button_pause.IsEnabled = true;
                    this.button_pause.Background = getImageSrc("/Images/toolbar_pause.png");
                    this.button_run.IsEnabled = false;
                    this.button_run.Background = Brushes.DimGray;
                    this.button_oneStep.IsEnabled = true;
                    this.button_oneStep.Background = Brushes.DimGray;
                    this.button_runTo.IsEnabled = true;
                    this.button_runTo.Background = getImageSrc("/Images/toolbar_runto.png");
                    this.button_resume.IsEnabled = false;
                    this.button_resume.Background = Brushes.DimGray;

                    m_thread = new Thread(new ThreadStart(this.THreadFun_RunTo_Click));//创建线程实例
                    m_thread.Start();//启动线程，即调用ThreadFun线程函数，实现断点执行到

                    fisrst_enter_runTo_click_flag = false;
                }
                else
                    allDone.Set();
            }
            else
                MessageBox.Show("请先设置断点", "提示", MessageBoxButton.OK);
        }
        private void THreadFun_RunTo_Click()
        {
            AlgorThread algroThread = new AlgorThread(allDone, allDone, this);
            algroThread.Run(flag, 3);//调用AlgorThread的run函数，执行线程体  
        }
        /// <summary>
        /// 恢复最开始的状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void resume_Click(object sender, RoutedEventArgs e)
        {
            //if (m_thread != null)
            //{
            //    try
            //    {
            //        m_thread.Abort();
            //        //m_thread.Join();//无限期阻塞该线程
            //        Thread.Sleep(100);
            //        Console.WriteLine(m_thread.IsAlive);
            //        Console.WriteLine(m_thread.ThreadState);
            //    }
            //    catch
            //    {

            //    }
            //}

            initUI(flag);
            this.button_run.IsEnabled = true;
            this.button_run.Background = getImageSrc("/Images/toolbar_run.png");
            this.lable_pause.Content = "暂停";
            clearAllPoint();
        }
        public int[] wherePoint;
        /// <summary>
        /// 设置断点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void breakPoint_Click(object sender, RoutedEventArgs e)
        {
            //得到算法区选中的代码行数
            int count = this.listBox_code.SelectedItems.Count;
            this.wherePoint = new int[count];
            //一次存储选中行在代码区中的行数
            IList lists = this.listBox_code.SelectedItems;
            for (int i = 0; i < count; i++)
                this.wherePoint[i] = this.listBox_code.Items.IndexOf(lists[i]);

            this.listBox_currentRow.SelectedIndex = -1;//先清除所有断点在设置
            for (int i = 0; i < wherePoint.Length; i++)//显示用户设置的当前断点到this.listBox_currentRow
                this.listBox_currentRow.SelectedItems.Add(wherePoint[i]);
            MessageBox.Show("已设置断点 " + count + " 处\n可以电机断点执行按钮", "提示", MessageBoxButton.OK);
        }

        /// <summary>
        /// 清除所有断点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clearAllPoint_Click(object sender, RoutedEventArgs e)
        {
            //testThreadAsyn();
            clearAllPoint();
        }
        private void clearAllPoint()
        {
            this.listBox_code.SelectedIndex = -1;
            this.listBox_currentRow.SelectedIndex = -1;
            if (wherePoint == null)
                MessageBox.Show("未设置任何断点", "提示", MessageBoxButton.OK);
            else
            {
                wherePoint = null;
                MessageBox.Show("断点清除成功", "提示", MessageBoxButton.OK);
            }
        }
        /// <summary>
        /// 显示当前断点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void currentPoint_Click(object sender, RoutedEventArgs e)
        {
            if (wherePoint != null)
            {
                Array.Sort<int>(wherePoint);
                string[] str = new string[wherePoint.Length];
                StringBuilder sb = new StringBuilder();
                for (int i=0;i<wherePoint.Length;i++)
                {
                    str[i] = "代码区第 " + wherePoint[i] + " 行\n";
                    sb.Append(str[i]);
                }
                MessageBox.Show(sb.ToString(), "提示", MessageBoxButton.OK);
            }
            else
                MessageBox.Show("您当前未设置任何断点", "提示", MessageBoxButton.OK);
        }
        /// <summary>
        /// 线程测试函数
        /// </summary>
        private void testThreadAsyn()
        {
            Console.WriteLine("Thread Start/Stop/Join Sample");
            ThreadClass ThreadC = new ThreadClass();
            Thread thread = new Thread(new ThreadStart(ThreadC.threadFun));
            thread.Start();

            //挂起当前线程
            allDone.WaitOne();

            Console.WriteLine("Main 1");

            //因为ThreadClass.threadFun方法里调用了Reset()，所以这里的WaitOne()方法会使主线程也挂起
            //allDone.WaitOne();

            //使主线程挂起1秒钟，为了ThreadClass.threadFun方法里的Program.allDone.WaitOne()方法，运行时间在Main()方法的allDone.Set()方法前面
            Thread.Sleep(5000);

            //设置为有信号，如果没有这条语句，ThreadClass.threadFun方法里最后一条语句就不会运行
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

                //设置为无信号，如果注释这条语句，下面的WaitOne()方法就不起做用了
                MainDemon.allDone.Reset();

                //使当前线程挂起
                MainDemon.allDone.WaitOne();

                Console.WriteLine("ThreadClass.threadFun 3");

            }
        }
        /// <summary>
        /// 显示当前代码
        /// </summary>
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
        /// <summary>
        /// 显示当前动画
        /// </summary>
        //显示当前动画
        public void ShowDemon()
        {
            Demonstration dempnstration = new Demonstration(flag, this);
            dempnstration.ShowDemon();
        }
        //显示当前变量
        public void ShowValue()
        {
            Demonstration dempnstration = new Demonstration(flag, this);
            dempnstration.ShowValue();
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
