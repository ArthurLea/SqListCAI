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
using SqListCAI.Utils;
namespace SqListCAI.Pages.BaseContent
{
    /// <summary>
    /// SqListDefine.xaml 的交互逻辑
    /// </summary>
    public partial class SqListBassContent : Page
    {
        //private TextBlock tb_showContent;        
        private static string SQLIST_DEFINE = "";
        private static string SQLIST_FEATURE = "";
        private static string SQLIST_STRUCTURE = "";
        private static string SQLIST_OPERATOR = "";
        public SqListBassContent()
        {
            InitializeComponent();
        }
        public SqListBassContent(uint flag)
        {
            InitializeComponent();
            switch (flag)
            { 
                case 1://define
                    if (SQLIST_DEFINE == "")
                        SQLIST_DEFINE = System.Text.Encoding.Default.GetString(Files.read("sqListDefine.txt"));
                    this.tb_showContent.Text = SQLIST_DEFINE;
                    break;
                case 2://feature
                    if (SQLIST_FEATURE == "")
                        SQLIST_FEATURE = System.Text.Encoding.Default.GetString(Files.read("sqListFeature.txt"));
                    this.tb_showContent.Text = SQLIST_FEATURE;
                    break;
                case 3://structure
                    if (SQLIST_STRUCTURE == "")
                        SQLIST_STRUCTURE = System.Text.Encoding.Default.GetString(Files.read("sqListStructure.txt"));
                    this.tb_showContent.Text = SQLIST_STRUCTURE;
                    break;
                case 4://operator
                    if (SQLIST_OPERATOR == "")
                        SQLIST_OPERATOR = System.Text.Encoding.Default.GetString(Files.read("sqListFeature.txt"));
                    this.tb_showContent.Text = SQLIST_OPERATOR;
                    break;
                default:

                    break;
            }
        }

    }
}
