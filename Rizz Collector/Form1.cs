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
        Rectangle player = new Rectangle(530, 340, 40, 10);
        int playerSpeed = 12;

        List<Rectangle> rizz = new List<Rectangle>();
        List<int> rizzSpeed = new List<int>();
        List<string> rizzColor = new List<string>();

        List<Rectangle> rejections = new List<Rectangle>();
        List<int> rejectionsSpeed = new List<int>();
        List<string> rejectionColor = new List<string>();

        int rizzSize = 10;
        int rejectionSize = 10;
        int rizzSpeeds = 5;
        int rejectionSpeed = 5;

        int score = 0;

        int groundHeight = 50;

        bool leftDown = false;
        bool rightDown = false;
        bool escDown = false;

        SolidBrush greenBrush = new SolidBrush(Color.Green);
        SolidBrush redBrush = new SolidBrush(Color.Red);
        SolidBrush pinkBrush = new SolidBrush(Color.HotPink);
        SolidBrush purpleBrush = new SolidBrush(Color.DarkSlateBlue);

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
            if (leftDown == true && player.X > 0)
            {
                player.X -= playerSpeed;
            }

            if (rightDown == true && player.X < this.Width - player.Width)
            {
                player.X += playerSpeed;
            }

            //move rizz
            for (int i = 0; i < rizz.Count; i++)
            {
                int y = rizz[i].Y + rizzSpeeds;
                rizz[i] = new Rectangle(rizz[i].X, y, rizzSize, rizzSize);
            }
            // move rejections 
            for (int i = 0; i < rejections.Count; i++)
            {
                int y = rejections[i].Y + rejectionSpeed;
                rejections[i] = new Rectangle(rejections[i].X, y, rejectionSize, rejectionSize);
            }
            //generate random value
            randValue = randGen.Next(1, 101);
            // generate new rizz/rejection if it is time
            if (randValue < 6)
            {
                rejections.Add(new Rectangle(randGen.Next(0, this.Width - rejectionSize), 0, rejectionSize, rejectionSize));
                rejectionColor.Add("red");
                rizzSpeed.Add(12);
            }
            else if (randValue < 17)
            {
                rizz.Add(new Rectangle(randGen.Next(0, this.Width - rizzSize), 0, rizzSize, rizzSize));
                rizzColor.Add("green");
                rejectionsSpeed.Add(8);
            }
            // remove rizz/rejection if it goes off the screen 
            for (int i = 0; i < rizz.Count; i++)
            {
                if (rizz[i].Y >= this.Height)
                {
                    rizz.RemoveAt(i);
                    rizzColor.RemoveAt(i);
                }
            }

            for (int i = 0; i < rejections.Count; i++)
            {
                if (rejections[i].Y >= this.Height)
                {
                    rejections.RemoveAt(i);
                    rejectionsSpeed.RemoveAt(i);
                    rejectionColor.RemoveAt(i);
                }
            }
            // check for collison between any rizz and player
            for (int i = 0; i < rizz.Count; i++)
            {
                if (player.IntersectsWith(rizz[i]))
                {
                    if (rizzColor[i] == "green")
                    {
                        score += 5;
                        scoreLabel.Text = $"Score: {score}";
                    }

                    rizz.RemoveAt(i);
                    rizzSpeed.RemoveAt(i);
                    rizzColor.RemoveAt(i);
                }
            }
            // check for collison between any rejection and player
            for (int i = 0; i < rejections.Count; i++)
            {
                if (player.IntersectsWith(rejections[i]))
                {
                    if (rejectionColor[i] == "red")
                    {
                        // make lives dissapear 
                    }

                    rejections.RemoveAt(i);
                    rejectionsSpeed.RemoveAt(i);
                    rejectionColor.RemoveAt(i);
                }
            }

            // every time the score gains 30 points display a dating tip
            if (score == 30)
            {
                gameLoop.Stop();
                instructionLabel.Visible = true;
                instructionLabel.Text = "Compliment her \n and mean it\n Press Space bar to Continue";
                
            }
            // end gane if three lives are lost
            if (life1.Visible == false && life2.Visible == false && life3.Visible == false)
            {
                singleEnding.Visible = true;
            }


            Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //game state = waiting
            if (gameState == "waiting")
            {
                scoreLabel.Visible = false;
                livesLabel.Visible = false;
                gameLabel.Text = $"Rizz Collector \n Collect: Green Circles \n Avoid: Red Circles\n Press space bar to begin";
                singleEnding.Visible = false;
                instructionLabel.Text = $"Press Esc to exit Game";
                life1.Visible = false;
                life2.Visible = false;
                life3.Visible = false;
            }
            else if (gameState == "running")
            {
                //update labels 
                scoreLabel.Visible = true;
                livesLabel.Visible = true;
                gameLabel.Visible = false;
                singleEnding.Visible = false;
                instructionLabel.Text = "";
                life1.Visible = true;
                life2.Visible = true;
                life3.Visible = true;

                gameLabel.Enabled = false;
                singleEnding.Enabled = false;
                instructionLabel.Enabled = false;

                //draw a rectangle at the bottom to represent the ground
                e.Graphics.FillRectangle(purpleBrush, 0, this.Height - groundHeight, this.Width, groundHeight);


                //draw player
                e.Graphics.FillRectangle(pinkBrush, player);

                //draw  rizz
                for (int i = 0; i < rizz.Count(); i++)
                {
                    if (rizzColor[i] == "green")
                    {
                        e.Graphics.FillEllipse(greenBrush, rizz[i]);
                    }
                }

                // draw rejections
                for (int i = 0; i < rejections.Count(); i++)
                {
                    if (rejectionColor[i] == "red")
                    {
                        e.Graphics.FillEllipse(redBrush, rejections[i]);
                    }
                }
            }
            else if (gameState == "over")
            {
                scoreLabel.Visible = false;
                livesLabel.Visible = false;
                singleEnding.Visible = true;
                instructionLabel.Visible = true;
            }
        }
    }
}

