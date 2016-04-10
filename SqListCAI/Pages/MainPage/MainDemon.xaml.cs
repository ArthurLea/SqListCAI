using System.Threading;
using System.Windows;
using System.Windows.Controls;
using SqListCAI.Dialogs;
using SqListCAI.Events;
using SqListCAI.Entities;
using SqListCAI.Algorithm;
using SqListCAI.Utils.SourceCodes;
namespace SqListCAI.Pages.MainPage
{
    /// <summary>
    /// MianDemon.xaml 的交互逻辑
    /// </summary>
    public partial class MainDemon : Page
    {
        public SqList sqlist;
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
        public static ManualResetEvent allDone = new ManualResetEvent(false);//当前线程的信号
        public delegate void DelegateStep(int s);
        public DelegateStep m_DelegateStep;
        //顺序表插入
        public MainDemon(string demon_name,string srcData, char insertData,int position)
        {
            InitializeComponent();
            flag = 1;
            this.demon_lable_name.Content = demon_name;//初始化当前操作内容
            sqlist = new SqList(srcData, insertData, position);
            ShowDemon();
            ShowCode();
            ShowValue();
            m_DelegateStep += Step;
            startAlgorThread();
        }
        private void Step(int s) { this.listBox_code.SelectedIndex = s; }

        public void startAlgorThread()
        {
            Thread m_thread = new Thread(new ThreadStart(this.THreadFun));//创建线程实例
            m_thread.Start();//启动线程，即调用ThreadFun线程函数
        }

        private void THreadFun()
        {
            AlgorThread algroThread = new AlgorThread(allDone, allDone,this);
            algroThread.Run(1);//调用AlgorThread的run函数，执行线程体  
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
            string currentOperator = this.demon_lable_name.Content.ToString();
            switch (currentOperator)
            {
                case "SqListInsert":
                    {
                        ListDialog insertWindow = new ListDialog();
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

        private void RecieveOrderInsert(object sender, PassValuesEventArgs e)
        {
            this.demon_lable_name.Content = "SqListInsert";
        }
        

        private void explain_Click(object sender, RoutedEventArgs e)
        {
            Demonstration dempnstration = new Demonstration(flag, this);
            switch (flag)
            {
                case 1://顺序表的插入
                    {
                        MessageBox.Show(Explain.OrderInsExplain, "顺序表插入", MessageBoxButton.OK);
                        break;
                    }
                case 2://顺序表的删除
                    {
                        MessageBox.Show(Explain.OrderDelExplain, "顺序表删除", MessageBoxButton.OK);
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

        private void pause_Click(object sender, RoutedEventArgs e)
        {
            new ThreadWindow().ShowDialog();
        }

        private void run_Click(object sender, RoutedEventArgs e)
        {

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

        }

        private void breakPoint_Click(object sender, RoutedEventArgs e)
        {

        }

        private void set_Click(object sender, RoutedEventArgs e)
        {
            if(this.listBox_code.SelectedItem != null)
            {
                string currentdata = this.listBox_code.SelectedItem.ToString();
                int index = this.listBox_code.SelectedIndex;
                MessageBox.Show(index + "-" + currentdata, "当前选中的boxItem内容", MessageBoxButton.OK);
            }
        }
        private void initDemon_code_value(string srcData, string insertData, int pisition)
        {

        }
        //显示当前动画
        public void ShowDemon()
        {
            Demonstration dempnstration = new Demonstration(flag, this);
            switch (flag)
            {
                case 1://顺序表的插入
                    {
                        if(sqlist != null)
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
        //显示当前变量
        public void ShowValue()
        {
            Demonstration dempnstration = new Demonstration(flag, this);
            switch (flag)
            {
                case 1://顺序表的插入
                    {
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
}
