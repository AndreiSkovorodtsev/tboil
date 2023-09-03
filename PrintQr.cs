using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tboil_v20._10._21
{
    public partial class PrintQr : Form
    {
        string name;
        string surname;
        string email;

        public PrintQr(string name, string email , string surname)
        {
            InitializeComponent();
            this.name = name;
            this.email = email;
            this.surname = surname;
            LoadQr();
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(PrintPageHandler);
            printDocument.Print();
            this.Close();
        }
        private void PrintPageHandler(object sender, PrintPageEventArgs e)
        {
            Bitmap bitmapQR = new Bitmap(pictureBoxQr.Image,pictureBoxQr.Width,pictureBoxQr.Height);
            e.Graphics.DrawImage(bitmapQR, 190, 20);
            bitmapQR.Dispose();
            e.Graphics.DrawString(name.ToUpper(), new Font("Arial", 14, System.Drawing.FontStyle.Bold), Brushes.Black, (190 - (name.Length * 14)) / 2, 70);
            e.Graphics.DrawString(surname.ToUpper(), new Font("Arial", 14, System.Drawing.FontStyle.Bold), Brushes.Black, (190 - (surname.Length * 14)) / 2, 100);
        }
        private void LoadQr()
        {
            pictureBoxQr.LoadAsync("https://tboil.spb.ru/personal/user/profile/images/qr" + email + ".png");
        }
    }
}
