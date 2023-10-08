using System.Drawing;

namespace LaloLibrary.Geometry
{
    public class Circle
    {
        private Point center;
        private float radius;
        private float diameter;
        private double area;
        private double perimeter;

        public Circle()
        {
        }

        public Circle(int centerX, int centerY, float radius)
        {
            this.Center = new Point(centerX, centerY);
            this.radius = radius;
            this.diameter = radius * 2;
            this.area = Math.PI * radius * radius;
            this.perimeter = Math.PI * radius * 2;
        }

        public Circle(Point center, int radius)
        {
            this.Center = center;
            this.radius = radius;
            this.diameter = radius * 2;
            this.area = Math.PI * radius * radius;
            this.perimeter = Math.PI * radius * 2;
        }

        public float Diameter { get => diameter; set => diameter = value; }
        public float Radius { get => radius; set => radius = value; }
        public double Area { get => area; set => area = value; }
        public double Perimeter { get => perimeter; set => perimeter = value; }
        public Point Center { get => center; set => center = value; }

        public void ChangeX(int newX)
        {
            this.Center = new Point(newX, Center.Y);
        }

        public void ChangeY(int newY)
        {
            this.Center = new Point(Center.X, newY);
        }

    }
}