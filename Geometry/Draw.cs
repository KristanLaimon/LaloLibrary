using System;
using System.Drawing;
using System.Windows.Forms;

namespace LaloLibrary.Geometry
{
    public class Draw
    {

        #region Bitmaps
        /// <summary>
        /// Draws a circle on a bitmap with the specified parameters. You don't have to worry about "the rectangle". Just coordinates and radius. That's it!
        /// </summary>
        /// <param name="radio"></param>
        /// <param name="centroX"></param>
        /// <param name="centroY"></param>
        /// <param name="bitmap"></param>
        /// <param name="grosor"></param>
        /// <param name="color"></param>
        /// <exception cref="Exception"></exception>
        public static Bitmap DrawCircleOnBitmap(float radio, float centroX, float centroY, Bitmap bitmap, int grosor, Color color)
        {
            if (grosor <= 0 || radio <= 0) throw new Exception("El ancho no puede ser 0 o menor");

            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                using (Pen pen = new Pen(color, grosor))
                {
                    graphics.DrawEllipse(pen, new RectangleF(centroX - radio, centroY - radio, radio * 2, radio * 2));
                }
            }

            return bitmap;
        }

        public static Bitmap DrawCircleOnBitmap(Circle circle, Bitmap bitmap, int grosor, Color color)
        {
            if (grosor <= 0 || circle.Radius <= 0) throw new Exception("El ancho no puede ser 0 o menor");

            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                using (Pen pen = new Pen(color, grosor))
                {
                    graphics.DrawEllipse(pen, new RectangleF(circle.Center.X - circle.Radius, circle.Center.Y - circle.Radius, circle.Radius * 2, circle.Radius * 2));
                }
            }

            return bitmap;
        }

        public static Bitmap DrawCircleFOnBitmap(CircleF circle, Bitmap bitmap, int grosor, Color color)
        {
            if (grosor <= 0 || circle.Radius <= 0) throw new Exception("El ancho no puede ser 0 o menor");

            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                using (Pen pen = new Pen(color, grosor))
                {
                    graphics.DrawEllipse(pen, new RectangleF(circle.CenterF.X - circle.Radius, circle.CenterF.Y - circle.Radius, circle.Radius * 2, circle.Radius * 2));
                }
            }
            return bitmap;

        }

        /// <summary>
        /// Draws a fill circle on a bitmap with the specified parameters. You don't have to worry about "the rectangle". Just coordinates and radius. That's it!
        /// </summary>
        /// <param name="radio">The radius</param>
        /// <param name="centroX"></param>
        /// <param name="centroY"></param>
        /// <param name="bitmap"></param>
        /// <param name="color"></param>
        /// <exception cref="Exception"></exception>
        public static Bitmap DrawFillCircleOnBitmap(float radio, float centroX, float centroY, Bitmap bitmap, Color color)
        {
            if (radio <= 0) throw new Exception("El ancho no puede ser 0 o menor");

            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                using (Brush brush = new SolidBrush(color))
                {
                    graphics.FillEllipse(brush, new RectangleF(centroX - radio, centroY - radio, radio * 2, radio * 2));
                }
            }
            return bitmap;

        }

        public static Bitmap DrawFillCircleOnBitmap(Circle circle, ref Bitmap bitmap, Color color)
        {
            if (circle.Radius <= 0) throw new Exception("El ancho no puede ser 0 o menor");

            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                using (Brush brush = new SolidBrush(color))
                {
                    graphics.FillEllipse(brush, new RectangleF(circle.Center.X - circle.Radius, circle.Center.Y - circle.Radius, circle.Radius * 2, circle.Radius * 2));
                }
            }
            return bitmap;

        }

        public static Bitmap DrawFillCircleFOnBitmap(CircleF circle, Bitmap bitmap, Color color)
        {
            if (circle.Radius <= 0) throw new Exception("El ancho no puede ser 0 o menor");

            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                using (Brush brush = new SolidBrush(color))
                {
                    graphics.FillEllipse(brush, new RectangleF(circle.CenterF.X - circle.Radius, circle.CenterF.Y - circle.Radius, circle.Radius * 2, circle.Radius * 2));
                }
            }
            return bitmap;

        }

        #endregion

   

        //easter egg main
    }
}
