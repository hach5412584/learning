using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web.UI;

namespace learningEX.ImageRecognition
{

public partial class BandW : System.Web.UI.Page
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
                

                int maxWidth = 500; // 最大宽度
                int maxHeight = 500; // 最大高度
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
                    Bitmap negativeBitmap = ConvertToNegative(resizedBitmap);
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                    negativeBitmap.Save(memoryStream, ImageFormat.Png);
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

    private Bitmap ConvertToNegative(Bitmap original)
    {
        Bitmap negative = new Bitmap(original.Width, original.Height);

        for (int y = 0; y < original.Height; y++)
        {
            for (int x = 0; x < original.Width; x++)
            {
                Color originalColor = original.GetPixel(x, y);
                Color negativeColor = Color.FromArgb(255 - originalColor.R, 255 - originalColor.G, 255 - originalColor.B);
                negative.SetPixel(x, y, negativeColor);
            }
        }

        return negative;
    }
    }
}
