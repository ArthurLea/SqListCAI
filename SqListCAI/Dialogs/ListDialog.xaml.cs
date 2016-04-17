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
            switch(flag)
            {
                case 1://顺序表插入
                    radioButton_orderIns.IsChecked = true;
                    this.listDialog.Title = "顺序表插入";
                    break;
                case 2://顺序表删除
                    radioButton_orderDel.IsChecked = true;
                    this.listDialog.Title = "顺序表删除";
                    this.lable_insertData.Background = Brushes.DarkBlue;
                    this.insertData.IsEnabled = false;
                    this.insertData.Background = Brushes.DarkBlue;
                    this.lable_place.Content = "删除位置";
                    break;
                case 3://链表创建
                    radioButton_linkedCre.IsChecked = true;
                    this.listDialog.Title = "链表创建";
                    this.srcData.Text = "【?9bD";
                    this.lable_insertData.Background = Brushes.DarkBlue;
                    this.insertData.IsEnabled = false; this.insertData.Background = Brushes.DarkBlue;
                    this.lable_place.Background = Brushes.DarkBlue;
                    this.position.IsEnabled = false; this.position.Background = Brushes.DarkBlue;
                    break;
                case 4://链表插入
                    radioButton_linkedIns.IsChecked = true;
                    this.listDialog.Title = "链表插入";
                    this.srcData.Text = "【?9bD";
                    break;
                case 5://链表删除
                    radioButton_linkedDel.IsChecked = true;
                    this.listDialog.Title = "链表删除";
                    this.srcData.Text = "【?9bD";
                    this.lable_insertData.Background = Brushes.DarkBlue;
                    this.insertData.IsEnabled = false;
                    this.insertData.Background = Brushes.DarkBlue;
                    this.lable_place.Content = "删除位置";
                    break;
                default:
                    break;
            }
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
                default:
                    break;
            }
        }

        private void linkedListIns()
        {
            insert_Order_Linked();
        }

        private void linkedListCre()
        {
            if ((this.srcData.Text == String.Empty))
            {
                MessageBox.Show("内容不能为空！", "警告", MessageBoxButton.OK);
                return;
            }
            string src = this.srcData.Text;
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
                int position = Convert.ToInt32(this.position.Text);
                PassValuesEventArgs args = new PassValuesEventArgs(src, position);
                PassValuesEvent(this, args);
                this.Close();
            }
            catch (FormatException fe)
            {
                MessageBox.Show("插入位置必须为数字！" + fe.StackTrace, "警告", MessageBoxButton.OK);
                return;
            }
        }

        private void orderInsert()
        {
            insert_Order_Linked();
        }

        private void insert_Order_Linked()
        {
            if ((this.srcData.Text == String.Empty) || insertData.Text.Equals("") || (this.position.Text == String.Empty))
            {
                MessageBox.Show("内容不能为空！", "警告", MessageBoxButton.OK);
                return;
            }
            try
            {
                string src = this.srcData.Text;
                if (this.insertData.Text.Length > 1)
                {
                    MessageBox.Show("插入内容为一个字符！", "警告", MessageBoxButton.OK);
                    return;
                }
                char insert = this.insertData.Text[0];

                int position = Convert.ToInt32(this.position.Text);
                PassValuesEventArgs args = new PassValuesEventArgs(src, insert, position);
                PassValuesEvent(this, args);
                this.Close();
            }
            catch (FormatException fe)
            {
                MessageBox.Show("插入位置必须为数字！" + fe.StackTrace, "警告", MessageBoxButton.OK);
                return;
            }
        }

        private void button_radom_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button_reset_Click(object sender, RoutedEventArgs e)
        {
            this.srcData.Text = "";
            this.insertData.Text = "";
            this.position.Text = "";
        }
    }
}
