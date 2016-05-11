using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SqListCAI.Pages.BaseContent;
using SqListCAI.Pages.MainPage;
using SqListCAI.Dialogs;
using SqListCAI.Events;
using SqListCAI.Pages.Example;

namespace SqListCAI
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        //最小化到任务栏
        System.Windows.Forms.NotifyIcon notifyIcon;
        private SetDialog setDialog = null;
        private MainDemon maindemon = null;//关联动画窗口
        private SqListBassContent baseStaticPage = null;
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;//窗口居中

            // 应用下拉日历样式
            TextBox tb = new TextBox();
            Style sCalendar = (Style)tb.TryFindResource("tbCalendarStyle");
            if (sCalendar != null)
                this.datePick.Style = sCalendar;
            //应用动画
            System.Windows.Media.Animation.Storyboard s = (System.Windows.Media.Animation.Storyboard)TryFindResource("sb");
            s.Begin();	// Start animation

            this.border_currentOperator.Height = 0;//初始化不显示当前操作显示文本框

            //托盘应用
            trayIcon();
        }
        /// <summary>
        /// 实现应用程序的托盘应用
        /// </summary>
        private void trayIcon()
        {
            this.notifyIcon = new System.Windows.Forms.NotifyIcon();
            this.notifyIcon.Text = "线性表演示系统";//最小化到托盘时，鼠标移动到该图标上方时显示的文本
            System.Windows.Forms.MenuItem open = new System.Windows.Forms.MenuItem("打开");//打开菜单项
            open.Click += new EventHandler(Show);
            System.Windows.Forms.MenuItem hide = new System.Windows.Forms.MenuItem("隐藏");//隐藏菜单项
            hide.Click += new EventHandler(Hide);
            System.Windows.Forms.MenuItem exit = new System.Windows.Forms.MenuItem("退出");//退出菜单项
            exit.Click += new EventHandler(Close);
            //关联托盘控件
            System.Windows.Forms.MenuItem[] childen = new System.Windows.Forms.MenuItem[] { open, hide,exit };
            this.notifyIcon.ContextMenu = new System.Windows.Forms.ContextMenu(childen);
            //设置托盘图标,读取程序图标，来作为托盘图标
            this.notifyIcon.Icon = System.Drawing.Icon.ExtractAssociatedIcon(System.Windows.Forms.Application.ExecutablePath);//new System.Drawing.Icon(@"Images/app8.ico");
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler((sender, e) =>
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                    this.Show(sender, e);
            });
            //设置程序启动时显示的文本
            this.notifyIcon.ShowBalloonTip(5000, "线性表的动态演示系统", "开始启动", System.Windows.Forms.ToolTipIcon.Info);
            System.Threading.Thread.Sleep(2000); //Wait 2 second
            this.notifyIcon.Visible = false; //这样可以控制2秒后其乖乖地消失在人间
            this.notifyIcon.Visible = true; //只是会闪一下
        }
        /// <summary>
        /// 显示隐藏的应用程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Show(object sender, EventArgs e)
        {
            this.Visibility = Visibility.Visible;
            this.ShowInTaskbar = true;
            this.Activate();
        }
        /// <summary>
        /// 隐藏应用程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Hide(object sender, EventArgs e)
        {
            this.ShowInTaskbar = false;
            this.Visibility = Visibility.Hidden;
        }
        /// <summary>
        /// 关闭应用程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Close(object sender, EventArgs e)
        {
            //Application.Current.Shutdown();
            Environment.Exit(0);
        }
        /// <summary>
        /// 鼠标左键按下后未放开可以开始拖动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.DragMove();
                e.Handled = true;//阻止消息默认路由
            }
            catch(Exception ee)
            {
                Console.WriteLine(ee.ToString());
                return;
            }
        }
        /// <summary>
        /// 隐藏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MouseLeftButtonDown_Hide(object sender,MouseButtonEventArgs e)
        {
            //this.WindowState = WindowState.Minimized;
            Hide(sender,e);
        }
        /// <summary>
        /// 应用程序的设置功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MouseLeftButtonDown_Setting(object sender,MouseButtonEventArgs e)
        {
            if(setDialog == null)
                setDialog = new SetDialog();
            if (setDialog.IsActive)
                setDialog.Visibility = Visibility.Visible;
            else
                setDialog.ShowDialog();
        }
        /// <summary>
        /// 关闭应用程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MouseLeftButtonDown_Logout(object sender, MouseButtonEventArgs e)
        {
            //this.Close(); //只是关闭当前窗口，若不是主窗体的话，是无法退出程序的，另外若有托管线程（非主线程），也无法干净地退出；
            //关闭当前窗口，可以在OnClosing和 OnClosed中捕获消息，在OnClosing的时候，可以取消关闭窗口。
            //Application.Current.Shutdown();// 关闭当前程序，如果有其他线程没有结束，不会关闭
            Environment.Exit(0); //这是最彻底的退出方式，不管什么线程都被强制退出，把程序结束的很干净。
        }

        public static bool __isLeftHide = false;
        /// <summary>
        /// 悬浮框的点击，主窗口左边四个操作区域是否隐藏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void spliter_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //Left Bar hide and show
            __isLeftHide = !__isLeftHide;
            if (maindemon == null)
            {
                maindemon = new MainDemon();
            }
            if (__isLeftHide)//隐藏
            {
                this.gridForm.ColumnDefinitions[0].Width = new GridLength(0d);
                maindemon.changeUI(true);
            }
            else//未隐藏
            {
                this.gridForm.ColumnDefinitions[0].Width = new GridLength(200d);
                maindemon.changeUI(false);
            }
        }
        /// <summary>
        /// 静态内容展示的公用接口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="flag"></param>
        private void getStaticContent(object sender,int flag)
        {
            getCurrentOperator(sender);
            baseStaticPage= new SqListBassContent(flag);
            this.pageContainer.Navigate(baseStaticPage, "");
            this.baseStaticPage.gridBaseStatic.Visibility = Visibility.Visible;
        }
        /// <summary>
        /// 线性表的基本定义
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Label_MouseLeftButtonDownSQListDefine(object sender, MouseButtonEventArgs e)
        {
            //this.pageContainer.Source = new Uri("Pages/BaseContent/SqListDefine.xaml", UriKind.RelativeOrAbsolute);
            getStaticContent(sender,1);
        }
        /// <summary>
        /// 线性表的特征
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Label_MouseLeftButtonDownSQListFeature(object sender, MouseButtonEventArgs e)
        {
            getStaticContent(sender, 2);
        }
        /// <summary>
        /// 线性表的结构
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Label_MouseLeftButtonDownSQListStructure(object sender, MouseButtonEventArgs e)
        {
            getStaticContent(sender, 3);
        }
        /// <summary>
        /// 线性表的操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Label_MouseLeftButtonDownSQListOperator(object sender, MouseButtonEventArgs e)
        {
            getStaticContent(sender, 4);
        }
        /// <summary>
        /// 线性表的存储结构说明
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Label_MouseLeftButtonDownStorageExplain(object sender, MouseButtonEventArgs e)
        {
            getStaticContent(sender, 5);
        }
        /// <summary>
        /// 得到当前处理的操作
        /// </summary>
        /// <param name="sender"></param>
        private void getCurrentOperator(object sender)
        {
            this.border_currentOperator.Height = 35;
            String[] content = sender.ToString().Split(new char[2] { '.', ':' });
            this.currentOperator.Content = content[content.Length - 1];
        }
        /// <summary>
        /// 线性表的顺序插入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Label_MouseLeftButtonDownOrderInsert(object sender, MouseButtonEventArgs e)
        {
            //pageContainer.Source = new Uri("/Pages/MainDemon.xaml", UriKind.RelativeOrAbsolute);
            getCurrentOperator(sender);
            maindemon = new MainDemon();
            maindemon.Demon_Code_Value.Height = 0;
            this.pageContainer.Navigate(maindemon, "SqListInsert");

            ListDialog insertWindow = new ListDialog(1);
            //订阅事件
            insertWindow.PassValuesEvent += new ListDialog.PassValuesHandler(ReceiveValuesOfOrderInsert);
            insertWindow.ShowDialog();
        }
        private void ReceiveValuesOfOrderInsert(object sender, PassValuesEventArgs e)
        {
            maindemon = new MainDemon("SqListInsert", e.srcData, e.insertData, e.position);
            this.pageContainer.Navigate(maindemon, "SqListInsert");
        }
        /// <summary>
        /// 线性表的顺序删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Label_MouseLeftButtonDownOrderDelete(object sender, MouseButtonEventArgs e)
        {
            getCurrentOperator(sender);
            maindemon = new MainDemon();
            maindemon.Demon_Code_Value.Height = 0;
            this.pageContainer.Navigate(maindemon, "SqListDelete");

            ListDialog deleteWindow = new ListDialog(2);
            //订阅事件
            deleteWindow.PassValuesEvent += new ListDialog.PassValuesHandler(ReceiveValuesOfOrderDelete);
            deleteWindow.ShowDialog();
        }
        private void ReceiveValuesOfOrderDelete(object sender, PassValuesEventArgs e)
        {
            maindemon = new MainDemon("SqListDelete", e.srcData, e.position);
            this.pageContainer.Navigate(maindemon, "SqListDelete");
        }
        /// <summary>
        /// 线性表的链表创建
        /// </summary>
        private void Label_MouseLeftButtonDownLinkedCreate(object sender, MouseButtonEventArgs e)
        {
            getCurrentOperator(sender);
            maindemon = new MainDemon();
            maindemon.Demon_Code_Value.Height = 0;
            this.pageContainer.Navigate(maindemon, "LinkedListedCreate");

            ListDialog linkedCreWindow = new ListDialog(3);
            //订阅事件
            linkedCreWindow.PassValuesEvent += new ListDialog.PassValuesHandler(ReceiveValuesOfLinkedListCreate);
            linkedCreWindow.ShowDialog();
        }
        private void ReceiveValuesOfLinkedListCreate(object sender, PassValuesEventArgs e)
        {
            maindemon = new MainDemon("LinkedListedCreate", e.srcData);
            this.pageContainer.Navigate(maindemon, "LinkedListedCreate");
        }
        /// <summary>
        /// 线性表的链表插入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Label_MouseLeftButtonDownLinkedInsert(object sender, MouseButtonEventArgs e)
        {
            getCurrentOperator(sender);
            maindemon = new MainDemon();
            maindemon.Demon_Code_Value.Height = 0;
            this.pageContainer.Navigate(maindemon, "LinkedListedInsert");

            ListDialog linkedInsWindow = new ListDialog(4);
            //订阅事件
            linkedInsWindow.PassValuesEvent += new ListDialog.PassValuesHandler(ReceiveValuesOfLinkedListInsert);
            linkedInsWindow.ShowDialog();
        }
        private void ReceiveValuesOfLinkedListInsert(object sender, PassValuesEventArgs e)
        {
            maindemon = new MainDemon("LinkedListedInsert", e.srcData,e.insertData,e.position,4);//这个构造方法跟顺序表插入的一样，但是为了区别，在多传一个无用参数
            this.pageContainer.Navigate(maindemon, "LinkedListedInsert");
        }
        /// <summary>
        /// 线性表的链表删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Label_MouseLeftButtonDownLinkedDelete(object sender, MouseButtonEventArgs e)
        {
            getCurrentOperator(sender);
            maindemon = new MainDemon();
            maindemon.Demon_Code_Value.Height = 0;
            this.pageContainer.Navigate(maindemon, "LinkedListedDelete");

            ListDialog linkedDelWindow = new ListDialog(5);
            //订阅事件
            linkedDelWindow.PassValuesEvent += new ListDialog.PassValuesHandler(ReceiveValuesOfLinkedListDelete);
            linkedDelWindow.ShowDialog();
        }

        private void ReceiveValuesOfLinkedListDelete(object sender, PassValuesEventArgs e)
        {
            maindemon = new MainDemon("LinkedListedDelete", e.srcData, e.position, 5);//这个构造方法跟顺序表删除的一样，但是为了区别，在多传一个无用参数
            this.pageContainer.Navigate(maindemon, "LinkedListedDelete");
        }
        /// <summary>
        /// 顺序表的查找
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
       private void Label_MouseLeftButtonDownOrderSearch(object sender, MouseButtonEventArgs e)
       {
            getCurrentOperator(sender);
            maindemon = new MainDemon();
            maindemon.Demon_Code_Value.Height = 0;
            this.pageContainer.Navigate(maindemon, "OrderSearch");

            ListDialog OrderSearchWindow = new ListDialog(6);
            //订阅事件
            OrderSearchWindow.PassValuesEvent += new ListDialog.PassValuesHandler(ReceiveValuesOfOrderSearch);
            OrderSearchWindow.ShowDialog();
        }

        private void ReceiveValuesOfOrderSearch(object sender, PassValuesEventArgs e)
        {
            maindemon = new MainDemon("OrderSearch", e.srcData, e.searchData,true);
            this.pageContainer.Navigate(maindemon, "OrderSearch");
        }

       private void Label_MouseLeftButtonDownOrderBinSearch(object sender, MouseButtonEventArgs e)
       {
            getCurrentOperator(sender);
            maindemon = new MainDemon();
            maindemon.Demon_Code_Value.Height = 0;
            this.pageContainer.Navigate(maindemon, "BinarySearch");

            ListDialog BinarySearchWindow = new ListDialog(7);
            //订阅事件
            BinarySearchWindow.PassValuesEvent += new ListDialog.PassValuesHandler(ReceiveValuesOfBinaarySearch);
            BinarySearchWindow.ShowDialog();
        }
        private void ReceiveValuesOfBinaarySearch(object sender, PassValuesEventArgs e)
        {
            maindemon = new MainDemon("BinarySearch", e.srcData, e.searchData, false);
            this.pageContainer.Navigate(maindemon, "BinarySearch");
        }

        private void Label_MouseLeftButtonDownDirectInsertSort(object sender, MouseButtonEventArgs e)
        {
            getCurrentOperator(sender);
            maindemon = new MainDemon();
            maindemon.Demon_Code_Value.Height = 0;
            this.pageContainer.Navigate(maindemon, "DirInsSort");

            ListDialog DirInsSortWindow = new ListDialog(8);
            //订阅事件
            DirInsSortWindow.PassValuesEvent += new ListDialog.PassValuesHandler(ReceiveValuesOfDirInsSort);
            DirInsSortWindow.ShowDialog();
        }

        private void ReceiveValuesOfDirInsSort(object sender, PassValuesEventArgs e)
        {
            maindemon = new MainDemon("DirInsSort", e.srcData,'1');//直接插入排序---1，‘1’三个排序的构造函数一样，为了区别，传递一个mark
            this.pageContainer.Navigate(maindemon, "DirInsSort");
        }

        private void Label_MouseLeftButtonDownSwapSort(object sender, MouseButtonEventArgs e)
        {
            getCurrentOperator(sender);
            maindemon = new MainDemon();
            maindemon.Demon_Code_Value.Height = 0;
            this.pageContainer.Navigate(maindemon, "SwapSort");

            ListDialog SwapSortWindow = new ListDialog(9);
            //订阅事件
            SwapSortWindow.PassValuesEvent += new ListDialog.PassValuesHandler(ReceiveValuesOfSwapSort);
            SwapSortWindow.ShowDialog();
        }

        private void ReceiveValuesOfSwapSort(object sender, PassValuesEventArgs e)
        {
            maindemon = new MainDemon("SwapSort", e.srcData, '2');//冒泡排序---2
            this.pageContainer.Navigate(maindemon, "SwapSort");
        }

        private void Label_MouseLeftButtonDownPartitionSort(object sender, MouseButtonEventArgs e)
        {
            getCurrentOperator(sender);
            maindemon = new MainDemon();
            maindemon.Demon_Code_Value.Height = 0;
            this.pageContainer.Navigate(maindemon, "QuickSort");

            ListDialog FastSortWindow = new ListDialog(10);
            //订阅事件
            FastSortWindow.PassValuesEvent += new ListDialog.PassValuesHandler(ReceiveValuesOfFastSort);
            FastSortWindow.ShowDialog();
        }
        private void ReceiveValuesOfFastSort(object sender, PassValuesEventArgs e)
        {
            maindemon = new MainDemon("QuickSort", e.srcData, '3');//快速排序---3
            this.pageContainer.Navigate(maindemon, "QuickSort");
        }
        //例子窗口
        ExampleDemon exampleDemon = null;
        /// <summary>
        /// 顺序表的例子（两个集合的合并）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void orderExample_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            getCurrentOperator(sender);
            exampleDemon = new ExampleDemon(1);
            this.pageContainer.Navigate(exampleDemon, "OrderMerge");
        }
        /// <summary>
        /// 单链表例子（删除带头结点链表中最大的元素）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkedExample_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            getCurrentOperator(sender);
            exampleDemon = new ExampleDemon(2);
            this.pageContainer.Navigate(exampleDemon, "LinkedReverse");
        }
    }
}
