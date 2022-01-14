using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Dijkstra;
/* 课程：测绘程序设计
 * Written by 罗承澄 2019302141346
              宋刚 2019302141348

 * 基于迪杰斯特拉算法(Dijkstra)的路径规划
 * 地图区域：武汉大学信息学部、文理学部、工学部
 *   数据来源：OpenStreet Map
 *  2021/06/13
   
*/


//
//界面设计

namespace DijkstraForRoutePlanning
{
    public partial class Form1 : Form
    {

        string poipath  = "whupoi.txt";//兴趣点文件路径
        Pen blackpen = new Pen(Color.Black, 2);
        Pen redpen = new Pen(Color.Red, 10);
        Pen greenpen = new Pen(Color.Green, 10);
        double desX = new double();//destination 目的地横坐标
        double desY = new double();
        double depX = new double();//departure 出发地横坐标
        double depY = new double();
        Point dep = new Point();//出发点
        Point desp = new Point();//目的地
        int v = 0;
        int t = 0;
        int z = 0;//用于记录鼠标在pictureBox1上点击的次数，奇数点击记录为起点，偶数点击记录为终点

        public Form1()
        {
            InitializeComponent();//初始化
        }
        private void Form1_Load_1(object sender, EventArgs e)
        {
            Graphics map = this.pictureBox1.CreateGraphics();

            List<structure.position> poi = new List<structure.position>();
            read.poi(poi,poipath);
            for (int i = 0; i < poi.Count; i++)
                dataGridView2.Rows.Add(poi[i].name.ToString());//表格显示兴趣点 地名
        }     
        public void InputPoi_ComputeAndDraw()//地名输入模式
        {
            Graphics map = this.pictureBox1.CreateGraphics();
            map.Clear(Color.White);
            List<structure.road> route = new List<structure.road>();
            List<structure.node> node = new List<structure.node>();
            List<structure.position> poi = new List<structure.position>();
       
            read.poi(poi,poipath);//读入兴趣点文件
            read.road(route);//读入路径信息
            read.road2node(route, node);//路径转节点

            int n = node.Count;
            double[] dist = new double[node.Count];
            int[] fore = new int[node.Count];

            int depo = poi.FindIndex((structure.position w) => (w.name == textBox2.Text));
            int deso = poi.FindIndex((structure.position w) => (w.name == textBox4.Text));
            //记录输入的地名在poi链表中的位置 以获得其经纬度
            for (int i = 0; i < poi.Count; i++)
                dataGridView2.Rows.Add(poi[i].name.ToString());

            depX = poi[depo].L;
            depY = poi[depo].B;
            desX = poi[deso].L;
            desY = poi[deso].B;

            v = Near(node, depX, depY);//起点的点号
            t = Near(node, desX, desY);//终点的点号

            Dijkstra.compute.shortestPath(node, n, v, dist, fore);
            //计算最短路径

            string str;
            str = (v).ToString();
            int[] order = new int[node.Count];
            int k = 0;
            int des = t;

            textBox3.Text = dist[des].ToString();



            Point[] p = new Point[node.Count];
            for (int i = 0; i < node.Count; i++)//将经纬度转换为picturebox坐标系
            {
                p[i] = new Point(Convert.ToInt32((node[i].L - 114.3472239) / (114.37215 - 114.3472239) * pictureBox1.Width), Convert.ToInt32((30.54986212 - node[i].B) / (30.54986212 - 30.5258531) * pictureBox1.Height));

            }
            while (t != v)
            {
                order[k] = t;
                map.DrawLine(redpen, p[t], p[fore[t]]);//以红色画出最短路径的路线
                t = fore[t];
                k++;

            }
            double road = 0;
            for (int j = k - 1; j >= 0; j--)
            {
                str = str + ">>" + (order[j]).ToString();
                if (j == k - 1)
                {
                    int t = 0;
                    t = node[v].id.FindIndex((double h) => (h == order[j]));
                    road = node[v].length[t];
                }
                else
                {
                    int t = 0;
                    t = node[order[j + 1]].id.FindIndex((double h) => (h == order[j]));
                    road = road + node[order[j + 1]].length[t];
                }
            }
          
            textBox1.Text = str;
            //输出对应的最短路径的轨迹
           

            for (int i = 0; i < route.Count; i++)//画出地图
            {
                map.DrawLine(blackpen, p[Convert.ToInt32(route[i].startID)], p[Convert.ToInt32(route[i].endID)]);
            }
        }
        public void FreeSection_ComputeAndDrawf()//自由选点模式
        {
            Graphics map = this.pictureBox1.CreateGraphics();
            map.Clear(Color.White);
            List<structure.road> mm = new List<structure.road>();
            List<structure.node> node = new List<structure.node>();
            List<structure.position> poi = new List<structure.position>();
            //map.DrawRectangle(greenpen, new Rectangle(new Point(Convert.ToInt32(depX),Convert.ToInt32(depY)), new Size(2, 2)));
            //map.DrawRectangle(redpen, new Rectangle(new Point(Convert.ToInt32(desX), Convert.ToInt32(desY)), new Size(2, 2)));
            read.poi(poi,poipath);
            read.road(mm);
            read.road2node(mm, node);

            int n = node.Count;
            double[] dist = new double[node.Count];
            int[] fore = new int[node.Count];

            int depo = poi.FindIndex((structure.position w) => (w.name == textBox2.Text));
            int deso = poi.FindIndex((structure.position w) => (w.name == textBox4.Text));
            for (int i = 0; i < poi.Count; i++)
                dataGridView2.Rows.Add(poi[i].name.ToString());


            v = Near(node, depX, depY);
            t = Near(node, desX, desY);
            compute.shortestPath(node, n, v, dist, fore);
            string str;
            str = (v).ToString();
            int[] order = new int[node.Count];
            int k = 0;
            int des = t;

            textBox3.Text = dist[des].ToString();



            Point[] p = new Point[node.Count];
            for (int i = 0; i < node.Count; i++)
            {
                p[i] = new Point(Convert.ToInt32((node[i].L - 114.3472239) / (114.37215 - 114.3472239) * pictureBox1.Width), Convert.ToInt32((30.54986212 - node[i].B) / (30.54986212 - 30.5258531) * pictureBox1.Height));

            }
            while (t != v)
            {
                order[k] = t;
                map.DrawLine(redpen, p[t], p[fore[t]]);

                t = fore[t];
                k++;

            }
            double road = 0;
            for (int j = k - 1; j >= 0; j--)
            {
                str = str + ">>" + (order[j]).ToString();
                if (j == k - 1)
                {
                    int t = 0;
                    t = node[v].id.FindIndex((double h) => (h == order[j]));
                    road = node[v].length[t];
                }
                else
                {
                    int t = 0;
                    t = node[order[j + 1]].id.FindIndex((double h) => (h == order[j]));
                    road = road + node[order[j + 1]].length[t];
                }
            }
            textBox3.Text = road.ToString();
            textBox1.Text = str;//输出对应的最短路径的轨迹
            map.DrawString("终点", new Font("黑体", 16), Brushes.Blue, desp);
            map.DrawString("起点", new Font("黑体", 16), Brushes.Blue, dep);
            for (int i = 0; i < mm.Count; i++)
            {
                map.DrawLine(blackpen, p[Convert.ToInt32(mm[i].startID)], p[Convert.ToInt32(mm[i].endID)]);
            }
        }
        private void button1_Click_1(object sender, EventArgs e)//执行地名输入模式
        {

            dataGridView2.Rows.Clear();
            InputPoi_ComputeAndDraw();
        }
        private void button2_Click_1(object sender, EventArgs e)//执行自由选点
        {
            dataGridView2.Rows.Clear();
            FreeSection_ComputeAndDrawf();
        }
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)//自由选点中标记出发地和目的地
        {
            Graphics map = pictureBox1.CreateGraphics();
            if (z % 2 == 0)
            {

                dep = e.Location;
                depX = dep.X * (114.37215 - 114.3472239) / pictureBox1.Width + 114.3472239;
                depY = 30.54986212 - dep.Y * (30.54986212 - 30.5258531) / pictureBox1.Height;
                map.DrawRectangle(greenpen, new Rectangle(dep, new Size(2, 2)));
                map.DrawString("起点", new Font("黑体", 16), Brushes.Blue, dep);
                z++;
            }
            else
            {

                desp = e.Location;
                desX = desp.X * (114.37215 - 114.3472239) / pictureBox1.Width + 114.3472239;
                desY = 30.54986212 - desp.Y * (30.54986212 - 30.5258531) / pictureBox1.Height;
                map.DrawRectangle(redpen, new Rectangle(desp, new Size(2, 2)));
                map.DrawString("终点", new Font("黑体", 16), Brushes.Blue, desp);
                z++;
            }
        }
        public static int Near(List<structure.node> node, double x, double y)//寻找兴趣点距离最近的节点
        {
            List<double> box = new List<double>();
            for (int i = 0; i < node.Count; i++)
            {
                double line = (node[i].B - y) * (node[i].B - y) + (node[i].L - x) * (node[i].L - x);
                box.Add(line);
            }
            double min = box.Min();
            int minIndex = box.FindIndex((double k) => k == min);
            return minIndex;
        }

        private void 导入兴趣点文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenFileDialog1 = new OpenFileDialog();
            if (OpenFileDialog1.ShowDialog()==DialogResult.OK)
            {
                poipath = OpenFileDialog1.FileName;
                List<structure.position> poi = new List<structure.position>();
                read.poi(poi, poipath);
                for (int i = 0; i < poi.Count; i++)
                    dataGridView2.Rows.Add(poi[i].name.ToString());//表格显示兴趣点 地名
            }
        }
    }

}
