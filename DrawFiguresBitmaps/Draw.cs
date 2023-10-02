using System;
using System.Drawing;
using System.Windows.Forms;

namespace KrsUtils.DrawFigures.Bitmaps
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
        public static void DrawCircleOnBitmap(float radio, float centroX, float centroY, ref Bitmap bitmap, int grosor, Color color)
        {
            if (grosor <= 0 || radio <= 0) throw new Exception("El ancho no puede ser 0 o menor");

            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                using (Pen pen = new Pen(color, grosor))
                {
                    graphics.DrawEllipse(pen, new RectangleF(centroX - radio, centroY - radio, radio * 2, radio * 2));
                }
            }
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
        public static void DrawFillCircleOnBitmap(float radio, float centroX, float centroY, ref Bitmap bitmap, Color color)
        {
            if (radio <= 0) throw new Exception("El ancho no puede ser 0 o menor");

            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                using (Brush brush = new SolidBrush(color))
                {
                    graphics.FillEllipse(brush, new RectangleF(centroX - radio, centroY - radio, radio * 2, radio * 2));
                }
            }
        }

        public static void DrawFillCircleOnBitmap(Circle circle, ref Bitmap bitmap, Color color)
        {
            if (circle.Radius <= 0) throw new Exception("El ancho no puede ser 0 o menor");

            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                using (Brush brush = new SolidBrush(color))
                {
                    graphics.FillEllipse(brush, new RectangleF(circle.CenterF.X - circle.Radius, circle.CenterF.Y - circle.Radius, circle.Radius * 2, circle.Radius * 2));
                }
            }
        }

        #endregion

        #region BitmapPictureBox

        /// <summary>
        /// Updates a picturebox with a bitmap. As easy as that.
        /// </summary>
        /// <param name="picturebox"></param>
        /// <param name="bitmap"></param>
        public static void UpdatePictureBox(PictureBox picturebox, Bitmap bitmap) => picturebox.Image = bitmap;

        public static void MakeBitmapBlank(ref Bitmap bitmap)
        {
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.White);
            }
        }
        #endregion

        //easter egg main
    }
}
