using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OX_Project
{
    public partial class Form1 : Form
    {
        strGameStatue GameStatue = new strGameStatue();

        enPlayer PlayerTurn = enPlayer.PlayerOne;

        enum enPlayer
        {
            PlayerOne,
            PlayerTwo
        }
        struct strGameStatue
        {

            public bool GameOver;
            public enWinner Winner;
            public byte PlayRounds;
            public byte GameTimer;

        }
        enum enWinner
        {
            PlayerOne,
            PlayerTwo,
            Draw
        }
        public Form1()
        {
            InitializeComponent();
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;
            button7.Enabled = false;
            button8.Enabled = false;
            button9.Enabled = false;
        }


        void NewGame()
        {
            GameStatue.GameOver = false;
            GameStatue.PlayRounds = 0;
            GameStatue.GameTimer = 10;

            PlayerTurnLabel.Text = "Player One";
            WinnerLabel.Text = "In Progress....";
            TimerLabel.Text = GameStatue.GameTimer.ToString();

            button1.BackgroundImage = Properties.Resources.question_mark_96;
            button2.BackgroundImage = Properties.Resources.question_mark_96;
            button3.BackgroundImage = Properties.Resources.question_mark_96;
            button4.BackgroundImage = Properties.Resources.question_mark_96;
            button5.BackgroundImage = Properties.Resources.question_mark_96;
            button6.BackgroundImage = Properties.Resources.question_mark_96;
            button7.BackgroundImage = Properties.Resources.question_mark_96;
            button8.BackgroundImage = Properties.Resources.question_mark_96;
            button9.BackgroundImage = Properties.Resources.question_mark_96;

            button1.Tag = "?";
            button2.Tag = "?";
            button3.Tag = "?";
            button4.Tag = "?";
            button5.Tag = "?";
            button6.Tag = "?";
            button7.Tag = "?";
            button8.Tag = "?";
            button9.Tag = "?";


            TimerLabel.BackColor = Color.Black;
            TimerLabel.ForeColor = Color.LimeGreen;
        }

        void EndGame()
        {
            PlayerTurnLabel.Text = "Game Over";
            switch (GameStatue.Winner)
            {
                case enWinner.PlayerOne:
                    WinnerLabel.Text = "Player One";
                    break;

                case enWinner.PlayerTwo:
                    WinnerLabel.Text = "Player Two";
                    break;

                default:
                    WinnerLabel.Text = "Draw";
                    break;
            }

            MessageBox.Show("New Game", "New Game", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            ResetGame();

        }
        bool WinCase(Button btn1, Button btn2, Button btn3)
        {
            if (btn1.Tag.ToString() != "?" && btn2.Tag.ToString() == btn1.Tag.ToString() && btn1.Tag.ToString() == btn3.Tag.ToString())
            {
                btn1.BackColor = Color.Coral;
                btn2.BackColor = Color.Coral;
                btn3.BackColor = Color.Coral;

                GameStatue.GameOver = true;

                if (btn1.Tag.ToString() == "X")
                    GameStatue.Winner = enWinner.PlayerOne;
                else
                    GameStatue.Winner = enWinner.PlayerTwo;

                EndGame();
                NewGame();
                btn1.BackColor = Color.Transparent;
                btn2.BackColor = Color.Transparent;
                btn3.BackColor = Color.Transparent;
                return true;
            }
            return false;
        }
        void Winner()
        {
            //Row Cases
            if (WinCase(button1, button2, button3)) return;
            if (WinCase(button4, button5, button6)) return;
            if (WinCase(button7, button8, button9)) return;

            //Column Cases
            if (WinCase(button1, button4, button7)) return;
            if (WinCase(button2, button5, button8)) return;
            if (WinCase(button3, button6, button9)) return;

            //Special Cases
            if (WinCase(button1, button5, button9)) return;
            if (WinCase(button3, button5, button7)) return;
        }

        void ChangeButton(Button btn)
        {

            if (btn.Tag.ToString() != "?")
            {
                MessageBox.Show("Wrong Choice", "Wrong", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ResetRoundTimer();
            switch (PlayerTurn)
            {
                case enPlayer.PlayerOne:
                    btn.BackgroundImage = Properties.Resources.X;
                    PlayerTurnLabel.Text = "Player Two";
                    PlayerTurn = enPlayer.PlayerTwo;

                    btn.Tag = "X";
                    Winner();
                    break;

                case enPlayer.PlayerTwo:
                    btn.BackgroundImage = Properties.Resources.O;
                    PlayerTurnLabel.Text = "Player One";
                    PlayerTurn = enPlayer.PlayerOne;

                    btn.Tag = "O";
                    Winner();

                    break;

            }

            GameStatue.PlayRounds++;

            if (GameStatue.PlayRounds >= 9)
            {
                MessageBox.Show("New Game", "New Game", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                NewGame();
            }

        }



        private void btnClick(object sender, EventArgs e)
        {
            ChangeButton((Button)sender);

        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void RoundTimer(object sender, EventArgs e)
        {
            TimerLabel.Text = GameStatue.GameTimer--.ToString();

            if (GameStatue.GameTimer <= 0)
            {
                switch (PlayerTurn)
                {
                    case enPlayer.PlayerOne:
                        GameStatue.Winner = enWinner.PlayerTwo;
                        break;

                    case enPlayer.PlayerTwo:
                        GameStatue.Winner = enWinner.PlayerOne;
                        break;
                }
                timer1.Enabled = false;
                EndGame();
            }
            if (GameStatue.GameTimer < 4)
            {
                TimerLabel.BackColor = Color.Red;
                TimerLabel.ForeColor = Color.Yellow;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;

            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;
            button6.Enabled = true;
            button7.Enabled = true;
            button8.Enabled = true;
            button9.Enabled = true;
            NewGame();

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetGame();

        }

        void ResetRoundTimer()
        {
            GameStatue.GameTimer = 10;
        }

        void ResetGame()
        {
            timer1.Enabled = false;
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;
            button7.Enabled = false;
            button8.Enabled = false;
            button9.Enabled = false;
            NewGame();
        }
    }
}
