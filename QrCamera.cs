using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;

namespace Tboil_v20._10._21
{
    public partial class QrCamera : Form
    {
        string name;
        string email;
        string surname;
        int startIndex = 21;
        int endIndex = 0;
        string text = null;
        int countAttendances = 0;
        FilterInfoCollection filterInfoCollection;
        VideoCaptureDevice captureDevice;
        int idEvent;
        public QrCamera(int idEvent)
        {
            InitializeComponent();
        
            this.Load += QrScanner_Load;
            this.ShowInTaskbar = false;
            this.TbUrlString.HideSelection = true;
            this.idEvent = idEvent;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (pbCamera.Image != null)
            {
                BarcodeReader barcodeReader = new BarcodeReader();
                Result result = barcodeReader.Decode((Bitmap)pbCamera.Image);
                if (result != null)
                {
                    if (result.Text.Contains("https://tboil.spb.ru"))
                    {
                        TbUrlString.Text = result.ToString();
                        MarkVisit(TbUrlString.Text);
                    }
                }
            }
        }

        private void QrScanner_Load(object sender, EventArgs e)
        {
            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            captureDevice = new VideoCaptureDevice(filterInfoCollection[0].MonikerString);
            captureDevice.NewFrame += CaptureDevice_NewFrame;
            captureDevice.Start();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void CaptureDevice_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            pbCamera.Image = (Bitmap)eventArgs.Frame.Clone();
        }

        private async void MarkVisit(string qrText)
        {
            Regex regex = new Regex(@"^https:\/\/tboil.spb.ru\/\d{1,}\/$");
            Regex regexWithCodeSlash = new Regex(@"^https\:\/\/tboil\.spb\.ru\/\d{1,}\/\?code\=[a-z0-9]{32}$");
            Regex regexWithId = new Regex(@"^https\:\/\/tboil\.spb\.ru\/\d{1,}$");
            Regex regexWithIdAndCode = new Regex(@"^https\:\/\/tboil\.spb\.ru\/\d{1,}\?code\=[a-z0-9]{32}$");

            if (!string.IsNullOrWhiteSpace(TbUrlString.Text) && regexWithCodeSlash.IsMatch(TbUrlString.Text))
            {
                if (TbUrlString.Text[TbUrlString.Text.Length - 39] == '/')
                {
                    for (int i = TbUrlString.Text.Length - 40; i > 0; i--)
                    {
                        if (char.IsDigit(TbUrlString.Text[i]))
                        {
                            text += TbUrlString.Text[i];
                        }
                        if (TbUrlString.Text[i] == '/')
                        {
                            char[] s = text.ToCharArray();
                            Array.Reverse(s);
                            text = new string(s);
                            break;
                        }
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(TbUrlString.Text) && regexWithIdAndCode.IsMatch(TbUrlString.Text))
            {
                if (TbUrlString.Text[TbUrlString.Text.Length - 38] == '?')
                {
                    for (int i = TbUrlString.Text.Length - 39; i > 0; i--)
                    {
                        if (char.IsDigit(TbUrlString.Text[i]))
                        {
                            text += TbUrlString.Text[i];
                        }
                        if (TbUrlString.Text[i] == '/')
                        {
                            char[] s = text.ToCharArray();
                            Array.Reverse(s);
                            text = new string(s);
                            break;
                        }
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(TbUrlString.Text) && regex.IsMatch(TbUrlString.Text))
            {
                if (TbUrlString.Text[TbUrlString.Text.Length - 1] == '/' && char.IsDigit(TbUrlString.Text[TbUrlString.Text.Length - 2]))
                {
                    endIndex = TbUrlString.Text.Length - 1;
                    text = TbUrlString.Text.Substring(startIndex, endIndex - startIndex);
                }
            }

            if (!string.IsNullOrWhiteSpace(TbUrlString.Text) && regexWithId.IsMatch(TbUrlString.Text))
            {
                for (int i = TbUrlString.Text.Length - 1; i > 0; i--)
                {
                    if (char.IsDigit(TbUrlString.Text[i]))
                    {
                        text += TbUrlString.Text[i];
                    }
                    if (TbUrlString.Text[i] == '/')
                    {
                        char[] s = text.ToCharArray();
                        Array.Reverse(s);
                        text = new string(s);
                        break;
                    }
                }
            }

            int userId = Convert.ToInt32(text);
            var user = await Entity.Processor.User.GetUserByIdAsync(userId);
            if (user != null)
            {
                email = user[0].email;
                surname = user[0].lastname;
                name = user[0].name;
            }
            var request = await Entity.Processor.Request.GetRequestsAsync(idEvent, userId);
            if (request == null)
            {
                await Entity.Processor.Request.Add(idEvent, userId, 1, 0);
                countAttendances++;
                PrintQr();
                labelAtt.Text = string.Format("Количество  посещений: {0}", countAttendances.ToString());
            }
            else
            {
                int reqId = request.requests[0].id;
                var updateResult = await Entity.Processor.Request.UpdateStatus(reqId, 1);
                countAttendances++;
                PrintQr();
                labelAtt.Text = string.Format("Количество  посещений: {0}", countAttendances.ToString());
            }
            TbUrlString.Text = string.Empty;
            text = string.Empty;
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
        private void timer_Tick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TbUrlString.Text))
            {
                TbUrlString.Text = string.Empty;
            }
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new CultureInfo("en-En"));
            this.ActiveControl = TbUrlString;
        }

        private void QrScanner_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer.Stop();
            captureDevice.Stop();
        }

       
    }
}
