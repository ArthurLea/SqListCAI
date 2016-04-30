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
using SqListCAI.Utils;

namespace SqListCAI.Base_Content
{
    /// <summary>
    /// BaseContentWindow.xaml 的交互逻辑
    /// </summary>
    public partial class BaseContentWindow : Window
    {
        private TextBox textBox;
        private string SQLIST_DEFINE = "";
        private string SQLIST_FEATURE = "";
        private string SQLIST_STRUCTURE = "";
        private string SQLIST_OPERATOR = "";
        public BaseContentWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            textBox = (TextBox)FindName("tb_showContent");
        }

        private void sqListDefine(object sender, RoutedEventArgs e)
        {
            SQLIST_DEFINE = System.Text.Encoding.Default.GetString(Files.read("sqListDefine.txt"));
            textBox.Text = SQLIST_DEFINE;
        }

        private void sqListFeature(object sender, RoutedEventArgs e)
        {
            SQLIST_FEATURE = System.Text.Encoding.Default.GetString(Files.read("sqListFeature.txt"));
            textBox.Text = SQLIST_FEATURE;
        }

        private void sqListStructure(object sender, RoutedEventArgs e)
        {
            SQLIST_STRUCTURE = System.Text.Encoding.Default.GetString(Files.read("sqListStructure.txt"));
            textBox.Text = SQLIST_STRUCTURE;
        }

        private void sqListOperator(object sender, RoutedEventArgs e)
        {

        }
    }
}
