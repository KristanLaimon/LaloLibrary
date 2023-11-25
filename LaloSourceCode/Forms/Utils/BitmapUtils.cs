using System.Drawing;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace LaloLibrary.Forms.Utils
{
    [SupportedOSPlatform(platformName: "Windows")]
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