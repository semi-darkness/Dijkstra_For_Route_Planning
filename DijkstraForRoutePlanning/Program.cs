using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
/* 课程：测绘程序设计
 * Written by 罗承澄 2019302141346
              宋刚 2019302141348

 * 基于迪杰斯特拉算法(Dijkstra)的路径规划
 * 地图区域：武汉大学信息学部、文理学部、工学部
 *   数据来源：OpenStreet Map
 *  2021/06/13
   
*/

//
//程序入口


namespace DijkstraForRoutePlanning
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
