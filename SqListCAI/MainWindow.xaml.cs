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
using System.Windows.Navigation;
using System.Windows.Shapes;

using SqListCAI.Pages.BaseContent;
namespace SqListCAI
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;//窗口居中

            this.notifyCount.Text = "20";
            // 应用下拉日历样式
            TextBox tb = new TextBox();
            Style sCalendar = (Style)tb.TryFindResource("tbCalendarStyle");
            if (sCalendar != null)
                this.datePick.Style = sCalendar;
            //应用动画
            System.Windows.Media.Animation.Storyboard s = (System.Windows.Media.Animation.Storyboard)TryFindResource("sb");
            s.Begin();	// Start animation

        }
        /// <summary>
        /// 鼠标左键按下后未放开可以开始拖动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Logout_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Label_MouseLeftButtonDownSQListDefine(object sender, MouseButtonEventArgs e)
        {
            //this.pageContainer.Source = new Uri("Pages/BaseContent/SqListDefine.xaml", UriKind.RelativeOrAbsolute);
            this.pageContainer.Navigate(new SqListBassContent(1),"1");
        }
        private void Label_MouseLeftButtonDownSQListFeature(object sender, MouseButtonEventArgs e)
        {
            //this.pageContainer.Navigate(new SqListDefine(), "123");
            this.pageContainer.Navigate(new SqListBassContent(2), "1");
        }
        private void Label_MouseLeftButtonDownSQListStructure(object sender, MouseButtonEventArgs e)
        {
            this.pageContainer.Navigate(new SqListBassContent(3), "1");
        }
        private void Label_MouseLeftButtonDownSQListOperator(object sender, MouseButtonEventArgs e)
        {
            this.pageContainer.Navigate(new SqListBassContent(4), "1");
        }
        private bool __isLeftHide = false;
        private void spliter_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //Left Bar hide and show
            __isLeftHide = !__isLeftHide;
            if (__isLeftHide)
            {
                this.gridForm.ColumnDefinitions[0].Width = new GridLength(1d);
            }
            else
            {
                this.gridForm.ColumnDefinitions[0].Width = new GridLength(200d);
            }

        }


        private void TabItem_MouseMove_1(object sender, MouseEventArgs e)
        {
            //var part_text= this.LeftTabControl.Template.FindName("PART_Text", this.LeftTabControl);
            //null
        }


    }
}
