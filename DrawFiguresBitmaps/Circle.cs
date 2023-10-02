

namespace KrsUtils.DrawFigures
{
    public class Circle
    {
        private int x;
        private int y;
        private int radio;
        private int diametro;
        Vector2 targetVector;

        public Circle()
        {

        }

        public Circle(int x, int y, int radio)
        {
            this.radio = radio;
            diametro = radio * 2;
            this.x = x;
            this.y = y;
        }

        public Circle(int x , int y, int radio, Vector2 vector)
        {
            this.x = x;
            this.y = y;
            diametro = radio * 2;
            this.radio = radio;
            this.targetVector = vector;
        }

        public int Y { get => y; set => y = value; }
        public int Diametro { get => diametro; set => diametro = value; }
        public int X { get => x; set => x = value; }
        public int Radio { get => radio; set => radio = value; }
        public Vector2 TargetVector { get => targetVector; set => targetVector = value; }

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
