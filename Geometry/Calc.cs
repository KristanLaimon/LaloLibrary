using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaloLibrary.Geometry
{
    public class Calc
    {
        public static double GetDistanceFrom(Point point1, Point point2)
        {
            int deltaX = point2.X - point1.X;
            int deltaY = point2.Y - point1.Y;
            return Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
        }

        public static double GetDistanceXFrom(Point point1, Point point2)
        {
            return point2.X - point1.X;
        }

        public static double GetDistanceYFrom(Point point1, Point point2)
        {
            return point2.Y - point1.Y;
        }
    }
}
