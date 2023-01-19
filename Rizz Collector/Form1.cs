using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// Alana Fortin:January 24th 
// a variation on the catch game we made
// one player
// catches "rizz" and gets dating tips
// if player collides with "rejection" they loose a life
// 3 lives lost and game is over

namespace Rizz_Collector
{
    public partial class form1 : Form
    {
        Rectangle player = new Rectangle(540,200,40,10);
        int playerSpeed = 10;

        List<Rectangle> rizz = new List<Rectangle>();
        List<int> rizzSpeed = new List<int>();
        List<string> rizzColor = new List<string>();

        List<Rectangle> rejections = new List<Rectangle>();
        List<int> rejectionsSpeed = new List<int>();
        List<string> rejectionColor = new List<string>();


        int rizzSpeeds = 10;
        int rejectionSpeed = 10;

        int score = 0;

        int groundHeight = 150;

        bool leftDown = false;
        bool rightDown = false;
        bool escDown = false;

        SolidBrush greenBrush = new SolidBrush(Color.Green);
        SolidBrush redBrush = new SolidBrush(Color.Red);
        SolidBrush pinkBrush = new SolidBrush(Color.HotPink);
        SolidBrush purpleBrush = new SolidBrush(Color.Purple);

        Random randGen = new Random();
        int randValue = 0;

        string gameState = "waiting";

        public form1()
        {
            InitializeComponent();
        }

        public void GameSetup()
        {
            gameState = "running";

            instructionLabel.Visible = false; 

            gameLoop.Enabled = true;
            score = 0;

            player.X = 280;
            rizz.Clear();
            rizzSpeed.Clear();
            rejections.Clear();
            rejectionsSpeed.Clear();
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftDown = false;
                    break;
                case Keys.Right:
                    rightDown = false;
                    break;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftDown = true;
                    break;
                case Keys.Right:
                    rightDown = true;
                    break;
                case Keys.Space:
                    if (gameState == "waiting" || gameState == "over")
                    {
                        GameSetup();
                    }
                    break;
                case Keys.Escape:
                    if (gameState == "waiting" || gameState == "over" || gameState == "running")
                    {
                        this.Close();
                    }
                    break;
            }
        }

        private void gameLoop_Tick(object sender, EventArgs e)
        {
            //move player
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //game state = waiting
            if (gameState == "waiting")
            {
                scoreLabel.Visible = false;
                livesLabel.Visible = false;
         //       bottomBar.Visible = false;
                gameLabel.Visible = true;
                singleEnding.Visible = false;
                instructionLabel.Visible = true;
            }
            else if (gameState == "running")
            {
                //update labels 
                //bottomBar.Visible = true;
                scoreLabel.Visible = true;
                livesLabel.Visible = true;
                gameLabel.Visible = false;
                singleEnding.Visible = false;
                instructionLabel.Visible = false;

                gameLabel.Enabled = false;
                singleEnding.Enabled = false;
                instructionLabel.Enabled = false;

                //draw a rectangle at the bottom to represent the ground
                e.Graphics.FillRectangle(purpleBrush, 0, this.Height - groundHeight, this.Width, groundHeight);


                //draw player
                e.Graphics.FillRectangle(pinkBrush, player);

                //draw  rizz
                //for (int i = 0; i < rizz.Count(); i++)
                //{
                //    if (rizzColor[i] == "green")
                //    {
                //        e.Graphics.FillEllipse(greenBrush, rizz[i]);
                //    }
                //}

                //// draw rejections
                //for (int i = 0; i < rejections.Count(); i++)
                //{
                //    if (rejectionColor[i] == "red")
                //    {
                //        e.Graphics.FillEllipse(redBrush, rejections[i]);
                //    }
                //}
            }
            else if (gameState == "over")
            {
                scoreLabel.Visible = false;
                livesLabel.Visible = false;
          //      bottomBar.Visible = false;
                singleEnding.Visible = true;
                instructionLabel.Visible = true;
            }
        }
    }
}
