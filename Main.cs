using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tboil_v20._10._21.Entity.Processor;


namespace Tboil_v20._10._21
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            this.Load += Main_Load;
        }
        private Bitmap LoadBitmapFromUrl(string url)
        {
            if (url != null)
            {
                Bitmap bitmap;
                using (WebClient wc = new WebClient())
                {
                    using (Stream s = wc.OpenRead("https://tboil.spb.ru/" + url))
                    {
                        bitmap = new Bitmap(s);
                    }
                }
                return bitmap;
            }
            else
            {
                return null;
            }
        }
        private async void  Main_Load(object sender, EventArgs e)
        {
            string dateStart = DateTime.Now.Subtract(TimeSpan.FromDays(15)).ToShortDateString();
            string dateEnd = DateTime.Now.AddDays(3).ToShortDateString();

            this.Enabled = false;

            Entity.Models.Events eventsList = await Entity.Processor.Event.GetEventAsync(dateStart, dateEnd, 5);

            if (eventsList != null)
            {
                for (int i = 0; i < eventsList.events.Count; i++)
                {
                    Panel PanelEvent = new Panel()
                    {
                        Size = new Size(500, 500),
                        Margin = new Padding(100, 5, 5, 40),
                        BorderStyle = BorderStyle.FixedSingle,
                        BackColor = Color.White,
                    };
                    Button BtnRequest = new Button()
                    {
                        Size = new Size(230, 40),
                        BackColor = SystemColors.ActiveCaption,
                        ForeColor = Color.White,
                        Text = "Заявки",
                        Font = new Font("Century Gothic", 12, FontStyle.Bold),
                        Tag = eventsList.events[i].id
                    };
                    Panel PanelEventHeader = new Panel()
                    {
                        Width = PanelEvent.Width,
                        Height = (int)(PanelEvent.Height * 0.3),
                        BorderStyle = BorderStyle.FixedSingle,
                        BackColor = SystemColors.ActiveCaption,

                    };
                    Panel PanelEventFooter = new Panel()
                    {
                        Width = PanelEvent.Width,
                        Height = (int)(PanelEvent.Height * 0.7),
                        BorderStyle = BorderStyle.Fixed3D,
                        Top = PanelEventHeader.Height,

                    };
                    PictureBox PictureboxEvent = new PictureBox()
                    {
                        Size = new Size(370, 220),
                        SizeMode = PictureBoxSizeMode.Zoom,
                        Image = LoadBitmapFromUrl(eventsList.events[i].photo)
                    };
                    Label LabelEventName = new Label()
                    {
                        TextAlign = ContentAlignment.MiddleCenter,
                        Text = eventsList.events[i].name,
                        Font = new Font("Century Gothic", 15, FontStyle.Bold),
                        Size = new Size(410, 120),
                        ForeColor = Color.White,
                    };
                    BtnRequest.Left = (PanelEventFooter.Width / 2) - (BtnRequest.Width / 2);
                    BtnRequest.Top = (int)(PanelEventFooter.Height * 0.85);

                    PictureboxEvent.Left = (PanelEventFooter.Width / 2) - (PictureboxEvent.Width / 2);
                    PictureboxEvent.Top = (int)(PanelEventFooter.Height * 0.10);

                    LabelEventName.Left = (PanelEvent.Width / 2) - (LabelEventName.Width / 2);
                    LabelEventName.Top = (int)(PanelEvent.Height * 0.05);

                    BtnRequest.Click += BtnShowRequests;

                    PanelEvent.Controls.Add(PanelEventHeader);
                    PanelEvent.Controls.Add(PanelEventFooter);

                    PanelEventFooter.Controls.Add(BtnRequest);
                    PanelEventFooter.Controls.Add(PictureboxEvent);
                    PanelEventHeader.Controls.Add(LabelEventName);

                    flowLayoutPanel.Controls.Add(PanelEvent);

                }

                this.Enabled = true;
            }
        }
        private void BtnShowRequests(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            int eventId = Convert.ToInt32(button.Tag);
            new Requests(eventId).ShowDialog();
        }
        private void BtnTboilEntance_Click(object sender, EventArgs e)
        {
            new TboilEntrance().ShowDialog();
        }

        private void BtnTboilEntrance_Click(object sender, EventArgs e)
        {
            new TboilEntrance().ShowDialog();
        }
    }
}
