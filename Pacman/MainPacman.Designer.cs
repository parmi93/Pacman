namespace Pacman
{
    partial class MainPacman
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Liberare le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainPacman));
            this.lbl_Close = new System.Windows.Forms.Label();
            this.lbl_minimizza = new System.Windows.Forms.Label();
            this.lbl_info = new System.Windows.Forms.Label();
            this.Pic_Setting = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.Pic_Setting)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_Close
            // 
            this.lbl_Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_Close.AutoSize = true;
            this.lbl_Close.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Close.ForeColor = System.Drawing.Color.White;
            this.lbl_Close.Location = new System.Drawing.Point(463, 2);
            this.lbl_Close.Name = "lbl_Close";
            this.lbl_Close.Size = new System.Drawing.Size(26, 25);
            this.lbl_Close.TabIndex = 7;
            this.lbl_Close.Text = "X";
            this.lbl_Close.Click += new System.EventHandler(this.lbl_Close_Click);
            this.lbl_Close.MouseEnter += new System.EventHandler(this.lbl_Close_MouseEnter);
            this.lbl_Close.MouseLeave += new System.EventHandler(this.lbl_Close_MouseLeave);
            // 
            // lbl_minimizza
            // 
            this.lbl_minimizza.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_minimizza.AutoSize = true;
            this.lbl_minimizza.Font = new System.Drawing.Font("Courier New", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_minimizza.ForeColor = System.Drawing.Color.White;
            this.lbl_minimizza.Location = new System.Drawing.Point(437, 0);
            this.lbl_minimizza.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_minimizza.Name = "lbl_minimizza";
            this.lbl_minimizza.Size = new System.Drawing.Size(23, 23);
            this.lbl_minimizza.TabIndex = 8;
            this.lbl_minimizza.Text = "_";
            this.lbl_minimizza.Click += new System.EventHandler(this.lbl_minimizza_Click);
            this.lbl_minimizza.MouseEnter += new System.EventHandler(this.lbl_minimizza_MouseEnter);
            this.lbl_minimizza.MouseLeave += new System.EventHandler(this.lbl_Close_MouseLeave);
            // 
            // lbl_info
            // 
            this.lbl_info.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_info.AutoSize = true;
            this.lbl_info.Font = new System.Drawing.Font("Courier New", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_info.ForeColor = System.Drawing.Color.White;
            this.lbl_info.Location = new System.Drawing.Point(414, 5);
            this.lbl_info.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_info.Name = "lbl_info";
            this.lbl_info.Size = new System.Drawing.Size(23, 23);
            this.lbl_info.TabIndex = 9;
            this.lbl_info.Text = "?";
            this.lbl_info.TabIndexChanged += new System.EventHandler(this.lbl_info_Click);
            this.lbl_info.Click += new System.EventHandler(this.lbl_info_Click);
            this.lbl_info.MouseEnter += new System.EventHandler(this.lbl_minimizza_MouseEnter);
            this.lbl_info.MouseLeave += new System.EventHandler(this.lbl_Close_MouseLeave);
            // 
            // Pic_Setting
            // 
            this.Pic_Setting.Image = global::Pacman.Properties.Resources.setting;
            this.Pic_Setting.Location = new System.Drawing.Point(2, 5);
            this.Pic_Setting.Name = "Pic_Setting";
            this.Pic_Setting.Size = new System.Drawing.Size(22, 22);
            this.Pic_Setting.TabIndex = 10;
            this.Pic_Setting.TabStop = false;
            this.Pic_Setting.Click += new System.EventHandler(this.Pic_Setting_Click);
            this.Pic_Setting.MouseEnter += new System.EventHandler(this.Pic_Setting_MouseEnter);
            this.Pic_Setting.MouseLeave += new System.EventHandler(this.Pic_Setting_MouseLeave);
            // 
            // MainPacman
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(487, 516);
            this.Controls.Add(this.Pic_Setting);
            this.Controls.Add(this.lbl_info);
            this.Controls.Add(this.lbl_minimizza);
            this.Controls.Add(this.lbl_Close);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainPacman";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pacman";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainPacman_KeyDown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainPacman_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainPacman_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainPacman_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.Pic_Setting)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_Close;
        private System.Windows.Forms.Label lbl_minimizza;
        private System.Windows.Forms.Label lbl_info;
        private System.Windows.Forms.PictureBox Pic_Setting;
    }
}

