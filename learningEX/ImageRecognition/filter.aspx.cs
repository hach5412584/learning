using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web.UI;

namespace learningEX.ImageRecognition
{

    public partial class filter : System.Web.UI.Page
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
                    Bitmap SmootherBitmap = ConvertToSmoother(resizedBitmap);
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        SmootherBitmap.Save(memoryStream, ImageFormat.Png);
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
        protected void Button2_Click(object sender, EventArgs e)
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
                    Bitmap MediumBitmap = ConvertToMedium(negativeBitmap);
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        MediumBitmap.Save(memoryStream, ImageFormat.Png);
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
        public static Bitmap ConvertToMedium(Bitmap original)
        {
            Bitmap Medium = new Bitmap(original.Width, original.Height);
            
            int[] pixel_mask = new int[9];
            int pixS;            
            for (int x = 1; x < original.Width - 1; x++)            
                for (int y = 1; y < original.Height-1; y++)
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
                    Array.Sort(pixel_mask);
                    pixS = pixel_mask[4];
                    Medium.SetPixel(x, y, Color.FromArgb(pixS, pixS, pixS));
                }
            return Medium;
        }
    public static Bitmap ConvertToSmoother(Bitmap original)
    {
            Bitmap Smoother = new Bitmap(original.Width, original.Height);
            int[] Smoothing = new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1 };
            int[] pixel_mask = new int[9];
            int pixS;
            for (int x = 1; x < original.Width - 1; x++)
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
                    pixS = 0;
                    for (int i = 0; i < 9; i++)
                        pixS += (pixel_mask[i] * Smoothing[i]);
                    pixS /= 9;
                    Smoother.SetPixel(x, y, Color.FromArgb(pixS, pixS, pixS));
                }
            return Smoother;
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