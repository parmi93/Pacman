using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;

namespace Pacman
{
    class Mappa : PictureBox
    {
        public event EventHandler incrementoPunteggio;
        public event EventHandler incrementoPunteggioSpeciale;
        public event EventHandler mortePacman;
        public event EventHandler morteGhost;
        public event EventHandler vinto;
        int prossimaDirezione = -1, direzioneAttuale, coordinataXPacman, coordinataYPacman, direzioneInizialePacman;
        List<int[]> zonaNavigabileOrizzontale = new List<int[]>(), zonaNavigabileVerticale = new List<int[]>();
        List<int[]> pilloleOrizzontale, pilloleVerticale, pilloleSpeciali;
        List<PictureBox[]> pilloleOrizzontalePic = new List<PictureBox[]>();
        List<PictureBox[]> pilloleVerticalePic = new List<PictureBox[]>();
        List<PictureBox[]> specialPillolePic = new List<PictureBox[]>();
        int[,] zoneSpeciali = new int[0, 0];
        Pacman pacman;
        Ghost[] ghosts;
        datiGhost[] datiGhosts;
        Timer pacmanSpeed_ = new Timer();
        Timer ritardoRipristina = new Timer();
        bool ripristina;
        public int raggio = 40000;

        GraphicsPath circonferenzaPath;

        public Mappa(Image sfondo, List<int[]> zonaNavigabileOrizzontale, List<int[]> zonaNavigabileVerticale, Pacman pacman, int coordinataXPacman, int coordinataYPacman, int direzioneInizialePacman, Timer pacmanSpeed, List<int[]> pilloleOrizzontale, List<int[]> pilloleVerticale, List<int[]> pilloleSpeciali)
        {
            circonferenzaPath = new GraphicsPath();
            circonferenzaPath.AddEllipse(0, 0, 12, 12);

            this.zonaNavigabileOrizzontale = zonaNavigabileOrizzontale;
            this.zonaNavigabileVerticale = zonaNavigabileVerticale;
            this.coordinataXPacman = coordinataXPacman;
            this.coordinataYPacman = coordinataYPacman;
            this.pacman = pacman;
            this.Controls.Add(pacman);
            this.Image = sfondo;
            this.BackColor = Color.Black;
            this.Size = sfondo.Size;
            this.pilloleOrizzontale = pilloleOrizzontale;
            this.pilloleVerticale = pilloleVerticale;
            this.pilloleSpeciali = pilloleSpeciali;
            this.ritardoRipristina.Intervallo = 2000;
            this.ritardoRipristina.Tick += Ripristina;

            this.pacmanSpeed_ = pacmanSpeed;
            this.pacmanSpeed = pacmanSpeed.Intervallo;
            this.direzioneInizialePacman = direzioneAttuale = direzioneInizialePacman;
            pacmanSpeed_.Tick += spostaPacman;

            posizionaPacman();
            posizionaPillole();
        }

        public Mappa(Image sfondo, List<int[]> zonaNavigabileOrizzontale, List<int[]> zonaNavigabileVerticale, Pacman pacman, int coordinataXPacman, int coordinataYPacman, int direzioneInizialePacman, Timer pacmanSpeed, List<int[]> pilloleOrizzontale, List<int[]> pilloleVerticale, List<int[]> pilloleSpeciali, Ghost[] ghosts, ref datiGhost[] datiGhosts, int[,] zoneSpeciali)
            : this(sfondo, zonaNavigabileOrizzontale, zonaNavigabileVerticale, pacman, coordinataXPacman, coordinataYPacman, direzioneInizialePacman, pacmanSpeed, pilloleOrizzontale, pilloleVerticale, pilloleSpeciali)
        {
            this.zoneSpeciali = zoneSpeciali;
            this.ghosts = ghosts;
            this.datiGhosts = datiGhosts;

            for (int i = 0; i < ghosts.Length; i++)
            {
                this.Controls.Add(this.ghosts[i]);
                this.ghosts[i].Location = datiGhosts[i].puntoPartenza;
                this.ghosts[i].BringToFront();
                this.ghosts[i].Tag = i;
                this.ghosts[i].fineAfraid += velocitaNormaleGhost;
                datiGhosts[i].speed.Tick += spostaGhost;
                datiGhosts[i].durataMorte.Tick += rinascitaGhost;
                datiGhosts[i].vivoMorto = true;
            }

            pacman.BringToFront();
        }

        public int direzionePacman
        {
            get
            {
                return direzioneAttuale;
            }

            set
            {
                if (value >= 0 && value <= 4)
                    prossimaDirezione = value;
            }
        }

        public int pacmanSpeed
        {
            get
            {
                return pacmanSpeed_.Intervallo;
            }

            set
            {
                if (value > 0)
                    pacmanSpeed_.Intervallo = value;
                else
                    pacmanSpeed_.Intervallo = 29;

                this.pacman.impostaVelocitàAnimazione = (int)((double)(57 / 29) * (double)pacmanSpeed_.Intervallo);
            }
        }

        public void Start()
        {
            if (!ripristina)
            {
                pacmanSpeed_.StartP();
                pacman.Start();

                for (int i = 0; i < ghosts.Length; i++)
                {
                    ghosts[i].Start();
                    datiGhosts[i].speed.StartP();
                    if (!datiGhosts[i].vivoMorto)
                        datiGhosts[i].durataMorte.StartP();
                }
            }
            else
                ritardoRipristina.StartP();
        }

        public void Pause()
        {
            pacmanSpeed_.Pause();
            pacman.Pause();
            if(ripristina)
                ritardoRipristina.Pause();

            for (int i = 0; i < ghosts.Length; i++)
            {
                ghosts[i].Pause();
                datiGhosts[i].speed.Pause();
                if(!datiGhosts[i].vivoMorto)
                    datiGhosts[i].durataMorte.Pause();
            }
        }

        public void pacmanMuori()
        {
            pacman.Muori();
            pacmanSpeed_.Pause();

            for (int i = 0; i < ghosts.Length; i++)
            {
                ghosts[i].Pause();
                datiGhosts[i].speed.Pause();
                if (!datiGhosts[i].vivoMorto)
                    datiGhosts[i].durataMorte.Pause();
            }
        }

        public void Ripristina()
        {
            ripristina = true;
            ritardoRipristina.StartP();
        }

        public void posizionaPacman()
        {
            pacman.Location = new Point(coordinataXPacman, coordinataYPacman);
        }

        public void posizionaPillole()
        {
            while(0 != pilloleOrizzontalePic.Count)
            {
                for (int c = 0; c < pilloleOrizzontalePic[0].Length; c++)
                    if(pilloleOrizzontalePic[0][c] != null) 
                        pilloleOrizzontalePic[0][c].Dispose();
                pilloleOrizzontalePic.RemoveAt(0);
            }

            while (0 != pilloleVerticalePic.Count)
            {
                for (int c = 0; c < pilloleVerticalePic[0].Length; c++)
                    if (pilloleVerticalePic[0][c] != null) 
                        pilloleVerticalePic[0][c].Dispose();
                pilloleVerticalePic.RemoveAt(0);
            }

            while (0 != specialPillolePic.Count)
            {
                for (int c = 0; c < specialPillolePic[0].Length; c++)
                    if (specialPillolePic[0][c] != null)
                        specialPillolePic[0][c].Dispose();
                specialPillolePic.RemoveAt(0);
            }

            for (int i = 0; i < pilloleOrizzontale.Count; i++)
            {
                int temp = 0;
                for (int c = 3; c < pilloleOrizzontale[i].Length; c += 3)
                    temp += pilloleOrizzontale[i][c];


                pilloleOrizzontalePic.Add(new PictureBox[temp]);
            }

            for (int i = 0; i < pilloleOrizzontale.Count; i++)
            {
                for (int c = 1, jj = 0; c < pilloleOrizzontale[i].Length; c += 3)
                {
                    for (int j = 0; j < pilloleOrizzontale[i][c + 2]; j++, jj++)
                    {
                        pilloleOrizzontalePic[i][jj] = new PictureBox();
                        pilloleOrizzontalePic[i][jj].Size = new Size(3, 3);
                        pilloleOrizzontalePic[i][jj].Image = Properties.Resources.pill;
                        Controls.Add(pilloleOrizzontalePic[i][jj]);
                        pilloleOrizzontalePic[i][jj].Location = new Point((int)(pilloleOrizzontale[i][c] + (((double)(pilloleOrizzontale[i][c + 1] - pilloleOrizzontale[i][c] + 1) / (double)pilloleOrizzontale[i][c + 2]) * (double)j)), pilloleOrizzontale[i][0]);
                    }
                }
            }


            for (int i = 0; i < pilloleVerticale.Count; i++)
            {
                int temp = 0;
                for (int c = 3; c < pilloleVerticale[i].Length; c += 3)
                    temp += pilloleVerticale[i][c];


                pilloleVerticalePic.Add(new PictureBox[temp]);
            }

            for (int i = 0; i < pilloleVerticale.Count; i++)
            {
                for (int c = 1, jj = 0; c < pilloleVerticale[i].Length; c += 3)
                {
                    for (int j = 0; j < pilloleVerticale[i][c + 2]; j++, jj++)
                    {
                        pilloleVerticalePic[i][jj] = new PictureBox();
                        pilloleVerticalePic[i][jj].Size = new Size(3, 3);
                        pilloleVerticalePic[i][jj].Image = Properties.Resources.pill;
                        Controls.Add(pilloleVerticalePic[i][jj]);
                        pilloleVerticalePic[i][jj].Location = new Point(pilloleVerticale[i][0], (int)(pilloleVerticale[i][c] + (((double)(pilloleVerticale[i][c + 1] - pilloleVerticale[i][c] + 1) / (double)pilloleVerticale[i][c + 2]) * (double)j)));
                    }
                }
            }

            for (int i = 0; i < pilloleSpeciali.Count; i++)
            {
                int temp = 0;
                for (int c = 3; c < pilloleSpeciali[i].Length; c += 3)
                    temp += pilloleSpeciali[i][c];


                specialPillolePic.Add(new PictureBox[temp]);
            }

            for (int i = 0; i < pilloleSpeciali.Count; i++)
            {
                for (int c = 1, jj = 0; c < pilloleSpeciali[i].Length; c += 3)
                {
                    for (int j = 0; j < pilloleSpeciali[i][c + 2]; j++, jj++)
                    {
                        specialPillolePic[i][jj] = new PictureBox();
                        specialPillolePic[i][jj].Size = new Size(12, 12);
                        specialPillolePic[i][jj].Paint += new PaintEventHandler(PillolaSpeciale_Paint);
                        specialPillolePic[i][jj].Image = Properties.Resources.specialPill;
                        Controls.Add(specialPillolePic[i][jj]);
                        specialPillolePic[i][jj].Location = new Point((int)(pilloleSpeciali[i][c] + (((double)(pilloleSpeciali[i][c + 1] - pilloleSpeciali[i][c] + 1) / (double)pilloleSpeciali[i][c + 2]) * (double)j)), pilloleSpeciali[i][0]);
                    }
                }
            }


        }

        private void PillolaSpeciale_Paint(object sender, PaintEventArgs e)
        {
            ((PictureBox)sender).Region = new Region(circonferenzaPath);
        }

        private void spostaPacman(object sender, EventArgs e)
        {
            bool temp = true, flag = true;
            int tempX, tempY, tempDirezione;

            if (prossimaDirezione != -1)
            {
                if (direzioneAttuale == 1 || direzioneAttuale == 2)
                    tempDirezione = 1;
                else
                    tempDirezione = -1;

                if (prossimaDirezione == 0)
                {
                    if (navigabile(tempX = pacman.Location.X - 3, tempY = pacman.Location.Y) || navigabile(tempX, tempY -= tempDirezione) || navigabile(tempX, tempY -= tempDirezione))
                    {
                        pacman.Location = new Point(tempX, tempY);
                        pacman.Svolta = 0;
                        direzioneAttuale = prossimaDirezione;
                        prossimaDirezione = -1;
                        temp = false;
                        pacmanGhostSovrapposto();
                    }
                }
                else
                    if (prossimaDirezione == 1)
                    {
                        if (navigabile(tempX = pacman.Location.X, tempY = pacman.Location.Y - 3) || navigabile(tempX += tempDirezione, tempY) || navigabile(tempX += tempDirezione, tempY))
                        {
                            pacman.Location = new Point(tempX, tempY);
                            pacman.Svolta = 1;
                            direzioneAttuale = prossimaDirezione;
                            prossimaDirezione = -1;
                            temp = false;
                            pacmanGhostSovrapposto();
                        }
                    }
                    else
                        if (prossimaDirezione == 2)
                        {
                            if (navigabile(tempX = pacman.Location.X + 3, tempY = pacman.Location.Y) || navigabile(tempX, tempY -= tempDirezione) || navigabile(tempX, tempY -= tempDirezione))
                            {
                                pacman.Location = new Point(tempX, tempY);
                                pacman.Svolta = 2;
                                direzioneAttuale = prossimaDirezione;
                                prossimaDirezione = -1;
                                temp = false;
                                pacmanGhostSovrapposto();
                            }
                        }
                        else
                            if (prossimaDirezione == 3)
                            {
                                if (navigabile(tempX = pacman.Location.X, tempY = pacman.Location.Y + 3) || navigabile(tempX += tempDirezione, tempY) || navigabile(tempX += tempDirezione, tempY))
                                {
                                    pacman.Location = new Point(tempX, tempY);
                                    pacman.Svolta = 3;
                                    direzioneAttuale = prossimaDirezione;
                                    prossimaDirezione = -1;
                                    temp = false;
                                    pacmanGhostSovrapposto();
                                }
                            }
            }

            if (temp)
            {
                if (direzioneAttuale == 0)
                {
                    if (flag = (navigabile(tempX = pacman.Location.X - 3, tempY = pacman.Location.Y) || navigabile(++tempX, tempY) || navigabile(++tempX, tempY)))
                    {
                        pacman.Location = new Point(tempX, tempY);
                        pacmanGhostSovrapposto();
                    }
                }
                else
                    if (direzioneAttuale == 1)
                    {
                        if (flag = (navigabile(tempX = pacman.Location.X, tempY = pacman.Location.Y - 3) || navigabile(tempX, ++tempY) || navigabile(tempX, ++tempY)))
                        {
                            pacman.Location = new Point(tempX, tempY);
                            pacmanGhostSovrapposto();
                        }
                    }
                    else
                        if (direzioneAttuale == 2)
                        {
                            if (flag = (navigabile(tempX = pacman.Location.X + 3, tempY = pacman.Location.Y) || navigabile(--tempX, tempY) || navigabile(--tempX, tempY)))
                            {
                                pacman.Location = new Point(tempX, tempY);
                                pacmanGhostSovrapposto();
                            }
                        }
                        else
                            if (direzioneAttuale == 3)
                            {
                                if (flag = (navigabile(tempX = pacman.Location.X, tempY = pacman.Location.Y + 3) || navigabile(tempX, --tempY) || navigabile(tempX, --tempY)))
                                {
                                    pacman.Location = new Point(tempX, tempY);
                                    pacmanGhostSovrapposto();
                                }
                            }
            }

            if (temp && !flag && !zonaSpeciale(pacman.Location.X, pacman.Location.Y, pacman))
                pacman.Pause();
            else
                if ((!temp || flag) && pacman.StartPause == 1)
                    pacman.Start();

            bool esci = false;
            for (int i = 0; i < pilloleOrizzontalePic.Count && !esci; i++)
            {
                bool azzerato = true;
                for (int c = 0; c < pilloleOrizzontalePic[i].Length; c++)
                    if (pilloleOrizzontalePic[i][c] != null)
                    {
                        if (esci = (pilloleOrizzontalePic[i][c].Location.Y >= pacman.Location.Y && pilloleOrizzontalePic[i][c].Location.Y + 2 <= pacman.Location.Y + 21))
                            for (int j = c; j < pilloleOrizzontalePic[i].Length; j++)
                                if (pilloleOrizzontalePic[i][j] != null && pilloleOrizzontalePic[i][j].Location.X >= pacman.Location.X && pilloleOrizzontalePic[i][j].Location.X + 2 <= pacman.Location.X + 21)
                                {
                                    pilloleOrizzontalePic[i][j].Dispose();
                                    pilloleOrizzontalePic[i][j] = null;
                                    OnIncrementoPunteggio(EventArgs.Empty);
                                    break;
                                }

                        azzerato = false;
                        break;
                    }

                if (azzerato)
                    pilloleOrizzontalePic.RemoveAt(i);

                if (pilloleOrizzontalePic.Count == 0 && pilloleVerticalePic.Count == 0 && specialPillolePic.Count == 0)
                    OnVinto(EventArgs.Empty);
            }

            esci = false;
            for (int i = 0; i < pilloleVerticalePic.Count && !esci; i++)
            {
                bool azzerato = true;
                for (int c = 0; c < pilloleVerticalePic[i].Length; c++)
                    if (pilloleVerticalePic[i][c] != null)
                    {
                        if (esci = (pilloleVerticalePic[i][c].Location.X >= pacman.Location.X && pilloleVerticalePic[i][c].Location.X + 2 <= pacman.Location.X + 21))
                            for (int j = c; j < pilloleVerticalePic[i].Length; j++)
                                if (pilloleVerticalePic[i][j] != null && pilloleVerticalePic[i][j].Location.Y >= pacman.Location.Y && pilloleVerticalePic[i][j].Location.Y + 2 <= pacman.Location.Y + 21)
                                {
                                    pilloleVerticalePic[i][j].Dispose();
                                    pilloleVerticalePic[i][j] = null;
                                    OnIncrementoPunteggio(pilloleVerticalePic[i][j]);
                                    break;
                                }

                        azzerato = false;
                        break;
                    }

                if (azzerato)
                    pilloleVerticalePic.RemoveAt(i);

                if (pilloleOrizzontalePic.Count == 0 && pilloleVerticalePic.Count == 0 && specialPillolePic.Count == 0)
                    OnVinto(EventArgs.Empty);
            }

            esci = false;
            for (int i = 0; i < specialPillolePic.Count && !esci; i++)
            {
                bool azzerato = true;
                for (int c = 0; c < specialPillolePic[i].Length; c++)
                    if (specialPillolePic[i][c] != null)
                    {
                        if (esci = (specialPillolePic[i][c].Location.Y >= pacman.Location.Y && specialPillolePic[i][c].Location.Y + 2 <= pacman.Location.Y + 21))
                            for (int j = c; j < specialPillolePic[i].Length; j++)
                                if (specialPillolePic[i][j] != null && specialPillolePic[i][j].Location.X >= pacman.Location.X && specialPillolePic[i][j].Location.X + 2 <= pacman.Location.X + 21)
                                {
                                    specialPillolePic[i][j].Dispose();
                                    specialPillolePic[i][j] = null;
                                    OnIncrementoPunteggioSpeciale(EventArgs.Empty);
                                    break;
                                }

                        azzerato = false;
                        break;
                    }

                if (azzerato)
                    specialPillolePic.RemoveAt(i);

                if (pilloleOrizzontalePic.Count == 0 && pilloleVerticalePic.Count == 0 && specialPillolePic.Count == 0)
                    OnVinto(EventArgs.Empty);
            }
        }

        private void spostaGhost(object sender, EventArgs e)
        {
            int tempX, tempY, n = (int)((Timer)sender).Tag;
            bool flag = true;
            Random ran = new Random((int)DateTime.Now.Ticks);

            int[] cross = incrocio(ghosts[n].Location.X, ghosts[n].Location.Y, datiGhosts[n].direzione);


            if (cross[0] != 0)
            {
                bool scappaRincorri = ghosts[n].Stato == 1 || ghosts[n].Stato == 2;

                int distanza = (pacman.Location.X - ghosts[n].Location.X) * (pacman.Location.X - ghosts[n].Location.X) + (pacman.Location.Y - ghosts[n].Location.Y) * (pacman.Location.Y - ghosts[n].Location.Y);
                if (distanza < raggio && ghosts[n].Stato != -1)
                {
                    int min = 0, temp;
                    for (int i = 0; i < cross[0]; i++)
                        if ((temp = (pacman.Location.X - cross[i * 3 + 2]) * (pacman.Location.X - cross[i * 3 + 2]) + (pacman.Location.Y - cross[i * 3 + 3]) * (pacman.Location.Y - cross[i * 3 + 3])) < distanza ^ scappaRincorri)
                        {
                            min = i;
                            distanza = temp;
                        }

                    int tempRan = ran.Next(0, cross[0]) * 3 + 1;
                    ghosts[n].Svolta = cross[min * 3 + 1];
                    datiGhosts[n].direzione = ghosts[n].Svolta;
                    ghosts[n].Location = new Point(cross[min * 3 + 1 + 1], cross[min * 3 + 1 + 2]);
                }
                else
                {
                    int tempRan = ran.Next(0, cross[0]) * 3 + 1;
                    ghosts[n].Svolta = cross[tempRan];
                    datiGhosts[n].direzione = ghosts[n].Svolta;
                    ghosts[n].Location = new Point(cross[tempRan + 1], cross[tempRan + 2]);
                }

                pacmanGhostSovrapposto();
            }
            else
            {
                if (datiGhosts[n].direzione == 0)
                {
                    if (flag = (navigabile(tempX = ghosts[n].Location.X - 3, tempY = ghosts[n].Location.Y) || navigabile(++tempX, tempY) || navigabile(++tempX, tempY)))
                    {
                        ghosts[n].Location = new Point(tempX, tempY);
                        pacmanGhostSovrapposto();
                    }
                }
                else
                    if (datiGhosts[n].direzione == 1)
                    {
                        if (flag = (navigabile(tempX = ghosts[n].Location.X, tempY = ghosts[n].Location.Y - 3) || navigabile(tempX, ++tempY) || navigabile(tempX, ++tempY)))
                        {
                            ghosts[n].Location = new Point(tempX, tempY);
                            pacmanGhostSovrapposto();
                        }
                    }
                    else
                        if (datiGhosts[n].direzione == 2)
                        {
                            if (flag = (navigabile(tempX = ghosts[n].Location.X + 3, tempY = ghosts[n].Location.Y) || navigabile(--tempX, tempY) || navigabile(--tempX, tempY)))
                            {
                                ghosts[n].Location = new Point(tempX, tempY);
                                pacmanGhostSovrapposto();
                            }
                        }
                        else
                            if (datiGhosts[n].direzione == 3)
                            {
                                if (flag = (navigabile(tempX = ghosts[n].Location.X, tempY = ghosts[n].Location.Y + 3) || navigabile(tempX, --tempY) || navigabile(tempX, --tempY)))
                                {
                                    ghosts[n].Location = new Point(tempX, tempY);
                                    pacmanGhostSovrapposto();
                                }
                            }
            }

            if (!flag)
            {
                zonaSpeciale(ghosts[n].Location.X, ghosts[n].Location.Y, ghosts[n]);
                pacmanGhostSovrapposto();
            }
        }

        private void rinascitaGhost(object sender, EventArgs e)
        {
            datiGhosts[(int)(((Timer)sender).Tag)].speed.Intervallo = datiGhosts[(int)(((Timer)sender).Tag)].speedIniziale;
            ghosts[(int)(((Timer)sender).Tag)].animazione(null, null);
            ghosts[(int)(((Timer)sender).Tag)].Ripristina();
            //ghosts[(int)(((Timer)sender).Tag)].Location = datiGhosts[(int)(((Timer)sender).Tag)].puntoPartenza;
            ((Timer)sender).StopP();
        }

        public void Ripristina(object sender, EventArgs e)
        {
            ritardoRipristina.StopP();
            ripristina = false;
            posizionaPacman();
            pacman.Ripristina();
            pacman.Svolta = direzioneAttuale = direzioneInizialePacman;

            for (int i = 0; i < ghosts.Length; i++)
            {
                this.ghosts[i].Location = datiGhosts[i].puntoPartenza;
                this.ghosts[i].Svolta = datiGhosts[i].direzione;
                this.datiGhosts[i].speed.Intervallo = datiGhosts[i].speedIniziale;
                this.ghosts[i].Ripristina();
                this.datiGhosts[i].vivoMorto = true;
                this.datiGhosts[i].durataMorte.StopP();
            }
            if(sender != null)
                this.Start();
        }

        private void velocitaNormaleGhost(object sender, EventArgs e)
        {
            for (int i = 0; i < ghosts.Length; i++)
                datiGhosts[i].speed.Intervallo = datiGhosts[i].speedIniziale;
        }

        private bool navigabile(int x, int y)
        {
            //Graphics temp = this.CreateGraphics();
            //Pen pen = new Pen(Color.Red, 1);

            for (int i = 0; i < zonaNavigabileOrizzontale.Count; i++)
                if (y == zonaNavigabileOrizzontale[i][0])
                {
                    for (int c = 1; c < zonaNavigabileOrizzontale[i].Length; c += 2)
                    {
                        //temp.DrawLine(pen, new Point(zonaNavigabileOrizzontale[i][c], zonaNavigabileOrizzontale[i][0]), new Point(zonaNavigabileOrizzontale[i][c + 1], zonaNavigabileOrizzontale[i][0]));

                        if (x >= zonaNavigabileOrizzontale[i][c] && x <= zonaNavigabileOrizzontale[i][c + 1])
                            return true;
                    }
                    break;
                }


            for (int i = 0; i < zonaNavigabileVerticale.Count; i++)
                if (x == zonaNavigabileVerticale[i][0])
                {
                    for (int c = 1; c < zonaNavigabileVerticale[i].Length; c += 2)
                    {
                        //temp.DrawLine(pen, new Point(zonaNavigabileVerticale[i][0], zonaNavigabileVerticale[i][c]), new Point(zonaNavigabileVerticale[i][0], zonaNavigabileVerticale[i][c + 1]));

                        if (y >= zonaNavigabileVerticale[i][c] && y <= zonaNavigabileVerticale[i][c + 1])
                            return true;
                    }
                    break;
                }

            return false;
        }

        private int[] incrocio(int x, int y, int direzione)
        {
            int tempX, tempY, tempDirezione, indice = 1;
            int[] ritorno = new int[10];

            if (direzione == 0 || direzione == 1)
                tempDirezione = 1;
            else
                tempDirezione = -1;

            if (direzione == 0 || direzione == 2)
            {
                tempX = x;
                tempY = y - 3;
                if (navigabile(tempX, tempY) || navigabile(tempX -= tempDirezione, tempY) || navigabile(tempX -= tempDirezione, tempY))
                {
                    ritorno[indice++] = 1;
                    ritorno[indice++] = tempX;
                    ritorno[indice++] = tempY;

                    if (navigabile(tempX, tempY = y + 3))
                    {
                        ritorno[indice++] = 3;
                        ritorno[indice++] = tempX;
                        ritorno[indice++] = tempY;
                    }

                    if (navigabile(tempX -= tempDirezione, tempY = y))
                    {
                        ritorno[indice++] = direzione;
                        ritorno[indice++] = tempX;
                        ritorno[indice++] = tempY;
                    }
                }

                if (indice == 1)
                {
                    tempX = x;
                    tempY = y + 3;
                    if (navigabile(tempX, tempY) || navigabile(tempX -= tempDirezione, tempY) || navigabile(tempX -= tempDirezione, tempY))
                    {
                        ritorno[indice++] = 3;
                        ritorno[indice++] = tempX;
                        ritorno[indice++] = tempY;

                        if (navigabile(tempX -= tempDirezione, tempY = y))
                        {
                            ritorno[indice++] = direzione;
                            ritorno[indice++] = tempX;
                            ritorno[indice++] = tempY;
                        }
                    }
                }
            }
            else
            {
                tempX = x - 3;
                tempY = y;
                if (navigabile(tempX, tempY) || navigabile(tempX, tempY -= tempDirezione) || navigabile(tempX, tempY -= tempDirezione))
                {
                    ritorno[indice++] = 0;
                    ritorno[indice++] = tempX;
                    ritorno[indice++] = tempY;

                    if (navigabile(tempX = x + 3, tempY))
                    {
                        ritorno[indice++] = 2;
                        ritorno[indice++] = tempX;
                        ritorno[indice++] = tempY;
                    }

                    if (navigabile(tempX = x, tempY -= tempDirezione))
                    {
                        ritorno[indice++] = direzione;
                        ritorno[indice++] = tempX;
                        ritorno[indice++] = tempY;
                    }
                }

                if (indice == 1)
                {
                    tempX = x + 3;
                    tempY = y;
                    if (navigabile(tempX, tempY) || navigabile(tempX, tempY -= tempDirezione) || navigabile(tempX, tempY -= tempDirezione))
                    {
                        ritorno[indice++] = 2;
                        ritorno[indice++] = tempX;
                        ritorno[indice++] = tempY;

                        if (navigabile(tempX = x, tempY -= tempDirezione))
                        {
                            ritorno[indice++] = direzione;
                            ritorno[indice++] = tempX;
                            ritorno[indice++] = tempY;
                        }
                    }
                }
            }

            ritorno[0] = (indice - 1) / 3;

            return ritorno;
        }

        private bool zonaSpeciale(int x, int y, Personaggi personaggio)
        {
            for (int i = 0, righe = zoneSpeciali.Length / 4; i < righe; i++)
            {
                if (x == zoneSpeciali[i, 0] && y == zoneSpeciali[i, 1])
                {
                    personaggio.Location = new Point(zoneSpeciali[i, 2], zoneSpeciali[i, 3]);
                    personaggio.Svolta = zoneSpeciali[i, 4];

                    if (personaggio.info == "pacman")
                        direzioneAttuale = zoneSpeciali[i, 4];

                    return true;
                }
            }

            return false;
        }

        private void pacmanGhostSovrapposto()
        {
            for (int i = 0; i < ghosts.Length; i++)
                if (pacman.Location.X <= ghosts[i].Location.X + 15 && pacman.Location.X + 15 >= ghosts[i].Location.X && pacman.Location.Y <= ghosts[i].Location.Y + 15 && pacman.Location.Y + 15 >= ghosts[i].Location.Y)
                {
                    if (ghosts[i].Stato == 1 || ghosts[i].Stato == 2)
                    {
                        ghosts[i].Muori();
                        datiGhosts[i].durataMorte.StartP();
                        datiGhosts[i].vivoMorto = false;
                        Application.DoEvents();
                        Thread.Sleep(600);
                        OnMorteGhost(EventArgs.Empty);
                    }
                    else
                        if (ghosts[i].Stato != -1 && pacman.VivoMorto == 0)
                        {
                            OnMortePacman(EventArgs.Empty);
                        }
                }
        }

        protected virtual void OnIncrementoPunteggio(Object sender)
        {
            if (incrementoPunteggio != null)
                incrementoPunteggio(sender, null);
        }

        protected virtual void OnIncrementoPunteggioSpeciale(Object sender)
        {
            for (int i = 0; i < ghosts.Length; i++)
            {
                ghosts[i].Afraid();
                datiGhosts[i].speed.Intervallo *= 2;
            }

            if (incrementoPunteggioSpeciale != null)
                incrementoPunteggioSpeciale(sender, null);
        }

        protected virtual void OnVinto(Object sender)
        {
            if (vinto != null)
                vinto(sender, null);
        }

        protected virtual void OnMortePacman(Object sender)
        {
            if (mortePacman != null)
            {
                mortePacman(sender, null);
            }
        }

        protected virtual void OnMorteGhost(Object sender)
        {
            if (morteGhost != null)
            {
                morteGhost(sender, null);
            }
        }
    }
}
