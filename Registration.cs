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
    public partial class Registration : Form
    {
        readonly int idEvent;
        ToolTip toolTip;
        bool mtbReady = false;
        bool tbReady = false;
        public Registration(int idEvent)
        {
            InitializeComponent();
            this.idEvent = idEvent;
            toolTip = new ToolTip();
        }

        private void TbEmail_Validating(object sender, CancelEventArgs e)
        {
            
            var currentb = (sender as TextBox);
            switch (currentb.Name)
            {
                case "tbEmail":
                    if (!IsValidEmail(currentb.Text))
                    {
                        toolTip.Show("Неправильный формат электронной почты!", currentb, 5000);
                        e.Cancel = true;
                        return;
                    }
                    break;
                case "tbLastName":
                    if (string.IsNullOrWhiteSpace(currentb.Text))
                    {
                        toolTip.Show("Заполните данное обязательное поле!", currentb, 5000);
                        e.Cancel = true;
                        return;
                    }
                    break;
                case "tbName":
                    if (string.IsNullOrWhiteSpace(currentb.Text))
                    {
                        toolTip.Show("Заполните данное обязательное поле!", currentb, 5000);
                        e.Cancel = true;
                        return;
                    }
                    break;
                case "tbAddress":
                    if (string.IsNullOrWhiteSpace(currentb.Text))
                        if (string.IsNullOrWhiteSpace(currentb.Text))
                        {
                            toolTip.Show("Заполните данное обязательное поле!", currentb, 5000);
                            e.Cancel = true;
                            return;
                        }
                    break;
                case "tbCompany":
                    if (string.IsNullOrWhiteSpace(currentb.Text))
                        if (string.IsNullOrWhiteSpace(currentb.Text))
                        {
                            toolTip.Show("Заполните данное обязательное поле!", currentb, 5000);
                            e.Cancel = true;
                            return;
                        }
                    break;
            }
            tbReady = true;
        }
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        public static string FirstLetterToUpper(string s)
        {
            if (string.IsNullOrEmpty(s))
                return string.Empty;

            char[] a = s.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return new string(a);
        }
        private async void BtnRegister_Click(object sender, EventArgs e)
        {
            if(tbReady == true && mtbReady == true)
            {
                string name = FirstLetterToUpper(tbName.Text);
                string lastname = FirstLetterToUpper(tbLastName.Text);
                string middlename = "";
                if (!string.IsNullOrWhiteSpace(tbMiddleName.Text))
                {
                    middlename = FirstLetterToUpper(tbMiddleName.Text);
                }
                string email = tbEmail.Text;
                string birthday = mTbBirthDay.Text;
                string company = tbCompany.Text;
                string position = tbPosition.Text;
                string tel = mTbPhone.Text;
                bool addResult =  await Entity.Processor.User.Add(name, lastname, middlename, email, birthday, company, position, tel);
                if (addResult)
                {
                    Entity.Models.Users users = await Entity.Processor.User.GetUsersAsync(email);
                    if (users != null)
                    {
                        int userId = users.users[0].id;
                        string addMessage = await Entity.Processor.Request.Add(idEvent, userId, 1, 0);
                        MessageBox.Show(addMessage);
                        PrintQr();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Произошла ошибка при добавлении пользователя!");
                    }

                }
                else
                {
                    MessageBox.Show("Произошла ошибка при добавлении пользователя");
                }
            }
        }

        private void MTbBirthDay_Validating(object sender, CancelEventArgs e)
        {
            var currentb = (sender as MaskedTextBox);
            switch (currentb.Name)
            {
                case "mTbPhone":
                    if (!(currentb.MaskCompleted))
                    {
                        toolTip.Show("Неправильный формат номера телефона!", currentb, 5000);
                        e.Cancel = true;
                        return;
                    }
                    break;
                case "mTbBirthDay":
                    if (!(currentb.MaskCompleted))
                    {
                        toolTip.Show("Неправильный формат даты рождения!", currentb, 5000);
                        e.Cancel = true;
                        return;
                    }
                    break;
            }
            mtbReady = true;

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
                using (Stream s = wc.OpenRead("https://tboil.spb.ru/personal/user/profile/images/qr" + tbEmail.Text + ".png"))
                {
                    bitmapQROriginal = new Bitmap(s);
                }
            }
            Bitmap bitmapQR = new Bitmap(bitmapQROriginal, new Size(150, 150));
            e.Graphics.DrawImage(bitmapQR, 190, 20);
            bitmapQR.Dispose();
            e.Graphics.DrawString(tbName.Text.ToUpper(), new Font("Arial", 14, System.Drawing.FontStyle.Bold), Brushes.Black, (190 - (tbName.Text.Length * 14)) / 2, 70);
            e.Graphics.DrawString(tbLastName.Text.ToUpper(), new Font("Arial", 14, System.Drawing.FontStyle.Bold), Brushes.Black, (190 - (tbLastName.Text.Length * 14)) / 2, 100);
        }
    }
}