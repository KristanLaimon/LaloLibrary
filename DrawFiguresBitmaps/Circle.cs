

using System.Drawing;

namespace KrsUtils.DrawFigures
{
    public class Circle
    {
        private PointF centerF;      
        private float radius;
        private float diameter;
        private double area;
        private double perimeter;

        public Circle()
        {

        }

        public Circle(float centerX, float centerY, float radius)
        {
            this.centerF = new PointF(centerX, centerY);
            this.radius = radius;
            this.diameter = radius * 2;
            this.area = Math.PI*radius*radius;
            this.perimeter = Math.PI*radius * 2;

        }

        public Circle(PointF centro, int radius)
        {
            this.centerF = centro;
            this.radius = radius;
            this.diameter = radius * 2;
            this.area = Math.PI * radius * radius;
            this.perimeter = Math.PI * radius * 2;
        }

        public float Diameter { get => diameter; set => diameter = value; }
        public float Radius { get => radius; set => radius = value; }
        public PointF CenterF { get => centerF; set => centerF = value; }
        public double Area { get => area; set => area = value; }
        public double Perimeter { get => perimeter; set => perimeter = value; }



        #region Static Fabric Constructores

        /// <summary>
        /// Crea un objeto con valores por DEFECTO para este programa
        /// </summary>
        /// <returns>Devuelve un objeto círculo con valores por DEFECTO (Arriba de la foto del hombre negro). x = 54, y = 269, radio = 20</returns>
        public static Circle NewDefault()
        {
            return new Circle(54, 269, 20);
        }

        /// <summary>
        /// Crea un círculo con valores vacíos
        /// </summary>
        /// <returns>Devuelve un objeto círculo con valores: X = 0, Y = 0, Radio = 0</returns>
        public static Circle NewEmpty()
        {
            return new Circle(0, 0, 0);
        }

        public static Circle Custom(int x, int y, int radio)
        {
            return new Circle(x, y, radio);
        }
        #endregion

    }
}
