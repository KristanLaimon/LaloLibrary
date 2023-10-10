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

        public static Bitmap PaintBitmap(Bitmap bitmap, Color color)
        {
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(color);
            }
            return bitmap;
        }

        public static Bitmap GetDimensionsFromPictureBox(PictureBox picturebox)
        {
            return new Bitmap(picturebox.Width, picturebox.Height);
        }
    }

    //I'm thinking to remove static and make them part of the object itself: a new bitmap extended class??
    partial class BetterBitmap
    {
        private Bitmap bitmap;

        public BetterBitmap(Bitmap bitmap)
        {
            this.bitmap = bitmap;
        }

        //Add functions and extended logic here
    }
}
