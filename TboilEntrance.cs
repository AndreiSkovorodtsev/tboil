using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tboil_v20._10._21
{
    public partial class TboilEntrance : Form
    {
        public TboilEntrance()
        {
            InitializeComponent();
            this.Load += TboilEntrance_Load;

        }

        private async void TboilEntrance_Load(object sender, EventArgs e)
        {
            labelCurrentDate.Text = DateTime.Now.ToLongDateString();
            this.AcceptButton = this.BtnSearch;
            await LoadRequestsAsync();
        }

        public async Task LoadRequestsAsync()
        {
            var res = await Entity.Processor.TboilRequest.GetRequestsAsync(DateTime.Now.ToShortDateString(), DateTime.Now.ToShortDateString());

            if (res != null)
            {
                DgvTboilRequest.DataSource = res.requests.Select(x => new { id = x.id, user = x.user.lastname + " " + x.user.name, date = Convert.ToDateTime(x.date).ToShortTimeString() }).ToList();
                if (DgvTboilRequest.DataSource != null)
                {
                    DgvTboilRequest.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
                    DgvTboilRequest.Columns[0].Visible = false;
                    DgvTboilRequest.Columns[1].HeaderText = "Пользователь";
                    DgvTboilRequest.Columns[2].HeaderText = "Время прохода";

                }
            }

        }

        private async void BtnSearch_Click(object sender, EventArgs e)
        {
            if (textBoxSearch.Text.Length > 2)
            {
                DgvUsersTboil.DataSource = null;

                if (this.IsValidEmail(textBoxSearch.Text))
                {
                    await LoadUsersAsync("", textBoxSearch.Text);
                }
                else
                {
                    await LoadUsersAsync(textBoxSearch.Text, "");
                }
            }
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
        public async Task LoadUsersAsync(string search, string email)
        {

            var res = await Entity.Processor.User.GetUsersAsync(search);

            if (res != null)
            {
                DgvUsersTboil.DataSource = res.users.Select(x => new { id = x.id, user = x.lastname + " " + x.name + " " + x.middlename, email = x.email, phone = x.tel }).ToList();
                if (DgvUsersTboil.DataSource != null)
                {
                    DgvUsersTboil.Columns[0].Visible = false;
                    DgvUsersTboil.Columns[1].HeaderText = "Фио";
                    DgvUsersTboil.Columns[2].HeaderText = "Почта";
                    DgvUsersTboil.Columns[3].HeaderText = "Телефон";
                }
            }

        }

        private void textBoxSearch_Enter(object sender, EventArgs e)
        {
            if (textBoxSearch.Text == "Фамилия + Имя | Email")
            {
                textBoxSearch.Text = string.Empty;
            }
        }

        private void DgvTboilRequest_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            labelTotalCountRequests.Text = string.Format("Всего: {0}", DgvTboilRequest.Rows.Count);
        }

        private async void DgvUsersTboil_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DgvUsersTboil.Rows.Count != 0)
            {
                if (DgvUsersTboil.SelectedCells.Count > 0)
                {
                    int idUser = Convert.ToInt32(DgvUsersTboil.SelectedRows[0].Cells[0].Value);
                    bool addResult = await Entity.Processor.TboilRequest.Add(idUser, "");
                    if(!addResult)
                    {
                        MessageBox.Show("Произошла ошибка при выполнении запроса!");
                    }
                    else
                    {
                        MessageBox.Show("Заявка успешно создана");
                        await LoadRequestsAsync();
                    }
                }
            }
        }
    }
}
