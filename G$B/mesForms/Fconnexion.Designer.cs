namespace G_B
{
    partial class Fconnexion
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
            this.BackgroundLogin = new System.Windows.Forms.Panel();
            this.FormLogin = new System.Windows.Forms.Panel();
            this.Login_btnLogin = new System.Windows.Forms.Button();
            this.Login_tbPwd = new System.Windows.Forms.TextBox();
            this.Login_tbLogin = new System.Windows.Forms.TextBox();
            this.Login_lblPwd = new System.Windows.Forms.Label();
            this.Login_lblLogin = new System.Windows.Forms.Label();
            this.LoginTitle = new System.Windows.Forms.Label();
            this.BackgroundLogin.SuspendLayout();
            this.FormLogin.SuspendLayout();
            this.SuspendLayout();
            // 
            // BackgroundLogin
            // 
            this.BackgroundLogin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BackgroundLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(205)))), ((int)(((byte)(255)))));
            this.BackgroundLogin.BackgroundImage = global::G_B.Properties.Resources.profil;
            this.BackgroundLogin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BackgroundLogin.Controls.Add(this.FormLogin);
            this.BackgroundLogin.Location = new System.Drawing.Point(12, 12);
            this.BackgroundLogin.Name = "BackgroundLogin";
            this.BackgroundLogin.Size = new System.Drawing.Size(1240, 443);
            this.BackgroundLogin.TabIndex = 0;
            // 
            // FormLogin
            // 
            this.FormLogin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FormLogin.BackColor = System.Drawing.Color.White;
            this.FormLogin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FormLogin.Controls.Add(this.Login_btnLogin);
            this.FormLogin.Controls.Add(this.Login_tbPwd);
            this.FormLogin.Controls.Add(this.Login_tbLogin);
            this.FormLogin.Controls.Add(this.Login_lblPwd);
            this.FormLogin.Controls.Add(this.Login_lblLogin);
            this.FormLogin.Controls.Add(this.LoginTitle);
            this.FormLogin.Location = new System.Drawing.Point(832, 13);
            this.FormLogin.Name = "FormLogin";
            this.FormLogin.Size = new System.Drawing.Size(396, 419);
            this.FormLogin.TabIndex = 0;
            // 
            // Login_btnLogin
            // 
            this.Login_btnLogin.AutoSize = true;
            this.Login_btnLogin.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.Login_btnLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Login_btnLogin.Location = new System.Drawing.Point(154, 314);
            this.Login_btnLogin.Name = "Login_btnLogin";
            this.Login_btnLogin.Size = new System.Drawing.Size(133, 30);
            this.Login_btnLogin.TabIndex = 5;
            this.Login_btnLogin.Text = "Login_btnLogin";
            this.Login_btnLogin.UseVisualStyleBackColor = false;
            this.Login_btnLogin.Click += new System.EventHandler(this.Login_btnLogin_Click);
            this.Login_btnLogin.MouseLeave += new System.EventHandler(this.Login_btnLogin_MouseLeave);
            this.Login_btnLogin.MouseHover += new System.EventHandler(this.Login_btnLogin_MouseHover);
            // 
            // Login_tbPwd
            // 
            this.Login_tbPwd.Location = new System.Drawing.Point(154, 228);
            this.Login_tbPwd.Name = "Login_tbPwd";
            this.Login_tbPwd.PasswordChar = '*';
            this.Login_tbPwd.Size = new System.Drawing.Size(204, 22);
            this.Login_tbPwd.TabIndex = 4;
            // 
            // Login_tbLogin
            // 
            this.Login_tbLogin.Location = new System.Drawing.Point(154, 170);
            this.Login_tbLogin.Name = "Login_tbLogin";
            this.Login_tbLogin.Size = new System.Drawing.Size(204, 22);
            this.Login_tbLogin.TabIndex = 3;
            // 
            // Login_lblPwd
            // 
            this.Login_lblPwd.AutoSize = true;
            this.Login_lblPwd.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Login_lblPwd.Location = new System.Drawing.Point(24, 230);
            this.Login_lblPwd.Name = "Login_lblPwd";
            this.Login_lblPwd.Size = new System.Drawing.Size(108, 20);
            this.Login_lblPwd.TabIndex = 2;
            this.Login_lblPwd.Text = "Login_lblPwd";
            // 
            // Login_lblLogin
            // 
            this.Login_lblLogin.AutoSize = true;
            this.Login_lblLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Login_lblLogin.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Login_lblLogin.Location = new System.Drawing.Point(24, 170);
            this.Login_lblLogin.Name = "Login_lblLogin";
            this.Login_lblLogin.Size = new System.Drawing.Size(117, 20);
            this.Login_lblLogin.TabIndex = 1;
            this.Login_lblLogin.Text = "Login_lblLogin";
            // 
            // LoginTitle
            // 
            this.LoginTitle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.LoginTitle.AutoSize = true;
            this.LoginTitle.Font = new System.Drawing.Font("Informal Roman", 25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoginTitle.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.LoginTitle.Location = new System.Drawing.Point(104, 26);
            this.LoginTitle.Margin = new System.Windows.Forms.Padding(10);
            this.LoginTitle.Name = "LoginTitle";
            this.LoginTitle.Size = new System.Drawing.Size(183, 46);
            this.LoginTitle.TabIndex = 0;
            this.LoginTitle.Text = "LoginTitle";
            this.LoginTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Fconnexion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.ClientSize = new System.Drawing.Size(1264, 467);
            this.Controls.Add(this.BackgroundLogin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Fconnexion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Fconnexion";
            this.BackgroundLogin.ResumeLayout(false);
            this.FormLogin.ResumeLayout(false);
            this.FormLogin.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel BackgroundLogin;
        private System.Windows.Forms.Panel FormLogin;
        private System.Windows.Forms.Label LoginTitle;
        private System.Windows.Forms.TextBox Login_tbLogin;
        private System.Windows.Forms.Label Login_lblPwd;
        private System.Windows.Forms.Label Login_lblLogin;
        private System.Windows.Forms.TextBox Login_tbPwd;
        private System.Windows.Forms.Button Login_btnLogin;
    }
}

