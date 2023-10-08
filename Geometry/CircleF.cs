using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaloLibrary.Geometry
{
    public class CircleF
    {
        private PointF centerF;
        private float radius;
        private float diameter;
        private double area;
        private double perimeter;

        public CircleF()
        {
        }

        public CircleF(float centerX, float centerY, float radius)
        {
            this.CenterF = new PointF(centerX, centerY);
            this.radius = radius;
            this.diameter = radius * 2;
            this.area = Math.PI * radius * radius;
            this.perimeter = Math.PI * radius * 2;
        }

        public CircleF(Point center, int radius)
        {
            this.CenterF = center;
            this.radius = radius;
            this.diameter = radius * 2;
            this.area = Math.PI * radius * radius;
            this.perimeter = Math.PI * radius * 2;
        }

        public float Diameter { get => diameter; set => diameter = value; }
        public float Radius { get => radius; set => radius = value; }
        public double Area { get => area; set => area = value; }
        public double Perimeter { get => perimeter; set => perimeter = value; }
        public PointF CenterF { get => centerF; set => centerF = value; }

        public void ChangeX(int newX)
        {
            this.CenterF = new PointF(newX, CenterF.Y);
        }

        public void ChangeY(int newY)
        {
            this.CenterF = new PointF(CenterF.X, newY);
        }

    }
}
