using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Drawing.Text;
using System.Media;

namespace Pacman
{
    public partial class MainPacman : Form
    {
        Pacman pacman = new Pacman(0, 60, 120);
        Timer pacmanSpeed = new Timer();
        Ghost[] ghosts;
        datiGhost[] datiGhosts;
        Mappa mappa;
        Gioco gioco;
        Info info;
        Impostazioni impostazioni;
        int puntiGhosts = 200, destraSinistraInfo = 5, sopraSottoImpostazioni = -5;
        Timer spostaInfo = new Timer();
        Timer spostaImpostazioni = new Timer();
        bool drag;
        Point startPoint;
        public FontFamily MyFontFamily;
        Timer start = new Timer();

        SoundPlayer eat_ghostPlayer = new SoundPlayer(Properties.Resources.eat_ghost);
        SoundPlayer eat_pillPlayer = new SoundPlayer(Properties.Resources.eat_pill);
        SoundPlayer eat_powPlayer = new SoundPlayer(Properties.Resources.eat_pow);
        SoundPlayer mortoPlayer = new SoundPlayer(Properties.Resources.morto);
        SoundPlayer riderePlayer = new SoundPlayer(Properties.Resources.ridere);
        SoundPlayer startPlayer = new SoundPlayer(Properties.Resources.start);
        SoundPlayer extra_livesPlayer = new SoundPlayer(Properties.Resources.extra_lives);

        public MainPacman()
        {
            InitializeComponent();
            IncorporaFont(17f, lbl_Close.Font.Style);

            eat_ghostPlayer.Load();
            eat_pillPlayer.Load();
            eat_powPlayer.Load();
            mortoPlayer.Load();
            riderePlayer.Load();
            startPlayer.Load();
            extra_livesPlayer.Load();
            

            lbl_Close.Font = new System.Drawing.Font(MyFontFamily, 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            #region dati

            List<int[]> zonaNavigabileOrizzontale = new List<int[]>();
            List<int[]> zonaNavigabileVerticale = new List<int[]>();
            List<int[]> pilloleOrizzontale = new List<int[]>();
            List<int[]> pilloleVerticale = new List<int[]>();
            List<int[]> pilloleSpeciali = new List<int[]>();

            zonaNavigabileOrizzontale.Add(new int[] { 12, 12, 162, 203, 354 });
            zonaNavigabileOrizzontale.Add(new int[] { 66, 12, 354 });
            zonaNavigabileOrizzontale.Add(new int[] { 107, 12, 80, 121, 162, 203, 244, 285, 354 });
            zonaNavigabileOrizzontale.Add(new int[] { 149, 121, 244 });
            zonaNavigabileOrizzontale.Add(new int[] { 190, -22, 121, 244, 387 });
            zonaNavigabileOrizzontale.Add(new int[] { 231, 121, 244 });
            zonaNavigabileOrizzontale.Add(new int[] { 272, 12, 162, 203, 354 });
            zonaNavigabileOrizzontale.Add(new int[] { 313, 12, 38, 80, 285, 327, 354 });
            zonaNavigabileOrizzontale.Add(new int[] { 354, 12, 80, 121, 162, 203, 244, 285, 354 });
            zonaNavigabileOrizzontale.Add(new int[] { 395, 12, 354 });

            zonaNavigabileVerticale.Add(new int[] { 12, 12, 107, 272, 313, 354, 395 });
            zonaNavigabileVerticale.Add(new int[] { 38, 313, 354 });
            zonaNavigabileVerticale.Add(new int[] { 80, 12, 354 });
            zonaNavigabileVerticale.Add(new int[] { 121, 66, 107, 149, 272, 313, 354 });
            zonaNavigabileVerticale.Add(new int[] { 162, 12, 66, 107, 149, 272, 313, 354, 395 });
            zonaNavigabileVerticale.Add(new int[] { 203, 12, 66, 107, 149, 272, 313, 354, 395 });
            zonaNavigabileVerticale.Add(new int[] { 244, 66, 107, 149, 272, 313, 354 });
            zonaNavigabileVerticale.Add(new int[] { 285, 12, 354 });
            zonaNavigabileVerticale.Add(new int[] { 327, 313, 354 });
            zonaNavigabileVerticale.Add(new int[] { 354, 12, 107, 272, 313, 354, 395 });

            pilloleOrizzontale.Add(new int[] { 21, 21, 185, 12, 213, 378, 12 });
            pilloleOrizzontale.Add(new int[] { 76, 21, 378, 26 });
            pilloleOrizzontale.Add(new int[] { 117, 21, 102, 6, 130, 186, 4, 213, 268, 4, 296, 378, 6 });
            pilloleOrizzontale.Add(new int[] { 281, 21, 185, 12, 213, 378, 12 });
            pilloleOrizzontale.Add(new int[] { 322, 34, 62, 2, 89, 185, 7, 213, 309, 7, 336, 364, 2 });
            pilloleOrizzontale.Add(new int[] { 364, 21, 102, 6, 130, 186, 4, 213, 268, 4, 296, 378, 6 });
            pilloleOrizzontale.Add(new int[] { 405, 21, 378, 26 });

            pilloleVerticale.Add(new int[] { 21, 35, 35, 1, 62, 62, 1, 89, 117, 2, 295, 322, 2, 377, 405, 2 });
            pilloleVerticale.Add(new int[] { 48, 336, 364, 2 });
            pilloleVerticale.Add(new int[] { 89, 35, 76, 3, 89, 117, 2, 131, 281, 11, 295, 322, 2, 336, 364, 2 });
            pilloleVerticale.Add(new int[] { 130, 89, 117, 2, 336, 364, 2 });
            pilloleVerticale.Add(new int[] { 172, 34, 76, 3, 295, 323, 2, 377, 405, 2 });
            pilloleVerticale.Add(new int[] { 213, 34, 76, 3, 295, 323, 2, 377, 405, 2 });
            pilloleVerticale.Add(new int[] { 255, 89, 117, 2, 336, 364, 2 });
            pilloleVerticale.Add(new int[] { 296, 35, 76, 3, 89, 117, 2, 131, 281, 11, 295, 322, 2, 336, 364, 2 });
            pilloleVerticale.Add(new int[] { 336, 336, 364, 2 });
            pilloleVerticale.Add(new int[] { 365, 35, 35, 1, 62, 62, 1, 89, 117, 2, 295, 322, 2, 377, 405, 2 });

            pilloleSpeciali.Add(new int[] { 44, 17, 17, 1, 360, 360, 1 });
            pilloleSpeciali.Add(new int[] { 317, 17, 17, 1, 360, 360, 1 });

            int[,] zoneSpeciali = new int[,] { { 387, 190, -22, 190, 2 }, { -22, 190, 387, 190, 0 } };


            ghosts = new Ghost[4];
            datiGhosts = new datiGhost[4];

            ghosts[0] = new Ghost("blinky", 0, 60, 7000, 3000, 250);
            datiGhosts[0].speed = new Timer();
            datiGhosts[0].speedIniziale = datiGhosts[0].speed.Intervallo = 32;
            datiGhosts[0].durataMorte = new Timer();
            datiGhosts[0].durataMorte.Intervallo = 5000;
            datiGhosts[0].durataMorte.Tag = datiGhosts[0].speed.Tag = 0;
            datiGhosts[0].direzione = 0;
            datiGhosts[0].puntoPartenza = new Point(12, 12);

            ghosts[1] = new Ghost("inky", 0, 60, 7000, 3000, 250);
            datiGhosts[1].speed = new Timer();
            datiGhosts[1].speedIniziale = datiGhosts[1].speed.Intervallo = 33;
            datiGhosts[1].durataMorte = new Timer();
            datiGhosts[1].durataMorte.Intervallo = 5000;
            datiGhosts[1].durataMorte.Tag = datiGhosts[1].speed.Tag = 1;
            datiGhosts[1].direzione = 0;
            datiGhosts[1].puntoPartenza = new Point(354, 12);

            ghosts[2] = new Ghost("pinky", 0, 60, 7000, 3000, 250);
            datiGhosts[2].speed = new Timer();
            datiGhosts[2].speedIniziale = datiGhosts[2].speed.Intervallo = 34;
            datiGhosts[2].durataMorte = new Timer();
            datiGhosts[2].durataMorte.Intervallo = 5000;
            datiGhosts[2].durataMorte.Tag = datiGhosts[2].speed.Tag = 2;
            datiGhosts[2].direzione = 0;
            datiGhosts[2].puntoPartenza = new Point(12, 395);

            ghosts[3] = new Ghost("clyde", 0, 60, 7000, 3000, 250);
            datiGhosts[3].speed = new Timer();
            datiGhosts[3].speedIniziale = datiGhosts[3].speed.Intervallo = 35;
            datiGhosts[3].durataMorte = new Timer();
            datiGhosts[3].durataMorte.Intervallo = 5000;
            datiGhosts[3].durataMorte.Tag = datiGhosts[3].speed.Tag = 3;
            datiGhosts[3].direzione = 0;
            datiGhosts[3].puntoPartenza = new Point(354, 395);

            #endregion

            pacmanSpeed.Intervallo = 29;

            mappa = new Mappa(Properties.Resources.background, zonaNavigabileOrizzontale, zonaNavigabileVerticale, pacman, 190, 313, 0, pacmanSpeed, pilloleOrizzontale, pilloleVerticale, pilloleSpeciali, ghosts, ref datiGhosts, zoneSpeciali);
            mappa.incrementoPunteggio += new EventHandler(incrementoPunteggio);
            mappa.incrementoPunteggioSpeciale += new EventHandler(incrementoPunteggioSpeciale);
            mappa.mortePacman += mortePacman;
            mappa.morteGhost += incrementoPunteggioGhost;
            mappa.vinto += vinto;

            gioco = new Gioco(mappa, 2, lbl_info, MyFontFamily);
            this.Size = new Size(0, 0);
            this.Controls.Add(gioco);
            gioco.Location = new Point(0, 30);

            gioco.MouseDown += MainPacman_MouseDown;
            gioco.MouseUp += MainPacman_MouseUp;
            gioco.MouseMove += MainPacman_MouseMove;
            mappa.MouseDown += MainPacman_MouseDown;
            mappa.MouseUp += MainPacman_MouseUp;
            mappa.MouseMove += MainPacman_MouseMove;

            this.info = new Info(MyFontFamily);
            this.Controls.Add(this.info);
            this.info.Location = new Point(gioco.Size.Width, gioco.Size.Height - info.Size.Height + 30);
            this.info.BringToFront();

            spostaInfo.Intervallo = 10;
            spostaInfo.Tick += spostaSinistraInfo;

            impostazioni = new Impostazioni(mappa, ref datiGhosts);
            this.Controls.Add(impostazioni);
            impostazioni.Location = new Point(0, -impostazioni.Size.Height);
            impostazioni.BringToFront();
            spostaImpostazioni.Intervallo = 10;
            spostaImpostazioni.Tick += spostaSinistraImpostazioni;

            start.Tick += inizia;
            start.Intervallo = 10;
            start.StartP();
        }

        [DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pdv, [In] ref uint pcFonts);

        private void IncorporaFont(float dimensione, FontStyle stile)
        {
            // legge la risorsa come vettore di byte (freak è il nome della risorsa importata dalle proprietà del progetto)
            byte[] Bytes = Properties.Resources.WhimsyTT;

            // alloca un blocco di memoria e ne ottiene il puntatore
            IntPtr ptr = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(Bytes.Length);

            // copia i byte della risorsa font nel blocco appena allocato
            Marshal.Copy(Bytes, 0, ptr, Bytes.Length);

            // registra il font nel sistema (chiamata ad API di sistema) 
            uint cFonts = 0;
            AddFontMemResourceEx(ptr, (uint)Bytes.Length, IntPtr.Zero, ref cFonts);

            // incorpora un font privato (interno) nell'applicazione 
            PrivateFontCollection pfc = new PrivateFontCollection();
            pfc.AddMemoryFont(ptr, Bytes.Length);

            // libera il blocco di memoria precedentemente allocato
            Marshal.FreeCoTaskMem(ptr);

            // definisce un oggetto Font dal font incorportato e lo assegna all'attributo
            MyFontFamily = pfc.Families[0];
        }

        private void inizia(object sender, EventArgs e)
        {
            if (this.Size.Width < gioco.Size.Width)
            {
                this.DesktopLocation = new Point(this.Location.X - 4, this.Location.Y);
                this.Size = new Size(this.Size.Width != gioco.Size.Width ? this.Size.Width + 8 : this.Size.Width, 30);

            }

            if (this.Size.Width >= gioco.Size.Width)
            {
                this.DesktopLocation = new Point(this.Location.X, this.Location.Y - 4);
                this.Size = new Size(this.Size.Width, this.Size.Height != gioco.Size.Height + 30 ? this.Size.Height + 8 : this.Size.Height);
            }

            if (this.Size.Width >= gioco.Size.Width && this.Size.Height >= gioco.Size.Height + 30)
            {
                start.Stop();
                startPlayer.Play();
                gioco.Inizia(4000);
            }
        }

        private void mortePacman(object sender, EventArgs e)
        {
            mortoPlayer.Play();
            mappa.pacmanMuori();
            gioco.Vite--;
            if (gioco.Vite >= 0)
            {
                mappa.Ripristina();
            }
            else
                gioco.gameOver();
        }

        private void incrementoPunteggioSpeciale(object sender, EventArgs e)
        {
            puntiGhosts = 200;
            gioco.Punti += 50;

            eat_powPlayer.Play();
            if ((gioco.Punti - 50) / 10000 < gioco.Punti / 10000 && gioco.Vite <= 5)
            {
                gioco.Vite++;
                extra_livesPlayer.Play();
            }
        }

        private void incrementoPunteggio(object sender, EventArgs e)
        {
            gioco.Punti += 10;
            eat_pillPlayer.Play();
            if ((gioco.Punti - 10) / 10000 < gioco.Punti / 10000 && gioco.Vite <= 5)
            {
                gioco.Vite++;
                extra_livesPlayer.Play();
            }
        }

        private void incrementoPunteggioGhost(object sender, EventArgs e)
        {
            gioco.Punti += puntiGhosts;
            eat_ghostPlayer.Play();

            if ((gioco.Punti - puntiGhosts) / 10000 < gioco.Punti / 10000 && gioco.Vite <= 5)
            {
                gioco.Vite++;
                extra_livesPlayer.Play();
            }

            puntiGhosts *= 2;
        }

        private void vinto(object sender, EventArgs e)
        {
            riderePlayer.Play();
            gioco.nextLivel();
        }

        private void MainPacman_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
                mappa.direzionePacman = 0;
            else
                if (e.KeyCode == Keys.Up)
                    mappa.direzionePacman = 1;
                else
                    if (e.KeyCode == Keys.Right)
                        mappa.direzionePacman = 2;
                    else
                        if (e.KeyCode == Keys.Down)
                            mappa.direzionePacman = 3;
                        else
                            if (e.KeyCode == Keys.Space)
                            {
                                if (gioco.statoGioco)
                                    gioco.Pause();
                                else
                                    gioco.Start();

                            }
        }

        private void lbl_Close_MouseEnter(object sender, EventArgs e)
        {
            ((Label)sender).ForeColor = Color.Red;
        }

        private void lbl_Close_MouseLeave(object sender, EventArgs e)
        {
            ((Label)sender).ForeColor = Color.White;
        }

        private void lbl_minimizza_MouseEnter(object sender, EventArgs e)
        {
            ((Label)sender).ForeColor = Color.Blue;
        }

        private void lbl_Close_Click(object sender, EventArgs e)
        {
            bool temp;
            if(temp = gioco.statoGioco)
                gioco.pausePlayMouseClick(null, null);

            if (MessageBox.Show("Uscire dal gioco?", "Attenzione", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
            else
                if (temp)
                    gioco.pausePlayMouseClick(null, null);
        }

        private void lbl_minimizza_Click(object sender, EventArgs e)
        {
            if (gioco.statoGioco)
                gioco.pausePlayMouseClick(null, null);

            this.WindowState = FormWindowState.Minimized;
        }
        
        private void MainPacman_MouseDown(object sender, MouseEventArgs e)
        {
            this.drag = true;
            this.startPoint = new Point(e.X, e.Y);
        }

        private void MainPacman_MouseUp(object sender, MouseEventArgs e)
        {
            this.drag = false;
        }

        private void MainPacman_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.drag)
            {
                Point p1 = new Point(e.X, e.Y);
                Point p2 = this.PointToScreen(p1);
                Point p3 = new Point(p2.X - this.startPoint.X, p2.Y - this.startPoint.Y);
                this.Location = p3;
            }
        }

        private void lbl_info_Click(object sender, EventArgs e)
        {
            if (destraSinistraInfo < 0)
            {
                gioco.infoActive = false;
                destraSinistraInfo = 5;
                spostaInfo.StartP();
                gioco.Start();
            }
            else
            {
                gioco.infoActive = true;
                destraSinistraInfo = -5;
                spostaInfo.StartP();
                gioco.Pause();
            }
        }

        private void spostaSinistraInfo(object sender, EventArgs e)
        {
            info.Location = new Point(info.Location.X + destraSinistraInfo, info.Location.Y);

            if ((info.Location.X <= 5 && destraSinistraInfo < 0) || (info.Location.X >= gioco.Size.Width && destraSinistraInfo > 0))
            {
                spostaInfo.StopP();
            }
        }

        private void Pic_Setting_MouseEnter(object sender, EventArgs e)
        {
            Pic_Setting.Image = Properties.Resources.setting2;
        }

        private void Pic_Setting_MouseLeave(object sender, EventArgs e)
        {
            Pic_Setting.Image = Properties.Resources.setting;
        }

        private void Pic_Setting_Click(object sender, EventArgs e)
        {
            if (sopraSottoImpostazioni == -5)
            {
                sopraSottoImpostazioni = 5;
                impostazioni.Enabled = true;
            }
            else
            {
                sopraSottoImpostazioni = -5;
                impostazioni.Enabled = false;
            }

            spostaImpostazioni.StartP();
        }

        private void spostaSinistraImpostazioni(object sender, EventArgs e)
        {
            impostazioni.Location = new Point(this.Size.Width / 2 - impostazioni.Size.Width / 2, impostazioni.Location.Y + sopraSottoImpostazioni);

            if ((impostazioni.Location.Y >= 0 && sopraSottoImpostazioni > 0) || (impostazioni.Location.Y <= -impostazioni.Size.Height && sopraSottoImpostazioni < 0))
                spostaImpostazioni.StopP();
        }
    }
}