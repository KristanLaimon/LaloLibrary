using System.Drawing;

namespace LaloLibrary.Physics
{
    public class Vector2
    {
        private double x;
        private double y;
        private double magnitude;
        private double angleRadians;
        private double angleDegrees;

        public Vector2() { }
        public Vector2(double Vx, double Vy)
        {
            this.x = Vx;
            this.y = Vy;
        }

        public Vector2(double Vx, double Vy, double initialX, double initialY)
        {
            this.x = Vx;
            this.y = Vy;
            x = initialX;
        }

        public double X { get; set; }
        public double Y { get; set; }
        public double Magnitude { get; set; }
        public double AngleRadians { get; set; }
        public double AngleDegrees { get; set; }

        public static Vector2 GetVectorFromPoints(PointF p1, PointF p2)
        {
            return new Vector2(p2.X - p1.X, p1.Y - p2.Y);
        }
    }
}
