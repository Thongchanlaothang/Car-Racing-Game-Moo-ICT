using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Car_Racing_Game_Moo_ICT
{
    public partial class Form1 : Form
    {
        int roadspeed;
        int trafficSpeed;
        int playerSpeed = 12;
        int score;
        int carImage;

        Random rand = new Random();
        Random carPosition = new Random();

        bool goleft, goright;
        public Form1()
        {
            InitializeComponent();
            ResetGame();
        }

       
        private void keyisdown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goleft = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                goright = true;
            }
        }

        private void keyisup(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Left)
            {
                goleft = false ;
            }
            if(e.KeyCode == Keys.Right  )
            {
                goright = false ;
            }
        }

        private async void gameTimerEvent(object sender, EventArgs e)
        {
            label1.Text = "Score:" + score;
            score++;

            if (goleft == true && player.Left > 10 )
            {
                player.Left -= playerSpeed;
            }
            if (goright == true && player .Right < 400)
            {
                player.Left += playerSpeed;
            }

            roadTrack1.Top += roadspeed;
            roadTrack2.Top += roadspeed;

            if (roadTrack2.Top > this .Height )
            {
                roadTrack2.Top = -roadTrack2 .Height ;
            }

            if (roadTrack1.Top > this .Height )
            {
                roadTrack1.Top = -roadTrack1.Height ;
            }
            AI1.Top += trafficSpeed;
            AI2.Top += trafficSpeed;

            if (AI1.Top > this .Height )
            {
                changeAIcars(AI1);
            }

            if (AI2.Top > this .Height )
            {
                changeAIcars(AI2);
            }


            if(player .Bounds .IntersectsWith (AI1 .Bounds ) || player .Bounds.IntersectsWith(AI2 .Bounds ))
            {
                gameOver();
            }

            if (score > 40 && score < 500)
            {
                award.Image = Properties.Resources.bronze;

            }

            if (score > 500 && score < 2000)
            {
                award.Image = Properties.Resources.silver;
                roadspeed = 20;
                trafficSpeed = 22;

            }

            if (score > 2000)
            {
                award.Image = Properties.Resources.gold;
                trafficSpeed = 27;
                roadspeed = 20;
            }
        }
        private void changeAIcars(PictureBox tempCar )
        {
            carImage = rand.Next(1, 9);

            switch (carImage)
            {
                case 1:
                    tempCar.Image = Properties.Resources.ambulance;
                    break;
                case 2:
                    tempCar.Image = Properties.Resources.carGreen;
                    break;
                case 3:
                    tempCar.Image = Properties.Resources.carGrey;
                    break;
                case 4:
                    tempCar.Image = Properties.Resources.carOrange;
                    break;
                case 5:
                    tempCar.Image = Properties.Resources.carPink;
                    break;
                case 6:
                    tempCar.Image = Properties.Resources.CarRed;
                    break;
                case 7:
                    tempCar.Image = Properties.Resources.carYellow;
                    break;
                case 8:
                    tempCar.Image = Properties.Resources.TruckBlue;
                    break;
                case 9:
                    tempCar.Image = Properties.Resources.TruckWhite;
                    break;
            }
            tempCar.Top = carPosition.Next(100,400) *-1;

            if ((string )tempCar .Tag == "carLeft")
            {
                tempCar.Left = carPosition.Next(10, 400);
            }

            if ((string )tempCar .Tag == "carRight")
            {
                tempCar.Left = carPosition.Next(247, 422);
            }
        }
        private async void gameOver()
        {
            playSound();
            gameTimer.Start();
            explosion.Visible = true;
            player.Controls.Add(explosion);
            explosion.Location = new Point(-8, 5);
            explosion.BackColor = Color.Transparent;

            award.Visible = true;
            award.BringToFront();

            btnStart.Enabled = true;

        }
        private async void ResetGame()
        {
            btnStart.Enabled = false;
            explosion.Visible = false;
            award.Visible = false;
            goleft = false;
            goright = false;
            score = 0;
            award.Image = Properties.Resources.bronze;

            roadspeed = 12;
            trafficSpeed = 15;
            AI1.Top = carPosition.Next(200, 500) *-1;
            AI1.Left = carPosition.Next(10, 218);

            AI2.Top = carPosition.Next(200, 500) *-1;
            AI2.Left = carPosition.Next(247,422);

            gameTimer.Start();
        }

       

        private void restartGame(object sender, EventArgs e)
        {
            ResetGame();
        }

       

        private void playSound()
        {
            System.Media.SoundPlayer playCrash = new System.Media.SoundPlayer(Properties .Resources .hit );
            playCrash.Play();

        }
    }
}
