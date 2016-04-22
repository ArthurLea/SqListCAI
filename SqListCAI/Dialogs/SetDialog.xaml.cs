using System;
using System.ComponentModel;
using System.Windows;

namespace SqListCAI.Dialogs
{
    /// <summary>
    /// SetDialog.xaml 的交互逻辑
    /// </summary>
    public partial class SetDialog : Window
    {
        public static bool isOpen = false;
        public static bool isInit = false;
        public SetDialog()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;//窗口居中
            if(App.MP == null)
                App.init_MP();//初始化全局MediaPlayer

            if(isOpen)
                this.checkBox_open.IsChecked = true;
            isInit = true;
            this.Closing += windowSizeChanged;
        }

        private void windowSizeChanged(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void Button_OK(object sender, RoutedEventArgs e)
        {
            App.WAITTIME =  this.slider_demon.Value;
            if(this.checkBox_open.IsChecked == true)//播放音乐
            {
                App.Play();
                isOpen = true;
            }
            else
            {
                App.Stop();
                isOpen = false;
            }
            this.Hide();
        }
        private void Button_Cancle(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void slider_sound_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (App.MP == null)
                App.init_MP();  
            App.MP.Volume = e.NewValue;
            if (isInit)
                this.sliderCurrent_sound.Text = (int)e.NewValue + "";
        }

        private void slider_demon_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            App.WAITTIME = e.NewValue;
            Algorithm.AlgorThread.WAITTIME = (int)App.WAITTIME;//修改算法执行单行的时间
            if(isInit)
                this.sliderCurrent_Demon.Text = (int)App.WAITTIME + "";
        }
    }
}
