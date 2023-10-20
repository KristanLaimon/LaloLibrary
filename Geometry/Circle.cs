using System.Drawing;
using LaloLibrary.Geometry.Interfaces;

namespace LaloLibrary.Geometry
{
    public class Circle : Figure2D
    {
        protected int radius;
        protected int diameter;

        public Circle() { }
        public Circle(int centerX, int centerY, int radius)
        {
            Location = new Point(centerX, centerY);
            InitializeItself(radius);
        }

        public Circle(Point center, int radius)
        {
            Location = center;
            InitializeItself(radius);
        }

        private void InitializeItself(int radius)
        {
            this.radius = radius;
            diameter = radius * 2;
            Area = Math.PI * radius * radius;
            Perimeter = Math.PI * radius * 2;
        }

        public int Diameter { get => diameter; set => diameter = value; }
        public int Radius { get => radius; set => radius = value; }

        public void ChangeX(float newX)
        {
            Location = new PointF(newX, Location.Y);
        }

        public void ChangeY(float newY)
        {
            Location = new PointF(Location.X, newY);
        }

    }
}