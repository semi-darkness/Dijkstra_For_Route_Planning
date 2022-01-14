using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;



/* 课程：测绘程序设计
 * Written by 罗承澄 2019302141346
              宋刚 2019302141348

 * 基于迪杰斯特拉算法(Dijkstra)的路径规划
 * 地图区域：武汉大学信息学部、文理学部、工学部
 *   数据来源：OpenStreet Map
 *  2021/06/13
   
*/

//
//读取文件类 read

namespace Dijkstra
{
   public class read
    {
        public static void road(List<structure.road> m)//读取路径信息并储存
        {
            string path = "lineInread.txt";//文件
            StreamReader rd = new StreamReader(path);
            string rubbish = rd.ReadLine();//第一行不要
            while (!rd.EndOfStream)
            {
                string box = rd.ReadLine();
                string[] bbox = box.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
                structure.road bottle = new structure.road();
                bottle.name = bbox[0];
                bottle.Xstart = double.Parse(bbox[2]);
                bottle.Ystart = double.Parse(bbox[3]);
                bottle.Xend = double.Parse(bbox[4]);
                bottle.Yend = double.Parse(bbox[5]);
                bottle.length = double.Parse(bbox[6]);
                bottle.startID = double.Parse(bbox[7]);
                bottle.endID = double.Parse(bbox[8]);
                m.Add(bottle);
            }

        }
        public static void road2node(List<structure.road> m, List<structure.node> Node)//路径信息转换为节点信息
        {  
            string path = "dotInread.txt";//文件
            StreamReader rd = new StreamReader(path);
            string rubbish = rd.ReadLine();//第一行不要
            while (!rd.EndOfStream)
            {
                string box = rd.ReadLine();
                string[] bbox = box.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
                structure.node bottle = new structure.node();
                bottle.name = double.Parse(bbox[0]);
                bottle.L= double.Parse(bbox[1]);
                bottle.B = double.Parse(bbox[2]);
                Node.Add(bottle);
            }
           
            for (int i=0;i<Node.Count;i++)//路径信息转换为节点信息
            {
                          Node[i].num= 0;//初始化节点的连接数为0
                for (int j=0;j<m.Count;j++)
                {
                    if (m[j].startID == Node[i].name)//如果道路的起点与该节点的名称相同，则记录下该路径的长度和终点
                    {
                        Node[i].id.Add(m[j].endID);
                        Node[i].length.Add(m[j].length);
                        Node[i].num++;
                    }
                    else if (m[j].endID == Node[i].name)//如果道路的终点与该节点的名称相同，则记录下该路径的长度和起点
                    {
                        Node[i].id.Add(m[j].startID);
                        Node[i].length.Add(m[j].length);
                        Node[i].num++;
                    }
                    else continue;
                }
                
            }
        }

        public static void poi(List<structure.position> poi,string path)//读取兴趣点信息并储存
        {
           //path = "whupoi.txt";
            StreamReader rd = new StreamReader(@path, System.Text.Encoding.GetEncoding("gb2312"));
            string rubbish = rd.ReadLine();//第一行不要
            while (!rd.EndOfStream)
            {
                string box = rd.ReadLine();
                string[] bbox = box.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
                structure.position bottle = new structure.position();
                bottle.name = bbox[0];
                bottle.L = double.Parse(bbox[2]);
                bottle.B = double.Parse(bbox[1]);
                poi.Add(bottle);
            }
        }
    }

}
