using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web.UI;

namespace learningEX.ImageRecognition
{

    public partial class EdgeDetection : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {

                // 讀取上傳的圖片
                using (Stream stream = FileUpload1.PostedFile.InputStream)
                {
                    Bitmap bitmap = new Bitmap(stream);


                    int maxWidth = 500;
                    int maxHeight = 500;
                    int newWidth, newHeight;

                    if (bitmap.Width > bitmap.Height)
                    {
                        newWidth = maxWidth;
                        newHeight = (int)((float)bitmap.Height / bitmap.Width * maxWidth);
                    }
                    else
                    {
                        newWidth = (int)((float)bitmap.Width / bitmap.Height * maxHeight);
                        newHeight = maxHeight;
                    }

                    Bitmap resizedBitmap = new Bitmap(bitmap, newWidth, newHeight);
                    Bitmap negativeBitmap = ConvertToGrayscale(resizedBitmap);
                    Bitmap EdgeDetectionBitmap = ConvertToEdgeDetection(resizedBitmap, Convert.ToInt32(TextBox1.Text));
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        EdgeDetectionBitmap.Save(memoryStream, ImageFormat.Png);
                        string base64String = Convert.ToBase64String(memoryStream.ToArray());
                        Image1.ImageUrl = "data:image/png;base64," + base64String;
                    }
                    using (MemoryStream memoryStream2 = new MemoryStream())
                    {
                        resizedBitmap.Save(memoryStream2, ImageFormat.Png);
                        string base64String = Convert.ToBase64String(memoryStream2.ToArray());
                        Image2.ImageUrl = "data:image/jpeg;base64," + base64String;
                    }
                }
            }
        }
        
        public static Bitmap ConvertToGrayscale(Bitmap original)
        {
            Bitmap grayscale = new Bitmap(original.Width, original.Height);

            for (int y = 0; y < original.Height; y++)
            {
                for (int x = 0; x < original.Width; x++)
                {
                    Color pixel = original.GetPixel(x, y);
                    int average = (pixel.R + pixel.G + pixel.B) / 3;
                    Color grayscalePixel = Color.FromArgb(average, average, average);
                    grayscale.SetPixel(x, y, grayscalePixel);
                }
            }

            return grayscale;
        }
        public static Bitmap ConvertToEdgeDetection(Bitmap original, int threshold)
        {

            Bitmap Edge = new Bitmap(original.Width, original.Height);
            int[] horizontal = new int[] { 1, 0, -1, 2, 0, -2, 1, 0, -1 };
            int[] vertical = new int[] { 1, 2, 1, 0, 0, 0, -1, -2, -1 };
            int[,] pixS = new int[original.Height, original.Width];
            int[] pixel_mask = new int[9];
            int pixS_h = 0, pixS_v = 0;
            int max = 0;
            int min = 0;

            if (threshold >= 0 && threshold <= 255)
            {

                for (int x = 1; x < original.Width - 1; x++)
                {
                    for (int y = 1; y < original.Height - 1; y++)
                    {
                        pixel_mask[0] = original.GetPixel(x - 1, y - 1).G;
                        pixel_mask[1] = original.GetPixel(x, y - 1).G;
                        pixel_mask[2] = original.GetPixel(x + 1, y - 1).G;
                        pixel_mask[3] = original.GetPixel(x - 1, y).G;
                        pixel_mask[4] = original.GetPixel(x, y).G;
                        pixel_mask[5] = original.GetPixel(x + 1, y).G;
                        pixel_mask[6] = original.GetPixel(x - 1, y + 1).G;
                        pixel_mask[7] = original.GetPixel(x, y + 1).G;
                        pixel_mask[8] = original.GetPixel(x + 1, y + 1).G;
                        for (int i = 0; i < 9; i++)
                        {
                            pixS_h += (pixel_mask[i] * horizontal[i]);
                            pixS_v += (pixel_mask[i] * vertical[i]);
                        }
                        pixS_h = pixS_h / 9;
                        pixS_v = pixS_v / 9;
                        pixS_v = Math.Abs(pixS_v);
                        pixS_h = Math.Abs(pixS_h);
                        pixS[y - 1, x - 1] = pixS_h + pixS_v;
                        if (max < pixS[y - 1, x - 1])
                        {
                            max = pixS[y - 1, x - 1];
                        }
                        if (min > pixS[y - 1, x - 1])
                        {
                            min = pixS[y - 1, x - 1];
                        }
                    }
                }
                for (int x = 0; x < original.Width; x++)
                {
                    for (int y = 0; y < original.Height; y++)
                    {
                        pixS[y, x] = (pixS[y, x] - min) * 255 / (max - min);
                        if (pixS[y, x] >= Convert.ToInt32(threshold))
                        {
                            pixS[y, x] = 255;
                        }
                        else
                        {
                            pixS[y, x] = 0;
                        }
                        Edge.SetPixel(x, y, Color.FromArgb(pixS[y, x], pixS[y, x], pixS[y, x]));
                    }
                }
            }
            return Edge;
        }
        
        private byte[] ImageToByteArray(Image image)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                image.Save(stream, ImageFormat.Jpeg);
                return stream.ToArray();
            }
        }
    }
}