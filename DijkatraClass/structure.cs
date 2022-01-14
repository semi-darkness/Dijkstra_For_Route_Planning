using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 课程：测绘程序设计
 * Written by 罗承澄 2019302141346
              宋刚 2019302141348

 * 基于迪杰斯特拉算法(Dijkstra)的路径规划
 * 地图区域：武汉大学信息学部、文理学部、工学部
 *   数据来源：OpenStreet Map
 *  2021/06/13
   
*/

//
//存放各种结构体的类 structure
namespace Dijkstra
{
    public class structure
    {

        public struct position //用于存放兴趣点
        {
           public   string name;
            public double B;//纬度
            public double L;//经度
        }
        public struct road //存放路径信息
        {
            public string name;//名称
            public double startID;//路径起始点的ID
            public double endID;//路径终点的ID
            public double Xstart;//起点 经度
            public double Ystart;//起点 纬度
            public double Xend;//终点 经度
            public double Yend;//终点 纬度
            public double length;//长度，单位：米
          
        }
      public  class node//节点 存放节点信息
        {
            public  double name;//名称
            public  double B;//纬度
            public  double L;//经度
            public  int num;//记录与该节点相连的其他的节点的数量
            public List<double> id=new List<double>();//记录与该节点相连的其他的节点的名称，与下面的length的序号意义对应
            public List<double> length = new List<double>();//记录与该节点相连的其他的节点的距离
        }

    }
}