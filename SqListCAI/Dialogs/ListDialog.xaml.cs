using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SqListCAI.Events;
namespace SqListCAI.Dialogs
{
    /// <summary>
    /// InsertWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ListDialog : Window
    {
        public delegate void PassValuesHandler(object sender, PassValuesEventArgs e);
        public event PassValuesHandler PassValuesEvent;

        int flag;
        public ListDialog(int flag)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;//窗口居中
            this.flag = flag;
            switch (flag)
            {
                case 1://顺序表插入
                    radioButton_orderIns.IsChecked = true;
                    radioButton_orderIns.Foreground = Brushes.Green;
                    this.listDialog.Title = "顺序表插入";
                    this.srcData.Text = "xmXpzvq6s4CRb";
                    this.insertData.Text = 'n' + "";
                    this.position.Text = 4 + "";
                    break;
                case 2://顺序表删除
                    radioButton_orderDel.IsChecked = true;
                    radioButton_orderDel.Foreground = Brushes.Green;
                    this.listDialog.Title = "顺序表删除";
                    this.srcData.Text = "3zE5NwmahrbOpS";
                    this.position.Text = 5 +"";
                    this.lable_insertData.Background = Brushes.DarkGray; this.lable_insertData.Foreground = Brushes.DarkGray;
                    this.insertData.IsEnabled = false; this.insertData.Background = Brushes.DarkGray; this.insertData.Foreground = Brushes.DarkGray;
                    this.insertData.Visibility = Visibility.Hidden;
                    this.lable_place.Content = "删除位置";
                    break;
                case 3://链表创建
                    radioButton_linkedCre.IsChecked = true;
                    radioButton_linkedCre.Foreground = Brushes.Green;
                    this.listDialog.Title = "链表创建";
                    this.srcData.Text = "E89bD";
                    this.lable_insertData.Background = Brushes.DarkGray; this.lable_insertData.Foreground = Brushes.DarkGray;
                    this.insertData.IsEnabled = false; this.insertData.Background = Brushes.DarkGray; this.insertData.Foreground = Brushes.DarkGray;
                    this.insertData.Visibility = Visibility.Hidden;
                    this.lable_place.Background = Brushes.DarkGray; this.lable_place.Foreground = Brushes.DarkGray;
                    this.position.IsEnabled = false; this.position.Background = Brushes.DarkGray; this.position.Foreground = Brushes.DarkGray;
                    this.position.Visibility = Visibility.Hidden;                   
                    break;
                case 4://链表插入
                    radioButton_linkedIns.IsChecked = true;
                    radioButton_linkedIns.Foreground = Brushes.Green;
                    this.listDialog.Title = "链表插入";
                    this.srcData.Text = "Z6ehS";
                    this.insertData.Text = 'I' + "";
                    this.position.Text = 4+"";
                    break;
                case 5://链表删除
                    radioButton_linkedDel.IsChecked = true;
                    radioButton_linkedDel.Foreground = Brushes.Green;
                    this.listDialog.Title = "链表删除";
                    this.srcData.Text = "N8V56";
                    this.position.Text = 3+"";
                    this.lable_insertData.Background = Brushes.DarkGray; this.lable_insertData.Foreground = Brushes.DarkGray;
                    this.insertData.IsEnabled = false; this.insertData.Background = Brushes.DarkGray; this.insertData.Foreground = Brushes.DarkGray;
                    this.insertData.Visibility = Visibility.Hidden;
                    this.lable_place.Content = "删除位置";
                    break;
                case 6://顺序查找
                    radioButton_OreSearch.IsChecked = true;
                    radioButton_OreSearch.Foreground = Brushes.Green;
                    this.listDialog.Title = "顺序查找";
                    this.srcData.Text = "1e9X2AcDXdfop";//1e9X2AcDXdf
                    this.lable_insertData.Content = "查找元素：";
                    this.insertData.Text = "A";
                    this.lable_place.Background = Brushes.DarkGray; this.lable_place.Foreground = Brushes.DarkGray;
                    this.position.IsEnabled = false; this.position.Background = Brushes.DarkGray; this.position.Foreground = Brushes.DarkGray;
                    this.position.Visibility = Visibility.Hidden; 
                    break;
                case 7://折半查找
                    radioButton_BinSearch.IsChecked = true;
                    radioButton_BinSearch.Foreground = Brushes.Green;
                    this.listDialog.Title = "折半查找";
                    this.srcData.Text = "acdefghjlopsv";//需要排序
                    this.lable_insertData.Content = "查找元素：";
                    this.insertData.Text = "f";
                    this.lable_place.Background = Brushes.DarkGray; this.lable_place.Foreground = Brushes.DarkGray;
                    this.position.IsEnabled = false; this.position.Background = Brushes.DarkGray; this.position.Foreground = Brushes.DarkGray;
                    this.position.Visibility = Visibility.Hidden; 
                    break;
                case 8://直接插入排序
                    radioButton_InsSort.IsChecked = true;
                    radioButton_InsSort.Foreground = Brushes.Green;
                    this.listDialog.Title = "直接插入排序";
                    sortCommon();
                    break;
                case 9://交换排序
                    radioButton_SwapSort.IsChecked = true;
                    radioButton_SwapSort.Foreground = Brushes.Green;
                    this.listDialog.Title = "交换排序";
                    sortCommon();
                    break;
                case 10://快速排序
                    radioButton_FastSort.IsChecked = true;
                    radioButton_FastSort.Foreground = Brushes.Green;
                    this.listDialog.Title = "快速排序";
                    sortCommon();
                    break;
                default:
                    break;
            }
        }
        private void sortCommon()
        {
            this.srcData.Text = "4196582703";//待排序
            this.lable_insertData.Background = Brushes.DarkGray; this.lable_insertData.Foreground = Brushes.DarkGray;
            this.insertData.IsEnabled = false; this.insertData.Background = Brushes.DarkGray; this.insertData.Foreground = Brushes.DarkGray;
            this.insertData.Visibility = Visibility.Hidden;
            this.lable_place.Background = Brushes.DarkGray; this.lable_place.Foreground = Brushes.DarkGray;
            this.position.IsEnabled = false; this.position.Background = Brushes.DarkGray; this.position.Foreground = Brushes.DarkGray;
            this.position.Visibility = Visibility.Hidden; 
        }

        private void button_ok_Click(object sender, RoutedEventArgs e)
        {

            switch(flag)
            {
                case 1:
                    if(radioButton_orderIns.IsChecked == false)
                    {
                        MessageBox.Show("请选中线性表插入!", "警告", MessageBoxButton.OK);
                        return;
                    }
                    orderInsert();
                    break;
                case 2:
                    if (radioButton_orderDel.IsChecked == false)
                    {
                        MessageBox.Show("请选中线性表删除！", "警告", MessageBoxButton.OK);
                        return;
                    }
                    orderDelete();
                    break;
                case 3:
                    if (radioButton_linkedCre.IsChecked == false)
                    {
                        MessageBox.Show("请选中链表创建！", "警告", MessageBoxButton.OK);
                        return;
                    }
                    linkedListCre();
                    break;
                case 4:
                    if(radioButton_linkedIns.IsChecked == false)
                    {
                        MessageBox.Show("请选中链表插入！", "警告", MessageBoxButton.OK);
                        return;
                    }
                    linkedListIns();
                    break;
                case 5:
                    if (radioButton_linkedDel.IsChecked == false)
                    {
                        MessageBox.Show("请选中链表删除！", "警告", MessageBoxButton.OK);
                        return;
                    }
                    linkedListDel();
                    break;
                case 6:
                    if(radioButton_OreSearch.IsChecked == false)
                    {
                        MessageBox.Show("请选中顺序查找！", "警告", MessageBoxButton.OK);
                        return;
                    }
                    orderSearch();
                    break;
                case 7:
                    if (radioButton_BinSearch.IsChecked == false)
                    {
                        MessageBox.Show("请选中折半查找！", "警告", MessageBoxButton.OK);
                        return;
                    }
                    binarySearch();
                    break;
                case 8:
                    if (radioButton_InsSort.IsChecked == false)
                    {
                        MessageBox.Show("请选中插入排序！", "警告", MessageBoxButton.OK);
                        return;
                    }
                    getPrepareSortData();
                    break;
                case 9:
                    if (radioButton_SwapSort.IsChecked == false)
                    {
                        MessageBox.Show("请选中冒泡排序！", "警告", MessageBoxButton.OK);
                        return;
                    }
                    getPrepareSortData();
                    break;
                case 10:
                    if (radioButton_FastSort.IsChecked == false)
                    {
                        MessageBox.Show("请选中快速排序！", "警告", MessageBoxButton.OK);
                        return;
                    }
                    getPrepareSortData();
                    break;
                default:
                    break;
            }
        }

        private void getPrepareSortData()
        {
            if ((this.srcData.Text == String.Empty))
            {
                MessageBox.Show("内容不能为空！", "警告", MessageBoxButton.OK);
                return;
            }
            string src = this.srcData.Text;
            src = srcDataSpilt(src, '8');//判断源数据输入是否合法（主要是长度的检测）
            PassValuesEventArgs args = new PassValuesEventArgs(src);
            PassValuesEvent(this, args);
            this.Close();
        }

        private void binarySearch()
        {
            if ((this.srcData.Text == String.Empty) || insertData.Text.Equals(""))
            {
                MessageBox.Show("内容不能为空！", "警告", MessageBoxButton.OK);
                return;
            }
            string src = this.srcData.Text;
            src = srcDataSpilt(src, '6');//判断源数据输入是否合法（主要是长度的检测）
            char[] srcCh = new char[src.Length];
            for (int i = 0; i < src.Length; i++)
                srcCh[i] = src[i];

            StringBuilder sb = new StringBuilder(13);
            //对src进行排序
            for(int i=0;i<srcCh.Length;i++)
            {
                for(int j=i+1;j< srcCh.Length;j++)
                {
                    if(srcCh[j] < srcCh[i])
                    {
                        char temp = srcCh[j];
                        srcCh[j] = srcCh[i];
                        srcCh[i] = temp;
                    }
                }
            }

            if (this.insertData.Text.Length > 1)
            {
                MessageBox.Show("插入内容为一个字符！", "警告", MessageBoxButton.OK);
                return;
            }
            char binartData = this.insertData.Text[0];

            PassValuesEventArgs args = new PassValuesEventArgs(new string(srcCh), binartData);
            PassValuesEvent(this, args);
            this.Close();
        }

        private void orderSearch()
        {
            if ((this.srcData.Text == String.Empty) || insertData.Text.Equals(""))
            {
                MessageBox.Show("内容不能为空！", "警告", MessageBoxButton.OK);
                return;
            }
            string src = this.srcData.Text;
            src = srcDataSpilt(src, '6');//判断源数据输入是否合法（主要是长度的检测）
            if (this.insertData.Text.Length > 1)
            {
                MessageBox.Show("插入内容为一个字符！", "警告", MessageBoxButton.OK);
                return;
            }
            char orderSearch = this.insertData.Text[0];

            PassValuesEventArgs args = new PassValuesEventArgs(src, orderSearch);
            PassValuesEvent(this, args);
            this.Close();
        }

        private void linkedListDel()
        {
            if ((this.srcData.Text == String.Empty) || (this.position.Text == String.Empty))
            {
                MessageBox.Show("内容不能为空！", "警告", MessageBoxButton.OK);
                return;
            }
            try
            {
                string src = this.srcData.Text;
                src = srcDataSpilt(src, '3');//判断源数据输入是否合法（主要是长度的检测）
                int position = Convert.ToInt32(this.position.Text);
                if((2<=position) && (position<=src.Length))
                {
                    PassValuesEventArgs args = new PassValuesEventArgs(src, position);
                    PassValuesEvent(this, args);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("删除位置必须为[2," + src.Length + "]", "警告", MessageBoxButton.OK);
                    return;
                }
            }
            catch (FormatException fe)
            {
                MessageBox.Show("插入位置必须为数字！" + fe.StackTrace, "警告", MessageBoxButton.OK);
                return;
            }
        }

        private void linkedListIns()
        {
            if ((this.srcData.Text == String.Empty) || insertData.Text.Equals("") || (this.position.Text == String.Empty))
            {
                MessageBox.Show("内容不能为空！", "警告", MessageBoxButton.OK);
                return;
            }
            try
            {
                string src = this.srcData.Text;
                src = srcDataSpilt(src, '3');//判断源数据输入是否合法（主要是长度的检测）
                if (this.insertData.Text.Length > 1)
                {
                    MessageBox.Show("插入内容为一个字符！", "警告", MessageBoxButton.OK);
                    return;
                }
                char insert = this.insertData.Text[0];
                int position = Convert.ToInt32(this.position.Text);
                if((2<=position) && (position<=(src.Length+1)))
                {
                    PassValuesEventArgs args = new PassValuesEventArgs(src, insert, position);
                    PassValuesEvent(this, args);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("插入位置必须为[2," + (src.Length + 1) + "]", "警告", MessageBoxButton.OK);
                    return;
                }
            }
            catch (FormatException fe)
            {
                MessageBox.Show("插入位置必须为数字！" + fe.StackTrace, "警告", MessageBoxButton.OK);
                return;
            }
        }

        private void linkedListCre()
        {
            if ((this.srcData.Text == String.Empty))
            {
                MessageBox.Show("内容不能为空！", "警告", MessageBoxButton.OK);
                return;
            }
            string src = this.srcData.Text;
            src = srcDataSpilt(src, '3');//判断源数据输入是否合法（主要是长度的检测）
            PassValuesEventArgs args = new PassValuesEventArgs(src);
            PassValuesEvent(this, args);
            this.Close();
        }

        private void orderDelete()
        {
            if ((this.srcData.Text == String.Empty) || (this.position.Text == String.Empty))
            {
                MessageBox.Show("内容不能为空！", "警告", MessageBoxButton.OK);
                return;
            }
            try
            {
                string src = this.srcData.Text;
                src = srcDataSpilt(src, '2');//判断源数据输入是否合法（主要是长度的检测）
                int position = Convert.ToInt32(this.position.Text);
                if((1<=position) && (position<=src.Length))
                {
                    PassValuesEventArgs args = new PassValuesEventArgs(src, position);
                    PassValuesEvent(this, args);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("删除位置必须为[1," + src.Length + "]", "警告", MessageBoxButton.OK);
                    return;
                }
            }
            catch (FormatException fe)
            {
                MessageBox.Show("插入位置必须为数字！" + fe.StackTrace, "警告", MessageBoxButton.OK);
                return;
            }
        }

        private void orderInsert()
        {
            if ((this.srcData.Text == String.Empty) || insertData.Text.Equals("") || (this.position.Text == String.Empty))
            {
                MessageBox.Show("内容不能为空！", "警告", MessageBoxButton.OK);
                return;
            }
            try
            {
                string src = this.srcData.Text;
                src = srcDataSpilt(src,'1');//判断源数据输入是否合法（主要是长度的检测）得到源数据
                if (this.insertData.Text.Length > 1)
                {
                    MessageBox.Show("插入内容为一个字符！", "警告", MessageBoxButton.OK);
                    return;
                }
                char insert = this.insertData.Text[0];//得到插入数据
                int position = Convert.ToInt32(this.position.Text); //得到插入位置
                if((1<=position) && (position<=src.Length+1))
                {
                    PassValuesEventArgs args = new PassValuesEventArgs(src, insert, position);
                    PassValuesEvent(this, args);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("插入位置必须为[1,"+(src.Length+1)+"]", "警告", MessageBoxButton.OK);
                    return;
                }
            }
            catch (FormatException fe)
            {
                MessageBox.Show("插入位置必须为数字！" + fe.StackTrace, "警告", MessageBoxButton.OK);
                return;
            }
        }
        public static string SRCDATAWARN = "源数据警告";
        /// <summary>
        /// 源数据的截取（因为具体空间的限制），截取方式为前截取
        /// </summary>
        /// <param name="src"></param>
        /// <param name="currntOperatorFlag"></param>
        /// <returns></returns>
        private string srcDataSpilt(string src, char currntOperatorFlag)
        {
            if (MainWindow.__isLeftHide)//隐藏（未展开，宽度变长）
            {

                switch (currntOperatorFlag)
                {
                    case '1'://最长17（顺序表插入）
                        if(src.Length>17)
                        {
                            MessageBox.Show("源数据最多输入17个字符，已截取", SRCDATAWARN, MessageBoxButton.OK);
                            src = src.Substring(0, 17);//截取17个字符
                        }
                        break;
                    case '2'://最长18（顺序表删除）
                        if (src.Length > 18)
                        {
                            MessageBox.Show("源数据最多输入18个字符，已截取", SRCDATAWARN, MessageBoxButton.OK);
                            src = src.Substring(0, 18);//截取18个字符
                        }
                        break;
                    case '3'://最长7(链表的创建、插入、删除都是一样的判断)
                        if (src.Length > 7)
                        {
                            MessageBox.Show("源数据最多输入7个字符，已截取", SRCDATAWARN, MessageBoxButton.OK);
                            src = src.Substring(0, 7);//截取7个字符
                        }
                        break;
                    case '6'://最长17(顺序查找、折半查找都是一样的截取)
                        if (src.Length > 17)
                        {
                            MessageBox.Show("源数据最多输入17个字符，已截取", SRCDATAWARN, MessageBoxButton.OK);
                            src = src.Substring(0, 17);//截取17个字符
                        }
                        break;
                    case '8'://最长13（插入、交换、快速排序都是一样的截取)
                        if (src.Length > 13)
                        {
                            MessageBox.Show("源数据最多输入14个字符，已截取", SRCDATAWARN, MessageBoxButton.OK);
                            src = src.Substring(0, 13);//截取14个字符
                        }
                        break;
                    default:
                        break;
                }
            }
            else//未隐藏（展开，宽度变窄）
            {
                switch(currntOperatorFlag)
                {
                    case '1'://最长13
                        if (src.Length > 13)
                        {
                            MessageBox.Show("源数据最多输入13个字符，已截取", SRCDATAWARN, MessageBoxButton.OK);
                            src = src.Substring(0, 13);//截取13个字符
                        }
                        break;
                    case '2':
                        if (src.Length > 14)
                        {
                            MessageBox.Show("源数据最多输入14个字符，已截取", SRCDATAWARN, MessageBoxButton.OK);
                            src = src.Substring(0, 14);//截取14个字符
                        }
                        break;
                    case '3'://最长5(链表的创建、插入、删除都是一样的判断)
                        if (src.Length > 5)
                        {
                            MessageBox.Show("源数据最多输入5个字符，已截取", SRCDATAWARN, MessageBoxButton.OK);
                            src = src.Substring(0, 5);//截取5个字符
                        }
                        break;
                    case '6'://最长13(顺序查找、折半查找都是一样的截取)
                        if (src.Length > 13)
                        {
                            MessageBox.Show("源数据最多输入13个字符，已截取", SRCDATAWARN, MessageBoxButton.OK);
                            src = src.Substring(0, 13);//截取13个字符
                        }
                        break;
                    case '8'://最长11（插入、交换、快速排序都是一样的截取)
                        if (src.Length > 10)
                        {
                            MessageBox.Show("源数据最多输入11个字符，已截取", SRCDATAWARN, MessageBoxButton.OK);
                            src = src.Substring(0, 10);//截取11个字符
                        }
                        break;
                    default:
                        break;
                }
            }
            return src;
        }
        char[] chars = new char[] 
        {
            '0','1','2','3','4','5','6','7','8','9',
            'a','b','c','d','e','f','g','h','i','j',
            'k','l','m','n','o','p','q','r','s','t',
            'u','v','w','x','y','z',
            'A','B','C','D','E','F','G','H','I','J',
            'K','L','M','N','O','P','Q','R','S','T',
            'U','V','W','X','Y','Z',
        };
        string str = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private void button_radom_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow.__isLeftHide)//隐藏（未展开，宽度变长）
            {
                switch (flag)
                {
                    case 1://顺序表插入、最长17
                        generateData(17);
                        break;
                    case 2://顺序表删除、最长18
                        generateData(18);
                        break;
                    case 3://(链表的创建、插入、删除都是一样的判断)、最长7
                        generateData(7);
                        break;
                    case 4:
                        generateData(7);
                        break;
                    case 5:
                        generateData(7);
                        break;
                    case 6://(顺序查找、折半查找都是一样的截取)、最长17
                        generateData(17);
                        break;
                    case 7:
                        generateData(17);
                        break;
                    case 8://插入、交换、快速排序都是一样的截取)、最长13
                        generateData(13);
                        break;
                    case 9:
                        generateData(13);
                        break;
                    case 10:
                        generateData(13);
                        break;
                    default:
                        break;
                }
            }
            else//未隐藏（展开，宽度变窄）
            {
                switch (flag)
                {
                    case 1://顺序表插入、最长13
                        generateData(13);
                        break;
                    case 2://顺序表插入、最长14
                        generateData(14);
                        break;
                    case 3://(链表的创建、插入、删除都是一样的判断)、最长5
                        generateData(5);
                        break;
                    case 4:
                        generateData(5);
                        break;
                    case 5:
                        generateData(5);
                        break;
                    case 6://(顺序查找、折半查找都是一样的截取)、最长13
                        generateData(13);
                        break;
                    case 7:
                        generateData(13);
                        break;
                    case 8://插入、交换、快速排序都是一样的截取)、最长10
                        generateData(10);
                        break;
                    case 9:
                        generateData(10);
                        break;
                    case 10:
                        generateData(10);
                        break;
                    default:
                        break;
                }
            }
            str = new String(chars);
        }

        private void generateData(int maxLength)
        {
            Random random = new Random();
            StringBuilder sb_srcData = new StringBuilder();
            int length;
            int position;
            for (int i = 0; i < maxLength; i++)
            {
                length = str.Length;
                position = random.Next(length);
                char ch = str[position];
                sb_srcData.Append(ch);
                str = str.Remove(position, 1);
            }
            this.srcData.Text = sb_srcData + "";//源数据生成
            if((flag==6) || (flag==7))//查找就在源数据中查找
            {
                position = random.Next(sb_srcData.Length);
                this.insertData.Text = sb_srcData[position] + "";//插入数据生成
            }
            else
            {
                position = random.Next(str.Length);
                this.insertData.Text = str[position] + "";//插入数据生成
            }
            if (flag == 4)//链表插入生成的插入位置检测
            {
                position = random.Next(2, sb_srcData.Length+1+1);//链表插入生成的插入位置必须在[2,src.Length+1)
            }
            else if (flag == 5)//链表删除生成的删除位置检测
            {
                position = random.Next(2, sb_srcData.Length+1);//[2,sb_srcData.Length)
            }
            else
            {
                position = random.Next(1,sb_srcData.Length+1);//[1,sb_srcData.Length)
            }
            this.position.Text = position + "";//插入位置生成
        }

        private void button_reset_Click(object sender, RoutedEventArgs e)
        {
            this.srcData.Text = "";
            this.insertData.Text = "";
            this.position.Text = "";
        }
    }
}
