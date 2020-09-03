using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        ArrayList locationOfMines = new ArrayList(); //List for mines' Locations
        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
                EasyMiddleHard(10);
            if (radioButton2.Checked == true)
                EasyMiddleHard(25);
            if (radioButton3.Checked == true)
                EasyMiddleHard(40);
        }
        public void EasyMiddleHard(int minesNumber)
        {
            flowLayoutPanel1.Controls.Clear();
            locationOfMines.Clear();
            Random rnd = new Random();
            int field = 100;
            int number = 0;
            label1.Text = "Number of Mines: " + minesNumber;

            for (int i = 0; i < minesNumber; i++)
            {
                number = rnd.Next(0, field);
                if (!locationOfMines.Contains(number))  //if the list not contains number, add it;
                    locationOfMines.Add(number);        //else i decrease to complete minesNumber 10,25 or 40.
                else
                    i--; //if I don't do it, for ex; minesNumber 25,  but it fills 23. Because hold random same number.
            }

            for (int i = 0; i < field; i++) // fill FlowLayoutPanel 100 field that include 100 button
            {
                Button btn = new Button();
                btn.Size = new Size(40, 40);
                btn.BackColor = Color.Crimson;
                if (locationOfMines.Contains(i)) // give -1 all of them which take bombs images
                    btn.Tag = -1;
                else
                    btn.Tag = rnd.Next(1, minesNumber); //if minesNumber=10 the buttons take 1-9 number

                btn.Click += Btn_Click; // give Click Event for every button 
                flowLayoutPanel1.Controls.Add(btn);
            }
        }
        int score = 0;
        private void Btn_Click(object sender, EventArgs e)
        {
            Button clicked = (Button)sender;
            if (Convert.ToInt32(clicked.Tag) == -1) // if you find a bomb
            {
                clicked.BackgroundImage = Image.FromFile("bombs.jpg");
                for (int i = 0; i < flowLayoutPanel1.Controls.Count; i++) // find all of bombs
                {
                    flowLayoutPanel1.Controls[i].Enabled = false;
                    if (Convert.ToInt32(flowLayoutPanel1.Controls[i].Tag) == -1)
                        flowLayoutPanel1.Controls[i].BackgroundImage = Image.FromFile("bombs.jpg");
                    else if(flowLayoutPanel1.Controls[i].BackColor != Color.White)
                        flowLayoutPanel1.Controls[i].BackColor = Color.Crimson; // which don't includes bombs open with red backcolor

                }
                MessageBox.Show("Game Over \n Score: " + score); //and finish
                score = 0;
            }
            else
            {
                score += int.Parse(clicked.Tag.ToString());
                clicked.Text = clicked.Tag.ToString();
                clicked.BackColor = Color.White;
                label2.Text = "Score: " + score;
            }
        }
    }
}

