namespace FamilyFinance
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.name = new System.Windows.Forms.Label();
            this.password = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.remeber = new System.Windows.Forms.CheckBox();
            this.btnRegist = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.lblPasswordError = new System.Windows.Forms.Label();
            this.lblNameError = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblAllError = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // name
            // 
            this.name.AutoSize = true;
            this.name.Font = new System.Drawing.Font("STCaiyun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.name.Location = new System.Drawing.Point(60, 104);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(62, 17);
            this.name.TabIndex = 1;
            this.name.Text = "用户名:";
            // 
            // password
            // 
            this.password.AutoSize = true;
            this.password.Font = new System.Drawing.Font("STCaiyun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.password.Location = new System.Drawing.Point(60, 140);
            this.password.Name = "password";
            this.password.Size = new System.Drawing.Size(46, 17);
            this.password.TabIndex = 1;
            this.password.Text = "密码:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(137, 104);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(135, 21);
            this.txtName.TabIndex = 1;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(137, 140);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(135, 21);
            this.txtPassword.TabIndex = 2;
            // 
            // remeber
            // 
            this.remeber.AutoSize = true;
            this.remeber.Font = new System.Drawing.Font("SimSun", 10F);
            this.remeber.Location = new System.Drawing.Point(134, 179);
            this.remeber.Name = "remeber";
            this.remeber.Size = new System.Drawing.Size(138, 18);
            this.remeber.TabIndex = 3;
            this.remeber.Text = "记住用户名和密码";
            this.remeber.UseVisualStyleBackColor = true;
            // 
            // btnRegist
            // 
            this.btnRegist.Location = new System.Drawing.Point(197, 222);
            this.btnRegist.Name = "btnRegist";
            this.btnRegist.Size = new System.Drawing.Size(75, 29);
            this.btnRegist.TabIndex = 5;
            this.btnRegist.Text = "注册";
            this.btnRegist.UseVisualStyleBackColor = true;
            this.btnRegist.Click += new System.EventHandler(this.btnRegist_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(63, 222);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(74, 29);
            this.btnLogin.TabIndex = 4;
            this.btnLogin.Text = "登录";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // lblPasswordError
            // 
            this.lblPasswordError.AutoSize = true;
            this.lblPasswordError.Image = global::FamilyFinance.Properties.Resources.check_error;
            this.lblPasswordError.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblPasswordError.Location = new System.Drawing.Point(278, 145);
            this.lblPasswordError.Name = "lblPasswordError";
            this.lblPasswordError.Size = new System.Drawing.Size(95, 12);
            this.lblPasswordError.TabIndex = 8;
            this.lblPasswordError.Text = "   密码不能为空";
            this.lblPasswordError.Visible = false;
            // 
            // lblNameError
            // 
            this.lblNameError.AutoSize = true;
            this.lblNameError.Image = global::FamilyFinance.Properties.Resources.check_error;
            this.lblNameError.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblNameError.Location = new System.Drawing.Point(278, 113);
            this.lblNameError.Name = "lblNameError";
            this.lblNameError.Size = new System.Drawing.Size(107, 12);
            this.lblNameError.TabIndex = 7;
            this.lblNameError.Text = "   用户名不能为空";
            this.lblNameError.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.LightSkyBlue;
            this.pictureBox1.Image = global::FamilyFinance.Properties.Resources.login;
            this.pictureBox1.Location = new System.Drawing.Point(23, 22);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(280, 50);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // lblAllError
            // 
            this.lblAllError.AutoSize = true;
            this.lblAllError.Image = global::FamilyFinance.Properties.Resources.check_error;
            this.lblAllError.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblAllError.Location = new System.Drawing.Point(278, 174);
            this.lblAllError.Name = "lblAllError";
            this.lblAllError.Size = new System.Drawing.Size(119, 12);
            this.lblAllError.TabIndex = 9;
            this.lblAllError.Text = "   用户名或密码错误";
            this.lblAllError.Visible = false;
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Peru;
            this.ClientSize = new System.Drawing.Size(398, 279);
            this.Controls.Add(this.lblAllError);
            this.Controls.Add(this.lblPasswordError);
            this.Controls.Add(this.lblNameError);
            this.Controls.Add(this.btnRegist);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.remeber);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.password);
            this.Controls.Add(this.name);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "LoginForm";
            this.Text = "家庭理财管家-登录";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label name;
        private System.Windows.Forms.Label password;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.CheckBox remeber;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnRegist;
        private System.Windows.Forms.Label lblPasswordError;
        private System.Windows.Forms.Label lblNameError;
        private System.Windows.Forms.Label lblAllError;
    }
}

