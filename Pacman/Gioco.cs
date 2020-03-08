using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;
using System.Runtime.InteropServices;
using System.Drawing.Text;

namespace Pacman
{
    class Gioco : Panel
    {
        Mappa mappa;
        Label lbl_Score = new Label(), lbl_ScoreNumber = new Label(), lbl_Lives = new Label(), lbl_Livello = new Label(), lbl_gameOver = new Label(), lbl_Pause = new Label();
        List<Pacman> lives = new List<Pacman>();
        PictureBox restart = new PictureBox();
        PictureBox pausePlay = new PictureBox();
        int vite, punti = 0, viteGioco = -1;
        double puntiCheat;
        Image[,] imagePausePlay = new Image[2, 2];
        int pausaPlay = 0, livello = 1;
        bool gameOver_ = false;
        Timer sposta = new Timer();
        int destraSinistra;
        public bool infoActive = false;
        Label info;
        FontFamily MyFontFamily;

        public Gioco(Mappa mappa, int vite, Label info, FontFamily MyFontFamily)
        {
            this.mappa = mappa;
            this.mappa.Location = new Point(0, 20);
            this.Controls.Add(mappa);
            this.BackColor = Color.Black;
            this.Size = new Size(mappa.Size.Width, mappa.Size.Height + mappa.Location.Y + 80);
            this.info = info;
            this.MyFontFamily = MyFontFamily;

            this.Controls.Add(lbl_Score);
            lbl_Score.Text = "SCORE";
            lbl_Score.Font = new System.Drawing.Font(MyFontFamily, 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lbl_Score.ForeColor = Color.White;
            lbl_Score.BackColor = Color.Transparent;
            lbl_Score.Location = new Point(0, mappa.Size.Height + mappa.Location.Y);
            lbl_Score.AutoSize = true;

            this.Controls.Add(lbl_ScoreNumber);
            lbl_ScoreNumber.Text = "0";
            lbl_ScoreNumber.Font = new System.Drawing.Font(MyFontFamily, 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lbl_ScoreNumber.ForeColor = Color.Yellow;
            lbl_ScoreNumber.BackColor = Color.Transparent;
            lbl_ScoreNumber.Location = new Point(lbl_Score.Size.Width, mappa.Size.Height + mappa.Location.Y);
            lbl_ScoreNumber.BringToFront();
            lbl_ScoreNumber.AutoSize = true;

            this.Controls.Add(lbl_Lives);
            lbl_Lives.Text = "LIVES";
            lbl_Lives.Font = new System.Drawing.Font(MyFontFamily, 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lbl_Lives.ForeColor = Color.White;
            lbl_Lives.BackColor = Color.Transparent;
            lbl_Lives.Location = new Point(180, mappa.Size.Height + mappa.Location.Y);
            lbl_Lives.BringToFront();
            lbl_Lives.AutoSize = true;

            this.Controls.Add(lbl_Livello);
            lbl_Livello.Text = "LIVELLO " + this.livello.ToString();
            lbl_Livello.Font = new System.Drawing.Font(MyFontFamily, 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lbl_Livello.ForeColor = Color.Green;
            lbl_Livello.BackColor = Color.Transparent;
            lbl_Livello.Location = new Point(0, mappa.Size.Height + mappa.Location.Y + 25);
            lbl_Livello.BringToFront();
            lbl_Livello.AutoSize = true;

            this.Controls.Add(lbl_Pause);
            lbl_Pause.Text = "PAUSE";
            lbl_Pause.Font = new System.Drawing.Font(MyFontFamily, 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lbl_Pause.ForeColor = Color.White;
            lbl_Pause.BackColor = Color.Transparent;
            lbl_Pause.Location = new Point(-230, 230);
            lbl_Pause.BringToFront();
            lbl_Pause.AutoSize = true;

            restart.Image = Properties.Resources.refresh2;
            this.Controls.Add(restart);
            restart.Location = new Point(23, 0);
            restart.MouseEnter += restartMouseEnter;
            restart.MouseLeave += restartMouseLeave;
            restart.Click += restartMouseClick;
            restart.Size = Properties.Resources.refresh2.Size;

            imagePausePlay[0, 0] = Properties.Resources.pause2;
            imagePausePlay[0, 1] = Properties.Resources.pause;

            imagePausePlay[1, 0] = Properties.Resources.play2;
            imagePausePlay[1, 1] = Properties.Resources.play;

            pausePlay.Image = imagePausePlay[0, 0];
            this.Controls.Add(pausePlay);
            pausePlay.Location = new Point(0, 0);
            pausePlay.MouseEnter += pausePlayMouseEnter;
            pausePlay.MouseLeave += pausePlayMouseLeave;
            pausePlay.Click += pausePlayMouseClick;
            pausePlay.Size = Properties.Resources.refresh2.Size;

            sposta.Intervallo = 10;
            sposta.Tick += spostaDestra;

            this.Vite = vite;
        }

        public int Vite
        {
            get
            {
                return vite;
            }

            set
            {
                if (value >= -1)
                {
                    int temp;
                    if (value >= vite)
                        temp = 1;
                    else
                        temp = -1;

                    for (int i = vite; value != i; i += temp)
                        if (temp == 1 && i != -1)
                        {
                            lives.Add(new Pacman());
                            lives[i].Location = new Point(lbl_Lives.Location.X + lbl_Lives.Size.Width + ((lives.Count - 1) * 22), mappa.Size.Height + mappa.Location.Y + 5);
                            this.Controls.Add(lives[i]);
                        }
                        else
                            if (i != 0 && i != -1)
                            {
                                lives[i - 1].Dispose();
                                lives.RemoveAt(lives.Count - 1);
                            }
                        

                    vite = value;
                    if (viteGioco == -1)
                        viteGioco = value;
                }
                else
                    vite = 0;
            }
        }

        public int Punti
        {
            get
            {
                return punti;
            }

            set
            {
                if (value >= 0)
                {
                    if (puntiCheat / 2.5 != Punti)
                    {
                        mappa.Pause();
                        MessageBox.Show("Vergognati neanche a giocare Pacman senza Cheattare sei capace -.-", "Fai schifo=)", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        Application.Exit();
                    }

                    punti = value;
                    puntiCheat = (double)punti * 2.5;
                }
                else
                    puntiCheat = punti = 0;

                lbl_ScoreNumber.Text = punti.ToString();
            }
        }

        public void Inizia(int tempo)
        {
            gameOver_ = false;
            Label lbl_GerReady = new Label();
            this.Controls.Add(lbl_GerReady);
            lbl_GerReady.Text = "GET READY";
            lbl_GerReady.Font = new System.Drawing.Font(MyFontFamily, 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lbl_GerReady.ForeColor = Color.Gold;
            lbl_GerReady.BackColor = Color.Transparent;
            lbl_GerReady.Location = new Point(this.Size.Width / 2 - lbl_GerReady.Size.Width + 30, this.Size.Height - lbl_GerReady.Size.Height - 10);
            lbl_GerReady.AutoSize = true;
            lbl_GerReady.BringToFront();
            Application.DoEvents();

            Thread.Sleep(tempo);
            lbl_GerReady.Dispose();
            mappa.Start();
        }

        public void Pause()
        {
            if(statoGioco)
                pausePlayMouseClick(null, null);
        }

        public void Start()
        {
            if (!statoGioco)
                pausePlayMouseClick(null, null);
        }

        public void nextLivel()
        {
            mappa.Pause();
            lbl_Livello.Text = "LIVELLO " + (++this.livello);
            Application.DoEvents();
            mappa.raggio += 10;
            mappa.Ripristina(null, null);
            mappa.posizionaPillole();
            Application.DoEvents();
            this.Inizia(4000);
        }

        public void gameOver()
        {
            gameOver_ = true;
            lbl_gameOver = new Label();
            this.Controls.Add(lbl_gameOver);
            lbl_gameOver.Text = "GAME OVER";
            lbl_gameOver.Font = new System.Drawing.Font(MyFontFamily, 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lbl_gameOver.ForeColor = Color.Red;
            lbl_gameOver.BackColor = Color.Transparent;
            lbl_gameOver.Location = new Point(250, mappa.Size.Height + mappa.Location.Y);
            lbl_gameOver.BringToFront();
            lbl_gameOver.AutoSize = true;
        }

        public bool statoGioco
        {
            get
            {
                if (pausaPlay == 0)
                    return true;
                else
                    return false;
            }
        }

        public void restartMouseEnter(object sender, EventArgs e)
        {
            restart.Image = Properties.Resources.refresh;
        }

        public void restartMouseLeave(object sender, EventArgs e)
        {
            restart.Image = Properties.Resources.refresh2;
        }

        public void restartMouseClick(object sender, EventArgs e)
        {
            mappa.Pause();
            if (MessageBox.Show("Riniziare la partita?", "Attenzione", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                mappa.Ripristina(null, null);
                mappa.posizionaPillole();
                lbl_gameOver.Dispose();
                Punti = 0;
                Vite = viteGioco;
                this.livello = 1;
                lbl_Livello.Text = "LIVELLO 1";
                mappa.raggio = 40000;
                this.Inizia(4000);
            }
            else
                if(!gameOver_)
                    mappa.Start();
        }

        public void pausePlayMouseEnter(object sender, EventArgs e)
        {
            if (!gameOver_)
            {
                pausePlay.Image = imagePausePlay[pausaPlay, 1];
                lbl_Pause.ForeColor = Color.Green;
            }
        }

        public void pausePlayMouseLeave(object sender, EventArgs e)
        {
            pausePlay.Image = imagePausePlay[pausaPlay, 0];
            lbl_Pause.ForeColor = Color.White;
        }

        public void pausePlayMouseClick(object sender, EventArgs e)
        {
            if (!gameOver_)
            {
                if (pausaPlay == 0)
                {
                    mappa.Pause();
                    this.Parent.Controls.Add(lbl_Pause);
                    destraSinistra = 5;
                    this.Parent.Controls.Add(pausePlay);
                    pausePlay.Location = new Point(0, 30);
                    sposta.StartP();
                    pausaPlay = 1;
                }
                else
                {                        
                    destraSinistra = -5;
                    sposta.StartP();
                    pausaPlay = 0;

                    if (infoActive)
                        info.TabIndex += info.TabIndex % 2 == 0 ? 1 : -1;
                }
                if (sender == null)
                    pausePlay.Image = imagePausePlay[pausaPlay, 0];
                else
                    pausePlay.Image = imagePausePlay[pausaPlay, 1];
            }
        }

        private void spostaDestra(object sender, EventArgs e)
        {
            this.Location = new Point(this.Location.X + destraSinistra, this.Location.Y);
            lbl_Pause.Location = new Point(lbl_Pause.Location.X + destraSinistra, lbl_Pause.Location.Y);
            if ((destraSinistra > 0 && this.Location.X > this.Size.Width) || (destraSinistra < 0 && this.Location.X <= 0))
            {
                if(this.Location.X < 0)
                    this.Location = new Point(0, this.Location.Y);

                if (destraSinistra < 0)
                {
                    this.Controls.Add(pausePlay);
                    pausePlay.Location = new Point(0, 0);
                    mappa.Start();
                }
                sposta.StopP();
            }
        }
    }
}
