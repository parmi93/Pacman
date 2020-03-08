using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Pacman
{
    public class Personaggi : PictureBox
    {
        public string info;

        int direzione = 0;

        public int Svolta
        {
            get
            {
                return direzione;
            }

            set
            {
                if (0 <= value && value <= 4)
                    direzione = value;
            }
        }
    }

    public class Ghost : Personaggi
    {
        public event EventHandler fineAfraid;
        Image[,] frame;
        Image[,] afraidFrame;
        Image[] eyes;

        Timer velocitàAnimazioneTimer = new Timer();
        Timer velocitàAnimazioneAfraidTimer = new Timer();
        Timer velocitàAnimazioneAfraidEndTimer = new Timer();
        Timer velocitàAnimazioneMuoriTimer = new Timer();
        Timer afraidTimeTimer = new Timer();
        Timer afraidTimeEndTimer = new Timer();
        Timer afraidTimeEndInvertTimer = new Timer();

        int frameAttuale = 0, frameTotali = 6, ondeDestraSinistra = -1, afraidInvert = 1;
        bool startStop = false, animazioneStartStop = true, afraidStartStop = false, afraidEndStartStop = false, afraidEndInvertStartStop = false, muoriStartStop = false;
        string tipoGhost_;

        public Ghost(string tipoDiFantasma)
        {
            Svolta = 0;
            velocitàAnimazione = 100;
            afraidTime = 4500;
            afraidTimeEnd = 2500;
            afraidTimeEndInvert = 400;

            tipoGhost = tipoDiFantasma;
            associaMetodiAnimazione();


            this.Image = frame[Svolta, frameAttuale];
            this.Size = frame[0, 0].Size;
        }

        public Ghost(string tipoDiFantasma, int direzioneIniziale, int velocitàAnimazione, int afraidTime, int afraidTimeEnd, int afraidTimeEndInvert)
            : this(tipoDiFantasma)
        {
            Svolta = direzioneIniziale;
            this.velocitàAnimazione = velocitàAnimazione;
            this.afraidTime = afraidTime;
            this.afraidTimeEnd = afraidTimeEnd;
            this.afraidTimeEndInvert = afraidTimeEndInvert;
        }

        public int velocitàAnimazione
        {
            get
            {
                return velocitàAnimazioneTimer.Intervallo;
            }

            set
            {
                if (value > 0)
                    velocitàAnimazioneMuoriTimer.Intervallo = velocitàAnimazioneAfraidEndTimer.Intervallo = velocitàAnimazioneAfraidTimer.Intervallo = velocitàAnimazioneTimer.Intervallo = value;
                else
                    velocitàAnimazioneMuoriTimer.Intervallo = velocitàAnimazioneAfraidEndTimer.Intervallo = velocitàAnimazioneAfraidTimer.Intervallo = velocitàAnimazioneTimer.Intervallo = 100;
            }
        }

        public int afraidTime
        {
            get
            {
                return afraidTimeTimer.Intervallo;
            }

            set
            {
                if (value > 0)
                    afraidTimeTimer.Intervallo = value;
                else
                    afraidTimeTimer.Intervallo = 10000;
            }
        }

        public int afraidTimeEnd
        {
            get
            {
                return afraidTimeEndTimer.Intervallo;
            }

            set
            {
                if (value >= 0)
                    afraidTimeEndTimer.Intervallo = value;
                else
                    afraidTimeEndTimer.Intervallo = 3000;
            }
        }

        public int afraidTimeEndInvert
        {
            get
            {
                return afraidTimeEndInvertTimer.Intervallo;
            }

            set
            {
                if (value >= 0)
                    afraidTimeEndInvertTimer.Intervallo = value;
                else
                    afraidTimeEndInvertTimer.Intervallo = 400;
            }
        }

        public string tipoGhost
        {
            get
            {
                return tipoGhost_;
            }

            set
            {
                if (value != "blinky" && value != "clyde" && value != "inky" && value != "pinky")
                {
                    tipoGhost_ = "blinky";
                    caricaFrame("blinky");
                }
                else
                {
                    tipoGhost_ = value;
                    caricaFrame(value);
                }
            }
        }

        public int Stato
        {
            get
            {
                if (animazioneStartStop == false && afraidStartStop == false && afraidEndStartStop == true && afraidEndInvertStartStop == true && muoriStartStop == false)
                    return 2;
                else
                    if (animazioneStartStop == false && afraidStartStop == true && afraidEndStartStop == false && afraidEndInvertStartStop == false && muoriStartStop == false)
                        return 1;
                    else
                        if (muoriStartStop == false)
                            return 0;

                return -1;
            }
        }

        public void Start()
        {
            if (!startStop)
            {
                if (animazioneStartStop)
                    velocitàAnimazioneTimer.StartP();

                if (afraidStartStop)
                {
                    velocitàAnimazioneAfraidTimer.StartP();
                    afraidTimeTimer.StartP();
                }

                if (afraidEndStartStop)
                {
                    velocitàAnimazioneAfraidEndTimer.StartP();
                    afraidTimeEndTimer.StartP();
                }

                if (afraidEndInvertStartStop)
                    afraidTimeEndInvertTimer.StartP();

                if (muoriStartStop)
                    velocitàAnimazioneMuoriTimer.StartP();

                startStop = true;
            }
        }

        public void Pause()
        {
            if (startStop)
            {
                velocitàAnimazioneTimer.Pause();
                velocitàAnimazioneAfraidTimer.Pause();
                velocitàAnimazioneAfraidEndTimer.Pause();
                velocitàAnimazioneMuoriTimer.Pause();

                afraidTimeTimer.Pause();
                afraidTimeEndTimer.Pause();
                afraidTimeEndInvertTimer.Pause();

                startStop = false;
            }
        }

        public void Afraid()
        {
            if (startStop)
            {
                if (Stato != -1)
                {
                    velocitàAnimazioneTimer.StopP();
                    velocitàAnimazioneAfraidTimer.StopP();
                    velocitàAnimazioneAfraidEndTimer.StopP();
                    velocitàAnimazioneMuoriTimer.StopP();

                    afraidTimeTimer.StopP();
                    afraidTimeEndTimer.StopP();
                    afraidTimeEndInvertTimer.StopP();

                    animazioneStartStop = false;
                    afraidStartStop = true;
                    afraidEndStartStop = false;
                    afraidEndInvertStartStop = false;
                    muoriStartStop = false;

                    velocitàAnimazioneAfraidTimer.StartP();
                    afraidTimeTimer.StartP();
                }
            }
        }

        public void Muori()
        {
            velocitàAnimazioneTimer.StopP();
            velocitàAnimazioneAfraidTimer.StopP();
            velocitàAnimazioneAfraidEndTimer.StopP();

            afraidTimeTimer.StopP();
            afraidTimeEndTimer.StopP();
            afraidTimeEndInvertTimer.StopP();

            animazioneStartStop = false;
            afraidStartStop = false;
            afraidEndStartStop = false;
            afraidEndInvertStartStop = false;
            muoriStartStop = true;

            animazioneMuori(null, null);
            velocitàAnimazioneMuoriTimer.StartP();
        }

        public void Ripristina()
        {
            velocitàAnimazioneAfraidTimer.StopP();
            velocitàAnimazioneAfraidEndTimer.StopP();
            velocitàAnimazioneMuoriTimer.StopP();

            afraidTimeTimer.StopP();
            afraidTimeEndTimer.StopP();
            afraidTimeEndInvertTimer.StopP();

            animazioneStartStop = true;
            afraidStartStop = false;
            afraidEndStartStop = false;
            afraidEndInvertStartStop = false;
            muoriStartStop = false;

            velocitàAnimazioneTimer.StartP();
        }

        private void associaMetodiAnimazione()
        {
            velocitàAnimazioneTimer.Tick += new EventHandler(animazione);
            velocitàAnimazioneAfraidTimer.Tick += new EventHandler(animazioneAfraid);
            velocitàAnimazioneAfraidEndTimer.Tick += new EventHandler(animazioneAfraidEnd);
            velocitàAnimazioneMuoriTimer.Tick += new EventHandler(animazioneMuori);

            afraidTimeTimer.Tick += new EventHandler(stopAfraid);
            afraidTimeEndTimer.Tick += new EventHandler(stopAfraidEnd);
            afraidTimeEndInvertTimer.Tick += new EventHandler(animazioneAfraidEndInvert);
        }

        private void stopAfraid(object sender, EventArgs e)
        {
            afraidTimeTimer.StopP();
            velocitàAnimazioneAfraidTimer.StopP();

            afraidStartStop = false;
            afraidEndStartStop = true;
            afraidEndInvertStartStop = true;

            afraidTimeEndTimer.StartP();
            afraidTimeEndInvertTimer.StartP();
            velocitàAnimazioneAfraidEndTimer.StartP();
        }

        private void stopAfraidEnd(object sender, EventArgs e)
        {
            afraidTimeEndTimer.StopP();
            afraidTimeEndInvertTimer.StopP();
            velocitàAnimazioneAfraidEndTimer.StopP();

            animazioneStartStop = true;
            afraidEndStartStop = false;
            afraidEndInvertStartStop = false;

            velocitàAnimazioneTimer.StartP();
            Fine_Afraid(EventArgs.Empty);
        }

        public void animazione(object sender, EventArgs e)
        {
            if (frameAttuale == 0)
                ondeDestraSinistra = 1;
            else
                if (frameAttuale == frameTotali)
                    ondeDestraSinistra = -1;

            frameAttuale += ondeDestraSinistra;

            if (frameAttuale == frameTotali)
                frameAttuale = 0;
            else
                if (frameAttuale == 0)
                    frameAttuale = frameTotali;

            if (Svolta != 0)
                this.Image = frame[Svolta, (frameTotali - 1) - (frameAttuale % frameTotali)];
            else
                this.Image = frame[Svolta, frameAttuale % frameTotali];
        }

        private void animazioneAfraid(object sender, EventArgs e)
        {
            if (frameAttuale == 0)
                ondeDestraSinistra = 1;
            else
                if (frameAttuale == frameTotali)
                    ondeDestraSinistra = -1;

            frameAttuale += ondeDestraSinistra;

            if (frameAttuale == frameTotali)
                frameAttuale = 0;
            else
                if (frameAttuale == 0)
                    frameAttuale = frameTotali;

            if (Svolta != 0)
                this.Image = afraidFrame[0, (frameTotali - 1) - (frameAttuale % frameTotali)];
            else
                this.Image = afraidFrame[0, frameAttuale % frameTotali];
        }

        private void animazioneAfraidEnd(object sender, EventArgs e)
        {
            if (frameAttuale == 0)
                ondeDestraSinistra = 1;
            else
                if (frameAttuale == frameTotali)
                    ondeDestraSinistra = -1;

            frameAttuale += ondeDestraSinistra;

            if (frameAttuale == frameTotali)
                frameAttuale = 0;
            else
                if (frameAttuale == 0)
                    frameAttuale = frameTotali;

            if (Svolta != 0)
                this.Image = afraidFrame[afraidInvert, (frameTotali - 1) - (frameAttuale % frameTotali)];
            else
                this.Image = afraidFrame[afraidInvert, frameAttuale % frameTotali];
        }

        private void animazioneAfraidEndInvert(object sender, EventArgs e)
        {
            if (afraidInvert == 1)
                afraidInvert = 0;
            else
                afraidInvert = 1;
        }

        private void animazioneMuori(object sender, EventArgs e)
        {
            this.Image = eyes[Svolta];
        }

        private void caricaFrame(string tipoDiFantasma)
        {
            frame = new Image[4, 6];
            afraidFrame = new Image[2, 6];
            eyes = new Image[4];

            if (tipoDiFantasma == "blinky")
            {
                frame[0, 0] = Properties.Resources.blinky1Left;
                frame[0, 1] = Properties.Resources.blinky2Left;
                frame[0, 2] = Properties.Resources.blinky3Left;
                frame[0, 3] = Properties.Resources.blinky4Left;
                frame[0, 4] = Properties.Resources.blinky5Left;
                frame[0, 5] = Properties.Resources.blinky6Left;

                frame[1, 0] = Properties.Resources.blinky1Up;
                frame[1, 1] = Properties.Resources.blinky2Up;
                frame[1, 2] = Properties.Resources.blinky3Up;
                frame[1, 3] = Properties.Resources.blinky4Up;
                frame[1, 4] = Properties.Resources.blinky5Up;
                frame[1, 5] = Properties.Resources.blinky6Up;

                frame[2, 0] = Properties.Resources.blinky1Right;
                frame[2, 1] = Properties.Resources.blinky2Right;
                frame[2, 2] = Properties.Resources.blinky3Right;
                frame[2, 3] = Properties.Resources.blinky4Right;
                frame[2, 4] = Properties.Resources.blinky5Right;
                frame[2, 5] = Properties.Resources.blinky6Right;


                frame[3, 0] = Properties.Resources.blinky1Down;
                frame[3, 1] = Properties.Resources.blinky2Down;
                frame[3, 2] = Properties.Resources.blinky3Down;
                frame[3, 3] = Properties.Resources.blinky4Down;
                frame[3, 4] = Properties.Resources.blinky5Down;
                frame[3, 5] = Properties.Resources.blinky6Down;
            }
            else
                if (tipoDiFantasma == "clyde")
                {
                    frame[0, 0] = Properties.Resources.clyde1Left;
                    frame[0, 1] = Properties.Resources.clyde2Left;
                    frame[0, 2] = Properties.Resources.clyde3Left;
                    frame[0, 3] = Properties.Resources.clyde4Left;
                    frame[0, 4] = Properties.Resources.clyde5Left;
                    frame[0, 5] = Properties.Resources.clyde6Left;

                    frame[1, 0] = Properties.Resources.clyde1Up;
                    frame[1, 1] = Properties.Resources.clyde2Up;
                    frame[1, 2] = Properties.Resources.clyde3Up;
                    frame[1, 3] = Properties.Resources.clyde4Up;
                    frame[1, 4] = Properties.Resources.clyde5Up;
                    frame[1, 5] = Properties.Resources.clyde6Up;

                    frame[2, 0] = Properties.Resources.clyde1Right;
                    frame[2, 1] = Properties.Resources.clyde2Right;
                    frame[2, 2] = Properties.Resources.clyde3Right;
                    frame[2, 3] = Properties.Resources.clyde4Right;
                    frame[2, 4] = Properties.Resources.clyde5Right;
                    frame[2, 5] = Properties.Resources.clyde6Right;

                    frame[3, 0] = Properties.Resources.clyde1Down;
                    frame[3, 1] = Properties.Resources.clyde2Down;
                    frame[3, 2] = Properties.Resources.clyde3Down;
                    frame[3, 3] = Properties.Resources.clyde4Down;
                    frame[3, 4] = Properties.Resources.clyde5Down;
                    frame[3, 5] = Properties.Resources.clyde6Down;
                }
                else
                    if (tipoDiFantasma == "inky")
                    {
                        frame[0, 0] = Properties.Resources.inky1Left;
                        frame[0, 1] = Properties.Resources.inky2Left;
                        frame[0, 2] = Properties.Resources.inky3Left;
                        frame[0, 3] = Properties.Resources.inky4Left;
                        frame[0, 4] = Properties.Resources.inky5Left;
                        frame[0, 5] = Properties.Resources.inky6Left;

                        frame[1, 0] = Properties.Resources.inky1Up;
                        frame[1, 1] = Properties.Resources.inky2Up;
                        frame[1, 2] = Properties.Resources.inky3Up;
                        frame[1, 3] = Properties.Resources.inky4Up;
                        frame[1, 4] = Properties.Resources.inky5Up;
                        frame[1, 5] = Properties.Resources.inky6Up;

                        frame[2, 0] = Properties.Resources.inky1Right;
                        frame[2, 1] = Properties.Resources.inky2Right;
                        frame[2, 2] = Properties.Resources.inky3Right;
                        frame[2, 3] = Properties.Resources.inky4Right;
                        frame[2, 4] = Properties.Resources.inky5Right;
                        frame[2, 5] = Properties.Resources.inky6Right;

                        frame[3, 0] = Properties.Resources.inky1Down;
                        frame[3, 1] = Properties.Resources.inky2Down;
                        frame[3, 2] = Properties.Resources.inky3Down;
                        frame[3, 3] = Properties.Resources.inky4Down;
                        frame[3, 4] = Properties.Resources.inky5Down;
                        frame[3, 5] = Properties.Resources.inky6Down;
                    }
                    else
                        if (tipoDiFantasma == "pinky")
                        {
                            frame[0, 0] = Properties.Resources.pinky1Left;
                            frame[0, 1] = Properties.Resources.pinky2Left;
                            frame[0, 2] = Properties.Resources.pinky3Left;
                            frame[0, 3] = Properties.Resources.pinky4Left;
                            frame[0, 4] = Properties.Resources.pinky5Left;
                            frame[0, 5] = Properties.Resources.pinky6Left;

                            frame[1, 0] = Properties.Resources.pinky1Up;
                            frame[1, 1] = Properties.Resources.pinky2Up;
                            frame[1, 2] = Properties.Resources.pinky3Up;
                            frame[1, 3] = Properties.Resources.pinky4Up;
                            frame[1, 4] = Properties.Resources.pinky5Up;
                            frame[1, 5] = Properties.Resources.pinky6Up;

                            frame[2, 0] = Properties.Resources.pinky1Right;
                            frame[2, 1] = Properties.Resources.pinky2Right;
                            frame[2, 2] = Properties.Resources.pinky3Right;
                            frame[2, 3] = Properties.Resources.pinky4Right;
                            frame[2, 4] = Properties.Resources.pinky5Right;
                            frame[2, 5] = Properties.Resources.pinky6Right;

                            frame[3, 0] = Properties.Resources.pinky1Down;
                            frame[3, 1] = Properties.Resources.pinky2Down;
                            frame[3, 2] = Properties.Resources.pinky3Down;
                            frame[3, 3] = Properties.Resources.pinky4Down;
                            frame[3, 4] = Properties.Resources.pinky5Down;
                            frame[3, 5] = Properties.Resources.pinky6Down;
                        }

            afraidFrame[0, 0] = Properties.Resources.afraidGhost1;
            afraidFrame[0, 1] = Properties.Resources.afraidGhost2;
            afraidFrame[0, 2] = Properties.Resources.afraidGhost3;
            afraidFrame[0, 3] = Properties.Resources.afraidGhost4;
            afraidFrame[0, 4] = Properties.Resources.afraidGhost5;
            afraidFrame[0, 5] = Properties.Resources.afraidGhost6;

            afraidFrame[1, 0] = Properties.Resources.afraidGhostEnd1;
            afraidFrame[1, 1] = Properties.Resources.afraidGhostEnd2;
            afraidFrame[1, 2] = Properties.Resources.afraidGhostEnd4;
            afraidFrame[1, 3] = Properties.Resources.afraidGhostEnd5;
            afraidFrame[1, 4] = Properties.Resources.afraidGhostEnd5;
            afraidFrame[1, 5] = Properties.Resources.afraidGhostEnd6;

            eyes[0] = Properties.Resources.eyesLeft;
            eyes[1] = Properties.Resources.eyesUp;
            eyes[2] = Properties.Resources.eyesRight;
            eyes[3] = Properties.Resources.eyesDown;
        }

        protected virtual void Fine_Afraid(Object sender)
        {
            if (fineAfraid != null)
            {
                fineAfraid(sender, null);
            }
        }
    }

    public class Pacman : Personaggi
    {
        Image[,] frame;
        Timer velocitàAnimazione = new Timer();
        Timer velocitàAnimazioneMorte = new Timer();
        int frameAttuale = 4, frameTotali = 14, apriChiudi = -1;
        bool startPause, vivoMorto = true;

        GraphicsPath circonferenzaPath;

        public Pacman()
        {
            circonferenzaPath = new GraphicsPath();
            circonferenzaPath.AddEllipse(0, 0, 22, 22);

            this.Paint += new PaintEventHandler(Pacman_Paint);
            Svolta = 0;
            impostaVelocitàAnimazione = 60;
            impostaVelocitàAnimazioneMorte = 120;

            caricaFrame();

            associaMetodiAnimazione();
            this.Image = frame[Svolta, frameAttuale];
            this.Size = frame[0, 0].Size;
            this.info = "pacman";
        }

        public Pacman(int direzioneIniziale, int velocitàAnimazione, int velocitàAnimazioneMorte)
            : this()
        {
            Svolta = direzioneIniziale;
            impostaVelocitàAnimazione = velocitàAnimazione;
            impostaVelocitàAnimazioneMorte = velocitàAnimazioneMorte;
        }

        public int impostaVelocitàAnimazione
        {
            get
            {
                return velocitàAnimazione.Intervallo;
            }

            set
            {
                if (value > 0)
                    velocitàAnimazione.Intervallo = value;
                else
                    velocitàAnimazione.Intervallo = 60;
            }
        }

        public int impostaVelocitàAnimazioneMorte
        {
            get
            {
                return velocitàAnimazioneMorte.Intervallo;
            }

            set
            {
                if (value > 0)
                    velocitàAnimazioneMorte.Intervallo = value;
                else
                    velocitàAnimazioneMorte.Intervallo = 120;
            }
        }

        public void Start()
        {
            if (!startPause)
            {
                if (vivoMorto)
                    velocitàAnimazione.StartP();
                else
                    velocitàAnimazioneMorte.StartP();

                startPause = true;
            }
        }

        public void Pause()
        {
            if (startPause)
            {
                velocitàAnimazione.Pause();
                startPause = false;
            }
        }

        public void Muori()
        {
            vivoMorto = false;
            velocitàAnimazione.StopP();
            velocitàAnimazioneMorte.StartP();
        }

        public void Ripristina()
        {
            vivoMorto = true;
            frameAttuale = 0;
            velocitàAnimazione.StartP();
            velocitàAnimazioneMorte.StopP();
        }

        public int VivoMorto
        {
            get
            {
                if (!vivoMorto)
                    return -1;
                else
                    return 0;
            }
        }

        public int StartPause
        {
            get
            {
                if (startPause)
                    return 0;
                else
                    return 1;
            }
        }

        private void associaMetodiAnimazione()
        {
            velocitàAnimazione.Tick += new EventHandler(animazione);
            velocitàAnimazioneMorte.Tick += new EventHandler(animazioneMorte);
        }

        private void animazione(object sender, EventArgs e)
        {
            if (frameAttuale == 0)
                apriChiudi = 1;
            else
                if (frameAttuale == 4)
                    apriChiudi = -1;

            frameAttuale += apriChiudi;

            this.Image = frame[Svolta, frameAttuale % 5];
        }

        private void animazioneMorte(object sender, EventArgs e)
        {
            if (frameAttuale != frameTotali)
            {
                this.Image = frame[Svolta, frameAttuale++ % frameTotali];
                Application.DoEvents();
            }
            else
                velocitàAnimazioneMorte.Stop();
        }

        private void caricaFrame()
        {
            frame = new Image[4, frameTotali];


            frame[0, 0] = Properties.Resources.pacman1Left;
            frame[0, 1] = Properties.Resources.pacman2Left;
            frame[0, 2] = Properties.Resources.pacman3Left;
            frame[0, 3] = Properties.Resources.pacman4Left;
            frame[0, 4] = Properties.Resources.pacman5Left;
            frame[0, 5] = Properties.Resources.pacman6Left;
            frame[0, 6] = Properties.Resources.pacman7Left;
            frame[0, 7] = Properties.Resources.pacman8Left;
            frame[0, 8] = Properties.Resources.pacman9Left;
            frame[0, 9] = Properties.Resources.pacman10Left;
            frame[0, 10] = Properties.Resources.pacman11Left;
            frame[0, 11] = Properties.Resources.pacman12Left;
            frame[0, 12] = Properties.Resources.pacman13Left;
            frame[0, 13] = Properties.Resources.pacman14Left;


            frame[1, 0] = Properties.Resources.pacman1Up;
            frame[1, 1] = Properties.Resources.pacman2Up;
            frame[1, 2] = Properties.Resources.pacman3Up;
            frame[1, 3] = Properties.Resources.pacman4Up;
            frame[1, 4] = Properties.Resources.pacman5Up;
            frame[1, 5] = Properties.Resources.pacman6Up;
            frame[1, 6] = Properties.Resources.pacman7Up;
            frame[1, 7] = Properties.Resources.pacman8Up;
            frame[1, 8] = Properties.Resources.pacman9Up;
            frame[1, 9] = Properties.Resources.pacman10Up;
            frame[1, 10] = Properties.Resources.pacman11Up;
            frame[1, 11] = Properties.Resources.pacman12Up;
            frame[1, 12] = Properties.Resources.pacman13Up;
            frame[1, 13] = Properties.Resources.pacman14Up;


            frame[2, 0] = Properties.Resources.pacman1Right;
            frame[2, 1] = Properties.Resources.pacman2Right;
            frame[2, 2] = Properties.Resources.pacman3Right;
            frame[2, 3] = Properties.Resources.pacman4Right;
            frame[2, 4] = Properties.Resources.pacman5Right;
            frame[2, 5] = Properties.Resources.pacman6Right;
            frame[2, 6] = Properties.Resources.pacman7Right;
            frame[2, 7] = Properties.Resources.pacman8Right;
            frame[2, 8] = Properties.Resources.pacman9Right;
            frame[2, 9] = Properties.Resources.pacman10Right;
            frame[2, 10] = Properties.Resources.pacman11Right;
            frame[2, 11] = Properties.Resources.pacman12Right;
            frame[2, 12] = Properties.Resources.pacman13Right;
            frame[2, 13] = Properties.Resources.pacman14Right;


            frame[3, 0] = Properties.Resources.pacman1Down;
            frame[3, 1] = Properties.Resources.pacman2Down;
            frame[3, 2] = Properties.Resources.pacman3Down;
            frame[3, 3] = Properties.Resources.pacman4Down;
            frame[3, 4] = Properties.Resources.pacman5Down;
            frame[3, 5] = Properties.Resources.pacman6Down;
            frame[3, 6] = Properties.Resources.pacman7Down;
            frame[3, 7] = Properties.Resources.pacman8Down;
            frame[3, 8] = Properties.Resources.pacman9Down;
            frame[3, 9] = Properties.Resources.pacman10Down;
            frame[3, 10] = Properties.Resources.pacman11Down;
            frame[3, 11] = Properties.Resources.pacman12Down;
            frame[3, 12] = Properties.Resources.pacman13Down;
            frame[3, 13] = Properties.Resources.pacman14Down;
        }

        private void Pacman_Paint(object sender, PaintEventArgs e)
        {
            this.Region = new Region(circonferenzaPath);
        }
    }

    public struct datiGhost
    {
        public Timer speed;
        public int speedIniziale;
        public Timer durataMorte;
        public bool vivoMorto;
        public int direzione;
        public Point puntoPartenza;
    }
}
