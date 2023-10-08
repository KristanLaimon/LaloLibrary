using LaloLibrary.Exceptions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaloLibrary.Geometry
{
    public class Triangle
    {
        private Point[] vertices;
        private Point center;
        private int heigth;
        private int lengthSide;
        private float area;

        public Triangle()
        {

        }

        public Triangle(int x1, int y1, int x2, int y2, int x3, int y3, Point center)
        {
            Point vertexOne = new Point(x1, y1);
            Point vertexTwo = new Point(x2, y2);
            Point vertexThree = new Point(x3, y3);

            Vertices = new Point[] { vertexOne, vertexTwo, vertexThree };
            
        }

        public Triangle(Point[] vertices, Point center)
        {
            Vertices = vertices;
            Center = center;
        }

        public Point[] Vertices 
        { 
            get => vertices;
            set
            {
                if (value.Length > 3)
                {
                    throw new ExcessVerticesFigure();
                }
            }
        }

        public Point Center { get => center; set => center = value; }
    }
}
