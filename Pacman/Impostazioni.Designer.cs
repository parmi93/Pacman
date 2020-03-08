namespace Pacman
{
    partial class Impostazioni
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

        #region Codice generato da Progettazione componenti

        /// <summary> 
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare 
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.Pn_Impostazioni = new System.Windows.Forms.Panel();
            this.GrB_Velocita = new System.Windows.Forms.GroupBox();
            this.CoB_VelocitaPinky = new System.Windows.Forms.ComboBox();
            this.lbl_elocitaPinky = new System.Windows.Forms.Label();
            this.CoB_VelocitaInky = new System.Windows.Forms.ComboBox();
            this.lbl_elocitaInky = new System.Windows.Forms.Label();
            this.CoB_VelocitaClyde = new System.Windows.Forms.ComboBox();
            this.lbl_elocitaClyde = new System.Windows.Forms.Label();
            this.CoB_VelocitaBlinky = new System.Windows.Forms.ComboBox();
            this.lbl_elocitaBlinky = new System.Windows.Forms.Label();
            this.CoB_VelocitaPacman = new System.Windows.Forms.ComboBox();
            this.lbl_elocitaPacman = new System.Windows.Forms.Label();
            this.Pn_Impostazioni.SuspendLayout();
            this.GrB_Velocita.SuspendLayout();
            this.SuspendLayout();
            // 
            // Pn_Impostazioni
            // 
            this.Pn_Impostazioni.BackColor = System.Drawing.Color.Black;
            this.Pn_Impostazioni.Controls.Add(this.GrB_Velocita);
            this.Pn_Impostazioni.Location = new System.Drawing.Point(0, 0);
            this.Pn_Impostazioni.Name = "Pn_Impostazioni";
            this.Pn_Impostazioni.Size = new System.Drawing.Size(204, 166);
            this.Pn_Impostazioni.TabIndex = 0;
            // 
            // GrB_Velocita
            // 
            this.GrB_Velocita.Controls.Add(this.CoB_VelocitaPinky);
            this.GrB_Velocita.Controls.Add(this.lbl_elocitaPinky);
            this.GrB_Velocita.Controls.Add(this.CoB_VelocitaInky);
            this.GrB_Velocita.Controls.Add(this.lbl_elocitaInky);
            this.GrB_Velocita.Controls.Add(this.CoB_VelocitaClyde);
            this.GrB_Velocita.Controls.Add(this.lbl_elocitaClyde);
            this.GrB_Velocita.Controls.Add(this.CoB_VelocitaBlinky);
            this.GrB_Velocita.Controls.Add(this.lbl_elocitaBlinky);
            this.GrB_Velocita.Controls.Add(this.CoB_VelocitaPacman);
            this.GrB_Velocita.Controls.Add(this.lbl_elocitaPacman);
            this.GrB_Velocita.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GrB_Velocita.ForeColor = System.Drawing.Color.White;
            this.GrB_Velocita.Location = new System.Drawing.Point(1, 0);
            this.GrB_Velocita.Name = "GrB_Velocita";
            this.GrB_Velocita.Size = new System.Drawing.Size(202, 164);
            this.GrB_Velocita.TabIndex = 0;
            this.GrB_Velocita.TabStop = false;
            this.GrB_Velocita.Text = "Velocità";
            // 
            // CoB_VelocitaPinky
            // 
            this.CoB_VelocitaPinky.BackColor = System.Drawing.Color.Black;
            this.CoB_VelocitaPinky.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CoB_VelocitaPinky.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CoB_VelocitaPinky.ForeColor = System.Drawing.Color.White;
            this.CoB_VelocitaPinky.FormattingEnabled = true;
            this.CoB_VelocitaPinky.Items.AddRange(new object[] {
            "Diabolic",
            "Fast",
            "Standard",
            "Slow",
            "Very Slow"});
            this.CoB_VelocitaPinky.Location = new System.Drawing.Point(76, 132);
            this.CoB_VelocitaPinky.Name = "CoB_VelocitaPinky";
            this.CoB_VelocitaPinky.Size = new System.Drawing.Size(121, 21);
            this.CoB_VelocitaPinky.TabIndex = 4;
            this.CoB_VelocitaPinky.SelectedIndexChanged += new System.EventHandler(this.CoB_VelocitaPinky_SelectedIndexChanged);
            // 
            // lbl_elocitaPinky
            // 
            this.lbl_elocitaPinky.AutoSize = true;
            this.lbl_elocitaPinky.BackColor = System.Drawing.Color.Transparent;
            this.lbl_elocitaPinky.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_elocitaPinky.ForeColor = System.Drawing.Color.Violet;
            this.lbl_elocitaPinky.Location = new System.Drawing.Point(24, 133);
            this.lbl_elocitaPinky.Name = "lbl_elocitaPinky";
            this.lbl_elocitaPinky.Size = new System.Drawing.Size(46, 16);
            this.lbl_elocitaPinky.TabIndex = 9;
            this.lbl_elocitaPinky.Text = "Pinky";
            // 
            // CoB_VelocitaInky
            // 
            this.CoB_VelocitaInky.BackColor = System.Drawing.Color.Black;
            this.CoB_VelocitaInky.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CoB_VelocitaInky.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CoB_VelocitaInky.ForeColor = System.Drawing.Color.White;
            this.CoB_VelocitaInky.FormattingEnabled = true;
            this.CoB_VelocitaInky.Items.AddRange(new object[] {
            "Diabolic",
            "Fast",
            "Standard",
            "Slow",
            "Very Slow"});
            this.CoB_VelocitaInky.Location = new System.Drawing.Point(76, 105);
            this.CoB_VelocitaInky.Name = "CoB_VelocitaInky";
            this.CoB_VelocitaInky.Size = new System.Drawing.Size(121, 21);
            this.CoB_VelocitaInky.TabIndex = 3;
            this.CoB_VelocitaInky.SelectedIndexChanged += new System.EventHandler(this.CoB_VelocitaInky_SelectedIndexChanged);
            // 
            // lbl_elocitaInky
            // 
            this.lbl_elocitaInky.AutoSize = true;
            this.lbl_elocitaInky.BackColor = System.Drawing.Color.Transparent;
            this.lbl_elocitaInky.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_elocitaInky.ForeColor = System.Drawing.Color.Turquoise;
            this.lbl_elocitaInky.Location = new System.Drawing.Point(34, 105);
            this.lbl_elocitaInky.Name = "lbl_elocitaInky";
            this.lbl_elocitaInky.Size = new System.Drawing.Size(36, 16);
            this.lbl_elocitaInky.TabIndex = 7;
            this.lbl_elocitaInky.Text = "Inky";
            // 
            // CoB_VelocitaClyde
            // 
            this.CoB_VelocitaClyde.BackColor = System.Drawing.Color.Black;
            this.CoB_VelocitaClyde.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CoB_VelocitaClyde.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CoB_VelocitaClyde.ForeColor = System.Drawing.Color.White;
            this.CoB_VelocitaClyde.FormattingEnabled = true;
            this.CoB_VelocitaClyde.Items.AddRange(new object[] {
            "Diabolic",
            "Fast",
            "Standard",
            "Slow",
            "Very Slow"});
            this.CoB_VelocitaClyde.Location = new System.Drawing.Point(76, 78);
            this.CoB_VelocitaClyde.Name = "CoB_VelocitaClyde";
            this.CoB_VelocitaClyde.Size = new System.Drawing.Size(121, 21);
            this.CoB_VelocitaClyde.TabIndex = 2;
            this.CoB_VelocitaClyde.SelectedIndexChanged += new System.EventHandler(this.CoB_VelocitaClyde_SelectedIndexChanged);
            // 
            // lbl_elocitaClyde
            // 
            this.lbl_elocitaClyde.AutoSize = true;
            this.lbl_elocitaClyde.BackColor = System.Drawing.Color.Transparent;
            this.lbl_elocitaClyde.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_elocitaClyde.ForeColor = System.Drawing.Color.Orange;
            this.lbl_elocitaClyde.Location = new System.Drawing.Point(22, 79);
            this.lbl_elocitaClyde.Name = "lbl_elocitaClyde";
            this.lbl_elocitaClyde.Size = new System.Drawing.Size(48, 16);
            this.lbl_elocitaClyde.TabIndex = 5;
            this.lbl_elocitaClyde.Text = "Clyde";
            // 
            // CoB_VelocitaBlinky
            // 
            this.CoB_VelocitaBlinky.BackColor = System.Drawing.Color.Black;
            this.CoB_VelocitaBlinky.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CoB_VelocitaBlinky.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CoB_VelocitaBlinky.ForeColor = System.Drawing.Color.White;
            this.CoB_VelocitaBlinky.FormattingEnabled = true;
            this.CoB_VelocitaBlinky.Items.AddRange(new object[] {
            "Diabolic",
            "Fast",
            "Standard",
            "Slow",
            "Very Slow"});
            this.CoB_VelocitaBlinky.Location = new System.Drawing.Point(76, 51);
            this.CoB_VelocitaBlinky.Name = "CoB_VelocitaBlinky";
            this.CoB_VelocitaBlinky.Size = new System.Drawing.Size(121, 21);
            this.CoB_VelocitaBlinky.TabIndex = 1;
            this.CoB_VelocitaBlinky.SelectedIndexChanged += new System.EventHandler(this.CoB_VelocitaBlinky_SelectedIndexChanged);
            // 
            // lbl_elocitaBlinky
            // 
            this.lbl_elocitaBlinky.AutoSize = true;
            this.lbl_elocitaBlinky.BackColor = System.Drawing.Color.Transparent;
            this.lbl_elocitaBlinky.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_elocitaBlinky.ForeColor = System.Drawing.Color.Red;
            this.lbl_elocitaBlinky.Location = new System.Drawing.Point(20, 52);
            this.lbl_elocitaBlinky.Name = "lbl_elocitaBlinky";
            this.lbl_elocitaBlinky.Size = new System.Drawing.Size(50, 16);
            this.lbl_elocitaBlinky.TabIndex = 3;
            this.lbl_elocitaBlinky.Text = "Blinky";
            // 
            // CoB_VelocitaPacman
            // 
            this.CoB_VelocitaPacman.BackColor = System.Drawing.Color.Black;
            this.CoB_VelocitaPacman.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CoB_VelocitaPacman.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CoB_VelocitaPacman.ForeColor = System.Drawing.Color.White;
            this.CoB_VelocitaPacman.FormattingEnabled = true;
            this.CoB_VelocitaPacman.Items.AddRange(new object[] {
            "Diabolic",
            "Fast",
            "Standard",
            "Slow",
            "Very Slow"});
            this.CoB_VelocitaPacman.Location = new System.Drawing.Point(76, 24);
            this.CoB_VelocitaPacman.Name = "CoB_VelocitaPacman";
            this.CoB_VelocitaPacman.Size = new System.Drawing.Size(121, 21);
            this.CoB_VelocitaPacman.TabIndex = 0;
            this.CoB_VelocitaPacman.SelectedIndexChanged += new System.EventHandler(this.CoB_VelocitaPacman_SelectedIndexChanged);
            // 
            // lbl_elocitaPacman
            // 
            this.lbl_elocitaPacman.AutoSize = true;
            this.lbl_elocitaPacman.BackColor = System.Drawing.Color.Transparent;
            this.lbl_elocitaPacman.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_elocitaPacman.ForeColor = System.Drawing.Color.Yellow;
            this.lbl_elocitaPacman.Location = new System.Drawing.Point(6, 25);
            this.lbl_elocitaPacman.Name = "lbl_elocitaPacman";
            this.lbl_elocitaPacman.Size = new System.Drawing.Size(64, 16);
            this.lbl_elocitaPacman.TabIndex = 1;
            this.lbl_elocitaPacman.Text = "Pacman";
            // 
            // Impostazioni
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Pn_Impostazioni);
            this.DoubleBuffered = true;
            this.Enabled = false;
            this.Name = "Impostazioni";
            this.Size = new System.Drawing.Size(204, 169);
            this.Pn_Impostazioni.ResumeLayout(false);
            this.GrB_Velocita.ResumeLayout(false);
            this.GrB_Velocita.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Pn_Impostazioni;
        private System.Windows.Forms.ComboBox CoB_VelocitaPacman;
        private System.Windows.Forms.GroupBox GrB_Velocita;
        private System.Windows.Forms.ComboBox CoB_VelocitaPinky;
        private System.Windows.Forms.Label lbl_elocitaPinky;
        private System.Windows.Forms.ComboBox CoB_VelocitaInky;
        private System.Windows.Forms.Label lbl_elocitaInky;
        private System.Windows.Forms.ComboBox CoB_VelocitaClyde;
        private System.Windows.Forms.Label lbl_elocitaClyde;
        private System.Windows.Forms.ComboBox CoB_VelocitaBlinky;
        private System.Windows.Forms.Label lbl_elocitaBlinky;
        private System.Windows.Forms.Label lbl_elocitaPacman;
    }
}
