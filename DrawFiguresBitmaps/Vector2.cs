

using System.Drawing;

namespace KrsUtils.DrawFigures
{
    internal enum Seccion
    {
        Vector_X_Y,
        Magnitude_Radians,
        Magnitude_Degrees
    }
    public class Vector2
    {
        private double Vx;
        private double Vy;
        private double x;
        private double y;
        private double magnitude;
        private double angleRadians;
        private double angleDegrees;

        public Vector2()
        {

        }

        /// <summary>
        /// This sets it's initial componentes. Initialx and initialY = 0 by default.
        /// </summary>
        /// <param name="Vx"></param>
        /// <param name="Vy"></param>
        public Vector2(double Vx, double Vy)
        {
            this.Vx = Vx;
            this.Vy = Vy;
            x = 0;
            y = 0;
        }

        public Vector2(double Vx, double Vy, double initialX, double initialY)
        {
            this.Vx = Vx;
            this.Vy = Vy;
            x = initialX;

        }

        public double X 
        { 
            get => Vx;
            set
            {
                Vx = value;
            }
        }
        public double Y 
        {
            get => Vy; 
            set
            {
                Vy = value; 
            } 
        }
        public double Magnitude { get => magnitude; set => magnitude = value; }
        public double AngleRadians 
        { 
            get => angleRadians; 
            set
            {
                angleRadians = value;
            } 
        }
        public double AngleDegrees 
        { 
            get => angleDegrees;
            set
            {
                angleDegrees = value;
            }
        }

        /// <summary>
        /// Gets a vector doing P2-P1 formula
        /// </summary>
        /// <param name="p1">The initial Point</param>
        /// <param name="p2">The target Point</param>
        /// <returns></returns>
        public static Vector2 GetVectorFromPoints(Point p1, Point p2)
        {
            return new Vector2(p2.X - p1.X, p1.Y - p2.Y);
        }


    }
}
