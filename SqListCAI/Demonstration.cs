using SqListCAI.Pages.MainPage;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows;
using System.Data;
using SqListCAI.Entities;
using SqListCAI.Dialogs;
using SqListCAI.Events;
using System;
using SqListCAI.Utils.SourceCodes;

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
            {
                this.m_maindemon.listBox_currentRow.Items.Insert(i, i);
                this.m_maindemon.listBox_code.Items.Insert(i, str[i]);
            }
                
            //修改ListBox中某行，先移除，然后插入
            //TextBlock tb = new TextBlock();
            //tb.Text = str[1];
            //tb.Foreground = Brushes.Blue;
            //this.m_maindemon.listBox_code.Items.RemoveAt(0);
            //this.m_maindemon.listBox_code.Items.Insert(0, tb);
            //this.m_maindemon.listBox_code.Items.Add("1234567887946516316");//在末尾增加一行显示

        }
        /// <summary>
        /// 显示当前动画
        /// </summary>
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
                        for(int i=0;i<rc.Length;i++)//初始化需要画的数组矩形和标签元素的相同属性
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
                        for (int i=0;i< rc.Length - 1; i++)
                        {
                            double rc_margin_left = i * rc[i].Width + margin_left;
                            Thickness rc_src_margin = new Thickness(rc_margin_left, margin_top, 0, 0);
                            rc[i].Margin = rc_src_margin;

                            if(i==length)
                                lable[i].Content = '?';
                            else
                                lable[i].Content = SqList.srcData_ins[i];
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
                        rc[rc.Length - 1].Fill = Brushes.DarkGray;
                        lable[rc.Length-1].Content = SqList.insertData;
                        lable[rc.Length - 1].Foreground = Brushes.Red;
                        lable[rc.Length-1].Margin = new Thickness(ins_margin_left + (rc[rc.Length-1].Width - lable[lable.Length-1].Width) / 2, margin_top - 60, 0, 0);
                        
                        m_maindemon.canse_demon.Children.Add(rc[rc.Length-1]);
                        m_maindemon.canse_demon.Children.Add(lable[lable.Length-1]);
                        index[index.Length-1] = m_maindemon.canse_demon.Children.IndexOf(rc[rc.Length-1]);//存储插入数据矩形位置
                        break;
                    }
                case 2://顺序表的删除
                    {
                        int margin_left = 20;
                        int margin_top = 90;
                        double ins_margin_left = 0.0d;
                        int length = SqList.length;
                        Rectangle[] rc = new Rectangle[length + 1];//矩形
                        Label[] lable = new Label[length + 1];//标签，放原始内容
                        index = new int[rc.Length];//存储矩形在canse中的索引
                        for (int i = 0; i < rc.Length; i++)//初始化需要画的数组矩形和标签元素的相同属性
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
                        for (int i = 0; i < rc.Length - 1; i++)
                        {
                            double rc_margin_left = i * rc[i].Width + margin_left;
                            Thickness rc_src_margin = new Thickness(rc_margin_left, margin_top, 0, 0);
                            rc[i].Margin = rc_src_margin;
                            
                            lable[i].Content = SqList.srcData_del[i];
                            double lable_margin_left = rc_margin_left + (rc[i].Width - lable[i].Width) / 2;
                            Thickness lable_src_margin = new Thickness(lable_margin_left, margin_top, 0, 0);
                            lable[i].Margin = lable_src_margin;

                            m_maindemon.canse_demon.Children.Add(rc[i]);
                            m_maindemon.canse_demon.Children.Add(lable[i]);

                            index[i] = m_maindemon.canse_demon.Children.IndexOf(rc[i]);//存储矩形在canse_demon中的位置
                                                                                       //相应lable位置在其后
                                                                                       //保存插入位置的矩形
                            if (SqList.delPosition == (i + 1))
                            {
                                ins_margin_left = rc_margin_left;
                            }
                        }
                        //绘制删除位置
                        rc[rc.Length - 1].Margin = new Thickness(ins_margin_left, margin_top - 60, 0, 0);
                        rc[rc.Length - 1].Fill = Brushes.DarkGray;
                        lable[rc.Length - 1].Content = SqList.delPosition;
                        lable[rc.Length - 1].Foreground = Brushes.Red;
                        lable[rc.Length - 1].Margin = new Thickness(ins_margin_left + (rc[rc.Length - 1].Width - lable[lable.Length - 1].Width) / 2, margin_top - 60, 0, 0);

                        m_maindemon.canse_demon.Children.Add(rc[rc.Length - 1]);
                        m_maindemon.canse_demon.Children.Add(lable[lable.Length - 1]);
                        index[index.Length - 1] = m_maindemon.canse_demon.Children.IndexOf(rc[rc.Length - 1]);//存储插入数据矩形位置
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


        public static DataTable data_ins = new DataTable("DataTable_Ins");
        public static DataTable data_del = new DataTable("DataTable_Del");
        public static DataTable data_linkCre = new DataTable("DataTable_LinkCre");
        /// <summary>
        /// 显示变量区中的变量
        /// </summary>
        /// <param name="str"></param>
        public void ShowValue()
        {
            switch (flag)
            {
                case 1://顺序表的插入
                    if(data_ins.Columns.Count == 0)
                    {
                        data_ins.Columns.Add(new DataColumn("NAME", typeof(string)));//第一列
                        data_ins.Columns.Add(new DataColumn("VALUE", typeof(string)));//第二列
                    }
                    m_maindemon.listView_value.DataContext = GetDataTable_Ins(SqListCodes.INSERT_VALUE, 3,null,0).DefaultView;
                    break;
                case 2:
                    if(data_del.Columns.Count == 0)
                    {
                        data_del.Columns.Add(new DataColumn("NAME", typeof(string)));//第一列
                        data_del.Columns.Add(new DataColumn("VALUE", typeof(string)));//第二列
                    }
                    m_maindemon.listView_value.DataContext = GetDataTable_Del(SqListCodes.DELETE_VALUE, 4, null, 0).DefaultView;
                    break;
                case 3:
                    {
                        if(data_linkCre.Columns.Count == 0)
                        {
                            data_linkCre.Columns.Add(new DataColumn("NAME", typeof(string)));//第一列
                            data_linkCre.Columns.Add(new DataColumn("VALUE", typeof(string)));//第二列
                        }
                    }
                    m_maindemon.listView_value.DataContext = GetDataTable_LinkCre(LinkedListCodes.CREATE_VALUE, 4, null, 0).DefaultView;
                    break;
                default:
                    break;
            }
        }
        public DataTable GetDataTable_LinkCre(string[] str, int updateFlag, string updateValue, int movePosition)
        {
            if(updateFlag == 0)//改变La(指向头结点)
            {
                data_linkCre.Rows[0]["VALUE"] = updateValue;
                m_maindemon.listView_value.SelectedIndex = 0;
            }
            if(updateFlag == 1)//改变p的指向
            {
                data_linkCre.Rows[LinkedList.length + 2]["VALUE"] = "当前指向值为"+updateValue+"的结点";
                m_maindemon.listView_value.SelectedIndex = LinkedList.length + 2;
            }
            if(updateFlag == 2)//改变索引值i
            {
                data_linkCre.Rows[LinkedList.length + 3]["VALUE"] = updateValue;
                m_maindemon.listView_value.SelectedIndex = LinkedList.length + 3;
            }
            if (updateFlag == 4)//显示最开始的数据
            {
                data_linkCre.Rows.Add(str[0], "未知");    //添加头结点
                int length = LinkedList.length;
                for (int i = 0; i < length; i++)          //添加源数据
                {
                    string name = str[1] + "[" + i + "]";
                    string value = value = LinkedList.srcData[i] + "";
                    data_linkCre.Rows.Add(name, value);
                }
                data_linkCre.Rows.Add(str[2], length);     //添加长度
                data_linkCre.Rows.Add(str[3], "当前指向？");//添加指向值为？的结点（是当前插入的结点）
                data_linkCre.Rows.Add(str[4], "未知");     //添加索引变量
            }
            return data_linkCre;
        }

        public DataTable GetDataTable_Del(string[] str, int updateFlag, string updateValue, int movePosition)
        {
            if(updateFlag == 0)//返回值
            {
                data_del.Rows[SqList.length + 2]["VALUE"] = updateValue;
                m_maindemon.listView_value.SelectedIndex = SqList.length + 2;
            }
            if(updateFlag == 1)//左移
            {
                data_del.Rows[movePosition-1]["VALUE"] = updateValue;
                m_maindemon.listView_value.SelectedIndex = movePosition-1;
            }
            if(updateFlag == 2)//p值
            {
                data_del.Rows[SqList.length + 4]["VALUE"] = updateValue;
                m_maindemon.listView_value.SelectedIndex = SqList.length + 4;
            }
            if(updateFlag == 3)//长度
            {
                data_del.Rows[SqList.length]["VALUE"] = updateValue;
                m_maindemon.listView_value.SelectedIndex = SqList.length;

                data_del.Rows[SqList.length]["VALUE"] = "已删除";
            }
            if (updateFlag == 4)//显示最开始的数据
            {
                int length = SqList.length;
                for (int i = 0; i < length; i++)//添加源数据
                {
                    string name = str[0] + ".srcData[" + i + "]";
                    string value = value = SqList.srcData_del[i] + "";
                    data_del.Rows.Add(name, value);
                }
                data_del.Rows.Add(str[0] + ".length", SqList.length);//长度
                data_del.Rows.Add(str[1], SqList.delPosition);
                data_del.Rows.Add(str[2], "未知");
                data_del.Rows.Add(str[3], SqList.MAXSIZE);
                data_del.Rows.Add(str[4], "未知");
            }
            return data_del;
        }
        public DataTable GetDataTable_Ins(string[] str,int updateFlag,string updateValue,int movePosition)
        {
            //设置主键
            //ID.AutoIncrement = true; //自动递增ID号 
            //DataColumn[] keys = new DataColumn[1];
            //keys[0] = NAME;
            //data.PrimaryKey = keys;
            if(updateFlag == 0)//右移
            {
                data_ins.Rows[movePosition+1]["VALUE"] = updateValue;
                m_maindemon.listView_value.SelectedIndex = movePosition + 1;
            }
            if(updateFlag == 1)//修改P
            {
                data_ins.Rows[SqList.length+5]["VALUE"] = updateValue;
                m_maindemon.listView_value.SelectedIndex = SqList.length + 5;
            }
            if(updateFlag == 2)//修改长度
            {
                data_ins.Rows[SqList.length]["VALUE"] = updateValue;
                m_maindemon.listView_value.SelectedIndex = SqList.length;
            }
            if(updateFlag == 3)//显示最开始的数据
            {
                int length = SqList.length;
                for (int i = 0; i < length + 1; i++)//添加源数据
                {
                    string name = str[0] + ".srcData[" + i + "]";
                    string value = null;
                    if (i == length)
                        value = '?'.ToString();
                    else
                        value = SqList.srcData_ins[i] + "";
                    data_ins.Rows.Add(name, value);
                }
                data_ins.Rows.Add(str[0] + ".length", SqList.length);
                data_ins.Rows.Add(str[1], SqList.insPosition);
                data_ins.Rows.Add(str[2], SqList.insertData);
                data_ins.Rows.Add(str[3], SqList.MAXSIZE);
                data_ins.Rows.Add(str[4], "未知");
            }
            return data_ins;
        }
        public void SetData()
        {
            m_maindemon.canse_demon.Children.Clear();
            m_maindemon.listBox_currentRow.Items.Clear();
            m_maindemon.listBox_code.Items.Clear();
            m_maindemon.listView_value.DataContext = null;
            switch (flag)
            {
                case 1://顺序表插入
                    {
                        Demonstration.data_ins.Clear();
                        ListDialog insertWindow = new ListDialog(flag);
                        //订阅事件
                        insertWindow.PassValuesEvent += new ListDialog.PassValuesHandler(RecieveOrderInsert);
                        insertWindow.ShowDialog();
                        break;
                    }
                case 2://顺序表删除
                    {
                        Demonstration.data_del.Clear();
                        ListDialog deleteWindow = new ListDialog(flag);
                        //订阅事件
                        deleteWindow.PassValuesEvent += new ListDialog.PassValuesHandler(RecieveOrderDelete);
                        deleteWindow.ShowDialog();
                        break;
                    }
                case 3://链表的创建
                    {
                        Demonstration.data_linkCre.Clear();
                        ListDialog linkCreWindow = new ListDialog(flag);
                        //订阅事件
                        linkCreWindow.PassValuesEvent += new ListDialog.PassValuesHandler(RecieveLinkCre);
                        linkCreWindow.ShowDialog();
                        break;
                    }
                default:
                    break;

            }
        }

        private void RecieveLinkCre(object sender, PassValuesEventArgs e)
        {
            m_maindemon.srcData_linkedListCreate = e.srcData;
            m_maindemon.initUI(flag);
        }

        private void RecieveOrderDelete(object sender, PassValuesEventArgs e)
        {
            m_maindemon.srcData_del = e.srcData;
            m_maindemon.position_del = e.position;
            m_maindemon.initUI(flag);
        }

        private void RecieveOrderInsert(object sender, PassValuesEventArgs e)
        {
            m_maindemon.srcData_ins = e.srcData;
            m_maindemon.insertData_ins = e.insertData;
            m_maindemon.position_ins = e.position;
            m_maindemon.initUI(flag);
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
                        MessageBox.Show(Explain.LinkedCreExplain, "链表创建", MessageBoxButton.OK);
                        break;
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
    }
}
;