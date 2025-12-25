using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace JiYiTunnelSystem.MVCSite
{
    public class VerifyCode
    {
        public static byte[] GetVerifyCode(int codeLength, int codeW, int codeH)
        {
            string str = string.Empty;
            Random random = new Random();
            char[] character = { '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'e', 'f', 'g', 'h', 'm', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'M' };
            for (int i = 0; i < codeLength; i++)
            {
                str += character[random.Next(character.Length)];
            }
            HttpContext.Current.Session["verifyCode"] = str;
            Bitmap bmp = new Bitmap(codeW, codeH);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.White);//背景白
            SolidBrush solidBrush = new SolidBrush(Color.Red);
            g.DrawString(str, new Font("Aril", 66), solidBrush, 20, 18);
            for (int i = 0; i < 10; i++)
            {
                int x1 = random.Next(bmp.Width);
                int y1 = random.Next(bmp.Height);
                int x2 = random.Next(bmp.Width);
                int y2 = random.Next(bmp.Height);
                g.DrawLine(new Pen(Color.Red), x1, y1, x2, y2);
            }
            for (int i = 0; i < 1000; i++)
            {
                bmp.SetPixel(random.Next(bmp.Width), random.Next(bmp.Height), Color.FromArgb(random.Next()));
            }
            g.DrawRectangle(new Pen(Color.Blue), 0, 0, bmp.Width, bmp.Height);
            g.DrawRectangle(new Pen(Color.Blue), -1, -1, bmp.Width, bmp.Height);
            MemoryStream ms = new MemoryStream();
            try
            {
                bmp.Save(ms, ImageFormat.Jpeg);
                return ms.ToArray();
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                g.Dispose();
                bmp.Dispose();
            }
        }
    }
}