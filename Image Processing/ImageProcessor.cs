using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.Drawing;
using System.Windows.Forms;


//By: Ryan Guarascia.
//Grade 11 - 2013/14
//This is the main function where the process takes place and all the sweetness happens. All picture should render in less than 2
//seconds although not complete tested.
namespace Image_Processing
{
    public class ImageProcessor
    {
        public Bitmap Rendering(string bmpPath, byte _Alpha, byte _Red, byte _Green, byte _Blue)
        {
            /// <summary>
            /// This function is the "heart" of the program. It get's the pixels via LockBits, manipulates them
            /// then adds then to the picture. This is, in my mind, the most efficent way to do this.
            /// I have tested a 3987x2459 and it rendered in less than a second, so it is very fast.
            /// </summary>


            string PathWay = Path.GetDirectoryName(bmpPath) + "\\" + Path.GetFileNameWithoutExtension(bmpPath) + ".txt";


            Bitmap imageFile = new Bitmap(bmpPath);

            BitmapData imageData = new BitmapData();

            IntPtr Pointer;

            imageData = imageFile.LockBits(new Rectangle(0, 0, imageFile.Width, imageFile.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

            Pointer = imageData.Scan0;

            int ArraySize = Math.Abs(imageData.Stride) * imageFile.Height;

            byte[] PixelArray = new byte[ArraySize];

            Marshal.Copy(Pointer, PixelArray, 0, ArraySize);

            int PixelAmount = 4; //ArGb

            Color ArGBformat = new Color();

            byte NewAlpha;
            byte NewRed;
            byte NewGreen;
            byte NewBlue;

            DialogResult MsgYesNo = MessageBox.Show("Save all the pixels to a text file?. Warning, if you chose to save, it will take about a minute or two depending on file size. The file will be located wherever the image you import was.", "Save", MessageBoxButtons.YesNo);

            StreamWriter TextWriter = TextWriter = new StreamWriter(PathWay);

            unsafe
            {
                for (int y = 0; y < imageData.Height; y++)
                {
                    byte* row = (byte*)imageData.Scan0 + (y * imageData.Stride);

                    for (int x = 0; x < imageData.Width; x++)
                    {
                        int offSet = x * PixelAmount;

                        // read pixel
                        byte blue = row[offSet];

                        byte green = row[offSet + 1];

                        byte red = row[offSet + 2];

                        byte alpha = row[offSet + 3];

                        //Manipulates pixel
                        NewAlpha = (byte)Math.Abs(alpha - _Alpha);
                        NewRed = (byte)Math.Abs(red - _Red);
                        NewBlue = (byte)Math.Abs(blue - _Blue);
                        NewGreen = (byte)Math.Abs(green - _Green);

                        //Sets image
                        row[offSet] = NewBlue;
                        row[offSet + 1] = NewGreen;
                        row[offSet + 2] = NewRed;
                        row[offSet + 3] = NewAlpha;

                        ArGBformat = Color.FromArgb(NewAlpha, NewRed, NewGreen, NewBlue);

                        if (MsgYesNo == DialogResult.Yes)
                        {

                            TextWriter.Write(ArGBformat);
                        }
                    }
                }
                TextWriter.Flush();
                TextWriter.Dispose();
                imageFile.UnlockBits(imageData);
                return imageFile;
            }
        }
    }
}
