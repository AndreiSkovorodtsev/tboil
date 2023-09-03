using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Tboil_v20._10._21
{
    public partial class QrScanner : Form
    {
        int idEvent;
        string userId;
        string name;
        string email;
        string surname;
        public QrScanner(int idEvent)
        {
            InitializeComponent();
            this.Load += QrScanner_Load;
            this.idEvent = idEvent;
            
        }

        private void QrScanner_Load(object sender, EventArgs e)
        {
            this.ActiveControl = TbUrl;
        }

        private async  void TbUrl_TextChanged(object sender, EventArgs e)
        {
            if (TbUrl.Text.Contains("https://tboil.spb.ru/"))
            { 
                Regex regex = new Regex(@"\/\d{1,}(\/|\?)");
                Match match = regex.Match(TbUrl.Text);
             
                if(match.Success)
                {
                    TbUrl.Text = string.Empty;
                    userId = match.Value.Substring(1, match.Value.Length-2);
                    int id = Convert.ToInt32(userId);

                    var user = await Entity.Processor.User.GetUserByIdAsync(id);
                    if (user != null)
                    {
                        email = user[0].email;
                        surname = user[0].lastname;
                        name = user[0].name;
                    }
                    var request = await Entity.Processor.Request.GetRequestsAsync(idEvent, id);
                    if (request == null)
                    {
                        await Entity.Processor.Request.Add(idEvent, id, 1, 0);
                        // countAttendances++;
                        PrintQr();
                        // labelAtt.Text = string.Format("Количество  посещений: {0}", countAttendances.ToString());
                    }
                    else
                    {
                        int reqId = request.requests[0].id;
                        var updateResult = await Entity.Processor.Request.UpdateStatus(reqId, 1);
                        // countAttendances++;
                        PrintQr();
                        //labelAtt.Text = string.Format("Количество  посещений: {0}", countAttendances.ToString());
                    }
                     TbUrl.Text = string.Empty;
                    // text = string.Empty;
                    //  MessageBox.Show(id.ToString());
                }
            }

        }


     

        private void PrintQr()
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
                using (Stream s = wc.OpenRead("https://tboil.spb.ru/personal/user/profile/images/qr" + email + ".png"))
                {
                    bitmapQROriginal = new Bitmap(s);
                }
            }
            Bitmap bitmapQR = new Bitmap(bitmapQROriginal, new Size(150, 150));
            e.Graphics.DrawImage(bitmapQR, 190, 20);
            bitmapQR.Dispose();
            e.Graphics.DrawString(name.ToUpper(), new Font("Arial", 14, System.Drawing.FontStyle.Bold), Brushes.Black, (190 - (name.Length * 14)) / 2, 70);
            e.Graphics.DrawString(surname.ToUpper(), new Font("Arial", 14, System.Drawing.FontStyle.Bold), Brushes.Black, (190 - (surname.Length * 14)) / 2, 100);
        }
    }
}
