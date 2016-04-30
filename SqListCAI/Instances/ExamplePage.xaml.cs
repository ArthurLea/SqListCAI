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
using AxShockwaveFlashObjects;
using System.Windows.Forms.Integration;

namespace SqListCAI.Instances
{
    /// <summary>
    /// ExamplePage.xaml 的交互逻辑
    /// </summary>
    public partial class ExamplePage : Page
    {
        public ExamplePage()
        {
            InitializeComponent();
            //AxShockwaveFlash axShockwaveFlash = new AxShockwaveFlash();
            //this.windowsFormsHost.Child = axShockwaveFlash;
            //string swfPath = Environment.CurrentDirectory;
            //swfPath += @"\Swfs\顺序表的删除运算.swf";
            //axShockwaveFlash.Movie = swfPath; //指定播放的flash文件路径  
            ////添加flash调用处理方法，让WPF可以处理来自Flash的调用请求 
            //axShockwaveFlash.FlashCall += new _IShockwaveFlashEvents_FlashCallEventHandler(axShockwaveFlash_FlashCall);//添加flash调用处理方法，让WPF可以处理来自Flash的调用请求  

        }
        
        private void FlashLoaded(object sender, RoutedEventArgs e)
        {
            //WindowsFormsHost windowsFormsHost = new WindowsFormsHost();
            AxShockwaveFlash axShockwaveFlash = new AxShockwaveFlash();
            this.windowsFormsHost.Child = axShockwaveFlash;
            //mainGrid.Children.Add(formHost);
            string swfPath = Environment.CurrentDirectory;
            swfPath += @"\Swfs\顺序表的删除运算.swf";
            axShockwaveFlash.Movie = swfPath;
            axShockwaveFlash.Play();
        }
    }
}
