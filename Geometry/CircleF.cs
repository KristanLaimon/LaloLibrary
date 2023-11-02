using System.Drawing;

namespace LaloLibrary.Geometry
{
    public class CircleF : Circle
    {
        private new float radius;
        private new float diameter;

        public CircleF(Point center, float radius)
        {
            this.Location = center;
            InitializeItself(radius);
        }

        public CircleF(float centerX, float centerY, float radius)
        {
            this.Location = new PointF(centerX, centerY);
            InitializeItself(radius);
        }

        public float Radius1 { get => radius; set => radius = value; }
        public float Diameter1 { get => diameter; set => diameter = value; }

        private void InitializeItself(float radius)
        {
            this.radius = radius;
            diameter = radius * 2;
            Area = Math.PI * radius * radius;
            Perimeter = Math.PI * radius * 2;
        }
    }
}