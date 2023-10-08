using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LaloLibrary.FormsUtils.Bitmaps
{
    public class BitmapUtils
    {

        /// <summary>
        /// Updates a picturebox with a bitmap. As easy as that.
        /// </summary>
        /// <param name="picturebox"></param>
        /// <param name="bitmap"></param>
        public static void UpdatePictureBox(PictureBox picturebox, Bitmap bitmap) => picturebox.Image = bitmap;

        public static void MakeBitmapBlank(Bitmap bitmap)
        {
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.White);
            }
        }

        public static void PaintBitmap(Bitmap bitmap, Color color)
        {
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(color);
            }
        }

        public static Bitmap GetDimensionsFromPictureBox(PictureBox picturebox)
        {
            return new Bitmap(picturebox.Width, picturebox.Height);
        }
    }
}
