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
using SqListCAI.Base_Content;
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
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void openBaseContentsWindow(object sender, RoutedEventArgs e)
        {
            BaseContentWindow bcw = new BaseContentWindow();
            this.Close();
            bcw.Show();
        }

        private void openMapWaysWindow(object sender, RoutedEventArgs e)
        {

        }

        private void openFindSortWindow(object sender, RoutedEventArgs e)
        {

        }

        private void openIllustrateWindow(object sender, RoutedEventArgs e)
        {

        }

    }
}
