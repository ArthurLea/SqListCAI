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
    public partial class InsertWindow : Window
    {
        public delegate void PassValuesHandler(object sender, PassValuesEventArgs e);
        public event PassValuesHandler PassValuesEvent;
        public InsertWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;//窗口居中
        }

        private void button_ok_Click(object sender, RoutedEventArgs e)
        {
            if ((this.srcData.Text == String.Empty) || insertData.Text.Equals("") || (this.insertPosition.Text == String.Empty))
            {
                MessageBox.Show("内容不能为空！", "警告", MessageBoxButton.OK);
                return;
            }
            try
            {
                string src = this.srcData.Text;
                if(this.insertData.Text.Length > 1)
                {
                    MessageBox.Show("插入内容为一个字符！", "警告", MessageBoxButton.OK);
                    return;
                }
                char insert = this.insertData.Text[0];

                int position = Convert.ToInt32(this.insertPosition.Text);
                PassValuesEventArgs args = new PassValuesEventArgs(src, insert, position);
                PassValuesEvent(this, args);
                this.Close();
            }
            catch (FormatException fe)
            {
                MessageBox.Show("插入位置必须为数字！"+fe.StackTrace, "警告", MessageBoxButton.OK);
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
            this.insertPosition.Text = "";
        }
    }
}
