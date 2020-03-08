using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Pacman
{
    class Info : Panel
    {
        private System.Windows.Forms.Label lbl_Info;
        private System.Windows.Forms.Label lbl_programmer;
        private System.Windows.Forms.Label lbl_singhParminder;
        private System.Windows.Forms.PictureBox Pic_image;
        private System.Windows.Forms.Label lbl_version;
        private System.Windows.Forms.Label lbl_ver;
        private System.Windows.Forms.Label lbl_subTitle;
        private System.Windows.Forms.Label lbl_copy;
        private System.Windows.Forms.Label lbl_testo;
        private System.Windows.Forms.Panel Pn_main;

        public Info(FontFamily MyFontFamily)
        {
            this.lbl_Info = new System.Windows.Forms.Label();
            this.lbl_programmer = new System.Windows.Forms.Label();
            this.lbl_singhParminder = new System.Windows.Forms.Label();
            this.Pic_image = new System.Windows.Forms.PictureBox();
            this.lbl_version = new System.Windows.Forms.Label();
            this.lbl_ver = new System.Windows.Forms.Label();
            this.lbl_subTitle = new System.Windows.Forms.Label();
            this.lbl_copy = new System.Windows.Forms.Label();
            this.lbl_testo = new System.Windows.Forms.Label();
            this.Pn_main = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.Pic_image)).BeginInit();
            this.Pn_main.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_Info
            // 
            this.lbl_Info.AutoSize = true;
            this.lbl_Info.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Info.Font = new System.Drawing.Font(MyFontFamily, 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Info.ForeColor = System.Drawing.Color.White;
            this.lbl_Info.Location = new System.Drawing.Point(311, 0);
            this.lbl_Info.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_Info.Name = "lbl_Info";
            this.lbl_Info.Size = new System.Drawing.Size(69, 34);
            this.lbl_Info.TabIndex = 0;
            this.lbl_Info.Text = "INFO";
            // 
            // lbl_programmer
            // 
            this.lbl_programmer.AutoSize = true;
            this.lbl_programmer.BackColor = System.Drawing.Color.Transparent;
            this.lbl_programmer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_programmer.ForeColor = System.Drawing.Color.White;
            this.lbl_programmer.Location = new System.Drawing.Point(3, 16);
            this.lbl_programmer.Name = "lbl_programmer";
            this.lbl_programmer.Size = new System.Drawing.Size(97, 16);
            this.lbl_programmer.TabIndex = 1;
            this.lbl_programmer.Text = "Programmer:";
            // 
            // lbl_singhParminder
            // 
            this.lbl_singhParminder.AutoSize = true;
            this.lbl_singhParminder.BackColor = System.Drawing.Color.Transparent;
            this.lbl_singhParminder.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_singhParminder.ForeColor = System.Drawing.Color.White;
            this.lbl_singhParminder.Location = new System.Drawing.Point(106, 16);
            this.lbl_singhParminder.Name = "lbl_singhParminder";
            this.lbl_singhParminder.Size = new System.Drawing.Size(107, 16);
            this.lbl_singhParminder.TabIndex = 2;
            this.lbl_singhParminder.Text = "Singh Parminder";
            // 
            // Pic_image
            // 
            this.Pic_image.Image = global::Pacman.Properties.Resources.pac;
            this.Pic_image.Location = new System.Drawing.Point(-9, 63);
            this.Pic_image.Name = "Pic_image";
            this.Pic_image.Size = new System.Drawing.Size(400, 150);
            this.Pic_image.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.Pic_image.TabIndex = 3;
            this.Pic_image.TabStop = false;
            // 
            // lbl_version
            // 
            this.lbl_version.AutoSize = true;
            this.lbl_version.BackColor = System.Drawing.Color.Transparent;
            this.lbl_version.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_version.ForeColor = System.Drawing.Color.White;
            this.lbl_version.Location = new System.Drawing.Point(35, 41);
            this.lbl_version.Name = "lbl_version";
            this.lbl_version.Size = new System.Drawing.Size(65, 16);
            this.lbl_version.TabIndex = 4;
            this.lbl_version.Text = "Version:";
            // 
            // lbl_ver
            // 
            this.lbl_ver.AutoSize = true;
            this.lbl_ver.BackColor = System.Drawing.Color.Transparent;
            this.lbl_ver.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_ver.ForeColor = System.Drawing.Color.White;
            this.lbl_ver.Location = new System.Drawing.Point(106, 41);
            this.lbl_ver.Name = "lbl_ver";
            this.lbl_ver.Size = new System.Drawing.Size(35, 16);
            this.lbl_ver.TabIndex = 5;
            this.lbl_ver.Text = "1.0.0";
            // 
            // lbl_subTitle
            // 
            this.lbl_subTitle.AutoSize = true;
            this.lbl_subTitle.BackColor = System.Drawing.Color.Transparent;
            this.lbl_subTitle.Font = new System.Drawing.Font(MyFontFamily, 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_subTitle.ForeColor = System.Drawing.Color.Turquoise;
            this.lbl_subTitle.Location = new System.Drawing.Point(284, 34);
            this.lbl_subTitle.Name = "lbl_subTitle";
            this.lbl_subTitle.Size = new System.Drawing.Size(96, 26);
            this.lbl_subTitle.TabIndex = 6;
            this.lbl_subTitle.Text = "PACMAN";
            // 
            // lbl_copy
            // 
            this.lbl_copy.AutoSize = true;
            this.lbl_copy.BackColor = System.Drawing.Color.Transparent;
            this.lbl_copy.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_copy.ForeColor = System.Drawing.Color.White;
            this.lbl_copy.Location = new System.Drawing.Point(64, 283);
            this.lbl_copy.Name = "lbl_copy";
            this.lbl_copy.Size = new System.Drawing.Size(266, 16);
            this.lbl_copy.TabIndex = 8;
            this.lbl_copy.Text = "Copyright © 2013 - All rights reserved.";
            // 
            // lbl_testo
            // 
            this.lbl_testo.AutoSize = true;
            this.lbl_testo.BackColor = System.Drawing.Color.Transparent;
            this.lbl_testo.ForeColor = System.Drawing.Color.Gainsboro;
            this.lbl_testo.Location = new System.Drawing.Point(3, 216);
            this.lbl_testo.Name = "lbl_testo";
            this.lbl_testo.Size = new System.Drawing.Size(380, 52);
            this.lbl_testo.TabIndex = 9;
            this.lbl_testo.Text = 
@"Nato nel 1979 in Giappone come Puck-man, dopo più 30 anni c’è ancora 
gente che dice che questo gioco fa impazzire, se fosse così, tutti noi dovremmo 
muoverci in stanze buie ascoltando musica ripetitiva e mangiando pillole… beh, 
effettivamente i tamarri che vanno a disco lo fanno...";
            // 
            // Pn_main
            // 
            this.Pn_main.BackColor = System.Drawing.Color.Black;
            this.Pn_main.Controls.Add(this.lbl_testo);
            this.Pn_main.Controls.Add(this.lbl_copy);
            this.Pn_main.Controls.Add(this.lbl_subTitle);
            this.Pn_main.Controls.Add(this.lbl_ver);
            this.Pn_main.Controls.Add(this.lbl_version);
            this.Pn_main.Controls.Add(this.Pic_image);
            this.Pn_main.Controls.Add(this.lbl_singhParminder);
            this.Pn_main.Controls.Add(this.lbl_programmer);
            this.Pn_main.Controls.Add(this.lbl_Info);
            this.Pn_main.Name = "Pn_main";
            this.Pn_main.Size = new System.Drawing.Size(382, 304);
            this.Pn_main.TabIndex = 0;
            // 
            // Info_
            // 
            this.Controls.Add(this.Pn_main);
            this.Name = "Info_";
            this.Size = new System.Drawing.Size(382, 305);
            ((System.ComponentModel.ISupportInitialize)(this.Pic_image)).EndInit();
            this.Pn_main.ResumeLayout(false);
            this.Pn_main.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}
