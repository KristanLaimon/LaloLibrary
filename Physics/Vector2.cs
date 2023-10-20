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
            x = Vx;
            y = Vy;
        }
        public double X { get => x; set => x = value; }
        public double Y { get => y; set => y = value; }
        public double Magnitude { get => magnitude; set => magnitude = value; }
        public double AngleRadians { get => angleRadians; set => angleRadians = value; }
        public double AngleDegrees { get => angleDegrees; set => angleDegrees = value; }

        public static Vector2 GetVectorFromPoints(PointF p1, PointF p2)
        {
            return new Vector2(p2.X - p1.X, p1.Y - p2.Y);
        }

        public static Vector2 operator +(Vector2 value1, Vector2 value2)
        {
            Vector2 result = new();
            result.X = value1.X + value2.X;
            result.Y = value1.Y + value2.Y;
            return result;
        }
    }
}
