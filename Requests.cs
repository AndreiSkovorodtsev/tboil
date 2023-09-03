using AForge.Video.DirectShow;
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
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using Tboil_v20._10._21.Entity.Processor;

namespace Tboil_v20._10._21
{
    public partial class Requests : Form
    {
        private FilterInfoCollection filterInfoCollection;
        private VideoCaptureDevice captureDevice;
        private ToolTip toolTip;
        readonly int idEvent;
        bool sendToTboil;
        public Requests(int idEvent)
        {
            toolTip = new ToolTip();
            
            InitializeComponent();
            this.AcceptButton = this.BtnSearch;
            this.idEvent = idEvent;
            this.Load += Requests_Load;
        }
        private async Task LoadRequests()
        {
            flowLayoutPanel.Controls.Clear();
            panelUserProfile.Enabled = false;
            flowLayoutPanel.Enabled = false;
            Entity.Models.Requests requestsList = await Entity.Processor.Request.GetRequestsAsync(idEvent, 1, 0);
            if (requestsList != null)
            {
                var data = requestsList.requests.Select(x => new { x.id, user = x.user.lastname + " " + x.user.name, x.user.email, phone = x.user.tel, x.user.photo }).ToList();
                for (int i = 0; i < data.Count; i++)
                {
                    ExtendedPanel extendedPanel = new ExtendedPanel()
                    {
                        Opacity = 50,
                        Size = new Size(300, 100),
                    };
                    Panel PanelMainRequest = new Panel()
                    {
                        Size = new Size(300, 300),
                        Margin = new Padding(40, 20, 20, 0),
                        BackColor = Color.White,
                    };

                    PictureBox PictureboxUser = new PictureBox()
                    {
                        Size = new Size(300, 300),
                        SizeMode = PictureBoxSizeMode.Zoom,
                        Image = data[i].photo != null ? LoadBitmapFromUrl("https://tboil.spb.ru/" + data[i].photo) : Properties.Resources.userProfileIcon
                    };
                    Label LabelUserName = new Label()
                    {
                        TextAlign = ContentAlignment.MiddleCenter,
                        Text = data[i].user ?? "-",
                        Font = new Font("Century Gothic", 12, System.Drawing.FontStyle.Bold),
                        Size = new Size(300, 30),
                        ForeColor = Color.White,
                        BackColor = System.Drawing.Color.Transparent
                    };
                    Label LabelUserEmail = new Label()
                    {
                        TextAlign = ContentAlignment.MiddleCenter,
                        Text = data[i].email ?? "-",
                        Font = new Font("Century Gothic", 11),
                        Size = new Size(300, 30),
                        ForeColor = Color.White,
                        BackColor = System.Drawing.Color.Transparent

                    };
                    Label LabelUserPhone = new Label()
                    {
                        TextAlign = ContentAlignment.MiddleCenter,
                        Text = data[i].phone ?? "-",
                        Font = new Font("Century Gothic", 11),
                        Size = new Size(300, 30),
                        ForeColor = Color.White,
                        BackColor = System.Drawing.Color.Transparent

                    };

                    PictureboxUser.Left = (PanelMainRequest.Width / 2) - (PictureboxUser.Width / 2);
                    PictureboxUser.Top = (int)(PanelMainRequest.Height * 0.05);
                    PictureboxUser.MouseHover += PictureboxUser_MouseHover;

                    extendedPanel.Left = (PictureboxUser.Width / 2) - (extendedPanel.Width / 2);
                    extendedPanel.Top = (int)(PictureboxUser.Height * 0.65);
                    extendedPanel.Hide();

                    LabelUserName.Left = (extendedPanel.Width / 2) - (LabelUserName.Width / 2);
                    LabelUserName.Top = (int)(extendedPanel.Height * 0.1);

                    LabelUserEmail.Left = (extendedPanel.Width / 2) - (LabelUserEmail.Width / 2);
                    LabelUserEmail.Top = (int)(extendedPanel.Height * 0.3);

                    LabelUserPhone.Left = (extendedPanel.Width / 2) - (LabelUserPhone.Width / 2);
                    LabelUserPhone.Top = (int)(extendedPanel.Height * 0.5);

                    PanelMainRequest.Controls.Add(PictureboxUser);

                    extendedPanel.Controls.Add(LabelUserName);
                    extendedPanel.Controls.Add(LabelUserEmail);
                    extendedPanel.Controls.Add(LabelUserPhone);

                    PictureboxUser.Controls.Add(extendedPanel);

                    flowLayoutPanel.Controls.Add(PanelMainRequest);

                }

                flowLayoutPanel.Enabled = true;
            }
        }
        private async void Requests_Load(object sender, EventArgs e)
        {
            await LoadRequests();
            //flowLayoutPanel.AutoScrollPosition = new Point(0, flowLayoutPanel.Height);
        }

        private void PictureboxUser_MouseHover(object sender, EventArgs e)
        {
            PictureBox pbUser = (PictureBox)sender;
            if (pbUser.HasChildren)
            {
                foreach (Control childControl in pbUser.Controls)
                {
                    var type = childControl.GetType();
                    if (type.BaseType.Name == "Panel")
                    {
                        childControl.Show();
                    }

                }
                var children = pbUser.Controls.OfType<Control>();
            }
        }

        private Bitmap LoadBitmapFromUrl(string url)
        {
            if (url != null)
            {
                Bitmap bitmap;
                using (WebClient wc = new WebClient())
                {
                    using (Stream s = wc.OpenRead( url))
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
        public async Task LoadRequestsAsync()
        {
            var res = await Request.GetRequestsAsync(idEvent, 1, 0);

            if (res != null)
            {
                var data = res.requests.Select(x => new { x.id, user = x.user.lastname + " " + x.user.name, x.user.email, phone = x.user.tel }).ToList();
            }

        }

        private async void BtnSearch_Click(object sender, EventArgs e)
        {
            if (textBoxSearch.Text.Length > 2)
            {
                DgvUsersLeader.DataSource = null;
                DgvUsersTboil.DataSource = null;

                await LoadUsersAsync(textBoxSearch.Text);
                await LoadLeaderUsersAsync(textBoxSearch.Text);

                if (DgvUsersTboil.DataSource == null && DgvUsersLeader.Rows.Count == 0)
                {
                    DialogResult dialogResult = MessageBox.Show("Пользователь не найден! Желайте зарегистрировать", "Необходимость регистрации", MessageBoxButtons.YesNo);

                    if (dialogResult == DialogResult.Yes)
                    {
                        new Registration(idEvent).ShowDialog();
                        await LoadRequestsAsync();
                    }
                }
            }

        }
        public async Task LoadUsersAsync(string search)
        {
            var res = await Entity.Processor.User.GetUsersAsync(search);

            if (res != null)
            {
                DgvUsersTboil.DataSource = res.users.Select(x => new { x.id, user = x.lastname + " " + x.name + " " + x.middlename, x.email, x.tel }).ToList();
                if (DgvUsersTboil.DataSource != null)
                {
                    DgvUsersTboil.Columns[0].Visible = false;
                    DgvUsersTboil.Columns[1].HeaderText = "Фио";
                    DgvUsersTboil.Columns[2].HeaderText = "Почта";
                    DgvUsersTboil.Columns[3].HeaderText = "Телефон";
                }
            }

        }

        public async Task LoadLeaderUsersAsync(string search)
        {
            var res = await Entity.Processor.UserLeader.GetUsersAsync(search);

            if (res != null)
            {
                DgvUsersLeader.DataSource = res.Data.Select(x => new { id = x.Id, user = x.LastName + " " + x.FirstName + " " + x.FatherName, email = x.Email, birthday = Convert.ToDateTime(x.Birthday).ToString("dd.MM.yyyy"), company = x.CompanyName, position = x.Position,  x.LastName,  x.FirstName,  x.FatherName }).ToList();
                if (DgvUsersLeader.DataSource != null)
                {
                    DgvUsersLeader.Columns[0].Visible = false;
                    DgvUsersLeader.Columns[1].HeaderText = "Фио";
                    DgvUsersLeader.Columns[2].HeaderText = "Почта";
                    DgvUsersLeader.Columns[3].Visible = false;
                    DgvUsersLeader.Columns[4].Visible = false;
                    DgvUsersLeader.Columns[5].Visible = false;
                    DgvUsersLeader.Columns[6].Visible = false;
                    DgvUsersLeader.Columns[7].Visible = false;
                    DgvUsersLeader.Columns[8].Visible = false;
                }
            }
        }

        private void TextBoxSearch_Enter_1(object sender, EventArgs e)
        {
            if (textBoxSearch.Text == "Фамилия + Имя | Email")
            {
                textBoxSearch.Text = string.Empty;
            }
        }

        private async void BtnRegistration_Click(object sender, EventArgs e)
        {
            new Registration(idEvent).ShowDialog();
            await LoadRequests();
        }

        private async void BtnQrScanner_Click(object sender, EventArgs e)
        {
            new QrCamera(idEvent).ShowDialog();
            await LoadRequests();
        }

        private async void BtnQrScanner_Click_1(object sender, EventArgs e)
        {
            new QrScanner(idEvent).ShowDialog();
            await LoadRequests();
        }

        private void TextBoxSearch_Click(object sender, EventArgs e)
        {
            if (textBoxSearch.Text == "Фамилия + Имя | Email")
            {
                textBoxSearch.Text = string.Empty;
            }
        }

        private async void DgvUsersTboil_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            panelUserProfile.Enabled = false;
            if (DgvUsersTboil.Rows.Count != 0)
            {
                if (DgvUsersTboil.SelectedCells.Count > 0)
                {
                    int idUserTboil = Convert.ToInt32(DgvUsersTboil.SelectedRows[0].Cells[0].Value);
                    var user = await Entity.Processor.User.GetUserByIdAsync(idUserTboil);

                    if (user != null)
                    {
                        tbEmail.Text = user[0].email;
                        tbLastName.Text = user[0].lastname;
                        tbName.Text = user[0].name;
                        tbCompany.Text = user[0].work_company;
                        tbPosition.Text = user[0].profession;
                        tbMiddleName.Text = user[0].middlename;
                        mTbBirthDay.Text = user[0].birthday;
                        mTbPhone.Text = user[0].tel;
                        pbUserProfile.Image = user[0].photo != null ? LoadBitmapFromUrl("https://tboil.spb.ru/" + user[0].photo) : Properties.Resources.userProfileIcon;
                    }
                }
            }
            sendToTboil = true;
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (sendToTboil)
            {
                if (DgvUsersLeader.SelectedRows.Count > 0 || DgvUsersLeader.SelectedRows.Count > 0)
                {
                    filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                    if (filterInfoCollection.Count > 0)
                    {
                        captureDevice = new VideoCaptureDevice(filterInfoCollection[0].MonikerString);
                    }
                    else
                    {
                        MessageBox.Show("Не подключена веб-камера. Проверьте настройки!");
                    }
                    panelUserProfile.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Выберите запись для редактирования профиля!");
                }
            }
        }

        private void PbUserProfile_DoubleClick(object sender, EventArgs e)
        {
            if(captureDevice != null)
            {
                captureDevice.NewFrame += CaptureDevice_NewFrame; ;
                captureDevice.Start();
            }
        }

        private void CaptureDevice_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            pbUserProfile.Image = (Bitmap)eventArgs.Frame.Clone();
        }
        private async void SendTboilRequest()
        {
            if (DgvUsersTboil.SelectedCells.Count > 0)
            {
                int idUserTboil = Convert.ToInt32(DgvUsersTboil.SelectedRows[0].Cells[0].Value);
                Entity.Models.Requests requests = await Entity.Processor.Request.GetRequestsAsync(idEvent, idUserTboil);

                if (requests == null)
                {
                    //добавляем заявку
                    string additionResult = await Entity.Processor.Request.Add(idEvent, idUserTboil, 1, 0);

                    MessageBox.Show(additionResult);

                    if (checkBoxPrintQr.Checked)
                    {
                        List<Entity.Models.User> user = await Entity.Processor.User.GetUserByIdAsync(idUserTboil);
                        Badge badge = new Badge(user[0].name, user[0].lastname, user[0].email);
                        badge.Print();
                    }

                    await LoadRequests();

                }
                else
                {
                    //обновляем статус заявки
                    int requestId = requests.requests[0].id;
                    string updateResult = await Entity.Processor.Request.UpdateStatus(requestId, 1);

                    MessageBox.Show(updateResult);

                    if (checkBoxPrintQr.Checked)
                    {
                        string email = DgvUsersTboil.SelectedRows[0].Cells[2].Value.ToString();
                        string[] fioArr = DgvUsersTboil.SelectedRows[0].Cells[1].Value.ToString().Split(new string[] { " " }, StringSplitOptions.None);

                        if (fioArr.Length > 1)
                        {
                            string surname = fioArr[0];
                            string name = fioArr[1];

                            Badge badge = new Badge(surname, name, email);
                            badge.Print();
                        }

                    }

                    await LoadRequests();
                }
            }
        }
        private async void SendLeaderRequest()
        {
            Entity.Models.UserLeader userLeader = new Entity.Models.UserLeader()
            {
                Email = DgvUsersLeader.SelectedRows[0].Cells[2].Value.ToString(),
                LastName = DgvUsersLeader.SelectedRows[0].Cells[6].Value.ToString(),
                FirstName = DgvUsersLeader.SelectedRows[0].Cells[7].Value.ToString(),
                Birthday = DgvUsersLeader.SelectedRows[0].Cells[3].Value.ToString(),
                FatherName = DgvUsersLeader.SelectedRows[0].Cells[8].Value.ToString(),
                CompanyName = DgvUsersLeader.SelectedRows[0].Cells[4].Value.ToString(),
                Position = DgvUsersLeader.SelectedRows[0].Cells[5].Value.ToString(),
            };

            Entity.Models.Users users = await Entity.Processor.User.GetUsersAsync(userLeader.Email);

            if (users == null)
            {
                //регистрируем пользователя и создаем заявку

                await Entity.Processor.User.Add(userLeader.FirstName, userLeader.LastName, userLeader.FatherName, userLeader.Email, userLeader.Birthday, userLeader.CompanyName, userLeader.Position);
                users = await Entity.Processor.User.GetUsersAsync(userLeader.Email);
                string additionRequestResult = await Entity.Processor.Request.Add(idEvent, users.users[0].id, 1, 0);

                MessageBox.Show(additionRequestResult);

                if (checkBoxPrintQr.Checked)
                {
                    Badge badge = new Badge(users.users[0].name, users.users[0].lastname, users.users[0].email);
                    badge.Print();
                }

                await LoadRequests();
            }
            else
            {
                //обновляем статус заявки
                Entity.Models.Requests requests = await Entity.Processor.Request.GetRequestsAsync(idEvent, users.users[0].id);
                if (requests == null)
                {
                    string additionRequestResult = await Entity.Processor.Request.Add(idEvent, users.users[0].id, 1, 0);
                    if (checkBoxPrintQr.Checked)
                    {
                        Badge badge = new Badge(users.users[0].name, users.users[0].lastname, users.users[0].email);
                        badge.Print();
                    }
                    MessageBox.Show(additionRequestResult);

                }
                else
                {
                    var updateRequestResult = await Entity.Processor.Request.UpdateStatus(requests.requests[0].id, 1);
                    if (checkBoxPrintQr.Checked)
                    {
                        Badge badge = new Badge(users.users[0].name, users.users[0].lastname, users.users[0].email);
                        badge.Print();
                    }
                    MessageBox.Show(updateRequestResult);

                }
                await LoadRequests();
            }
        }
        private void BtnRegister_Click(object sender, EventArgs e)
        {
            //if (!Common.IsValidEmail(tbEmail.Text))
            //{
            //    toolTip.Show("Неправильный формат электронной почты!", tbEmail, 5000);
            //    return;
            //}
            //if (string.IsNullOrWhiteSpace(tbLastName.Text))
            //{
            //    toolTip.Show("Заполните данное обязательное поле!", tbLastName, 5000);
            //    return;
            //}
            //if (string.IsNullOrWhiteSpace(tbName.Text))
            //{
            //    toolTip.Show("Заполните данное обязательное поле!", tbName, 5000);
            //    return;
            //}
            //if (string.IsNullOrWhiteSpace(tbCompany.Text))
            //{
            //    toolTip.Show("Заполните данное обязательное поле!", tbCompany, 5000);
            //    return;
            //}
            //if (!mTbBirthDay.MaskCompleted)
            //{
            //    toolTip.Show("Неправильный формат номера телефона!", mTbBirthDay, 5000);
            //    return;
            //}
            //if (!mTbPhone.MaskCompleted)
            //{
            //    toolTip.Show("Неправильный формат номера телефона!", mTbPhone, 5000);
            //    return;
            //}

            if (sendToTboil)
            {
                SendTboilRequest();
            }
            else
            {
                SendLeaderRequest();
            }
        }

        private void PbUserProfile_Click(object sender, EventArgs e)
        {
            if(captureDevice != null)
            { 
                if(captureDevice.IsRunning && pbUserProfile.Image != null)
                {
                    captureDevice.Stop();
                }
            }
        }

        private async void DgvUsersLeader_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            panelUserProfile.Enabled = false;
            if (DgvUsersLeader.Rows.Count != 0)
            {
                if (DgvUsersLeader.SelectedCells.Count > 0)
                {
                    int idUserLeader = Convert.ToInt32(DgvUsersLeader.SelectedRows[0].Cells[0].Value);
                    var user = await Entity.Processor.UserLeader.GetUsersAsync(idUserLeader.ToString());

                    if (user != null)
                    {
                        tbEmail.Text = user.Data[0].Email;
                        tbLastName.Text = user.Data[0].LastName;
                        tbName.Text = user.Data[0].FirstName;
                        tbCompany.Text = user.Data[0].CompanyName;
                        tbPosition.Text = user.Data[0].Position;
                        tbMiddleName.Text = user.Data[0].FatherName;
                        mTbBirthDay.Text = user.Data[0].Birthday;
                       // mTbPhone.Text = user.Data[0].t;
                        pbUserProfile.Image = LoadBitmapFromUrl(user.Data[0].Photo) ?? Properties.Resources.userProfileIcon;
                    }
                }
            }
            sendToTboil = false;
        }

        private async void BtnQrCammera_Click(object sender, EventArgs e)
        {
            new QrCamera(idEvent).ShowDialog();
            await LoadRequests();
        }
    }
}