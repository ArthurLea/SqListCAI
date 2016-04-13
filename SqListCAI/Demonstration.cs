using SqListCAI.Pages.MainPage;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows;
using System.Data;
using SqListCAI.Entities;
namespace SqListCAI
{
    public partial class Demonstration
    {
        public static int[] index = null;
        int flag;
        MainDemon m_maindemon;
        public Demonstration(int flag,MainDemon maindemon)
        {
            this.flag = flag;
            this.m_maindemon = maindemon;
        }
        public void ShowCode(string[] str)
        {
            this.m_maindemon.listBox_code.Items.Clear();
            for (int i = 0; i < str.Length; i++)
                this.m_maindemon.listBox_code.Items.Insert(i, str[i]);

            //修改ListBox中某行，先移除，然后插入
            //TextBlock tb = new TextBlock();
            //tb.Text = str[1];
            //tb.Foreground = Brushes.Blue;
            //this.m_maindemon.listBox_code.Items.RemoveAt(0);
            //this.m_maindemon.listBox_code.Items.Insert(0, tb);
            //this.m_maindemon.listBox_code.Items.Add("1234567887946516316");//在末尾增加一行显示

        }
        public void ShowDemon()
        {
            m_maindemon.canse_demon.Children.Clear();
            switch (flag)
            {
                case 1://线性表插入
                    {
                        int margin_left = 20;
                        int margin_top = 90;
                        double ins_margin_left = 0.0d;
                        int length = SqList.length;
                        Rectangle[] rc = new Rectangle[length + 2];//矩形
                        Label[] lable = new Label[length + 2];//标签，放原始内容
                        index = new int[rc.Length];
                        for(int i=0;i<length+2;i++)//初始化需要画的数组矩形和标签元素的相同属性
                        {
                            rc[i] = new Rectangle();
                            rc[i].Stroke = Brushes.Yellow;
                            rc[i].Fill = Brushes.SkyBlue;
                            rc[i].Width = 50;
                            rc[i].Height = 35;

                            lable[i] = new Label();
                            lable[i].Width = 25;
                            lable[i].Height = 35;
                            lable[i].FontSize = 15;
                            lable[i].VerticalContentAlignment = VerticalAlignment.Center;

                            index[i] = 0;//初始化位置索引
                        }
                        //绘制原始数据
                        for (int i=0;i<length+1;i++)
                        {
                            double rc_margin_left = i * rc[i].Width + margin_left;
                            Thickness rc_src_margin = new Thickness(rc_margin_left, margin_top, 0, 0);
                            rc[i].Margin = rc_src_margin;

                            if(i==length)
                                lable[i].Content = '?';
                            else
                                lable[i].Content = SqList.srcData[i];
                            double lable_margin_left = rc_margin_left + (rc[i].Width - lable[i].Width) / 2;
                            Thickness lable_src_margin = new Thickness(lable_margin_left, margin_top, 0,0);
                            lable[i].Margin = lable_src_margin;

                            m_maindemon.canse_demon.Children.Add(rc[i]);
                            m_maindemon.canse_demon.Children.Add(lable[i]);
                            index[i] = m_maindemon.canse_demon.Children.IndexOf(rc[i]);//存储矩形在canse_demon中的位置
                                                                                       //相应lable位置在其后
                            //保存插入位置的矩形
                            if (SqList.insPosition == (i+1))
                            {
                                ins_margin_left = rc_margin_left;
                            }
                        }
                        //绘制插入数据
                        rc[rc.Length-1].Margin = new Thickness(ins_margin_left, margin_top - 60, 0, 0);
                        lable[rc.Length-1].Content = SqList.insertData;
                        lable[rc.Length-1].Margin = new Thickness(ins_margin_left + (rc[rc.Length-1].Width - lable[lable.Length-1].Width) / 2, margin_top - 60, 0, 0);
                        
                        m_maindemon.canse_demon.Children.Add(rc[rc.Length-1]);
                        m_maindemon.canse_demon.Children.Add(lable[lable.Length-1]);
                        index[index.Length-1] = m_maindemon.canse_demon.Children.IndexOf(rc[rc.Length-1]);//存储插入数据矩形位置
                        break;
                    }
                    
                default:
                    break;
            }
        }

        public void ShowValue(string[] str)
        {
            m_maindemon.listView_value.DataContext = null;
            switch (flag)
            {
                case 1://顺序表的插入
                    if(data.Columns.Count == 0)
                    {
                        data.Columns.Add(new DataColumn("NAME", typeof(string)));//第一列
                        data.Columns.Add(new DataColumn("VALUE", typeof(string)));//第二列
                    }
                    m_maindemon.listView_value.DataContext = GetDataTable(str,4,null,0).DefaultView;
                    break;
            }
        }
        public static DataTable data = new DataTable("DataTable");
        public DataTable GetDataTable(string[] str,int updateFlag,string updateValue,int movePosition)
        {
            //设置主键
            //ID.AutoIncrement = true; //自动递增ID号 
            //DataColumn[] keys = new DataColumn[1];
            //keys[0] = NAME;
            //data.PrimaryKey = keys;
            if(updateFlag == 0)//修改移动的值
            {
                data.Rows[movePosition+1]["VALUE"] = updateValue;
                m_maindemon.listView_value.SelectedIndex = movePosition;
            }
            if(updateFlag == 1)//修改P
            {
                data.Rows[SqList.length+5]["VALUE"] = updateValue;
                m_maindemon.listView_value.SelectedIndex = SqList.length + 5;
            }
            if(updateFlag == 2)//修改长度
            {
                data.Rows[SqList.length]["VALUE"] = updateValue;
                m_maindemon.listView_value.SelectedIndex = SqList.length;
            }
            if(updateFlag == 4)//显示最开始的数据
            {
                int length = SqList.length;
                for (int i = 0; i < length + 1; i++)//添加源数据
                {
                    string name = str[0] + ".srcData[" + i + "]";
                    string value = null;
                    if (i == length)
                        value = '?'.ToString();
                    else
                        value = SqList.srcData[i] + "";
                    data.Rows.Add(name, value);
                }
                data.Rows.Add(str[0] + ".length", SqList.length);
                data.Rows.Add(str[1], SqList.insPosition);
                data.Rows.Add(str[2], SqList.insertData);
                data.Rows.Add(str[3], SqList.MAXSIZE);
                data.Rows.Add(str[4], "未知");
            }
            return data;
        }
        public void ShowExplain()
        {
            switch (flag)
            {
                case 1://顺序表的插入
                    {
                        MessageBox.Show(Explain.OrderInsExplain, "顺序表插入", MessageBoxButton.OK);
                        break;
                    }
                case 2://顺序表的删除
                    {
                        MessageBox.Show(Explain.OrderDelExplain, "顺序表删除", MessageBoxButton.OK);
                        break;
                    }
                case 3://链表的创建
                    {
                        break;
                    }
                case 4://链表的插入
                    {
                        break;
                    }
                case 5://链表的删除
                    {
                        break;
                    }
                case 6://直接查找
                    {
                        break;
                    }
                case 7://二分查找
                    {
                        break;
                    }
                default:
                    break;
            }
        }
        public void SetData(string str)
        {

        }
        public void ReStart()
        {

        }
    }
}
;