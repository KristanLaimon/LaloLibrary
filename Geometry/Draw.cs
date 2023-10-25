using System;
using System.Drawing;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace LaloLibrary.Geometry
{
    [SupportedOSPlatform("windows")]
    public class Draw
    {
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
                    graphics.DrawEllipse(pen, new RectangleF(circle.Location.X - circle.Radius, circle.Location.Y - circle.Radius, circle.Radius * 2, circle.Radius * 2));
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
                    graphics.DrawEllipse(pen, new RectangleF(circle.Location.X - circle.Radius, circle.Location.Y - circle.Radius, circle.Radius * 2, circle.Radius * 2));
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

        public static Bitmap DrawFillCircleOnBitmap(Circle circle, Bitmap bitmap, Color color)
        {
            if (circle.Radius <= 0) throw new Exception("El ancho no puede ser 0 o menor");

            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                using (Brush brush = new SolidBrush(color))
                {
                    graphics.FillEllipse(brush, new RectangleF(circle.Location.X - circle.Radius, circle.Location.Y - circle.Radius, circle.Radius * 2, circle.Radius * 2));
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
                    graphics.FillEllipse(brush, new RectangleF(circle.Location.X - circle.Radius, circle.Location.Y - circle.Radius, circle.Radius * 2, circle.Radius * 2));
                }
            }
            return bitmap;

        }

        public static void DrawPerimeterOnBitmap(Bitmap bitmap, int grosor, Color color)
        {
            if (grosor <= 0) throw new Exception("El ancho tiene que ser mayor que 0");

            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                using (Pen pen = new Pen(color, grosor))
                {
                    graphics.DrawRectangle(pen, 0, 0, bitmap.Width, bitmap.Height);
                }
            }
        }

        //easter egg main
        //when eres un easter egg en main: soy un easter egg en main xDXxdXXD
        //but estas en la clase Draw: oh mi lente de contacto XDDDXxXXD
    }
}
