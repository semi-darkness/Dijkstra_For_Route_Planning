using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
//dijkatra 算法计算程序
namespace Dijkstra
{
  public  class compute
    {


        static double Max_int = 100000000;//定义一个常量，这个常量超过该地图最长的一条路径
        public static int shortestPath(List<structure.node> mm, int n, int v, double[] dist, int[] fore)//计算最短路径
        {
            //mm为node的列表，n为mm的长度，v为起点，
            //dist记录v点到各个点的最短距离，fore[]用于存放每个点最短路径中的前一个点，用于记录轨迹
            int N = mm.Count;//记录节点的数量
            double[,] m = new double[N, N];//定义一个二维数组
            //行号代表节点，列号代表与该节点的相连的节点。不相连的节点记录长度为无穷大，相连的赋值（下方循环）
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    m[i, j] = 1000000000000000;//初始化数组，不相连的节点记录长度为无穷大
                }
            }
            for (int i=0;i<N;i++)
            {
                for(int j=0;j<mm[i].num;j++)
                {
                    m[i,Convert.ToInt32( mm[i].id[j])] =mm[i].length[j];
                    //相连的赋值
                }
            }
       
            int[] s = new int[N];//s[]是用来保存已经确定最短路径的顶点的集合。
            int u = 0;double min = 0;
            for (int i = 0; i < N; i++)
            {
                dist[i] = m[v,i];// 0初始化dist数组
                s[i] = 0;//一开始只有顶点v在集合里面
            }

            s[v] = 1;
            fore[v] = v;//fore[]用于存放每个点最短路径中的前一个点，用于记录轨迹
            dist[v] = 0;
            for (int i = 0; i < n - 1; i++)//进行n-次循环
            {
                min = Max_int;//首先让最短路径为一个比较大的数（小于无穷大）
                //在u并s中选u,使得dist[u]为最短
                u = -1;
                for (int j = 0; j < n; j++)
                {
                    if (s[j] == 0 && dist[j] != 0 && dist[j] < min)//如果j点不是起点，与起点的距离小于min，
                    {                                              //且还没有被记录到s[]中 则执以下
                        min = dist[j];
                        u = j;

                    }
                }
                for (int j = 0; j < n; j++)//记录路径，起点的前一个点还是起点
                {
                    if (i == 0 && m[v, j] < Max_int)
                    {
                        fore[j] = v;

                    }
                }
                if (u == -1) return 0;//如果u=-1说明，该点与起点不相连
                s[u] = 1;//记录下该点

                for (int j = 0; j < n; j++)
                {
                    if (s[j] == 0 && m[u, j] < Max_int)
                    {

                        if (dist[j] > dist[u] + m[u, j])//如果现在的最短距离小于到u点的距离加上u点到j点的距离，则执行以下
                        {
                            dist[j] = dist[u] + m[u, j];//改变最短路径
                            fore[j] = u;//记录路径
                        }
                    }
                }
            }
            return 1;
           
        }

    }
    }
