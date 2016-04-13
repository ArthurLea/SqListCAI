using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading.Tasks;
//引入线程命名空间
using System.Windows.Threading;

namespace SqListCAI.Algorithm
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ThreadWindow : Window
    {
        public ThreadWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;//窗口居中
        }
        //方式一
        DispatcherTimer tm = new DispatcherTimer();//实例化一个DispatcherTimer对象
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            tm.Tick += new EventHandler(tm_Tick);//订阅Tick事件
            tm.Interval = TimeSpan.FromSeconds(0.05);
            tm.Start();
            // tm.Stop();
        }

        void tm_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value<=100)
            {
                progressBar1.Value++;
                this.label2.Content = progressBar1.Value++ +"%";
            }
            else
            {
                tm.Stop();
            }
          
        }

        //方式二
        public void newActionThread(int value)
        {
            this.progressBar1.Value = value;
            this.label2.Content = progressBar1.Value++ + "%";
            System.Threading.Thread.Sleep(100);
        }
      
        // 使用线程 方法
        public void DispatcherThread()
        {
            Dispatcher newDispatcher = Dispatcher.CurrentDispatcher;//提供线程工作环境
            Action<int> newAction = new Action<int>(this.newActionThread);

            for (int i = 0; i < 100; i++)
            {
                newDispatcher.Invoke(newAction, i);
                System.Threading.Thread.Sleep(100);
                this.DoEvents();
                //   newDispatcher.Thread.Abort();
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            DispatcherThread();
        }

        //方式三

        public void newActionThread2(object value)
        {
            Action<int> newAction = new Action<int>(this.newActionThread);
            this.progressBar1.Dispatcher.Invoke(newAction, (int)value);//同步执行指定的委托
            System.Threading.Thread.Sleep(100);
        }
        public void Thread()
        {
            //winform中的线程
            //  System.Threading.Thread;
            //wpf环境中的线程
            //System.Windows.Threading.Dispatcher.CurrentDispatcher.Thread
            System.Threading.ParameterizedThreadStart ts = new System.Threading.ParameterizedThreadStart(this.newActionThread2);//一个线程执行委托


            for (int i = 0; i <= 100; i++)
            {
                System.Threading.Thread t = new System.Threading.Thread(ts);
                System.Threading.Thread.Sleep(100);
                t.Start(i);
                this.label2.Content = progressBar1.Value++ + "%";
                this.DoEvents();//界面刷新
            }
        }
        private void button3_Click(object sender, RoutedEventArgs e)
        {
            Thread();
        }
        //方式四BackgroundWorker
        //http://msdn.microsoft.com/zh-cn/library/vstudio/system.componentmodel.backgroundworker.aspx更多信息
        System.ComponentModel.BackgroundWorker bw;
        private void button4_Click(object sender, RoutedEventArgs e)
        {
            bw = new System.ComponentModel.BackgroundWorker();//创建BackgroundWorker对象实例
            bw.DoWork += new System.ComponentModel.DoWorkEventHandler(bw_DoWork);//订阅DoWork事件
            bw.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(bw_ProgressChanged);//订阅报告进程事件
            bw.WorkerReportsProgress = true;
            bw.RunWorkerAsync();//开始执行后台操作
          
        }

        void bw_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            this.progressBar1.Value = e.ProgressPercentage;//获取进度百分比
            this.label2.Content = (e.ProgressPercentage.ToString() + "%");
        }


        void bw_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            for (int i = 0; i <= 100; i++)
            {
                this.bw.ReportProgress(i);
                System.Threading.Thread.Sleep(100);
            }
        }
        //方式5我们可以利用线程并行来处理
    }
}
