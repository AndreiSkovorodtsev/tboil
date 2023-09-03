using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Tboil_v20._10._21
{
    class Badge
    {
        public Badge(string name, string surname, string email)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Surname = surname ?? throw new ArgumentNullException(nameof(surname));
            Email = email ?? throw new ArgumentNullException(nameof(email));
        }

        private string Name { get; set; }
        private string Surname { get; set; }
        private string Email { get; set; }

        public void Print()
        {
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(PrintPageHandler);
            printDocument.Print();
        }
        private void PrintPageHandler(object sender, PrintPageEventArgs e)
        {
            Bitmap bitmapQROriginal;
            using (WebClient wc = new WebClient())
            {
                using (Stream s = wc.OpenRead("https://tboil.spb.ru/personal/user/profile/images/qr" + Email + ".png"))
                {
                    bitmapQROriginal = new Bitmap(s);
                }
            }
            Bitmap bitmapQR = new Bitmap(bitmapQROriginal, new Size(150, 150));
            e.Graphics.DrawImage(bitmapQR, 190, 20);
            bitmapQR.Dispose();
            e.Graphics.DrawString(Name.ToUpper(), new Font("Arial", 14, System.Drawing.FontStyle.Bold), Brushes.Black, (190 - (Name.Length * 14)) / 2, 70);
            e.Graphics.DrawString(Surname.ToUpper(), new Font("Arial", 14, System.Drawing.FontStyle.Bold), Brushes.Black, (190 - (Surname.Length * 14)) / 2, 100);
        }
    }
}
