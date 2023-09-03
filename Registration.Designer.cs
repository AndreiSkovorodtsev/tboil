
namespace Tboil_v20._10._21
{
    partial class Registration
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbEmail = new System.Windows.Forms.TextBox();
            this.labelEmail = new System.Windows.Forms.Label();
            this.labelLastName = new System.Windows.Forms.Label();
            this.tbLastName = new System.Windows.Forms.TextBox();
            this.labelName = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.labelBirthDay = new System.Windows.Forms.Label();
            this.mTbBirthDay = new System.Windows.Forms.MaskedTextBox();
            this.mTbPhone = new System.Windows.Forms.MaskedTextBox();
            this.labelPhone = new System.Windows.Forms.Label();
            this.labelPosition = new System.Windows.Forms.Label();
            this.tbPosition = new System.Windows.Forms.TextBox();
            this.btnRegister = new System.Windows.Forms.Button();
            this.labelMiddleName = new System.Windows.Forms.Label();
            this.tbMiddleName = new System.Windows.Forms.TextBox();
            this.labelCompany = new System.Windows.Forms.Label();
            this.tbCompany = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tbEmail
            // 
            this.tbEmail.Font = new System.Drawing.Font("Century Gothic", 10F);
            this.tbEmail.Location = new System.Drawing.Point(22, 69);
            this.tbEmail.Margin = new System.Windows.Forms.Padding(2);
            this.tbEmail.Name = "tbEmail";
            this.tbEmail.Size = new System.Drawing.Size(178, 24);
            this.tbEmail.TabIndex = 0;
            this.tbEmail.Validating += new System.ComponentModel.CancelEventHandler(this.TbEmail_Validating);
            // 
            // labelEmail
            // 
            this.labelEmail.AutoSize = true;
            this.labelEmail.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelEmail.Location = new System.Drawing.Point(19, 31);
            this.labelEmail.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelEmail.Name = "labelEmail";
            this.labelEmail.Size = new System.Drawing.Size(163, 21);
            this.labelEmail.TabIndex = 1;
            this.labelEmail.Text = "Электронная почта";
            // 
            // labelLastName
            // 
            this.labelLastName.AutoSize = true;
            this.labelLastName.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelLastName.Location = new System.Drawing.Point(19, 126);
            this.labelLastName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelLastName.Name = "labelLastName";
            this.labelLastName.Size = new System.Drawing.Size(85, 21);
            this.labelLastName.TabIndex = 3;
            this.labelLastName.Text = "Фамилия";
            // 
            // tbLastName
            // 
            this.tbLastName.Font = new System.Drawing.Font("Century Gothic", 10F);
            this.tbLastName.Location = new System.Drawing.Point(22, 164);
            this.tbLastName.Margin = new System.Windows.Forms.Padding(2);
            this.tbLastName.Name = "tbLastName";
            this.tbLastName.Size = new System.Drawing.Size(178, 24);
            this.tbLastName.TabIndex = 1;
            this.tbLastName.Validating += new System.ComponentModel.CancelEventHandler(this.TbEmail_Validating);
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelName.Location = new System.Drawing.Point(19, 217);
            this.labelName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(43, 21);
            this.labelName.TabIndex = 5;
            this.labelName.Text = "Имя";
            // 
            // tbName
            // 
            this.tbName.Font = new System.Drawing.Font("Century Gothic", 10F);
            this.tbName.Location = new System.Drawing.Point(22, 255);
            this.tbName.Margin = new System.Windows.Forms.Padding(2);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(178, 24);
            this.tbName.TabIndex = 2;
            this.tbName.Validating += new System.ComponentModel.CancelEventHandler(this.TbEmail_Validating);
            // 
            // labelBirthDay
            // 
            this.labelBirthDay.AutoSize = true;
            this.labelBirthDay.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelBirthDay.Location = new System.Drawing.Point(270, 304);
            this.labelBirthDay.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelBirthDay.Name = "labelBirthDay";
            this.labelBirthDay.Size = new System.Drawing.Size(132, 21);
            this.labelBirthDay.TabIndex = 7;
            this.labelBirthDay.Text = "Дата рождения";
            // 
            // mTbBirthDay
            // 
            this.mTbBirthDay.Font = new System.Drawing.Font("Century Gothic", 10F);
            this.mTbBirthDay.Location = new System.Drawing.Point(273, 339);
            this.mTbBirthDay.Margin = new System.Windows.Forms.Padding(2);
            this.mTbBirthDay.Mask = "00/00/0000";
            this.mTbBirthDay.Name = "mTbBirthDay";
            this.mTbBirthDay.Size = new System.Drawing.Size(178, 24);
            this.mTbBirthDay.TabIndex = 4;
            this.mTbBirthDay.Validating += new System.ComponentModel.CancelEventHandler(this.MTbBirthDay_Validating);
            // 
            // mTbPhone
            // 
            this.mTbPhone.Font = new System.Drawing.Font("Century Gothic", 10F);
            this.mTbPhone.Location = new System.Drawing.Point(274, 67);
            this.mTbPhone.Margin = new System.Windows.Forms.Padding(2);
            this.mTbPhone.Mask = "+7(000)0000000";
            this.mTbPhone.Name = "mTbPhone";
            this.mTbPhone.Size = new System.Drawing.Size(178, 24);
            this.mTbPhone.TabIndex = 6;
            this.mTbPhone.Validating += new System.ComponentModel.CancelEventHandler(this.MTbBirthDay_Validating);
            // 
            // labelPhone
            // 
            this.labelPhone.AutoSize = true;
            this.labelPhone.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelPhone.Location = new System.Drawing.Point(271, 31);
            this.labelPhone.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelPhone.Name = "labelPhone";
            this.labelPhone.Size = new System.Drawing.Size(153, 21);
            this.labelPhone.TabIndex = 9;
            this.labelPhone.Text = "Номер телефона";
            // 
            // labelPosition
            // 
            this.labelPosition.AutoSize = true;
            this.labelPosition.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelPosition.Location = new System.Drawing.Point(270, 209);
            this.labelPosition.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelPosition.Name = "labelPosition";
            this.labelPosition.Size = new System.Drawing.Size(96, 21);
            this.labelPosition.TabIndex = 14;
            this.labelPosition.Text = "Должность";
            // 
            // tbPosition
            // 
            this.tbPosition.Font = new System.Drawing.Font("Century Gothic", 10F);
            this.tbPosition.Location = new System.Drawing.Point(274, 247);
            this.tbPosition.Margin = new System.Windows.Forms.Padding(2);
            this.tbPosition.Name = "tbPosition";
            this.tbPosition.Size = new System.Drawing.Size(178, 24);
            this.tbPosition.TabIndex = 8;
            // 
            // btnRegister
            // 
            this.btnRegister.Location = new System.Drawing.Point(158, 404);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(153, 31);
            this.btnRegister.TabIndex = 9;
            this.btnRegister.Text = "Отправить";
            this.btnRegister.UseVisualStyleBackColor = true;
            this.btnRegister.Click += new System.EventHandler(this.BtnRegister_Click);
            // 
            // labelMiddleName
            // 
            this.labelMiddleName.AutoSize = true;
            this.labelMiddleName.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelMiddleName.Location = new System.Drawing.Point(19, 304);
            this.labelMiddleName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelMiddleName.Name = "labelMiddleName";
            this.labelMiddleName.Size = new System.Drawing.Size(80, 21);
            this.labelMiddleName.TabIndex = 17;
            this.labelMiddleName.Text = "Отчество";
            // 
            // tbMiddleName
            // 
            this.tbMiddleName.Font = new System.Drawing.Font("Century Gothic", 10F);
            this.tbMiddleName.Location = new System.Drawing.Point(22, 342);
            this.tbMiddleName.Margin = new System.Windows.Forms.Padding(2);
            this.tbMiddleName.Name = "tbMiddleName";
            this.tbMiddleName.Size = new System.Drawing.Size(178, 24);
            this.tbMiddleName.TabIndex = 3;
            // 
            // labelCompany
            // 
            this.labelCompany.AutoSize = true;
            this.labelCompany.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelCompany.Location = new System.Drawing.Point(270, 122);
            this.labelCompany.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelCompany.Name = "labelCompany";
            this.labelCompany.Size = new System.Drawing.Size(117, 21);
            this.labelCompany.TabIndex = 19;
            this.labelCompany.Text = "Организация";
            // 
            // tbCompany
            // 
            this.tbCompany.Font = new System.Drawing.Font("Century Gothic", 10F);
            this.tbCompany.Location = new System.Drawing.Point(274, 160);
            this.tbCompany.Margin = new System.Windows.Forms.Padding(2);
            this.tbCompany.Name = "tbCompany";
            this.tbCompany.Size = new System.Drawing.Size(178, 24);
            this.tbCompany.TabIndex = 7;
            this.tbCompany.Validating += new System.ComponentModel.CancelEventHandler(this.TbEmail_Validating);
            // 
            // Registration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(479, 469);
            this.Controls.Add(this.labelCompany);
            this.Controls.Add(this.tbCompany);
            this.Controls.Add(this.labelMiddleName);
            this.Controls.Add(this.tbMiddleName);
            this.Controls.Add(this.btnRegister);
            this.Controls.Add(this.labelPosition);
            this.Controls.Add(this.tbPosition);
            this.Controls.Add(this.mTbPhone);
            this.Controls.Add(this.labelPhone);
            this.Controls.Add(this.mTbBirthDay);
            this.Controls.Add(this.labelBirthDay);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.labelLastName);
            this.Controls.Add(this.tbLastName);
            this.Controls.Add(this.labelEmail);
            this.Controls.Add(this.tbEmail);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Registration";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Регистрация";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbEmail;
        private System.Windows.Forms.Label labelEmail;
        private System.Windows.Forms.Label labelLastName;
        private System.Windows.Forms.TextBox tbLastName;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label labelBirthDay;
        private System.Windows.Forms.MaskedTextBox mTbBirthDay;
        private System.Windows.Forms.MaskedTextBox mTbPhone;
        private System.Windows.Forms.Label labelPhone;
        private System.Windows.Forms.Label labelPosition;
        private System.Windows.Forms.TextBox tbPosition;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.Label labelMiddleName;
        private System.Windows.Forms.TextBox tbMiddleName;
        private System.Windows.Forms.Label labelCompany;
        private System.Windows.Forms.TextBox tbCompany;
    }
}