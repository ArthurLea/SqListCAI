using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace SqListCAI
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public static double WAITTIME = 400;//执行算法的一行时需要缓冲等待的时间
        public static MediaPlayer MP = null;
        public static void init_MP()
        {
            MP = new MediaPlayer();
            Uri uri = new Uri(Directory.GetCurrentDirectory()+"/Audios/K.Will-说干什么呢.MP3");
            MP.Open(uri);
        }

        private static void AgainPlay(object sender, EventArgs e)
        {
            MP.Play();
        }
        //全局播放音乐
        public static void Play()
        {
            MP.MediaEnded += new EventHandler(media_MediaEnded);
            MP.Balance = 0.0;
            MP.Play();
        }

        private static void media_MediaEnded(object sender, EventArgs e)
        {
            init_MP();
            MP.Play();
        }

        public static void Pause()
        {
            MP.Pause();
        }
        public static void Stop()
        {
            MP.Stop();
        }
    }
}
