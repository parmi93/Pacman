using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Pacman
{
    public partial class Impostazioni : UserControl
    {
        datiGhost[] datiGhosts;
        Mappa mappaPacman;

        public Impostazioni(object mappaPacman, ref datiGhost[] datiGhosts)
        {
            InitializeComponent();
            this.mappaPacman = (Mappa)mappaPacman;
            this.datiGhosts = datiGhosts;

            CoB_VelocitaPinky.SelectedIndex = CoB_VelocitaInky.SelectedIndex = CoB_VelocitaClyde.SelectedIndex = CoB_VelocitaBlinky.SelectedIndex = CoB_VelocitaPacman.SelectedIndex = 2;
        }

        private void CoB_VelocitaPacman_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CoB_VelocitaPacman.SelectedIndex == 0)
                mappaPacman.pacmanSpeed = 5;
            else
                if (CoB_VelocitaPacman.SelectedIndex == 1)
                    mappaPacman.pacmanSpeed = 16;
                else
                    if (CoB_VelocitaPacman.SelectedIndex == 2)
                        mappaPacman.pacmanSpeed = 29;
                    else
                        if (CoB_VelocitaPacman.SelectedIndex == 3)
                            mappaPacman.pacmanSpeed = 38;
                        else
                            if (CoB_VelocitaPacman.SelectedIndex == 4)
                                mappaPacman.pacmanSpeed = 57;
        }

        private void CoB_VelocitaBlinky_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CoB_VelocitaBlinky.SelectedIndex == 0)
                datiGhosts[0].speed.Intervallo = datiGhosts[0].speedIniziale = 8;
            else
                if (CoB_VelocitaBlinky.SelectedIndex == 1)
                    datiGhosts[0].speed.Intervallo = datiGhosts[0].speedIniziale = 18;
                else
                    if (CoB_VelocitaBlinky.SelectedIndex == 2)
                        datiGhosts[0].speed.Intervallo = datiGhosts[0].speedIniziale = 32;
                    else
                        if (CoB_VelocitaBlinky.SelectedIndex == 3)
                            datiGhosts[0].speed.Intervallo = datiGhosts[0].speedIniziale = 41;
                        else
                            if (CoB_VelocitaBlinky.SelectedIndex == 4)
                                datiGhosts[0].speed.Intervallo = datiGhosts[0].speedIniziale = 60;
        }

        private void CoB_VelocitaClyde_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CoB_VelocitaClyde.SelectedIndex == 0)
                datiGhosts[1].speed.Intervallo = datiGhosts[1].speedIniziale = 9;
            else
                if (CoB_VelocitaClyde.SelectedIndex == 1)
                    datiGhosts[1].speed.Intervallo = datiGhosts[1].speedIniziale = 19;
                else
                    if (CoB_VelocitaClyde.SelectedIndex == 2)
                        datiGhosts[1].speed.Intervallo = datiGhosts[1].speedIniziale = 33;
                    else
                        if (CoB_VelocitaClyde.SelectedIndex == 3)
                            datiGhosts[1].speed.Intervallo = datiGhosts[1].speedIniziale = 42;
                        else
                            if (CoB_VelocitaClyde.SelectedIndex == 4)
                                datiGhosts[1].speed.Intervallo = datiGhosts[1].speedIniziale = 61;
        }

        private void CoB_VelocitaInky_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CoB_VelocitaInky.SelectedIndex == 0)
                datiGhosts[2].speed.Intervallo = datiGhosts[2].speedIniziale = 10;
            else
                if (CoB_VelocitaInky.SelectedIndex == 1)
                    datiGhosts[2].speed.Intervallo = datiGhosts[2].speedIniziale = 20;
                else
                    if (CoB_VelocitaInky.SelectedIndex == 2)
                        datiGhosts[2].speed.Intervallo = datiGhosts[2].speedIniziale = 34;
                    else
                        if (CoB_VelocitaInky.SelectedIndex == 3)
                            datiGhosts[2].speed.Intervallo = datiGhosts[2].speedIniziale = 43;
                        else
                            if (CoB_VelocitaInky.SelectedIndex == 4)
                                datiGhosts[2].speed.Intervallo = datiGhosts[2].speedIniziale = 62;
        }

        private void CoB_VelocitaPinky_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CoB_VelocitaPinky.SelectedIndex == 0)
                datiGhosts[3].speed.Intervallo = datiGhosts[3].speedIniziale = 11;
            else
                if (CoB_VelocitaPinky.SelectedIndex == 1)
                    datiGhosts[3].speed.Intervallo = datiGhosts[3].speedIniziale = 21;
                else
                    if (CoB_VelocitaPinky.SelectedIndex == 2)
                        datiGhosts[3].speed.Intervallo = datiGhosts[3].speedIniziale = 35;
                    else
                        if (CoB_VelocitaPinky.SelectedIndex == 3)
                            datiGhosts[3].speed.Intervallo = datiGhosts[3].speedIniziale = 44;
                        else
                            if (CoB_VelocitaPinky.SelectedIndex == 4)
                                datiGhosts[3].speed.Intervallo = datiGhosts[3].speedIniziale = 63;
        }
    }
}
