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

namespace SqListCAI.Instances
{
    /// <summary>
    /// WindowSwf.xaml 的交互逻辑
    /// </summary>
    public partial class WindowSwf : Window
    {
        public WindowSwf()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;//窗口居中
            ExamplePage examplePage = new ExamplePage();
            this.WindowSwfPageContainer.Navigate(examplePage, "OrderInsertSwf");
        }
    }
}
