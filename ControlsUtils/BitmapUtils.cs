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
        public static void UpdatePictureBox(PictureBox picturebox, Bitmap bitmap) => picturebox.Image = bitmap;

        public static Bitmap PaintBitmap(Bitmap bitmap, Color color)
        {
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(color);
            }
            return bitmap;
        }

        public static Bitmap CreateWithPictureBoxDimensions(PictureBox picturebox)
        {
            return new Bitmap(picturebox.Width, picturebox.Height);
        }
    }

}
