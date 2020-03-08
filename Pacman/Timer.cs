using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pacman
{
    public class Timer : System.Windows.Forms.Timer
    {
        System.Diagnostics.Stopwatch stopWatch = new System.Diagnostics.Stopwatch();
        int intervallo, intervallo_temp;
        bool pauseFlag = false, stopFlag = true;

        public Timer()
        {
            this.Tick += reset;
        }

        public int Intervallo
        {
            get
            {
                return intervallo_temp;
            }

            set
            {
                if (value > 0)
                    if (stopFlag)   //imposto un intervallo nuovo solo se il timer è stoppato
                        this.Interval = intervallo = intervallo_temp = value;
                    else
                        intervallo_temp = value;    //altrimenti salvo l'intervallo passatomi in una variabile temporanea
                else
                    throw new Exception("l'intervallo deve essere maggiore di 0!");
            }
        }

        public void StartP()
        {
            if (pauseFlag || stopFlag)  //se sono in pausa oppure sono stoppato allora
            {
                pauseFlag = false;
                stopFlag = false;

                stopWatch.Start();  //avvio il cronometro interno
                this.Start();       //ed avvio il timer
            }
        }

        public void StopP()
        {
            this.Stop();        //stoppo il timer
            stopWatch.Stop();   //stoppo il cronometro interno
            stopWatch.Reset();  //resetto il cronometro interno
            reset(null, null);
            stopFlag = true;    //tengo conto del fatto di essere stoppato
        }

        public void Pause()
        {
            if (!pauseFlag)     //se non sono già in pausa allora
            {
                stopWatch.Stop();   //stoppo il cronometro interno
                this.Stop();        //stoppo il timer
                pauseFlag = true;   //tengo conto del fatto di essere in pausa

                int temp = (int)stopWatch.Elapsed.TotalMilliseconds;    //leggo il tempo strascorso fino ad ora

                if (temp < intervallo)                  //verifico che il tempo trascorso fino ad ora sia minore dell'intervallo del mio timer
                    this.Interval = intervallo - temp;  //imposto un nuovo intervallo. Es. se intervallo = 10, temp = 3, il nuovo interval sarà 7
            }
        }

        private void reset(object sender, EventArgs e)
        {
            stopWatch.Reset();                          //resetto il cronometro interno
            stopWatch.Start();                          //faccio ripartire il cronometro interno
            this.Interval = intervallo = intervallo_temp;
            //reimposto l'intervallo del timer a quello originale, o all'intervallo che è stato passatomi in un momento precedente
        }
    }
}
