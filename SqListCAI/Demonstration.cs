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
                    drawSqlistInsert();
                    break;
                case 2://顺序表的删除
                    drawSqlistDelete();
                    break;
                case 3://链表的创建
                    break;  
                case 4://链表的插入
                    drawLinkedInsert();
                    break;
                case 5://链表的删除,初始时跟链表的插入一样
                    drawLinkedDelete();
                    break;
                case 6://直接查找
                    drawOrderSearch();
                    break;
                case 7://二分查找
                    drawBinarySearch();
                    break;
                default:
                    break;
            }
        }

        private void drawBinarySearch()
        {
            int margin_left = 20;
            int margin_top = 90;
            int length = Search.length;
            Rectangle[] rc = new Rectangle[length + 1];//矩形,第一个画查找的数据
            Label[] lable = new Label[length + 1];//标签，放原始内容，第一个画查找的数据
            index = new int[rc.Length];
            for (int i = 0; i < rc.Length; i++)//初始化需要画的数组矩形和标签元素的相同属性
            {
                rc[i] = new Rectangle();
                lable[i] = new Label();
                init_recKey_labelKey(rc[i], lable[i]);
            }
            //绘制需要查找的数据
            rc[0].Fill = Brushes.DarkGray;
            rc[0].Margin = new Thickness(margin_left, margin_top, 0, 0);
            lable[0].Content = "?";
            lable[0].Margin = new Thickness(margin_left + (50 - 25) / 2, margin_top, 0, 0);
            m_maindemon.canse_demon.Children.Add(rc[0]);
            m_maindemon.canse_demon.Children.Add(lable[0]);
            //绘制原始数据
            for (int i = 1; i < rc.Length; i++)
            {
                double rc_margin_left = i * rc[i].Width + margin_left;
                Thickness rc_src_margin = new Thickness(rc_margin_left, margin_top, 0, 0);
                rc[i].Margin = rc_src_margin;

                lable[i].Content = Search.srcData_BinSearch[i - 1];
                double lable_margin_left = rc_margin_left + (rc[i].Width - lable[i].Width) / 2;
                Thickness lable_src_margin = new Thickness(lable_margin_left, margin_top, 0, 0);
                lable[i].Margin = lable_src_margin;

                m_maindemon.canse_demon.Children.Add(rc[i]);
                m_maindemon.canse_demon.Children.Add(lable[i]);
            }
            //画比较动画需要的组件（上方位置矩形、label）        
            Rectangle rec_key = new Rectangle();
            Label label_key = new Label();
            rec_key.Fill = Brushes.White;
            rec_key.Stroke = Brushes.White;
            init_recKey_labelKey(rec_key,label_key);
            m_maindemon.canse_demon.Children.Add(rec_key);
            m_maindemon.canse_demon.Children.Add(label_key);
        }
        private void init_recKey_labelKey(Rectangle rec_key, Label label_key)
        {
            rec_key.Stroke = Brushes.Yellow;
            rec_key.Fill = Brushes.SkyBlue;
            rec_key.Width = 50;
            rec_key.Height = 35;

            label_key.Width = 25;
            label_key.Height = 35;
            label_key.FontSize = 15;
            label_key.Foreground = Brushes.Black;
            label_key.VerticalContentAlignment = VerticalAlignment.Center;
        }
        private void drawOrderSearch()
        {
            int margin_left = 20;
            int margin_top = 90;
            int length = Search.length;
            Rectangle[] rc = new Rectangle[length + 1];//矩形,第一个画查找的数据
            Label[] lable = new Label[length + 1];//标签，放原始内容，第一个画查找的数据
            index = new int[rc.Length];
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
                lable[i].Foreground = Brushes.Black;
                lable[i].VerticalContentAlignment = VerticalAlignment.Center;
            }
            //绘制需要查找的数据
            rc[0].Fill = Brushes.DarkGray;
            rc[0].Margin = new Thickness(margin_left, margin_top, 0, 0);
            lable[0].Content = "?";
            lable[0].Margin = new Thickness(margin_left + (50 - 25) / 2, margin_top, 0, 0);
            m_maindemon.canse_demon.Children.Add(rc[0]);
            m_maindemon.canse_demon.Children.Add(lable[0]);
            //绘制原始数据
            for (int i = 1; i < rc.Length; i++)
            {
                double rc_margin_left = i * rc[i].Width + margin_left;
                Thickness rc_src_margin = new Thickness(rc_margin_left, margin_top, 0, 0);
                rc[i].Margin = rc_src_margin;

                lable[i].Content = Search.srcData_OrderSearch[i-1];
                double lable_margin_left = rc_margin_left + (rc[i].Width - lable[i].Width) / 2;
                Thickness lable_src_margin = new Thickness(lable_margin_left, margin_top, 0, 0);
                lable[i].Margin = lable_src_margin;

                m_maindemon.canse_demon.Children.Add(rc[i]);
                m_maindemon.canse_demon.Children.Add(lable[i]);
            }
        }

        private void drawLinkedDelete()
        {
            double canse_left_1 = 20;//头结点距离canse left的距离
            double canse_top = 120;//rec_node、label_data距离canse top的距离
            double canse_top_label_node = 98;//label_node距离canse top的距离
            Label[] label_node = new Label[LinkedList.length + 1];
            Rectangle[] rec_node = new Rectangle[LinkedList.length + 1];
            Label[] label_data = new Label[LinkedList.length + 1];
            Line[] line1 = new Line[LinkedList.length + 1];//直线
            Line[] line2 = new Line[LinkedList.length + 1];//下方直线
            Line[] line3 = new Line[LinkedList.length + 1];//上方直线
            for (int i = 0; i < LinkedList.length + 1; i++)//初始化所有组件（一个节点label、一个矩形、一个数据label，三条线）
            {
                label_node[i] = new Label();
                m_maindemon.init_labe_node(label_node[i]);
                rec_node[i] = new Rectangle();
                label_data[i] = new Label();
                m_maindemon.init_rec_lab_node_data(rec_node[i], label_data[i]);
                line1[i] = new Line();
                line2[i] = new Line();
                line3[i] = new Line();
                line1[i].Stroke = Brushes.Black;
                line2[i].Stroke = Brushes.Black;
                line3[i].Stroke = Brushes.Black;
            }
            //画头结点
            label_node[0].Content = "La";
            label_node[0].Foreground = Brushes.Red;
            label_node[0].Margin = new Thickness(canse_left_1, canse_top_label_node, 0, 0);
            m_maindemon.canse_demon.Children.Add(label_node[0]);

            rec_node[0].Margin = new Thickness(canse_left_1, canse_top, 0, 0);
            label_data[0].Content = "";
            label_data[0].Margin = new Thickness(canse_left_1, canse_top, 0, 0);
            m_maindemon.canse_demon.Children.Add(rec_node[0]);
            m_maindemon.canse_demon.Children.Add(label_data[0]);
            //画新的结点，（一个节点label、一个矩形、一个数据label，三条线）
            for (int i = 1; i < LinkedList.length + 1; i++)
            {
                double canse_left = canse_left_1 + i * 2 * 60;
                //结点label
                label_node[i].Content = "";
                label_node[i].Background = Brushes.Green;
                label_node[i].Margin = new Thickness(canse_left, canse_top_label_node, 0, 0);
                m_maindemon.canse_demon.Children.Add(label_node[i]);

                //矩形、数据label
                rec_node[i].Margin = new Thickness(canse_left, canse_top, 0, 0);
                rec_node[i].Fill = Brushes.SkyBlue;
                label_data[i].Content = LinkedList.srcData[i - 1];
                label_data[i].Margin = new Thickness(canse_left, canse_top, 0, 0);
                m_maindemon.canse_demon.Children.Add(rec_node[i]);
                m_maindemon.canse_demon.Children.Add(label_data[i]);
                //三条线
                line1[i].X1 = canse_left_1 + (2 * i - 1) * 60 - 10; line1[i].X2 = line1[i].X1 + 70;
                line1[i].Y1 = 145; line1[i].Y2 = 145;
                line2[i].X1 = line1[i].X2; line2[i].X2 = line2[i].X1 - 10;
                line2[i].Y1 = 145; line2[i].Y2 = line2[i].Y1 + 10;
                line3[i].X1 = line1[i].X2; line3[i].X2 = line3[i].X1 - 10;
                line3[i].Y1 = 145; line3[i].Y2 = line3[i].Y1 - 10;
                m_maindemon.canse_demon.Children.Add(line1[i]);
                m_maindemon.canse_demon.Children.Add(line2[i]);
                m_maindemon.canse_demon.Children.Add(line3[i]);
            }
            //画最后的NULL 用'^'表示
            Label lab_over = new Label();
            lab_over.Content = "^";
            lab_over.Foreground = Brushes.Red;
            lab_over.HorizontalContentAlignment = HorizontalAlignment.Center;
            lab_over.VerticalContentAlignment = VerticalAlignment.Center;
            lab_over.FontSize = 15;
            lab_over.Width = 60 - 40;
            lab_over.Height = 50;
            lab_over.Background = Brushes.SkyBlue;
            double canse_left_over = canse_left_1 + LinkedList.length * 2 * 60 + 40;
            lab_over.Margin = new Thickness(canse_left_over, canse_top, 0, 0);
            m_maindemon.canse_demon.Children.Add(lab_over);
        }

        private void drawLinkedInsert()
        {
            double canse_left_1 = 20;//头结点距离canse left的距离
            double canse_top = 120;//rec_node、label_data距离canse top的距离
            double canse_top_label_node = 98;//label_node距离canse top的距离
            Label[] label_node = new Label[LinkedList.length + 1];
            Rectangle[] rec_node = new Rectangle[LinkedList.length + 1];
            Label[] label_data = new Label[LinkedList.length + 1];
            Line[] line1 = new Line[LinkedList.length + 1];//直线
            Line[] line2 = new Line[LinkedList.length + 1];//下方直线
            Line[] line3 = new Line[LinkedList.length + 1];//上方直线
            for (int i = 0; i < LinkedList.length + 1; i++)//初始化所有组件（一个节点label、一个矩形、一个数据label，三条线）
            {
                label_node[i] = new Label();
                m_maindemon.init_labe_node(label_node[i]);
                rec_node[i] = new Rectangle();
                label_data[i] = new Label();
                m_maindemon.init_rec_lab_node_data(rec_node[i], label_data[i]);
                line1[i] = new Line();
                line2[i] = new Line();
                line3[i] = new Line();
                line1[i].Stroke = Brushes.Black;
                line2[i].Stroke = Brushes.Black;
                line3[i].Stroke = Brushes.Black;
            }
            //画头结点
            label_node[0].Content = "La";
            label_node[0].Foreground = Brushes.Red;
            label_node[0].Margin = new Thickness(canse_left_1, canse_top_label_node, 0, 0);
            m_maindemon.canse_demon.Children.Add(label_node[0]);

            rec_node[0].Margin = new Thickness(canse_left_1, canse_top, 0, 0);
            label_data[0].Content = "";
            label_data[0].Margin = new Thickness(canse_left_1, canse_top, 0, 0);
            m_maindemon.canse_demon.Children.Add(rec_node[0]);
            m_maindemon.canse_demon.Children.Add(label_data[0]);
            //画新的结点，（一个节点label、一个矩形、一个数据label，三条线）
            for (int i = 1; i < LinkedList.length + 1; i++)
            {
                double canse_left = canse_left_1 + i * 2 * 60;
                //结点label
                label_node[i].Content = "";
                label_node[i].Background = Brushes.Green;
                label_node[i].Margin = new Thickness(canse_left, canse_top_label_node, 0, 0);
                m_maindemon.canse_demon.Children.Add(label_node[i]);

                //矩形、数据label
                rec_node[i].Margin = new Thickness(canse_left, canse_top, 0, 0);
                rec_node[i].Fill = Brushes.SkyBlue;
                label_data[i].Content = LinkedList.srcData[i - 1];
                label_data[i].Margin = new Thickness(canse_left, canse_top, 0, 0);
                m_maindemon.canse_demon.Children.Add(rec_node[i]);
                m_maindemon.canse_demon.Children.Add(label_data[i]);
                //三条线
                line1[i].X1 = canse_left_1 + (2 * i - 1) * 60 - 10; line1[i].X2 = line1[i].X1 + 70;
                line1[i].Y1 = 145; line1[i].Y2 = 145;
                line2[i].X1 = line1[i].X2; line2[i].X2 = line2[i].X1 - 10;
                line2[i].Y1 = 145; line2[i].Y2 = line2[i].Y1 + 10;
                line3[i].X1 = line1[i].X2; line3[i].X2 = line3[i].X1 - 10;
                line3[i].Y1 = 145; line3[i].Y2 = line3[i].Y1 - 10;
                m_maindemon.canse_demon.Children.Add(line1[i]);
                m_maindemon.canse_demon.Children.Add(line2[i]);
                m_maindemon.canse_demon.Children.Add(line3[i]);
            }
            //画最后的NULL 用'^'表示
            Label lab_over = new Label();
            lab_over.Content = "^";
            lab_over.Foreground = Brushes.Red;
            lab_over.HorizontalContentAlignment = HorizontalAlignment.Center;
            lab_over.VerticalContentAlignment = VerticalAlignment.Center;
            lab_over.FontSize = 15;
            lab_over.Width = 60 - 40;
            lab_over.Height = 50;
            lab_over.Background = Brushes.SkyBlue;
            double canse_left_over = canse_left_1 + LinkedList.length * 2 * 60 + 40;
            lab_over.Margin = new Thickness(canse_left_over, canse_top, 0, 0);
            m_maindemon.canse_demon.Children.Add(lab_over);
        }

        private void drawSqlistDelete()
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
        }

        private void drawSqlistInsert()
        {
            int margin_left = 20;
            int margin_top = 90;
            double ins_margin_left = 0.0d;
            int length = SqList.length;
            Rectangle[] rc = new Rectangle[length + 2];//矩形
            Label[] lable = new Label[length + 2];//标签，放原始内容
            index = new int[rc.Length];
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

                if (i == length)
                    lable[i].Content = '?';
                else
                    lable[i].Content = SqList.srcData_ins[i];
                double lable_margin_left = rc_margin_left + (rc[i].Width - lable[i].Width) / 2;
                Thickness lable_src_margin = new Thickness(lable_margin_left, margin_top, 0, 0);
                lable[i].Margin = lable_src_margin;

                m_maindemon.canse_demon.Children.Add(rc[i]);
                m_maindemon.canse_demon.Children.Add(lable[i]);
                index[i] = m_maindemon.canse_demon.Children.IndexOf(rc[i]);//存储矩形在canse_demon中的位置
                                                                           //相应lable位置在其后
                                                                           //保存插入位置的矩形
                if (SqList.insPosition == (i + 1))
                {
                    ins_margin_left = rc_margin_left;
                }
            }
            //绘制插入数据
            rc[rc.Length - 1].Margin = new Thickness(ins_margin_left, margin_top - 60, 0, 0);
            rc[rc.Length - 1].Fill = Brushes.DarkGray;
            lable[rc.Length - 1].Content = SqList.insertData;
            lable[rc.Length - 1].Foreground = Brushes.Red;
            lable[rc.Length - 1].Margin = new Thickness(ins_margin_left + (rc[rc.Length - 1].Width - lable[lable.Length - 1].Width) / 2, margin_top - 60, 0, 0);

            m_maindemon.canse_demon.Children.Add(rc[rc.Length - 1]);
            m_maindemon.canse_demon.Children.Add(lable[lable.Length - 1]);
            index[index.Length - 1] = m_maindemon.canse_demon.Children.IndexOf(rc[rc.Length - 1]);//存储插入数据矩形位置
        }

        public static DataTable data_ins = new DataTable("DataTable_Ins");
        public static DataTable data_del = new DataTable("DataTable_Del");
        public static DataTable data_linkCre = new DataTable("DataTable_LinkCre");
        public static DataTable data_linkIns = new DataTable("DataTable_LinkIns");
        public static DataTable data_linkDel = new DataTable("DataTable_LinkDel"); 
        public static DataTable data_orderSearch = new DataTable("DataTable_OrderSea"); 
        public static DataTable data_binarySearch = new DataTable("DataTable_BinarySea");
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
                    if(data_linkCre.Columns.Count == 0)
                    {
                        data_linkCre.Columns.Add(new DataColumn("NAME", typeof(string)));//第一列
                        data_linkCre.Columns.Add(new DataColumn("VALUE", typeof(string)));//第二列
                    }
                    m_maindemon.listView_value.DataContext = GetDataTable_LinkCre(LinkedListCodes.CREATE_VALUE, 4, null, 0).DefaultView;
                    break;
                case 4:
                    if (data_linkIns.Columns.Count == 0)
                    {
                        data_linkIns.Columns.Add(new DataColumn("NAME", typeof(string)));//第一列
                        data_linkIns.Columns.Add(new DataColumn("VALUE", typeof(string)));//第二列
                    }
                    m_maindemon.listView_value.DataContext = GetDataTable_LinkIns(LinkedListCodes.INSERT_VALUE, 4, null, 0).DefaultView;
                    break;
                case 5:
                    if (data_linkDel.Columns.Count == 0)
                    {
                        data_linkDel.Columns.Add(new DataColumn("NAME", typeof(string)));//第一列
                        data_linkDel.Columns.Add(new DataColumn("VALUE", typeof(string)));//第二列
                    }
                    m_maindemon.listView_value.DataContext = GetDataTable_LinkDel(LinkedListCodes.DELETE_VALUE, 4, null, 0).DefaultView;
                    break;
                case 6:
                    if (data_orderSearch.Columns.Count == 0)
                    {
                        data_orderSearch.Columns.Add(new DataColumn("NAME", typeof(string)));//第一列
                        data_orderSearch.Columns.Add(new DataColumn("VALUE", typeof(string)));//第二列
                    }
                    m_maindemon.listView_value.DataContext = GetDataTable_OrderSea(SearchCodes.ORDER_SEARCH_VALUE, 4, null, 0).DefaultView;
                    break;
                case 7:
                    if (data_binarySearch.Columns.Count == 0)
                    {
                        data_binarySearch.Columns.Add(new DataColumn("NAME", typeof(string)));//第一列
                        data_binarySearch.Columns.Add(new DataColumn("VALUE", typeof(string)));//第二列
                    }
                    m_maindemon.listView_value.DataContext = GetDataTable_BinarySea(SearchCodes.BINARY_SEARCH_VALUE, 4, null, 0).DefaultView;
                    break;
                default:
                    break;
            }
        }

        public DataTable GetDataTable_BinarySea(string[] str, int updateFlag, string updateValue, int movePositio)
        {
            if(updateFlag == 0)//修改low
            {
                data_binarySearch.Rows[Search.length + 1]["VALUE"] = updateValue;
                m_maindemon.listView_value.SelectedIndex = Search.length + 1;
            }
            if(updateFlag == 1)//修改mid
            {
                data_binarySearch.Rows[Search.length + 2]["VALUE"] = updateValue;
                m_maindemon.listView_value.SelectedIndex = Search.length + 2;
            }
            if(updateFlag == 2)//修改high
            {
                data_binarySearch.Rows[Search.length + 3]["VALUE"] = updateValue;
                m_maindemon.listView_value.SelectedIndex = Search.length + 3;

            }
            if (updateFlag == 4)
            {
                //添加源数据
                for (int i = 0; i < Search.length; i++)//srcData
                {
                    string name = str[0] + "[" + i + "]";
                    string value = Search.srcData_BinSearch[i] + "";
                    data_binarySearch.Rows.Add(name, value);
                }
                data_binarySearch.Rows.Add(str[1], Search.searchData + "");//searchData
                data_binarySearch.Rows.Add(str[2], "未知");//low
                data_binarySearch.Rows.Add(str[3], "未知");//mid
                data_binarySearch.Rows.Add(str[4], "未知");//high
                data_binarySearch.Rows.Add(str[5], Search.length);
            }
            return data_binarySearch;
        }

        public DataTable GetDataTable_OrderSea(string[] str, int updateFlag, string updateValue, int movePosition)
        {
            if(updateFlag ==0)//修改索引值
            {
                data_orderSearch.Rows[Search.length + 1]["VALUE"] = updateValue;
                m_maindemon.listView_value.SelectedIndex = Search.length + 1;
            }
            if (updateFlag == 4)
            {
                //添加源数据
                for (int i = 0; i < Search.length; i++)//srcData
                {
                    string name = str[0] + "[" + i + "]";
                    string value = Search.srcData_OrderSearch[i]+"";
                    data_orderSearch.Rows.Add(name,value);
                }
                data_orderSearch.Rows.Add(str[1], Search.searchData+"");//searchData
                data_orderSearch.Rows.Add(str[2], "未知");
                data_orderSearch.Rows.Add(str[3], Search.length);
            }
            return data_orderSearch;
        }

        public DataTable GetDataTable_LinkDel(string[] str, int updateFlag, string updateValue, int movePosition)
        {
            if (updateFlag == 0)//修改P的当前指向，最开始指向头结点
            {
                data_linkDel.Rows[LinkedList.length + 2]["VALUE"] = updateValue;
                m_maindemon.listView_value.SelectedIndex = LinkedList.length + 2;
            }
            if (updateFlag == 1)//修改j变量值
            {
                data_linkDel.Rows[LinkedList.length + 3]["VALUE"] = updateValue;
                m_maindemon.listView_value.SelectedIndex = LinkedList.length + 3;
            }
            if (updateFlag == 2)//修改删除结点Q
            {
                data_linkDel.Rows[LinkedList.length + 6]["VALUE"] = updateValue;
                m_maindemon.listView_value.SelectedIndex = LinkedList.length + 6;
            }
            if (updateFlag == 3)//修改删除元素返回值
            {
                data_linkDel.Rows[LinkedList.length + 5]["VALUE"] = updateValue;
                m_maindemon.listView_value.SelectedIndex = LinkedList.length + 5;
            }
            if (updateFlag == 4)//显示最开始数据
            {
                data_linkDel.Rows.Add(str[0], "指向头结点");    //添加头结点
                int length = LinkedList.length;
                for (int i = 0; i < length; i++)          //添加源数据
                {
                    string name = str[1] + "[" + i + "]";
                    string value = value = LinkedList.srcData[i] + "";
                    data_linkDel.Rows.Add(name, value);
                }
                data_linkDel.Rows.Add(str[2], length);      //添加长度
                data_linkDel.Rows.Add(str[3], "当前指向？");//删除结点的前一个位置
                data_linkDel.Rows.Add(str[4], "未知");      //添加索引变量j
                data_linkDel.Rows.Add(str[5], LinkedList.deletePosition); //删除位置i
                data_linkDel.Rows.Add(str[6], "未知");      //返回删除元素的值
                data_linkDel.Rows.Add(str[7], "删除结点？"); //作为删除的结点的指标
            }

            return data_linkDel;
        }

        public DataTable GetDataTable_LinkIns(string[] str, int updateFlag, string updateValue, int movePosition)
        {
            if(updateFlag == 0)//修改P的当前指向，最开始指向头结点
            {
                data_linkIns.Rows[LinkedList.length+2]["VALUE"] = updateValue;
                m_maindemon.listView_value.SelectedIndex = LinkedList.length + 2;
            }
            if(updateFlag == 1)//修改j变量值
            {
                data_linkIns.Rows[LinkedList.length + 3]["VALUE"] = updateValue;
                m_maindemon.listView_value.SelectedIndex = LinkedList.length + 3;
            }
            if (updateFlag == 2)//修改新结点S
            {
                data_linkIns.Rows[LinkedList.length + 6]["VALUE"] = updateValue;
                m_maindemon.listView_value.SelectedIndex = LinkedList.length + 6;
            }
            if (updateFlag == 4)//显示最开始数据
            {
                data_linkIns.Rows.Add(str[0], "指向头结点");    //添加头结点
                int length = LinkedList.length;
                for (int i = 0; i < length; i++)          //添加源数据
                {
                    string name = str[1] + "[" + i + "]";
                    string value = value = LinkedList.srcData[i] + "";
                    data_linkIns.Rows.Add(name, value);
                }
                data_linkIns.Rows.Add(str[2], length);      //添加长度
                data_linkIns.Rows.Add(str[3], "当前指向？");//添加指向值为？的结点（是当前插入的结点）
                data_linkIns.Rows.Add(str[4], "未知");      //添加索引变量j
                data_linkIns.Rows.Add(str[5], LinkedList.insertPosition);      //插入位置i
                data_linkIns.Rows.Add(str[6], LinkedList.insertData);      //插入元素
                data_linkIns.Rows.Add(str[7], "插入结点？");      //插入元素
            }

            return data_linkIns;
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
                case 4://链表的插入
                    {
                        Demonstration.data_linkCre.Clear();
                        ListDialog linkInsWindow = new ListDialog(flag);
                        //订阅事件
                        linkInsWindow.PassValuesEvent += new ListDialog.PassValuesHandler(RecieveLinkIns);
                        linkInsWindow.ShowDialog();
                        break;
                    }
                case 5://链表的删除
                    {
                        Demonstration.data_linkCre.Clear();
                        ListDialog linkDelWindow = new ListDialog(flag);
                        //订阅事件
                        linkDelWindow.PassValuesEvent += new ListDialog.PassValuesHandler(RecieveLinkDel);
                        linkDelWindow.ShowDialog();
                        break;
                    }
                case 6://顺序查找
                    {
                        Demonstration.data_orderSearch.Clear();
                        ListDialog OrderSearchWindow = new ListDialog(flag);
                        //订阅事件
                        OrderSearchWindow.PassValuesEvent += new ListDialog.PassValuesHandler(RecieveOrderSearch);
                        OrderSearchWindow.ShowDialog();
                        break;
                    }
                case 7://折半查找
                    {
                        Demonstration.data_orderSearch.Clear();
                        ListDialog BinarySearchWindow = new ListDialog(flag);
                        //订阅事件
                        BinarySearchWindow.PassValuesEvent += new ListDialog.PassValuesHandler(RecieveBinarySearch);
                        BinarySearchWindow.ShowDialog();
                        break;
                    }
                default:
                    break;

            }
        }

        private void RecieveBinarySearch(object sender, PassValuesEventArgs e)
        {
            m_maindemon.srcData_Search = e.srcData;
            m_maindemon.searchData = e.searchData;
            m_maindemon.initUI(flag);
        }

        private void RecieveOrderSearch(object sender, PassValuesEventArgs e)
        {
            m_maindemon.srcData_Search = e.srcData;
            m_maindemon.searchData = e.searchData;
            m_maindemon.initUI(flag);
        }

        private void RecieveLinkDel(object sender, PassValuesEventArgs e)
        {
            m_maindemon.srcData_LinkedDel = e.srcData;
            m_maindemon.position_LinkedDel = e.position;
            m_maindemon.initUI(flag);
        }

        private void RecieveLinkIns(object sender, PassValuesEventArgs e)
        {
            m_maindemon.srcData_LinkedIns = e.srcData;
            m_maindemon.insertData_ins = e.insertData;
            m_maindemon.position_LinkedIns = e.position;
            m_maindemon.initUI(flag);
        }

        private void RecieveLinkCre(object sender, PassValuesEventArgs e)
        {
            m_maindemon.srcData_linkedCre = e.srcData;
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
                    }
                case 4://链表的插入
                    {
                        MessageBox.Show(Explain.LinkedInsExplain, "链表插入", MessageBoxButton.OK);
                        break;
                    }
                case 5://链表的删除
                    {
                        MessageBox.Show(Explain.LinkedDelExplain, "链表删除", MessageBoxButton.OK);
                        break;
                    }
                case 6://顺序查找
                    {
                        MessageBox.Show(Explain.OrderSearchExplain, "顺序查找", MessageBoxButton.OK);
                        break;
                    }
                case 7://二分查找
                    {
                        MessageBox.Show(Explain.BinarySearchExplain, "二分查找", MessageBoxButton.OK);
                        break;
                    }
                default:
                    break;
            }
        }
    }
}
;