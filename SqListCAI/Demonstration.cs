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
        int flag;
        MainDemon m_maindemon;
        public Demonstration(int flag,MainDemon maindemon)
        {
            this.flag = flag;
            this.m_maindemon = maindemon;
        }
        public void ShowCode(string[] str)
        {
            if(flag == 10)//快速排序需要显示另一种代码
            {
                m_maindemon.listBox_currentRow.Items.Clear();
                m_maindemon.listBox_code.Items.Clear();
            }
            for (int i = 0; i < str.Length; i++)
            {
                this.m_maindemon.listBox_currentRow.Items.Insert(i, i);
                this.m_maindemon.listBox_code.Items.Insert(i, str[i]);
            }
        }
        /// <summary>
        /// 显示当前动画
        /// </summary>
        public void ShowDemon()
        {
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
                case 8://插入排序
                    drawSort();
                    break;
                case 9://冒泡排序
                    drawSort();
                    break;
                case 10://快速排序
                    drawSort();
                    break;
                default:
                    break;
            }
        }
        private void drawSort()
        {
            int margin_left = 20;
            int margin_top = 90;
            int length = Sort.length;
            Rectangle[] rc = new Rectangle[length];//矩形,第一个画查找的数据
            Label[] lable = new Label[length];//标签，放原始内容，第一个画查找的数据
            Label[] label_tabNum = new Label[length];
            for (int i = 0; i < rc.Length; i++)//初始化需要画的数组矩形和标签元素的相同属性
            {
                rc[i] = new Rectangle();
                lable[i] = new Label();
                label_tabNum[i] = new Label();
                init_recKey_labelKey(rc[i], lable[i], label_tabNum[i]);
            }
            //绘制原始数据
            for (int i = 0; i < rc.Length; i++)
            {
                double rc_margin_left = i * rc[i].Width + margin_left;
                Thickness rc_src_margin = new Thickness(rc_margin_left, margin_top, 0, 0);
                rc[i].Margin = rc_src_margin;

                lable[i].Content = Sort.srcData[i];
                double lable_margin_left = rc_margin_left + (rc[i].Width - lable[i].Width) / 2;
                Thickness lable_src_margin = new Thickness(lable_margin_left, margin_top, 0, 0);
                lable[i].Margin = lable_src_margin;

                label_tabNum[i].Content = "R["+i+"]";//60
                label_tabNum[i].Margin = new Thickness(rc_margin_left, 60, 0, 0);

                m_maindemon.canse_demon.Children.Add(rc[i]);
                m_maindemon.canse_demon.Children.Add(lable[i]);
                m_maindemon.canse_demon.Children.Add(label_tabNum[i]);
            }
        }

        private void drawBinarySearch()
        {
            int margin_left = 20;
            int margin_top = 90;
            int length = Search.length;
            Rectangle[] rc = new Rectangle[length + 1];//矩形,第一个画查找的数据
            Label[] lable = new Label[length + 1];//标签，放原始内容，第一个画查找的数据
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
            lable[0].Margin = new Thickness(margin_left + (rc[0].Width - lable[0].Width) / 2, margin_top, 0, 0);
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
            //画比较动画需要的组件（上方位置矩形、label组成的需要查找的元素）        
            Rectangle rec_key = new Rectangle();
            Label label_key = new Label();
            init_recKey_labelKey(rec_key, label_key);
            rec_key.Fill = Brushes.White;
            rec_key.Stroke = Brushes.White;
            m_maindemon.canse_demon.Children.Add(rec_key);
            m_maindemon.canse_demon.Children.Add(label_key);
        }

        private void init_recKey_labelKey(Rectangle rec_key, Label label_key,Label label_tabNum)
        {
            rec_key.Stroke = Brushes.Yellow;
            rec_key.Fill = Brushes.SkyBlue;
            rec_key.Width = 65;
            rec_key.Height = 50;

            label_key.Width = 45;
            label_key.Height = 50;
            label_key.FontSize = 25;
            label_key.Foreground = Brushes.Black;
            label_key.VerticalContentAlignment = VerticalAlignment.Center;
            label_key.HorizontalContentAlignment = HorizontalAlignment.Center;

            label_tabNum.Width = 65;
            label_tabNum.Height = 30;
            label_tabNum.FontSize = 15;
            label_tabNum.Foreground = Brushes.DarkBlue;
            label_tabNum.VerticalContentAlignment = VerticalAlignment.Center;
            label_tabNum.HorizontalContentAlignment = HorizontalAlignment.Center;
        }
        private void init_recKey_labelKey(Rectangle rec_key, Label label_key)
        {
            rec_key.Stroke = Brushes.Yellow;
            rec_key.Fill = Brushes.SkyBlue;
            rec_key.Width = 50;
            rec_key.Height = 60;

            label_key.Width = 45;
            label_key.Height = 60;
            label_key.FontSize = 25;
            label_key.Foreground = Brushes.Black;
            label_key.VerticalContentAlignment = VerticalAlignment.Center;
            label_key.HorizontalContentAlignment = HorizontalAlignment.Center;
        }
        private void drawOrderSearch()
        {
            int margin_left = 20;
            int margin_top = 90;
            int length = Search.length;
            Rectangle[] rc = new Rectangle[length + 1];//矩形,第一个画查找的数据
            Label[] lable = new Label[length + 1];//标签，放原始内容，第一个画查找的数据
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
            lable[0].Margin = new Thickness(margin_left + (rc[0].Width - lable[0].Width) / 2, margin_top, 0, 0);
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
            double canse_top_label_node = 90;//label_node距离canse top的距离
            Label[] label_node = new Label[LinkedList.srcData.Length + 1];
            Rectangle[] rec_node = new Rectangle[LinkedList.srcData.Length + 1];
            Label[] label_data = new Label[LinkedList.srcData.Length + 1];
            Line[] line1 = new Line[LinkedList.srcData.Length + 1];//直线
            Line[] line2 = new Line[LinkedList.srcData.Length + 1];//下方直线
            Line[] line3 = new Line[LinkedList.srcData.Length + 1];//上方直线
            for (int i = 0; i < LinkedList.srcData.Length + 1; i++)//初始化所有组件（一个节点label、一个矩形、一个数据label，三条线）
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
            label_node[0].Content = "L";
            label_node[0].Foreground = Brushes.Red;
            label_node[0].Margin = new Thickness(canse_left_1, canse_top_label_node, 0, 0);
            m_maindemon.canse_demon.Children.Add(label_node[0]);

            rec_node[0].Margin = new Thickness(canse_left_1, canse_top_label_node + label_node[0].Height, 0, 0);
            label_data[0].Content = "";
            label_data[0].Margin = rec_node[0].Margin;
            m_maindemon.canse_demon.Children.Add(rec_node[0]);
            m_maindemon.canse_demon.Children.Add(label_data[0]);
            //画新的结点，（一个节点label、一个矩形、一个数据label，三条线）
            for (int i = 1; i < LinkedList.srcData.Length + 1; i++)
            {
                double canse_left = canse_left_1 + i * 2 * 60;
                //结点label
                label_node[i].Content = "";
                label_node[i].Background = Brushes.Green;
                label_node[i].Margin = new Thickness(canse_left, canse_top_label_node, 0, 0);
                m_maindemon.canse_demon.Children.Add(label_node[i]);

                //矩形、数据label
                rec_node[i].Margin = new Thickness(canse_left, canse_top_label_node+25, 0, 0);
                rec_node[i].Fill = Brushes.SkyBlue;
                label_data[i].Content = LinkedList.srcData[i - 1];
                label_data[i].Margin = rec_node[i].Margin;
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
            double canse_left_over = canse_left_1 + LinkedList.srcData.Length * 2 * 60 + 40;
            lab_over.Margin = new Thickness(canse_left_over, canse_top_label_node+25, 0, 0);
            m_maindemon.canse_demon.Children.Add(lab_over);
        }

        private void drawLinkedInsert()
        {
            double canse_left_1 = 20;//头结点距离canse left的距离
            double canse_top_label_node = 90;//label_node距离canse top的距离
            Label[] label_node = new Label[LinkedList.srcData.Length + 1];
            Rectangle[] rec_node = new Rectangle[LinkedList.srcData.Length + 1];
            Label[] label_data = new Label[LinkedList.srcData.Length + 1];
            Line[] line1 = new Line[LinkedList.srcData.Length + 1];//直线
            Line[] line2 = new Line[LinkedList.srcData.Length + 1];//下方直线
            Line[] line3 = new Line[LinkedList.srcData.Length + 1];//上方直线
            for (int i = 0; i < LinkedList.srcData.Length + 1; i++)//初始化所有组件（一个节点label、一个矩形、一个数据label，三条线）
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
            label_node[0].Content = "L";
            label_node[0].Foreground = Brushes.Red;
            label_node[0].Margin = new Thickness(canse_left_1, canse_top_label_node, 0, 0);
            m_maindemon.canse_demon.Children.Add(label_node[0]);

            rec_node[0].Margin = new Thickness(canse_left_1, canse_top_label_node + label_node[0].Height, 0, 0);
            label_data[0].Content = "";
            label_data[0].Margin = rec_node[0].Margin;
            m_maindemon.canse_demon.Children.Add(rec_node[0]);
            m_maindemon.canse_demon.Children.Add(label_data[0]);
            //画新的结点，（一个节点label、一个矩形、一个数据label，三条线）
            for (int i = 1; i < LinkedList.srcData.Length + 1; i++)
            {
                double canse_left = canse_left_1 + i * 2 * 60;
                //结点label
                label_node[i].Content = "";
                label_node[i].Background = Brushes.Green;
                label_node[i].Margin = new Thickness(canse_left, canse_top_label_node, 0, 0);
                m_maindemon.canse_demon.Children.Add(label_node[i]);

                //矩形、数据label
                rec_node[i].Margin = new Thickness(canse_left, canse_top_label_node+label_node[i].Height, 0, 0);
                rec_node[i].Fill = Brushes.SkyBlue;
                label_data[i].Content = LinkedList.srcData[i - 1];
                label_data[i].Margin = rec_node[i].Margin;
                this.m_maindemon.canse_demon.Children.Add(rec_node[i]);
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
            double canse_left_over = canse_left_1 + LinkedList.srcData.Length * 2 * 60 + 40;
            lab_over.Margin = new Thickness(canse_left_over, canse_top_label_node+25, 0, 0);
            m_maindemon.canse_demon.Children.Add(lab_over);
        }

        private void drawSqlistDelete()
        {
            int margin_left = 20;
            int margin_top = 90;
            double del_margin_left = 0.0d;
            int length = SqList.length;
            Rectangle[] rc = new Rectangle[length + 1];//矩形
            Label[] lable = new Label[length + 1];//标签，放原始内容
            for (int i = 0; i < rc.Length; i++)//初始化需要画的数组矩形和标签元素的相同属性
            {
                rc[i] = new Rectangle();
                lable[i] = new Label();
                init_recKey_labelKey(rc[i], lable[i]);
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

                if (SqList.delPosition == (i + 1))
                {
                    del_margin_left = rc_margin_left;
                }
            }
            //绘制删除位置
            rc[rc.Length - 1].Margin = new Thickness(del_margin_left, margin_top - 80, 0, 0);
            rc[rc.Length - 1].Fill = Brushes.DarkGray;
            lable[rc.Length - 1].Content = SqList.delPosition;
            lable[rc.Length - 1].Foreground = Brushes.Red;
            lable[rc.Length - 1].Margin = new Thickness(del_margin_left + (rc[rc.Length - 1].Width - lable[lable.Length - 1].Width) / 2, margin_top - 80, 0, 0);

            m_maindemon.canse_demon.Children.Add(rc[rc.Length - 1]);
            m_maindemon.canse_demon.Children.Add(lable[lable.Length - 1]);

        }

        private void drawSqlistInsert()
        {
            int margin_left = 20;
            int margin_top = 90;
            double ins_margin_left = 0.0d;
            int length = SqList.length;
            Rectangle[] rc = new Rectangle[length + 2];//矩形
            Label[] lable = new Label[length + 2];//标签，放原始内容
            for (int i = 0; i < rc.Length; i++)//初始化需要画的数组矩形和标签元素的相同属性
            {
                rc[i] = new Rectangle();
                lable[i] = new Label();
                init_recKey_labelKey(rc[i], lable[i]);
            }
            //绘制原始数据
            for (int i = 0; i < rc.Length - 1; i++)
            {
                double rc_margin_left = i * rc[i].Width + margin_left;
                Thickness rc_src_margin = new Thickness(rc_margin_left, margin_top, 0, 0);
                rc[i].Margin = rc_src_margin;

                if (i == length)
                    lable[i].Content = "";
                else
                    lable[i].Content = SqList.srcData_ins[i];
                double lable_margin_left = rc_margin_left + (rc[i].Width - lable[i].Width) / 2;
                Thickness lable_src_margin = new Thickness(lable_margin_left, margin_top, 0, 0);
                lable[i].Margin = lable_src_margin;

                m_maindemon.canse_demon.Children.Add(rc[i]);
                m_maindemon.canse_demon.Children.Add(lable[i]);

                if (SqList.insPosition == (i + 1))
                {
                    ins_margin_left = rc_margin_left;
                }
            }
            //绘制插入数据
            rc[rc.Length - 1].Margin = new Thickness(ins_margin_left, margin_top - 80, 0, 0);
            rc[rc.Length - 1].Fill = Brushes.DarkGray;
            lable[rc.Length - 1].Content = SqList.insertData;
            lable[rc.Length - 1].Foreground = Brushes.Red;
            lable[rc.Length - 1].Margin = new Thickness(ins_margin_left + (rc[rc.Length - 1].Width - lable[lable.Length - 1].Width) / 2, margin_top - 80, 0, 0);

            m_maindemon.canse_demon.Children.Add(rc[rc.Length - 1]);
            m_maindemon.canse_demon.Children.Add(lable[lable.Length - 1]);
        }

        public static DataTable data_ins = new DataTable("DataTable_Ins");
        public static DataTable data_del = new DataTable("DataTable_Del");
        public static DataTable data_linkCre = new DataTable("DataTable_LinkCre");
        public static DataTable data_linkIns = new DataTable("DataTable_LinkIns");
        public static DataTable data_linkDel = new DataTable("DataTable_LinkDel"); 
        public static DataTable data_orderSearch = new DataTable("DataTable_OrderSea"); 
        public static DataTable data_binarySearch = new DataTable("DataTable_BinarySea");
        public static DataTable data_insSort = new DataTable("DataTable_InsSort"); 
        public static DataTable data_swapSort = new DataTable("DataTable_SwapSort"); 
        public static DataTable data_fastSort = new DataTable("DataTable_FastSort"); 
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
                    this.m_maindemon.listView_value.DataContext = GetDataTable_Ins(SqListCodes.INSERT_VALUE, 3,null,0).DefaultView;
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
                case 8:
                    if(data_insSort.Columns.Count == 0)
                    {
                        data_insSort.Columns.Add(new DataColumn("NAME", typeof(string)));//第一列
                        data_insSort.Columns.Add(new DataColumn("VALUE", typeof(string)));//第二列
                    }
                    m_maindemon.listView_value.DataContext = GetDataTable_InsSort(SortCodes.INSERT_SORT_VALUE, 4, null, 0).DefaultView;
                    break;
                case 9:
                    if (data_swapSort.Columns.Count == 0)
                    {
                        data_swapSort.Columns.Add(new DataColumn("NAME", typeof(string)));//第一列
                        data_swapSort.Columns.Add(new DataColumn("VALUE", typeof(string)));//第二列
                    }
                    m_maindemon.listView_value.DataContext = GetDataTable_SwapSort(SortCodes.SWAP_SORT_VALUE, 4, null, 0).DefaultView;
                    break;
                case 10:
                    if (data_fastSort.Columns.Count == 0)
                    {
                        data_fastSort.Columns.Add(new DataColumn("NAME", typeof(string)));//第一列
                        data_fastSort.Columns.Add(new DataColumn("VALUE", typeof(string)));//第二列
                    }
                    m_maindemon.listView_value.DataContext = GetDataTable_FastSort(SortCodes.FAST_SORT_VALUE, 4, null, 0).DefaultView;
                    break;
                default:
                    break;
            }
        }

        public DataTable GetDataTable_FastSort(string[] str, int updateFlag, string updateValue, int movePosition)
        {
            if (updateFlag == 0)//修改low
            {
                data_fastSort.Rows[Sort.length + 1]["VALUE"] = updateValue;
                m_maindemon.listView_value.SelectedIndex = Sort.length + 1;
            }
            if (updateFlag == 1)//修改high
            {
                data_fastSort.Rows[Sort.length + 2]["VALUE"] = updateValue;
                m_maindemon.listView_value.SelectedIndex = Sort.length + 2;
            }
            if (updateFlag == 2)//修改支点记录R[0]
            {
                data_fastSort.Rows[0]["VALUE"] = updateValue;
                m_maindemon.listView_value.SelectedIndex = 0;
            }
            if (updateFlag == 3)//修改pivokey
            {
                data_fastSort.Rows[Sort.length + 3]["VALUE"] = updateValue;
                m_maindemon.listView_value.SelectedIndex = Sort.length + 3;
            }
            if (updateFlag == 5)//修改pivotloc
            {
                data_fastSort.Rows[Sort.length + 4]["VALUE"] = updateValue;
                m_maindemon.listView_value.SelectedIndex = Sort.length + 4;
            }
            if(updateFlag == 6)//修改源数据
            {
                data_fastSort.Rows[movePosition]["VALUE"] = updateValue;
                m_maindemon.listView_value.SelectedIndex = movePosition;
            }
            if (updateFlag == 4)
            {
                for (int i = 0; i < Sort.length; i++)//srcData
                {
                    string name = str[0] + "[" + i + "]";
                    if (i == 0)//作为交换的介质R[0]支点记录
                        name = "支点记录-" + name;
                    string value = Sort.srcData[i] + "";
                    data_fastSort.Rows.Add(name, value);
                }
                data_fastSort.Rows.Add(str[1], Sort.length + "");//length
                data_fastSort.Rows.Add(str[2], "未知");//low
                data_fastSort.Rows.Add(str[3], "未知");//high
                data_fastSort.Rows.Add(str[4], "未知");
                data_fastSort.Rows.Add(str[5], "未知");
            }
            return data_fastSort;
        }

        public DataTable GetDataTable_SwapSort(string[] str, int updateFlag, string updateValue, int movePosition)
        {
            if (updateFlag == 0)//修改i
            {
                data_swapSort.Rows[Sort.length + 1]["VALUE"] = updateValue;
                m_maindemon.listView_value.SelectedIndex = Sort.length + 1;
            }
            if (updateFlag == 1)//修改j
            {
                data_swapSort.Rows[Sort.length + 2]["VALUE"] = updateValue;
                m_maindemon.listView_value.SelectedIndex = Sort.length + 2;
            }
            if (updateFlag == 2)//修改R[0]临时变量存储，作为交换的介质
            {
                data_swapSort.Rows[0]["VALUE"] = updateValue;
                m_maindemon.listView_value.SelectedIndex = 0;
            }
            if(updateFlag == 3)//修改swap
            {
                data_swapSort.Rows[Sort.length + 3]["VALUE"] = updateValue;
                m_maindemon.listView_value.SelectedIndex = Sort.length + 3;
            }
            if(updateFlag == 5)//修改源数据
            {
                data_swapSort.Rows[movePosition]["VALUE"] = updateValue;
                m_maindemon.listView_value.SelectedIndex = movePosition;
            }
            if (updateFlag == 4)
            {
                for (int i = 0; i < Sort.length; i++)//srcData
                {
                    string name = str[0] + "[" + i + "]";
                    if (i == 0)//临时变量存储，作为交换的介质
                        name = "交换介质-" + name;
                    string value = Sort.srcData[i] + "";
                    data_swapSort.Rows.Add(name, value);
                }
                data_swapSort.Rows.Add(str[1], Sort.length + "");//length
                data_swapSort.Rows.Add(str[2], "未知");//外部循环索引
                data_swapSort.Rows.Add(str[3], "未知");//内部循环索引
                data_swapSort.Rows.Add(str[4], "未知");//轮询一次是否有交换标志
            }
            return data_swapSort;
        }

        public DataTable GetDataTable_InsSort(string[] str, int updateFlag, string updateValue, int movePosition)
        {
            if(updateFlag == 0)//修改i
            {
                data_insSort.Rows[Sort.length + 1]["VALUE"] = updateValue;
                m_maindemon.listView_value.SelectedIndex = Sort.length + 1;
            }
            if(updateFlag == 1)//修改j
            {
                data_insSort.Rows[Sort.length + 2]["VALUE"] = updateValue;
                m_maindemon.listView_value.SelectedIndex = Sort.length + 2;
            }
            if(updateFlag == 2)//修改R[0]监视哨
            {
                data_insSort.Rows[0]["VALUE"] = updateValue;
                m_maindemon.listView_value.SelectedIndex = 0;
            }
            if(updateFlag == 3)//修改源数据
            {
                data_insSort.Rows[movePosition]["VALUE"] = updateValue;
                m_maindemon.listView_value.SelectedIndex = movePosition;
            }
            if(updateFlag == 4)
            {
                for (int i = 0; i < Sort.length; i++)//srcData
                {
                    string name = str[0] + "[" + i + "]";
                    if (i == 0)//监视哨
                        name = "监视哨-" + name;
                    string value = Sort.srcData[i] + "";
                    data_insSort.Rows.Add(name, value);
                }
                data_insSort.Rows.Add(str[1], Sort.length + "");//length
                data_insSort.Rows.Add(str[2], "未知");//外部循环索引
                data_insSort.Rows.Add(str[3], "未知");//内部循环索引
            }
            return data_insSort;
        }

        public DataTable GetDataTable_BinarySea(string[] str, int updateFlag, string updateValue, int movePosition)
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
            if (updateFlag == 4)//添加源数据
            {
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
                data_linkDel.Rows[LinkedList.srcData.Length + 2]["VALUE"] = updateValue;
                m_maindemon.listView_value.SelectedIndex = LinkedList.srcData.Length + 2;
            }
            if (updateFlag == 1)//修改j变量值
            {
                data_linkDel.Rows[LinkedList.srcData.Length + 3]["VALUE"] = updateValue;
                m_maindemon.listView_value.SelectedIndex = LinkedList.srcData.Length + 3;
            }
            if (updateFlag == 2)//修改删除结点Q
            {
                data_linkDel.Rows[LinkedList.srcData.Length + 6]["VALUE"] = updateValue;
                m_maindemon.listView_value.SelectedIndex = LinkedList.srcData.Length + 6;
            }
            if (updateFlag == 3)//修改删除元素返回值
            {
                data_linkDel.Rows[LinkedList.srcData.Length + 5]["VALUE"] = updateValue;
                m_maindemon.listView_value.SelectedIndex = LinkedList.srcData.Length + 5;
            }
            if (updateFlag == 4)//显示最开始数据
            {
                data_linkDel.Rows.Add(str[0], "指向头结点");    //添加头结点
                int length = LinkedList.srcData.Length;
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
                data_linkIns.Rows[LinkedList.srcData.Length + 2]["VALUE"] = updateValue;
                m_maindemon.listView_value.SelectedIndex = LinkedList.srcData.Length + 2;
            }
            if(updateFlag == 1)//修改j变量值
            {
                data_linkIns.Rows[LinkedList.srcData.Length + 3]["VALUE"] = updateValue;
                m_maindemon.listView_value.SelectedIndex = LinkedList.srcData.Length + 3;
            }
            if (updateFlag == 2)//修改新结点S
            {
                data_linkIns.Rows[LinkedList.srcData.Length + 6]["VALUE"] = updateValue;
                m_maindemon.listView_value.SelectedIndex = LinkedList.srcData.Length + 6;
            }
            if (updateFlag == 4)//显示最开始数据
            {
                data_linkIns.Rows.Add(str[0], "指向头结点");    //添加头结点
                int length = LinkedList.srcData.Length;
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
                data_linkCre.Rows[LinkedList.srcData.Length + 2]["VALUE"] = "当前指向值为"+updateValue+"的结点";
                m_maindemon.listView_value.SelectedIndex = LinkedList.srcData.Length + 2;
            }
            if(updateFlag == 2)//改变索引值i
            {
                data_linkCre.Rows[LinkedList.srcData.Length + 3]["VALUE"] = updateValue;
                m_maindemon.listView_value.SelectedIndex = LinkedList.srcData.Length + 3;
            }
            if (updateFlag == 3)//改变中间节点r的指向
            {
                data_linkCre.Rows[LinkedList.srcData.Length + 4]["VALUE"] = updateValue;
                m_maindemon.listView_value.SelectedIndex = LinkedList.srcData.Length + 4;
            }
            if (updateFlag == 4)//显示最开始的数据
            {
                data_linkCre.Rows.Add(str[0], "未知");    //添加头结点
                int length = LinkedList.srcData.Length;
                for (int i = 0; i < length; i++)          //添加源数据
                {
                    string name = str[1] + "[" + i + "]";
                    string value = value = LinkedList.srcData[i] + "";
                    data_linkCre.Rows.Add(name, value);
                }
                data_linkCre.Rows.Add(str[2], length);     //添加长度
                data_linkCre.Rows.Add(str[3], "当前指向？");//添加指向值为？的结点（是当前插入的结点）
                data_linkCre.Rows.Add(str[4], "未知");     //添加索引变量
                data_linkCre.Rows.Add(str[5], "未知");     //添加中间节点r
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
                data_del.Rows[SqList.length+1]["VALUE"] = updateValue;
                m_maindemon.listView_value.SelectedIndex = SqList.length +1;

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
            if(updateFlag == 0)//右移
            {
                data_ins.Rows[movePosition+1]["VALUE"] = updateValue;
                m_maindemon.listView_value.SelectedIndex = movePosition + 1;
            }
            if(updateFlag == 1)//修改P
            {
                data_ins.Rows[movePosition]["VALUE"] = updateValue;
                m_maindemon.listView_value.SelectedIndex = movePosition;
            }
            if(updateFlag == 2)//修改长度
            {
                data_ins.Rows[movePosition]["VALUE"] = updateValue;
                m_maindemon.listView_value.SelectedIndex = movePosition;
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
                case 8://直接插入排序
                    {
                        Demonstration.data_insSort.Clear();
                        ListDialog DirInsSortWindow = new ListDialog(flag);
                        //订阅事件
                        DirInsSortWindow.PassValuesEvent += new ListDialog.PassValuesHandler(RecieveSort);
                        DirInsSortWindow.ShowDialog();
                        break;
                    }
                case 9://交换（冒泡）排序
                    {
                        Demonstration.data_insSort.Clear();
                        ListDialog SwapSortWindow = new ListDialog(flag);
                        //订阅事件
                        SwapSortWindow.PassValuesEvent += new ListDialog.PassValuesHandler(RecieveSort);
                        SwapSortWindow.ShowDialog();
                        break;
                    }
                case 10://快速排序
                    {
                        Demonstration.data_insSort.Clear();
                        ListDialog FastSortWindow = new ListDialog(flag);
                        //订阅事件
                        FastSortWindow.PassValuesEvent += new ListDialog.PassValuesHandler(RecieveSort);
                        FastSortWindow.ShowDialog();
                        break;
                    }
                default:
                    break;

            }
        }

        private void RecieveSort(object sender, PassValuesEventArgs e)
        {
            m_maindemon.srcData_Sort = e.srcData;
            m_maindemon.initUI(flag);
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
        /// <summary>
        /// 算法解释
        /// </summary>
        public void ShowExplain()
        {
            switch (flag)
            {
                case 1://顺序表的插入
                        MessageBox.Show(Explain.OrderInsExplain, "顺序表插入", MessageBoxButton.OK);
                        break;
                case 2://顺序表的删除
                        MessageBox.Show(Explain.OrderDelExplain, "顺序表删除", MessageBoxButton.OK);
                        break;
                case 3://链表的创建
                        MessageBox.Show(Explain.LinkedCreExplain, "链表创建", MessageBoxButton.OK);
                        break;
                case 4://链表的插入
                        MessageBox.Show(Explain.LinkedInsExplain, "链表插入", MessageBoxButton.OK);
                        break;
                case 5://链表的删除
                        MessageBox.Show(Explain.LinkedDelExplain, "链表删除", MessageBoxButton.OK);
                        break;
                case 6://顺序查找
                        MessageBox.Show(Explain.OrderSearchExplain, "顺序查找", MessageBoxButton.OK);
                        break;
                case 7://二分查找
                        MessageBox.Show(Explain.BinarySearchExplain, "二分查找", MessageBoxButton.OK);
                        break;
                case 8://直接插入排序
                    MessageBox.Show(Explain.InsertSortExplain, "直接插入排序", MessageBoxButton.OK);
                    break;
                case 9://冒泡排序
                    MessageBox.Show(Explain.SwapSortExplain, "冒泡排序", MessageBoxButton.OK);
                    break;
                case 10://快速排序
                    MessageBox.Show(Explain.PartitionSortExplain, "快速排序", MessageBoxButton.OK);
                    break;
                default:
                    break;
            }
        }
    }
}
;